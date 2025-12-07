namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Entities
{
    public class CommercialDocumentType
    {

		public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string Abbreviation	{ get; set; } = string.Empty;
		public bool SalesDocument { get; set; }
		public bool PurchaseDocument { get; set; }
		public bool GetSetDocument { get; set; }
		public bool Status { get; set; }

		private CommercialDocumentType()
		{
		}

		public CommercialDocumentType(
			string description,
			string code,
			string abbreviation,
			bool purchaseDocument,
			bool salesDocument,
			bool getSetDocument,
			Guid id)
		{
			Description = description;
			Code = code;
			Abbreviation = abbreviation;
			PurchaseDocument = purchaseDocument;
			SalesDocument = salesDocument;
			GetSetDocument = getSetDocument;
			Status = true;
			Id = id;
		}

	}
}
