using first.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace first.ViewModel
{
    public class ProductCategory
    {

        
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        
    }
}
