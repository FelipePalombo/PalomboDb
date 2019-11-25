using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IOperacoesRepositorio
    {
        IEnumerable<AlunoDominio> Listar(OperacaoDominio operacao, TransacaoDominio transacao);
        IEnumerable<AlunoDominio> ListarTudo(TransacaoDominio transacao);
        IEnumerable<RegistroDominio> ListarRegistros();

        void Inserir(OperacaoDominio operacao, string pathOp);

        void Atualizar(OperacaoDominio operacao, string pathOp);

        void Deletar(OperacaoDominio operacao, string pathOp);
    }
}