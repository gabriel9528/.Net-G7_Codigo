using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities
{
    public class FieldMedicalFormat
    {
        public FieldMedicalFormat(Guid fieldId, Guid medicalFormatId)
        {
            FieldId = fieldId;
            MedicalFormatId = medicalFormatId;
        }

        public Guid FieldId { get; set; }
        public Guid MedicalFormatId { get; set; }
        public MedicalFormat MedicalFormat { get; set; }
        public Field Field { get; set; }
    }
}
