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
    public static class LogsServico
    {
        public static void AdicionarLog(LogDominio log)
        {
            string path = @"..\Repositorio\Banco\logs.json";

            JsonSerializerSettings serializerSettings;
            serializerSettings = new JsonSerializerSettings 
            { 
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto 
            };

            string json = File.ReadAllText(path);
            var list = JsonConvert.DeserializeObject<List<LogDominio>>(json) ?? new List<LogDominio>();
            list.Add(log);

            var jsonNovo = JsonConvert.SerializeObject(list, serializerSettings);
            File.WriteAllText(path, jsonNovo);
        }
    }
}