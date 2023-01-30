﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseOrder.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public string? Phone { get; set; }
        [NotMapped]
        public string? Address { get; set; }
    }
}
