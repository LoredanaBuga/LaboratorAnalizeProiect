using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LaboratorAnalize.Models;

namespace LaboratorAnalize.Data
{
    public class LaboratorAnalizeContext : IdentityDbContext
    {
        public LaboratorAnalizeContext(DbContextOptions<LaboratorAnalizeContext> options)
            : base(options)
        {
        }

        public DbSet<Programare> Programare { get; set; }
        public DbSet<Pacient> Pacient { get; set; }
        public DbSet<PachetAnalize> PachetAnalize { get; set; }
        public DbSet<TipAnaliza> TipAnaliza { get; set; } = default!;
        public DbSet<ProgramareTipAnaliza> ProgramareTipAnaliza { get; set; } = default!;
        public DbSet<BuletinAnalize> BuletinAnalize { get; set; } = default!;
        public DbSet<RezultatAnaliza> RezultatAnaliza { get; set; } = default!;
    }
}
