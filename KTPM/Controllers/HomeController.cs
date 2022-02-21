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
using MVC.Models;
using Microsoft.EntityFrameworkCore;

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
        //add cart
        public IActionResult Add(int id)
        {
            return Add(id, 1);
        }
        [HttpPost]
        public IActionResult Add(int productid, int quantity)
        {
            string username = HttpContext.Session.GetString("AccountUsername");
            int accountid = int.Parse(HttpContext.Session.GetString("AccountId"));
            Cart cart = _context.Carts.FirstOrDefault(c => c.AccountId == accountid && c.ProductId == productid);
            if (cart == null)
            {
                if (quantity < cart.Product.Stock)
                {
                    cart = new Cart();
                    cart.AccountId = accountid;
                    cart.ProductId = productid;
                    cart.Quantity = quantity;
                    _context.Carts.Add(cart);
                }
                else
                {
                    ViewBag.ErrorCartMessenge = "So luong khong du";
                }

            }
            else
            {
                cart.Quantity += quantity;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Cart()
        {

            int accountid = 0;
            if (HttpContext.Session.GetString("AccountId") != null)
            {
                accountid = int.Parse(HttpContext.Session.GetString("AccountId"));
            }

            var shopContext = _context.Carts.Include(c => c.Product).Where(c => c.AccountId == accountid);

            return View(await shopContext.ToListAsync());
        }
        //delete cart
        public IActionResult Delete(int id)
        {
            return Delete(id, 1);
        }

        [HttpPost]
        public IActionResult Delete(int id, int status)
        {
            Cart cart = _context.Carts.FirstOrDefault(c => c.Id == id);

            _context.Carts.Remove(cart);

            _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }
        //update cart
        public IActionResult Update(int id, int quantity)
        {
            return Update(id, quantity);
        }
        [HttpPost]
        public IActionResult Update(int productid, int quantity, int status)
        {
            string username = HttpContext.Session.GetString("AccountUsername");
            int accountid = int.Parse(HttpContext.Session.GetString("AccountId"));
            Cart cart = _context.Carts.FirstOrDefault(c => c.AccountId == accountid && c.ProductId == productid);
            if (quantity < cart.Product.Stock)
            {
                cart.Quantity += quantity;
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
