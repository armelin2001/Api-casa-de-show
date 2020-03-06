using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Api_casa_de_show.Repositorio;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Api_casa_de_show.Models.ViewModels.EventoViewModels;
using Api_casa_de_show.Models;
namespace Api_casa_de_show.Controllers
{
    public class EventoController:Controller
    {
        private readonly EventoRepositorio _eventoRepositorio;
        public EventoController(EventoRepositorio eventoRepositorio){
            _eventoRepositorio = eventoRepositorio;
        }
        [Route("api/eventos")]
        [HttpGet]
        public IActionResult PegarEventos(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                //StatusCode(StatusCodes.Status200OK);
                Response.StatusCode = 200;
                return new ObjectResult(listaEventos);
            }
            else{
                //StatusCode(StatusCodes.Status404NotFound);
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não existe um evento registrado"});
            }
        }
        [Route("api/eventos/{id}")]
        [HttpGet]
        public IActionResult PegarEventos(int id){
            var evento = _eventoRepositorio.BuscarEvento(id);
            if(evento!=null){
                Response.StatusCode = 200;
                return new ObjectResult(evento);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel encontrar o evento"});
            }   
             
        }
        [Route("api/eventos")]
        [HttpPost]
        public IActionResult CriarEvento([FromBody] CriandoEventoViewModel eventoTemp){
            if(ModelState.IsValid){
                var evento = new Evento();
                evento.CasaDeShowsId = eventoTemp.CasaDeShowsId;
                evento.GeneroDoEventoId = eventoTemp.GeneroEvento;
                evento.NomeDoEvento = eventoTemp.NomeDoEvento;
                evento.Capacidade = eventoTemp.Capacidade;
                evento.PrecoIngresso = eventoTemp.PrecoIngresso;
                evento.DataEvento = eventoTemp.DataEvento;
                evento.HorarioEvento = eventoTemp.HorarioEvento;
                _eventoRepositorio.AdicionarEvento(evento);
                var eventoResult = _eventoRepositorio.BuscarEvento(evento.Id);
                Response.StatusCode = 201;
                return new ObjectResult(eventoResult);
            }
            else{
                Response.StatusCode = 400;
                return new ObjectResult(eventoTemp);
            }
        }
        [Route("api/eventos/{id}")]
        [HttpPut]
        public IActionResult EditarEvento([FromBody] EdicaoEventoViewModel eventoTemp){
            try{
                var evento = _eventoRepositorio.BuscarEvento(eventoTemp.Id);
                if(ModelState.IsValid){
                    eventoTemp.Id= evento.Id;
                    evento.CasaDeShowsId = eventoTemp.CasaDeShowsId;
                    evento.GeneroDoEventoId = eventoTemp.GeneroEvento;
                    evento.NomeDoEvento = eventoTemp.NomeDoEvento;
                    evento.Capacidade = eventoTemp.Capacidade;
                    evento.PrecoIngresso = eventoTemp.PrecoIngresso;
                    evento.DataEvento = eventoTemp.DataEvento;
                    evento.HorarioEvento = eventoTemp.HorarioEvento;
                    _eventoRepositorio.EditarEvento(evento);
                    Response.StatusCode = 200;
                    return new  ObjectResult(evento);
                }
                else{
                    Response.StatusCode = 400;
                    return new ObjectResult(eventoTemp);
                }
            }
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar esse evento"});
            }   
        }
        [Route("api/eventos/{id}")]
        [HttpDelete]
        public IActionResult DeletandoEvento(int id){
            var evento = _eventoRepositorio.BuscarEvento(id);
            if(evento!=null){
                _eventoRepositorio.ExcluirEvento(evento);
                Response.StatusCode = 200;
                return new ObjectResult(new {msg="Evento foi detado com sucesso"});
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel encontrar o evento"});
            }
        }
        [Route("api/eventos/capacidade/asc")]
        [HttpGet]
        public IActionResult OrdenandoCapAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderBy(x=>x.Capacidade).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar a pacidade por ordem ascendente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/capacidade/desc")]
        [HttpGet]
        public IActionResult OrdenandoCapDes(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderByDescending(x=>x.Capacidade).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar a pacidade por ordem decrescente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/data/asc")]
        [HttpGet]
        public IActionResult OrdenandoDataAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderBy(x=>x.DataEvetno).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar a data por ordem ascendente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/data/desc")]
        [HttpGet]
        public IActionResult OrdenandoDataDes(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderByDescending(x=>x.DataEvetno).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar a data por ordem decrescente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/nome/asc")]
        [HttpGet]
        public IActionResult OrdenandoNomeAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderBy(x=>x.NomeDoEvento).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar o nome por ordem ascendente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/nome/desc")]
        [HttpGet]
        public IActionResult OrdenandoNomeDesc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderByDescending(x=>x.NomeDoEvento).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar o nome por ordem decrescente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/preco/asc")]
        [HttpGet]
        public IActionResult OrdenandoPrecoAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderBy(x=>x.PrecoIngresso).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar o preço por ordem ascendente pois não existe um evento ainda"});
            }
        }
        [Route("api/eventos/preco/desc")]
        [HttpGet]
        public IActionResult OrdenandoPrecoDesc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento<0){
                Response.StatusCode = 200;
                var listaAsc = listaEventos.OrderByDescending(x=>x.PrecoIngresso).FirstOrDefault();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foipossivel ordenar o nome por ordem decrescente pois não existe um evento ainda"});
            }
        }
    }
}