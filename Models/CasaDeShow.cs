using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace Api_casa_de_show.Models
{
    public class CasaDeShow
    {
        public int Id{get;set;}
        public string NomeCasaDeShow{get;set;}
        public string Endereco{get;set;}
    }
}