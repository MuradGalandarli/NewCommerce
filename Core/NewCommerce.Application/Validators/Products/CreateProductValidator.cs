using FluentValidation;
using NewCommerce.Application.ViewModels.Products;

namespace NewCommerce.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Products>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(150).MinimumLength(5);
            RuleFor(x => x.stock).NotNull().NotEmpty().Must(s => s >= 0);
            RuleFor(x => x.Price).NotNull().NotEmpty().Must(s => s >= 0);

         
        }
    }
}
