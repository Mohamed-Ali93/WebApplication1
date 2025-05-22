using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShop.Infrastructure
{
    public class BookShopDbContext : DbContext
    {
        private readonly ILogger<BookShopDbContext> _logger;

        public BookShopDbContext(DbContextOptions<BookShopDbContext> options, ILogger<BookShopDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(message => _logger.LogInformation(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _logger.LogInformation("Configuring Book entity...");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Stock)
                      .IsRequired();
            });

            _logger.LogInformation("Seeding initial data...");
            // Seed initial data
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 9.99m, Stock = 10 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 12.99m, Stock = 15 },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", Price = 11.99m, Stock = 20 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Price = 10.99m, Stock = 12 }
            );
            _logger.LogInformation("Model configuration completed.");
        }
    }
}