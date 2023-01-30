using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Models
{
    public class PurchaseModel
    {
        [Key]
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime BillDate { get; set; }
        public int UserID { get; set; }
    }
}
