using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Models
{
    class Compromisso
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string local { get; set; }
        public DateTime data { get; set; }
        public bool realizado { get; set; }
    }
}
