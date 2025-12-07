using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class RegisterRangeHeaderRequest
    {
        public Guid? GenderId { get; set; }
        public Guid? SubsidiaryId { get; set; }
        public ParameterType ParameterType { get; set; }
        public List<RegisterRangeRequest>? Details { get; set; }
    }
}
