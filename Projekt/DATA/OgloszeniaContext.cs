using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.DATA
{
    public class OgloszeniaContext : DbContext
    {
        public OgloszeniaContext(DbContextOptions<OgloszeniaContext> options)
            : base(options)
        {
        }

        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Ogloszenie> Ogloszenie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Uzytkownik>().ToTable("Uzytkownik");//Dodaj odpowiednie mapowania odnoszące się do klasy modelu 
            modelBuilder.Entity<Kategoria>().ToTable("Kategoria");//Dodaj odpowiednie mapowania odnoszące się do klasy modelu 
            modelBuilder.Entity<Ogloszenie>().ToTable("Ogloszenie");
        }
    }
}
