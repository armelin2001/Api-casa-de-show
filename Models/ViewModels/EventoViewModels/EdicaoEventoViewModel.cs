using System;
using System.ComponentModel.DataAnnotations;
namespace Api_casa_de_show.Models.ViewModels.EventoViewModels
{
    public class EdicaoEventoViewModel
    {
        public int Id{get;set;}
        [Required(ErrorMessage="Digite uma id de casa de show")]
        [Range(1,int.MaxValue,ErrorMessage="Digite uma id de casa de show")]

        public int? CasaDeShowsId{get;set;}
        [Required(ErrorMessage="Digite uma id para genero de evento")]
        [Range(1,int.MaxValue,ErrorMessage="Digite uma id para genero de evento")]

        public int? GeneroEvento{get;set;}
        [Required(ErrorMessage="Digite um nome para o evnto")]
        [MinLength(3,ErrorMessage="Digite um nome de evento maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome de evento menor ou igual a 40 letras")]
        public string NomeDoEvento{get;set;}
        [Required(ErrorMessage="Digite uma capacidade")]
        [Range(1,int.MaxValue,ErrorMessage="Digite uma capacidade")]
        public int Capacidade{get;set;}
        [Required(ErrorMessage="Digite um preço para o ingresso")]
        [Range(1,float.MaxValue,ErrorMessage="Digite um preço para o ingresso")]
        public float PrecoIngresso{get;set;}
        [Required(ErrorMessage="Selecione uma data para o evento")]
        [Display(Name="DataEvento")]
        public DateTime DataEvento{get;set;}

    }
}