using System.Diagnostics;
using System.Security.Policy;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryInventoryTracker.Data;
using LibraryInventoryTracker.Models;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryInventoryTracker.Controllers;

public class HomeController : Controller
{
    private readonly LibraryInventoryTrackerContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, LibraryInventoryTrackerContext context = null)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult SignIn()
    {
        return View("SignIn");
    }
    
    public IActionResult SignUp()
    {
        List<SelectListItem> categoryList = new List<SelectListItem>();
        categoryList.Add(new SelectListItem{Value="0",Text="Customer"});
        categoryList.Add(new SelectListItem{Value="1",Text="Librarian"});

        ViewData.Add("Category", categoryList);

        return View("SignUp");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult SignIn(User user)
    {
        var obj = _context.User.Where(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password)).FirstOrDefault();
        if (obj != null)
        {
            //FormsAuthentication.SetAuthCookie(user.UserID,false);
            HttpContext.Session.SetString("UserID", obj.UserID.ToString());
            HttpContext.Session.SetString("UserName", obj.UserName.ToString());
            HttpContext.Session.SetString("Category", obj.Category.ToString());
            HttpContext.Session.SetString("LoggedOn", "True");
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "The User ID or password are incorrect.");
        }
        return View(user);
    }
    public ActionResult SignOut()
    {
        //FormsAuthentication.SignOut();
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
