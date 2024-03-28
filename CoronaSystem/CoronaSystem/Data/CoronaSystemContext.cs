using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoronaSystem.Models;

namespace CoronaSystem.Data
{
    public class CoronaSystemContext : DbContext
    {
        public CoronaSystemContext (DbContextOptions<CoronaSystemContext> options)
            : base(options)
        {
        }

        public DbSet<CoronaSystem.Models.Member> Member { get; set; } = default!;

        public DbSet<CoronaSystem.Models.Vaccination>? Vaccination { get; set; }

        public DbSet<CoronaSystem.Models.SickMember>? SickMember { get; set; }
    }
}
