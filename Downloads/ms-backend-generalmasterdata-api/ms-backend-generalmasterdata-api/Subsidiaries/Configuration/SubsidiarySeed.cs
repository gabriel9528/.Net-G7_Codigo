using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Configuration
{
    public class SubsidiarySeed : IEntityTypeConfiguration<Subsidiary>
    {
        public void Configure(EntityTypeBuilder<Subsidiary> builder)
        {
            builder.HasData(new Subsidiary("Sede One health", "0001", "150101", "", Guid.Parse("81D84F23-D7CE-4BEC-8D55-4D733B8F95DF"), Guid.Parse("721B327E-82BE-4345-AC30-3C980B804F3D"), Guid.Parse("4B6A5C95-14B3-4FBD-A743-7CA9FB8B7F81"), "552-556747"));
        }
    }
}
