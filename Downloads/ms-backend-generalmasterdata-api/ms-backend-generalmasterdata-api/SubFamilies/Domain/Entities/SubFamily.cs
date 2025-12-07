using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.SubFamilies.Domain.Entities
{
    public class SubFamily
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Company? Company { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public Guid FamilyId { get; set; }
        public Family Family { get; set; }
        public SubFamilyType SubFamilyType { get; set; }
        public int OrderRow { get; set; }
        private SubFamily()
        {

        }

        public SubFamily(string description, string code, Guid companyId, Guid familyId, Guid id, SubFamilyType subFamilyType = SubFamilyType.DOES_NOT_APPLY, int orderRow = CommonStatic.DefaultOrderRow)
        {
            Code = code;
            Description = description;
            Status = true;
            CompanyId = companyId;
            FamilyId = familyId;
            SubFamilyType = subFamilyType;
            Id = id;
            OrderRow = orderRow;
        }
    }
}
