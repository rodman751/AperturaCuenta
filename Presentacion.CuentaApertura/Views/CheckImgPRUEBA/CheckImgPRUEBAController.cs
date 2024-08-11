using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Views.CheckImgPRUEBA
{
    public class CheckImgPRUEBAController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public CheckImgPRUEBAController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
            
        }
        public async Task<IActionResult> Index()
        {
            // Espera el resultado de ObtenerTodasLasImagenes
            var imagenes = await _repositoryManager.registrosRepository.ObtenerTodasLasImagenes();

            // Aplica Select después de obtener la lista
            var imagenesViewModel = imagenes.Select(i => new FaceScan
            {
                ImageUrl = i.Foto
            }).ToList(); // Conviértelo a una lista

            // Crea el ViewModel
            var viewModel = new Getimg
            {
                Imagenes = imagenesViewModel
            };

            // Devuelve la vista con el ViewModel
            return View(viewModel);
        }




    }
}
