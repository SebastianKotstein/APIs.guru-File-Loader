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
    public class ApiGuruApis
    {
        private IDictionary<string, JToken> _rawApis = new Dictionary<string, JToken>();
        private IDictionary<string, ApiGuruApi> _apis = new Dictionary<string, ApiGuruApi>();

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

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            foreach(string key in RawApis.Keys)
            {
                ApiGuruApi api = JsonConvert.DeserializeObject<ApiGuruApi>(RawApis[key].ToString());
                Apis.Add(key, api);
            }
        }

        public void SaveOnDisk(string path)
        {
            int counter = 0;
            IList<Alias> alias = new List<Alias>();

            foreach(string key in Apis.Keys)
            {
                Console.WriteLine("Save: " + key);
                Directory.CreateDirectory(path + "\\" + counter);
                Apis[key].SaveOnDisk(path + "\\" + counter);
                alias.Add(new Alias() { Key = counter + "", Value = key });
                counter++;
            }
            File.WriteAllText(path+"\\alias.json",JsonConvert.SerializeObject(alias));
        }
    }
}
