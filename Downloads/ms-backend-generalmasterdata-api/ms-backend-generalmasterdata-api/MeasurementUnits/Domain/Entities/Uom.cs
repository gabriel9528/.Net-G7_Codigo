namespace AnaPrevention.GeneralMasterData.Api.MeasurementUnits.Domain.Entities
{
    public class Uom
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string FiscalCode { get; set; }
        public bool Status { get; set; }

        public Uom() { }

        public Uom(string description, string code, string fiscalCode, string abbreviation, Guid id)
        {
            Description = description;
            Code = code;
            Status = true;
            FiscalCode = fiscalCode;
            Abbreviation = abbreviation;
            Id = id;
        }
    }
}
