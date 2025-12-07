//using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities
{
    public class ServiceType
    {
        public Guid Id { get; set; }
        public ServiceTypeEnum Code { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }

        //public Company Company { get; set; }

        public ServiceType()
        {

        }

        public ServiceType(string description, ServiceTypeEnum code, Guid companyId, Guid id)
        {
            Code = code;
            Description = description;
            Status = true;
            CompanyId = companyId;
            Id = id;
        }
    }
}
