namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos
{
    public class EquipmentDetailRequest
    {
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Datecalibration { get; set; } = string.Empty;

    }
}
