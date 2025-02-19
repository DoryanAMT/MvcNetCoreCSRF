using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreCSRF.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Productos()
        {
            //  SI EL USUARIO NO SE HA VALIDAD TODAVIA
            //  LO REDIRIGIMOS A DENIED
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Productos
            (string direccion, string[] producto)
        {
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            else
            {
                //  MEDIANTE TEMPDATA SE ALMACENA LA INFROMACION
                //  PARA SER ENVIADA A OTROS CONTROLLERS O
                //  METODOS IACtionResult EN LAS DIRECCIONES
                TempData["PRODUCTOS"] = producto;
                TempData["DIRECCION"] = direccion;
                return RedirectToAction("PedidoFinal");
            }
        }
        public IActionResult PedidoFinal()
        {
            //  AQUI NECESITO LOS PRODUCTOS Y LA DIRECCION
            //  DEL METODO POS DE PRODUCTOS
            string[] productos = TempData["PRODUCTOS"] as string[];
            ViewData["DIRECCION"] = TempData["DIRECCION"];
            return View(productos);
        }
    }
}
