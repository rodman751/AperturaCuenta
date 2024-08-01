﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class Face_scanController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public Face_scanController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }
        public ActionResult Index()
        {
             var datosStep1 = _serviceManager.CookieService.ObtenerDatosCookie<Models.FaceScan>("FaceScanCookie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new Models.FaceScan();
            return View(model);
        }

    }
}
