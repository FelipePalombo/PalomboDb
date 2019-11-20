using System.Collections.Generic;

namespace AplicacaoBase
{
    public class AtualizarDto
    {
        public int Tid { get; set; }
        public AlunoDto Aluno { get; set; }
        public IEnumerable<FiltroDto> Filtros { get; set; }
    }
}