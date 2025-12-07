using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Settings.Domain.Enums;


namespace AnaPrevention.GeneralMasterData.Api.MedicalForms.Application.Dtos
{
    public class MedicalFormByPrintDto
    {
        public string Description { get; set; } = string.Empty;
        public List<MedicalFormsType>? MedicalFormsTypes { get; set; }
       public List<OrderFileType>? OrderFileType { get; set; }
    }
}
