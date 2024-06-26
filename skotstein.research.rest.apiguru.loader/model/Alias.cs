﻿// MIT License
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    /// <summary>
    /// <see cref="Alias"/> maps a <see cref="Key"/> (e.g. an integer value) to a <see cref="Value"/> (e.g. the name of an API).
    /// This .NET type is used to map directory names containing APIs to the respective names of the APIs (see <see cref="ApiGuruApis.SaveOnDisk(string)"/>) as well as 
    /// for mapping directory names containing versions of a respective API to the version names (see <see cref="ApiGuruApi.SaveOnDisk(string)"/>).
    /// Use <see cref="ReadList(string)"/> to load the list of <see cref="Alias"/> stored within the 'alias.json' meta files created by <see cref="ApiGuruApis.SaveOnDisk(string)"/> and <see cref="ApiGuruApi.SaveOnDisk(string)"/>.
    /// </summary>
    public class Alias
    {
        private string _key;
        private string _value;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [JsonProperty("key")]
        public string Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
            }
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Deserializes the specified JSON structure to a collection of <see cref="Alias"/> and returns this collection as a <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IList<Alias> ReadList(string json)
        {
            return JsonConvert.DeserializeObject<IList<Alias>>(json);
        }
    }

   
}
