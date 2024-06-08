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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    /// <summary>
    /// Container for <see cref="ApiGuruApi"/>s. To create an instance of this class containing all APIs provided by apis.guru, use <see cref="JsonConvert.DeserializeObject(string)"/> 
    /// with the JSON structure obtained from https://api.apis.guru/v2/list.json as input.
    /// </summary>
    public class ApiGuruApis
    {
        private IDictionary<string, JToken> _rawApis = new Dictionary<string, JToken>();
        private IDictionary<string, ApiGuruApi> _apis = new Dictionary<string, ApiGuruApi>();

        /// <summary>
        /// Gets or sets the set of raw APIs in JSON.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> RawApis
        {
            get
            {
                return _rawApis;
            }

            set
            {
                _rawApis = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of deserialized <see cref="ApiGuruApi"/>s.
        /// </summary>
        [JsonIgnore]
        public IDictionary<string, ApiGuruApi> Apis
        {
            get
            {
                return _apis;
            }

            set
            {
                _apis = value;
            }
        }

        /// <summary>
        /// Deserializes the API entities that are stored as <see cref="JToken"/>s in <see cref="RawApis"/> and stores them as objects in <see cref="Apis"/>.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            foreach(string key in RawApis.Keys)
            {
                ApiGuruApi api = JsonConvert.DeserializeObject<ApiGuruApi>(RawApis[key].ToString());
                Apis.Add(key, api);
            }
        }

        /// <summary>
        /// The method saves this collection including all APIs on disk. The first argument specifies the root directory where the collection should be stored.
        /// Each API is stored within a separate sub directory located whithin the root directory. We use integer values rather than the API's name for naming these sub directories in order to
        /// avoid naming conflicts and issues with special characters that are not supported by the file system. These integer values are assigned in ascending order starting with '0'.
        /// Moreover, the method creates a meta file 'alias.json' in the specified root directory that maps these integer values used for naming these sub directories to the names of the respective APIs.
        /// See <see cref="Alias"/> for further details and instructions in order to read this meta file.
        /// <param name="path"></param>
        /// <param name="useVerboseKeys"></param>
        /// <param name="verboseOutput"></param>
        public void SaveOnDisk(string path, bool useVerboseKeys, bool verboseOutput)
        {
            int counter = 0;
            IList<Alias> alias = new List<Alias>();

            foreach(string apiName in Apis.Keys)
            {
                string key = null;
                if (useVerboseKeys)
                {
                    key = apiName.Replace(":", ".");
                }
                else
                {
                    key = counter + "";
                }

                Console.WriteLine("Save: " + key);
                Directory.CreateDirectory(path + "\\" + key);
                Apis[apiName].SaveOnDisk(path + "\\" + key, useVerboseKeys, verboseOutput);
                alias.Add(new Alias() { Key = key, Value = apiName });
                counter++;
            }
            File.WriteAllText(path+"\\alias.json",JsonConvert.SerializeObject(alias));
        }
    }
}
