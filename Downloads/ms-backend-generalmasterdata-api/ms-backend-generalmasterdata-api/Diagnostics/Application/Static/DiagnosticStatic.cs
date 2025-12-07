namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Static
{
    public static class DiagnosticStatic
    {
        
        public const int Description2MaxLength = 200;
        public const int Cie10MaxLength = 10;

        
        public const string Cie10MsgErrorMaxLength = "CIE 10 debe ser igual o menor de {0} caracteres";
        public const string Description2MsgErrorMaxLength = "Segunda descripción debe ser igual o menor de {0} caracteres";

        
        public const string Cie10MsgErrorRequiered = "CIE 10 es obligatorio";
        public const string IdMsgErrorRequiered = "Id es obligatorio";

        
        public const string Cie10MsgErrorDuplicate = "CIE 10 ya existe";
    }
}
