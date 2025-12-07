using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enum;

namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities
{
    public class MedicalFormat
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public MedicalFormatType MedicalFormatType { get; set; }
        public bool Status { get; set; }

        public MedicalFormat() { }

        public MedicalFormat(string description, string code, MedicalFormatType medicalFormatType, Guid id)
        {
            Description = description;
            Code = code;
            Status = true;
            MedicalFormatType = medicalFormatType;
            Id = id;
        }
    }
}
