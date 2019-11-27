using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;
using System.Linq;
using System;

namespace Servico.Implementacao
{
    public class OperacoesServico : IOperacoesServico
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IChaveRepositorio _chaveRepositorio;
        private readonly ITransacaoServico _transacaoServico;
        private readonly IBloqueioRepositorio _bloqueioRepositorio;
        private readonly int tipoChaveAluno = EnumTiposChaves.Aluno.getInt();
        private readonly int tipoChaveBloqueio = EnumTiposChaves.Bloqueio.getInt();
        private readonly int tipoBloqueioExclusivo = EnumTiposBloqueios.Exclusivo.getInt();  
        private readonly int tipoBloqueioCompartilhado = EnumTiposBloqueios.Compartilhado.getInt();      
        public OperacoesServico(IOperacoesRepositorio operacoesRepositorio, IChaveRepositorio chaveRepositorio, ITransacaoServico transacaoServico, IBloqueioRepositorio bloqueioRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _chaveRepositorio = chaveRepositorio;
            _transacaoServico = transacaoServico;
            _bloqueioRepositorio = bloqueioRepositorio;
        }

        public IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros, int tid)
        {
            var tipoOperacao = EnumTiposOperacoes.Select.getInt();

            OperacaoDominio operacao = new OperacaoDominio 
            {
                Tipo = tipoOperacao,
                Alunos = new List<AlunoDominio>(),
                Filtros = filtros
            };

            var transacao = _transacaoServico.ObterTransacaoPorTid(tid);

            var alunos = _operacoesRepositorio.Listar(operacao, transacao);

            foreach(AlunoDominio aluno in alunos)
            {
                var bloqueado = aluno.Chave != null ? _bloqueioRepositorio.IsBloqueado(aluno, tipoBloqueioCompartilhado, tid) : false;
            
                if(bloqueado)
                {
                    throw new Exception($"Registro de chave {aluno.Chave} está bloqueado!");
                }

                var bloqueio = new BloqueioDominio()
                {
                    Chave = _chaveRepositorio.ProximaChave(tipoChaveBloqueio).ProximaChave,
                    ChaveAluno = Convert.ToInt32(aluno.Chave),
                    Tid = tid,
                    Tipo = tipoBloqueioCompartilhado
                };
                _bloqueioRepositorio.InserirBloqueio(bloqueio);                        
            }            
            
            return alunos.OrderBy(o => o.Codigo);
        }
        
        public IEnumerable<RegistroDominio> ListarRegistros(int? tid)
        {
            TransacaoDominio transacao = null;
            transacao = tid == null ? transacao : _transacaoServico.ObterTransacaoPorTid(Convert.ToInt32(tid));
            var registros = _operacoesRepositorio.ListarRegistros(transacao);
            
            foreach(RegistroDominio registro in registros)
            {
                var bloqueios = _bloqueioRepositorio.ObterBloqueios(registro.Chave);
                if(bloqueios.Count() > 0)
                {
                    var existeBloqueioExclusivo = bloqueios.Any(b => b.Tipo == tipoBloqueioExclusivo);
                    if(existeBloqueioExclusivo)
                    {
                        registro.Bloqueado = 'S';
                        registro.TipoBloqueio = EnumTiposBloqueios.Exclusivo.ObterTipoPorEnum();
                    }
                    else
                    {
                        registro.Bloqueado = 'S';
                        registro.TipoBloqueio = EnumTiposBloqueios.Compartilhado.ObterTipoPorEnum();
                    }
                }
            }
            return registros;
        } 

        public void Inserir(IEnumerable<AlunoDominio> alunos, int tid)
        {
            var transacao = _transacaoServico.ObterTransacaoPorTid(tid);

            foreach(AlunoDominio aluno in alunos)
            {
                aluno.Chave = _chaveRepositorio.ProximaChave(tipoChaveAluno).ProximaChave;                
            }

            var tipoOperacao = EnumTiposOperacoes.Insert.getInt();

            OperacaoDominio operacao = new OperacaoDominio 
            {                
                Tipo = tipoOperacao,
                Alunos = alunos,
                Filtros = new List<FiltroDominio>()
            };           

            _operacoesRepositorio.Inserir(operacao, transacao.Path);
        }

        public void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros, int tid)
        {
            if(aluno.Chave == null)
            {
                throw new Exception("Chave não pode ser null");
            }

            var bloqueado = aluno.Chave != null ? _bloqueioRepositorio.IsBloqueado(Convert.ToInt32(aluno.Chave), tid) : false;
            
            if(bloqueado)
            {
                throw new Exception("Registro está bloqueado!");
            }

            var bloqueio = new BloqueioDominio()
            {
                Chave = _chaveRepositorio.ProximaChave(tipoChaveBloqueio).ProximaChave,
                ChaveAluno = Convert.ToInt32(aluno.Chave),
                Tid = tid,
                Tipo = tipoBloqueioExclusivo
            };
            _bloqueioRepositorio.InserirBloqueio(bloqueio);
                        
            var transacao = _transacaoServico.ObterTransacaoPorTid(tid);

            var tipoOperacao = EnumTiposOperacoes.Update.getInt();

            var alunos = new List<AlunoDominio>();
            alunos.Add(aluno);

            OperacaoDominio operacao = new OperacaoDominio 
            {
                Tipo = tipoOperacao,
                Alunos = alunos,
                Filtros = filtros
            };

            _operacoesRepositorio.Atualizar(operacao, transacao.Path);
        }

        public void Deletar(IEnumerable<FiltroDominio> filtros, int tid)
        {
            var tipoOperacao = EnumTiposOperacoes.Delete.getInt();

            OperacaoDominio operacao = new OperacaoDominio 
            {
                Tipo = tipoOperacao,
                Alunos = new List<AlunoDominio>(),
                Filtros = filtros
            };

            var transacao = _transacaoServico.ObterTransacaoPorTid(tid);

            var alunos = _operacoesRepositorio.Listar(operacao, transacao);

            foreach(AlunoDominio aluno in alunos)
            {
                var bloqueado = aluno.Chave != null ? _bloqueioRepositorio.IsBloqueado(Convert.ToInt32(aluno.Chave), tid) : false;
            
                if(bloqueado)
                {
                    throw new Exception($"Registro {aluno.Chave} está bloqueado!");
                }

                var bloqueio = new BloqueioDominio()
                {
                    Chave = _chaveRepositorio.ProximaChave(tipoChaveBloqueio).ProximaChave,
                    ChaveAluno = Convert.ToInt32(aluno.Chave),
                    Tid = tid,
                    Tipo = tipoBloqueioExclusivo
                };
                _bloqueioRepositorio.InserirBloqueio(bloqueio);                        
            }              

            _operacoesRepositorio.Deletar(operacao, transacao.Path);
        }
    }
}
