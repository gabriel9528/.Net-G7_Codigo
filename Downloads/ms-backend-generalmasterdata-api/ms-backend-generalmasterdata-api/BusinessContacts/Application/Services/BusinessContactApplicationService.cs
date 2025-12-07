using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.BusinessContacts.Infrastructure.Repositories;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.ValueObjects;

namespace AnaPrevention.GeneralMasterData.Api.BusinessContacts.Application.Services
{
    public class BusinessContactApplicationService(
       AnaPreventionContext context,
       RegisterBusinessContactValidator registerBusinessContactValidator,
       EditBusinessContactValidator editBusinessContactValidator,
       BusinessContactRepository businessContactRepository)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterBusinessContactValidator _registerBusinessContactValidator = registerBusinessContactValidator;
        private readonly EditBusinessContactValidator _editBusinessContactValidator = editBusinessContactValidator;
        private readonly BusinessContactRepository _businessContactRepository = businessContactRepository;

        public Result<RegisterBusinessContactResponse, Notification> RegisterBusinessContact(RegisterBusinessContactRequest request, Guid userId)
        {
            Result<Email, Notification> resultEmail = Email.Create(request.Email.Trim());
            if (resultEmail.IsFailure)
                return resultEmail.Error;

            Notification notification = _registerBusinessContactValidator.Validate(request);
            if (notification.HasErrors())
                return notification;



            string firstName = request.FirstName.Trim();
            string lastName = request.LastName.Trim();
            string position = request.Position.Trim();
            string cellPhone = request.CellPhone.Trim();
            string secondCellPhone = request.SecondCellPhone.Trim();
            string phone = request.Phone.Trim();
            string secondPhone = request.SecondPhone.Trim();
            Email email = resultEmail.Value;
            string comment = request.Comment.Trim();
            Guid businessId = request.BusinessId;




            BusinessContact businessContact = new(firstName, lastName, position, cellPhone, secondCellPhone
                , phone, secondPhone, email, comment, businessId, Guid.NewGuid());

            _businessContactRepository.Save(businessContact);

            _context.SaveChanges(userId);

            var response = new RegisterBusinessContactResponse
            {
                Id = businessContact.Id,
                FirstName = businessContact.FirstName,
                LastName = businessContact.LastName,
                Position = businessContact.Position,
                CellPhone = businessContact.CellPhone,
                Phone = businessContact.Phone,
                SecondPhone = businessContact.SecondPhone,
                SecondCellPhone = businessContact.SecondCellPhone,
                Email = businessContact.Email.Value,
                Comment = businessContact.Comment,
                BusinessId = businessContact.BusinessId,
                Status = businessContact.Status,
            };

            return response;
        }

        public EditBusinessContactResponse EditBusinessContact(EditBusinessContactRequest request, BusinessContact businessContact, Guid userId)
        {
            businessContact.FirstName = request.FirstName.Trim();
            businessContact.LastName = request.LastName.Trim();
            businessContact.Position = request.Position.Trim();
            businessContact.CellPhone = request.CellPhone.Trim();
            businessContact.Phone = request.Phone.Trim();
            businessContact.SecondPhone = request.SecondPhone.Trim();
            businessContact.SecondCellPhone = request.SecondCellPhone.Trim();
            businessContact.Email = Email.Create(request.Email).Value;
            businessContact.Comment = request.Comment.Trim();

            _context.SaveChanges(userId);

            var response = new EditBusinessContactResponse
            {
                Id = businessContact.Id,
                FirstName = businessContact.FirstName,
                LastName = businessContact.LastName,
                Position = businessContact.Position,
                CellPhone = businessContact.CellPhone,
                Phone = businessContact.Phone,
                SecondPhone = businessContact.SecondPhone,
                SecondCellPhone = businessContact.SecondCellPhone,
                Email = businessContact.Email.Value,
                Comment = businessContact.Comment,
                BusinessId = businessContact.BusinessId,
                Status = businessContact.Status,
            };

            return response;
        }

        public EditBusinessContactResponse ActiveBusinessContact(BusinessContact businessContact, Guid userId)
        {
            businessContact.Status = true;

            _context.SaveChanges(userId);

            var response = new EditBusinessContactResponse
            {
                Id = businessContact.Id,
                FirstName = businessContact.FirstName,
                LastName = businessContact.LastName,
                Position = businessContact.Position,
                CellPhone = businessContact.CellPhone,
                Phone = businessContact.Phone,
                SecondPhone = businessContact.SecondPhone,
                SecondCellPhone = businessContact.SecondCellPhone,
                Email = businessContact.Email.Value,
                Comment = businessContact.Comment,
                BusinessId = businessContact.BusinessId,
                Status = businessContact.Status,
            };

            return response;
        }
        public Notification ValidateEditBusinessContactRequest(EditBusinessContactRequest request)
        {
            return _editBusinessContactValidator.Validate(request);
        }

        public EditBusinessContactResponse RemoveBusinessContact(BusinessContact businessContact, Guid userId)
        {
            businessContact.Status = false;
            _context.SaveChanges(userId);

            var response = new EditBusinessContactResponse
            {
                Id = businessContact.Id,
                FirstName = businessContact.FirstName,
                LastName = businessContact.LastName,
                Position = businessContact.Position,
                CellPhone = businessContact.CellPhone,
                Phone = businessContact.Phone,
                SecondPhone = businessContact.SecondPhone,
                SecondCellPhone = businessContact.SecondCellPhone,
                Email = businessContact.Email.Value,
                Comment = businessContact.Comment,
                BusinessId = businessContact.BusinessId,
                Status = businessContact.Status,
            };

            return response;
        }

        public BusinessContact? GetById(Guid id)
        {
            return _businessContactRepository.GetById(id);
        }

        public BusinessContactDto? GetDtoById(Guid id)
        {
            return _businessContactRepository.GetDtoById(id);
        }

        
        public List<BusinessContactDto> GetListAll(Guid businessId)
        {
            return _businessContactRepository.GetListAll(businessId);
        }

        public Tuple<IEnumerable<BusinessContactDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid businessId, bool status, string firstNameSearch = "", string lastNameSearch = "", string emailSearch = "")
        {
            return _businessContactRepository.GetList(pageNumber, pageSize, businessId, status, firstNameSearch, lastNameSearch, emailSearch);
        }

      

    }
}
