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

        // GET operacoes
        [HttpGet]
        public IEnumerable<AlunoDto> Get()
        {
            var aluno = new AlunoDto{ Codigo = 01, Nome = "Felipe", Nota = 10 };
            var listaAlunos = new List<AlunoDto>();
            listaAlunos.Add(aluno);
            return listaAlunos;
        }

        // GET operacoes/inseriralunos
        [HttpPost]        
        public dynamic InserirAlunos([FromBody] IEnumerable<AlunoDto> alunos)
        {
            try
            {
                var pAlunos = _mapper.Map<IEnumerable<AlunoDominio>>(alunos);
                _servico.Inserir(pAlunos);
                return alunos;
            }
            catch(Exception e)
            {
                return e;
            }
            
        }
    }
}
