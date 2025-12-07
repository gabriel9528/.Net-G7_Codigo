namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities
{
    public class Audit
    {
        public Guid Id { get; set; }                    /*Log id*/
        public DateTime Datetime { get; set; }  /*Log time*/
        public string? AuditType { get; set; }           /*Create, Update or Delete*/
        public Guid UserId { get; set; }           /*Log User*/
        public string TableName { get; set; }           /*Table where rows been 
                                                          created/updated/deleted*/
        public string KeyValues { get; set; }           /*Table Pk and it's values*/
        public string OldValues { get; set; }           /*Changed column name and old value*/
        public string NewValues { get; set; }           /*Changed column name 
                                                          and current value*/
        public Guid ItemId { get; set; }      /*Id del item*/
    }
}
