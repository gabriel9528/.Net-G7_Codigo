using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos
{
    public class RegisterDimensionRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<RegisterCostCenterRequest> costCenters { get; set; }
    }
}
