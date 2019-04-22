using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Download_SAP_Schemas
{    
    class ListOfSchemas
    {
        public List<Schemas> Schemas { get; set; }
    }

    class Schemas
    {
        public Schemas()
        {

        }

        public Schemas (string name, string content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; }
        public string Content { get; set; }
    }

}
