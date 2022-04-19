using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Homework1.EF
{
    internal class TeamContext : DbContext
    {
        public DbSet<Team> Teams { get; set;}
        public DbSet<Player> Player { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AE5PD09; Database=TeamDB; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>().HasData(
             new Team { Id = 1, Title = "First"},
             new Team { Id = 2, Title = "Second" },
             new Team { Id = 3, Title = "Third" }
            );

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, Name = "Player1", Age = 21, Cost = 10000, idTeam = 2},
                new Player { Id = 2, Name = "Player2", Age = 22, Cost = 1000, idTeam = 1 },
                new Player { Id = 3, Name = "Player3", Age = 23, Cost = 100, idTeam = 3 },
                new Player { Id = 4, Name = "Player4", Age = 24, Cost = 10000, idTeam = 1 },
                new Player { Id = 5, Name = "Player5", Age = 25, Cost = 1000, idTeam = 2 },
                new Player { Id = 6, Name = "Player6", Age = 26, Cost = 100, idTeam = 3 },
                new Player { Id = 7, Name = "Player7", Age = 27, Cost = 10000, idTeam = 3 },
                new Player { Id = 8, Name = "Player8", Age = 28, Cost = 1000, idTeam = 2 },
                new Player { Id = 9, Name = "Player9", Age = 29, Cost = 100, idTeam = 1 }
                );
        }

        public void CreateDbIfNotExist()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
    }

    public class Player
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public double Cost { get; set; }

        public int? idTeam { get; set; }
        [ForeignKey("idTeam")]
        public Team team { get; set; }
    }

    public class Team
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
