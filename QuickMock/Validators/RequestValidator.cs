using FluentValidation;
using QuickMock.Models.Request;

namespace QuickMock.Validators;

public class RequestValidator : AbstractValidator<RequestAddModel>
{
    public RequestValidator()
    {
        RuleFor(x => x.Url)
            .NotNull().NotEmpty().WithMessage("URL is required")
            .Must(x => x == null || !x.StartsWith('/')).WithMessage("Url should not start with '/'");        
        
        RuleFor(x => x.Value)
            .NotNull().NotEmpty().WithMessage("Value is required");
    }
}