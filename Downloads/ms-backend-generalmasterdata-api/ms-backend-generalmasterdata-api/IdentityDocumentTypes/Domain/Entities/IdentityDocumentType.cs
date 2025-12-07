using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Entities
{
    public class IdentityDocumentType
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public int Length { get; set; }
        public TaxpayerType TaxpayerType { get; set; }
        public IndicatorLength IndicatorLength { get; set; }
        public InputType InputType { get; set; }
        public PersonType PersonType { get; set; }
        public bool Status { get; set; }

        private IdentityDocumentType()
        {
        }

        public IdentityDocumentType(
            string description,
            string code,
            string abbreviation,
            int length,
            TaxpayerType taxpayerType,
            IndicatorLength indicatorLength,
            InputType inputType,
            Guid id,
            PersonType personType = PersonType.BOTH)
        {
            Description = description;
            Code = code;
            Abbreviation = abbreviation;
            TaxpayerType = taxpayerType;
            Length = length;
            IndicatorLength = indicatorLength;
            Status = true;
            InputType = inputType;
            Id = id;
            PersonType = personType;
        }

    }
}
