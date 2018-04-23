using EfsaBusinessRuleValidator;
using System.Xml.Linq;

namespace ValidateEfsaXml
{
    internal class BusinessRuleError
    {
        public XElement El { get; set; }
        public Outcome outcome  { get; set; }

    }
}