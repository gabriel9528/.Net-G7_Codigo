namespace AnaPrevention.GeneralMasterData.Api.Equipments.Domain.Entities
{
    public class EquipmentCalibration
    {

        public Guid Id { get; set; }

        public Equipment Equipment { get; set; }
        public Guid EquipmentId { get; set; }
        public DateTime Datecalibration { get; set; }
        public DateTime NextDatecalibration { get; set; }

        public EquipmentCalibration(Guid id, Guid equipmentId, DateTime datecalibration, DateTime nextDatecalibration)
        {
            Id = id;
            EquipmentId = equipmentId;
            Datecalibration = datecalibration;
            NextDatecalibration = nextDatecalibration;
        }
    }
}
