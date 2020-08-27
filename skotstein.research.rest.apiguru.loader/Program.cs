using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader
{
    public class Program
    {
        private static HttpClient _client;
        public static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (Directory.Exists(args[0]))
                {
                    Console.WriteLine("Delete directory: " + args[0]);
                    Directory.Delete(args[0], true);
                }
                Directory.CreateDirectory(args[0]);

                _client = new HttpClient();
                string json = GetWebContent(@"https://api.apis.guru/v2/list.json");
                apiguru.loader.model.ApiGuruApis apis = JsonConvert.DeserializeObject<apiguru.loader.model.ApiGuruApis>(json);
                apis.SaveOnDisk(args[0]);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Exception stackTrace = e;
                while (e != null)
                {
                    Console.WriteLine(e.Message);
                    e = e.InnerException;
                }
                Console.WriteLine(stackTrace.StackTrace);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press key to terminate");
                Console.ReadLine();
            }
          
            
        }

        public static string GetWebContent(string url)
        {
            return _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri(url))).Result.Content.ReadAsStringAsync().Result;
        }
    }

   
}
