using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.VendaViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Api_casa_de_show.Repositorio
{
    public class VendaRepositorio
    {
        private readonly ApplicationDbContext _database;
        public VendaRepositorio(ApplicationDbContext database){
            _database = database;
        }
        public List<ListarVendaViewModel> ListarVendas(){
            return _database.Vendas.Include(x=>x.Evento).Select(x=> new ListarVendaViewModel
            {
                Id = x.Id,
                Qtdingresso = x.QtdIngresso,
                ValorCompra = x.ValorCompra,
                EventoId = x.EventoId,
                NomeEvento = x.Evento.NomeDoEvento,
                EndercoCasaDeShow = x.Evento.CasaDeShow.Endereco,
                DataEvento = x.Evento.DataEvento
            }).ToList();
        }
        // public VendaUnicaViewModel vend(int id){
        //     return _database.Vendas.Include(x=>x.Evento).Select(x=> new VendaUnicaViewModel{
        //         Id = ForeignKeyExtensions
        //     })
        // }

        public VendaUnicaViewModel BuscarVenda(int id){
            var busca = _database.Vendas.Include(x=>x.Evento).Select(x=> new VendaUnicaViewModel {
                Id = x.Id,
                UserId = x.Usuario.Id,
                QtdIngresso = x.QtdIngresso,
                ValorCompra = x.ValorCompra,
                EventoId = x.EventoId,
                EventoNome = x.Evento.NomeDoEvento
            }).FirstOrDefault(x=>x.Id == id);
            return busca; 
        }
    }
}