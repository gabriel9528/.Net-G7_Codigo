using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos
{
    public class RegisterServiceTypeRequest
    {
        public ServiceTypeEnum Code { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}
