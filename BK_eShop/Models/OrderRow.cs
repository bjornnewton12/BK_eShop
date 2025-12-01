using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Models
{
    public class OrderRow
    {
        // PK
        public int OrderRowId { get; set; }
        [Required]
        public int OrderRowQuantity { get; set; }

        // FK
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
