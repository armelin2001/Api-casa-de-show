using System;
namespace Api_casa_de_show.Models.ViewModels.VendaViewModels
{
    public class ListarVendaViewModel
    {
        public int Id{get;set;}
        public int Qtdingresso{get;set;}
        public float ValorCompra{get;set;}
        public int EventoId{get;set;}
        public string NomeEvento{get;set;}
        public string EndercoCasaDeShow{get;set;}
        public DateTime DataEvento{get;set;}

    }
}