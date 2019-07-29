using Investimento.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Investimento.Data
{
    public interface IFundo
    {
        Fundo Update(Fundo updateFundo);
        IEnumerable<Fundo> GetFundosListagemByNome(string name);
        IEnumerable<Fundo> GetFundosListagem();
    }
}
