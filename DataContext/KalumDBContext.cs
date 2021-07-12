using System.IO;
using kalum2021.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace kalum2021.DataContext
{
    public class KalumDBContext : DbContext 
    {
        public DbSet<Alumnos> Alumnos {get;set;}
        public DbSet<Salones> Salones {get;set;}
        public DbSet<Horarios> Horarios {get;set;}
        public DbSet<Instructores> Instructores {get;set;}
        public DbSet<CarrerasTecnicas> CarrerasTecnicas {get;set;}
        public DbSet <Clases> Clases {get;set;}
        public KalumDBContext(DbContextOptions<KalumDBContext> options)
            :base(options)
        {

        }

        public KalumDBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumnos>()
                .ToTable("Alumnos")
                .HasKey(c => new {c.Carne});
            modelBuilder.Entity<Salones>()
                .ToTable("Salones")
                .HasKey(s => new {s.SalonId});
            modelBuilder.Entity<Horarios>()
                .ToTable("Horarios")
                .HasKey(h => new {h.HorarioId});
            modelBuilder.Entity<Instructores>()
                .ToTable("Instructores")
                .HasKey(i => new {i.InstructorId});
            modelBuilder.Entity<CarrerasTecnicas>()
                .ToTable("CarrerasTecnicas")
                .HasKey(ct => new {ct.CodigoCarrera});
            modelBuilder.Entity<Clases>()
                .ToTable("Clases")
                .HasKey(c => new {c.ClaseId});
            modelBuilder.Entity<Clases>()
                .HasOne<CarrerasTecnicas>(c => c.CarrerasTecnicas)
                .WithMany(ct => ct.Clases)
                .HasForeignKey(c => c.CodigoCarrera);
            modelBuilder.Entity<Clases>()
                .HasOne<Horarios>(h => h.Horarios)
                .WithMany(h => h.Clases)
                .HasForeignKey(c => c.HorarioId);
            modelBuilder.Entity<Clases>()
                .HasOne<Instructores>(i => i.Instructores)
                .WithMany(i => i.Clases)
                .HasForeignKey(c => c.InstructorId);
            modelBuilder.Entity<Clases>()
                .HasOne<Salones>(s => s.Salones)
                .WithMany(s => s.Clases)
                .HasForeignKey(c => c.SalonId);
        }
    }
}