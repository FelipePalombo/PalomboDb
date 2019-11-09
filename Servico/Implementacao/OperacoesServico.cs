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
        public OperacoesServico(IOperacoesRepositorio operacoesRepositorio)
        {
            _operacoesRepositorio = operacoesRepositorio;
        }

        public IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros)
        {
            return _operacoesRepositorio.Listar(filtros);
        }
        
        public IEnumerable<RegistroDominio> ListarRegistros() => _operacoesRepositorio.ListarRegistros();

        public void Inserir(IEnumerable<AlunoDominio> alunos)
        {
            _operacoesRepositorio.Inserir(alunos);
        }

        public void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros)
        {
            _operacoesRepositorio.Atualizar(aluno, filtros);
        }

        public void Deletar(IEnumerable<FiltroDominio> filtros)
        {
            _operacoesRepositorio.Deletar(filtros);
        }
    }
}
