using System;
using System.Collections.Generic;
using System.Text;

namespace Investimento.Core
{
    public class Movimentacao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtMovimentacao { get; set; }
        public decimal  VlMovimentacao{ get; set; }
        public string Cpf { get; set; }        
        public Guid IdFundos;
        public TipoMovimentacao tipomovimentacao { get; set; }
        public Fundo Fundos { get; set; }

        public Movimentacao()
        {

        }

        public Movimentacao(Guid id, string nome, DateTime dtMovimentacao, decimal vlMovimentacao, 
                            Guid idFundos, string cpf, Fundo Fundos)
        {
            id =Id;
            nome = Nome;
            vlMovimentacao = VlMovimentacao;
            dtMovimentacao = DtMovimentacao;
            idFundos = Fundos.Id;
            cpf = Cpf;
            
        }
        
    }
}
