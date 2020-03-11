using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.GeneroViewModels;
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
    public class GeneroController:Controller
    {
        private readonly GeneroRepositorio _generoRepositorio;
        public GeneroController(GeneroRepositorio generoRepositorio){
            _generoRepositorio=generoRepositorio;
        }
        /// <summary>
        /// Listagem de generos
        /// </summary>
        ///<response code="302">Genero listado com sucesso</response>
        ///<response code="404">Não tem generos cadastrados</response>
        [Route("api/generos")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarGeneros(){
            var listaGeneros = _generoRepositorio.ListarGeneros();
            int tamanhoListaGeneros = listaGeneros.Count;
            if(tamanhoListaGeneros>0){
                Response.StatusCode = 302;
                return new ObjectResult(listaGeneros);
            }else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não existe nenhum genero cadastrado ainda"});
            }
        }
        /// <summary>
        /// Listagem de generos
        /// </summary>
        ///<response code="302">Genero listado com sucesso</response>
        ///<response code="404">Não tem generos cadastrados</response>
        [Route("api/generos/{id}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarGeneros(int id){
            var genero = _generoRepositorio.BuscarGenero(id);
            if(genero!=null){
                Response.StatusCode = 302;
                return new ObjectResult(genero);
            }else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar o genero"});
            }
        }
        /// <summary>
        /// Criação de generos
        /// </summary>
        ///<response code="201">Genero criado com sucesso</response>
        ///<response code="400">Formmulario prenchido de forma incorreta</response>
        [Route("api/generos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult CriaGenero([FromBody]CriaGeneroViewModel generoTemp){
            if(ModelState.IsValid){
                var genero = new GeneroEvento();
                genero.NomeGenero = generoTemp.NomeGenero;
                _generoRepositorio.AdicionarGenero(genero);
                var generoResult = _generoRepositorio.BuscarGenero(genero.Id);
                Response.StatusCode = 201;
                return new ObjectResult(generoResult);
            }
            else
            {
                Response.StatusCode = 400;
                var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                return new ObjectResult(erros);
            }
        }
        /// <summary>
        /// Criação de alteração de genero
        /// </summary>
        ///<response code="201">Genero criado com sucesso</response>
        ///<response code="400">Formmulario prenchido de forma incorreta</response>
        ///<response code="404">O id passado no corpo da requisição é diferente do que esta na rota</response>
        [Route("api/generos/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult EditarGenero([FromBody]EdicaoGeneroViewModel generoTemp){
            try{
                if(generoTemp.Id >0){
                    var genero = _generoRepositorio.BuscarGenero(generoTemp.Id);
                    if(ModelState.IsValid){
                        generoTemp.Id = genero.Id;
                        genero.NomeGenero = generoTemp.NomeGenero;
                        _generoRepositorio.EditarGenero(genero);
                        Response.StatusCode = 200;
                        return new ObjectResult(genero);
                    }else{
                        Response.StatusCode = 400;
                        var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                        return new ObjectResult(erros);
                    }
                }
                else{
                    Response.StatusCode = 404;
                    return new ObjectResult(new{msg="Não foi possivel encontra esse genero musical"});

                }
            }
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontra esse genero musical"});
            }
        }
        /// <summary>
        /// Deleção de generos
        /// </summary>
        ///<response code="200">Genero deletado com sucesso</response>
        ///<response code="404">Não foi possivel encontrar o genero pelo id passado</response>
        [Route("api/generos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public IActionResult DeletandoGenero(int id){
            var genero = _generoRepositorio.BuscarGenero(id);
            if(genero!=null){
                _generoRepositorio.ExcluirGenero(genero);
                Response.StatusCode = 200;
                return new ObjectResult(new{msg="Genero deletado com sucesso"});
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi encontrado nenhum genero"});
            }
        }
        /// <summary>
        /// Listagem de genero ascendente
        /// </summary>
        ///<response code="302">Genero listado com sucesso</response>
        ///<response code="404">Não tem generos para serem listados</response>
        [Route("api/generos/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoGeneroAsc(){
            var listaGeneros = _generoRepositorio.ListarGeneros();
            int tamanhoListaGeneros = listaGeneros.Count;
            if(tamanhoListaGeneros>0){
                Response.StatusCode = 302;
                var listAsc = listaGeneros.OrderBy(x=>x.NomeGenero).ToList();
                return new ObjectResult(listaGeneros);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar um genero"});
            }
        }
        /// <summary>
        /// Listagem de genero descendente
        /// </summary>
        ///<response code="302">Genero listado com sucesso</response>
        ///<response code="404">Não tem generos para serem listados</response>
        [Route("api/generos/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoGeneroDesc(){
            var listaGeneros = _generoRepositorio.ListarGeneros();
            int tamanhoListaGeneros = listaGeneros.Count;
            if(tamanhoListaGeneros>0){
                Response.StatusCode = 302;
                var listaDesc = listaGeneros.OrderByDescending(x=>x.NomeGenero).ToList();
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel ordenar os generos em ordem descendentes"});
            }
        }
    }
}