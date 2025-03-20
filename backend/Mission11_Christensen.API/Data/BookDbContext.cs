using Microsoft.EntityFrameworkCore;
namespace Mission11_Christensen.API.Data;

public class BookDbContext : DbContext 
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
}