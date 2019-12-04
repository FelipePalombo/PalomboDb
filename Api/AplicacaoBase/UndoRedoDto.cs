using System;
using System.Collections.Generic;

namespace AplicacaoBase
{
    public class UndoRedoDto
    {
        public IEnumerable<TransacaoDto> Undo { get; set; }
        public IEnumerable<TransacaoDto> Redo { get; set; }
    }
}
