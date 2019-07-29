using System;
using System.Collections.Generic;
using System.Text;
using Investimento.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Investimento.Data
{
    public class AplicaMovimentacao
    {

        private readonly InvestimentoDbContext db;

        public AplicaMovimentacao()
        {

        }

        public AplicaMovimentacao(InvestimentoDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }


        public Movimentacao Aplica(Movimentacao operacaoAplica)
        {

            var move = new Movimentacao
            {
                Id = operacaoAplica.Id,
                IdFundos = operacaoAplica.IdFundos,
                Nome = operacaoAplica.Nome,
                Cpf = operacaoAplica.Cpf,
                DtMovimentacao = DateTime.Now,
                tipomovimentacao = operacaoAplica.tipomovimentacao,
                VlMovimentacao = operacaoAplica.VlMovimentacao
            };

            var fundos = from f in db.Fundos
                         where f.Id == operacaoAplica.IdFundos
                         select f;

            decimal iim = fundos.FirstOrDefault().IIM;
            string cnpj = fundos.FirstOrDefault().Cnpj;


            if (operacaoAplica.VlMovimentacao > iim)
            {
                throw new NotImplementedException();
            }
            else
            {
                db.Dispose();
                aplicaMetodo(move.VlMovimentacao, cnpj, iim, move.IdFundos);
            }

            db.Add(move);
            Commit();
            
            return operacaoAplica;
        }

        private void aplicaMetodo(decimal vlMovimentacao, string g, decimal iim, Guid idfundos)
        {

            if (iim > vlMovimentacao)
            {
                iim += vlMovimentacao;
                var fundos = from fs in db.Fundos
                             where fs.Cnpj == g
                             select fs;
                foreach (Fundo fundo in fundos)
                {
                    fundo.IIM -= vlMovimentacao;
                }

                Commit();
                var entity = db.Fundos.Attach(fundos.FirstOrDefault());
                entity.State = EntityState.Modified;
            }
        }
    }
}
