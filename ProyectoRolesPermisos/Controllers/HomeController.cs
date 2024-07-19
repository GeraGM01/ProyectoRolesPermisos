using Microsoft.AspNetCore.Mvc;
using ProyectoRolesPermisos.Datos;
using ProyectoRolesPermisos.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoRolesPermisos.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //Para acceder a la base de datos creo un atributo privado de lectira, es para el contexto
        //Internamente lo que hago es llamar al dbcontext para poder hacer uso de los modelos que esten dentro del contexto
        private readonly ApplicationDbContext _contexto;

        //Hago la inyeccion de dependencias con mi metodo constructor
        public HomeController(ApplicationDbContext context)
        {
            _contexto = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Supervisor,Empleado")]
        public IActionResult Ventas()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult Compras()
        {
            return View();
        }

        [Authorize(Roles = "Administrador,Supervisor")]
        public IActionResult Clientes()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
