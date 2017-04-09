namespace F17I4DABH3Gr18.Models
{
    public class HarAdresseDTO
    {
        public long AdresseId { get; set; }

        public long PersonId { get; set; }

    }

    public class HarAdresseDetailDTO
    {
        public long AdresseId { get; set; }

        public long PersonId { get; set; }

        public string Type { get; set; }
    }
}