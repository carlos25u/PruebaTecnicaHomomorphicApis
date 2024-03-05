using PruebaTecnica_Cifrado_Homomorfico.Service;
using PruebaTecnicaHomomorphicApis.DAL;
using PruebaTecnicaHomomorphicApis.modelos;

namespace PruebaTecnicaHomomorphicApis.Services
{
    public class UsuariosServices
    {
        private readonly EncriptadoService encriptar;
        private readonly Contexto contexto;

        public UsuariosServices(Contexto contexto)
        {
            encriptar = DependencyContainer.GetEncriptadoService();
            this.contexto = contexto;
        }

        public bool guardar(Usuarios usuarios)
        {
            bool paso = false;

            try
            {
                var id = encriptar.Encriptar(usuarios.UsuarioId);
                var nombre = encriptar.Encriptar(usuarios.Nombre);
                var usuario = encriptar.Encriptar(usuarios.Usuario);
                var clave = encriptar.Encriptar(usuarios.Password);

                var usuariosGuardar = new Usuarios()
                {
                    UsuarioId = id,
                    Usuario = usuario,
                    Nombre = nombre,
                    Password = clave,
                };

                contexto.Usuarios.Add(usuariosGuardar);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encriptar: {ex.ToString()}");
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        //hazme un login
        public Usuarios login(string usuario, string password)
        {
            Usuarios usuarioEncontrado = new Usuarios();
            try
            {
                var usuarioDesencriptado = encriptar.Desencriptar(usuario);
                var passwordDesencriptado = encriptar.Desencriptar(password);

                usuarioEncontrado = contexto.Usuarios.FirstOrDefault(u => u.Usuario == usuarioDesencriptado && u.Password == passwordDesencriptado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desencriptar: {ex.ToString()}");
            }
            finally
            {
                contexto.Dispose();
            }

            return usuarioEncontrado!;
        }
    }
}
