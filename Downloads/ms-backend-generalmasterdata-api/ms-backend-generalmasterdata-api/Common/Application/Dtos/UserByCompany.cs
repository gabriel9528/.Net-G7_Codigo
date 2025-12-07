namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos
{
    public class UserByCompany
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public string? Setting { get; set; }

        public Guid userId { get; set; }



    }
}
