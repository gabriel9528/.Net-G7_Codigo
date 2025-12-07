namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Application.Dtos
{
    public class EditMedicalAreaResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
