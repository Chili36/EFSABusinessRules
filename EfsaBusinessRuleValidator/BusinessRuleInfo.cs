using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EfsaBusinessRuleValidator.RuleValidator;

namespace EfsaBusinessRuleValidator
{
    /// <summary>
    /// Describes a rule
    /// </summary>
    public class BusinessRuleInfo
    {
        /// <summary>
        /// The name of rule
        /// </summary>
        public string BusinessRule { get; set; }        
        
        /// <summary>
        /// Type of rule <see cref="RuleValidatorType"/>
        /// </summary>
        public RuleValidatorType RuleType { get; set; }        
        
        /// <summary>
        /// A comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Is the rule deprecated
        /// </summary>
        public bool Deprecated { get; set; }
    }
}
