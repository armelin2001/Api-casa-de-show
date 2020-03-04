using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Api_casa_de_show.Models;
namespace Api_casa_de_show.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Usuario> Usuarios{get;set;}
        public DbSet<Evento> Eventos {get;set;}
        public DbSet<CasaDeShow> CasasDeShows {get;set;}
        public DbSet<GeneroEvento> Generos{get;set;}
        public DbSet<Venda> Vendas{get;set;}
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options){}
    }
}