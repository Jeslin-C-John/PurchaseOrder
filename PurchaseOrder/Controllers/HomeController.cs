using Microsoft.AspNetCore.Mvc;
using PurchaseBilling.Data;
using PurchaseOrder.Models;

namespace PurchaseOrder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DboContext Context = new DboContext();

            var ProductList = Context.Products
            .ToList();

            var ProductModelList = new List<Models.ProductModel>();

            for (int i = 0; i < ProductList.Count; i++)
            {
                ProductModel ProductModelObj= new ProductModel();
                ProductModelObj.ProductName = ProductList[i].ProductName;
                ProductModelObj.Price= ProductList[i].Price;
                
                ProductModelList.Add(ProductModelObj);
            }


            return View(ProductModelList);
        }

        [HttpPost]
        public IActionResult Index(List<ProductModel> e)
        {
            
            
            return View();
        }
    }
}
