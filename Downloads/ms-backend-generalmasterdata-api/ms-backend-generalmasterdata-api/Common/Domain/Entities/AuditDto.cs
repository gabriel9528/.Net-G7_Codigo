namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities
{
    public class AuditDto
    {
        public Guid Id { get; set; }                    /*Log id*/
        public string Date { get; set; } = string.Empty;
        public string Hour { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string AuditType { get; set; } = string.Empty;           /*Create, Update or Delete*/
        public Guid userId { get; set; }           
        public string TableName { get; set; } = string.Empty;        /*Table where rows been created/updated/deleted*/
        public string? KeyValues { get; set; }           /*Table Pk and it's values*/
        public string? OldValues { get; set; }           /*Changed column name and old value*/
        public string? NewValues { get; set; }           /*Changed column name and current value*/
        public Guid ItemId { get; set; }      /*Id del item*/
        public List<Dictionary<string, string>>? ResumeAudit { get; set; }
    }
}
