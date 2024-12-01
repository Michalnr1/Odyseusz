using Microsoft.AspNetCore.Mvc;
using Odyseusz.bll;
using Odyseusz.domain;
using System.Collections.Generic;

namespace Odyseusz.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ProductService _productService;

        public DatabaseController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
    }
}
