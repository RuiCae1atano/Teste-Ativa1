using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Investimento.Core;
using Investimento.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TesteHoje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {


        private readonly IMovimentacao iDataMovimentacao;

        public MovimentacoesController(IMovimentacao iDataMovimentacao)
        {
            this.iDataMovimentacao = iDataMovimentacao;

        }

        // GET api/values
        [HttpGet("getAllMovimentacoes")]
        public IEnumerable<Movimentacao> Get()
        {
            return iDataMovimentacao.GetAllMovimentacao();
        }

        // POST: api/Movimentacoes
        [Produces("application/json")]
        [HttpPut]
        [Route("resgate")]
        public void Resgate([FromBody] Movimentacao m)
        {
            iDataMovimentacao.Resgate(m);
        }

        [HttpGet("getMovimetacaoByname/{nome}")]
        public IEnumerable<Movimentacao> GetAplicacaoListagembyNome(string nome)
        {
            return iDataMovimentacao.GetMovimentacaoListagembyNome(nome);
        }

        // PUT: api/Movimentacoes/5
        [HttpPut]
        [Route("aplicacao")]
        public void Put(int id, [FromBody] Movimentacao m)
        {
            iDataMovimentacao.Aplica(m);
        }

    }
}
