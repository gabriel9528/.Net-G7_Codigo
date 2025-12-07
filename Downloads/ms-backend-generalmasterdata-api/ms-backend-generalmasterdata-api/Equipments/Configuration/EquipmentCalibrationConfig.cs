using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Configuration
{
    public class EquipmentCalibrationConfig : IEntityTypeConfiguration<EquipmentCalibration>
    {
        public void Configure(EntityTypeBuilder<EquipmentCalibration> builder)
        {
            builder.ToTable("equipmentCalibrations").HasKey(k => k.Id);
            builder.Property(p => p.Datecalibration).IsRequired();
            builder.Property(p => p.NextDatecalibration).IsRequired();
            builder.Property(p => p.EquipmentId).HasColumnName("equipment_id");
            builder.HasOne(c => c.Equipment).WithMany().HasForeignKey(c => c.EquipmentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
