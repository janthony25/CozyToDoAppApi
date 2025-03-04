using CozyToDoAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CozyToDoAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<TaskItem> TaskItems { get; set; }      
    }
}
