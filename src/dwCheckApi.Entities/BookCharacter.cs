namespace dwCheckApi.Entities
{
    public class BookCharacter : BaseAuditClass
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CharacterId {get; set; }
        public virtual Character Character { get; set; }
    }
}