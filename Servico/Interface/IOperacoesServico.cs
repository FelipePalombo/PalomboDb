using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Servico.Interface
{
    public interface IOperacoesServico
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros);

        void Inserir(IEnumerable<AlunoDominio> alunos);

        void Update(IEnumerable<AlunoDominio> alunos, IEnumerable<FiltroDominio> filtros);

        void Delete(IEnumerable<FiltroDominio> filtros);
    }
}