using System;

namespace Api_casa_de_show.Models.ViewModels.EventoViewModels
{
    public class EventoUnicoViewModel
    {
        public int Id{get;set;}
        public int CasaDeShowId{get;set;}
        public string NomeCasaDeShow{get;set;}
        public int GeneroEventoId{get;set;}
        public string NomeGenero{get;set;}
        public string NomeDoEvento{get;set;}
        public int Capacidade{get;set;}
        public float PrecoIngresso{get;set;}
        public DateTime DataEvento{get;set;}
        public DateTime HorarioEvento{get;set;}
    }
}