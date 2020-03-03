using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api_casa_de_show.Models
{
    public class Evento
    {
        public int Id{get;set;}
        public int? CasaDeShowsId{get;set;}
        [ForeignKey("CasaDeShowsId")]
        public virtual CasaDeShow CasaDeShow { get; set; }
        public int? GeneroDoEventoId{get;set;}
        [ForeignKey("GeneroDoEventoId")]
        public virtual GeneroEvento GeneroEvento { get; set;}
        public string NomeDoEvento{get;set;}   
        public int Capacidade{get;set;}
        public float PrecoIngresso{get;set;}
        public DateTime DataEvento{get;set;}
        public DateTime HorarioEvento{get;set;}
    }
}