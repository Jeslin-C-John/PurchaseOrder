using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Models
{
    public class ParentModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int PurchaseId { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        public DateTime BillDate { get; set; }

        public int TotalAmount { get; set; }
    }
}
