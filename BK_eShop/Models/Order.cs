using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Models
{
    public class Order
    {
        // PK
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string OrderStatus { get; set; } = null!;
        [Required]
        public decimal OrderTotalAmount { get; set; }

        //FK
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        // Navigation
        public List<OrderRow> OrderRows { get; set; } = new();

    }
}
