﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EfsaBusinessRuleValidator
{
    /// <summary>
    /// Class with metods to test elements agianst rules
    /// </summary>
    public class RuleValidator
    {
        private string _yearToTest;

        /// <summary>
        /// Constructor for <see cref="RuleValidator"/>
        /// </summary>
        /// <param name="yearToTest">The year that the rules to test against, correct format is YYYY</param>
        public RuleValidator(string yearToTest)
        {
            _yearToTest = yearToTest;
        }

        public enum RuleValidatorType
        {
            PEST = 1,
            CHEM = 2,
            VMPR = 3,
            GENERAL = 4,
        }

        /// <summary>
        /// Validate an xelement against its domain rules AND general rules
        /// </summary>
        /// <param name="ruleType">Domaintype</param>
        /// <param name="ruleNames">List of rule names to validate against</param>
        /// <param name="element">The element to validate</param>
        /// <returns>A list of BusinessRuleReturnInfo containing the xelement and outcome from rule</returns>
        public IList<BusinessRuleValidateResult> ValidateRules(RuleValidatorType ruleValidatorType, IList<string> ruleNames, XElement element)
        {
            var result = new List<BusinessRuleValidateResult>();                        
            Type domainType = RuleHelper.GetRuleDomainType(ruleValidatorType);
            //Test domainrules
            RuleValidationForType(domainType, ruleNames, result, element);
            //Test General rules
            RuleValidationForType(typeof(GeneralBusinessRules), ruleNames, result, element);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleType">Domaintype</param>
        /// <param name="ruleNames">List of rule names to validate against</param>
        /// <param name="result">The list of result that will be returned</param>
        /// <param name="element">The element to validate</param>
        private void RuleValidationForType(Type ruleType, IList<string> ruleNames, List<BusinessRuleValidateResult> result, XElement element)
        {
            var tmpArray = new object[] { element };

            Object[] args = { _yearToTest };

            object ruleInstance = Activator.CreateInstance(ruleType, args);

            foreach (var regel in ruleNames)
            {
                MethodInfo theMethod = ruleType.GetMethod(regel);
                if (theMethod == null)
                {
                    result.Add(new BusinessRuleValidateResult { El = element, Outcome = new Outcome() { Passed = false }, UnknownRule = true, Message = $"The rule {regel} doesnt exist in type {ruleType.Name}" });
                    continue;
                }
                var o = theMethod.Invoke(ruleInstance, tmpArray);
                if (o == null)
                {
                    result.Add(new BusinessRuleValidateResult { El = element, Outcome = new Outcome() { Passed = false }, UnknownRule = false, Message= $"The rule {regel} in type {ruleType.Name} didn´t return any outcome" });                    
                }
                else if (o is Outcome a)
                {
                    if (!a.Passed)
                    {
                        result.Add(new BusinessRuleValidateResult { El = element, Outcome = a, UnknownRule = false });
                    }
                }
            }
        }

        
        /// <summary>
        /// Describes validation result
        /// </summary>
        public class BusinessRuleValidateResult
        {
            /// <summary>
            /// A message if applicable
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// Is the rule unknown, not implemented
            /// </summary>
            public bool UnknownRule { get; set; }

            /// <summary>
            /// The xml-element validated
            /// </summary>
            public XElement El { get; set; }

            /// <summary>
            /// <see cref="Outcome"/> of the validation 
            /// </summary>
            public Outcome Outcome { get; set; }
        }
    }
}
