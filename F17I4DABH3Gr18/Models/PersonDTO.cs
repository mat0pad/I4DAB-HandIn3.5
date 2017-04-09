using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F17I4DABH3Gr18.Models
{
    public class PersonDTO
    {
        public long Id { get; set; }

        public string Fornavn { get; set; }

        public string Efternavn { get; set; }

        public long AdresseId { get; set; }
    }


    public class PersonDetailDTO
    {
        public PersonDetailDTO()
        {
            HarAdresse = new HashSet<HarAdresseDTO>();
            TelefonNrs = new HashSet<TelefonNrDTO>();
        }

        public long Id { get; set; }

        public string Fornavn { get; set; }

        public string Mellemnavn { get; set; }

        public string Efternavn { get; set; }

        public string Type { get; set; }

        public long PrimaryAdressId { get; set; }

        public virtual ICollection<HarAdresseDTO> HarAdresse { get; set; }

        public virtual ICollection<TelefonNrDTO> TelefonNrs { get; set; }
    }
}