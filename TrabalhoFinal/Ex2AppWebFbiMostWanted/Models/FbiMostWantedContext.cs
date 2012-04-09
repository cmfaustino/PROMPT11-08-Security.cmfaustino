using System.Data.Entity;

namespace Ex2AppWebFbiMostWanted.Models
{
    // HACK: criada classe derivada de DbContext
    public class FbiMostWantedContext : DbContext
    {
        public DbSet<FbiMostWanted> FbiMostWantedCriminals { get; set; }
    }
}