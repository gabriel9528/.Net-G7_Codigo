using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Infrastructure.Repositories
{
    public class EquipmentCalibrationRepository(AnaPreventionContext context) : Repository<EquipmentCalibration>(context)
    {
    }
}
