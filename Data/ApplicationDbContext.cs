using Microsoft.EntityFrameworkCore;
using Feedback.Models;

namespace Feedback.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<FeedBack> FeedBack { get; set; }
    }
}
