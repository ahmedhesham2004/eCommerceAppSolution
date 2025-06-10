using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Infrastructure.Data.Configuration;
internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasData
       (
            new PaymentMethod
            {
                Id = new Guid("977bf7fb-422a-45a3-9d4d-e944895b0663"),
                Name = "Credit Card",
            }
        );
    }
}