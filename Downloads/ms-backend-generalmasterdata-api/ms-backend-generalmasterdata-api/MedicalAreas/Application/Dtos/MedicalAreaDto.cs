using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos
{
    public class MedicalAreaDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int OrderRowTourSheet { get; set; } = CommonStatic.DefaultOrderRow;
        public bool Status { get; set; }
    }
}
