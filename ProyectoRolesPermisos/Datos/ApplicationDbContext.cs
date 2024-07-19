using Microsoft.EntityFrameworkCore;
using ProyectoRolesPermisos.Models;

namespace ProyectoRolesPermisos.Datos
{
    public class ApplicationDbContext : DbContext
    {
        //Constructor para cargar la inyeccion de dependencias
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
