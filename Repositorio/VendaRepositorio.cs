using Api_casa_de_show.Data;
using Api_casa_de_show.Models;
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
        public List<Venda> ListarVendas(){
            return _database.Vendas.ToList();
        }
        public Venda BuscarVenda(int id){
            var buscaVenda = _database.Vendas.FirstOrDefault(vend=> vend.Id == id);
            return buscaVenda;
        }
    }
}