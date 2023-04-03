using Microsoft.EntityFrameworkCore;

namespace FinesApi.Models;

public class FineContext : DbContext
{
    public FineContext(DbContextOptions<FineContext> options)
         : base(options)
         {
         }

    public DbSet<Fine> Fines {get; set;} = null!;
    
}