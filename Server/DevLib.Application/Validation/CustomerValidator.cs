using FluentValidation;
using DevLib.Domain.CustomerAggregate;

namespace DevLib.Application.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(customer => customer.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Address is required.");

        RuleFor(customer => customer.Phone)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be valid.");

        RuleFor(customer => customer.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email must be valid.");

        RuleFor(customer => customer.LinkedInUrl)
            .NotEmpty()
            .Must(IsValidUrl)
            .WithMessage("LinkedIn URL must be a valid URL.");

        RuleFor(customer => customer.FacebookUrl)
            .NotEmpty()
            .Must(IsValidUrl)
            .WithMessage("Facebook URL must be a valid URL.");

        RuleFor(customer => customer.InstagramUrl)
            .NotEmpty()
            .Must(IsValidUrl)
            .WithMessage("Instagram URL must be a valid URL.");

        RuleFor(customer => customer.TwitterUrl)
            .NotEmpty()
            .Must(IsValidUrl)
            .WithMessage("Twitter URL must be a valid URL.");

        RuleFor(customer => customer.WebsiteUrl)
            .NotEmpty()
            .Must(IsValidUrl)
            .WithMessage("Website URL must be a valid URL.");
    }

    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
