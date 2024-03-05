using Microsoft.EntityFrameworkCore;
using PruebaTecnicaHomomorphicApis.modelos;

namespace PruebaTecnicaHomomorphicApis.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
