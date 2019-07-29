using Investimento.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Investimento.Data
{
    public class InvestimentoDbContext : DbContext
    {


        public InvestimentoDbContext(DbContextOptions<InvestimentoDbContext> options) : base(options)
        {

        }

        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Fundo> Fundos { get; set; }

    }
}
