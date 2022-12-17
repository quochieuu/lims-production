namespace LIMS.Data.Entities
{
    public class BeenLove : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Background { get; set; }
        public DateTime StartDate { get; set; }
        public string User1 { get; set; }
        public string Username1 { get; set; }
        public string AvatarUser1 { get; set; }
        public DateTime AgeUser1 { get; set; }
        public string User2 { get; set; }
        public string Username2 { get; set; }
        public string AvatarUser2 { get; set; }
        public DateTime AgeUser2 { get; set; }
    }
}
