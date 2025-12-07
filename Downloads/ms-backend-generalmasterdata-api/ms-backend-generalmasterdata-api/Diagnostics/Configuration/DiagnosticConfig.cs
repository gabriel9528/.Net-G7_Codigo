using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Configuration
{
    public class DiagnosticConfig : IEntityTypeConfiguration<Diagnostic>
    {
        public void Configure(EntityTypeBuilder<Diagnostic> builder)
        {

            builder.ToTable("diagnostics").HasKey(k => k.Id);
            builder.Property(p => p.Description).HasColumnName("description");
            builder.Property(p => p.Description2).HasColumnName("description2");
            builder.Property(p => p.DiagnosticOptional).HasColumnName("diagnosticOptional");
            builder.Property(p => p.Cie10).HasColumnName("cie10");
            builder.Property(p => p.Status).HasColumnName("status");



        }
    }
}
