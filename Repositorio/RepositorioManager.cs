
using Repositorio.Interfaces;
using Repositorio.Repositorios;


namespace Repositorio
{
    public class RepositorioManager:IRepositorioManager
    {
        private readonly DbContext _context;
        
        private  Lazy<IUsuarioRepository> _usuarioRepository;

        private Lazy<IRegistrosRepository> _registrosRepository;


        public RepositorioManager(DbContext context )
        {
            _context = context;
            
            _usuarioRepository = new Lazy<IUsuarioRepository>(()=> new UsuarioRepository(_context));

            _registrosRepository = new Lazy<IRegistrosRepository>(() => new RegistrosRepository(_context));
        }

        public IUsuarioRepository UsuarioRepository => _usuarioRepository.Value;

        public IRegistrosRepository registrosRepository => _registrosRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }


    }
}

