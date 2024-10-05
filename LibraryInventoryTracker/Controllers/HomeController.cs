using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryInventoryTracker.Models;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryInventoryTracker.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
