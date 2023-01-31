using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Models
{
    public class ParentModel
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        
        public int Quantity { get; set; }
        
        public string? Name { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Address { get; set; }
    }
}
