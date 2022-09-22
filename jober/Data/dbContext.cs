using System;
using jober.Models;
using Microsoft.EntityFrameworkCore;
namespace jober.Data
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
            :base(options)
        {

        }

        public DbSet<posts> posts { get; set; } = null!;
    }
}

