using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Families.Application.Dtos
{
    public class RegisterFamilyRequest
    {
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Guid LineId { get; set; }
        public int OrderRow { get; set; } = CommonStatic.DefaultOrderRow;
    }
}
