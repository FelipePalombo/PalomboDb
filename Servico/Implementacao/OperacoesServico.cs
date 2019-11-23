using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;

namespace Servico.Implementacao
{
    public class OperacoesServico : IOperacoesServico
    {
        private readonly IOperacoesRepositorio _operacoesRepositorio;
        private readonly IChaveRepositorio _chaveRepositorio;
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        private readonly int tipoChaveAluno = EnumTiposChaves.Aluno.getInt();        
        public OperacoesServico(IOperacoesRepositorio operacoesRepositorio, IChaveRepositorio chaveRepositorio, ITransacaoRepositorio transacaoRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _chaveRepositorio = chaveRepositorio;
            _transacaoRepositorio = transacaoRepositorio;
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

            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);

            return _operacoesRepositorio.Listar(operacao, transacao);
        }
        
        public IEnumerable<RegistroDominio> ListarRegistros() => _operacoesRepositorio.ListarRegistros();

        public void Inserir(IEnumerable<AlunoDominio> alunos, int tid)
        {
            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);

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
            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);

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
            var transacao = _transacaoRepositorio.ObterTransacaoPorTid(tid);

            var tipoOperacao = EnumTiposOperacoes.Delete.getInt();

            OperacaoDominio operacao = new OperacaoDominio 
            {
                Tipo = tipoOperacao,
                Alunos = new List<AlunoDominio>(),
                Filtros = filtros
            };
            
            _operacoesRepositorio.Deletar(operacao, transacao.Path);
        }
    }
}
