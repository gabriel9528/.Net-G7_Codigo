namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.Security.Views
{
    public class UserMin
    {
        public Guid Id { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PersonId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
