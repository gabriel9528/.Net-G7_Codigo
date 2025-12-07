namespace AnaPrevention.GeneralMasterData.Api.Fields.Application.Static
{
    public static class FieldStatic
    {
        public const int NameMaxLength = 200;
        public const int DefaultValueMaxLength = 200;
        public const int LegendMaxLength = 200;
        public const int UomMaxLength = 20;

        public const string NameMsgErrorMaxLength = "Nombre del campo debe ser igual o menor de {0} caracteres";
        public const string DefaultValueMsgErrorMaxLength = "Valor por defecto debe ser igual o menor de {0} caracteres";
        public const string LegendMsgErrorMaxLength = "Leyenda debe ser igual o menor de {0} caracteres";
        public const string UomMsgErrorMaxLength = "Unidad de medida debe ser igual o menor de {0} caracteres";


        public const string NameMsgErrorDuplicate = "Nombre de campo ya existe para el formato {0}";

        public const string NameMsgErrorRequiered = "Nombre del campo es obligatorio";
        public const string UomMsgErrorRequiered = "Unidad de Medida es obligatoria";

        public const string FieldMsgNotFound = "Campo no existe";
        public const string FieldMsgRequiered = "Campo es Obligatorio";

        public const string FieldLaboratoryMsgNotFound = "Campo de resultado de Laboratorio no existe";
        public const string FieldLaboratoryMsgRequiered = "Campo de resultado de Laboratorio es Obligatorio";

        public const string FieldFatherMsgNotFound = "Campo Padre no existe";
        public const string FieldSectionMsgNotFound = "Seccion no existe";
        public const string FieldSectionFatherMsgNotFound = "Seccion Padre no existe";
        public const string FieldSectionFatherMsgMaxFather = "Solo esta permitido 2 niveles de secciones";

        public const string ValueConditionMsgErrorFormat = "Valor no tiene el formato correcto [{0}]";
        public const string ValueConditionMsgError = "Valor inicial de rango debe ser menor al valor final";
        public const string RangeMsgErrorInvalided = "tipo de Rango Invalido";

        public const string FieldTypeMsgErrorNotFormat = "Tipo de campo Invalido";

        public const string Decimal = "Decimal";
        public const string Int = "Entero";
    }
}
