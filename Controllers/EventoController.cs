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
            if(tamanhoListaEvento>0){
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
            try{
                var evento = _eventoRepositorio.BuscarEvento(id);
                Response.StatusCode = 200;
                return new ObjectResult(evento);
            }catch{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel encontrar o evento"});
            }
        }
        [Route("api/eventos")]
        [HttpPost]
        public IActionResult CriarEvento([FromBody] CriandoEventoViewModel eventoTemp){
            if(ModelState.IsValid){
                var evento = new Evento();
                evento.NomeDoEvento = eventoTemp.NomeDoEvento;
                Response.StatusCode = 201;
                _eventoRepositorio.AdicionarEvento(eventoTemp);
            }
            else{
                Response.StatusCode = 404;
                return eventoTemp;
            }
        }
    }
}