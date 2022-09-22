using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jober.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jober.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/[controller]")]
    public class UsersController : Controller
    {
        private UserManager< IdentityUser> _UserManager;
        private SignInManager<IdentityUser> _signInManager;

         
        public UsersController(UserManager<IdentityUser> userManager ,SignInManager<IdentityUser> signInManager)
        {
            _UserManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
       
        public IActionResult Index()
        {
            //return View();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Users req)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser { Email = req.Email, UserName = req.UserName };
                IdentityResult result = await _UserManager.CreateAsync(newUser, req.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("error_message", error.Description);

                    return View("Create");
                }

            }
            return RedirectToAction("Create");
        }

        // GET: /<controller>/
        public IActionResult submit([Required,MinLength(2)] string name)
        {
            //return View();
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login(Users req)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult  Signined = await _signInManager.PasswordSignInAsync(req.Email, req.Password, false, false);

                if (Signined.Succeeded)
                {
                    Redirect("Index");
                }
            }
            return View();
        }

        [Authorize]
        public string protectedRoute()
        {
            return "protected";
        }

        [Authorize(Roles ="Admin")]
        public string admin()
        {
            return "protected";
        }

        [Authorize]
        public async Task<IActionResult> Chekifauthorized()
        {

            if(User.Identity!=null && User.Identity.IsAuthenticated)
            {
                //means the user is autheticated;
                IdentityUser currentUser = await _UserManager.FindByNameAsync(User.Identity.Name);
                return View(currentUser);
            }

            return View();
        }
    }
}

