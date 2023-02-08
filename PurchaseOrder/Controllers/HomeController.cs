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
            int? BillTotal=0;

            for (int i = 0; i < ParentModelObj.PurchaseList.Count; i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 1)
                {
                    BillTotal = BillTotal + ParentModelObj.PurchaseList[i].Amount * ParentModelObj.PurchaseList[i].Quantity;
                }
            }

            DboContext Context = new DboContext();
            UserModel UserInstance = new UserModel()
            {
                UserName=ParentModelObj.UserType.UserName,
                Address=ParentModelObj.UserType.Address,
                Phone=ParentModelObj.UserType.Phone,
            };
            Context.Add(UserInstance);
            Context.SaveChanges();

            var UserDetails = Context.Users
           .Where(s => s.Phone == ParentModelObj.UserType.Phone)
           .ToList();

            BillModel BillInstance= new BillModel()
            {
                BillDate= DateTime.Now,
                UserId= ParentModelObj.UserType.UserId,

            };
            Context.Add(UserInstance);
            Context.SaveChanges();

            for (int i=0;i<ParentModelObj.PurchaseList.Count;i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 1)
                {

                }
            }


            return View("Success");
        }
    }
}
