using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ModelBuilderSample
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options):base(options)
        {
            
        }
    }

    
}