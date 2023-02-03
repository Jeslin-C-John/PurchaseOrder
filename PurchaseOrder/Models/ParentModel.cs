using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PurchaseOrder.Models
{
    public class ParentModel
    {
        public List<ProductModel>? ProductList { get; set; }
        public List<PurchaseModel>? PurchaseList { get; set; }
        public UserModel? UserType { get; set; }
        public BillModel? BillType { get; set; }
    }
}
