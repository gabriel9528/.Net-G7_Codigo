namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class RegisterSubsidiaryResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LedgerAccount { get; set; } = string.Empty;
        public Guid SubsidiaryTypeId { get; set; }
        public List<Guid>? ServiceTypeId { get; set; }
        public string GeoLocation { get; set; } = string.Empty;
        public string? OfficeHours { get; set; }
        public int Capacity { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid CompanyId { get; set; }
        public bool Status { get; set; }
    }
}
