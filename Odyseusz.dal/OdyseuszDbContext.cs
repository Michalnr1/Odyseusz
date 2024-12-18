using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Odyseusz.domain;

namespace Odyseusz.dal
{
    public class OdyseuszDbContext : DbContext
    {
        public OdyseuszDbContext(DbContextOptions<OdyseuszDbContext> options) : base(options) { }

        public DbSet<Adres> Adresy { get; set; }
        public DbSet<DaneOsobowe> DaneOsobowe { get; set; }
        public DbSet<EtapPodrozy> EtapyPodrozy { get; set; }
        public DbSet<Kraj> Kraje { get; set; }
        public DbSet<ZgloszeniePodrozy> ZgloszeniaPodrozy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kraj>()
                .HasKey(k => k.Nazwa); // Definicja klucza głównego

            modelBuilder.Entity<Adres>()
                .HasOne(a => a.Kraj)
                .WithMany()
                .HasForeignKey(a => a.NazwaKraju)
                .OnDelete(DeleteBehavior.Restrict); // Konfiguracja klucza obcego

            // Wczytanie krajów
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "countries.xml");
            XDocument xdoc = XDocument.Load(filePath);
            var kraje = xdoc.Descendants("country")
                .Select(c => new Kraj
                {
                    Nazwa = c.Element("name")?.Value,
                    KodKraju = c.Element("code")?.Value
                })
                .ToList();
            modelBuilder.Entity<Kraj>().HasData(kraje);

            //modelBuilder.Entity<Kraj>().HasQueryFilter(k => false);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Kraj>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Unchanged; // Blokowanie dodawania nowych krajów
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Kraj>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Unchanged; // Blokowanie dodawania nowych krajów
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
