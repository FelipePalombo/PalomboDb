using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;
using System.Linq;
using System;

namespace Servico.Implementacao
{
    public class CheckpointServico : ICheckpointServico
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IChaveRepositorio _chaveRepositorio;
        private readonly ITransacaoServico _transacaoServico;
        private readonly IBloqueioRepositorio _bloqueioRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;

        public CheckpointServico(IOperacoesRepositorio operacoesRepositorio, IChaveRepositorio chaveRepositorio, ITransacaoServico transacaoServico, IBloqueioRepositorio bloqueioRepositorio, IAlunoRepositorio alunoRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _chaveRepositorio = chaveRepositorio;
            _transacaoServico = transacaoServico;
            _bloqueioRepositorio = bloqueioRepositorio;
            _alunoRepositorio = alunoRepositorio;
        }

        public void Check()
        {
            List<TransacaoDominio> transacoes = _transacaoServico.ListarTransacoesEncerradas().ToList();
            foreach(TransacaoDominio transacao in transacoes)
            {
                var novosAlunos = _operacoesRepositorio.ListarTudo(transacao);
                _alunoRepositorio.ReescreverAlunos(novosAlunos);
                _transacaoServico.EliminarTransacao(transacao.Tid);
            }
        }
    }
}