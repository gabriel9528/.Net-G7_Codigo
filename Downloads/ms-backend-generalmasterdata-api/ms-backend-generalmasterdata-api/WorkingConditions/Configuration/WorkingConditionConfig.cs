using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.WorkingConditions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.WorkingConditions.Configuration
{
    public class WorkingConditionConfig : IEntityTypeConfiguration<WorkingCondition>
    {
        public void Configure(EntityTypeBuilder<WorkingCondition> builder)
        {
            builder.ToTable("workingConditions").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Status).IsRequired();
        }
    }
}
