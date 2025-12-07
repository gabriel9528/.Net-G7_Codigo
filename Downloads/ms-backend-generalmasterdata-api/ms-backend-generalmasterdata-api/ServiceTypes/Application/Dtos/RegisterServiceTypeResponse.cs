using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos
{
    public class RegisterServiceTypeResponse
    {
        public Guid Id { get; set; }

        public ServiceTypeEnum Code { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public Guid CompanyId { get; set; }
    }
}
