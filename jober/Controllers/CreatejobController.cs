using System;
using System.Collections.Generic;
using System.Linq;
using jober.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using jober.Data;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jober.Controllers
{
    public class CreatejobController : Controller
    {
        private dbContext _db;

        public CreatejobController(dbContext db)
        {
            _db = db;
        }
       

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //post
        [HttpPost]
        [ActionName("Indexx")]
        public IActionResult submit(posts obj)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(obj);
                Console.WriteLine(json);
               
                _db.posts.Add(new posts { title=obj.title,body=obj.body});
                _db.SaveChanges();
                TempData["success"] = "Created successfully";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["error"] = "an error occured";
                return RedirectToAction("Index");
                
            }
         
        }

       



    }
}

