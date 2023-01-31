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
                        Quantity = model[i].Quantity,
                        Name = model[0].Name,
                        Phone = model[0].Phone,
                        Address = model[0].Address
                    });
                }
            }


            


            DboContext Context = new DboContext();
            UserModel User = new UserModel();
            User.UserName = submittedList[0].Name;
            User.Address = submittedList[0].Address;
            User.Phone = submittedList[0].Phone;
            Context.Add(User);
            Context.SaveChanges();

            var ProductList = Context.Users
            .Where(s => s.UserName== User.UserName)
            .ToList();

            var UserId = ProductList[0].UserId;

            for (int i = 0; i < submittedList.Count; i++)
            {
                PurchaseModel Purchase = new PurchaseModel();
                Purchase.ProductId = submittedList[i].ProductId;
                Purchase.Quantity = submittedList[i].Quantity;
                Purchase.UserID = UserId;
                Purchase.BillDate=DateTime.Now;
                Context.Add(Purchase);
                Context.SaveChanges();
            }
            



            return View("Success");
        }
    }
}
