using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Models
{
    public class BillModel
    {
        [Key]
        public int? BillId { get; set; }
        public DateTime? BillDate { get; set; }
        public int? UserId { get; set; }

        public int? TotalAmount { get; set; }
    }
}
