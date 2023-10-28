using first.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace first.ViewModel
{
    public class ReviewViewModel
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
  
        public int ProductId { get; set; }
        public ReviewViewModel(int productId, int userId)
        {
            ProductId = productId;
            UserId = userId;
            ReviewDate = DateTime.Now;
        }
        public ReviewViewModel() { }    


    }
}
