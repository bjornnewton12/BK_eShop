using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Models
{
    public class Customer
    {
        // PK
        public int CustomerId { get; set; }

        [Required, MaxLength(150)]
        public string CustomerName { get; set; } = null!;

        [Required]
        public int CustomerPhone { get; set; }

        [Required, MaxLength(150)]
        public string CustomerEmail { get; set; } = null!;

    }
}
