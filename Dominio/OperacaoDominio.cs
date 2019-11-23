using System;
using System.Collections.Generic;

namespace Dominio
{
    public class OperacaoDominio
    {
        public int Tipo { get; set; }
        public IEnumerable<AlunoDominio> Alunos { get; set; }
        public IEnumerable<FiltroDominio> Filtros { get; set; }                    
    }
}
