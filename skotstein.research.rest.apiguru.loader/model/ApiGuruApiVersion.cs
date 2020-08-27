using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skotstein.research.rest.apiguru.loader.model
{
    public class ApiGuruApiVersion
    {
        private string _added;
        private string _swaggerUrl;
        private string _swaggerYamlUrl;
        private string _updated;

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

        public void SaveOnDisk(string path)
        {
            File.WriteAllText(path+"\\swagger.json", Program.GetWebContent(SwaggerUrl));
            File.WriteAllText(path + "\\swagger.yaml", Program.GetWebContent(SwaggerYamlUrl));
        }
    }
}
