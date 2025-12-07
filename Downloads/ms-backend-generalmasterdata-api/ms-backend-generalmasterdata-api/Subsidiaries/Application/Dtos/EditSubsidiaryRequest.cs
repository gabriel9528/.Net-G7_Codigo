namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class EditSubsidiaryRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string LedgerAccount { get; set; } = string.Empty;
        public Guid SubsidiaryTypeId { get; set; }
        public List<Guid>? ListServiceTypeId { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public Guid DoctorId { get; set; }
        public List<RegisterOfficeHourRequest>? OfficeHours { get; set; }
        public bool Status { get; set; }
        public string? EmailForAppointment { get; set; } = string.Empty;
        public Guid? CamoDoctorId { get; set; }
        public string? LogoBase64 { get; set; }
        public bool IsDeleteLogo { get; set; } = false;
    }
}
