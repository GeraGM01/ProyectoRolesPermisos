using ProyectoRolesPermisos.Datos;
using ProyectoRolesPermisos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace ProyectoRolesPermisos.Controllers
{
    public class AccesoController : Controller
    {
        //Este controlador nos va a servir para hacer el login de inicio de sesión
        public IActionResult Index()
        {
            return View();
        }


        //Metodo para obtener la info del formulario de login y validar si el usuario puede ingresar al sistema o no
        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            ModelState.Remove("Nombre");
            //Validacion a nivel de backend con entity para saber si estan correctos los campos de acuerdo a las restricciones dadas
            if (ModelState.IsValid)
            {
                LogicaDB _logicaUsuario = new LogicaDB();
                var usuario = _logicaUsuario.existeEnBD(_usuario.Correo, _usuario.Clave);

                //Si es true quiere decir que se encontro el usuario, caso contrario no lo encontro
                if (usuario == null)
                {
                    return View();
                }

                //Creacion del cookies de sesion para el usuario
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim("Correo", usuario.Correo)
            };



                //Recorremos todos los roles que tiene nuestro usuario
                foreach (string rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acceso");
        }
    }
}
