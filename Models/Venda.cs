using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api_casa_de_show.Models
{
    public class Venda
    {
        public int Id{get;set;}
        public int UserId{get;set;}
        [ForeignKey("UserId")]
        public virtual Usuario Usuario{get;set;}
        public int QtdIngresso{get;set;}
        public float ValorCompra{get;set;}
        public int EventoId{get;set;}
        [ForeignKey("EventoId")]
        public virtual Evento Evento{get;set;}

    }
}