using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product;

public class UpdateProductRequest : ProductBase
{
    [Required]
    public Guid Id { get; set; }
}
