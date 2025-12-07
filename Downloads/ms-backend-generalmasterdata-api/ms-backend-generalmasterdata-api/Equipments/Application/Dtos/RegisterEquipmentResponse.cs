namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos
{
    public class RegisterEquipmentResponse
	{
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public Guid MedicalAreaId { get; set; }
        public Guid SubsidiaryId { get; set; }
        public Guid PersonDeviceManagerId { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public List<string>? FileUrls { get; set; } 
        public bool Status { get; set; }
    }
}
