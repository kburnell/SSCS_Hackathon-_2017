using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Hack2017 {

    public class Reader {

        public IEnumerable<T> Read<T>(string filePath) where T : class {
            {
                try {
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(filePath)).ToList();
                }
                catch (Exception ex) {
                    Console.Write(ex);
                }
                return null;
            }
        }

    }

}