using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Servico.Interface
{
    public interface IOperacoesServico
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros, int tid);
        IEnumerable<RegistroDominio> ListarRegistros(int? tid);
        void Inserir(IEnumerable<AlunoDominio> alunos, int tid);
        void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros, int tid);
        void Deletar(IEnumerable<FiltroDominio> filtros, int tid);
    }
}