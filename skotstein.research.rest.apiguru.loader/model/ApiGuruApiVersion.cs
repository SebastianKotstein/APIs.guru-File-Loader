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
    /// <see cref="ApiGuruApiVersion"/> represents a version of a <see cref="ApiGuruApi"/> obtained from apis.guru.
    /// 
    /// </summary>
    public class ApiGuruApiVersion
    {
        private string _added;
        private string _swaggerUrl;
        private string _swaggerYamlUrl;
        private string _updated;

        private long _contentSizeJson = 0;
        private long _contentSizeYaml = 0;

        /// <summary>
        /// Gets or sets the date when the Version was added.
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
        /// Gets or sets the URI pointing to the OpenAPI documentation in JSON
        /// </summary>
        [JsonProperty("swaggerUrl")]
        public string SwaggerUrl
        {
            get
            {
                return _swaggerUrl;
            }

            set
            {
                _swaggerUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the URI pointing to the OpenAPI documentation in YAML
        /// </summary>
        [JsonProperty("swaggerYamlUrl")]
        public string SwaggerYamlUrl
        {
            get
            {
                return _swaggerYamlUrl;
            }

            set
            {
                _swaggerYamlUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the date when the Version was updated
        /// </summary>
        [JsonProperty("updated")]
        public string Updated
        {
            get
            {
                return _updated;
            }

            set
            {
                _updated = value;
            }
        }

        [JsonProperty("contentSizeJson")]
        public long ContentSizeJson
        {
            get
            {
                return _contentSizeJson;
            }
            set
            {
                _contentSizeJson = value;
            }
        }

        [JsonProperty("contentSizeYaml")]
        public long ContentSizeYaml
        {
            get
            {
                return _contentSizeYaml;
            }
            set
            {
                _contentSizeYaml = value;
            }
        }

        /// <summary>
        /// Fetches the OpenAPI documentations in JSON from apis.guru and stores them under the specified directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The content size in bytes</returns>
        public long SaveSwaggerJsonOnDisk(string path)
        {
            File.WriteAllText(path+"\\swagger.json", Program.GetWebContent(SwaggerUrl));
            return new System.IO.FileInfo(path + "\\swagger.json").Length;
        }

        /// <summary>
        /// Fetches the OpenAPI documentations in YAML from apis.guru and stores them under the specified directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The content size in bytes</returns>
        public long SaveSwaggerYamlOnDisk(string path)
        {
            File.WriteAllText(path + "\\swagger.yaml", Program.GetWebContent(SwaggerYamlUrl));
            return new System.IO.FileInfo(path + "\\swagger.yaml").Length;
        }
    }
}
