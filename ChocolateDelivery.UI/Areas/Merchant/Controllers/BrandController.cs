﻿using ChocolateDelivery.BLL;
using ChocolateDelivery.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateDelivery.UI.Areas.Merchant.Controllers;

[Area("Merchant")]
public class BrandController : Controller
{
    private AppDbContext context;
    private readonly IConfiguration _config;
    private IWebHostEnvironment iwebHostEnvironment;
    private string logPath = "";
    BrandService _brandService;


    public BrandController(AppDbContext cc, IConfiguration config, IWebHostEnvironment iwebHostEnvironment)
    {
        context = cc;
        _config = config;
        this.iwebHostEnvironment = iwebHostEnvironment;
        logPath = Path.Combine(this.iwebHostEnvironment.WebRootPath, _config.GetValue<string>("ErrorFilePath")); // "Information"
        _brandService = new BrandService(context);

    }
    public IActionResult Create()
    {
        var list_id = Request.Query["List_Id"];
        ViewBag.List_Id = list_id;
        return View();
    }

    // HTTP POST VERSION  
    [HttpPost]
    public IActionResult Create(SM_Brands brand)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            if (ModelState.IsValid)
            {


                var vendor_id = HttpContext.Session.GetInt32("VendorId");
                if (vendor_id != null)
                {
                    if (brand.Image_File != null)
                    {
                        var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + brand.Image_File.FileName;
                        
                        var path = AmazonS3Service.UploadToS3(brand.Image_File, "Brands", fileName).Result;

                        brand.Image_URL = path;
                    }
                    brand.Restaurant_Id = Convert.ToInt32(vendor_id);
                    brand.Created_By = Convert.ToInt16(vendor_id);
                    brand.Created_Datetime = StaticMethods.GetKuwaitTime();
                    _brandService.CreateBrand(brand);
                    return Redirect("/Merchant/List/" + list_id);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }



            }
            else
                return View();

        }
        catch (Exception ex)
        {
            /* lblError.Visible = true;
             lblError.Text = "Invalid username or password";*/
            ModelState.AddModelError("name", "Due to some technical error, data not saved");
            Helpers.WriteToFile(logPath, ex.ToString(), true);

        }
        return View();
        /*if (ModelState.IsValid)
        {

            var userDM = userBC.isUserExist(user.User_Id.Trim());
            //go to dashboard page
            return View("Thanks");
        }
        else
            return View();*/

    }

    public IActionResult Update(string Id)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
            var areaexist = _brandService.GetBrand(decryptedId);
            if (areaexist != null && areaexist.Brand_Id != 0)
            {
                return View("Create", areaexist);
            }
            else
            {
                ModelState.AddModelError("name", "Brand not exist");
            }

        }
        catch (Exception ex)
        {
            /* lblError.Visible = true;
             lblError.Text = "Invalid username or password";*/
            ModelState.AddModelError("name", "Due to some technical error, data not saved");
            Helpers.WriteToFile(logPath, ex.ToString(), true);

        }
        return View("Create");
    }

    [HttpPost]
    public IActionResult Update(SM_Brands brand, string Id)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            if (ModelState.IsValid)
            {
                var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
                var areaDM = _brandService.GetBrand(decryptedId);
                if (areaDM != null && areaDM.Brand_Id != 0)
                {

                    var vendor_id = HttpContext.Session.GetInt32("VendorId");
                    if (vendor_id != null)
                    {
                        if (brand.Image_File != null)
                        {
                            var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + brand.Image_File.FileName;
                        
                            var path = AmazonS3Service.UploadToS3(brand.Image_File, "Brands", fileName).Result;

                            brand.Image_URL = path;
                        }
                        brand.Brand_Id = decryptedId;
                        brand.Updated_By = Convert.ToInt16(vendor_id);
                        brand.Updated_Datetime = StaticMethods.GetKuwaitTime();
                        _brandService.CreateBrand(brand);
                        return Redirect("/Merchant/List/" + list_id);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Brand not exist");
                    return View("Create", brand);
                }
            }
            else
            {
                return View("Create", brand);
            }


        }
        catch (Exception ex)
        {
            /* lblError.Visible = true;
             lblError.Text = "Invalid username or password";*/
            ModelState.AddModelError("name", "Due to some technical error, data not saved");
            Helpers.WriteToFile(logPath, ex.ToString(), true);

        }
        return View("Create");
    }
}