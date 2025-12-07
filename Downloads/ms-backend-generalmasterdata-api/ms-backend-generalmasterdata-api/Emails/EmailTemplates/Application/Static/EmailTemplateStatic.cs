namespace AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Static
{
    public static class EmailTemplateStatic
    {
        public const string DescriptionMsgErrorMaxLength = "Descripcion debe ser igual o menor de {0} caracteres";
        public const string SubjectMsgErrorMaxLength = "Asunto debe ser igual o menor de {0} caracteres";

        public const string DescriptionMsgErrorRequiered = "Descripción es obligatorio";
        public const string SubjectMsgErrorRequiered = "Asunto es obligatorio";
        public const string BodyMsgErrorRequiered = "Cuerpo del correo es obligatorio";
        public const string EmailUserMsgErrorRequiered = "Debe Seleccionar Email para el envio del correo";

        public const string DescriptionMsgErrorDuplicate = "Ya existe un plantilla con la misma descripción";
        public static string RecoveryPassword(string names, string password, string usuario)
        {
            string body = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\" />\r\n<title>Envio de documentos</title>\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/>\r\n</head>\r\n<body >\r\n\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\t\r\n\t\t<tr>\r\n\t\t\t<td style=\"padding: 10px 0 30px 0;\">\r\n\t\t\t\t<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border: 1px solid #cccccc; border-collapse: collapse;\">\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td bgcolor=\"#AAC254\" style=\"padding: 30px 30px 30px 30px;\">\r\n\t\t\t\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t<td style=\"color: #ffffff; font-family: Arial, sans-serif; font-size: 20px;\" width=\"75%\">\r\n\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t\t<b><font color=\"#ffffff\">ENVIO DE CREDENCIALES DE USUARIO</font></b>\t\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t</table>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td bgcolor=\"#ffffff\" style=\"padding: 40px 30px 40px 30px;\">\r\n\t\t\t\t\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\r\n\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t<td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 16px;\">\r\n\t\t\t\t\t\t\t\t\t\tEstimado (a) " + names + " <br/> Su usuario es: " + usuario + " <br/> Contraseña es: \n " + password + " \r\n\n\t\t\t\t\t\t\t\t\t</td>\r\n\r\n\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t\t\t\t<td style=\"color: #153643; font-family: Arial, sans-serif; font-size: 16px;\">\r\n<b>Atte: Pulso Corporacion Medica S.R.L<b>\r\n\t\t\t\t\t\t\t\t\t</td>\r\n\t\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t</table>\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t\r\n\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<td style=\"padding: 5px 5px 5px 5px;\">\r\n\t\t\t\t\t\t</td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t\r\n\t\t\t\t</table>\r\n\t\t\t</td>\r\n\t\t</tr>\r\n\t</table>\r\n</body>\r\n</html>";

            return body;
        }
    }
}
