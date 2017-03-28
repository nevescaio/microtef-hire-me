using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneClassLibrary
{
    public class TransactionResult
    {
        public string date {get; set;}
        public string cardholderName { get; set; }
        public string number { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
        public int instalments { get; set; }
        public string result { get; set; }
    }
}
