using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(c => c.Color).HasMaxLength(7).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(30).IsRequired();

        builder.HasData(
          new Category(1, "Lazer", "#c0eb34"),
          new Category(2, "Vestuário", "#eb9334"),
          new Category(3, "Mercado", "#ebdc34"),
          new Category(4, "Saúde", "#349ceb")
        );
    }
}
