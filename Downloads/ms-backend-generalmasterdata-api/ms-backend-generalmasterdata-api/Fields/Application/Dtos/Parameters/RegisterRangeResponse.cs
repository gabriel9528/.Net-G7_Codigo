using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class RegisterRangeResponse
    {
        public string Message { get; set; } = string.Empty;
        public string ValueStart { get; set; } = string.Empty;
        public string ValueFinish { get; set; } = string.Empty;
        public RangeType RangeType { get; set; }
    }
}
