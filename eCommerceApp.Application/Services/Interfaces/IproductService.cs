using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Application.Services.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<ServiceResponse> AddAsync(CreateProductRequest product);
    Task<ServiceResponse> UpdateAsync(UpdateProductRequest product);
    Task<ServiceResponse> DeleteAsync(Guid id);
}
