using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.EventoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Api_casa_de_show.Repositorio
{
    public class EventoRepositorio
    {
        private readonly ApplicationDbContext _database;
        public EventoRepositorio(ApplicationDbContext database){
            _database = database;
        }
        public List<ListarEventoViewModel> ListarEventos(){
            return _database.Eventos.Select(x=> new ListarEventoViewModel
            {
                Id = x.Id,
                CasaDeShowId = x.CasaDeShowsId.Value,
                CasaDeShow = x.CasaDeShow.NomeCasaDeShow,
                GeneroDoEventoId = x.GeneroDoEventoId.Value,
                GeneroEvento = x.GeneroEvento.NomeGenero,
                NomeDoEvento = x.NomeDoEvento,
                Capacidade = x.Capacidade,
                DataEvetno = x.DataEvento,
                HorarioEvento = x.HorarioEvento
            }).ToList();
        }
        public Evento BuscarEvento(int id){
            var buscaEvento = _database.Eventos.FirstOrDefault(x=>x.Id == id);
            return buscaEvento;
        }
        public void AdicionarEvento(Evento evento){
            _database.Eventos.Add(evento);
            _database.SaveChanges();
        }
        public void ExcluirEvento(Evento evento){
            _database.Eventos.Remove(evento);
            _database.SaveChanges();
        }
        public void EditarEvento(Evento evento){
            _database.Eventos.Update(evento);
            _database.SaveChanges();
        }
    }
}