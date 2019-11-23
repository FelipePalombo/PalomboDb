using System.Collections.Generic;

namespace Dominio
{
    public enum EnumTiposBloqueios
    {        
        Compartilhado = 1,
        Exclusivo = 2
    }

    public static class EnumTiposBloqueiosExtensao
    {
        public static string ObterTipoPorEnum (this EnumTiposBloqueios enumTiposBloqueios)
        {
            switch (enumTiposBloqueios)
            {
                case EnumTiposBloqueios.Compartilhado:
                    return "Compartilhado";
                case EnumTiposBloqueios.Exclusivo:
                    return "Exclusivo";
                default:
                    return "Nenhum";        
            }
        }
    }
}
