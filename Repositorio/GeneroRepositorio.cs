using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Api_casa_de_show.Repositorio
{
    public class GeneroRepositorio
    {
        private readonly ApplicationDbContext _database;
        public GeneroRepositorio(ApplicationDbContext database){
            _database = database;
        }
        public List<GeneroEvento> ListarGeneros(){
            return _database.Generos.ToList();
        }
        public GeneroEvento BuscarGenero(int id){
            var buscarGenero = _database.Generos.FirstOrDefault(x=>x.Id ==id);
            return buscarGenero;
        }
        public void AdicionarGenero(GeneroEvento genero){
            _database.Generos.Add(genero);
            _database.SaveChanges();
        }
        public void ExcluirGenero(GeneroEvento genero){
            _database.Generos.Remove(genero);
            _database.SaveChanges();
        }
        public void EditarGenero(GeneroEvento genero){
            _database.Generos.Update(genero);
            _database.SaveChanges();
        }
    }
}