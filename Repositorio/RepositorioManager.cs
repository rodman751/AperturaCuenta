
using Repositorio.Interfaces;
using Repositorio.Repositorios;


namespace Repositorio
{
    public class RepositorioManager:IRepositorioManager
    {
        private readonly DbContext _context;
        
        private  Lazy<IUsuarioRepository> _usuarioRepository;
       
        public RepositorioManager(DbContext context)
        {
            _context = context;
            
            _usuarioRepository = new Lazy<IUsuarioRepository>(()=> new UsuarioRepository(_context));
        }

        public IUsuarioRepository UsuarioRepository => _usuarioRepository.Value;

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
