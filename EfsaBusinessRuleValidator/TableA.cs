using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfsaBusinessRuleValidator
{
    class TableA
    {
        public TableA()
        {
            


        }
        
        public List<dataitem> Data
        {
            get;
        }

    }


     class dataitem
    {
        public string prodCode { get; set; }
        public string paramCode { get; set; }
        public string paramTypeP005A { get; set; }
        public string paramTypeP004A { get; set; }

    }
}
