namespace AnaPrevention.GeneralMasterData.Api.IdentityDocumentTypes.Application.Static
{
    public static class IdentityDocumentTypeStatic
    {

        public const int CodeMaxLength = 3;
        public const int AbbreviationMaxLength = 3;

        public const string AbbreviationMsgErrorMaxLength = "Abreviatura debe ser igual o menor de {0} caracteres";

        public const string AbbreviationMsgErrorRequiered = "Abreviatura es obligatoria";

        public const string AbbreviationMsgErrorDuplicate = "Abreviatura ya existe";

        public const string IdentityDocumentTypeMsgErrorNotFound = "Tipo de documento no existe";
    }
}
