using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IBloqueioRepositorio
    {
        bool IsBloqueado(int chave, int tid);

        bool IsBloqueado(AlunoDominio aluno, int tipoBloqueio, int tid);

        IEnumerable<BloqueioDominio> ObterBloqueios(int chaveAluno);

        void InserirBloqueio(BloqueioDominio bloqueio);

        void EncerrarBloqueio(int tid);
    }
}