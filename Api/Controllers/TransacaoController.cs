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

        // GET transacao/nova
        [HttpGet]
        public dynamic Nova()
        {
            try
            {
                var tid = _servico.NovaTransacao();
                return new {Resultado = "Sucesso", Corpo = new {Tid = tid}, Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }
        }

        // GET transacao/commit
        [HttpGet]
        public dynamic Commit(int tid)
        {
            try
            {
                _servico.CommitTransacao(tid);
                return new {Resultado = "Sucesso", Corpo = "", Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }
        }

        // GET transacao/rollback
        [HttpGet]
        public dynamic Rollback(int tid)
        {
            try
            {
                _servico.RollbackTransacao(tid);
                return new {Resultado = "Sucesso", Corpo = "", Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }
        }
    }
}
