using System.ComponentModel;

namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Dtos
{
    public class EquipmentDetailDto
    {
        [Description("Descripcion")] public string? Description { get; set; } = string.Empty;
        [Description("Marca")] public string? Brand { get; set; } = string.Empty;
        [Description("Modelo")] public string? Model { get; set; } = string.Empty;
        [Description("Numero de Serie")] public string? SerialNumber { get; set; } = string.Empty;        
        [Description("Dia de Calibracion")] public string? Datecalibration { get; set; } = string.Empty;
    }
}
