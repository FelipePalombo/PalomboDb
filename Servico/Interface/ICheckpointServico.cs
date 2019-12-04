using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Servico.Interface
{
    public interface ICheckpointServico
    {        
        void Check(bool start);
        void SetUndoRedo();
    }
}