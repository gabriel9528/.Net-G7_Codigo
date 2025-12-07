using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Persons.Configuration
{
    public class DegreeInstructionConfig : IEntityTypeConfiguration<DegreeInstruction>
    {
        public void Configure(EntityTypeBuilder<DegreeInstruction> builder)
        {
            builder.ToTable("degreeInstructions").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasMaxLength(CommonStatic.DescriptionMaxLength).IsRequired().IsUnicode(false);
            builder.Property(p => p.Code).IsRequired();
            //builder.HasData(new List<DegreeInstruction>()
            //{
            //    new("Universitario Completo",DegreeInstructionType.COMPLETE_UNIVERSITY,Guid.NewGuid()),
            //    new("Universitario incompleto",DegreeInstructionType.INCOMPLETE_UNIVERSITY,Guid.NewGuid()),
            //    new("Técnico Completo",DegreeInstructionType.COMPLETE_TECHNICIAN,Guid.NewGuid()),
            //    new("Técnico incompleto",DegreeInstructionType.INCOMPLETE_TECHNICAL,Guid.NewGuid()),
            //    new("Secundaria Completo",DegreeInstructionType.COMPLETE_SECONDARY,Guid.NewGuid()),
            //    new("Secundaria incompleto",DegreeInstructionType.INCOMPLETE_SECONDARY,Guid.NewGuid()),
            //    new("Primaria Completo",DegreeInstructionType.COMPLETE_PRIMARY,Guid.NewGuid()),
            //    new("Primaria incompleto",DegreeInstructionType.INCOMPLETE_PRIMARY,Guid.NewGuid()),
            //    new("Analfabeto",DegreeInstructionType.ILLITERATE,Guid.NewGuid()),
            //});

        }
    }
}
