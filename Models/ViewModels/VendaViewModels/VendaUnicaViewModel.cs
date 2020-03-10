namespace Api_casa_de_show.Models.ViewModels.VendaViewModels
{
    public class VendaUnicaViewModel
    {
        public int Id{get;set;}
        public int UserId{get;set;}
        public int QtdIngresso{get;set;}
        public float ValorCompra{get;set;}
        public int EventoId{get;set;}
        public string EventoNome{get;set;}
    }
}