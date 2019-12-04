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
        IEnumerable<TransacaoDominio> ListarTransacoes();
        IEnumerable<TransacaoDominio> ListarTransacoesEncerradas();
        void EliminarTransacao(int tid);
        UndoRedoDominio ListarUndoRedo();
    }
}