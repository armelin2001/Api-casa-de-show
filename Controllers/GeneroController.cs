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

namespace Api_casa_de_show.Controllers
{
    public class GeneroController:Controller
    {
        private readonly GeneroRepositorio _generoRepositorio;
        public GeneroController(GeneroRepositorio generoRepositorio){
            _generoRepositorio=generoRepositorio;
        }
        [Route("api/generos")]
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
        [Route("api/generos/{id}")]
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
        [Route("api/generos")]
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
        [Route("api/generos/{id}")]
        [HttpPut]
        public IActionResult EditarGenero([FromBody]EdicaoGeneroViewModel generoTemp){
            try{
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
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontra esse genero musical"});
            }
        }
        [Route("api/generos/{id}")]
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
        [Route("api/generos/asc")]
        [HttpGet]
        public IActionResult OrdenandoGeneroAsc(){
            var listaGeneros = _generoRepositorio.ListarGeneros();
            int tamanhoListaGeneros = listaGeneros.Count;
            if(tamanhoListaGeneros>0){
                Response.StatusCode = 302;
                var listAsc = listaGeneros.OrderBy(x=>x.NomeGenero);
                return new ObjectResult(listaGeneros);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar um genero"});
            }
        }
        [Route("api/generos/desc")]
        [HttpGet]
        public IActionResult OrdenandoGeneroDesc(){
            var listaGeneros = _generoRepositorio.ListarGeneros();
            int tamanhoListaGeneros = listaGeneros.Count;
            if(tamanhoListaGeneros>0){
                Response.StatusCode = 302;
                var listaDesc = listaGeneros.OrderByDescending(x=>x.NomeGenero);
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel ordenar os generos em ordem descendentes"});
            }
        }
    }
}