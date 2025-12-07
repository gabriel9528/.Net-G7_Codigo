namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos.OccupationalMasterData.Dto
{
    public class AppointmentDetailMinDto
    {
        public Guid Id { get; set; }
        public string DateRegister { get; set; } = String.Empty;
        public string Date { get; set; } = String.Empty;
        public Guid? PersonId { get; set; }
        public string DocumentNumber { get; set; } = String.Empty;
        public Guid IdentityDocumentTypeId { get; set; }
        public string IdentityDocumentType { get; set; } = String.Empty;
        public string Names { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string? SecondLastName { get; set; } = String.Empty;
        public string? PersonalPhoneNumber { get; set; }
        public string? PersonalEmail { get; set; } = String.Empty;
        public Guid? AreaId { get; set; }
        public string Area { get; set; } = String.Empty;
        public string DocumentTypeAbreviation { get; set; } = String.Empty;
        public Guid? PositionId { get; set; }
        public string Position { get; set; } = String.Empty;
        public Guid ProfileId { get; set; }
        public string Profile { get; set; } = String.Empty;
        public string Subsidiary { get; set; } = String.Empty;
        public Guid? BusinessProjectId { get; set; }
        public string BusinessProject { get; set; } = String.Empty;
        public Guid ProtocolTypeId { get; set; }
        public string ProtocolType { get; set; } = String.Empty;
    }
}
