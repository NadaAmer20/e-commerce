using System.Collections.Generic;

namespace first.Models
{
    public class Category
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int TotalProducts { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
