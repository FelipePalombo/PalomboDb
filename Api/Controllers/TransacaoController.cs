using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AplicacaoBase;
using Servico.Interface;
using Dominio;

namespace Api.Controllers
{
    [Route("[controller]/[action]")]
    [Produces ("application/json")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private ITransacaoServico _servico;
        private IMapper _mapper;

        public TransacaoController(ITransacaoServico servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        // POST transacao/nova
        [HttpGet]
        public dynamic Nova()
        {
            try
            {
                var tid = _servico.NovaTransacao();
                return new {Resultado = "Sucesso", Corpo = new {Tid = tid}};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }
        }
    }
}
