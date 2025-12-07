using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Taxes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Taxes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Taxes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Taxes.Application.Services
{
    public class TaxApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterTaxValidator _registerTaxValidator;
        private readonly EditTaxValidator _editTaxValidator;
        private readonly TaxRepository _taxRepository;

        public TaxApplicationService(
       AnaPreventionContext context,
       RegisterTaxValidator registerTaxValidator,
       EditTaxValidator editTaxValidator,
       TaxRepository taxRepository)
        {
            _context = context;
            _registerTaxValidator = registerTaxValidator;
            _editTaxValidator = editTaxValidator;
            _taxRepository = taxRepository;
        }

        public Result<RegisterTaxResponse, Notification> RegisterTax(RegisterTaxRequest request,Guid userId)
        {
            Notification notification = _registerTaxValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = request.Code.Trim();
            decimal rate = request.Rate;


            Tax tax = new(description,rate, code, Guid.NewGuid());

            _taxRepository.Save(tax);

            _context.SaveChanges(userId);

            var response = new RegisterTaxResponse
            {
                Id = tax.Id,
                Description = tax.Description,
                Code = tax.Code,
                Rate = tax.Rate,
                Status = tax.Status,
            };

            return response;
        }

        public EditTaxResponse EditTax(EditTaxRequest request, Tax tax,Guid userId)
        {
            tax.Description = request.Description.Trim();
            tax.Code = request.Code.Trim();
            tax.Rate = request.Rate;
            tax.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditTaxResponse
            {
                Id = tax.Id,
                Description = tax.Description,
                Code = tax.Code,
                Rate = tax.Rate,
                Status = tax.Status
            };

            return response;
        }

        public EditTaxResponse ActiveTax(Tax tax, Guid userId)
        {
            tax.Status = true;

            _context.SaveChanges(userId);

            var response = new EditTaxResponse
            {
                Id = tax.Id,
                Description = tax.Description,
                Code = tax.Code,
                Rate = tax.Rate,
                Status = tax.Status
            };

            return response;
        }
        public Notification ValidateEditTaxRequest(EditTaxRequest request)
        {
            return _editTaxValidator.Validate(request);
        }

        public EditTaxResponse RemoveTax(Tax tax,Guid userId)
        {
            tax.Status = false;
            _context.SaveChanges(userId);

            var response = new EditTaxResponse
            {
                Id = tax.Id,
                Description = tax.Description,
                Status = tax.Status
            };

            return response;
        }

        public Tax? GetById(Guid id)
        {
            return _taxRepository.GetById(id);
        }

        public Tax? GetDtoById(Guid id)
        {
            return _taxRepository.GetDtoById(id);
        }
        public List<Tax> GetListAll()
        {
            return _taxRepository.GetListAll();
        }
        public Tuple<IEnumerable<Tax>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _taxRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
