namespace AnaPrevention.GeneralMasterData.Api.Common.Tools.Class
{
    public static class DateTimePersonalized
    {
        public static DateTime NowPeru
        {
            get { return GetNowPeru(); }
        }
        public static DateTime NowChile
        {
            get { return GetNowChile(); }
        }


        private static DateTime GetNowPeru()
        {
            DateTime horaActualUtc = DateTime.UtcNow;

            TimeZoneInfo zonaHorariaPeru = TimeZoneInfo.FindSystemTimeZoneById("America/Lima");

            DateTime horaActualPeru = TimeZoneInfo.ConvertTimeFromUtc(horaActualUtc, zonaHorariaPeru);

            return horaActualPeru;
        }

        private static DateTime GetNowChile()
        {
            DateTime horaActualUtc = DateTime.UtcNow;

            TimeZoneInfo zonaHorariaPeru = TimeZoneInfo.FindSystemTimeZoneById("America/Santiago");

            DateTime horaActualPeru = TimeZoneInfo.ConvertTimeFromUtc(horaActualUtc, zonaHorariaPeru);

            return horaActualPeru;
        }
    }
}
