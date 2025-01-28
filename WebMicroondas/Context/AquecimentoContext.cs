using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMicroondas.Models;

namespace WebMicroondas.Context
{
    public class AquecimentoContext : DbContext
    {
        public AquecimentoContext() : base("name=ConexaoPadrao")
        {
        }

        public DbSet<AquecimentoPreDB> AquecimentosPreDefinidos { get; set; }
        
        
    }
}