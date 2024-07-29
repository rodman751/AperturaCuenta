using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.CuentaApertura.Controllers
{
    public class UsuarioViewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


    }
}
