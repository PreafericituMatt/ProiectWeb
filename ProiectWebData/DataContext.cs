using Microsoft.EntityFrameworkCore;
using ProiectWebData.Entities;

namespace ProiectWebData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Items> Items { get; set; }
    }
}