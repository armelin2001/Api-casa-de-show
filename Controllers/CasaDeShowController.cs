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

namespace Api_casa_de_show.Controllers
{
    public class CasaDeShowController:Controller
    {
        private readonly CasaDeShowRepositorio _casaDeShowRepositorio;
        public CasaDeShowController(CasaDeShowRepositorio casaDeShowRepositorio){
            _casaDeShowRepositorio = casaDeShowRepositorio;
        }
        [Route("api/casas")]
        [HttpGet]
        public IActionResult PegarCasas(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas<0){
                Response.StatusCode = 200;
                return new ObjectResult(listaCasas);
            }
            else{
                Response.StatusCode= 404;
                return new ObjectResult(new{msg="Não existe uma casa de show registrada ainda"});
            }
        }
        [Route("api/casas/{id}")]
        [HttpGet]
        public IActionResult PegarCasas(int id){
            var casa = _casaDeShowRepositorio.BuscarCasasDeShows(id);
            if(casa!=null){
                Response.StatusCode = 200;
                return new ObjectResult(casa);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar essa casa de show"});
            }
        }
        [Route("api/casas")]
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
                return new ObjectResult(casaTemp); 
            }
        }
        [Route("api/casas/{id}")]
        [HttpPut]
        public IActionResult EditarCasaDeShow([FromBody] EdicaoCasaDeShowViewModel casaTemp){
            try{
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
                    return new ObjectResult(casaTemp);
                }
            }
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar essa casa de show"});
            }

        } 
        [Route("api/casas/{id}")]
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
        [Route("api/casas/asc")]
        [HttpGet]
        public IActionResult OrdeneandoCasaAsc(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas<0){
                Response.StatusCode = 200;
                var listaAsc = listaCasas.OrderBy(x=>x.NomeCasaDeShow);
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não possivel ordenar casas de show por ordem ascendente"});
            }
        }
        [Route("api/casas/desc")]
        [HttpGet]
        public IActionResult OrdeneandoCasaDesc(){
            var listaCasas = _casaDeShowRepositorio.ListarCasasDeShows();
            int tamanhoListaCasas = listaCasas.Count;
            if(tamanhoListaCasas<0){
                Response.StatusCode = 200;
                var 
            }
            else{
                Response.StatusCode = 404;
            }
        }
    }
}
