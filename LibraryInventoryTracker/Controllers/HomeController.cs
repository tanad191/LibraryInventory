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
        var FullBookList = _context.Book.ToList();
        List<Book> FeaturedBooks = new List<Book>();
        var rand = new Random();
        foreach (Book book in FullBookList) {
            if (rand.NextDouble() + 0.25 >= 0.5) {
                FeaturedBooks.Add(book);
            }
            if (FeaturedBooks.Count == 3) { break; }
        }
        return View(FeaturedBooks);
    }
    
    public IActionResult SignIn()
    {
        return View("SignIn");
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
