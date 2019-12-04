using System.Collections;
using Servico.Interface;
using System.Collections.Generic;
using Dominio;
using Repositorio.Interface;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.IO;

namespace Servico.Implementacao
{
    public class LogsServico
    {
        private JsonSerializerSettings serializerSettings;
        private string path = @"..\Repositorio\Banco\logs.json";
        public LogsServico()
        {
            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };
        }

        public void AdicionarLog(LogDominio log)
        {
            string json = File.ReadAllText(path);
            var list = JsonConvert.DeserializeObject<List<LogDominio>>(json) ?? new List<LogDominio>();
            list.Add(log);

            var jsonNovo = JsonConvert.SerializeObject(list, serializerSettings);
            File.WriteAllText(path, jsonNovo);
        }
    }
}