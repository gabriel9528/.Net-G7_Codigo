using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Tools.Class;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Services
{
    public class ConsoleLog
    {
        private readonly string Path = "";
        public static void WriteLine(string value)
        {
            if (CommonStatic.LogOn)
            {
                string strMisDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                ConsoleLog log = new(strMisDocumentos + "/logs");
                log.Add(value);
            }
            Console.WriteLine(value);

            //var client = new AmazonCloudWatchLogsClient(RegionEndpoint.USEast1); // Cambia la región según sea necesario


            //string logGroupName = "PULSO/AP/ERR-LOG";
            //string logStreamName = "COMMONLOGS";

            //var logEvent = new InputLogEvent
            //{
            //    Timestamp = DateTimePersonalized.NowPeru,
            //    Message = value
            //};

            //var request = new PutLogEventsRequest
            //{
            //    LogGroupName = logGroupName,
            //    LogStreamName = logStreamName,
            //    LogEvents = new() { logEvent }
            //};

            //var response = client.PutLogEventsAsync(request).Result;

            //if (response.HttpStatusCode == HttpStatusCode.OK)
            //{
            //    Console.WriteLine("Los registros se enviaron con éxito a CloudWatch Logs.");
            //}
            //else
            //{
            //    Console.WriteLine($"Error al enviar los registros. Código de estado: {response.HttpStatusCode}");
            //}
            //client.Dispose();
            // Console.WriteLine(value);
        }



        public ConsoleLog(string Path)
        {
            this.Path = Path;
        }

        public void Add(string sLog)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += DateTimePersonalized.NowPeru + " - " + sLog + Environment.NewLine;

            StreamWriter sw = new(Path + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();

        }

        private static string GetNameFile()
        {
            string nombre = "log_" + DateTimePersonalized.NowPeru.Year + "_" + DateTimePersonalized.NowPeru.Month + "_" + DateTimePersonalized.NowPeru.Day + ".txt";
            return nombre;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);



            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);

            }
        }

    }
}

