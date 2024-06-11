using ExampleTest2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleTest2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Character_Title> CharacterTitles { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item {
                    Id = 1,
                    Name = "Stone",
                    Weight = 100
                },
                new Item {
                    Id = 2,
                    Name = "Iron",
                    Weight = 150
                },
                new Item {
                    Id = 3,
                    Name = "Gold",
                    Weight = 250
                },
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title {
                    Id = 1,
                    Name = "King of the hill"
                },
                new Title {
                    Id = 2,
                    Name = "Guy"
                },
                new Title {
                    Id = 3,
                    Name = "Guy"
                },
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    CurrentWeight = 100,
                    MaxWeight = 250
                },
                new Character
                {
                    Id = 2,
                    FirstName = "Nohj",
                    LastName = "Eod",
                    CurrentWeight = 100,
                    MaxWeight = 1000
                },                
                new Character
                {
                    Id = 3,
                    FirstName = "Susan",
                    LastName = "Doe",
                    CurrentWeight = 50,
                    MaxWeight = 50
                },                
                new Character
                {
                    Id = 4,
                    FirstName = "Alice",
                    LastName = "Doe",
                    CurrentWeight = 200,
                    MaxWeight = 250
                },
            });

            modelBuilder.Entity<Character_Title>().HasData(new List<Character_Title>
            {
                new Character_Title
                {
                    CharacterId = 1,
                    TitleId = 1,
                    AcquiredAt = DateTime.Parse("2024-06-04")
                },
                new Character_Title
                {
                    CharacterId = 2,
                    TitleId = 1,
                    AcquiredAt = DateTime.Parse("2024-01-03")
                },
                new Character_Title
                {
                    CharacterId = 2,
                    TitleId = 2,
                    AcquiredAt = DateTime.Parse("2024-08-10")
                },
                new Character_Title
                {
                    CharacterId = 3,
                    TitleId = 2,
                    AcquiredAt = DateTime.Parse("2024-03-12")
                },
                new Character_Title
                {
                    CharacterId = 4,
                    TitleId = 2,
                    AcquiredAt = DateTime.Parse("2024-02-07")
                },
            });

            modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 3,
                },
                new Backpack
                {
                    CharacterId = 2,
                    ItemId = 2,
                    Amount = 4,
                },
                new Backpack
                {
                    CharacterId = 2,
                    ItemId = 3,
                    Amount = 10,
                },
                new Backpack
                {
                    CharacterId = 3,
                    ItemId = 1,
                    Amount = 30,
                },
            });
    }
}
