
using Microsoft.Extensions.Configuration;
using Repositorio.Interfaces;
using Repositorio.Repositorios;


namespace Repositorio
{
    public class RepositorioManager:IRepositorioManager
    {
        private readonly DbContext _context;
        private readonly IConfiguration _configuration;
        private  Lazy<IUsuarioRepository> _usuarioRepository;

        private Lazy<IRegistrosRepository> _registrosRepository;


        public RepositorioManager(IConfiguration configuration, DbContext context )
        {
            _configuration = configuration;
            _context = context;
            
            _usuarioRepository = new Lazy<IUsuarioRepository>(()=> new UsuarioRepository(_configuration,_context));

            _registrosRepository = new Lazy<IRegistrosRepository>(() => new RegistrosRepository(_context));
        }

        public IUsuarioRepository UsuarioRepository => _usuarioRepository.Value;

        public IRegistrosRepository registrosRepository => _registrosRepository.Value;

      


    }
}

