using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using System.Collections.Generic;
using System.Linq;
namespace Api_casa_de_show.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly ApplicationDbContext _database;
        public UsuarioRepositorio(ApplicationDbContext database){
            _database = database;
        }        
        public List<Usuario> ListarUsuarios(){
            return _database.Usuarios.ToList();
        }
        public Usuario BuscarUsuario(int id){
            var buscaUsuario = _database.Usuarios.FirstOrDefault(x=>x.Id ==id);
            return buscaUsuario;
        }
    }
    
}