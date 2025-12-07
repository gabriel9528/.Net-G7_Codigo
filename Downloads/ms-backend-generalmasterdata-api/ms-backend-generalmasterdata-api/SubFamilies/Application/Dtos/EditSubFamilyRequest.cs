using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos
{
    public class EditSubFamilyRequest
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
        public SubFamilyType SubFamilyType { get; set; } = SubFamilyType.DOES_NOT_APPLY;
        public Guid FamilyId { get; set; }
    }
}
