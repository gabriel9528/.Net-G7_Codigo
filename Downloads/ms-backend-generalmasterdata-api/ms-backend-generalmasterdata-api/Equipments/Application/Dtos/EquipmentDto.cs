using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos
{
    public class EquipmentDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Datecalibration { get; set; } = string.Empty;
        public DateTime DatecalibrationFormat { get; set; }
        public string NextDatecalibration { get; set; } = string.Empty;
        public Guid MedicalAreaId { get; set; }
        public Guid SubsidiaryId { get; set; }
        public string MedicalArea { get; set; } = string.Empty;
        public MedicalAreaType MedicalAreaType { get; set; }
        public string Subsidiary { get; set; } = string.Empty;
        public Guid PersonDeviceManagerId { get; set; }
        public string PersonDeviceManager { get; set; } = string.Empty;
        public string Supplier { get; set; } = string.Empty;
        public List<EquipmentCalibrationDto>? EquipmentCalibrations { get; set; }
        public List<Attachment>? Attachments { get; set; }
        public bool Status { get; set; }
    }
}
