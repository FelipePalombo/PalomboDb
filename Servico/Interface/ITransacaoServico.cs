using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Servico.Interface
{
    public interface ITransacaoServico
    {
        int NovaTransacao();
        TransacaoDominio ObterTransacaoPorTid(int tid);
        void CommitTransacao(int tid);
        void RollbackTransacao(int tid);
    }
}