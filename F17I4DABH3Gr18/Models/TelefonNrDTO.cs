namespace F17I4DABH3Gr18.Models
{
    public class TelefonNrDTO
    {
        public long Id { get; set; }

        public long PersonId { get; set; }
    }

    public class TelefonNrDetailDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public long Nummer { get; set; }

        public long PersonId { get; set; }
    }
}