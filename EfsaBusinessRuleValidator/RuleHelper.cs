using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static EfsaBusinessRuleValidator.RuleValidator;

namespace EfsaBusinessRuleValidator
{
    public class RuleHelper
    {
        /// <summary>
        /// Get a list of rules from the domain, general rules can also be included 
        /// </summary>
        /// <param name="ruleValidatorType">The domaintype to get businessrule from. Available types: <see cref="RuleValidatorType"/></param>
        /// <param name="includeGeneralBusinessRules">Include general business rules</param>
        /// <returns>A list of <see cref="BusinessRuleInfo"/></returns>
        public IList<BusinessRuleInfo> GetBusinessRules(RuleValidatorType ruleValidatorType, bool includeGeneralBusinessRules = true)
        {
            var res = new List<BusinessRuleInfo>();
            var domainType = GetRuleDomainType(ruleValidatorType);

            res.AddRange(GetBusinessRules(domainType, ruleValidatorType));
            if (includeGeneralBusinessRules)
            {
                var generalType = GetRuleDomainType(RuleValidatorType.GENERAL);
                res.AddRange((GetBusinessRules(generalType, RuleValidatorType.GENERAL)));
            }
            return res;
        }

        /// <summary>
        /// Get a list of rules from the type 
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get rules from </param>
        /// <param name="ruleValidatorType">The domaintype to get businessrule from. Available types: <see cref="RuleValidatorType"/></param>
        /// <returns>A list of <see cref="BusinessRuleInfo"/></returns>
        private IList<BusinessRuleInfo> GetBusinessRules(Type type, RuleValidatorType ruleValidatorType)
        {
            var res = new List<BusinessRuleInfo>();
            if (type == null)
                return res;

            var props = type.GetMethods();

            foreach (var item in props)
            {
                var ny = new BusinessRuleInfo()
                {
                    BusinessRule = item.Name,
                    Deprecated = false,
                    RuleType = ruleValidatorType,
                };
                if (ny != null && item.ReturnType == typeof(Outcome))
                {
                    var attribute = item.GetCustomAttribute<RuleAttribute>();
                    if (attribute != null)
                    {
                        ny.Comment = attribute.Description;
                        ny.Deprecated = attribute.Deprecated;
                    }
                    res.Add(ny);
                }
            }
            return res;
        }

        /// <summary>
        /// Resolves domainclass from type
        /// </summary>
        /// <param name="typeToUse">The domaintype. Available types: <see cref="RuleValidatorType"/></param>
        /// <returns>A <see cref="Type"/> for the actual domaintype</returns>
        internal static Type GetRuleDomainType(RuleValidatorType typeToUse)
        {
            switch (typeToUse)
            {
                case RuleValidatorType.PEST:
                    return typeof(PestBusinessRules);
                case RuleValidatorType.CHEM:
                    return typeof(ChemBusinessRules);
                case RuleValidatorType.VMPR:
                    return typeof(VmprBusinessRules);
                case RuleValidatorType.GENERAL:
                    return typeof(GeneralBusinessRules);
            }
            return null;
        }
    }
}
