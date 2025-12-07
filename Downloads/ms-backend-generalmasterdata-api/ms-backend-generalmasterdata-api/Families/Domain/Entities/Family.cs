using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities
{
    public class Family
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public Guid LineId { get; set; }
        public Line Line { get; set; }
        public int OrderRow { get; set; }

        private Family()
        {

        }

        public Family(string description, string code, Guid companyId, Guid lineId, Guid id, int orderRow = CommonStatic.DefaultOrderRow)
        {
            Code = code;
            Description = description;
            Status = true;
            CompanyId = companyId;
            LineId = lineId;
            Id = id;
            OrderRow = orderRow;
        }
    }
}
