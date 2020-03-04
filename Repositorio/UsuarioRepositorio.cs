using Api_casa_de_show.Data;
using Api_casa_de_show.Models.ViewModels.UsuarioViewModels;
using Api_casa_de_show.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api_casa_de_show.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly ApplicationDbContext _database;
        public UsuarioRepositorio(ApplicationDbContext database){
            _database = database;
        }        
        public List<ListarUsuarioViewModel> ListarUsuarios(){
            return _database.Usuarios.Select(x=> new ListarUsuarioViewModel{
              Id = x.Id,
              Nome = x.Nome,
              UltimoNome = x.UltimoNome,
              Email = x.Email
            }).ToList();
        }
        public ListarUsuarioViewModel BuscarUsuario(int id){
            var buscaUsuario = _database.Usuarios.FirstOrDefault(x=>x.Id ==id);
            /*List<Usuario> usuario = new List<Usuario>();
            usuario.Add(buscaUsuario);*/
            ListarUsuarioViewModel usuarioViewModel = new ListarUsuarioViewModel();
            usuarioViewModel.Id = buscaUsuario.Id;
            usuarioViewModel.Nome = buscaUsuario.Nome;
            usuarioViewModel.UltimoNome = buscaUsuario.UltimoNome;
            usuarioViewModel.Email = buscaUsuario.Email;
            return usuarioViewModel;
        }
    }
}