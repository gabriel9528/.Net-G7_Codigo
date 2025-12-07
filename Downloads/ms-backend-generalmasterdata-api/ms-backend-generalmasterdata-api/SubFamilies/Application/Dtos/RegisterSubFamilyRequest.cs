using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos
{
    public class RegisterSubFamilyRequest
    {
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public SubFamilyType SubFamilyType { get; set; } = SubFamilyType.DOES_NOT_APPLY;
        public Guid FamilyId { get; set; }
        public int OrderRow { get; set; } = CommonStatic.DefaultOrderRow;
    }
}
