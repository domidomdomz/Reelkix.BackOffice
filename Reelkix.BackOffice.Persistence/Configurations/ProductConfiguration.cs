using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reelkix.BackOffice.Domain.Products;

namespace Reelkix.BackOffice.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.ManufacturerId)
                .IsRequired();

            builder.Property(p => p.CostPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.SellingPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsDraft)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Manufacturer)
                .WithMany()
                .HasForeignKey(i => i.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
