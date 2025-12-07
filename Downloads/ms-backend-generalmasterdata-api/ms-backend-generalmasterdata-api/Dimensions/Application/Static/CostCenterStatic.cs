namespace AnaPrevention.GeneralMasterData.Api.Dimensions.Application.Static
{
    public static class CostCenterStatic
    {
        public const int DescriptionMaxLength = 300;


        public const string DescriptionMsgErrorMaxLength = "Descripción debe ser igual o menor de {0} caracteres [Centro de Costo]";
        public const string CodeMsgErrorMaxLength = "Código debe ser igual o menor de {0} caracteres [Centro de Costo]";

        public const string DescriptionMsgErrorRequiered = "Descripción es obligatoria [Centro de Costo]";
        public const string CodeMsgErrorRequiered = "Código es obligatorio [Centro de Costo]";
        public const string IdMsgErrorRequiered = "Id es obligatorio [Centro de Costo]";
        public const string DimensionMsgErrorRequiered = "Dimensión es obligatorio [Centro de Costo]";

        public const string DescriptionMsgErrorDuplicate = "Descripción ya existe [Centro de Costo]";
        public const string CodeMsgErrorDuplicate = "Código ya existe [Centro de Costo]";

        public const string DimensionMsgErrorNotFound = "Dimensión no encontrada [Centro de Costo]";
    }
}
