using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EfsaBusinessRuleValidator
{

    /// <summary>
    /// Validates EFSAs business rules with one method per Rule. Each method takes an Xelement (result) coded in workflow 2
    /// 
    /// Version 0.3 April 2018
    /// </summary>
    public class ChemBusinessRules
    {
        #region Testmetoder

        [Rule(Description = "This is a testmethod", ErrorMessage = "This method doesn´t return an error", RuleType = "Test")]
        public Outcome CHEM_TestOk(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM_TestOk",
                Passed = true,
                Description = "This is a testmethod",
                Error = "This method doesn´t return an error",
            };
            return outcome;
        }

        [Rule(Description = "This is a testmethod that returns an error", ErrorMessage = "A test error", RuleType = "Test")]
        public Outcome CHEM_TestError(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM_TestError",
                Passed = false,
                Description = "This is a testmethod that returns an error",
                Error = "A test error",
            };
            return outcome;
        }
        #endregion
    }
}
