using Microsoft.EntityFrameworkCore;
using System;

namespace Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options){

    }
    
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Publicacao> Publicacoes { get; set; }

}
