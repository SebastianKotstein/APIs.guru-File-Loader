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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{

    /// <summary>
    /// Container for <see cref="ApiGuruApiVersion"/>s. An instance of this class is nested in a <see cref="ApiGuruApi"/> object after deserializing the complete list of APIs from JSON.
    /// </summary>
    public class ApiGuruApiVersions
    {
        private IDictionary<string, JToken> _rawVersions = new Dictionary<string,JToken>();
        private IDictionary<string, ApiGuruApiVersion> _versions = new Dictionary<string,ApiGuruApiVersion>();

        /// <summary>
        /// Gets or sets the set of raw API Versions in JSON.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, JToken> RawVersions
        {
            get
            {
                return _rawVersions;
            }

            set
            {
                _rawVersions = value;
            }
        }

        /// <summary>
        /// Gets or sets the set of deserialized <see cref="ApiGuruApiVersion"/>s.
        /// </summary>
        [JsonIgnore]
        public IDictionary<string, ApiGuruApiVersion> Versions
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
        /// Deserializes the <see cref="ApiGuruApiVersion"/> entities that are stored as <see cref="JToken"/>s in <see cref="RawVersions"/> and stores them as objects in <see cref="Versions"/>.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            foreach(string key in RawVersions.Keys)
            {
                ApiGuruApiVersion version = JsonConvert.DeserializeObject<ApiGuruApiVersion>(RawVersions[key].ToString());
                Versions.Add(key, version);
            }
            
        }
    }
}
