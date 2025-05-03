using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateCategoryRequest category);
    Task<ServiceResponse> UpdateAsync(UpdateCategoryRequest category);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
