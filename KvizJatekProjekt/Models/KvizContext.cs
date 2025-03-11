using LegyenOnIsMilliardosClassLibary.Models;
using Microsoft.EntityFrameworkCore;

namespace KvizJatekProjekt.Data
{
    public class KvizJatekContext : DbContext
    {
        public DbSet<MultipleChoice> MultipleChoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if (!optionsBuilder.IsConfigured)
            {
                // Configure the MySQL connection string using Pomelo
                string connectionString = "server=localhost;userid=root;password=;database=kvizjatek";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the Valaszlehetosegek column to the database
            modelBuilder.Entity<MultipleChoice>()
                .Property(m => m.Valaszlehetosegek)
                .HasColumnName("Valaszlehetosegek");

            // Seed 5 Hungarian multiple choice questions
            modelBuilder.Entity<MultipleChoice>().HasData(
                new MultipleChoice
                {
                    Id = 1,
                    Kerdes_Szoveg = "Mi Magyarország fővárosa?",
                    Valaszlehetosegek = "Debrecen, Szeged, Budapest, Pécs",
                    Jovalasz = "Budapest"
                },
                new MultipleChoice
                {
                    Id = 2,
                    Kerdes_Szoveg = "Melyik évben lett Magyarország EU tag?",
                    Valaszlehetosegek = "2004, 2010, 1999, 2015",
                    Jovalasz = "2004"
                },
                new MultipleChoice
                {
                    Id = 3,
                    Kerdes_Szoveg = "Melyik folyó folyik át Budapesten?",
                    Valaszlehetosegek = "Tisza, Duna, Rába, Dráva",
                    Jovalasz = "Duna"
                },
                new MultipleChoice
                {
                    Id = 4,
                    Kerdes_Szoveg = "Melyik magyar költő írta a Nemzeti dalt?",
                    Valaszlehetosegek = "József Attila, Petőfi Sándor, Ady Endre, Radnóti Miklós",
                    Jovalasz = "Petőfi Sándor"
                },
                new MultipleChoice
                {
                    Id = 5,
                    Kerdes_Szoveg = "Melyik városban található a híres Halászbástya?",
                    Valaszlehetosegek = "Eger, Győr, Budapest, Veszprém",
                    Jovalasz = "Budapest"
                }
            );
        }

        // Seed the database if it's empty
        public static void Seed(KvizJatekContext context)
        {
            if (!context.MultipleChoices.Any())
            {
                context.MultipleChoices.AddRange(
                    new MultipleChoice
                    {
                        Id = 1,
                        Kerdes_Szoveg = "Mi Magyarország fővárosa?",
                        Valaszlehetosegek = "Debrecen, Szeged, Budapest, Pécs",
                        Jovalasz = "Budapest"
                    },
                    new MultipleChoice
                    {
                        Id = 2,
                        Kerdes_Szoveg = "Melyik évben lett Magyarország EU tag?",
                        Valaszlehetosegek = "2004, 2010, 1999, 2015",
                        Jovalasz = "2004"
                    },
                    new MultipleChoice
                    {
                        Id = 3,
                        Kerdes_Szoveg = "Melyik folyó folyik át Budapesten?",
                        Valaszlehetosegek = "Tisza, Duna, Rába, Dráva",
                        Jovalasz = "Duna"
                    },
                    new MultipleChoice
                    {
                        Id = 4,
                        Kerdes_Szoveg = "Melyik magyar költő írta a Nemzeti dalt?",
                        Valaszlehetosegek = "József Attila, Petőfi Sándor, Ady Endre, Radnóti Miklós",
                        Jovalasz = "Petőfi Sándor"
                    },
                    new MultipleChoice
                    {
                        Id = 5,
                        Kerdes_Szoveg = "Melyik városban található a híres Halászbástya?",
                        Valaszlehetosegek = "Eger, Győr, Budapest, Veszprém",
                        Jovalasz = "Budapest"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}