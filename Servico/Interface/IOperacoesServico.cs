using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Servico.Interface
{
    public interface IOperacoesServico
    {
        IEnumerable<AlunoDominio> Listar(IEnumerable<FiltroDominio> filtros);
        IEnumerable<RegistroDominio> ListarRegistros();
        void Inserir(IEnumerable<AlunoDominio> alunos);
        void Atualizar(AlunoDominio aluno, IEnumerable<FiltroDominio> filtros);
        void Deletar(IEnumerable<FiltroDominio> filtros);
    }
}