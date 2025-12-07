using AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Dtos
{
	public class IdentityDocumentTypeDto
	{
		public Guid Id { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string Abbreviation { get; set; } = string.Empty;
		public int Length { get; set; }
		public TaxpayerType TaxpayerType { get; set; }
		public IndicatorLength IndicatorLength { get; set; }
		public InputType InputType { get; set; }
		public bool Status { get; set; }
	}
}
