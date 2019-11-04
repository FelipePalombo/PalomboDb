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

        public void Inserir(IEnumerable<AlunoDominio> alunos)
        {
            foreach(AlunoDominio aluno in alunos){
                _operacoesRepositorio.Inserir(aluno);
            }            
        }

        public void Update(IEnumerable<AlunoDominio> alunos, IEnumerable<FiltroDominio> filtros)
        {

        }

        public void Delete(IEnumerable<FiltroDominio> filtros)
        {

        }
    }
}
