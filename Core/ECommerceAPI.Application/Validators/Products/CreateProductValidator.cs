using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Name can not be empty.")
                .NotNull()
                    .WithMessage("Name can not be null.")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Name lenght must be between 5 and 150.");
            RuleFor(p => p.Stock)
                .NotEmpty()
                    .WithMessage("Stock can not be empty.")
                .NotNull()
                    .WithMessage("Stock can not be null.")
                .Must(s => s >= 0)
                    .WithMessage("Stock can not be negative");
            RuleFor(p => p.Price)
                .NotEmpty()
                    .WithMessage("Price can not be empty.")
                .NotNull()
                    .WithMessage("Price can not be null.")
                .Must(s => s >= 0)
                    .WithMessage("Price can not be negative");
        }
    }
}
