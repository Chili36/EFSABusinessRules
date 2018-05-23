using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    public class PestBusinessRules
    {
        private string _yearToTest;

        /// <summary>
        /// Constructor for <see cref="PestBusinessRules"/>
        /// </summary>
        /// <param name="yearToTest">The year that the rules to test against, correct format is YYYY</param>
        public PestBusinessRules(string yearToTest)
        {
            _yearToTest = yearToTest;
        }

        #region Testmetoder

        [Rule(Description = "This is a testmethod", ErrorMessage = "This method doesn´t return an error", RuleType = "Test")]
        public Outcome PEST_TestOk(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST_Test",
                Passed = true,
                Description = "This is a testmethod",
                Error = "This method doesn´t return an error",
            };
            return outcome;
        }

        [Rule(Description = "This is a testmethod that returns an error", ErrorMessage = "A test error", RuleType = "Test")]
        public Outcome PEST_TestError(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST_TestError",
                Passed = false,
                Description = "This is a testmethod that returns an error",
                Error = "A test error",
            };
            return outcome;
        }
        #endregion

        #region SSD1Rules

        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is greater than the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Less than or equal to maximum permissible quantities' (J002A);
        [Rule(Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is greater than the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Less than or equal to maximum permissible quantities' (J002A)",
            ErrorMessage = "result evaluation is incorrect; result value exceeds the result legal limit",
            RuleType = "error")]
        public Outcome MRL_01(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode")?.Value;
            var paramCode = sample.Element("paramCode")?.Value;
            var resType = sample.Element("resType")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;
            var resVal = sample.Element("resVal")?.Value;
            var resEvaluation = sample.Element("resEvaluation")?.Value;
            var legalLimit = (string)sample.Element("resLegalLimit");

            var outcome = new Outcome
            {
                Name = "MRL_01",
                Lastupdate = "2017-04-28",
                Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is greater than the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Less than or equal to maximum permissible quantities' (J002A);",
                Error = "result evaluation is incorrect; result value exceeds the result legal limit;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".", ",")) >= decimal.Parse(legalLimit.Replace(".", ",")))
            {
                outcome.Passed = resEvaluation != "J002A";
            }
            return outcome;
        }

        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is less than or equal to the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Greater than maximum permissible quantities' (J003A) and 'Compliant due to measurement uncertainty' (J031A);
        [Rule(Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is less than or equal to the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Greater than maximum permissible quantities' (J003A) and 'Compliant due to measurement uncertainty' (J031A)",
            ErrorMessage = "Result evaluation is incorrect; result value is equal or below the result legal limit",
            RuleType = "error")]
        public Outcome MRL_02(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var paramCode = (string)sample.Element("paramCode");
            var resType = (string)sample.Element("resType");
            var prodTreat = (string)sample.Element("prodTreat");
            var resVal = (string)sample.Element("resVal");
            var resEvaluation = (string)sample.Element("resEvaluation");
            var legalLimit = (string)sample.Element("resLegalLimit");

            var outcome = new Outcome
            {
                Name = "MRL_02",
                Lastupdate = "2017-04-28",
                Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is less than or equal to the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Greater than maximum permissible quantities' (J003A) and 'Compliant due to measurement uncertainty' (J031A);",
                Error = "result evaluation is incorrect; result value is equal or below the result legal limit;",
                Type = "error",
                Passed = true
            };

            //Logik            
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".", ",")) <= decimal.Parse(legalLimit.Replace(".", ",")))
            {
                outcome.Passed = (resEvaluation != "(J003A" && resEvaluation != "J031A");
            }
            return outcome;
        }


        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has changed during the year, then a value in the data element 'Result legal limit' (resLegalLimit) must be reported;
        [Rule(Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has changed during the year, then a value in the data element 'Result legal limit' (resLegalLimit) must be reported",
            ErrorMessage = "resLegalLimit is missing, though it is mandatory to be reported when the MRL changed during 2016",
            RuleType = "error")]
        public Outcome MRL_03(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var paramCode = (string)sample.Element("paramCode");
            var resType = (string)sample.Element("resType");
            var prodTreat = (string)sample.Element("prodTreat");
            var resLegalLimit = (string)sample.Element("resLegalLimit");

            var outcome = new Outcome
            {
                Name = "MRL_03",
                Lastupdate = "2017-04-28",
                Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has changed during the year, then a value in the data element 'Result legal limit' (resLegalLimit) must be reported;",
                Error = "resLegalLimit is missing, though it is mandatory to be reported when the MRL changed during 2016;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A"))
            {
                outcome.Passed = string.IsNullOrEmpty(resLegalLimit) == false;
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Not in list' (XXXXXXA), or the value in the data element 'Parameter code' (paramCode) is equal to 'Not in list' (RF-XXXX-XXX-XXX), then the validation of the matrix tool is not possible",
            ErrorMessage = "WARNING: validation not possible (paramCode or prodCode are reported as Not in list)",
            RuleType = "warning")]
        public Outcome MTX_W06(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode")?.Value;
            var paramCode = sample.Element("paramCode")?.Value;

            var outcome = new Outcome
            {
                Name = "MTX_W06",
                Lastupdate = "2017-04-18",
                Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Not in list' (XXXXXXA), or the value in the data element 'Parameter code' (paramCode) is equal to 'Not in list' (RF-XXXX-XXX-XXX), then the validation of the matrix tool is not possible;",
                Error = "WARNING: validation not possible (paramCode or prodCode are reported as Not in list);",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (prodCode == "XXXXXXA" || paramCode == "RF-XXXX-XXX-XXX")
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported;
        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported",
            ErrorMessage = "WARNING: when prodCode reported is honey and other apicultural products and the concerned sample is not honey at such (e.g. royal jelly, pollen, etc.), then prodText should be provided",
            RuleType = "warning")]
        public Outcome PEST01(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST01",
                Lastupdate = "2017-04-11",
                Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported;",
                Error = "WARNING: when prodCode reported is honey and other apicultural products and the concerned sample is not honey at such (e.g. royal jelly, pollen, etc.), then prodText should be provided;",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (prodCode == "P1040000A")
            {
                outcome.Passed = String.IsNullOrEmpty(prodText) == false;
            }

            return outcome;
        }

        ///The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A);
        [Rule(Description = "The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A)",
            ErrorMessage = "prodTreat is unknown",
            RuleType = "error")]
        public Outcome PEST02(XElement sample)
        {
            // <checkedDataElements>;
            //prodTreat;
            var prodtreat = (string)sample.Element("prodTreat");
            var outcome = new Outcome
            {
                Name = "PEST02",
                Description = "The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A);",
                Error = "prodTreat is unknown;",
                Type = "error",
                Passed = true
            };
            outcome.Passed = prodtreat != "T899A";
            return outcome;
        }

        ///The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);
        [Rule(Description = "The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A)",
            ErrorMessage = "WARNING: prodTreat is not among those recommended in EFSA guidance",
            RuleType = "warning")]
        public Outcome PEST03(XElement sample)
        {
            // <checkedDataElements>;
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome
            {
                Name = "PEST03",
                Lastupdate = "2017-04-11",
                Description = "The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);",
                Error = "WARNING: prodTreat is not among those recommended in EFSA guidance;",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (!string.IsNullOrEmpty(prodTreat))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var tillstand = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                tillstand.Add("T100A");
                tillstand.Add("T101A");
                tillstand.Add("T102A");
                tillstand.Add("T103A");
                tillstand.Add("T104A");
                tillstand.Add("T110A");
                tillstand.Add("T111A");
                tillstand.Add("T112A");
                tillstand.Add("T113A");
                tillstand.Add("T114A");
                tillstand.Add("T116A");
                tillstand.Add("T120A");
                tillstand.Add("T121A");
                tillstand.Add("T122A");
                tillstand.Add("T123A");
                tillstand.Add("T124A");
                tillstand.Add("T125A");
                tillstand.Add("T128A");
                tillstand.Add("T129A");
                tillstand.Add("T130A");
                tillstand.Add("T131A");
                tillstand.Add("T132A");
                tillstand.Add("T134A");
                tillstand.Add("T136A");
                tillstand.Add("T148A");
                tillstand.Add("T150A");
                tillstand.Add("T152A");
                tillstand.Add("T153A");
                tillstand.Add("T154A");
                tillstand.Add("T155A");
                tillstand.Add("T999A");
                tillstand.Add("T998A");

                outcome.Passed = tillstand.Contains(prodTreat);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);
        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A)",
            ErrorMessage = "prodTreat is not processed, though prodCode is a baby food",
            RuleType = "error")]
        public Outcome PEST04(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome
            {
                Name = "PEST04",
                Lastupdate = "2016-04-25",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);",
                Error = "prodTreat is not processed, though prodCode is a baby food;",
                Type = "error",
                Passed = true
            };

