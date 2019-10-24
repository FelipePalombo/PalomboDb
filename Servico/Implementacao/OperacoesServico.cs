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

        public AlunoDominio Listar(IEnumerable<FiltroDominio> filtros)
        {
            return new AlunoDominio();
        }

        public void Inserir(AlunoDominio aluno)
        {

        }

        public void Update(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros)
        {

        }

        public void Delete(IEnumerable<FiltroDominio> filtros)
        {

        }
    }
}
