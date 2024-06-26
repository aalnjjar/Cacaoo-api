﻿using ChocolateDelivery.BLL;
using ChocolateDelivery.DAL;
using ChocolateDelivery.UI.Areas.Merchant.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateDelivery.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class ChefController : Controller
{
    private AppDbContext context;
    private readonly IConfiguration _config;
    private IWebHostEnvironment iwebHostEnvironment;
    private string logPath = "";
    ChefService _chefService;


    public ChefController(AppDbContext cc, IConfiguration config, IWebHostEnvironment iwebHostEnvironment)
    {
        context = cc;
        _config = config;
        this.iwebHostEnvironment = iwebHostEnvironment;
        logPath = Path.Combine(this.iwebHostEnvironment.WebRootPath, _config.GetValue<string>("ErrorFilePath")); // "Information"
        _chefService = new ChefService(context);
    }

    public IActionResult Create()
    {
        var list_id = Request.Query["List_Id"];
        ViewBag.List_Id = list_id;
        var type = Chef_Types.CHEF;
        if (!string.IsNullOrEmpty(Request.Query["Type"]))
        {
            type = Convert.ToInt16(Request.Query["Type"]);
        }

        ViewBag.Type = type;
        if (type == Chef_Types.CHEF)
        {
            ViewBag.TypeName = "Chef";
            ViewBag.Placeholder = "Enter Chef Name";
        }
        else
        {
            ViewBag.TypeName = "Boutique";
            ViewBag.Placeholder = "Enter Boutique Name";
        }

        return View();
    }

    // HTTP POST VERSION  
    [HttpPost]
    public IActionResult Create(SM_Chefs chef)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            var type = Chef_Types.CHEF;
            if (!string.IsNullOrEmpty(Request.Query["Type"]))
            {
                type = Convert.ToInt16(Request.Query["Type"]);
            }

            ViewBag.Type = type;
            if (type == Chef_Types.CHEF)
            {
                ViewBag.TypeName = "Chef";
                ViewBag.Placeholder = "Enter Chef Name";
            }
            else
            {
                ViewBag.TypeName = "Boutique";
                ViewBag.Placeholder = "Enter Boutique Name";
            }

            if (ModelState.IsValid)
            {
                var user_cd = HttpContext.Session.GetInt32("UserCd");
                if (user_cd != null)
                {
                    if (chef.Image_File != null)
                    {
                        var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + chef.Image_File.FileName;
                        var path = AmazonS3Service.UploadToS3(chef.Image_File, "category", fileName).Result;
                        chef.Image_URL = path;
                    }

                    chef.Type_Id = type;
                    chef.Created_By = Convert.ToInt16(user_cd);
                    chef.Created_Datetime = StaticMethods.GetKuwaitTime();
                    _chefService.CreateChef(chef);

                    foreach (var detail in chef.SM_Chef_Products)
                    {
                        detail.Chef_Id = chef.Chef_Id;
                        _chefService.CreateChefProduct(detail);
                    }

                    return Redirect("/List/" + list_id);
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
            var type = Chef_Types.CHEF;
            if (!string.IsNullOrEmpty(Request.Query["Type"]))
            {
                type = Convert.ToInt16(Request.Query["Type"]);
            }

            ViewBag.Type = type;
            if (type == Chef_Types.CHEF)
            {
                ViewBag.TypeName = "Chef";
                ViewBag.Placeholder = "Enter Chef Name";
            }
            else
            {
                ViewBag.TypeName = "Boutique";
                ViewBag.Placeholder = "Enter Boutique Name";
            }

            var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
            var areaexist = _chefService.GetChef(decryptedId);
            if (areaexist != null && areaexist.Chef_Id != 0)
            {
                return View("Create", areaexist);
            }
            else
            {
                ModelState.AddModelError("name", "Chef not exist");
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
    public IActionResult Update(SM_Chefs chef, string Id)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            var type = Chef_Types.CHEF;
            if (!string.IsNullOrEmpty(Request.Query["Type"]))
            {
                type = Convert.ToInt16(Request.Query["Type"]);
            }

            ViewBag.Type = type;
            if (type == Chef_Types.CHEF)
            {
                ViewBag.TypeName = "Chef";
                ViewBag.Placeholder = "Enter Chef Name";
            }
            else
            {
                ViewBag.TypeName = "Boutique";
                ViewBag.Placeholder = "Enter Boutique Name";
            }

            if (ModelState.IsValid)
            {
                var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
                var areaDM = _chefService.GetChef(decryptedId);
                if (areaDM != null && areaDM.Chef_Id != 0)
                {
                    var user_cd = HttpContext.Session.GetInt32("UserCd");
                    if (user_cd != null)
                    {
                        if (chef.Image_File != null)
                        {
                            var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + chef.Image_File.FileName;
                            var path = AmazonS3Service.UploadToS3(chef.Image_File, "category", fileName).Result;
                            chef.Image_URL = path;
                        }

                        chef.Chef_Id = decryptedId;
                        chef.Updated_By = Convert.ToInt16(user_cd);
                        chef.Updated_Datetime = StaticMethods.GetKuwaitTime();
                        _chefService.CreateChef(chef);

                        foreach (var detail in chef.SM_Chef_Products)
                        {
                            detail.Chef_Id = chef.Chef_Id;
                            _chefService.CreateChefProduct(detail);
                        }

                        return Redirect("/List/" + list_id);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Chef not exist");
                    return View("Create", chef);
                }
            }
            else
            {
                return View("Create", chef);
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
    public JsonResult DeleteChefProduct(AddOnDeleteRequest request)
    {
        var response = new AddOnDeleteResponse();
        try
        {
            var isDeleted = _chefService.DeleteChefProduct(request.Detail_Id);
            if (isDeleted)
            {
                response.Status = 0;
                response.Message = ServiceResponse.Success;
            }
            else
            {
                response.Status = 101;
                response.Message = "Chef Product Not found";
            }
        }
        catch (Exception ex)
        {
            Helpers.WriteToFile(logPath, ex.ToString(), true);
            response.Status = 1;
            response.Message = ServiceResponse.ServerError;
        }

        return Json(response);
    }
}