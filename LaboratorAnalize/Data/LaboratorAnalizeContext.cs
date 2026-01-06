using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Data
{
    public class LaboratorAnalizeContext : DbContext
    {
        public LaboratorAnalizeContext (DbContextOptions<LaboratorAnalizeContext> options)
            : base(options)
        {
        }

        public DbSet<Programare> Programare { get; set; }
        public DbSet<Pacient> Pacient { get; set; }
        public DbSet<PachetAnalize> PachetAnalize { get; set; }
    }
}
