using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using e_CommerceUsingModelsAndValidation.CustomValidators;

namespace e_CommerceUsingModelsAndValidation.Models
{
    public class Order : IValidatableObject
    {
        [BindNever]
        public int? OrderNo { get; set; }
        [Required]
        [MinimunYear(2000)]
        public DateTime? OrderDate { get; set; }
        [Required]
        public double? InvoicePrice { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "The field 'Products' must contain at least one product")]
        public List<Product> Products { get; set; } = new List<Product>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            double totalPrice = 0.00;
            foreach(Product product in Products)
            {
                totalPrice = Convert.ToDouble(product.Price * product.Quantity);
            }

            if(InvoicePrice != totalPrice)
            {
                yield return new ValidationResult("Invoice Price doesn't match with the total cost of the specified products in the order.", new[] { nameof(InvoicePrice) });
            }
        }
    }
}
