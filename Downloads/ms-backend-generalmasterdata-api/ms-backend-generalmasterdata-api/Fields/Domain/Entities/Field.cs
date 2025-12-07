using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Lines.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string? SecondCode { get; set; }
        public string Name { get; set; }
        public string Uom { get; set; }
        public string? Legend { get; set; }
        public string? OptionsJson { get; set; }
        public FieldType FieldType { get; set; }
        public MedicalFormSubType MedicalFormSubType { get; set; }
        public Guid? MedicalFormId { get; set; }
        public MedicalForm? MedicalForm { get; set; }
        public bool Status { get; set; }
        public CreateType CreateType { get; set; }
        public int OrderRow { get; set; }
        public CodeLineType FieldExamenType { get; set; }
        public string? ReferenceValuesJson { get; set; }
        public bool IsforAllFormats { get; set; }
        public FieldLabelType IsTittle { get; set; }

        public Field() { }

        public Field(string description, string code, string name, string uom, string? legend, FieldType fieldType, int orderRow, string? optionsJson, string? referenceValuesJson, Guid id, string? secondCode, bool isforAllFormats, FieldLabelType isTittle)
        {
            Status = true;
            CreateType = CreateType.DINAMIC;
            FieldExamenType = CodeLineType.LABORATORY_EXAM;
            Description = description;
            Code = code;
            Name = name;
            Uom = uom;
            Legend = legend;
            OptionsJson = optionsJson;
            FieldType = fieldType;
            MedicalFormSubType = MedicalFormSubType.NONE;
            MedicalFormId = null;
            OrderRow = orderRow;
            ReferenceValuesJson = referenceValuesJson;
            Id = id;
            SecondCode = secondCode;
            IsforAllFormats = isforAllFormats;
            IsTittle = isTittle;
        }
    }
}
