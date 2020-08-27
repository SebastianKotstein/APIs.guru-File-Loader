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
    public class ApiGuruApiVersions
    {
        private IDictionary<string, JToken> _rawVersions = new Dictionary<string,JToken>();
        private IDictionary<string, ApiGuruApiVersion> _versions = new Dictionary<string,ApiGuruApiVersion>();


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
