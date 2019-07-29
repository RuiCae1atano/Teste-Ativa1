using Investimento.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Investimento.Data
{
    public interface IMovimentacao
    { 
        IEnumerable<Movimentacao> GetMovimentacaoListagembyNome(string name);
        IEnumerable<Movimentacao> GetAllMovimentacao();
        Movimentacao Resgate(Movimentacao OperacaoResgate);
        Movimentacao Aplica(Movimentacao OperacaoAplcicao);
    }
}
