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
        private readonly int tipoChaveAluno = EnumTiposChaves.Aluno.getInt();
        public OperacoesServico(IOperacoesRepositorio operacoesRepositorio, IChaveRepositorio chaveRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
            _chaveRepositorio = chaveRepositorio;
        }

        public IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros, int tid)
        {
            return _operacoesRepositorio.Listar(filtros, tid);
        }
        
        public IEnumerable<RegistroDominio> ListarRegistros() => _operacoesRepositorio.ListarRegistros();

        public void Inserir(IEnumerable<AlunoDominio> alunos, int tid)
        {
            foreach(AlunoDominio aluno in alunos)
            {
                aluno.Chave = _chaveRepositorio.ProximaChave(tipoChaveAluno).ProximaChave;                
            }
            _operacoesRepositorio.Inserir(alunos, tid);
        }

        public void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros, int tid)
        {
            _operacoesRepositorio.Atualizar(aluno, filtros, tid);
        }

        public void Deletar(IEnumerable<FiltroDominio> filtros, int tid)
        {
            _operacoesRepositorio.Deletar(filtros, tid);
        }
    }
}
