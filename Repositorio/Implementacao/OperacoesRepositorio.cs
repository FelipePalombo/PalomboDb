using System.Collections;
using Repositorio.Interface;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Implementacao
{
    public class OperacoesRepositorio : IOperacoesRepositorio
    {
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
