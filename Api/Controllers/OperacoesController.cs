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
        public dynamic Listar([FromBody] IEnumerable<FiltroDto> filtros)
        {
            try
            {
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                var retorno = _mapper.Map<IEnumerable<AlunoFiltroDto>>(_servico.Listar(pFiltros));
                return new {Resultado = "Sucesso", Corpo = retorno};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }            
        }

        [HttpGet]
        /// <summary>
        /// Teste
        /// </summary>
        public dynamic ListarRegistros()
        {
            try
            {
                var retorno = _mapper.Map<IEnumerable<RegistroDto>>(_servico.ListarRegistros());
                return new {Resultado = "Sucesso", Corpo = retorno};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }            
        }

        // POST operacoes/inseriralunos
        [HttpPost]        
        public dynamic InserirAlunos([FromBody] IEnumerable<AlunoFiltroDto> alunos)
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

        // POST operacoes/atualizar
        [HttpPost]        
        public dynamic Atualizar(AlunoFiltroDto alunoFiltro)
        {
            AlunoDto aluno = alunoFiltro.aluno;
            IEnumerable<FiltroDto> filtros = alunoFiltro.filtros;
            try
            {
                var pAluno = _mapper.Map<AlunoDominio>(aluno);
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                _servico.Atualizar(pAluno, pFiltros);
                
                return new {Resultado = "Sucesso"};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }            
        }

        // POST operacoes/listar
        [HttpPost]
        public dynamic Deletar([FromBody] IEnumerable<FiltroDto> filtros)
        {
            try
            {
                var pFiltros = _mapper.Map<IEnumerable<FiltroDominio>>(filtros);
                _servico.Deletar(pFiltros);
                return new {Resultado = "Sucesso"};
            }
            catch(Exception e)
            {
                return new {Resultado = "Erro", Erro = e.Message};
            }            
        }
    }
}
