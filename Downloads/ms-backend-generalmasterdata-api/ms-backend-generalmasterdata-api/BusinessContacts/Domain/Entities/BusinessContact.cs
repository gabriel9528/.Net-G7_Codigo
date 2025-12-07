using AnaPrevention.GeneralMasterData.Api.Businesses.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Domain.Entities
{
    public class BusinessContact
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public string? CellPhone { get; set; }
        public string? SecondCellPhone { get; set; }
        public string? Phone { get; set; }
        public string? SecondPhone { get; set; }
        public Email? Email { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
        public BusinessContact() { }

        public BusinessContact(
            string firstName, string lastName, string position, string cellPhone, string secondCellPhone,
            string phone, string secondPhone, Email email, string comment, Guid businessId, Guid id)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            CellPhone = cellPhone;
            SecondCellPhone = secondCellPhone;
            Phone = phone;
            SecondPhone = secondPhone;
            Email = email;
            Comment = comment;
            BusinessId = businessId;
            Status = true;
            Id = id;
        }
    }
}
