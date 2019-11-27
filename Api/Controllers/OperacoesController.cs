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

        // POST operacoes/listar
        [HttpPost]
        public dynamic Listar([FromBody] TidFiltrosDto tidFiltrosDto)
        {
            IEnumerable<FiltroDto> filtros = tidFiltrosDto.Filtros;
            int tid = tidFiltrosDto.Tid;
            try
            {
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                var retorno = _mapper.Map<IEnumerable<AlunoDto>>(_servico.Listar(pFiltros, tid));
                return new {Resultado = "Sucesso", Corpo = retorno, Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }            
        }

        [HttpGet]
        public dynamic ListarRegistros(int tid)
        {
            try
            {
                var retorno = _mapper.Map<IEnumerable<RegistroDto>>(_servico.ListarRegistros(tid));
                return new {Resultado = "Sucesso", Corpo = retorno, Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }            
        }

        // POST operacoes/inserir
        [HttpPost]        
        public dynamic Inserir(InserirDto inserirDto)
        {
            IEnumerable<AlunoDto> alunos = inserirDto.Alunos;
            int tid = inserirDto.Tid;
            try
            {
                var pAlunos = _mapper.Map<IEnumerable<AlunoDominio>>(alunos);
                _servico.Inserir(pAlunos, tid);
                
                return new {Resultado = "Sucesso", Corpo = "", Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }
            
        }

        // POST operacoes/atualizar
        [HttpPut]        
        public dynamic Atualizar(AtualizarDto atualizarDto)
        {
            AlunoDto aluno = atualizarDto.Aluno;
            IEnumerable<FiltroDto> filtros = atualizarDto.Filtros;
            int tid = atualizarDto.Tid;
            try
            {
                var pAluno = _mapper.Map<AlunoDominio>(aluno);
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                _servico.Atualizar(pAluno, pFiltros, tid);
                
                return new {Resultado = "Sucesso", Corpo = "", Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }            
        }

        // POST operacoes/listar
        [HttpDelete]
        public dynamic Deletar([FromBody] TidFiltrosDto tidFiltrosDto)
        {
            IEnumerable<FiltroDto> filtros = tidFiltrosDto.Filtros;
            int tid = tidFiltrosDto.Tid;
            try
            {
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                _servico.Deletar(pFiltros, tid);
                return new {Resultado = "Sucesso", Corpo = "", Erro = ""};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Corpo = "", Erro = e.Message};
            }            
        }
    }
}
