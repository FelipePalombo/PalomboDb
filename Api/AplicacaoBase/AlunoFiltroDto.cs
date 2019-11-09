using System.Collections.Generic;

namespace AplicacaoBase
{
    public class AlunoFiltroDto
    {
        public AlunoDto aluno { get; set; }
        public IEnumerable<FiltroDto> filtros { get; set; }
    }
}