namespace AnaPrevention.GeneralMasterData.Api.Persons.Application.Dtos
{
    public class RegisterPersonResponse
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public string Names { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SecondLastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
