using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace e_CommerceUsingModelsAndValidation.Models
{
    public class Product
    {
        [Required]
        [DisplayName("Product Code")]
        public int? ProductCode { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }

    }
}
