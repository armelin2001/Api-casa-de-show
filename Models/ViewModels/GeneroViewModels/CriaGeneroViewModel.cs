using System;
using System.ComponentModel.DataAnnotations;
namespace Api_casa_de_show.Models.ViewModels.GeneroViewModels
{
    public class CriaGeneroViewModel
    {
        [Required(ErrorMessage="Digite um nome para um genero")]
        [MinLength(3,ErrorMessage="Digite um nome para um genero maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome para genero menor ou igual a 40 letras")]
        public string NomeGenero{get;set;}
    }
}