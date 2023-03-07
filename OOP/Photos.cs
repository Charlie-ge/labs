using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ооп8
{
    class Photos
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Description} - {Date}";
        }
    }
}
