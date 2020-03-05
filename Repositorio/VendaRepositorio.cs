using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
using Api_casa_de_show.Models.ViewModels.VendaViewModels;
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
            return _database.Vendas.Select(x=> new ListarVendaViewModel
            {
                Id = x.Id,
                Qtdingresso = x.QtdIngresso,
                ValorCompra = x.ValorCompra,
                NomeEvento = x.Evento.NomeDoEvento,
                EndercoEvento = x.Evento.CasaDeShow.Endereco
            }).ToList();
        }
        public Venda BuscarVenda(int id){
            var buscaVenda = _database.Vendas.FirstOrDefault(vend=> vend.Id == id);
            return buscaVenda;
        }
    }
}