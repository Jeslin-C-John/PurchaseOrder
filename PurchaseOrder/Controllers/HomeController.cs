using Microsoft.AspNetCore.Mvc;
using PurchaseBilling.Data;
using PurchaseOrder.Models;
using System.Runtime.CompilerServices;

namespace PurchaseOrder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DboContext Context = new DboContext();

            var ProductList = Context.Products
            .ToList();

            
            ParentModel ParentModelObj = new ParentModel();

            ParentModelObj.ProductList = ProductList;

            return View(ParentModelObj);
        }

        [HttpPost]
        public IActionResult Index(ParentModel? ParentModelObj)
        {
            




            return View("Success");
        }
    }
}
