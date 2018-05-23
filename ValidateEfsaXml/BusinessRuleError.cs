using EfsaBusinessRuleValidator;
using System.Xml.Linq;

namespace ValidateEfsaXml
{
    internal class BusinessRuleError
    {
        public string Meddelande { get; set; }
        public XElement El { get; set; }
        public Outcome Outcome { get; set; }

    }
}