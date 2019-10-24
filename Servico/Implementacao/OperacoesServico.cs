using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;

namespace Servico.Implementacao
{
    public class OperacoesServico : IOperacoesServico
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
