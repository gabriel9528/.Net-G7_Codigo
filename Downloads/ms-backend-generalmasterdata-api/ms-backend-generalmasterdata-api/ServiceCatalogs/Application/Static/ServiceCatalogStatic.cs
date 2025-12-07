namespace AnaPrevention.GeneralMasterData.Api.ServiceCatalogs.Application.Static
{
    public static class ServiceCatalogStatic
    {

        public const int CodeSecondMaxLength = 10;


        public const string CodeSecondMsgErrorMaxLength = "Código Externo debe ser igual o menor de {0} caracteres";


        public const string SubFamilyIdMsgErrorRequiered = "Sub Familia es obligatorio";
        public const string TaxIdMsgErrorRequiered = "Impuesto es obligatorio";
        public const string UomIdMsgErrorRequiered = "Unidad de medida es obligatorio";
        public const string UomSecondIdMsgErrorRequiered = "Unidad de medida SUNAT es obligatorio";
        public const string ExistenceTypeIdMsgErrorRequiered = "Tipo de existencia es obligatorio";
        public const string ServiceCatalogMsgErrorRequiered = "Examen es obligatorio";

        public const string SubFamilyMsgErrorNoFound = "Sub Familia no existe";
        public const string TaxMsgErrorNoFound = "Impuesto no existe";
        public const string UomSecondMsgErrorNoFound = "Unidad de  SUNAT no existe";
        public const string UomMsgErrorNoFound = "Unidad de medida no existe";
        public const string ServiceTypemsgErrorNoFound = "Tipo de Servicio no existe";
        public const string ExistenceTypeMsgErrorNoFound = "Tipo de existencia no existe";
        public const string ServiceCatalogMsgErrorNoFound = "Examen no existe";

        public const string ServiceCatalogMsgErrorFormat = "Examen no es valido";
        public const string ServiceCatalogMsgErrorNotIsSales = "Examen no es un item de venta";

        public const string MedicalFormLaboratoryMsgError = "Un servicio no se puede relacionar al formulario de labotorio";



    }
}
