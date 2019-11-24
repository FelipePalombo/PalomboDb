using System.Collections.Generic;

namespace Dominio
{
    public enum EnumTiposTransacoes
    {        
        Aberto = 0,
        Commit = 1,
        Rollback = 2
    }

    public static class EnumTiposTransacoesExtensao
    {
        public static string ObterTipoPorEnum (this EnumTiposTransacoes enumTiposTransacoes)
        {
            switch (enumTiposTransacoes)
            {
                case EnumTiposTransacoes.Aberto:
                    return "Aberta";
                case EnumTiposTransacoes.Commit:
                    return "Commited";
                case EnumTiposTransacoes.Rollback:
                    return "Rollbacked";
                default:
                    return "Aberta";                            
            }
        }
    }
}
