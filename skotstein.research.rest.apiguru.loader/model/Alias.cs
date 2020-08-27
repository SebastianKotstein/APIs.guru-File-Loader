using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    public class Alias
    {
        private string _key;
        private string _value;

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
        public static IList<Alias> ReadList(string json)
        {
            return JsonConvert.DeserializeObject<IList<Alias>>(json);
        }
    }

   
}
