using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Repositorio.Interface
{
    public interface IChaveRepositorio
    {
        ChaveDominio ProximaChave(int tipoChave);
    }
}