using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;
public class ProductService(IGeneric<Product> productInterface, IMapper mapper) : IProductService
{
    public async Task<ServiceResponse> AddAsync(CreateProductRequest product)
    {
        var mappedDate = mapper.Map<Product>(product);
        int result = await productInterface.AddAsync(mappedDate);

        return (result > 0) ? new ServiceResponse(true, "Product Added!")
            : new ServiceResponse(false, "Product faild to be Added");
    }
    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await productInterface.DeleteAsync(id);
      
        return (result > 0) ? new ServiceResponse(true, "Product deleted!") 
            : new ServiceResponse(false, "Product not fount or faild to be deleted");
    }
    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await productInterface.GetAllAsync();
        if (!products.Any()) return [];

        return mapper.Map<IEnumerable<ProductResponse>>(products);
    }
    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await productInterface.GetByIdAsync(id);
        if (product == null) return new ProductResponse();

        return mapper.Map<ProductResponse>(product);
    }
    public async Task<ServiceResponse> UpdateAsync(UpdateProductRequest product)
    {
        var mappedDate = mapper.Map<Product>(product);
        int result = await productInterface.UpdateAsync(mappedDate);

        return (result > 0) ? new ServiceResponse(true, "Product Updated!")
            : new ServiceResponse(false, "Product faild to be Updated");
    }
}
