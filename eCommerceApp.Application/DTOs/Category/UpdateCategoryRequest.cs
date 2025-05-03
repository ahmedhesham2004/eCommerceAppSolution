using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Category;

public class UpdateCategoryRequest : CategoryBase
{
    [Required]
    public Guid Id { get; set; }
}

