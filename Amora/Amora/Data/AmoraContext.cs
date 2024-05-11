using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Amora.Models;

namespace Amora.Data
{
    public class AmoraContext : DbContext
    {
        public AmoraContext (DbContextOptions<AmoraContext> options)
            : base(options)
        {
        }

        public DbSet<Amora.Models.RegisterViewModel> RegisterViewModel { get; set; } = default!;
    }
}
