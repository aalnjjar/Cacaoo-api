using ChocolateDelivery.BLL;
using ChocolateDelivery.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateDelivery.UI.Areas.Admin.Controllers;

[Area("Admin")]
public class OccasionController : Controller
{
    private AppDbContext context;
    private readonly IConfiguration _config;
    private IWebHostEnvironment iwebHostEnvironment;
    private string logPath = "";
    OccasionService _categoryService;


    public OccasionController(AppDbContext cc, IConfiguration config, IWebHostEnvironment iwebHostEnvironment)
    {
        context = cc;
        _config = config;
        this.iwebHostEnvironment = iwebHostEnvironment;
        logPath = Path.Combine(this.iwebHostEnvironment.WebRootPath, _config.GetValue<string>("ErrorFilePath")); // "Information"
        _categoryService = new OccasionService(context);
    }

    public IActionResult Create()
    {
        var list_id = Request.Query["List_Id"];
        ViewBag.List_Id = list_id;
        return View();
    }

    // HTTP POST VERSION  
    [HttpPost]
    public IActionResult Create(SM_Occasions category)
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
                    if (category.Image_File != null)
                    {
                        var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + category.Image_File.FileName;
                        var path = AmazonS3Service.UploadToS3(category.Image_File, "occasion", fileName).Result;
                        category.Image_URL = path;
                    }

                    category.Created_By = Convert.ToInt16(user_cd);
                    category.Created_Datetime = StaticMethods.GetKuwaitTime();
                    _categoryService.CreateOccasion(category);
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
            var areaexist = _categoryService.GetOccasion(decryptedId);
            if (areaexist != null && areaexist.Occasion_Id != 0)
            {
                return View("Create", areaexist);
            }
            else
            {
                ModelState.AddModelError("name", "Occasion not exist");
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
    public IActionResult Update(SM_Occasions category, string Id)
    {
        try
        {
            var list_id = Request.Query["List_Id"];
            ViewBag.List_Id = list_id;
            if (ModelState.IsValid)
            {
                var decryptedId = Convert.ToInt32(StaticMethods.GetDecrptedString(Id));
                var areaDM = _categoryService.GetOccasion(decryptedId);
                if (areaDM != null && areaDM.Occasion_Id != 0)
                {
                    var user_cd = HttpContext.Session.GetInt32("UserCd");
                    if (user_cd != null)
                    {
                        if (category.Image_File != null)
                        {
                            var fileName = Guid.NewGuid().ToString("N").Substring(0, 12) + "_" + category.Image_File.FileName;
                            var path = AmazonS3Service.UploadToS3(category.Image_File, "category", fileName).Result;
                            category.Image_URL = path;
                        }

                        category.Occasion_Id = decryptedId;
                        category.Updated_By = Convert.ToInt16(user_cd);
                        category.Updated_Datetime = StaticMethods.GetKuwaitTime();
                        _categoryService.CreateOccasion(category);
                        return Redirect("/List/" + list_id);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Occasion not exist");
                    return View("Create", category);
                }
            }
            else
            {
                return View("Create", category);
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