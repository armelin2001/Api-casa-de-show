using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api_casa_de_show.Models.ViewModels.CasaDeShowViewModels
{
    public class EdicaoCasaDeShowViewModel
    {
        public int Id{get;set;}
        [Required(ErrorMessage="Digite um nome para casa de show")]
        [MinLength(3,ErrorMessage="Digite um nome para casa de show maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome para casa de show menor ou igual a 40 letras")]
        public string NomeCasaDeShow{get;set;}
        [Required(ErrorMessage="Digite um endereço")]
        [MinLength(3,ErrorMessage="Digite um endereço maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um endereço para casa de show menor ou igual a 40 letras")]
        public string Endereco{get;set;}
    }
}