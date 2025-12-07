using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Enums;

namespace AnaPrevention.GeneralMasterData.Api.MedicalAreas.Domain.Entities
{
    public class MedicalArea
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public int OrderRowTourSheet { get; set; }
        public string? Icon { get; set; } = String.Empty;
        public int OrderRowAttention { get; set; }
        public MedicalAreaType MedicalAreaType { get; set; }

        public MedicalArea() { }

        public MedicalArea(string description, string code, Guid id, int orderRowTourSheet = CommonStatic.DefaultOrderRow, string? icon = null, int orderRowAttention = 0, MedicalAreaType medicalAreaType = MedicalAreaType.None)
        {
            Description = description;
            Code = code;
            Status = true;
            Id = id;
            OrderRowTourSheet = orderRowTourSheet;
            Icon = icon;
            OrderRowAttention = orderRowAttention;
            MedicalAreaType = medicalAreaType;
        }
    }
}
