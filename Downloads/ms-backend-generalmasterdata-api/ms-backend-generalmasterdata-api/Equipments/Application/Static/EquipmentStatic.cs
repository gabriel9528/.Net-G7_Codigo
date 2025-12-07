namespace AnaPrevention.GeneralMasterData.Api.Equipments.Application.Static
{
    public static class EquipmentStatic
    {
        public const string EquipmentMsgNotFound = "Equipo ingresado no existe";

        public const string PersonDeviceManagerIdMsgErrorRequiered = "Responsable es obligatorio";

        public const string PersonDeviceManagerIdMsgErrorNotFound = "Responsable no encontrado";

        public const int BrandMaxLength = 200;
        public const int ModelMaxLength = 200;
        public const int SerialNumberMaxLength = 200;
        public const int SupplierMaxLength = 200;

        public const string BrandMsgErrorMaxLength = "Marca debe ser igual o menor de {0} caracteres";
        public const string ModelMsgErrorMaxLength = "Modelo debe ser igual o menor de {0} caracteres";
        public const string SupplierMsgErrorMaxLength = "Proveedor debe ser igual o menor de {0} caracteres";
        public const string SerialNumberMsgErrorMaxLength = "Numero de serie debe ser igual o menor de {0} caracteres";

        public const string BrandMsgErrorRequiered = "Marca es obligatoria";
        public const string ModelMsgErrorRequiered = "Modelo es obligatoria";
        public const string SerialNumberMsgErrorRequiered = "Numero de serie es obligatoria";
        public const string SupplierMsgErrorRequiered = "Proveedor es obligatoria";

    }
}
