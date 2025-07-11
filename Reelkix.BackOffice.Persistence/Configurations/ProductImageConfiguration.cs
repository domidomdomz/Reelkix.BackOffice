using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reelkix.BackOffice.Domain.Products;

namespace Reelkix.BackOffice.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(i => new { i.ProductId, i.Url }); // Composite key using ProductId and Url

            builder.Property(i => i.Url)
                .IsRequired()
                .HasMaxLength(500); // URL length limit

            builder.Property(i => i.AltText)
                .IsRequired()
                .HasMaxLength(200); // Alt text length limit
            
            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); // Default value for CreatedAt

            builder.Property(i => i.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); // Default value for UpdatedAt

            builder.HasOne<Product>()
                .WithMany(p => p.Images) // One Product can have many images
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the product is deleted

            builder.HasIndex(i => new { i.ProductId, i.SortOrder }); // Index on ProductId and SortOrder for efficient querying
        }
    }
}
