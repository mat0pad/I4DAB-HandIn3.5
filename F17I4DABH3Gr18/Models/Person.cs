namespace F17I4DABH3Gr18.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Person")]
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            HarAdresses = new HashSet<HarAdresse>();
            TelefonNrs = new HashSet<TelefonNr>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonId { get; set; }

        [Required]
        [StringLength(255)]
        public string Fornavn { get; set; }

        [StringLength(255)]
        public string Mellemnavn { get; set; }

        [Required]
        [StringLength(255)]
        public string Efternavn { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public long AdresseId { get; set; }

        public virtual Adresse Adresse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HarAdresse> HarAdresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelefonNr> TelefonNrs { get; set; }
    }
}
