using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Configuration
{
    public class UomConfig : IEntityTypeConfiguration<Uom>
    {
        public void Configure(EntityTypeBuilder<Uom> builder)
        {
            builder.ToTable("measurementUnits").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired();
            builder.Property(p => p.FiscalCode).HasMaxLength(CommonStatic.CodeMaxLength).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            
        }
    }
}
