using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using AnaPrevention.GeneralMasterData.Api.ServiceTypes.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class SubsidiaryDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public string ProvinceId { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LedgerAccount { get; set; } = string.Empty;
        public string SubsidiaryType { get; set; } = string.Empty;
        public Guid SubsidiaryTypeId { get; set; }
        public List<ServiceTypeDto>? ServiceTypes { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Doctor { get; set; } = string.Empty;
        public Guid? DoctorId { get; set; }
        public List<Dictionary<string, string>>? OfficeHours { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
        public string? CamoDoctor { get; set; }
        public Guid? CamoDoctorId { get; set; }
        public string? EmailForAppointment { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
    }
}
