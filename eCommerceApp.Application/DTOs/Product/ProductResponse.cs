using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.DTOs.Product;
public class ProductResponse : ProductBase
{
    public Guid Id { get; set; }
    public CategoryResponse? Category { get; set; }
}
