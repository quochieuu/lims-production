namespace LIMS.Data.Entities
{
    public class TaskLabel : AuditableBaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid LabelId { get; set; }
    }
}
