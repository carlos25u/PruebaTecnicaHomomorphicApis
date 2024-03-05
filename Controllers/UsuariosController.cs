using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaHomomorphicApis.modelos;
using PruebaTecnicaHomomorphicApis.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PruebaTecnicaHomomorphicApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController:  ControllerBase
    {
        private readonly UsuariosServices usuariosRepository;
        public UsuariosController(UsuariosServices usuariosRepository)
        {
            this.usuariosRepository = usuariosRepository;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        //{
        //    var usuarios = usuariosRepository.GetUsuarios();
        //    return usuarios;
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Usuarios>> GetUsuario(String id)
        //{
        //    var usuario = usuariosRepository.buscar(id);

        //    if (usuario != null)
        //    {
        //        return usuario;
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            if (usuariosRepository.guardar(usuarios))
            {
                return Ok(usuarios);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuarios>> Login(Usuarios usuarios)
        {
            var usuario = usuariosRepository.login(usuarios.Usuario, usuarios.Password);

            if (usuario != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuarios.Usuario),
                        new Claim("Id", usuarios.UsuarioId.ToString()),
                        new Claim("Nombre", usuarios.Nombre.ToString()),
                      
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteUsuarios(String id)
        //{
        //    if (usuariosRepository.eliminar(id))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