#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization

            prodCodes.Add("PX100000A");
            prodCodes.Add("PX100001A");
            prodCodes.Add("PX100003A");
            prodCodes.Add("PX100004A");
            prodCodes.Add("PX100005A");

            if (prodCodes.Contains(prodCode))
            {
                outcome.Passed = prodTreat == "T100A";
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Milk pasteurisation' (T150A), or 'Freezing' (T998A), or 'Concentration' (T136A), or 'Unprocessed' (T999A);
        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Milk pasteurisation' (T150A), or 'Freezing' (T998A), or 'Concentration' (T136A), or 'Unprocessed' (T999A)",
            ErrorMessage = "prodTreat is not dehydration, churning, milk pasteurisation, freezing, concentration or unprocessed, though prodCode is milk of animal origin",
            RuleType = "error")]
        public Outcome PEST05(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.Values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.Values.Add(Tuple.Create("prodTreat", (string)sample.Element("prodTreat")));
            outcome.Name = "PEST05";
            outcome.Lastupdate = "2017-04-11";
            outcome.Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Milk pasteurisation' (T150A), or 'Freezing' (T998A), or 'Concentration' (T136A), or 'Unprocessed' (T999A);";
            outcome.Error = "prodTreat is not dehydration, churning, milk pasteurisation, freezing, concentration or unprocessed, though prodCode is milk of animal origin;";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodCodes.Add("P1020000A");
            prodCodes.Add("P1020010A");
            prodCodes.Add("P1020020A");
            prodCodes.Add("P1020030A");
            prodCodes.Add("P1020040A");
            prodCodes.Add("P1020990A");
            if (prodCodes.Contains(prodCode))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T131A");
                prodTreats.Add("T134A");
                prodTreats.Add("T136A");
                prodTreats.Add("T150A");
                prodTreats.Add("T152A");
                prodTreats.Add("T153A");
                prodTreats.Add("T154A");
                prodTreats.Add("T155A");
                prodTreats.Add("T998A");
                prodTreats.Add("T999A");
                outcome.Passed = prodTreats.Contains(prodTreat);

            }
            return outcome;
        }
        ///If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), or 'Churning' (T134A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), or 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or 'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);
        [Rule(Description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), or 'Churning' (T134A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), or 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or 'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A)",
            ErrorMessage = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation",
            RuleType = "error")]
        public Outcome PEST06(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");
            var outcome = new Outcome
            {
                Name = "PEST06",
                Lastupdate = "2017-07-04",
                Description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), or 'Churning' (T134A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), or 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or 'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);",
                Error = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation;",
                Type = "error",
                Passed = true
            };

            //Logik
