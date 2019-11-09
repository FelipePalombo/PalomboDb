using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IOperacoesRepositorio
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros);

        IEnumerable<RegistroDominio> ListarRegistros();

        void Inserir(IEnumerable<AlunoDominio> alunosNovos);

        void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros);

        void Deletar(IEnumerable<FiltroDominio> filtros);
    }
}