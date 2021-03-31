using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.data_.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopSolution.data_.Configuration
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(t => new {t.CategoryId,t.ProductId});

            builder.ToTable("ProductInCategory");

            builder.HasOne(t => t.Product).WithMany(p => p.ProductInCategories)
                .HasForeignKey(p=>p.ProductId);

            builder.HasOne(t => t.Category).WithMany(p => p.ProductInCategories)
                 .HasForeignKey(p => p.CategoryId);
        }
    }
}
