using eShopSolution.data_.Entities;
using eShopSolution.data_.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.data.Extentions
{
     public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "homeTitle", Value = "this is home page of eShopsolution" },
               new AppConfig() { Key = "homeKeyWord", Value = "this is Keyword of eShopsolution" },
               new AppConfig() { Key = "homeDescription", Value = "this is description of eShopsolution" }

               );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "tiếng Viêt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false }
                );

            modelBuilder.Entity<Category>().HasData(
                 new Category() {
                     Id=1,
                     IsShowOnHome = true,
                     ParentId = null, 
                     SortOrder = 1,
                     Status = Status.Active,
                   
                 },
                 new Category()
                 {
                     Id=2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active,
                  
                 } ) ;

            modelBuilder.Entity<CategoryTranslation>().HasData(
                        new CategoryTranslation(){Id=1, CategoryId=1, Name="Áo nam",LanguageId="vi-VN",SeoAlias="ao-nam",SeoDescription="sản phẩm áo thời trang nam",SeoTitle="sản phẩm áo thời trang nam"},
                        new CategoryTranslation(){Id=2,CategoryId=1, Name="Men shirt",LanguageId="en-US",SeoAlias="Men-shirt",SeoDescription="the Shirt products for men",SeoTitle="the Shirt products for men"},
                         new CategoryTranslation() { Id =3, CategoryId = 2, Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "sản phẩm áo thời trang nu", SeoTitle = "sản phẩm áo thời trang nu" },
                         new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women shirt", LanguageId = "en-US", SeoAlias = "Women-shirt", SeoDescription = "the Shirt products for women", SeoTitle = "the Shirt products for women" }


                        );
        



          modelBuilder.Entity<Product>().HasData(
               new Product() { 
                   Id=1,
                   DateCreated =DateTime.Now,
                   OriginalPrice=100000,
                   Price=20000,Stock=0,
                   ViewCount=0
                 
                
               });

            modelBuilder.Entity<ProductTranslation>().HasData(
                        new ProductTranslation()
                        {
                            Id=1,
                            ProductId=1,
                            Name = "Áo sơ mi trắng Việt Tiến",
                            LanguageId = "vi-VN",
                            SeoAlias = "ao-so-mi-trang-viet-tien",
                            SeoDescription = "Áo sơ mi trắng Việt Tiến",
                            SeoTitle = "Áo sơ mi trắng Việt Tiến",
                            Details = "Áo sơ mi trắng Việt Tiến",
                            Description = "Áo sơ mi trắng Việt Tiến"
                        },
                        new ProductTranslation()
                        {
                            Id=2,
                            ProductId=1,
                            Name = "viet tien Men T-shirt",
                            LanguageId = "en-US",
                            SeoAlias = "viet-tien-Men-T-shirt",
                            SeoDescription = "viet tien Men T-shirt",
                            SeoTitle = "viet tien Men T-shirt",
                            Details = "viet tien Men T-shirt",
                            Description = "viet tien Men T-shirt"
                        }
                );
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId=1,CategoryId=1}
                
                );

            // any guider
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tedu.international@gmail.com",
                NormalizedEmail = "tedu.international@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Toan",
                LastName = "Bach",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
