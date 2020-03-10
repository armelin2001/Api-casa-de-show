using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.EventoViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Api_casa_de_show.Repositorio
{
    public class EventoRepositorio
    {
        private readonly ApplicationDbContext _database;
        public EventoRepositorio(ApplicationDbContext database){
            _database = database;
        }
        public List<ListarEventoViewModel> ListarEventos(){
            return _database.Eventos.Include(x=>x.GeneroEvento).Select(x=> new ListarEventoViewModel
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
        public EventoUnicoViewModel BuscarEventoUnica(int id){
            var buscaUnica = _database.Eventos.Include(x=>x.CasaDeShow).Include(x=>x.GeneroEvento).Select(x=>
            new EventoUnicoViewModel{
                Id = x.Id,
                CasaDeShowId = x.CasaDeShow.Id,
                NomeCasaDeShow = x.CasaDeShow.NomeCasaDeShow,
                GeneroEventoId = x.GeneroEvento.Id,
                NomeGenero = x.GeneroEvento.NomeGenero,
                NomeDoEvento = x.NomeDoEvento,
                Capacidade = x.Capacidade,
                PrecoIngresso = x.PrecoIngresso,
                DataEvento = x.DataEvento,
                HorarioEvento = x.HorarioEvento

            }).FirstOrDefault(x=>x.Id ==id);
            return buscaUnica;
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