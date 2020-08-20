using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ngServer.Infrastructure.Domains;

namespace ngServer.Infrastructure.Contexts
{
    public class ClasesContext : DbContext
    {
        public ClasesContext(DbContextOptions<ClasesContext> options) : base(options)
        {

        }

        public DbSet<Clase> Clases { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Seccion> Secciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
