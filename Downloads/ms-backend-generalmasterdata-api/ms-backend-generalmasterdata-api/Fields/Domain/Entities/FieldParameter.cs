using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Fields.Domain.Entities
{
    public class FieldParameter
    {
        public Guid Id { get; set; }
        public Guid FieldId { get; set; }
        public Field? Field { get; set; }
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public string DefaultValue { get; set; }
        public string? Legend { get; set; }
        public string RangeJson { get; set; }
        public string? Uom { get; set; }
        public string? OptionsJson { get; set; }
        public bool IsMandatory { get; set; }
        public bool Show { get; set; }
        public bool Status { get; set; }
        public FieldParameter()
        {

        }

        public FieldParameter(string defaultValue, string? legend, string rangeJson, string? uom, bool isMandatory, Guid fieldId, Guid? genderId, bool show, string? optionsJson, Guid id)
        {
            Status = true;
            DefaultValue = defaultValue;
            Legend = legend;
            RangeJson = rangeJson;
            Uom = uom;
            IsMandatory = isMandatory;
            FieldId = fieldId;
            GenderId = genderId;
            Show = show;
            OptionsJson = optionsJson;
            Id = id;
        }
    }
}
