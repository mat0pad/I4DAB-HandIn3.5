namespace F17I4DABH3Gr18.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PersonKartoket : DbContext
    {
        public PersonKartoket()
            : base("name=PersonKartoket")
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<HarAdresse> HarAdresses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<TelefonNr> TelefonNrs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.People)
                .WithRequired(e => e.Adresse)
                .WillCascadeOnDelete(false);
        }
    }
}
