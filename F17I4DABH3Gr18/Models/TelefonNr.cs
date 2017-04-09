namespace F17I4DABH3Gr18.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TelefonNr")]
    public partial class TelefonNr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TelefonId { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public long Nummer { get; set; }

        public long PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
