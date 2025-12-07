using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Dtos;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Validators;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Diagnostics.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Diagnostics.Domain.Services
{
    public class DiagnosticApplicationService(AnaPreventionContext context, RegisterDiagnosticValidator registerDiagnosticValidator, EditDiagnosticValidator editDiagnosticValidator, DiagnosticRepository diagnosticRepository)
    {
        private readonly AnaPreventionContext _context = context;
        private readonly RegisterDiagnosticValidator _registerDiagnosticValidator = registerDiagnosticValidator;
        private readonly EditDiagnosticValidator _editDiagnosticValidator = editDiagnosticValidator;
        private readonly DiagnosticRepository _diagnosticRepository = diagnosticRepository;

        public Result<RegisterDiagnosticResponse, Notification> RegisterDiagnostic(RegisterDiagnosticRequest request, Guid userId)
        {
            Notification notification = _registerDiagnosticValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string cie10 = request.Cie10.Trim();
            string? description2 = request.Description2;
            string? diagnosticOptional = request.DiagnosticOptional;


            Diagnostic diagnostic = new(description, cie10, description2, diagnosticOptional, Guid.NewGuid());

            _diagnosticRepository.Save(diagnostic);

            _context.SaveChanges(userId);

            var response = new RegisterDiagnosticResponse
            {
                Id = diagnostic.Id,
                Description = diagnostic.Description,
                Cie10 = diagnostic.Cie10,
                Description2 = diagnostic.Description2,
                DiagnosticOptional = diagnosticOptional,
                Status = diagnostic.Status,
            };

            return response;
        }

        public EditDiagnosticResponse EditDiagnostic(EditDiagnosticRequest request, Diagnostic diagnostic, Guid userId)
        {
            diagnostic.Description = request.Description.Trim();
            diagnostic.Description2 = request.Description2;
            diagnostic.DiagnosticOptional = request.DiagnosticOptional;
            diagnostic.Cie10 = request.Cie10.Trim();
            diagnostic.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditDiagnosticResponse
            {
                Id = diagnostic.Id,
                Description = diagnostic.Description,
                Cie10 = diagnostic.Cie10,
                DiagnosticOptional = diagnostic.DiagnosticOptional,
                Status = diagnostic.Status
            };

            return response;
        }

        public EditDiagnosticResponse ActiveDiagnostic(Diagnostic diagnostic, Guid userId)
        {
            diagnostic.Status = true;

            _context.SaveChanges(userId);

            var response = new EditDiagnosticResponse
            {
                Id = diagnostic.Id,
                Description = diagnostic.Description,
                Cie10 = diagnostic.Cie10,
                Description2 = diagnostic.Description2,
                DiagnosticOptional = diagnostic.DiagnosticOptional,
                Status = diagnostic.Status
            };

            return response;
        }
        public Notification ValidateEditDiagnosticRequest(EditDiagnosticRequest request)
        {
            return _editDiagnosticValidator.Validate(request);
        }
        public EditDiagnosticResponse RemoveDiagnostic(Diagnostic diagnostic, Guid userId)
        {
            diagnostic.Status = false;
            _context.SaveChanges(userId);

            var response = new EditDiagnosticResponse
            {
                Id = diagnostic.Id,
                Description = diagnostic.Description,
                Description2 = diagnostic.Description2,
                Cie10 = diagnostic.Cie10,
                DiagnosticOptional = diagnostic.DiagnosticOptional,
                Status = diagnostic.Status
            };

            return response;
        }

        public Diagnostic? GetById(Guid id)
        {
            return _diagnosticRepository.GetById(id);
        }
        public List<Diagnostic> GetListAll(string? description, string? cie10Search)
        {
            return _diagnosticRepository.GetListAll(description, cie10Search);
        }

        public Tuple<IEnumerable<Diagnostic>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string? descriptionSearch, string? description2Search, string? cie10Search)
        {
            return _diagnosticRepository.GetList(pageNumber, pageSize, status, descriptionSearch, description2Search, cie10Search);
        }
    }
}
