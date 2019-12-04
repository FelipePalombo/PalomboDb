using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.IO;

namespace Servico.Implementacao
{
    public class CheckpointServico : ICheckpointServico
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IChaveRepositorio _chaveRepositorio;
        private readonly ITransacaoServico _transacaoServico;
        private readonly IBloqueioRepositorio _bloqueioRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private JsonSerializerSettings serializerSettings;

        public CheckpointServico(IOperacoesRepositorio operacoesRepositorio, IChaveRepositorio chaveRepositorio, ITransacaoServico transacaoServico, IBloqueioRepositorio bloqueioRepositorio, IAlunoRepositorio alunoRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _chaveRepositorio = chaveRepositorio;
            _transacaoServico = transacaoServico;
            _bloqueioRepositorio = bloqueioRepositorio;
            _alunoRepositorio = alunoRepositorio;            

            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public void Check(bool start)
        {
            var checkpointed = new List<int>();
            List<TransacaoDominio> transacoes = _transacaoServico.ListarTransacoes().ToList();
            foreach(TransacaoDominio transacao in transacoes)
            {
                if(transacao.Status == EnumTiposTransacoes.Commit.getInt())
                {
                    checkpointed.Add(transacao.Tid);
                    var novosAlunos = _operacoesRepositorio.ListarTudo(transacao);
                    _alunoRepositorio.ReescreverAlunos(novosAlunos);
                    _transacaoServico.EliminarTransacao(transacao.Tid);
                }else
                {
                    if(start)
                    {
                        _transacaoServico.EliminarTransacao(transacao.Tid);
                    }
                }        
            }

            LogDominio log = new LogDominio
            {
                Tid = 000,
                Acao = "Checkpoint",
                Detalhes = new {TransacoesArmazenadas = checkpointed}
            };
            LogsServico.AdicionarLog(log);
        }

        public void SetUndoRedo()
        {
            var json1 = "";
            File.WriteAllText(@"..\Repositorio\Banco\undoredo.json", json1);
            
            var transacoes = _transacaoServico.ListarTransacoes();
            var transacoesEncerradas = transacoes.Where(t => t.Status == EnumTiposTransacoes.Commit.getInt());
            var transacoesAbertas = transacoes.Where(t => t.Status == EnumTiposTransacoes.Aberto.getInt());

            var undoRedo = new UndoRedoDominio 
            {
                Redo = transacoesEncerradas,
                Undo = transacoesAbertas
            };

            var json = JsonConvert.SerializeObject(undoRedo, serializerSettings);
            File.WriteAllText(@"..\Repositorio\Banco\undoredo.json", json);
        }
    }
}