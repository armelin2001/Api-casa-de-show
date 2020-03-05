using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Api_casa_de_show.Repositorio;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Api_casa_de_show.Controllers
{
    public class VendaController:Controller
    {
        private readonly VendaRepositorio _vendaRepositorio;
        public VendaController(VendaRepositorio vendaRepositorio){
            _vendaRepositorio = vendaRepositorio;
        }
        /// <summary>
        /// Lista todas as vendas realizadas
        /// </summary>
        [Route("api/vendas")]
        [HttpGet]
        public IActionResult PegarVendas(){
            var listaDeVendas = _vendaRepositorio.ListarVendas();
            int tamanhoLista = listaDeVendas.Count;
            if(tamanhoLista>0){
                StatusCode(StatusCodes.Status200OK);
                return new ObjectResult(listaDeVendas);
            }
            else{
                StatusCode(StatusCodes.Status404NotFound);                
                return new ObjectResult(new{msg="Não Existe uma venda registrada"});
            }
        }
        /// <summary>
        /// Lista uma venda
        /// </summary>
        /// <param name="id"></param>
        [Route("api/vendas/{id}")]
        [HttpGet]
        public IActionResult PegarVendas(int id){
            try{
                var venda = _vendaRepositorio.BuscarVenda(id);
                StatusCode(StatusCodes.Status200OK);
                return new ObjectResult(venda);
            }catch{
                StatusCode(StatusCodes.Status404NotFound);
                return new ObjectResult(new {msg="Não foi possivel encontrar uma venda"});
            }
        }
    }
}