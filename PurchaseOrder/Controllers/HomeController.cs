using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PurchaseBilling.Data;
using PurchaseOrder.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace PurchaseOrder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var DataSource = "Data Source=PAVILION;Initial Catalog=PurchaseOrder;Integrated Security=True;TrustServerCertificate=True";

            var ProductList = new List<ProductModel>();
            using (SqlConnection con = new SqlConnection(DataSource))
            {

                using (SqlCommand cmd = new SqlCommand("ProductList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;




                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        var instance = new ProductModel();

                        instance.ProductId = rdr.GetInt32(0);
                        instance.ProductName = rdr.GetString(1);
                        instance.Price = rdr.GetInt32(2);

                        ProductList.Add(instance);

                    }
                    con.Close();
                }
            }




            ParentModel ParentModelObj = new ParentModel();

            ParentModelObj.ProductList = ProductList;

            return View(ParentModelObj);
        }

        [HttpPost]
        public IActionResult Index(ParentModel? ParentModelObj)
        {

            var DataSource = "Data Source=PAVILION;Initial Catalog=PurchaseOrder;Integrated Security=True;TrustServerCertificate=True";

            int? BillTotal = 0;

            for (int i = 0; i < ParentModelObj.PurchaseList.Count; i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 0)
                {

                    var ProductDetails = new List<ProductModel>();

                    using (SqlConnection con = new SqlConnection(DataSource))
                    {

                        using (SqlCommand cmd = new SqlCommand("ProductDetails", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ProductId", ParentModelObj.PurchaseList[i].ProductId);


                            con.Open();
                            SqlDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {

                                var instance = new ProductModel();
                                instance.ProductId = rdr.GetInt32(0);
                                instance.ProductName = rdr.GetString(1);
                                instance.Price = rdr.GetInt32(2);

                                ProductDetails.Add(instance);

                            }
                            con.Close();
                        }
                    }






                    BillTotal = BillTotal + ProductDetails[0].Price * ParentModelObj.PurchaseList[i].Quantity;
                }
            }



            var UserDetails = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(DataSource))
            {

                using (SqlCommand cmd = new SqlCommand("UserDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Phone", ParentModelObj.UserType.Phone);


                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        var instance = new UserModel();
                        instance.UserId = rdr.GetInt32(0);
                        instance.UserName = rdr.GetString(1);
                        instance.Address = rdr.GetString(2);
                        instance.Phone = rdr.GetString(3);

                        UserDetails.Add(instance);

                    }
                    con.Close();
                }
            }



            if (UserDetails.Count == 0)
            {

                using (SqlConnection con = new SqlConnection(DataSource))
                {
                    using (SqlCommand cmd = new SqlCommand("UserCreate", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserName", ParentModelObj.UserType.UserName);
                        cmd.Parameters.AddWithValue("@Address", ParentModelObj.UserType.Address);
                        cmd.Parameters.AddWithValue("@Phone", ParentModelObj.UserType.Phone);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                }



                using (SqlConnection con = new SqlConnection(DataSource))
                {

                    using (SqlCommand cmd = new SqlCommand("UserDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Phone", ParentModelObj.UserType.Phone);


                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {

                            var instance = new UserModel();
                            instance.UserId = rdr.GetInt32(0);
                            instance.UserName = rdr.GetString(1);
                            instance.Address = rdr.GetString(2);
                            instance.Phone = rdr.GetString(3);

                            UserDetails.Add(instance);

                        }
                        con.Close();
                    }
                }

            }



            using (SqlConnection con = new SqlConnection(DataSource))
            {
                using (SqlCommand cmd = new SqlCommand("BillCreate", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BillDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", UserDetails[0].UserId);
                    cmd.Parameters.AddWithValue("@TotalAmount", BillTotal);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

            }

            var BillDetails = new List<BillModel>();
            using (SqlConnection con = new SqlConnection(DataSource))
            {

                using (SqlCommand cmd = new SqlCommand("BillDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserDetails[0].UserId);


                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        var instance = new BillModel();
                        instance.BillId = rdr.GetInt32(0);
                        instance.BillDate = rdr.GetDateTime(1);
                        instance.UserId = rdr.GetInt32(2);
                        instance.TotalAmount = rdr.GetInt32(3);

                        BillDetails.Add(instance);

                    }
                    con.Close();
                }
            }

            for (int i = 0; i < ParentModelObj.PurchaseList.Count; i++)
            {
                if (ParentModelObj.PurchaseList[i].Quantity > 0)
                {
                    using (SqlConnection con = new SqlConnection(DataSource))
                    {
                        using (SqlCommand cmd = new SqlCommand("PurchaseCreate", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@BillId", BillDetails[0].BillId);
                            cmd.Parameters.AddWithValue("@ProductId", ParentModelObj.PurchaseList[i].ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", ParentModelObj.PurchaseList[i].Quantity);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }

                    }
                }
            }


            return View("Success");
        }
    }
}
