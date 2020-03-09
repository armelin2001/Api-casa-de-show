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
        ///<response code="302">Encontrou todos as vendas</response>
        ///<response code="404">Não tem nenhuma venda registrada</response>
        [Route("api/vendas")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public IActionResult PegarVendas(){
            var listaDeVendas = _vendaRepositorio.ListarVendas();
            int tamanhoLista = listaDeVendas.Count;
            if(tamanhoLista<0){
                Response.StatusCode = 302;
                return new ObjectResult(listaDeVendas);
            }
            else{
                Response.StatusCode = 404;                
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
            var venda = _vendaRepositorio.BuscarVenda(id);
            if(venda != null){
                Response.StatusCode = 302;
                return new ObjectResult(venda);
            }
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new {msg="Não foi possivel encontrar essa venda"});
            }
        }
    }
}