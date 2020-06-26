using Dominios.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Contextos
{
    public class MemoRizeContext: DbContext
    {

        public DbSet<Sessoes> Sessao { get; set; }


        public MemoRizeContext()
        {

        }

        public MemoRizeContext(DbContextOptions<MemoRizeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Verifica se o contexto já não esta configurado, caso não eseja utiliza a string de conexão abaixo
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ruindows\\SQLEXPRESS;Initial Catalog=Memo_Rize;Integrated Security=True");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


        }


    }
}
