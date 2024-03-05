using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaHomomorphicApis.modelos
{
    public class Usuarios
    {
        [Key]
        [MaxLength(4000)]
        public string? UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
    }
}
