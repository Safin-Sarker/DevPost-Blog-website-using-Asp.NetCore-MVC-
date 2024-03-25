using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Seed Roles(User,Admin,SuperAdmin)

            var adminRoleId = "3ba3eb98-345b-4b9f-8765-f6f7e4fdf7b9";
            var superAdminRoleId = "21997512-e0ca-4e3c-8652-73f18f4ae078";
            var userRoleId = "44da380e-5279-40c1-bc09-8fb1120dccb7";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole 
                {
                    Name= "SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },

                new IdentityRole
                {
                    Name= "User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser
            var superAdminId = "6d316e81-c77e-4bb3-af38-8a70fc8fc28b";
            var superAdminUser = new IdentityUser
            {
                UserName="superadmin@bloggie.com",
                Email="superadmin@bloggie.com",
                NormalizedEmail="superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id= superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    RoleId=adminRoleId ,
                    UserId= superAdminId ,
                },

                new IdentityUserRole<string>()
                {
                    RoleId= superAdminRoleId,
                    UserId= superAdminId ,
                },

                new IdentityUserRole<string>()
                {
                    RoleId= userRoleId,
                    UserId= superAdminId ,
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
