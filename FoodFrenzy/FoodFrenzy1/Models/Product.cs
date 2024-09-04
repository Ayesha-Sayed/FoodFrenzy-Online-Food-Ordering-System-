using System.ComponentModel.DataAnnotations;

namespace FoodFrenzy1.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string? ProductPicture { get; set; }
    }
}
