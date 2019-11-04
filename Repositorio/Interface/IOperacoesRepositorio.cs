using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IOperacoesRepositorio
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros);

        void Inserir(AlunoDominio aluno);

        void Update(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros);

        void Delete(IEnumerable<FiltroDominio> filtros);
    }
}