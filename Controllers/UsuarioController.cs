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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Api_casa_de_show.Controllers
{
    public class UsuarioController:Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private IConfiguration _config;
        public UsuarioController(UsuarioRepositorio usuarioRepositorio, IConfiguration config){
            _usuarioRepositorio = usuarioRepositorio;
            _config = config;
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
        private bool ValidarUsuario(UsuarioAutenticacaoViewModel loginDetalhe){
            if(loginDetalhe.Nome == "Admin" && loginDetalhe.Senha == "Admin"){
                return true;
            }
            else{
                return false;
            }
        }   
        /// <summary>
        /// Autenticação da api
        /// </summary>
        ///<response code="200">Gerou o token de autenticação</response>
        ///<response code="404">Nome ou senha incorretos para gerar um token</response>
        [Route("api/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public IActionResult Login([FromBody]UsuarioAutenticacaoViewModel login){
            if(ModelState.IsValid){
                bool resultado = ValidarUsuario(login);
                if(resultado == true){
                    var tokenString = GerarToken();
                    Response.StatusCode = 200;
                    return new ObjectResult(new{token = tokenString});
                }   
                else{
                    Response.StatusCode = 403;
                    return new ObjectResult(new{msg="Não autorizado"});
                } 
            }
            else{
                Response.StatusCode = 400;
                var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                return new ObjectResult(erros);
            }
        }   
        public string GerarToken(){
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(240);
            var chaveSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credenciais = new SigningCredentials(chaveSeguranca,SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(issuer:issuer,audience:audience,expires:DateTime.Now.AddMinutes(240),signingCredentials:credenciais);
            var tokenHander = new  JwtSecurityTokenHandler();
            var stringToken = tokenHander.WriteToken(token);
            return stringToken;
        }
    }
}