using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;
using System.Linq;

namespace Servico.Implementacao
{
    public class TransacaoServico : ITransacaoServico
    {
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        private readonly IBloqueioRepositorio _bloqueioRepositorio;
        private readonly int tipoRollback = EnumTiposTransacoes.Rollback.getInt();
        private readonly int tipoCommit = EnumTiposTransacoes.Commit.getInt();
        private readonly int tipoAberto = EnumTiposTransacoes.Aberto.getInt();  
        private string problemaTransacao = "";
        public TransacaoServico(ITransacaoRepositorio transacaoRepositorio, IBloqueioRepositorio bloqueioRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;
            _bloqueioRepositorio = bloqueioRepositorio;
        }

        public int NovaTransacao()
        {
            return _transacaoRepositorio.NovaTransacao();
        }

        public TransacaoDominio ObterTransacaoPorTid(int tid)
        {
            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);  
            return transacao;                        
        }

        public void CommitTransacao(int tid)
        {
            var transacao = this.ObterTransacaoPorTid(tid);
            var transacaoOk = this.VerificarTransacao(transacao);

            if(transacaoOk)
            {
                _bloqueioRepositorio.EncerrarBloqueio(tid);
                _transacaoRepositorio.CommitTransacao(tid);
            }
            else
            {
                throw new System.Exception(problemaTransacao);
            }            
        }

        public void RollbackTransacao(int tid)
        {
            var transacao = this.ObterTransacaoPorTid(tid);
            var transacaoOk = this.VerificarTransacao(transacao);

            if(transacaoOk)
            {
                _bloqueioRepositorio.EncerrarBloqueio(tid);
                _transacaoRepositorio.RollbackTransacao(tid);
            }
            else
            {
                throw new System.Exception(problemaTransacao);
            } 
        }

        public void EliminarTransacao(int tid) => _transacaoRepositorio.EliminarTransacao(tid);

        public IEnumerable<TransacaoDominio> ListarTransacoes() => _transacaoRepositorio.ListarTransacoes();

        public IEnumerable<TransacaoDominio> ListarTransacoesEncerradas()
        {
            var transacoes = _transacaoRepositorio.ListarTransacoes();
            return transacoes.Where(t => t.Status == tipoCommit);
        }

        private bool VerificarTransacao(TransacaoDominio transacao)
        {
            if(transacao == null)
            {
                problemaTransacao = "Transação inexistente";
                return false;
            }
            
            if (transacao.Status != tipoAberto)
            {
                problemaTransacao = "Transação encerrada";
                return false;
            }

            return true;
        }
    }
}
