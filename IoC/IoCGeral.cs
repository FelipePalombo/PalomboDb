using System;
using Microsoft.Extensions.DependencyInjection;
using Servico.Interface;
using Servico.Implementacao;
using Repositorio.Interface;
using Repositorio.Implementacao;

namespace IoC
{
    public class IoCGeral
    {
        public static void ConfigurarRepositorio(IServiceCollection services)
        {
            services.AddScoped<IOperacoesRepositorio, OperacoesRepositorio>();
            services.AddScoped<IChaveRepositorio, ChaveRepositorio>();
            services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();
            services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
        }

        public static void ConfigurarServico(IServiceCollection services)
        {            
            services.AddScoped<IOperacoesServico,OperacoesServico>();
            services.AddScoped<ITransacaoServico,TransacaoServico>();
        }
    }
}
