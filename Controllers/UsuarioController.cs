using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.UsuarioViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Api_casa_de_show.Repositorio;
namespace Api_casa_de_show.Controllers
{
    public class UsuarioController:Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(UsuarioRepositorio usuarioRepositorio){
            _usuarioRepositorio = usuarioRepositorio;
        }
        [Route("api/users")]
        [HttpGet]
        public IActionResult PegarUsuarios(){
            var listaUsuarios = _usuarioRepositorio.ListarUsuarios();
            int tamanhoListaUsuarios = listaUsuarios.Count;
            if(tamanhoListaUsuarios<0){
                Response.StatusCode = 200;
                return new ObjectResult(listaUsuarios);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não existe nenhum usuario cadastrado ainda"});
            }
        }
        [Route("api/users/{id}")]
        [HttpGet]
        public IActionResult PegarUsuarios(int id){
            var usuario = _usuarioRepositorio.BuscarUsuario(id);
            if(usuario!=null){
                Response.StatusCode = 200;
                return new ObjectResult(usuario);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar o usuario"});
            }
        } 
    }
}