namespace F17I4DABH3Gr18.Models
{
    public class AdresseDTO
    {
        public long Id { get; set; }

        public string Vejnavn { get; set; }

        public long PostNr { get; set; }
    }

    public class AdresseDetailDTO
    {
        public long Id { get; set; }

        public string Vejnavn { get; set; }

        public long PostNr { get; set; }

        public long HusNr { get; set; }

        public string Bynavn { get; set; }

        public string Type { get; set; }
    }

}