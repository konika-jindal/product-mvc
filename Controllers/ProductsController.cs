using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using product.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace product.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly Context _context;


        public ProductsController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.KProduct.ToListAsync());
        }

        public async Task<IActionResult> Electronics()
        {
            return View(await _context.KProduct.ToListAsync());
        }
        public async Task<IActionResult> Stationary()
        {
            return View(await _context.KProduct.ToListAsync());
        }
        public async Task<IActionResult> Apparels()
        {
            return View(await _context.KProduct.ToListAsync());
        }
       
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.KProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            var product = new Product { Id = 0, Name = "", Price = 0, Color = "",ProductCategories = new List<SelectListItem>()};

            var categories = _context.ProductCategory.ToList();
            foreach(var item in categories)
            {
                product.ProductCategories.Add(new SelectListItem
                { Text = item.Pname, Value = Convert.ToString(item.Id)}) ;

            }
            return View(product);
        }
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {

                _context.Add(products);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            
        }

        
        public async Task<IActionResult> EditAsync(int id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.KProduct.FindAsync(id);
            products.ProductCategories = new List<SelectListItem>();
            var categories = _context.ProductCategory.ToList();
            foreach (var item in categories)
            {
                products.ProductCategories.Add(new SelectListItem
                { Text = item.Pname, Value = Convert.ToString(item.Id) });

            }
            if (products == null)
            {
                return NotFound();
            }
            return View("~/Views/Products/Create.cshtml", products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product products)
        {


            try
            {
                _context.Update(products);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(products.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                return RedirectToAction(nameof(Index));
           
        }

        public async Task<IActionResult> Delete(int? id)
        {

            var products = await _context.KProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            _context.KProduct.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductsExists(int id)
        {
            return _context.KProduct.Any(e => e.Id == id);
        }


        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Accessing");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
