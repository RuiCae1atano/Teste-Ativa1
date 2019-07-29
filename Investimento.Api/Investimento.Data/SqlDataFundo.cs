using System;
using System.Collections.Generic;
using Investimento.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Investimento.Data
{
    public class SqlDataFundo : IFundo
    {

        private readonly InvestimentoDbContext db;

        public SqlDataFundo(InvestimentoDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Fundo> GetFundosListagem()
        {
           var query = from f in db.Fundos
                       select f;
            return query;
        }

        public IEnumerable<Fundo> GetFundosListagemByNome(string nome)
        {
            var query = from f in db.Fundos
                        where f.Nome.StartsWith(nome) || string.IsNullOrEmpty(nome)
                        select f;
            return query;
        }

        public Fundo Update(Fundo updateFundo)
        {
            var entity = db.Fundos.Attach(updateFundo);
            entity.State = EntityState.Modified;
            return updateFundo;
        }

        public void resgataMetodo(decimal vlMovimentacao, string g, decimal iim)
        {
            var fundos = from fs in db.Fundos
                         where fs.Cnpj == g
                         select fs;

            string cnpj = fundos.FirstOrDefault().Cnpj;

            if (cnpj == g)
            {
                if (iim > vlMovimentacao)
                {
                    iim -= vlMovimentacao;

                    Fundo fund = new Fundo
                    {
                        Id = fundos.FirstOrDefault().Id,
                        Cnpj = cnpj,
                        IIM = iim,
                        Nome = fundos.FirstOrDefault().Nome
                    };

                    var entity = db.Fundos.Attach(fund);
                    entity.State = EntityState.Modified;
                    
                }
            }
        }

    }
}