#pragma warning disable IDE0028 // Simplify collection initialization
            var prodtreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodtreats.Add("T150A");
            prodtreats.Add("T134A");
            prodtreats.Add("T152A");
            prodtreats.Add("T153A");
            prodtreats.Add("T154A");
            prodtreats.Add("T155A");

            if (prodtreats.Contains(prodTreat))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var produktkoder = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                produktkoder.Add("P1020000A");
                produktkoder.Add("P1020010A");
                produktkoder.Add("P1020020A");
                produktkoder.Add("P1020030A");
                produktkoder.Add("P1020040A");
                produktkoder.Add("P1020990A");

                outcome.Passed = produktkoder.Contains(prodCode);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A);
        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A)",
            ErrorMessage = "prodTreat is dehydration, though this value is not allowed when prodCode belongs to the 'Pulses (dry seeds)' food group",
            RuleType = "error")]
        public Outcome PEST07(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome
            {
                Name = "PEST07",
                Lastupdate = "2017-04-11",
                Description = "If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A);",
                Error = "prodTreat is dehydration, though this value is not allowed when prodCode belongs to the 'Pulses (dry seeds)' food group;",
                Type = "error",
                Passed = true
            };

            //Logik
#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodCodes.Add("P0300000A");
            prodCodes.Add("P0300010A");
            prodCodes.Add("P0300020A");
            prodCodes.Add("P0300030A");
            prodCodes.Add("P0300040A");
            prodCodes.Add("P0300990A");
            if (prodCodes.Contains(prodCode))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T131A");
                if (prodTreats.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        [Rule(Description = "If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A)",
            ErrorMessage = "prodCode is not a baby food, though progLegalRef is samples of food products falling under Directive 2006/125/EC or 2006/141/EC",
            RuleType = "error")]
        public Outcome PEST08(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = (string)sample.Element("progLegalRef");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome
            {
                Name = "PEST08",
                Lastupdate = "2016-04-06",
                Description = "If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);",
                Error = "prodCode is not a baby food, though progLegalRef is samples of food products falling under Directive 2006/125/EC or 2006/141/EC;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progLegalRef == "N028A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("PX100000A");
                prodCodes.Add("PX100001A");
                prodCodes.Add("PX100003A");
                prodCodes.Add("PX100004A");
                prodCodes.Add("PX100005A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);
        [Rule(Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A)",
            ErrorMessage = "progLegalRef is not samples of food products falling under Directive 2006/125/EC or 2006/141/EC, though prodCode is a baby food",
            RuleType = "error")]
        public Outcome PEST09(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = (string)sample.Element("progLegalRef");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome
            {
                Name = "PEST09"
            };
            outcome.Values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.Values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.Lastupdate = "2016-04-06";
            outcome.Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);";
            outcome.Error = "progLegalRef is not samples of food products falling under Directive 2006/125/EC or 2006/141/EC, though prodCode is a baby food;";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik

#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodCodes.Add("PX100000A");
            prodCodes.Add("PX100001A");
            prodCodes.Add("PX100003A");
            prodCodes.Add("PX100004A");
            prodCodes.Add("PX100005A");
            if (prodCodes.Contains(prodCode))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var progLegalRefs = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                progLegalRefs.Add("N028A");
                if (!progLegalRefs.Contains(progLegalRef))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);
        [Rule(Description = "The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A)",
            ErrorMessage = "labAccred is not accredited or none",
            RuleType = "error")]
        public Outcome PEST10(XElement sample)
        {
            // <checkedDataElements>;
            var labAccred = (string)sample.Element("labAccred");

            var outcome = new Outcome
            {
                Name = "PEST10",
                Lastupdate = "2016-04-25",
                Description = "The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);",
                Error = "labAccred is not accredited or none;",
                Type = "error",
                Passed = true
            };

            //Logik
#pragma warning disable IDE0028 // Simplify collection initialization
            var labAccreds = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            labAccreds.Add("L001A");
            labAccreds.Add("L003A");
            if (!labAccreds.Contains(labAccred))
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        ///The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);
        [Rule(Description = "The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A)",
            ErrorMessage = "resUnit is not reported in milligram per kilogram",
            RuleType = "error")]
        public Outcome PEST11(XElement sample)
        {
            // <checkedDataElements>;
            var resUnit = (string)sample.Element("resUnit");

            var outcome = new Outcome
            {
                Name = "PEST11",
                Lastupdate = "2016-04-25",
                Description = "The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);",
                Error = "resUnit is not reported in milligram per kilogram;",
                Type = "error",
                Passed = true
            };

            //Logik

#pragma warning disable IDE0028 // Simplify collection initialization
            var resUnits = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            resUnits.Add("G061A");
            if (!resUnits.Contains(resUnit))
            {
                outcome.Passed = false;
            }

            return outcome;
        }

        ///The value in the data element (exprRes) can only be equal to 'Whole weight' (B001A), or 'Fat basis' (B003A), or 'Reconstituted product' (B007A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST12(XElement sample)
        {
            // <checkedDataElements>;
            var exprRes = (string)sample.Element("exprRes");

            var outcome = new Outcome
            {
                Name = "PEST12",
                Lastupdate = "2016-04-25",
                Description = "The value in the data element (exprRes) can only be equal to 'Whole weight' (B001A), or 'Fat basis' (B003A), or 'Reconstituted product' (B007A);",
                Error = "exprRes is not whole weight, or fat basis, or reconstituted product;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (!string.IsNullOrEmpty(exprRes))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var exprRess = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                exprRess.Add("B001A");
                exprRess.Add("B003A");
                exprRess.Add("B007A");
                if (!exprRess.Contains(exprRes))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }
        ///If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST13(XElement sample)
        {
            // <checkedDataElements>;
            var exprRes = (string)sample.Element("exprRes");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome
            {
                Name = "PEST13",
                Lastupdate = "2016-04-06",
                Description = "If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);",
                Error = "WARNING: prodCode is not a baby food, though exprRes is reconstituted product;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (exprRes == "B007A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("PX100000A");
                prodCodes.Add("PX100001A");
                prodCodes.Add("PX100003A");
                prodCodes.Add("PX100004A");
                prodCodes.Add("PX100005A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }
        ///If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST14(XElement sample)
        {
            // <checkedDataElements>;
            var exprRes = (string)sample.Element("exprRes");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome
            {
                Name = "PEST14",
                Lastupdate = "2016-04-06",
                Description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);",
                Error = "prodCode is not milk of animal origin or egg samples, though exprRes is fat weight;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (exprRes == "B003A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("P1020000A");
                prodCodes.Add("P1020010A");
                prodCodes.Add("P1020020A");
                prodCodes.Add("P1020030A");
                prodCodes.Add("P1020040A");
                prodCodes.Add("P1020990A");
                prodCodes.Add("P1030000A");
                prodCodes.Add("P1030010A");
                prodCodes.Add("P1030020A");
                prodCodes.Add("P1030030A");
                prodCodes.Add("P1030040A");
                prodCodes.Add("P1030990A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST15(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var exprRes = (string)sample.Element("exprRes");
            var fatPerc = (string)sample.Element("fatPerc");

            var outcome = new Outcome
            {
                Name = "PEST15",
                Lastupdate = "2016-04-25",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;",
                Error = "WARNING: fat percentage in milk of animal origin on whole weight basis is not reported; EFSA will assume a fat content equal to 4%;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: no);
#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodCodes.Add("P1020000A");
            prodCodes.Add("P1020010A");
            prodCodes.Add("P1020020A");
            prodCodes.Add("P1020030A");
            prodCodes.Add("P1020040A");
            prodCodes.Add("P1020990A");
            if (prodCodes.Contains(prodCode) && exprRes == "B001A")
            {
                outcome.Passed = String.IsNullOrEmpty(fatPerc) == false;

            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST16(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var exprRes = (string)sample.Element("exprRes");
            var fatPerc = (string)sample.Element("fatPerc");

            var outcome = new Outcome
            {
                Name = "PEST16",
                Lastupdate = "2016-04-25",
                Description = "If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;",
                Error = "WARNING: fat percentage in egg samples on whole weight basis is not reported; EFSA will assume a fat content equal to 10%;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: no);
#pragma warning disable IDE0028 // Simplify collection initialization
            var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            prodCodes.Add("P1030000A");
            prodCodes.Add("P1030010A");
            prodCodes.Add("P1030020A");
            prodCodes.Add("P1030030A");
            prodCodes.Add("P1030040A");
            prodCodes.Add("P1030990A");
            if (prodCodes.Contains(prodCode) && exprRes == "B001A")
            {
                outcome.Passed = String.IsNullOrEmpty(fatPerc) == false; ;
            }
            return outcome;
        }

        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST17(XElement sample)
        {
            // <checkedDataElements>;
            var resEvaluation = (string)sample.Element("resEvaluation");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();

            outcome.Values.Add(Tuple.Create("resEvaluation", (string)sample.Element("resEvaluation")));
            outcome.Values.Add(Tuple.Create("resType", (string)sample.Element("resType")));

            outcome.Name = "PEST17";
            outcome.Lastupdate = "2016-04-25";
            outcome.Description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';";
            outcome.Error = "resType is different from VAL, though resEvaluation is 'greater than maximum permissible quantities' or 'compliant due to measurement uncertainty';";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik (ignore null: yes);
#pragma warning disable IDE0028 // Simplify collection initialization
            var resEvaluations = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            resEvaluations.Add("J003A");
            resEvaluations.Add("J031A");
            if (resEvaluations.Contains(resEvaluation))
            {
                outcome.Passed = resType == "VAL";
            }
            return outcome;
        }

        ///The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST18(XElement sample)
        {
            // <checkedDataElements>;
            var resLegalLimitType = (string)sample.Element("resLegalLimitType");

            var outcome = new Outcome();
            outcome.Values.Add(Tuple.Create("resLegalLimitType", (string)sample.Element("resLegalLimitType")));
            outcome.Name = "PEST18";
            outcome.Lastupdate = "2016-04-25";
            outcome.Description = "The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);";
            outcome.Error = "WARNING: resLegalLimitType is different from MRL and national or local limit;";
            outcome.Type = "warning";
            outcome.Passed = true;

            //Logik (ignore null: yes);
            if (!string.IsNullOrEmpty(resLegalLimitType))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var resLegalLimitTypes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                resLegalLimitTypes.Add("W002A");
                resLegalLimitTypes.Add("W990A");
                if (!resLegalLimitTypes.Contains(resLegalLimitType))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }
        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST19(XElement sample)
        {
            // <checkedDataElements>;
            var resEvaluation = (string)sample.Element("resEvaluation");
            var resVal = (string)sample.Element("resVal");
            var resLegalLimit = (string)sample.Element("resLegalLimit");

            var outcome = new Outcome
            {
                Name = "PEST19",
                Lastupdate = "2016-04-25",
                Description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);",
                Error = "resVal is less than or equal to resLegalLimit, though resEvaluation is 'greater than maximum permissible quantities', or 'compliant due to measurement uncertainty';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
#pragma warning disable IDE0028 // Simplify collection initialization
            var resEvaluations = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            resEvaluations.Add("J003A");
            resEvaluations.Add("J031A");
            if (resEvaluations.Contains(resEvaluation))
            {
                outcome.Passed = PD(resVal) > PD(resLegalLimit);
            }

            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST20(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome
            {
                Name = "PEST20",
                Lastupdate = "2016-04-06",
                Description = "If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;",
                Error = "resVal is reported, though resType is qualitative value (binary);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "BIN")
            {
                outcome.Passed = string.IsNullOrEmpty(resVal);
            }

            return outcome;
        }
        ///If the value in the data element 'Result type' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then the value in the data element 'Result value' (resVal) should not be greater than the value in the data element 'Result LOQ' (resLOQ);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST21(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resLOQ = (string)sample.Element("resLOQ");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome();
            outcome.Values.Add(Tuple.Create("resType", (string)sample.Element("resType")));
            outcome.Values.Add(Tuple.Create("resLOQ", (string)sample.Element("resLOQ")));
            outcome.Values.Add(Tuple.Create("resVal", (string)sample.Element("resVal")));
            outcome.Name = "PEST21";
            outcome.Lastupdate = "2016-07-15";
            outcome.Description = "If the value in the data element 'Result type' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then the value in the data element 'Result value' (resVal) should not be greater than the value in the data element 'Result LOQ' (resLOQ);";
            outcome.Error = "WARNING: resType is LOQ for a result that contains a value greater than the reported LOQ;";
            outcome.Type = "warning";
            outcome.Passed = true;

            //Logik (ignore null: yes);
            if (resType == "LOQ")
            {
                if (!String.IsNullOrEmpty(resVal))
                {
                    outcome.Passed = PD(resLOQ) > PD(resVal);
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical Value' (VAL), then the value in the data element 'Result LOQ' (resLOQ) should not be greater than the value in the data element 'Result value' (resVal) (if the result is a positive detection, the result value cannot be below the reported LOQ);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST22(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resLOQ = (string)sample.Element("resLOQ");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome
            {
                Name = "PEST22",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical Value' (VAL), then the value in the data element 'Result LOQ' (resLOQ) should not be greater than the value in the data element 'Result value' (resVal) (if the result is a positive detection, the result value cannot be below the reported LOQ);",
                Error = "WARNING: resType is VAL for a result that contains a value less than the reported LOQ;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (resType == "VAL")
            {
                outcome.Passed = PD(resVal) >= PD(resLOQ);
            }

            return outcome;
        }
        ///If the value in the data element 'Type of parameter' (paramType) is different from 'Part of a sum' (P002A) and the value in the data element 'Result value' (resVal) is greater than or equal to the value in the data element 'Legal Limit for the result' (resLegalLimit), then the value in the data element 'Evaluation of the result' (resEvaluation) should be different from 'Result not evaluated' (J029A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST23(XElement sample)
        {
            // <checkedDataElements>;
            var paramType = (string)sample.Element("paramType");
            var resVal = (string)sample.Element("resVal");
            var resLegalLimit = (string)sample.Element("resLegalLimit");
            var resEvaluation = (string)sample.Element("resEvaluation");

            var outcome = new Outcome
            {
                Name = "PEST23",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Type of parameter' (paramType) is different from 'Part of a sum' (P002A) and the value in the data element 'Result value' (resVal) is greater than or equal to the value in the data element 'Legal Limit for the result' (resLegalLimit), then the value in the data element 'Evaluation of the result' (resEvaluation) should be different from 'Result not evaluated' (J029A);",
                Error = "WARNING: where resVal greater than or equal to resLegalLimit, then the resEvaluation is not expected to be not evaluated;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resVal))
            {
                if (paramType != "P002A" && PD(resVal) > PD(resLegalLimit))
                {
                    outcome.Passed = resEvaluation != "J029A";

                }
            }
            return outcome;
        }
        ///If the value in the data element 'Result value recovery corrected' (resValRecCorr) is equal to 'Yes' (Y), then a value in the data element 'Result value recovery' (resValRec) should be reported;
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "warning")]
        public Outcome PEST24(XElement sample)
        {
            // <checkedDataElements>;
            var resValRecCorr = (string)sample.Element("resValRecCorr");
            var resValRec = (string)sample.Element("resValRec");

            var outcome = new Outcome
            {
                Name = "PEST24",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Result value recovery corrected' (resValRecCorr) is equal to 'Yes' (Y), then a value in the data element 'Result value recovery' (resValRec) should be reported;",
                Error = "WARNING: resValRec is missing, though resValRecCorr is reported; if the result is corrected for recovery the corrected value should be reported (mean recovery out of 70-120%);",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resValRecCorr == "Y")
            {
                outcome.Passed = string.IsNullOrEmpty(resValRecCorr) == false;
            }

            return outcome;
        }

        ///The value in the data element 'Sampling year' (sampY) should be equal to 2016;
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST25(XElement sample)
        {
            // <checkedDataElements>;
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome
            {
                Name = "PEST25",
                Lastupdate = "2017-04-11",
                Description = "The value in the data element 'Sampling year' (sampY) should be equal to " + _yearToTest + ";",
                Error = "sampY is different from " + _yearToTest + ";",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            outcome.Passed = sampY == _yearToTest;
            return outcome;
        }

        ///The value in the data element 'Parameter code' (paramCode) should be different from 'Not in list' (RF-XXXX-XXX-XXX);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST26(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string)sample.Element("paramCode");

            var outcome = new Outcome();
            outcome.Values.Add(Tuple.Create("paramCode", (string)sample.Element("paramCode")));
            outcome.Name = "PEST26";
            outcome.Lastupdate = "2017-04-17";
            outcome.Description = "The value in the data element 'Parameter code' (paramCode) should be different from 'Not in list' (RF-XXXX-XXX-XXX);";
            outcome.Error = "paramCode should be different from 'not in list'. Please contact catalogue@efsa.europa.eu to add the missing term;";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
            if (paramCode == "RF-XXXX-XXX-XXX")
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST_sampInfo005(XElement sample)
        {
            var progType = sample.Element("progType")?.Value;
            var progLegalRef = sample.Element("progLegalRef")?.Value;
            var progSampStrategy = sample.Element("progSampStrategy")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST_sampInfo005"
            };
            outcome.Values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.Values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.Values.Add(Tuple.Create("progSampStrategy", (string)sample.Element("progSampStrategy")));
            outcome.Lastupdate = "2016-07-15";
            outcome.Description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (National) programme' (K005A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A), or 'Suspect sampling' (ST30A);";
            outcome.Error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
            if (progType == "K005A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add("N027A");
                list.Add("N028A");
                list.Add("N247A");
                list.Add("N018A");

#pragma warning disable IDE0028 // Simplify collection initialization
                var list2 = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list2.Add("ST10A");
                list2.Add("ST20A");
                list2.Add("ST30A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.Passed = false;
                }


                if (!list2.Contains(progSampStrategy))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'Official (EU) programme' (K009A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST_sampInfo009(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var progLegalRef = sample.Element("progLegalRef")?.Value;
            var progSampStrategy = sample.Element("progSampStrategy")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST_sampInfo009"
            };
            outcome.Values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.Values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.Values.Add(Tuple.Create("progSampStrategy", (string)sample.Element("progSampStrategy")));
            outcome.Description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (EU) programme' (K009A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);";
            outcome.Error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
            if (progType == "K009A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add("N027A");
                list.Add("N028A");

#pragma warning disable IDE0028 // Simplify collection initialization
                var list2 = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list2.Add("ST10A");
                list2.Add("ST20A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.Passed = false;
                }
                if (!list2.Contains(progSampStrategy))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }


        ///If the value in the data element 'Programme type' (progType) is equal to 'Official (National and EU) programme' (K018A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST_sampInfo018(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var progLegalRef = sample.Element("progLegalRef")?.Value;
            var progSampStrategy = sample.Element("progSampStrategy")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST_sampInfo018",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (National and EU) programme' (K018A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);",
                Error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;",
                Type = "error",
                Passed = true
            };

            if (progType == "K018A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add("N027A");
                list.Add("N028A");
                list.Add("N247A");
                list.Add("N018A");

                var list2 = new List<string>();
                list.Add("ST10A");
                list.Add("ST20A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.Passed = false;
                }
                if (list2.Contains(progSampStrategy))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Suspect sampling' (ST30A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST_sampInfo019(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var progLegalRef = sample.Element("progLegalRef")?.Value;
            var progSampStrategy = sample.Element("progSampStrategy")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST_sampInfo019",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Suspect sampling' (ST30A);",
                Error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add("N027A");

                var list2 = new List<string>();
                list.Add("ST30A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.Passed = false;
                }
                if (list2.Contains(progSampStrategy))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in (origCountry) can only be equal to 'China' (CN), or 'Dominican Republic' (DO), or 'Egypt' (EG), or 'Kenya' (KE), or 'Cambodia' (KH), or 'Thailand' (TH), or 'Turkey' (TR), or 'Viet Nam' (VN);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_1(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;

            var outcome = new Outcome();
            outcome.Values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.Values.Add(Tuple.Create("origCountry", (string)sample.Element("origCountry")));
            outcome.Lastupdate = "2017-06-16";
            outcome.Name = "PEST669_1";
            outcome.Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in (origCountry) can only be equal to 'China' (CN), or 'Dominican Republic' (DO), or 'Egypt' (EG), or 'Kenya' (KE), or 'Cambodia' (KH), or 'Thailand' (TH), or 'Turkey' (TR), or 'Viet Nam' (VN);";
            outcome.Error = "origCountry is not a valid country code when progType reported is EU increased control programme on imported food (Reg 669/2009);";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
            if (progType == "K019A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add("CN");
                list.Add("DO");
                list.Add("EG");
                list.Add("KE");
                list.Add("KH");
                list.Add("TH");
                list.Add("TR");
                list.Add("VN");
                list.Add("IN");

                if (!list.Contains(origCountry))
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }
        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'China' (CN), then the value in 'Product code' (prodCode) can only be equal to 'Broccoli' (P0241010A), or 'Teas' (P0610000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_CN(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_CN",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'China' (CN), then the value in 'Product code' (prodCode) can only be equal to 'Broccoli' (P0241010A), or 'Teas' (P0610000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true,
                Lastupdate = "2017-03-15"
            };

            //Logik
            if (progType == "K019A" && origCountry == "CN")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var produktkoder = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                produktkoder.Add("P0241010A");
                produktkoder.Add("P0610000A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
                if (prodTreat != "T999A")
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Dominican Republic' (DO), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants'  (P0231030A), or 'Courgettes' (P0232030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_DO(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_DO",
                Lastupdate = "2017-03-15"
            };
            outcome.Values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.Values.Add(Tuple.Create("origCountry", (string)sample.Element("origCountry")));
            outcome.Values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.Values.Add(Tuple.Create("prodTreat", (string)sample.Element("prodTreat")));
            outcome.Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Dominican Republic' (DO), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants'  (P0231030A), or 'Courgettes' (P0232030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.Type = "error";
            outcome.Passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "DO")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var produktkoder = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                produktkoder.Add("P0231030A");
                produktkoder.Add("P0232030A");
                produktkoder.Add("P0231020A");
                produktkoder.Add("P0260010A");

#pragma warning disable IDE0028 // Simplify collection initialization
                var tillstand = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                tillstand.Add("T999A");
                tillstand.Add("T998A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.Passed = false;
                }

                if (!tillstand.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }

            }
            return outcome;
        }

        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_DO_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_DO_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Dominican Republic' (DO), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "DO" && prodCode == "P0231020A")
            {
                outcome.Passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Strawberries'  (P0152000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_EG(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_EG",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Strawberries'  (P0152000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "EG")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var produktkoder = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                produktkoder.Add("P0231020A");
                produktkoder.Add("P0152000A");

#pragma warning disable IDE0028 // Simplify collection initialization
                var tillstand = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                tillstand.Add("T999A");
                tillstand.Add("T998A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.Passed = false;
                }

                if (tillstand.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }

            }

            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Kenya' (KE), then the value in 'Product code' (prodCode) can only be equal to 'Peas (with pods)' (P0260030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_KE(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_KE",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Kenya' (KE), then the value in 'Product code' (prodCode) can only be equal to 'Peas (with pods)' (P0260030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "KE")
            {
                var produktkoder = new List<string>();
#pragma warning disable IDE0028 // Simplify collection initialization
                var tillstand = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                tillstand.Add("T999A");
                produktkoder.Add("P0260030A");
                if (!produktkoder.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
                if (tillstand.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }
        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_EG_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_EG_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "EG" && prodCode == "P0231020A")
            {
                outcome.Passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
            }
            return outcome;
        }


        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Celery leaves' (P0256030A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_KH(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_KH",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Celery leaves' (P0256030A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "KH")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("P0231030A");
                prodCodes.Add("P0260010A");
                prodCodes.Add(" P0256030A");

                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Chinese celery leaves';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_KH_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_KH_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Chinese celery leaves';",
                Error = "prodText doesn't contain the appropriate string when prodCode is celery leaves and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "KH" && prodCode == "P0256030A")
            {
                outcome.Passed = prodText.Contains("Chinese celery leaves");
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_TH(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_TH",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "TH")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("P0231030A");
                prodCodes.Add("P0260010A");
                prodCodes.Add("P0231020A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }

            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Chili peppers';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_TH_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_TH_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Chili peppers';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "TH" && prodCode == "P0231020A")
            {
                outcome.Passed = prodText.Contains("Chili peppers");
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Grape leaves and similar species' (P0253000A), or 'Lemons' (P0110030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), or 'Processed' (T100A), or 'Dehydration' (T131A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_TR(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_TR",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Grape leaves and similar species' (P0253000A), or 'Lemons' (P0110030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), or 'Processed' (T100A), or 'Dehydration' (T131A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "TR")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("P0231020A");
                prodCodes.Add("P0253000A");
                prodCodes.Add("P0110030A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                prodTreats.Add("T100A");
                prodTreats.Add("T131A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_TR_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_TR_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "TR" && prodCode == "P0231020A")
            {
                outcome.Passed = prodText.Contains("Sweet/bell peppers");
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), then the value in 'Product code' (prodCode) can only be equal to 'Basil and edible flowers' (P0256080A), or 'Celery leaves'  (P0256030A), or 'Prickly pears/cactus fruits' (P0163040A), or 'Okra/lady’s fingers' (P0231040A), or 'Parsley' (P0256040A), or 'Sweet peppers/bell peppers' (P0231020A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_VN(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodTreat = sample.Element("prodTreat")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_VN",
                Lastupdate = "2016-07-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), then the value in 'Product code' (prodCode) can only be equal to 'Basil and edible flowers' (P0256080A), or 'Celery leaves'  (P0256030A), or 'Prickly pears/cactus fruits' (P0163040A), or 'Okra/lady’s fingers' (P0231040A), or 'Parsley' (P0256040A), or 'Sweet peppers/bell peppers' (P0231020A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);",
                Error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "VN")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodCodes = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodCodes.Add("P0256080A");
                prodCodes.Add("P0256030A");
                prodCodes.Add("P0163040A");
                prodCodes.Add("P0256080A");
                prodCodes.Add("P0231040A");
                prodCodes.Add("P0256040A");
                prodCodes.Add("P0231020A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.Passed = false;
                }
#pragma warning disable IDE0028 // Simplify collection initialization
                var prodTreats = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                prodTreats.Add("T999A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.Passed = false;

                }
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Basil and edible flowers' (P0256080A), then the value in 'Product text' (prodText) mus contain the string 'Basil' or 'Mint';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_VN_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_VN_a",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Basil and edible flowers' (P0256080A), then the value in 'Product text' (prodText) mus contain the string 'Basil' or 'Mint';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0256080A")
            {
                outcome.Passed = (prodText.Contains("Basil") || prodText.Contains("Mint"));

            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Coriander leaves';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_VN_b(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_VN_b",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Coriander leaves';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0256030A")
            {
                outcome.Passed = (prodText.Contains("Coriander leaves"));
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';
        [Rule(Description = "",
            ErrorMessage = "",
            RuleType = "error")]
        public Outcome PEST669_VN_c(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType")?.Value;
            var origCountry = sample.Element("origCountry")?.Value;
            var prodCode = sample.Element("prodCode")?.Value;
            var prodText = sample.Element("prodText")?.Value;

            var outcome = new Outcome
            {
                Name = "PEST669_VN_c",
                Lastupdate = "2017-03-15",
                Description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';",
                Error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0231020A")
            {
                outcome.Passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
            }
            return outcome;
        }
        #endregion


        public string GetCountryFromAreaCode(string code)
        {

            XDocument xDoc = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("Nuts_rule_check"));
            var country = xDoc.Descendants("area").Where(x => x.Attribute("code").Value == code).Select(x => x.Attribute("country").Value).FirstOrDefault();
            return country;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal? ParseDec(string s)
        {
            s = s.Replace(',', '.');
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmp) ? tmp : default(decimal?);
        }

        /// <summary>
        /// UniqueValues
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private bool UniqueValues(List<XElement> elements)
        {
            List<string> list = new List<string>();
            foreach (var e in elements)
            {
                list.Add((string)e);
            }
            //Om sann (count överstiger 1) returnera false)
            return list.GroupBy(l => l).Any(l => l.Count() > 1) == false;
        }

        /// <summary>
        /// Parse Decimal
        /// </summary>
        /// <param name="s"></param>
        private decimal? PD(string s)
        {
            if (s == null)
            {
                return null;
            }
            decimal.TryParse(s.Replace(".", ","), out decimal r);
            return r;
        }
    }

}
