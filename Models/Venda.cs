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
        public string UserId{get;set;}
        public int QtdIngresso{get;set;}
        public float ValorCompra{get{return QtdIngresso * Evento.PrecoIngresso;}
        }
        public int EventoId{get;set;}
        [ForeignKey("EventoId")]
        public virtual Evento Evento{get;set;}

    }
}