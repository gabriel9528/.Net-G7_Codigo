using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Application.Dtos
{
    public class SubFamilyDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public SubFamilyType SubFamilyType { get; set; }
        public bool Status { get; set; }
        public Guid FamilyId { get; set; }
        public string FamilyDescription { get; set; } = String.Empty;
    }
}
