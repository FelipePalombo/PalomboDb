using System.Collections.Generic;

namespace AplicacaoBase
{
    public class InserirDto
    {
        public int Tid { get; set; }
        public IEnumerable<AlunoDto> Alunos { get; set; }
    }
}