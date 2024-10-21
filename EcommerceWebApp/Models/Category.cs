using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Range(1,100, ErrorMessage = "Display Order Must be Between 1 to 100")]
        public int DisplayOrder { get; set; }

		public ICollection<Product>? Products { get; set; }
	}
}
