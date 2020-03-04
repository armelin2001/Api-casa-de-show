using System;
using System.ComponentModel.DataAnnotations;
namespace Api_casa_de_show.Models.ViewModels.EventoViewModels
{
    public class CriandoEventoViewModel
    {
        public int? CasaDeShowsId{get;set;}
        public int? GeneroEvento{get;set;}
        [Required(ErrorMessage="Digite um nome para o evnto")]
        [MinLength(3,ErrorMessage="Digite um nome de evento maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome de evento menor ou igual a 40 letras")]
        public string NomeDoEvento{get;set;}
        [Required(ErrorMessage="Digite uma capacidade")]
        [Range(1,int.MaxValue)]
        public int Capacidade{get;set;}
        [Required(ErrorMessage="Digite um preço para o ingresso")]
        [Range(1,float.MaxValue)]
        public float PrecoIngresso{get;set;}
        [Required(ErrorMessage="Selecione uma data para o evento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name="DataEvento")]
        public DateTime DataEvento{get;set;}
        [Required(ErrorMessage="Digite um horaio para o evento")]
        [DisplayFormat(DataFormatString="{HH:mm}")]
        [DataType(DataType.Time)]
        [Display(Name="HorariosEvento")]
        public DateTime HorarioEvento{get;set;}
    }
}