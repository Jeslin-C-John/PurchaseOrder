using PurchaseOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace PurchaseBilling.Data
{
    public class DboContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=PAVILION;Initial Catalog=PurchaseOrder;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}