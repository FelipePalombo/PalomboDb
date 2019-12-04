using System;
using System.Collections.Generic;

namespace Dominio
{
    public class UndoRedoDominio
    {
        public IEnumerable<TransacaoDominio> Undo { get; set; }
        public IEnumerable<TransacaoDominio> Redo { get; set; }
    }
}
