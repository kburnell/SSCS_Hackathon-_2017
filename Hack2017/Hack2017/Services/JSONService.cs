using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Hack2017.Services
{
    public class JSONService
    {
        public IEnumerable<T> Read<T>(string filePath) where T : class
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(filePath)).ToList();
        }

        public void Write(object obj, string filePath)
        {
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(filePath, json);
        }

    }
}
