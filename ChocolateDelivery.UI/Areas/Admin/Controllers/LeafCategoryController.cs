using ChocolateDelivery.BLL;
using ChocolateDelivery.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateDelivery.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class LeafCategoryController : Controller
{
    private AppDbContext context;
    private readonly IConfiguration _config;
    private IWebHostEnvironment iwebHostEnvironment;
    private string logPath = "";
    LeafCategoryService _leafCategoryService;


    public LeafCategoryController(AppDbContext cc, IConfiguration config, IWebHostEnvironment iwebHostEnvironment)
    {
        context = cc;
        _config = config;
        this.iwebHostEnvironment = iwebHostEnvironment;
        logPath = Path.Combine(this.iwebHostEnvironment.WebRootPath, _config.GetValue<string>("ErrorFilePath")); // "Information"
        _leafCategoryService = new LeafCategoryService(context);
    }

    public IActionResult Create()
    {
        var list_id = Request.Query["List_Id"];
        ViewBag.List_Id = list_id;
        return View();
    }

    // HTTP POST VERSION  
    [HttpPost]
    public IActionResult Create(SM_Leaf_Categories leafCategory)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            if (ModelState.IsValid)
            {
                var user_cd = HttpContext.Session.GetInt32("UserCd");
                if (user_cd != null)
                {
                    if (leafCategory.Image_File != null)
                    {
                        var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + leafCategory.Image_File.FileName;
                        var path = AmazonS3Service.UploadToS3(leafCategory.Image_File, "category", fileName).Result;
                        leafCategory.Image_URL = path;
                    }

                    leafCategory.Created_By = Convert.ToInt16(user_cd);
                    leafCategory.Created_Datetime = StaticMethods.GetKuwaitTime();
                    _leafCategoryService.CreateLeafCategory(leafCategory);
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
            var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
            var areaexist = _leafCategoryService.GetLeafCategory(decryptedId);
            if (areaexist != null && areaexist.Sub_Category_Id != 0)
            {
                return View("Create", areaexist);
            }
            else
            {
                ModelState.AddModelError("name", "SubCategory not exist");
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
    public IActionResult Update(SM_Leaf_Categories leafCategory, string Id)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            if (ModelState.IsValid)
            {
                var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
                var areaDM = _leafCategoryService.GetLeafCategory(decryptedId);
                if (areaDM != null && areaDM.Sub_Category_Id != 0)
                {
                    var user_cd = HttpContext.Session.GetInt32("UserCd");
                    if (user_cd != null)
                    {
                        if (leafCategory.Image_File != null)
                        {
                            var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + leafCategory.Image_File.FileName;
                            var path = AmazonS3Service.UploadToS3(leafCategory.Image_File, "category", fileName).Result;
                            leafCategory.Image_URL = path;
                        }

                        leafCategory.Sub_Category_Id = decryptedId;
                        leafCategory.Updated_By = Convert.ToInt16(user_cd);
                        leafCategory.Updated_Datetime = StaticMethods.GetKuwaitTime();
                        _leafCategoryService.CreateLeafCategory(leafCategory);
                        return Redirect("/List/" + list_id);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "SubCategory not exist");
                    return View("Create", leafCategory);
                }
            }
            else
            {
                return View("Create", leafCategory);
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