
using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.DTOs.Category;
public class CategoryResponse : CategoryBase
{
    public Guid Id { get; set; }
    public ICollection<ProductResponse> Products { get; set; } = [];
}
