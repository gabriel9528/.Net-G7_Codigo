using AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class SubsidiaryMinDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
