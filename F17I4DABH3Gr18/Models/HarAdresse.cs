namespace F17I4DABH3Gr18.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HarAdresse")]
    public partial class HarAdresse
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AdresseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonId { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Person Person { get; set; }
    }
}
