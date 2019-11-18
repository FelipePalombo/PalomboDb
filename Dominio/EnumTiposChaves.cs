using System.Collections.Generic;

namespace Dominio
{
    public enum EnumTiposChaves
    {
        Transacao = 1,
        Aluno = 2,
        Bloqueio = 3
    }

    public static class EnumTiposChavesExtensao
    {
        public static int getInt(this EnumTiposChaves chave)
        {
            return (int) chave;
        }
    }
}
