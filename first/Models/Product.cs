using System.Collections.Generic;

namespace first.Models
{
    public class Picture 
    {
        public int Id { get; set; }
        public string picture { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
   
    public class Product
    {
        
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Picture> Pictures { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int SailedQuantity { get; set; }
        public int QCanBeAdded { get; set; } = 0;
        public int AdminId { get; set; }
        public Admin Admin { get; set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
