using eCommerceApp.Application.DTOs;
using FluentValidation;

namespace eCommerceApp.Application.Validation;

public class ValidationService : IValidationService
{
    public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
    {
        var validationResult = await validator.ValidateAsync(model);

        if (validationResult.IsValid)
            return new ServiceResponse { Success = true };

        var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        var errorMessage = string.Join("; ", errors);

        return new ServiceResponse { Message = errorMessage };
    }
}