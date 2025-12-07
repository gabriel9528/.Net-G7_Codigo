using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Families.Domain.Entities
{
    public class Line
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public Guid LineTypeId { get; set; }
        public LineType LineType { get; set; }
        public int OrderRow { get; set; }

        private Line()
        {

        }

        public Line(string description, string code, Guid companyId, Guid lineTypeId, Guid id, int orderRow = CommonStatic.DefaultOrderRow)
        {
            Code = code;
            Description = description;
            Status = true;
            CompanyId = companyId;
            LineTypeId = lineTypeId;
            Id = id;
            OrderRow = orderRow;
        }
    }
}
