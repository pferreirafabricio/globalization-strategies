using GlobalizationApiSql.Domain;
using Microsoft.EntityFrameworkCore;

namespace GlobalizationApiSql.Database;

public class TechnicalMessagesDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Language>? Languages { get; set; }
    public DbSet<TechnicalMessage>? TechnicalMessages { get; set; }
}
