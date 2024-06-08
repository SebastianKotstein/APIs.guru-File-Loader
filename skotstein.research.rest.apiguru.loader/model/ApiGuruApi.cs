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
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    /// <summary>
    /// <see cref="ApiGuruApi"/> represents a single API obtained from apis.guru
    /// </summary>
    public class ApiGuruApi
    {
        private string _added;
        private string _preferred;
        private ApiGuruApiVersions _versions;

        /// <summary>
        /// Gets or sets the date when the API was added.
        /// </summary>
        [JsonProperty("added")]
        public string Added
        {
            get
            {
                return _added;
            }

            set
            {
                _added = value;
            }
        }

        /// <summary>
        /// Gets or sets the version name of the preferred API
        /// </summary>
        [JsonProperty("preferred")]
        public string Preferred
        {
            get
            {
                return _preferred;
            }

            set
            {
                _preferred = value;
            }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="ApiGuruApiVersion"/>s.
        /// </summary>
        [JsonProperty("versions")]
        public ApiGuruApiVersions Versions
        {
            get
            {
                return _versions;
            }

            set
            {
                _versions = value;
            }
        }

        /// <summary>
        /// The creates a file 'info.json' on disk that contains the serialized API information in JSON. 'info.json' is stored within the directory specified as the first argument of this method.
        /// Moreover, the method creates a sub directory for each <see cref="ApiGuruApiVersion"/> of this API. We use integer values rather than the version name for naming these sub directories in order to
        /// avoid naming conflicts and issues with special characters that are not supported by the file system. These integer values are assigned in ascending order starting with '0'.
        /// The method creates a meta file 'alias.json' in the specified directory that maps these integer values used for naming these sub directories to the names of the respective version.
        /// See <see cref="Alias"/> for further details and instructions in order to read this meta file.
        /// <param name="path"></param>
        /// <param name="useVerboseKeys"></param>
        public void SaveOnDisk(string path, bool useVerboseKeys, bool verboseOutput)
        {
            int counter = 0;
            IList<Alias> alias = new List<Alias>();

            string info = JsonConvert.SerializeObject(this);
            File.WriteAllText(path + "\\info.json",info);

            foreach(string versionName in Versions.Versions.Keys)
            {
                string key = null;
                if (useVerboseKeys)
                {
                    key = versionName.Replace(":", ".");
                }
                else
                {
                    key = counter + "";
                }

                Directory.CreateDirectory(path + "\\" + key);
                Versions.Versions[key].ContentSizeJson = Versions.Versions[key].SaveSwaggerJsonOnDisk(path + "\\" + key);
                Versions.Versions[key].ContentSizeYaml = Versions.Versions[key].SaveSwaggerYamlOnDisk(path + "\\" + key);
                double totalSizeKB = (Versions.Versions[key].ContentSizeJson + Versions.Versions[key].ContentSizeYaml) / 1024.0;
                if (verboseOutput)
                {
                    Console.WriteLine("Version: " + key+", Size: "+String.Format("{0:0.##}", totalSizeKB) +" kB");
                }
                else
                {
                    Console.WriteLine("Version: " + key);
                }
                alias.Add(new Alias() { Key = key, Value = versionName });
                counter++;
            }
            File.WriteAllText(path + "\\alias.json", JsonConvert.SerializeObject(alias));
        }

        /// <summary>
        /// Loads an API description in JSON from disk. The loaded description is deserialized and returned as a <see cref="ApiGuruApi"/> object.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ApiGuruApi LoadFromDisk(string path)
        {
            return JsonConvert.DeserializeObject<ApiGuruApi>(File.ReadAllText(path));
        }
    }
}
