using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Lines.Application.Dtos
{
    public class RegisterLineRequest
    {
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Guid LineTypeId { get; set; }
        public int OrderRow { get; set; } = CommonStatic.DefaultOrderRow;

    }
}
