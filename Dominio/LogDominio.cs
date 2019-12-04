using System;
using System.Collections.Generic;

namespace Dominio
{
    public class LogDominio
    {
        public int Tid { get; set; }
        public string Acao { get; set; }
        public object Detalhes { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now.ToLocalTime(); 
    }
}