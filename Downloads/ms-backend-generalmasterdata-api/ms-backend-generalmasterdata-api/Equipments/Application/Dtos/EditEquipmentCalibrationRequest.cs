namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos
{
    public class EditEquipmentCalibrationRequest
    {
        public Guid? Id { get; set; }
        public string Datecalibration { get; set; } = string.Empty;
        public string NextDatecalibration { get; set; } = string.Empty;
    }
}
