using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Helpers
{
    internal class JsonReader
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string ApiToken { get; set; }
        public string BaseUrl { get; set; }
        public IReadOnlyList<string> Categories { get; set; }
        public List<string> numbers { get; set; }

        public int Age { get; set; }
        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("appsettings.json"))
            {
                string json = await sr.ReadToEndAsync();
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);

                if (data.numbers != null)
                {
                    var greaterFive = data.numbers.Select(int.Parse).Where(x => x < 5);
                    foreach (var number in greaterFive)
                    {
                        Console.WriteLine(number);
                    }
                }

                var instance = data.GetType();
                var assembly = data.GetType().Assembly;
                Console.WriteLine(assembly);
                Console.WriteLine(instance);
                //
                //for(int i =0; i < data.)
                //

                Token = data.Token;
                Prefix = data.Prefix;
                ApiToken = data.ApiToken;
                BaseUrl = data.BaseUrl;
            };
        }
    }

    internal sealed class JSONStructure
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string ApiToken { get; set; }
        public string BaseUrl { get; set; }
        public IReadOnlyList<string> Categories { get; set; }
        public IReadOnlyList<string> numbers { get; set; }

        public int Age { get; set; }
    }
}
