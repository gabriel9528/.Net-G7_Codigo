using AnaPrevention.GeneralMasterData.Api.Dimensions.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos
{
    public class DimensionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public List<CostCenter>? CostCenters { get; set; }

    }
}
