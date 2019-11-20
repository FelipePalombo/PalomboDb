using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IOperacoesRepositorio
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros, int tid);

        IEnumerable<RegistroDominio> ListarRegistros();

        void Inserir(IEnumerable<AlunoDominio> alunosNovos, int tid);

        void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros, int tid);

        void Deletar(IEnumerable<FiltroDominio> filtros, int tid);
    }
}