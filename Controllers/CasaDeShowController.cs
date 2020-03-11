using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.CasaDeShowViewModels;
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
    public class CasaDeShowController:Controller
    {
        private readonly CasaDeShowRepositorio _casaDeShowRepositorio;
        public CasaDeShowController(CasaDeShowRepositorio casaDeShowRepositorio){
            _casaDeShowRepositorio = casaDeShowRepositorio;
        }
        /// <summary>
        /// Listagem de casas de show
        /// </summary>
        ///<response code="302">Casas listadas com sucesso</response>
        ///<response code="404">Não tem casa cadastradas</response>
        [Route("api/casas")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarCasas(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas>0){
                Response.StatusCode = 302;
                return new ObjectResult(listaCasas);
            }
            else{
                Response.StatusCode= 404;
                return new ObjectResult(new{msg="Não existe uma casa de show registrada ainda"});
            }
        }
        /// <summary>
        /// Listagem de casas de show por id
        /// </summary>
        ///<response code="302">Casa listada com sucesso</response>
        ///<response code="404">Não foi possivel encontrar casa pelo id</response>
        [Route("api/casas/{id}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarCasas(int id){
            var casa = _casaDeShowRepositorio.BuscarCasasDeShows(id);
            if(casa!=null){
                Response.StatusCode = 302;
                return new ObjectResult(casa);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar essa casa de show"});
            }
        }
        /// <summary>
        /// Criando casa de show
        /// </summary>
        ///<response code="201">Casa cadastrada com sucesso</response>
        ///<response code="400">Formulario prenchido de forma incorreta</response>
        [Route("api/casas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult CriarCasaDeShow([FromBody] CriarCasaDeShowViewModel casaTemp){
            if(ModelState.IsValid){
                var casa = new CasaDeShow();
                casa.NomeCasaDeShow = casaTemp.NomeCasaDeShow;
                casa.Endereco = casaTemp.Endereco;
                _casaDeShowRepositorio.AdicionarCasasDeShows(casa);
                var casaResult = _casaDeShowRepositorio.BuscarCasasDeShows(casa.Id);
                Response.StatusCode = 201;
                return new ObjectResult(casaResult);
            }
            else{
                Response.StatusCode = 400;
                var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                return new ObjectResult(erros); 
            }
        }
        /// <summary>
        /// Editando casa de show
        /// </summary>
        ///<response code="200">Casa alterada com sucesso</response>
        ///<response code="400">Formulario prenchido de forma incorreta</response>
        ///<response code="404">O id passado no corpo da requisição é diferente do que esta na rota</response>
        [Route("api/casas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult EditarCasaDeShow([FromBody] EdicaoCasaDeShowViewModel casaTemp){
            try{
                if(casaTemp.Id>0){
                    var casa = _casaDeShowRepositorio.BuscarCasasDeShows(casaTemp.Id);
                    if(ModelState.IsValid){
                        casaTemp.Id = casa.Id;
                        casa.NomeCasaDeShow = casaTemp.NomeCasaDeShow;
                        casa.Endereco = casaTemp.Endereco;
                        _casaDeShowRepositorio.EditarCasasDeShows(casa);
                        Response.StatusCode = 200;
                        return new ObjectResult(casa);
                    }
                    else{
                        Response.StatusCode = 400;
                        var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                        return new ObjectResult(erros);
                    }
                }
                else{
                    Response.StatusCode = 404;
                    return new ObjectResult(new{msg="Não foi possivel encontrar essa casa de show"});
                }
            }
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar essa casa de show"});
            }

        }
        /// <summary>
        /// Deletando casa de show
        /// </summary>
        ///<response code="200">Casa deletada</response>
        ///<response code="404">Não foi possivel encontrar a casa de show pelo id</response>
        [Route("api/casas/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public IActionResult DeletandoCasa(int id){
            var casa = _casaDeShowRepositorio.BuscarCasasDeShows(id);
            if(casa!=null){
                _casaDeShowRepositorio.ExcluirCasasDeShows(casa);
                Response.StatusCode = 200;
                return new ObjectResult(new{msg="Não foi possivel encontar a casa de show"});
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel deletar a casa de show"});
            }
        }
        /// <summary>
        /// Ordenando casa de show ascendente
        /// </summary>
        ///<response code="302">Casa listada com sucesso</response>
        ///<response code="404">Não foi possivel encontrar a casa de show</response>
        [Route("api/casas/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdeneandoCasaAsc(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas>0){
                Response.StatusCode = 302;
                var listaAsc = listaCasas.OrderBy(x=>x.NomeCasaDeShow).ToList();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não possivel ordenar casas de show por ordem ascendente"});
            }
        }
        /// <summary>
        /// Ordenando casa de show descendente
        /// </summary>
        ///<response code="302">Casa listada com sucesso</response>
        ///<response code="404">Não foi possivel encontrar a casa de show</response>
        [Route("api/casas/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdeneandoCasaDesc(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas>0){
                Response.StatusCode = 302;
                var listaDesc = listaCasas.OrderByDescending(x=>x.NomeCasaDeShow).ToList();
                return new ObjectResult(listaDesc); 
                }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar as casas de show por ordem descendente"});
            }
        }
        /// <summary>
        /// Pegando casa pelo nome
        /// </summary>
        ///<response code="302">Casa listada com sucesso</response>
        ///<response code="404">Não foi possivel encontrar a casa de show pelo nome passado na rota</response>
        [Route("api/casas/nome/{nome}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdeneandoCasaNome(string nome){
            var listaCasasNome = _casaDeShowRepositorio.BuscarCasasDeShowsNome(nome);
            if(listaCasasNome!=null){
                Response.StatusCode = 302;
                return new ObjectResult(listaCasasNome); 
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontra uma casa de show pelo nome"});
            }
        }
    }
}
