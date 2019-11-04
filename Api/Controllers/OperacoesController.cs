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
    public class OperacoesController : ControllerBase
    {
        private IOperacoesServico _servico;
        private IMapper _mapper;

        public OperacoesController(IOperacoesServico servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        // GET operacoes/listar
        [HttpPost]
        public dynamic Listar([FromBody] IEnumerable<FiltroDto> filtros)
        {
            try
            {
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                var retorno = _mapper.Map<IEnumerable<AlunoDto>>(_servico.Listar(pFiltros));
                return new {Resultado = "Sucesso", Corpo = retorno};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }            
        }

        // GET operacoes/inseriralunos
        [HttpPost]        
        public dynamic InserirAlunos([FromBody] IEnumerable<AlunoDto> alunos)
        {
            try
            {
                var pAlunos = _mapper.Map<IEnumerable<AlunoDominio>>(alunos);
                _servico.Inserir(pAlunos);
                
                return new {Resultado = "Sucesso"};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }
            
        }
    }
}
