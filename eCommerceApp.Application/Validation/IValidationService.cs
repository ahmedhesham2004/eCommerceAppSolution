using eCommerceApp.Application.DTOs;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerceApp.Application.Validation;
public interface IValidationService
{
    Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
}
