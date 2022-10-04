using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace restaurantWebApp.Data.Configration
{
    public class RoleConfigration: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
        }
    }
}
