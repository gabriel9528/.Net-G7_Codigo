namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities
{
    public class SecretAccessDB
    {
        public string host { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string database { get; set; }
        public string password { get; set; }
    }
}
