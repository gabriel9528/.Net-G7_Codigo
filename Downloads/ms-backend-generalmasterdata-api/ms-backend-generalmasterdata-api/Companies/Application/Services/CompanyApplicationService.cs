using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Companies.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Companies.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Companies.Infrastructure.Repositories;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Services
{
    public class CompanyApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterCompanyValidator _registerCompanyValidator;
        private readonly EditCompanyValidator _editCompanyValidator;
        private readonly CompanyRepository _companyRepository;

        public CompanyApplicationService(
       AnaPreventionContext context,
       RegisterCompanyValidator registerCompanyValidator,
       EditCompanyValidator editCompanyValidator,
       CompanyRepository companyRepository)
        {
            _context = context;
            _registerCompanyValidator = registerCompanyValidator;
            _editCompanyValidator = editCompanyValidator;
            _companyRepository = companyRepository;
        }

        public Result<RegisterCompanyResponse, Notification> RegisterCompany(RegisterCompanyRequest request, Guid userId)
        {
            Notification notification = _registerCompanyValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string setting = JsonSerializer.Serialize(request.Setting);


            Company company = new(description, setting, Guid.NewGuid());

            _companyRepository.Save(company);

            _context.SaveChanges(userId);

            var response = new RegisterCompanyResponse
            {
                Id = company.Id,
                Description = company.Description,
                Status = company.Status
            };

            return response;
        }

        public EditCompanyResponse EditCompany(EditCompanyRequest request, Company company, Guid userId)
        {
            company.Description = request.Description.Trim();
            company.Setting = JsonSerializer.Serialize(request.Setting);
            company.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditCompanyResponse
            {
                Id = company.Id,
                Description = company.Description,
                Status = company.Status
            };

            return response;
        }

        public EditCompanyResponse ActiveCompany(Company company, Guid userId)
        {
            company.Status = true;

            _context.SaveChanges(userId);

            var response = new EditCompanyResponse
            {
                Id = company.Id,
                Description = company.Description,
                Status = company.Status
            };

            return response;
        }
        public Notification ValidateEditCompanyRequest(EditCompanyRequest request)
        {
            return _editCompanyValidator.Validate(request);
        }

        public Result<EditCompanyResponse, Notification> RemoveCompany(Company company, Guid userId)
        {
            Notification notification = _editCompanyValidator.ValidateRemove(company);

            if (notification.HasErrors())
                return notification;

            company.Status = false;
            _context.SaveChanges(userId);

            var response = new EditCompanyResponse
            {
                Id = company.Id,
                Description = company.Description,
                Status = company.Status
            };

            return response;
        }

        public Company? GetById(Guid id)
        {
            return _companyRepository.GetById(id);
        }

        public CompanyDto? GetDtoById(Guid id)
        {
            return _companyRepository.GetDtoById(id);
        }

        public List<CompanyDto> GetListAll(Guid userId)
        {
            return _companyRepository.GetListAll(userId);
        }
       
        public Tuple<IEnumerable<CompanyDto>, PaginationMetadata> GetList(int pageNumber, int pageSize, Guid userId, bool status, string descriptionSearch = "")
        {
            return _companyRepository.GetList(pageNumber, pageSize, userId, status, descriptionSearch);
        }

    }
}
