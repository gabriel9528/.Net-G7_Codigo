//using AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Dtos;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos
{
    public class MedicalAreaMinDto
    {
        public Guid MedicalAreaId { get; set; }
        public string MedicalArea { get; set; } = string.Empty;
        //public List<MedicalFormAttentionMinDto> MedicalForms { get; set; } = new();
    }
}
