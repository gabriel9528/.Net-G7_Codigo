using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Infrastructure.EF;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Infrastructure.Repositories
{
    public class AttachmentRepository(AnaPreventionContext context) : Repository<Attachment>(context)
    {
        public List<Attachment>? GetByIdEntity(Guid entityId, EntityType entityType)
        {
            return [.. _context.Set<Attachment>().Where(t1 => t1.EntityId == entityId && t1.EntityType == entityType)];
        }

        public List<AttachmentDto> GetDtoByIdEntity(Guid entityId, EntityType entityType)
        {
            return [.. GetDtoQueryable().Where(t1 => t1.EntityId == entityId && t1.EntityType == entityType)];
        }

        public List<AttachmentDto> GetDtoByIdEntity(Guid entityId)
        {
            return GetDtoQueryable().Where(t1 => t1.EntityId == entityId).ToList();
        }

        public AttachmentDto? GetDtoById(Guid id)
        {
            return GetDtoQueryable().Where(t1 => t1.Id == id).FirstOrDefault();
        }

        public List<Attachment> GetByIds(List<Guid> ids)
        {
            return [.. _context.Set<Attachment>().Where(t1 => ids.Contains(t1.Id))];
        }        

        private IQueryable<AttachmentDto> GetDtoQueryable()
        {
            return (from t1 in _context.Set<Attachment>()
                    where t1.Status == true
                    orderby t1.DateCreated
                    select new AttachmentDto()
                    {
                        Id = t1.Id,
                        Name = t1.Name,
                        Url = t1.Url,
                        EntityType = t1.EntityType,
                        EntityId = t1.EntityId,
                        FileType = t1.FileType,
                        FileSize = t1.FileSize,
                        Status = t1.Status,
                        DateCreated =  t1.DateCreated.ToString(CommonStatic.FormatDate)

                    });
        }
    }
}