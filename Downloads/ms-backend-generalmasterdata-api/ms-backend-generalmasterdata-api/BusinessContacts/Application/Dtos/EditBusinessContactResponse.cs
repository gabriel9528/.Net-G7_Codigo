namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Dtos
{
    public class EditBusinessContactResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string SecondCellPhone { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string SecondPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
    }
}
