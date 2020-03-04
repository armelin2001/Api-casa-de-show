using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api_casa_de_show.Models.ViewModels.GeneroViewModels
{
    public class EdicaoGeneroViewModel
    {
        public int Id{get;set;}
        [Required(ErrorMessage="Digite um nome para genero")]
        [MinLength(3,ErrorMessage="Digite um nome de genero maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome de genero menor ou igual a 40 letras")]

        public string NomeGenero{get;set;}
    }
}