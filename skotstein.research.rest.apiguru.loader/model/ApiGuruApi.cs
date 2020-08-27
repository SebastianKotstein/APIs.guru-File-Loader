using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    public class ApiGuruApi
    {
        private string _added;
        private string _preferred;
        private ApiGuruApiVersions _versions;

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

        public void SaveOnDisk(string path)
        {
            int counter = 0;
            IList<Alias> alias = new List<Alias>();

            string info = JsonConvert.SerializeObject(this);
            File.WriteAllText(path + "\\info.json",info);

            foreach(string key in Versions.Versions.Keys)
            {
                Console.WriteLine("Version: " + key);
                Directory.CreateDirectory(path + "\\" + counter);
                Versions.Versions[key].SaveOnDisk(path + "\\" + counter);
                alias.Add(new Alias() { Key = "" + counter, Value = key });
                counter++;
            }
            File.WriteAllText(path + "\\alias.json", JsonConvert.SerializeObject(alias));
        }

        public static ApiGuruApi LoadFromDisk(string path)
        {
            return JsonConvert.DeserializeObject<ApiGuruApi>(File.ReadAllText(path));
        }
    }
}
