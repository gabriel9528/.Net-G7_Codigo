using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos
{
    public class EditFieldResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public string SecondCode { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Uom { get; set; } = String.Empty;
        public string? Legend { get; set; } = String.Empty;
        public FieldType FieldType { get; set; }
        public Guid? MedicalFormId { get; set; }
        public int? OrderRow { get; set; }
    }
}
