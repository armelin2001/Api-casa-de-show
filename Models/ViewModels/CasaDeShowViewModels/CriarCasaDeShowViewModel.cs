using System.ComponentModel.DataAnnotations;

namespace Api_casa_de_show.Models.ViewModels.CasaDeShowViewModels
{
    public class CriarCasaDeShowViewModel
    {
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