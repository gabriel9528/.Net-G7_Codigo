using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.API;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.ItemTypes.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Services
{
    public class ItemTypeApplicationService
    {
        private readonly AnaPreventionContext _context;
        private readonly RegisterItemTypeValidator _registerItemTypeValidator;
        private readonly EditItemTypeValidator _editItemTypeValidator;
        private readonly ItemTypeRepository _itemTypeRepository;

        public ItemTypeApplicationService(
       AnaPreventionContext context,
       RegisterItemTypeValidator registerItemTypeValidator,
       EditItemTypeValidator editItemTypeValidator,
       ItemTypeRepository itemTypeRepository)
        {
            _context = context;
            _registerItemTypeValidator = registerItemTypeValidator;
            _editItemTypeValidator = editItemTypeValidator;
            _itemTypeRepository = itemTypeRepository;
        }

        public Result<RegisterItemTypeResponse, Notification> RegisterItemType(RegisterItemTypeRequest request, Guid userId)
        {
            Notification notification = _registerItemTypeValidator.Validate(request);

            if (notification.HasErrors())
                return notification;


            string description = request.Description.Trim();
            string code = GenerateCode();


            ItemType itemType = new(description, code, Guid.NewGuid());

            _itemTypeRepository.Save(itemType);

            _context.SaveChanges(userId);

            var response = new RegisterItemTypeResponse
            {
                Id = itemType.Id,
                Description = itemType.Description,
                Code = itemType.Code,
                Status = itemType.Status,
            };

            return response;
        }
        public string GenerateCode()
        {
            return _itemTypeRepository.GenerateCode();
        }

        public EditItemTypeResponse EditItemType(EditItemTypeRequest request, ItemType itemType, Guid userId)
        {
            itemType.Description = request.Description.Trim();
            itemType.Code = request.Code.Trim();
            itemType.Status = request.Status;


            _context.SaveChanges(userId);

            var response = new EditItemTypeResponse
            {
                Id = itemType.Id,
                Description = itemType.Description,
                Code = itemType.Code,
                Status = itemType.Status
            };

            return response;
        }

        public EditItemTypeResponse ActiveItemType(ItemType itemType, Guid userId)
        {
            itemType.Status = true;

            _context.SaveChanges(userId);

            var response = new EditItemTypeResponse
            {
                Id = itemType.Id,
                Description = itemType.Description,
                Code = itemType.Code,
                Status = itemType.Status
            };

            return response;
        }
        public Notification ValidateEditItemTypeRequest(EditItemTypeRequest request)
        {
            return _editItemTypeValidator.Validate(request);
        }

        public EditItemTypeResponse RemoveItemType(ItemType itemType, Guid userId)
        {
            itemType.Status = false;
            _context.SaveChanges(userId);

            var response = new EditItemTypeResponse
            {
                Id = itemType.Id,
                Description = itemType.Description,
                Code = itemType.Code,
                Status = itemType.Status
            };

            return response;
        }

        public ItemType? GetById(Guid id)
        {
            return _itemTypeRepository.GetById(id);
        }

        public ItemType? GetDtoById(Guid id)
        {
            return _itemTypeRepository.GetDtoById(id);
        }

        public List<ItemType> GetListAll()
        {
            return _itemTypeRepository.GetListAll();
        }
        public Tuple<IEnumerable<ItemType>, PaginationMetadata> GetList(int pageNumber, int pageSize, bool status, string descriptionSearch = "", string codeSearch = "")
        {
            return _itemTypeRepository.GetList(pageNumber, pageSize, status, descriptionSearch, codeSearch);
        }
    }
}
