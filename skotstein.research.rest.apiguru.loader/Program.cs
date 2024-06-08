// MIT License
//
// Copyright (c) 2020 Sebastian Kotstein
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
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
    /// <summary>
    /// Loads the list of APIs (https://api.apis.guru/v2/list.json) as well as all OpenAPI documentations of these APIs and stores them under the specified root directory (first argument) on disk.
    /// </summary>
    public class Program
    {
        private static HttpClient _client;
        public static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;

                //delete directory (if it exists)
                if (Directory.Exists(args[0]))
                {
                    Console.WriteLine("Delete directory: " + args[0]);
                    Directory.Delete(args[0], true);
                }
                Directory.CreateDirectory(args[0]);

                ISet<string> flags = new HashSet<string>();
                for(int i = 1; i < args.Length; i++)
                {
                    flags.Add(args[i]);
                }

                _client = new HttpClient();
                //fetch json file listing all APIs
                string json = GetWebContent(@"https://api.apis.guru/v2/list.json");

                //convert json into object model
                apiguru.loader.model.ApiGuruApis apis = JsonConvert.DeserializeObject<apiguru.loader.model.ApiGuruApis>(json);
                apis.SaveOnDisk(args[0],flags.Contains("-vk") || flags.Contains("-verbosekeys"), flags.Contains("-v") || flags.Contains("-verbose"));

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
