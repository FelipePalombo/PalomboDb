﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IoC;
using AutoMapper;
using Dominio;
using AplicacaoBase;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }       

        private MapperConfiguration RegistrosMapeados()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AlunoDto, AlunoDominio>().ReverseMap();
                cfg.CreateMap<FiltroDto, FiltroDominio>().ReverseMap();
                cfg.CreateMap<RegistroDto, RegistroDominio>().ReverseMap();
            });

            config.CompileMappings();
            config.AssertConfigurationIsValid();

            return config;             
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            IoCGeral.ConfigurarServico(services);

            IoCGeral.ConfigurarRepositorio(services);

            var mapeamento = RegistrosMapeados();
            services.AddScoped<IMapper>(x => new Mapper(mapeamento));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PalomboDb", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PalomboDb V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
