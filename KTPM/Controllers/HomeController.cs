using KTPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVC.Data;

namespace KTPM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopContext _context;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            /*if(HttpContext.Request.Cookies.ContainsKey("AccountUsername"))
            {
                ViewBag.AccountUsername = HttpContext.Request.Cookies["AccountUsername"].ToString(); 
            }  */
            if (HttpContext.Session.Keys.Contains("AccountUsername"))
            {
                ViewBag.AccountUsername = HttpContext.Session.GetString("AccountUsername");
            }
            var prd = _context.Products.ToList();
            return View(prd);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
