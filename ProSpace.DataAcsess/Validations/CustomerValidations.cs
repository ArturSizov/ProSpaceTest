using FluentValidation;
using ProSpace.Domain.Models;
using ProSpace.Infrastructure.Properties;
using System.Text.RegularExpressions;

namespace ProSpace.Infrastructure.Validations
{
    public class CustomerValidations : AbstractValidator<CustomerModel>
    {
        private readonly Regex _regex = new(@"\d{4}-\d{4}");

        public CustomerValidations()
        {
            RuleFor(x => x.Name)
               .NotEmpty();

            RuleFor(i => i.Code)
                .NotEmpty()
                .Matches(_regex).WithMessage($"'Code' {Resources.FormatErrorCustomerCode}");
        }
    }
}
