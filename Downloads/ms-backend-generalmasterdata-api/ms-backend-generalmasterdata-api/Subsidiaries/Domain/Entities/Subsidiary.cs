using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Doctors.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.GeographicLocations.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.SubsidiaryTypes.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Domain.Entities
{
    public class Subsidiary
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public District District { get; set; }
        public string DistrictId { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string LedgerAccount { get; set; }
        public SubsidiaryType SubsidiaryType { get; set; }
        public Guid SubsidiaryTypeId { get; set; }
        public string GeoLocation { get; set; }
        public int Capacity { get; set; }
        public Doctor? Doctor { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public bool Status { get; set; }
        public string? OfficeHours { get; set; }
        public Doctor? CamoDoctor { get; set; }
        public Guid? CamoDoctorId { get; set; }
        public string? DistributionListEmail { get; set; }
        public string? DistributionListEmaillaboratory { get; set; }
        public string? EmailForAppointment { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }

        public Subsidiary() { }

        public Subsidiary(
            string description,
            string code,
            string districtId,
            string ledgerAccount,
            string address,
            Guid subsidiaryTypeId,
            string geoLocation,
            int capacity,
            Guid doctorId,
            string officeHours,
            Guid companyId,
            Guid id,
            string? phoneNumber,
            string? emailForAppoiment,
            Guid? camoDoctorId,
            string? logoUrl)
        {
            Description = description;
            Code = code;
            DistrictId = districtId;
            LedgerAccount = ledgerAccount;
            Address = address;
            SubsidiaryTypeId = subsidiaryTypeId;
            GeoLocation = geoLocation;
            Capacity = capacity;
            DoctorId = doctorId;
            OfficeHours = officeHours;
            CompanyId = companyId;
            Status = true;
            Id = id;
            PhoneNumber = phoneNumber;
            EmailForAppointment = emailForAppoiment;
            CamoDoctorId = camoDoctorId;
            LogoUrl = logoUrl;
        }

        public Subsidiary(
            string description,
            string code,
            string districtId,
            string address,
            Guid subsidiaryTypeId,
            Guid companyId,
            Guid id,
            string? phoneNumber
            )
        {
            Description = description;
            Code = code;
            DistrictId = districtId;
            LedgerAccount = "";
            Address = address;
            SubsidiaryTypeId = subsidiaryTypeId;
            GeoLocation = "";
            Capacity = 0;
            CompanyId = companyId;
            Status = true;
            Id = id;
            PhoneNumber = phoneNumber;
        }

    }
}
