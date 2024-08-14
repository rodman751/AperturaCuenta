using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class Face_CheckController : Controller
    {
        // GET: Face_CheckController
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
