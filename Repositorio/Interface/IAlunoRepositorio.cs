using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IAlunoRepositorio
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros);

        void Inserir(IEnumerable<AlunoDominio> alunosNovos);

        void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros);

        void Deletar(IEnumerable<FiltroDominio> filtros);

        void ReescreverAlunos(IEnumerable<AlunoDominio> alunos);
    }
}