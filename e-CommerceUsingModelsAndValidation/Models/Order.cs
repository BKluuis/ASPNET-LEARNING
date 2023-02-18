using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using e_CommerceUsingModelsAndValidation.CustomValidators;
using System.ComponentModel;

namespace e_CommerceUsingModelsAndValidation.Models
{
    public class Order : IValidatableObject
    {
        [BindNever]
        [DisplayName("Order Number")]
        public int? OrderNo { get; set; }
        [Required]
        [DisplayName("Order Date")]
        [MinimunYear(2000)]
        public DateTime? OrderDate { get; set; }
        [Required]
        [DisplayName("Invoice Price")]
        public double? InvoicePrice { get; set; }
        [Required]
        [DisplayName("List of products")]
        [MinLength(1, ErrorMessage = "The field '{0}' must contain at least one product")]
        public List<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            double totalPrice = 0.00;
            foreach(Product product in Products)
            {
                totalPrice += Convert.ToDouble(product.Price * product.Quantity);
            }

            if(InvoicePrice != totalPrice)
            {
                yield return new ValidationResult("Invoice Price doesn't match with the total cost of the specified products in the order.", new[] { nameof(InvoicePrice) });
            }
        }
    }
}
