using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfsaBusinessRuleValidator
{
    /// <summary>
    /// Information about the rule
    /// </summary>
    public class RuleAttribute : Attribute
    {
        /// <summary>
        /// Type of rule
        /// </summary>
        public string RuleType { get; set; }
        
        /// <summary>
        /// Rule description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Felmeddelande
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// is this rule deprecated
        /// </summary>
        public bool Deprecated { get; set; }
    }
}
