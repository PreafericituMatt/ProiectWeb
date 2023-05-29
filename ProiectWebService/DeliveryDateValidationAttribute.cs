using ProiectWebService.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ProiectWebService
{
    public class DeliveryDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var order = validationContext.ObjectInstance as OrderDetailsDto;

            var time = DateTime.Now;

            if (order.DeliveryDate < time.AddDays(3))
            {
                return new ValidationResult("Delivery date must be greater than 3 days");
            }
               
            return ValidationResult.Success;
        }
    }
}
