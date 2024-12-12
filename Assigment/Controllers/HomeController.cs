using System.Diagnostics;
using Assigment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assigment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var res=_appDbContext.Users.ToList();   
            return View(res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User? user)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (user.Password == user.RePassword)
                    {
                        _appDbContext.Users.Add(user);
                        _appDbContext.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Password is not Repassword.");
                    }
                }

            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User? users)
        {
            User? res = _appDbContext.Users.FirstOrDefault((u => u.Email == users.Email));
            if (res == null)
            {

                ModelState.AddModelError("Error", "Error tray agin invalid username or password");
                return View();
            }
            if (res.Password != users.Password)
            {
                ModelState.AddModelError("Error", "Error tray agin invalid username or password");
                return View();
            }
           

            int id = res.Id;
            if(res.Admin==1)
            {

                return RedirectToAction("Privacy", "Home", new { id });
            }
            return RedirectToAction("Index", "Home", new { id });
        }
        public IActionResult Update(int id)
        {
            User user=_appDbContext.Users.FirstOrDefault(x=>x.Id == id);    
            return View(user);
        }
        [HttpPost]
        public IActionResult Update(User user)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Error tray agin invalid");
                return View();
            }
            if(user.Password!=user.RePassword)
            {
                ModelState.AddModelError("Error", "Error tray agin invalid password");
                return View();

            }
            _appDbContext.Users.Update(user);
            _appDbContext.SaveChanges();
            return RedirectToAction("Privacy", "Home");
           
        }
        public IActionResult Delete(int id)
        {
            User user = _appDbContext.Users.FirstOrDefault(x => x.Id == id);
            return View(user);
        }
        [HttpPost]
          public IActionResult Delete(User user)
          {
            int res = _appDbContext.Users.Count();
            if(res==1)
            {
                
             return RedirectToAction("Privacy", "Home");
            }
            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
            return RedirectToAction("Privacy", "Home");

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User? user)
        {
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (user.Password == user.RePassword)
                    {
                        _appDbContext.Users.Add(user);
                        _appDbContext.SaveChanges();
                        return RedirectToAction("Privacy");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Password is not Repassword.");
                    }
                }

            }
            return View();
        }

    }
}
