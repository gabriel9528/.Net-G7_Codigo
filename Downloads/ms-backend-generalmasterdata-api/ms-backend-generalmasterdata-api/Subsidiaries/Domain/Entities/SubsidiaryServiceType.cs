using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities
{
    public class SubsidiaryServiceType
    {
        public Guid Id { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public Guid SubsidiaryId { get; set; }
        public Subsidiary Subsidiary { get; set; }

        public SubsidiaryServiceType()
        {

        }
        public SubsidiaryServiceType(Guid serviceTypeId, Guid subsidiaryId)
        {
            ServiceTypeId = serviceTypeId;
            SubsidiaryId = subsidiaryId;
        }
    }
}
