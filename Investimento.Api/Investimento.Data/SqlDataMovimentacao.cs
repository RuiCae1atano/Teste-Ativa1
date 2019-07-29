using System;
using System.Collections.Generic;
using System.Text;
using Investimento.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Investimento.Data
{
    public class SqlDataMovimentacao : IMovimentacao
    {
        private readonly InvestimentoDbContext db;

        public SqlDataMovimentacao(InvestimentoDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }


        public IEnumerable<Movimentacao> GetMovimentacaoListagembyNome(string name)
        {
            var query = from a in db.Movimentacoes
                        where a.Nome.StartsWith(name) || string.IsNullOrEmpty(name)
                        select a;
            return query;
        }

        public IEnumerable<Movimentacao> GetAllMovimentacao()
        {
            var query = from f in db.Movimentacoes
                        select f;
            return query;
        }

        public Movimentacao Resgate(Movimentacao operacaoResgate)
        {

            var move = new Movimentacao
                {
                Id = operacaoResgate.Id,
                IdFundos = operacaoResgate.IdFundos,
                Nome = operacaoResgate.Nome,
                Cpf = operacaoResgate.Cpf,
                DtMovimentacao = DateTime.Now,
                tipomovimentacao = operacaoResgate.tipomovimentacao,
                VlMovimentacao = operacaoResgate.VlMovimentacao
                };

            var fundos = from f in db.Fundos
                         where f.Id == move.IdFundos
                         select f;

            decimal iim = fundos.FirstOrDefault().IIM;
            string cnpj = fundos.FirstOrDefault().Cnpj;


                if (operacaoResgate.VlMovimentacao > iim)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    db.Dispose();             
                    resgataMetodo(move.VlMovimentacao, cnpj, iim, move.IdFundos);
                    var resgate = from r in db.Movimentacoes
                                 where r.Id == move.Id
                                 select r;
                    foreach (Movimentacao m in resgate)
                    {
                        m.VlMovimentacao += move.VlMovimentacao;
                    }

            }

            var entity = db.Movimentacoes.Attach(move);
            entity.State = EntityState.Modified;

            return operacaoResgate;
        }

        private void resgataMetodo(decimal vlMovimentacao, string g, decimal iim, Guid idfundos)
        {

                if (iim > vlMovimentacao)
                {
                    iim -= vlMovimentacao;
                    var fundos = from fs in db.Fundos
                                 where fs.Cnpj == g
                                 select fs;
                    foreach(Fundo fundo in fundos)
                    {
                    fundo.IIM -= vlMovimentacao; 
                    }                             

                    Commit();
                    var entity = db.Fundos.Attach(fundos.FirstOrDefault());
                    entity.State = EntityState.Modified;                                     
                } 
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

            var entity = db.Movimentacoes.Attach(move);
            entity.State = EntityState.Modified;

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
