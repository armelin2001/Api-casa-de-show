using System;
using System.ComponentModel.DataAnnotations;
namespace Api_casa_de_show.Models.ViewModels.EventoViewModels
{
    public class CriandoEventoViewModel
    {
        [Required(ErrorMessage="Escolha uma casa de show valida")]
        [Range(1,int.MaxValue,ErrorMessage="Escolha uma casa de show valida")]
        public int? CasaDeShowsId{get;set;}
        [Required(ErrorMessage="Escolha um genero valido")]
        [Range(1,int.MaxValue,ErrorMessage="Escolha um genero valido")]
        public int? GeneroEvento{get;set;}
        [Required(ErrorMessage="Digite um nome para o evento")]
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
        // [Required(ErrorMessage="Digite um horaio para o evento")]
        // [DataType(DataType.Time)]
        // [Display(Name="HorarioEvento")]
        //public DateTime HorarioEvento{get;set;}
    }
}