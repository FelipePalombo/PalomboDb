using System.Collections.Generic;

namespace AplicacaoBase
{
    public class TidFiltrosDto
    {
        public int Tid { get; set; }
        public IEnumerable<FiltroDto> Filtros { get; set; }
    }
}