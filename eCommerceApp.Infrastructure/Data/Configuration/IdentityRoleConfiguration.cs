using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Data.Configuration;
public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new IdentityRole
            {
                Id = "0191d311-2918-7f76-bd8a-0bded8535075",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "0191d311-2918-7f76-bd8a-0bdf740adff8",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}