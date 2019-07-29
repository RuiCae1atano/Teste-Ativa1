using System;

namespace Investimento.Core
{
    public class Fundo
    {
        public Guid Id { get; set; }
        public string Nome{ get; set; }
        public string Cnpj { get; set; }
        public decimal IIM { get; set; }
    }
}
