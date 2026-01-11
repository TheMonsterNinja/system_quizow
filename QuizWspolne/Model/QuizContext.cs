using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Model
{
    public class QuizContext : DbContext
    {
        public DbSet<Quiz> Quizy { get; set; }
        public DbSet<Pytanie> Pytania { get; set; }
        public DbSet<Odpowiedz> Odpowiedzi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=QuizDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Pytania)
                .WithOne(p => p.Quiz)
                .HasForeignKey(p => p.QuizId);

            modelBuilder.Entity<Pytanie>()
                .HasMany(p => p.Odpowiedzi)
                .WithOne(o => o.Pytanie)
                .HasForeignKey(o => o.QuestionId);
        }

    }
}
