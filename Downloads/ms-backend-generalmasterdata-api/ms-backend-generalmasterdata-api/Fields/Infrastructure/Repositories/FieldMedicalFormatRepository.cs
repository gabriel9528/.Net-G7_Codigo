using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Infrastructure.Repositories
{
    public class FieldMedicalFormatRepository : Repository<FieldMedicalFormat>
    {
        public FieldMedicalFormatRepository(AnaPreventionContext context) : base(context)
        {
        }
    }
}
