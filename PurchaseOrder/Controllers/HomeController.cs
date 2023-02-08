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
            DboContext Context = new DboContext();

            int? BillTotal = 0;

            for (int i = 0; i < ParentModelObj.PurchaseList.Count; i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 0)
                {
                    var ProductDetails = Context.Products
                        .Where(s => s.ProductId == ParentModelObj.PurchaseList[i].ProductId)
                        .ToList();

                    BillTotal = BillTotal + ProductDetails[0].Price * ParentModelObj.PurchaseList[i].Quantity;
                }
            }

            var UserDetails = Context.Users
           .Where(s => s.Phone == ParentModelObj.UserType.Phone)
           .ToList();

            if (UserDetails.Count == 0)
            {

                UserModel UserInstance = new UserModel()
                {
                    UserName = ParentModelObj.UserType.UserName,
                    Address = ParentModelObj.UserType.Address,
                    Phone = ParentModelObj.UserType.Phone,
                };
                Context.Add(UserInstance);
                Context.SaveChanges();

                UserDetails = Context.Users
               .Where(s => s.Phone == ParentModelObj.UserType.Phone)
               .ToList();

            }

            BillModel BillInstance = new BillModel()
            {
                BillDate = DateTime.Now,
                UserId = UserDetails[0].UserId,
                TotalAmount = BillTotal

            };
            Context.Add(BillInstance);
            Context.SaveChanges();

            var BillDetails = Context.Bills
           .Where(s => s.UserId == UserDetails[0].UserId)
           .OrderByDescending(s => s.BillId)
           .ToList();

            for (int i = 0; i < ParentModelObj.PurchaseList.Count; i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 0)
                {
                    PurchaseModel PurchaseInstance = new PurchaseModel()
                    {
                        BillId = BillDetails[0].BillId,
                        ProductId = ParentModelObj.PurchaseList[i].ProductId,
                        Quantity = ParentModelObj.PurchaseList[i].Quantity
                    };
                    Context.Add(PurchaseInstance);
                    Context.SaveChanges();
                }
            }


            return View("Success");
        }
    }
}
