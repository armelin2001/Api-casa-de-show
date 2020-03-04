using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Api_casa_de_show.Repositorio;
namespace Api_casa_de_show.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController:ControllerBase
    {
        private readonly VendaRepositorio _vendaRepositorio;
        public VendaController(VendaRepositorio vendaRepositorio){
            _vendaRepositorio = vendaRepositorio;
        }
        [HttpGet]
        public IActionResult PegarVendas(){
            var listaDeEventos = _vendaRepositorio.ListarVendas();
            int tamanhoLista = listaDeEventos.Count;
            if(tamanhoLista>0){
                Response.StatusCode = 200;
                return new ObjectResult(new{listaDeEventos});
            }   
            else{
                Response.StatusCode = 404;
                return new ObjectResult(new{msg="Não Existe uma venda registrada ainda"});
            }
        }
    }
}