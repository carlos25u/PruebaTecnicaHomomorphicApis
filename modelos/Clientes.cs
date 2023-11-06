using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaHomomorphicApis.modelos
{
    public class Clientes
    {
        [Key]
        [MaxLength(4000)]
        public String? IdCliente { get; set; }
        public String? CedulaSerial { get; set; }
        public String? Nombres { get; set; }
        public String? Direccion { get; set; }
        public String? Telefono { get; set; }
        public String? LimiteCredito { get; set; }
    }
}
