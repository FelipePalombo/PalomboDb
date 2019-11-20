using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface ITransacaoRepositorio
    {
        int NovaTransacao();
        void CommitTransacao(int tid);
        void RollbackTransacao(int tid);
        IEnumerable<TransacaoDominio> ListarTransacoes();
        TransacaoDominio ObterTransacaoPorTid(int tid);
    }
}