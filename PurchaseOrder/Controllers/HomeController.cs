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

            var ParentModelList = new List<Models.ParentModel>();

            for (int i = 0; i < ProductList.Count; i++)
            {
                ParentModel ParentModelObj= new ParentModel();
                ParentModelObj.ProductName = ProductList[i].ProductName;
                ParentModelObj.Price= ProductList[i].Price;
                ParentModelObj.ProductId= ProductList[i].ProductId;    
                
                ParentModelList.Add(ParentModelObj);
            }


            return View(ParentModelList);
        }

        [HttpPost]
        public IActionResult Index(List<ParentModel> model)
        {
            var submittedList = new List<ParentModel>();
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].Quantity > 0)
                {
                    submittedList.Add(new ParentModel
                    {
                        ProductId = model[i].ProductId,
                        ProductName = model[i].ProductName,
                        Price = model[i].Price,
                        Quantity = model[i].Quantity,
                        Name = model[i].Name,
                        Phone = model[i].Phone,
                        Address = model[i].Address
                    });
                }
            }
            return View();
        }
    }
}
