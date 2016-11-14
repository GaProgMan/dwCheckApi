namespace dwCheckApi.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public virtual Book Book { get; set; }
    }
}