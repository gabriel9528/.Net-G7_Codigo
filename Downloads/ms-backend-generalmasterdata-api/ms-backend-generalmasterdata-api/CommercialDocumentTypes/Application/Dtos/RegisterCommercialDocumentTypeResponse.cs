namespace AnaPrevention.GeneralMasterData.Api.CommercialDocumentTypes.Application.Dtos
{
    public class RegisterCommercialDocumentTypeResponse
    {
		public Guid Id { get; set; }

		public string Description { get; set; } = string.Empty;

		public string Code { get; set; } = string.Empty;

		public string Abbreviation { get; set; } = string.Empty;

		public bool SalesDocument { get; set; }

		public bool PurchaseDocument { get; set; }

		public bool GetSetDocument { get; set; }

		public bool Status { get; set; }
	}
}
