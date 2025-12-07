using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;
using System.Reflection;
using System.Text.Json;

using System.Globalization;

using System.Text.RegularExpressions;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Fields.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Fields.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Static
{
    public static class CommonStatic
    {

        public const int DescriptionMaxLength = 200;
        public const int EmailContentMaxLength = 64;
        public const int CodeMaxLength = 10;
        public const int CodeLargeMaxLength = 30;
        public const int PhoneNumberMaxLenght = 10;
        public const int DefaultOrderRow = 999;
        public const string DefaultArea = "OTROS";


        public const int LenghtSignPDF = 70;
        public const int LenghtFingerPrinterPDF = 60;

        public const string DescriptionMsgErrorMaxLength = "Descripción debe ser igual o menor de {0} caracteres";
        public const string CodeMsgErrorMaxLength = "Código debe ser igual o menor de {0} caracteres";
        public const string GenericMsgErrorMaxLength = "{0} debe ser igual o menor de {0} caracteres";


        public const string DescriptionMsgErrorRequiered = "Descripción es obligatoria";
        public const string CodeMsgErrorRequiered = "Código es obligatorio";
        public const string IdMsgErrorRequiered = "Id es obligatorio";

        public const string DescriptionMsgErrorDuplicate = "Descripción ya existe";
        public const string CodeMsgErrorDuplicate = "Código ya existe";

        public const string IntmsgErrorError = "El campo {0} debe ser mayor a 0";

        public const string NameSheetDefault = "Hoja 1";
        public const string Active = "Activo";
        public const string Deactivated = "Desactivo";
        public const string MimeExcel = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,";

        public const int EmailMaxLength = 150;

        public const string EmailMsgErrorRequiered = "Email es obligatorio";
        public const string DateMsgErrorRequiered = "{0} es obligatorio";
        public const string TimeMsgErrorRequiered = "{0} es obligatorio";

        public const string EmailMsgErrorDuplicate = "Email ya existe";

        public const string Denies = "Niega";


        public const string EmailMsgErrorInvalidLength = "Tamaño invalido del Email";

        public const string EmailMsgErrorInvalidFormat = "Formato invalido del Email";
        public const string DateMsgErrorInvalidFormat = "Formato invalido de fecha : {0}";
        public const string TimeMsgErrorInvalidFormat = "Formato invalido de hora: {0}";
        public const string DateMsgErrorMinError = "{0} ingresa debe ser mayor a {1}";
        public const int YearMin = 1900;
        public const int MonthMin = 1;
        public const int DayMin = 1;

        public const int MinimumNumberUppercasePassword = 1;
        public const int MinimumNumberNumericPassword = 1;
        public const int MinimumLengthPassword = 6;
        public const int LengthGeneratedPassword = 10;
        public const string SpecialCharacters = "[!\"#\\$%&'()*+,-./:;=?@\\[\\]^_`{|}~]";

        public const string PasswordMsgErrorRequiered = "El password es obligatorio";
        public const string PasswordMinimunCharactersMsgErrorRequiered = "El Password debe tener como minimo {0} Caracteres";
        public const string PasswordMinimunUpperCaseMsgErrorRequiered = "El Password debe tener como minimo {0} Mayuscula(s)";
        public const string PasswordMinimunNumericCaseMsgErrorRequiered = "El Password debe tener como minimo {0} Numero(s)";

        public const string EnvironmentVariableDB = "PULSO_WEB";
        public const int MaxRowPageSize = 50;
        public const int FormatDateSQL = 103;
        public const int numberZerosCode = 6;
        public const int numberZerosCodeOrderOccupational = 4;
        public const int MaxRowAutocomplete = 10;
        public const string FormatDate = "dd-MM-yyyy";
        public const string FormatDate2 = "yyyy-MM-dd";
        public const string FormatHour = "HH:mm:ss";

        public const string FormatDateAndHour = "dd-MM-yyyy HH:mm:ss";

        public const string DateDescriptionDefault = "Fecha";
        public const string TimeDescriptionDefault = "Hora";

        public const string LaboratyDescription = "Examenes de Laboratorio";

        public const bool LogOn = true;


        public const string CountryDefault = "PE";

        public const string ImageFormartAccepted = "PNG|JPG|BMP|GIF|TIF|JPGE|JPEG|PDF|VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET";
        public const string FileFormartNotAccepted = "EXE|JS|SH|BAT";

        public const string PhotoOrderDefault = "photoOrderDefault.png";

        public const string ImageMsgErrorErrorFormart = "Imagen con errores de formato {0}";
        public const string ImageMsgErrorErrorBase64 = "Imagen con errores al convertir Base64 {0}";
        public const string ImageMsgErrorExtension = "Extension de Archivo invalido {0}";


        public const string FileMsgErrorExtension = "Extension de Archivo invalida {0}";
        public const string FileMsgErrorErrorBase64 = "File con errores al convertir Base64 {0}";
        public const string FileMsgErrorErrorFormart = "File con errores de formato {0}";
        public const string PrinterFinger = "Huella";
        public const string PrinterFingerRequiered = "La huella es obligatoria";

        public const string BaseDirectoryImage = "\\img\\";

        public const string Endpoint = "endpointonepulso-885129458973.s3-accesspoint.us-east-1.amazonaws.com";
        public const string AccessKey = "AKIA44FOTWUOX2A5LZMC";
        public const string SecretKey = "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";
        public const string Url = "https://onepulso.s3.amazonaws.com/";
        public const string BucketName = "files";
        public const string Location = "us-east-1";

        // ------------------------- Data Bucket Files ---------------------------------

        public const string BucketNameFiles = "pulscrp-0002-anap-apq-00-files";
        public const string AccessKeyFiles = "AKIA5BUSEWSHRQAN2WIA";
        public const string SecretKeyFiles = "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";
        public const string UrlFiles = "https://pulscrp-0004-stor-sq-00-files.s3.amazonaws.com/";

        //-------------------------- Data Bucket Qr Chile ------------------------------

        public const string BucketNameQrChile = "archivoschileqr";
        public const string AccessKeyQrChile = "AKIA5BUSEWSH2ZIJ3OJV";
        public const string SecretKeyQrChile = "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";
        public const string UrlQrChile = "https://archivoschileqr.s3.amazonaws.com/";


        private static readonly string? Envioroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static bool IsEnvioromentDev()
        {
            return !string.IsNullOrWhiteSpace(Envioroment);
        }


        public static JsonSerializerOptions Options = new()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
        };

        public static ValueConverter ConvertDate = new ValueConverter<Date, DateTime>(
            value => value.DateTimeValue,
            value => Date.Create(value).Value
            );

        public static ValueConverter ConvertTime = new ValueConverter<Time, DateTime>(
           value => value.TimeValue,
           value => Time.Create(value).Value
           );

        public const string MsgErrorConvertDateStart = "Fecha inicio no tiene el formato correcto";
        public const string MsgErrorConvertDateFinish = "Fecha fin no tiene el formato correcto";
        public static string ConvertAgeFull(DateTime? dateBirth)
        {
            string dateBirthFull = string.Empty;
            if (dateBirth != null)
            {
                int years;
                int months;
                int days;
                DateTime dateNow = DateTimePersonalized.NowPeru;
                if (dateNow.Month > dateBirth.Value.Month || (dateNow.Month == dateBirth.Value.Month && dateNow.Day > dateBirth.Value.Day))
                {
                    years = dateNow.Year - dateBirth.Value.Year;
                    months = dateNow.Month - dateBirth.Value.Month;
                    int day1 = dateBirth.Value.Day - DateTime.DaysInMonth(dateNow.Year, dateBirth.Value.Month);
                    int day2 = dateNow.Day - DateTime.DaysInMonth(dateNow.Year, dateNow.Month);
                    days = Math.Abs(day1 - day2);
                }
                else if (dateNow.Month < dateBirth.Value.Month || (dateNow.Month == dateBirth.Value.Month && dateNow.Day < dateBirth.Value.Day))
                {
                    years = dateNow.Year - dateBirth.Value.Year - 1;
                    months = dateBirth.Value.Month - dateNow.Month;
                    int day1 = dateBirth.Value.Day - DateTime.DaysInMonth(dateNow.Year, dateBirth.Value.Month);
                    int day2 = dateNow.Day - DateTime.DaysInMonth(dateNow.Year, dateNow.Month);
                    days = Math.Abs(day2 - day1);
                }
                else
                {
                    years = dateNow.Year - dateBirth.Value.Year;
                    months = 0;
                    days = 0;
                }

                dateBirthFull = years.ToString() + "a " + months.ToString() + "m " + days.ToString() + "d ";
            }
            return dateBirthFull;
        }

        public static string GetAbbreviatedMonthName(int? monthNumber)
        {
            if (monthNumber == null)
            {
                return string.Empty;
            }
            if (monthNumber >= 1 && monthNumber <= 12)
            {
                CultureInfo cultureInfo = new("es-ES");
                return cultureInfo.DateTimeFormat.GetAbbreviatedMonthName((int)monthNumber);
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool IsBase64String(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static List<string> ConvertJsonToListString(string? json)
        {
            try
            {
                if (json == null)
                    return new();

                var list = JsonSerializer.Deserialize<List<string>>(json, CommonStatic.Options);

                if (list == null)
                    return new();
                return list;
            }
            catch { return new(); }
        }

        public static List<Dictionary<string, string>> ConvertJsonToDictionaryString(string? json)
        {
            try
            {
                if (json == null)
                    return new();


                var list = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }
        public static Dictionary<FieldType, string> diccionarioTipos = new Dictionary<FieldType, string>
            {
                { FieldType.INT, "Entero" },
                { FieldType.DECIMAL, "Decimal" },
                { FieldType.VARCHAR, "Texto" },
                { FieldType.BOOL, "Booleano" },
                { FieldType.SECCTION, "Section" },
                { FieldType.LIST, "Lista" }
            };
        public static int? GetSimilarityValue(string targetWord)
        {
            string normalizedTargetWord = targetWord.ToUpper(); // Convertir la palabra de entrada a mayúsculas

            foreach (var kvp in diccionarioTipos)
            {
                if (kvp.Value.ToUpper().Contains(normalizedTargetWord))
                {
                    return (int)kvp.Key;
                }
            }
            return null;
        }

        public static List<RegisterRangeHeaderRequest> ConvertJsonToRegisterRangeResponse(string? json)
        {
            try
            {
                if (json == null)
                    return new();


                var list = JsonSerializer.Deserialize<List<RegisterRangeHeaderRequest>>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }
        //public static SettingDto ConvertJsonToSettingDto(string? json)
        //{
        //    try
        //    {
        //        if (json == null)
        //            return new();


        //        var list = JsonSerializer.Deserialize<SettingDto>(json, CommonStatic.Options);

        //        if (list == null)
        //            return new();

        //        return list;
        //    }
        //    catch { return new(); }
        //}

        public static List<Dictionary<string, string>>? ConvertJsonToListOfficeHourDto(string? json)
        {
            try
            {
                if (json == null)
                    return new();

                var list = JsonSerializer.Deserialize<List<Dictionary<string, string>>?>(json, CommonStatic.Options);

                if (list == null)
                    return new();
                return list;
            }
            catch { return new(); }
        }
   
        //------------------------------------- Convert Json To List  (ElectroEncephalogram)---------------------------------------------
     
        public static List<Guid> ConvertJsonToListGuid(string? json)
        {
            try
            {
                if (json == null)
                    return new();

                var list = JsonSerializer.Deserialize<List<Guid>>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }
        public static string GetValueAtribute(object? obj, string? atributeName)
        {
            if (obj == null)
                return string.Empty;

            if (string.IsNullOrEmpty(atributeName))
                return string.Empty;

            Type objType = obj.GetType();
            PropertyInfo? propiedad = objType.GetProperty(atributeName);

            if (propiedad != null)
            {
                object? value = propiedad.GetValue(obj);

                if (value == null)
                    return string.Empty;

                return value?.ToString() ?? "";
            }

            return string.Empty;
        }
        public static string Replace(string text, Dictionary<string, string> values)
        {
            foreach (var kvp in values)
            {
                text = text.Replace("[" + kvp.Key + "]", kvp.Value);
            }

            return text;
        }

        public static T? ConvertJsonToDto<T>(string? json) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return null;

                if (json.Contains("NULL", StringComparison.OrdinalIgnoreCase))
                {
                    json = json.Replace("NULL", "null", StringComparison.OrdinalIgnoreCase);
                }

                var dto = JsonSerializer.Deserialize<T>(json, CommonStatic.Options);

                if (dto == null)
                    return null;

                return dto;
            }
            catch
            {
                return null;
            }
        }
        public static Dictionary<string, string> ExtractProperties(object dto, List<string>? keys)
        {
            var result = new Dictionary<string, string>();

            if (keys == null)
                return result;
            // Obtener el tipo del objeto OHOrderHeaderDto
            var type = dto.GetType();

            // Iterar sobre las claves y obtener los valores de las propiedades correspondientes
            foreach (var key in keys)
            {
                // Intentar encontrar la propiedad con el nombre de la clave
                var property = type.GetProperty(key);

                if (property != null)
                {
                    // Obtener el valor de la propiedad y agregarlo al diccionario
                    var value = property.GetValue(dto);
                    if (value != null)
                        result.Add(key, value.ToString() ?? "");
                    else
                        result.Add(key, "");
                }
                else
                {
                    // Manejar el caso en el que la propiedad no existe
                    result.Add(key, "");
                }
            }

            return result;
        }
        public static List<OptionYesOrNot> ConvertJsonToOptionYesOrNot(string? json)
        {
            try
            {
                if (json == null)
                    return new();

                var list = JsonSerializer.Deserialize<List<OptionYesOrNot>>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }
        public static string ReplacewithParentheses(string text, Dictionary<string, string> values)
        {
            foreach (var kvp in values)
            {
                text = text.Replace(kvp.Key, kvp.Value);
            }

            return text;
        }
        public static string GetMimeType(string extension)
        {
            Dictionary<string, string> mimeTypes = new()
            {
            { "pdf", "application/pdf" },
            { "txt", "text/plain" },
            { "jpg", "image/jpeg" },
            { "png", "image/png" },
            { "gif", "image/gif" },
            { "bmp", "image/bmp" },
            { "doc", "application/msword" },
            { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { "xls", "application/vnd.ms-excel" },
            { "xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { "ppt", "application/vnd.ms-powerpoint" },
            { "pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            { "zip", "application/zip" },
            { "rar", "application/x-rar-compressed" },
        };

            string mimeType;

            if (mimeTypes.TryGetValue(extension.ToLower(), out mimeType))
            {
                return mimeType;
            }

            return "application/octet-stream"; // Tipo MIME por defecto si no se encuentra la extensión
        }

        public static bool IsValidJson(string jsonString)
        {
            try
            {
                JsonDocument.Parse(jsonString);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }


        public static bool DetermineGender(string word)
        {
            string[] masculineEndings = { "o", "or", "ón", "án", "ín", "un" };
            string[] feminineEndings = { "a", "ión", "án", "ín", "ad", "ud" };

            foreach (string ending in masculineEndings)
            {
                if (word.EndsWith(ending))
                {
                    return true; // Masculine
                }
            }

            foreach (string ending in feminineEndings)
            {
                if (word.EndsWith(ending))
                {
                    return false; 
                }
            }

            return true; 
        }

        public static string GetDemonym(string countryName = "", bool isFeminine = false)
        {
            countryName = countryName.ToUpper();
            Dictionary<string, string> demonyms = new()
            {
                { "AF", "afgano" },
                { "AL", "albanés" },
                { "DE", "alemán" },
                { "AD", "andorrano" },
                { "AO", "angoleño" },
                { "AI", "anguilense" },
                { "AQ", "antártico" },
                { "AG", "antiguano" },
                { "SA", "saudí" },
                { "DZ", "argelino" },
                { "AR", "argentino" },
                { "AM", "armenio" },
                { "AW", "arubeño" },
                { "AU", "australiano" },
                { "AT", "austríaco" },
                { "AZ", "azerbaiyano" },
                { "BS", "bahameño" },
                { "BH", "bahreiní" },
                { "BD", "bangladesí" },
                { "BB", "barbadense" },
                { "BE", "belga" },
                { "BZ", "beliceño" },
                { "BJ", "beninés" },
                { "BT", "butanés" },
                { "BY", "bielorruso" },
                { "MM", "birmano" },
                { "BO", "boliviano" },
                { "BA", "bosnio" },
                { "BW", "botsuano" },
                { "BR", "brasileño" },
                { "BN", "bruneano" },
                { "BG", "búlgaro" },
                { "BF", "burkinés" },
                { "BI", "burundés" },
                { "CV", "caboverdiano" },
                { "KH", "camboyano" },
                { "CM", "camerunés" },
                { "CA", "canadiense" },
                { "TD", "chadiano" },
                { "CL", "chileno" },
                { "CN", "chino" },
                { "CY", "chipriota" },
                { "VA", "vaticano" },
                { "CO", "colombiano" },
                { "KM", "comorense" },
                { "KP", "norcoreano" },
                { "KR", "surcoreano" },
                { "CI", "marfileño" },
                { "CR", "costarricense" },
                { "HR", "croata" },
                { "CU", "cubano" },
                { "CW", "curazoleño" },
                { "DK", "danés" },
                { "DM", "dominiqués" },
                { "EC", "ecuatoriano" },
                { "EG", "egipcio" },
                { "SV", "salvadoreño" },
                { "AE", "emiratí" },
                { "ER", "eritreo" },
                { "SK", "eslovaco" },
                { "SI", "esloveno" },
                { "ES", "español" },
                { "US", "estadounidense" },
                { "EE", "estonio" },
                { "ET", "etíope" },
                { "PH", "filipino" },
                { "FI", "finlandés" },
                { "FJ", "fiyiano" },
                { "FR", "francés" },
                { "GA", "gabonés" },
                { "GM", "gambiano" },
                { "GE", "georgiano" },
                { "GH", "ghanés" },
                { "GI", "gibraltareño" },
                { "GD", "granadino" },
                { "GR", "griego" },
                { "GL", "groenlandés" },
                { "GP", "guadalupense" },
                { "GU", "guameño" },
                { "GT", "guatemalteco" },
                { "GF", "guayanés" },
                { "GG", "guernseés" },
                { "GN", "guineano" },
                { "GQ", "ecuatoguineano" },
                { "GW", "guineano-bisáu" },
                { "GY", "guyanés" },
                { "HT", "haitiano" },
                { "HN", "hondureño" },
                { "HK", "hongkonés" },
                { "HU", "húngaro" },
                { "IN", "indio" },
                { "ID", "indonesio" },
                { "IQ", "iraquí" },
                { "IR", "iraní" },
                { "IE", "irlandés" },
                { "BV", "bouvetiano" },
                { "IM", "manés" },
                { "CX", "isleño" },
                { "NF", "norfolkés" },
                { "IS", "islandés" },
                { "BM", "bermudeño" },
                { "KY", "caimanés" },
                { "CC", "cocoense" },
                { "CK", "cookiano" },
                { "AX", "alandés" },
                { "FO", "feroés" },
                { "GS", "georgiano" },
                { "HM", "heardense" },
                { "MV", "maldivo" },
                { "FK", "malvinense" },
                { "MP", "marianense" },
                { "MH", "marshalés" },
                { "PN", "pitcairnés" },
                { "SB", "salomonense" },
                { "TC", "turcano" },
                { "UM", "menorino" },
                { "VG", "virgino británico" },
                { "VI", "virgino estadounidense" },
                { "IL", "israelí" },
                { "IT", "italiano" },
                { "JM", "jamaicano" },
                { "JP", "japonés" },
                { "JE", "jerosolimitano" },
                { "JO", "jordano" },
                { "KZ", "kazajo" },
                { "KE", "keniano" },
                { "KG", "kirguís" },
                { "KI", "kiribatiano" },
                { "KW", "kuwaití" },
                { "LA", "laosiano" },
                { "LS", "lesotense" },
                { "LV", "letón" },
                { "LB", "libanés" },
                { "LR", "liberiano" },
                { "LY", "libio" },
                { "LI", "liechtensteiniano" },
                { "LT", "lituano" },
                { "LU", "luxemburgués" },
                { "MO", "macaense" },
                { "MK", "macedonio" },
                { "MG", "malgache" },
                { "MY", "malayo" },
                { "MW", "malauí" },
                { "ML", "maliense" },
                { "MT", "maltés" },
                { "MA", "marroquí" },
                { "MQ", "martiniqués" },
                { "MU", "mauriciano" },
                { "MR", "mauritano" },
                { "YT", "mayotense" },
                { "MX", "mexicano" },
                { "FM", "micronesio" },
                { "MD", "moldavo" },
                { "MC", "monegasco" },
                { "MN", "mongol" },
                { "ME", "montenegrino" },
                { "MS", "montserratiano" },
                { "MZ", "mozambiqueño" },
                { "NA", "namibio" },
                { "NR", "nauruano" },
                { "NP", "nepalí" },
                { "NI", "nicaragüense" },
                { "NE", "nigerino" },
                { "NG", "nigeriano" },
                { "NU", "niueano" },
                { "NO", "noruego" },
                { "NC", "neocaledonio" },
                { "NZ", "neozelandés" },
                { "OM", "omaní" },
                { "NL", "neerlandés" },
                { "PK", "paquistaní" },
                { "PW", "palauano" },
                { "PS", "palestino" },
                { "PA", "panameño" },
                { "PG", "papuano" },
                { "PY", "paraguayo" },
                { "PE", "peruano" },
                { "PF", "polinesio" },
                { "PL", "polaco" },
                { "PT", "portugués" },
                { "PR", "puertorriqueño" },
                { "QA", "qatarí" },
                { "GB", "británico" },
                { "CF", "centroafricano" },
                { "CZ", "checo" },
                { "SS", "sursudanés" },
                { "CG", "congoleño" },
                { "CD", "congolés" },
                { "DO", "dominicano" },
                { "RE", "reunionés" },
                { "RW", "ruandés" },
                { "RO", "rumano" },
                { "RU", "ruso" },
                { "EH", "saharaui" },
                { "WS", "samoano" },
                { "AS", "samoano americano" },
                { "BL", "sanbartolomense" },
                { "KN", "sancristobaleño" },
                { "SM", "sanmarinense" },
                { "MF", "sanmartinense" },
                { "PM", "sanpedrino" },
                { "VC", "sanvicentino" },
                { "SH", "santahelénico" },
                { "LC", "santalucense" },
                { "ST", "santotomense" },
                { "SN", "senegalés" },
                { "RS", "serbio" },
                { "SC", "seychellense" },
                { "SL", "sierraleonés" },
                { "SG", "singapurense" },
                { "SX", "sanmaarteno" },
                { "SY", "sirio" },
                { "SO", "somalí" },
                { "LK", "ceilandés" },
                { "ZA", "sudafricano" },
                { "SD", "sursudanés" },
                { "SE", "sueco" },
                { "CH", "suizo" },
                { "SR", "surinamés" },
                { "SJ", "svalbardiano" },
                { "SZ", "suazi" },
                { "TH", "tailandés" },
                { "TW", "taiwanés" },
                { "TZ", "tanzano" },
                { "TJ", "tayiko" },
                { "IO", "chagosiano" },
                { "TF", "francés de Tierras Australes y Antárticas" },
                { "TL", "timorense" },
                { "TG", "togolés" },
                { "TK", "tongano" },
                { "TO", "tobiano" },
                { "TT", "triniteño" },
                { "TN", "tunecino" },
                { "TM", "turcomano" },
                { "TR", "turco" },
                { "TV", "tuvaluano" },
                { "UA", "ucraniano" },
                { "UG", "ugandés" },
                { "UY", "uruguayo" },
                { "UZ", "uzbeko" },
                { "VU", "vanuatuense" },
                { "VE", "venezolano" },
                { "VN", "vietnamita" },
                { "WF", "wallisiano" },
                { "YE", "yemení" },
                { "DJ", "yibutiano" },
                { "ZM", "zambiano" },
                { "ZW", "zimbabuense" }
            };


            countryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(countryName);

            if (demonyms.ContainsKey(countryName))
            {
                string demonym = demonyms[countryName];
                if (isFeminine)
                {
                    if (demonym.EndsWith("o"))
                    {
                        demonym = string.Concat(demonym.AsSpan(0, demonym.Length - 1), "a");
                    }
                }
                return  demonym;
            }
            else
            {
                return countryName;
            }
        }
        public static string RemoveSpecialCharacters(string input)
        {
            // Elimina todos los caracteres que no sean números o letras
            string pattern = "[^0-9a-zA-Z]";
            string result = Regex.Replace(input, pattern, "");

            return result;
        }

        private static readonly Dictionary<YesOrNot, string> yesOrNotNamesBasic = new()
        {
            { YesOrNot.YES, "SI" },
            { YesOrNot.NO, "NO" },
            { YesOrNot.NONE, "" },
            { YesOrNot._, "-" },
        };

        public static string GetYesOrNotNamesBasic(YesOrNot? result)
        {
            if (result == null)
            {
                return string.Empty;
            };

            return yesOrNotNamesBasic.TryGetValue((YesOrNot)result, out string? name) ? name ?? string.Empty : "No definido";
        }

        public static bool IsBoldHtml(string texto)
        {
            string patron = @"<b>.*?</b>";
            Regex regex = new Regex(patron);
            return regex.IsMatch(texto);
        }

        public static DateTime? IsValidDateFormat(string input)
        {
            string[] formatos = { "d/MM/yyyy", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy" };

            foreach (string formato in formatos)
            {
                if (DateTime.TryParseExact(input, formato, null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    return date; // Si se puede analizar con algún formato, es válido
                }
            }

            return null;
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            return Regex.IsMatch(email, pattern);
        }

        public static int CalculateAge(DateTime dateBirht)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateBirht.Year;

            if (now.Month < dateBirht.Month || (now.Month == dateBirht.Month && now.Day < dateBirht.Day))
            {
                age--;
            }

            return age;
        }
        public static string GetYesOrNot(YesOrNot? isChecked)
        {
            if (isChecked == YesOrNot.YES)
            {
                return "SI";
            }
            else if (isChecked == YesOrNot.NO)
            {
                return "NO";
            }
            else if (isChecked == YesOrNot.NONE)
            {
                return "-";
            }
            else
            {
                return "";
            }
        }
        public static string GetYesOrNotByBool(bool? isChecked)
        {
            if (isChecked == true)
            {
                return "SI";
            }
            else if (isChecked == false)
            {
                return "NO";
            }
            else
            {
                return "";
            }
        }
        public static string GetDoubleString(double? value)
        {
            if (value == null)
            {
                return "";
            }
            string stringOutZero = ((double)value).ToString("G");

            return stringOutZero;
        }
        public static string ConvertEnumFieldTypeToString(FieldType opcion)
        {
            if (diccionarioTipos.ContainsKey(opcion))
            {
                return diccionarioTipos[opcion];
            }
            else
            {
                return "";
            }
        }

        public static List<OptionFieldDto> ConvertJsonToListOptionFieldDto(string? json)
        {
            try
            {
                if (json == null)
                    return new();

                var list = JsonSerializer.Deserialize<List<OptionFieldDto>>(json, CommonStatic.Options);

                if (list == null)
                    return new();
                return list;
            }
            catch { return new(); }
        }

        public static SettingDto ConvertJsonToSettingDto(string? json)
        {
            try
            {
                if (json == null)
                    return new();


                var list = JsonSerializer.Deserialize<SettingDto>(json, CommonStatic.Options);

                if (list == null)
                    return new();

                return list;
            }
            catch { return new(); }
        }

        public static string GetDecimalString(decimal? value)
        {
            if (value == null)
            {
                return "";
            }
            string decimalText = ((decimal)value).ToString("G");

            int decimalIndex = decimalText.IndexOf('.');

            if (decimalIndex >= 0)
            {
                // Si hay un punto decimal, elimina los ceros al final.
                decimalText=  decimalText.TrimEnd('0');

                if (decimalText.EndsWith("."))
                {
                    decimalText = decimalText.Substring(0, decimalText.Length - 1);
                }

                return decimalText;
            }
            else
            {
                // Si no hay punto decimal, devuelve la cadena original.
                return decimalText;
            }
        }
        public static string CapitalLetter(string text)
        {
            // Utilizamos una expresión regular para encontrar palabras después del caracter "¿"
            // y capitalizar la primera letra de cada palabra.
            string patron = @"(?:¿\s*|\b)(\w)";
            string textoCorregido = Regex.Replace(text, patron, m => m.Groups[1].Value.ToUpper());
            return textoCorregido;
        }
        public static string CapitalFirstLetter(string text)
        {
            string[] palabras = text.Split(' ');

            if (palabras.Length > 0)
            {
                string primeraPalabra = palabras[0];

                if (!string.IsNullOrWhiteSpace(primeraPalabra))
                {
                    if (!char.IsLetter(primeraPalabra[0]))
                    {
                        // Si el primer carácter es un carácter especial, capitalizamos el segundo carácter (si existe)
                        if (primeraPalabra.Length > 1)
                        {
                            primeraPalabra = char.ToUpper(primeraPalabra[1], CultureInfo.CurrentCulture) + primeraPalabra.Substring(2);
                        }
                    }
                    else
                    {
                        // Si el primer carácter es una letra, capitalizamos la primera letra
                        primeraPalabra = char.ToUpper(primeraPalabra[0], CultureInfo.CurrentCulture) + primeraPalabra.Substring(1);
                    }
                }

                palabras[0] = primeraPalabra;
                return string.Join(" ", palabras);
            }
            else
            {
                return text;
            }
        }
    }
}
