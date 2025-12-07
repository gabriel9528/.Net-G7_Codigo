using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Configuration
{
    public class BusinessContactConfig : IEntityTypeConfiguration<BusinessContact>
    {
        public void Configure(EntityTypeBuilder<BusinessContact> builder)
        {
            builder.ToTable("businessContacts").HasKey(k => k.Id);
            builder.Property(p => p.BusinessId).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(t1 => t1.FirstName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.LastName).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(t1 => t1.Position).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.CellPhone).IsRequired().HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.SecondCellPhone).IsRequired().HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.Phone).IsRequired().HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.SecondPhone).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired(false).IsUnicode(false);
            builder.Property(t1 => t1.Comment).IsRequired(false).IsUnicode(false);
            builder.Property(p => p.Email)
                        .HasConversion(p => p.Value, p => Email.Create(p).Value)
                        .HasColumnName("email").IsRequired(false);
            builder.HasOne(c => c.Business).WithMany().HasForeignKey(c => c.BusinessId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
