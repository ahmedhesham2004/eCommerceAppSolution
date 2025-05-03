using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;

namespace eCommerceApp.Application.Services.Implementations;

public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResponse> AddAsync(CreateCategoryRequest category)
    {
        var mappedDate = mapper.Map<Category>(category);
        int result = await categoryInterface.AddAsync(mappedDate);

        return (result > 0) ? new ServiceResponse(true, "Category Added!")
            : new ServiceResponse(false, "Category faild to be Added");
    }
    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        int result = await categoryInterface.DeleteAsync(id);
        
        return (result > 0) ? new ServiceResponse(true, "Category deleted!")
            : new ServiceResponse(false, "Category not fount or faild to be deleted");
    }
    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categorys = await categoryInterface.GetAllAsync();
        if (!categorys.Any()) return [];

        return mapper.Map<IEnumerable<CategoryResponse>>(categorys);
    }
    public async Task<CategoryResponse> GetByIdAsync(Guid id)
    {
        var category = await categoryInterface.GetByIdAsync(id);
        if (category == null) return new CategoryResponse();

        return mapper.Map<CategoryResponse>(category);
    }
    public async Task<ServiceResponse> UpdateAsync(UpdateCategoryRequest category)
    {
        var mappedDate = mapper.Map<Category>(category);
        int result = await categoryInterface.UpdateAsync(mappedDate);

        return (result > 0) ? new ServiceResponse(true, "Category Updated!")
            : new ServiceResponse(false, "Category faild to be Updated");
    }
}
