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
        /// <summary>
        /// Mostra todos os eventos
        /// </summary>
        ///<response code="302">Encontrou os eventos</response>
        ///<response code="404">Não foi possivel encontrar os eventos</response>
        [Route("api/eventos")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarEventos(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                return new ObjectResult(listaEventos);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não existe um evento registrado"});
            }
        }
        /// <summary>
        /// Pega o evento por id
        /// </summary>
        ///<response code="200">Encontrou o evento</response>
        ///<response code="404">Evento não foi encontrado</response>
        [Route("api/eventos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarEventos(int id){
            var evento = _eventoRepositorio.BuscarEventoUnica(id);
            if(evento!=null){
                Response.StatusCode = 302;
                return new ObjectResult(evento);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel encontrar o evento"});
            }   
             
        }
        /// <summary>
        /// Criar evento
        /// </summary>
        ///<response code="201">Criou o evento</response>
        ///<response code="400">Fomrulario prenchido de forma incorreta</response>
        [Route("api/eventos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult CriarEvento([FromBody] CriandoEventoViewModel eventoTemp){
            if(ModelState.IsValid){
                var evento = new Evento();
                evento.CasaDeShowsId = eventoTemp.CasaDeShowsId;
                evento.GeneroDoEventoId = eventoTemp.GeneroEvento;
                evento.NomeDoEvento = eventoTemp.NomeDoEvento;
                evento.Capacidade = eventoTemp.Capacidade;
                evento.PrecoIngresso = eventoTemp.PrecoIngresso;
                evento.DataEvento = eventoTemp.DataEvento.Date;
                evento.HorarioEvento = eventoTemp.DataEvento.ToLocalTime();
                _eventoRepositorio.AdicionarEvento(evento);
                var eventoResult = _eventoRepositorio.BuscarEvento(evento.Id);
                Response.StatusCode = 201;
                return new ObjectResult(eventoResult);
            }
            else{
                var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                Response.StatusCode = 400;
                return new ObjectResult(erros);
            }
        }
        /// <summary>
        /// Edição de evento por Id
        /// </summary>
        ///<response code="200">Evento alterado com sucesso</response>
        ///<response code="400">Fomrulario prenchido de forma incorreta</response>
        ///<response code="404">O id passado no corpo da requisição é diferente do que esta na rota</response>
        [Route("api/eventos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public IActionResult EditarEvento([FromBody] EdicaoEventoViewModel eventoTemp){
            try{
                if(eventoTemp.Id > 0){
                    var evento = _eventoRepositorio.BuscarEvento(eventoTemp.Id);
                    if(ModelState.IsValid){
                        evento.CasaDeShowsId = eventoTemp.CasaDeShowsId; 
                        evento.GeneroDoEventoId = eventoTemp.GeneroEvento;
                        evento.Capacidade=eventoTemp.Capacidade;
                        evento.PrecoIngresso= eventoTemp.PrecoIngresso;
                        evento.DataEvento=eventoTemp.DataEvento;
                        evento.HorarioEvento = eventoTemp.DataEvento.ToLocalTime() ;
                        _eventoRepositorio.EditarEvento(evento);
                        Response.StatusCode = 200;
                        return new ObjectResult(evento);
                    }
                    else{
                        Response.StatusCode = 400;
                        var erros = ModelState.Values.SelectMany(v=>v.Errors).Select(v=>v.ErrorMessage+""+v.Exception).ToList();
                        return new ObjectResult(erros);
                    }
                }
                else{
                    Response.StatusCode = 404;
                    return new ObjectResult(new{msg="Não foi possivel encontrar esse evento"});
                }
            }
            catch{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não foi possivel encontrar esse evento"});
            }   
        }
        /// <summary>
        /// Deleção do evento por id
        /// </summary>
        ///<response code="200">Evento deletado com sucesso</response>
        ///<response code="404">O id passado não existe</response>
        [Route("api/eventos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Listagem de capacidade ascendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">não tem eventos para ordenar</response>
        [Route("api/eventos/capacidade/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoCapAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaAsc = listaEventos.OrderBy(x=>x.Capacidade).ToList();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar a pacidade por ordem ascendente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de capacidade descendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/capacidade/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoCapDes(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaDesc = listaEventos.OrderByDescending(x=>x.Capacidade).ToList();
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar a pacidade por ordem decrescente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de data ascendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/data/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoDataAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaAsc = listaEventos.OrderBy(x=>x.DataEvento).ToList();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar a data por ordem ascendente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de data descendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/data/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoDataDes(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaDesc = listaEventos.OrderByDescending(x=>x.DataEvento).ToList();
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar a data por ordem decrescente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de nome ascendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/nome/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoNomeAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaAsc = listaEventos.OrderBy(x=>x.NomeDoEvento).ToList();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar o nome por ordem ascendente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de nome descendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/nome/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoNomeDesc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaDesc = listaEventos.OrderByDescending(x=>x.NomeDoEvento).ToList();
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar o nome por ordem decrescente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de preco ascendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/preco/asc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoPrecoAsc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaAsc = listaEventos.OrderBy(x=>x.PrecoIngresso).ToList();
                return new ObjectResult(listaAsc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar o preço por ordem ascendente pois não existe um evento ainda"});
            }
        }
        /// <summary>
        /// Listagem de preco descendente
        /// </summary>
        ///<response code="302">Evento listado com sucesso</response>
        ///<response code="404">Não tem eventos para ordenar</response>
        [Route("api/eventos/preco/desc")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult OrdenandoPrecoDesc(){
            var listaEventos = _eventoRepositorio.ListarEventos();
            int tamanhoListaEvento = listaEventos.Count;
            if(tamanhoListaEvento>0){
                Response.StatusCode = 302;
                var listaDesc = listaEventos.OrderByDescending(x=>x.PrecoIngresso).ToList();
                return new ObjectResult(listaDesc);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel ordenar o nome por ordem decrescente pois não existe um evento ainda"});
            }
        }
    }
}