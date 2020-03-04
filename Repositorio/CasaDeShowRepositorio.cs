using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Api_casa_de_show.Repositorio
{
    public class CasaDeShowRepositorio
    {
        private readonly ApplicationDbContext _database;
        public CasaDeShowRepositorio(ApplicationDbContext database){
            _database = database;
        } 
        public List<CasaDeShow> ListarCasasDeShows(){
            return _database.CasasDeShows.ToList();
        }       
        public CasaDeShow BuscarCasasDeShows(int id){
            var buscarCasasDeShows = _database.CasasDeShows.FirstOrDefault(x=>x.Id == id);
            return buscarCasasDeShows;
        }
        public void AdicionarCasasDeShows(CasaDeShow casaDeShow){
            _database.CasasDeShows.Add(casaDeShow);
            _database.SaveChanges();
        }
        public void ExcluirCasasDeShows(CasaDeShow casaDeShow){
            _database.CasasDeShows.Remove(casaDeShow);
            _database.SaveChanges();
        }
        public void EditarCasasDeShows(CasaDeShow casaDeShow){
            _database.CasasDeShows.Update(casaDeShow);
            _database.SaveChanges();
        }
    }
}