namespace F17I4DABH3Gr18.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adresse")]
    public partial class Adresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adresse()
        {
            HarAdresses = new HashSet<HarAdresse>();
            People = new HashSet<Person>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AdresseId { get; set; }

        [Required]
        [StringLength(255)]
        public string Vejnavn { get; set; }

        public long HusNr { get; set; }

        public long PostNr { get; set; }

        [Required]
        [StringLength(255)]
        public string Bynavn { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HarAdresse> HarAdresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}
