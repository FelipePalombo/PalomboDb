using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;

namespace Servico.Implementacao
{
    public class TransacaoServico : ITransacaoServico
    {
        private readonly ITransacaoRepositorio _transacaoRepositorio;
        public TransacaoServico(ITransacaoRepositorio transacaoRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;
        }

        public int NovaTransacao()
        {
            return _transacaoRepositorio.NovaTransacao();
        }
    }
}
