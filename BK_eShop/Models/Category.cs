using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Models
{
    public class Category
    {
        // PK
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}
}
