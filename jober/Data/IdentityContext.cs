using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace jober.Data
{
    public class IdentityContext:IdentityDbContext<IdentityUser>   
    {
        public IdentityContext(DbContextOptions options):base(options)
        {
            
        }
    }
}

