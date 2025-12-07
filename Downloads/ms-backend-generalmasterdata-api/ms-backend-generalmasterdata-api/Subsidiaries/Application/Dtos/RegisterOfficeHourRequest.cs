namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class RegisterOfficeHourRequest
    {
        public DayOfWeek StartDay { get; set; }
        public DayOfWeek FinishDay { get; set; }
        public int HourStart { get; set; }
        public int MinuteStart { get; set; }
        public int HourFinish { get; set; }
        public int MinuteFinish { get; set; }
    }
}
