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
using Microsoft.AspNetCore.Http;

namespace Api_casa_de_show.Controllers
{
    public class UsuarioController:Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(UsuarioRepositorio usuarioRepositorio){
            _usuarioRepositorio = usuarioRepositorio;
        }
        /// <summary>
        /// Lista todos os usuarios
        /// </summary>
        ///<response code="302">Encontrou todos os usuarios</response>
        ///<response code="404">Não tem nenhum usuario</response>
        [Route("api/users")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarUsuarios(){
            var listaUsuarios = _usuarioRepositorio.ListarUsuarios();
            int tamanhoListaUsuarios = listaUsuarios.Count;
            if(tamanhoListaUsuarios>0){
                Response.StatusCode = 200;
                return new ObjectResult(listaUsuarios);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }
        /// <summary>
        /// Mostra usuario pelo id
        /// </summary>
        ///<response code="200">Encontrou o usuario</response>
        ///<response code="404">Não foi possivel encontrar o usuario</response>
        [Route("api/users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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