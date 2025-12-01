using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Models
{
    public class Product
    {
        // PK
        public int ProductId { get; set; }
        [Required, MaxLength(150)]
        public string ProductName { get; set; } = null!;
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public int ProductStock { get; set; }

        // FK
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<OrderRow> OrderRows { get; set; } = null!;
    }
}
