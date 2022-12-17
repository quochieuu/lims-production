using LIMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LIMS.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Administrator role"
            },
            new Role
            {
                Id = new Guid("92a170c6-118c-45c9-053a-08d83b9c9ecb"),
                Name = "Blogger",
                NormalizedName = "BLOGGER",
                Description = "Content Writter"
            },
            new Role
            {
                Id = new Guid("aa6f243a-5cbc-42d5-a432-08d83b5447b1"),
                Name = "User",
                NormalizedName = "USER",
                Description = "Member"
            },
            new Role
            {
                Id = new Guid("939528cd-a6f3-450b-053b-08d83b9c9ecb"),
                Name = "Seller",
                NormalizedName = "SELLER",
                Description = "Sales Collatorabor"
            });

            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "root@gmail.com",
                NormalizedUserName = "ROOT@GMAIL.COM",
                Email = "root@gmail.com",
                NormalizedEmail = "ROOT@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc123!@#"),
                SecurityStamp = string.Empty,
                LastName = "root",
                FirstName = "root",
                UrlAvatar = "admin-avt.jpg"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

        }
    }
}
