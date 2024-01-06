using ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ExamenContext:DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Conseiller> Conseillers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
            Initial Catalog=ExamenFestivalDB;Integrated Security=true;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configurer le type détenu
            modelBuilder.Entity<Activite>().OwnsOne(a => a.Zone);
            //Configurer la clé primaire de la table porteuse
            modelBuilder.Entity<Reservation>().HasKey(r => new { r.PackFk, r.ClientFk, r.DateReservation });
            base.OnModelCreating(modelBuilder);
            //Configurer la relation 1-*
            modelBuilder.Entity<Conseiller>().HasMany(cons => cons.Clients)
                .WithOne(cl => cl.Conseiller)
                .HasForeignKey(cl => cl.ConseillerFk)
                .OnDelete(DeleteBehavior.Cascade);

        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(100);

            base.ConfigureConventions(configurationBuilder);
        }

    }
}
