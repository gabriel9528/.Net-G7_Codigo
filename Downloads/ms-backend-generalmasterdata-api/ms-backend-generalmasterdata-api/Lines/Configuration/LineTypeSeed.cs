using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Configuration
{
    public class LineTypeSeed : IEntityTypeConfiguration<LineType>
    {
        public void Configure(EntityTypeBuilder<LineType> builder)
        {
            List<LineType> DataInitial = new()
            {
                new LineType(CodeLineType.LABORATORY_EXAM, "Examenes de Laboratorio", Guid.Parse("fa25b52e-0252-4f79-a690-6b0ce8ebc154")),
                new LineType(CodeLineType.MEDICAL_EXAM, "Examenes de Medico", Guid.Parse("5600564f-1660-4b90-b5a4-423e11bdb3d6")),
            };
            builder.HasData(DataInitial);
        }
    }
}
