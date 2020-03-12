using System;
using System.ComponentModel.DataAnnotations;
namespace Api_casa_de_show.Models.ViewModels.UsuarioViewModels
{
    public class UsuarioAutenticacaoViewModel
    {
        [Required(ErrorMessage="Digite um nome")]
        [MinLength(3,ErrorMessage="Digite um nome maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite um nome menor ou igual a 40 letras")]
        public string Nome{get;set;}
        [Required(ErrorMessage="Digite uma senha")]
        [MinLength(3,ErrorMessage="Digite uma senha maior ou igual a 3 letras")]
        [MaxLength(40,ErrorMessage="Digite uma senha menor ou igual a 40 letras")]
        public string Senha{get;set;}
    }
}