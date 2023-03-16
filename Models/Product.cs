using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace product.Models
{
    public class Product
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are       not allowed.")]
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal Price { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "special characters are       not allowed.")]
        public string Color { get; set; }
        public int ProductCategory { get; set; }
        [Required]
        [NotMapped]
        public List<SelectListItem> ProductCategories { get; set; }
    
    }
}
