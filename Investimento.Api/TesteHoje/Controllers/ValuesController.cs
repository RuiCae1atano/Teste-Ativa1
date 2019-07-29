using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimento.Core;
using Investimento.Data;
using Microsoft.AspNetCore.Mvc;

namespace TesteHoje.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IFundo iDataFundo;

        public ValuesController(IFundo iDataFundo)
        {
            this.iDataFundo = iDataFundo;
        }

        // GET api/values
        [HttpGet("getAllFundos")]
        public IEnumerable<Fundo> Get()
        {
            return iDataFundo.GetFundosListagem();
        }

        [HttpGet("getFundobyName/{nome}")]
        public IEnumerable<Fundo> GetByName(string nome)
        {
            return iDataFundo.GetFundosListagemByNome(nome);
        }

    }
}
