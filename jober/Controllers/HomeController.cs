using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using jober.Models;
using jober.Data;

namespace jober.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly dbContext _db;

    public HomeController(ILogger<HomeController> logger, dbContext db)
    {
        _logger = logger;
        _db = db;
    }

    

    public IActionResult Index()
    {
        //lkst all items
        IEnumerable<posts> allPost = _db.posts.ToList();
        return View(allPost);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
        public IActionResult Search( string search)
    {
        if(string.IsNullOrEmpty(search))
            {
            TempData["error"] = "You didnt provide a search string";
            return RedirectToAction("Index", "Home");
        }
        //search db

        IEnumerable<posts> result = _db.posts.Where(p => p.title.Contains(search));
        Console.WriteLine(result);
        return View(result);
    }
    public IActionResult Details(int? id)

    {
        if (id == null)
        {
            return NotFound();
        }
        var result = _db.posts.FirstOrDefault(a => a.id == id);

        if(result == null)
        {
            return NotFound();
        }
        ViewBag.result = result;
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
