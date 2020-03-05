using System;
using System.Text.Json.Serialization;
using Api_casa_de_show.Models.ViewModels.UsuarioViewModels;

namespace Api_casa_de_show.Models
{
    public class Usuario
    {
        public int Id{get;set;}
        public string Nome{get;set;}
        public string UltimoNome{get;set;}
        public string Email{get;set;}
        [JsonIgnore]
        public string Senha{get;set;}
    }
}