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
    public class GeneralBusinessRules
    {
        private string _yearToTest;

        /// <summary>
        /// Constructor for <see cref="GeneralBusinessRules"/>
        /// </summary>
        /// <param name="yearToTest">The year that the rules to test against, correct format is YYYY</param>
        public GeneralBusinessRules(string yearToTest)
        {
            _yearToTest = yearToTest;
        }

        void Main()
        {

        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique;",
            ErrorMessage = "The combination of values in paramCode, labSampCode and labSubSampCode is not unique", RuleType = "error", Deprecated = false)]
        public Outcome BR01A(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR01A",
                Description = "If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique",
                Error = "The combination of values in paramCode, labSampCode and labSubSampCode is not unique",
                Type = "error",
                Lastupdate = "",
            };

            if (sample.Element("paramCode")?.Value != "RF-XXXX-XXX-XXX")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var list = new List<XElement>();
#pragma warning restore IDE0028 // Simplify collection initialization
                list.Add(sample.Element("paramCode"));
                list.Add(sample.Element("labSampCode"));
                list.Add(sample.Element("labSubSampCode"));
                outcome.Passed = UniqueValues(list); //Alla värden är unika;
            }
            else
            {
                outcome.Passed = true;
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;",
            ErrorMessage = "analysisM is missing, though analysisD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_01(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_01",
                Description = "If the value in 'Day of analysis' (analysisD) is reported, then a value in 'Month of analysis' (analysisM) must be reported;",
                Error = "analysisM is missing, though analysisD is reported;",
                Type = "error",
                Lastupdate = "",
            };

            //Element att kontrollera
#pragma warning disable IDE0028 // Simplify collection initialization
            var elementAttKontrollera = new List<XElement>();
#pragma warning restore IDE0028 // Simplify collection initialization
            elementAttKontrollera.Add(sample.Element("analysisD"));
            elementAttKontrollera.Add(sample.Element("analysisM"));

            //Logik
            if (sample.Element("analysisD") != null)
            {
                outcome.Passed = (sample.Element("analysisM") != null);
            }
            else
            {
                outcome.Passed = true;
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;",
            ErrorMessage = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, resLegalLimit) is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_02(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_02",
                Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;",
                Error = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, resLegalLimit) is reported;",
                Type = "error",
                Lastupdate = "",
            };

            //Element att kontrollera
#pragma warning disable IDE0028 // Simplify collection initialization
            var elementAttKontrollera = new List<XElement>();
#pragma warning restore IDE0028 // Simplify collection initialization
            elementAttKontrollera.Add(sample.Element("resUnit"));
            elementAttKontrollera.Add(sample.Element("resLOD"));
            elementAttKontrollera.Add(sample.Element("resLOQ"));
            elementAttKontrollera.Add(sample.Element("CCalpha"));
            elementAttKontrollera.Add(sample.Element("CCbeta"));
            elementAttKontrollera.Add(sample.Element("resVal"));
            elementAttKontrollera.Add(sample.Element("resValUncert"));
            elementAttKontrollera.Add(sample.Element("resValUncertSD"));
            elementAttKontrollera.Add(sample.Element("resLegalLimit"));
            //Logik

            if (elementAttKontrollera.Any(x => x != null))
            {
                outcome.Passed = sample.Element("resUnit") != null && sample.Element("resUnit")?.Value != null;
            }
            else
            {
                outcome.Passed = true;
            }

            return outcome;
        }

        ///If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;
        [Rule(Description = "If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;",
            ErrorMessage = "prodM is missing, though prodD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_03(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_03",
                Description = "If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;",
                Error = "prodM is missing, though prodD is reported;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            var prodD = sample.Element("prodD");
            if (prodD != null)
            {
                //Verify
                var prodM = sample.Element("prodM");
                if (prodM == null)
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        [Rule(Description = "If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;",
            ErrorMessage = "expiryM is missing, though expiryD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_04(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_04",
                Description = "If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;",
                Error = "expiryM is missing, though expiryD is reported;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            var expiryD = sample.Element("expiryD");
            if (expiryD != null)
            {
                //Verify
                var expiryM = sample.Element("expiryM");
                if (expiryM == null)
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        [Rule(Description = "If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;",
            ErrorMessage = "sampM is missing, though sampD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_05(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_05",
                Description = "If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;",
                Error = "sampM is missing, though sampD is reported;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            var sampD = sample.Element("sampD");
            if (sampD != null)
            {
                //Verify
                var sampM = sample.Element("sampM");
                if (sampM == null)
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        [Rule(Description = "If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;",
            ErrorMessage = "lotSizeUnit is missing, though lotSize is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR02A_06(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_06",
                Description = "If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;",
                Error = "lotSizeUnit is missing, though lotSize is reported;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            var lotSize = sample.Element("lotSize");
            if (lotSize != null)
            {
                //Verify
                var lotSizeUnit = sample.Element("lotSizeUnit");
                if (lotSizeUnit == null)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;",
            ErrorMessage = "WARNING: resLegalLimitType is missing, though resLegalLimit is reported;", RuleType = "warning", Deprecated = false)]
        public Outcome BR02A_07(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_07",
                Description = "If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;",
                Error = "WARNING: resLegalLimitType is missing, though resLegalLimit is reported;",
                Passed = true,
                Type = "warning",
                Lastupdate = "",
            };

            //Villkor
            var resLegalLimit = sample.Element("resLegalLimit");
            if (resLegalLimit != null)
            {
                //Verify
                var resLegalLimitType = sample.Element("resLegalLimitType");
                if (resLegalLimitType == null)
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;",
            ErrorMessage = "analysisY is greater than the current year;", RuleType = "error", Deprecated = false)]
        public Outcome BR03A_01(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_01",
                Description = "The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;",
                Error = "analysisY is greater than the current year;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            var analysisY = sample.Element("analysisY")?.Value;
            if (int.Parse(analysisY) <= int.Parse(_yearToTest))
            {
                //Condition is true
                outcome.Passed = true;
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);",
            ErrorMessage = "resLOD is greater than resLOQ;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_02(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_02",
                Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);",
                Error = "resLOD is greater than resLOQ;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("resLOD") == null || sample.Element("resLOQ") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOD")?.Value.Replace(".", ",")) > decimal.Parse(sample.Element("resLOQ")?.Value.Replace(".", ",")))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);",
            ErrorMessage = "CCalpha is greater than CCbeta;", RuleType = "error", Deprecated = false)]
        public Outcome BR03A_03(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_03",
                Description = "The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);",
                Error = "CCalpha is greater than CCbeta;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik

            if (sample.Element("CCalpha") == null || sample.Element("CCbeta") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha")?.Value) > decimal.Parse(sample.Element("CCbeta")?.Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result value recovery' (resValRec) must be greater than 0;",
            ErrorMessage = "resValRec is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_04(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_04",
                Description = "The value in 'Result value recovery' (resValRec) must be greater than 0;",
                Error = "resValRec is less than or equal to 0;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("resValRec") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValRec")?.Value.Replace(".", ",")) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of production' (prodY) must be less than or equal to the current year;",
            ErrorMessage = "prodY is greater than the current year;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_05(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_05",
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the current year;",
                Error = "prodY is greater than the current year;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("prodY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY")?.Value) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);",
            ErrorMessage = "prodY is greater than expiryY;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_06(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_06",
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);",
                Error = "prodY is greater than expiryY;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("prodY") == null || sample.Element("expiryY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("expiryY").Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);",
            ErrorMessage = "prodY is greater than sampY;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_07(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_07",
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);",
                Error = "prodY is greater than sampY;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("prodY") == null || sample.Element("sampY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("sampY").Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
            ErrorMessage = "prodY is greater than analysisY;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_08(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_08",
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
                Error = "prodY is greater than analysisY;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("prodY") == null || sample.Element("analysisY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("analysisY").Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the current year;",
            ErrorMessage = "sampY is greater than the current year;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_09(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_09",
                Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the current year;",
                Error = "sampY is greater than the current year;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("sampY") == null)
            {
                return outcome;
            }
            else
            {
                if (int.Parse(sample.Element("sampY").Value) > int.Parse(_yearToTest))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
            ErrorMessage = "sampY is greater than analysisY;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_10(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_10",
                Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
                Error = "sampY is greater than analysisY;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("sampY") == null || sample.Element("analysisY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("sampY").Value) > decimal.Parse(sample.Element("analysisY").Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result LOD' (resLOD) must be greater than 0;",
            ErrorMessage = "resLOD is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_11(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_11",
                Description = "The value in 'Result LOD' (resLOD) must be greater than 0;",
                Error = "resLOD is less than or equal to 0;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("resLOD") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOD").Value.Replace(".", ",")) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;",
            ErrorMessage = "resLOQ is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_12(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_12",
                Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;",
                Error = "resLOQ is less than or equal to 0;",
                Passed = true,
                Type = "error",
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOQ").Value.Replace(".", ",")) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'CC alpha' (CCalpha) must be greater than 0;",
            ErrorMessage = "CCalpha is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_13(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_13",
                Description = "The value in 'CC alpha' (CCalpha) must be greater than 0;",
                Error = "CCalpha is less than or equal to 0;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            // <checkedDataElements>;
            //CCalpha;
            //Logik
            if (sample.Element("CCalpha") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha").Value) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'CC beta' (CCbeta) must be greater than 0;",
            ErrorMessage = "CCbeta is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_14(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_14",
                Description = "The value in 'CC beta' (CCbeta) must be greater than 0;",
                Error = "CCbeta is less than or equal to 0;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            // <checkedDataElements>;
            //CCbeta;
            if (sample.Element("CCbeta") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = decimal.Parse(sample.Element("CCbeta").Value) <= 0;
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result value' (resVal) must be greater than 0;",
            ErrorMessage = "resVal is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_15(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_15",
                Description = "The value in 'Result value' (resVal) must be greater than 0;",
                Error = "resVal is less than or equal to 0;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            // <checkedDataElements>;
            //resVal;
            //Logik
            if (sample.Element("resVal") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (ParseDec((string)sample.Element("resVal")) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;",
            ErrorMessage = "resValUncertSD is less than or equal to 0;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR03A_16(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR03A_16",
                Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;",
                Error = "resValUncertSD is less than or equal to 0;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            // <checkedDataElements>;
            //resValUncertSD;
            //Logik
            if (sample.Element("resValUncertSD") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValUncertSD").Value) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than 0;",
            ErrorMessage = "resValUncert is less than or equal to 0;", RuleType = "error", Deprecated = false)]
        public Outcome BR03A_17(XElement sample)
        {
            // <checkedDataElements>;
            //resValUncert;
            var outcome = new Outcome
            {
                Name = "BR03A_17",
                Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than 0;",
                Error = "resValUncert is less than or equal to 0;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            //Logik
            if (sample.Element("resValUncert") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValUncert").Value) >= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;",
            ErrorMessage = "resVal is reported, though resType is non detected value (below LOD);",
            RuleType = "error", Deprecated = false)]
        public Outcome BR04A(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resVal;
            var outcome = new Outcome
            {
                Name = "BR04A",
                Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;",
                Error = "resVal is reported, though resType is non detected value (below LOD);",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if ((string)sample.Element("resType") == "LOD")
            {
                outcome.Passed = String.IsNullOrEmpty((string)sample.Element("resVal"));
            }

            return outcome;
        }

        [Rule(Description = "If the value in 'Result value' (resVal) is greater than the value in 'Legal Limit for the result' (resLegalLimit), then the value in 'Evaluation of the result' (resEvaluation) must be different from 'less than or equal to maximum permissible quantities' (J002A);",
            ErrorMessage = "resEvaluation is less than or equal to maximum permissible quantities, though resVal is greater than resLegalLimit;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR05A(XElement sample)
        {
            // <checkedDataElements>;
            //resVal;
            //resLegalLimit;
            //resEvaluation;

            var outcome = new Outcome
            {
                Name = "BR05A",
                Description = "If the value in 'Result value' (resVal) is greater than the value in 'Legal Limit for the result' (resLegalLimit), then the value in 'Evaluation of the result' (resEvaluation) must be different from 'less than or equal to maximum permissible quantities' (J002A);",
                Error = "resEvaluation is less than or equal to maximum permissible quantities, though resVal is greater than resLegalLimit;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (String.IsNullOrEmpty((string)sample.Element("resType")))
            {
                outcome.Passed = false;
            }
            else
            {
                outcome.Passed = (string)sample.Element("resEvaluation") != "J002A";
            }
            return outcome;
        }

        [Rule(Description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);",
            ErrorMessage = "sampArea is not within sampCountry;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR07A_01(XElement sample)
        {
            // <checkedDataElements>;
            //sampArea;
            //sampCountry;
            var outcome = new Outcome
            {
                Name = "BR07A_01",
                Description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);",
                Error = "sampArea is not within sampCountry;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };
            //Logik
            if (sample.Element("sampArea") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = new OldBusinessRules().GetCountryFromAreaCode((string)sample.Element("sampArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }

        [Rule(Description = "The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);",
            ErrorMessage = "origArea is not within origCountry;", RuleType = "error", Deprecated = false)]
        public Outcome BR07A_02(XElement sample)
        {
            // <checkedDataElements>;
            //origArea;
            //origCountry;
            var outcome = new Outcome
            {
                Name = "BR07A_02",
                Description = "The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);",
                Error = "origArea is not within origCountry;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("origArea") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = new OldBusinessRules().GetCountryFromAreaCode((string)sample.Element("origArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }

        [Rule(Description = "The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);",
            ErrorMessage = "procArea is not within procCountry;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR07A_03(XElement sample)
        {
            // <checkedDataElements>;
            //procArea;
            //procCountry;
            var outcome = new Outcome
            {
                Name = "BR07A_03",
                Description = "The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);",
                Error = "procArea is not within procCountry;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("procArea") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = new OldBusinessRules().GetCountryFromAreaCode((string)sample.Element("procArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;",
            ErrorMessage = "resLOQ is missing, though resType is non quantified value;", RuleType = "error", Deprecated = false)]
        public Outcome BR08A_05(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resLOQ;
            var outcome = new Outcome
            {
                Name = "BR08A_05",
                Description = "If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;",
                Error = "resLOQ is missing, though resType is non quantified value;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if ((string)sample.Element("resType") == "LOQ" && sample.Element("resLOQ") == null)
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;",
            ErrorMessage = "moistPerc is not between 0 and 100;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR09A_09(XElement sample)
        {
            // <checkedDataElements>;
            //moistPerc;
            var outcome = new Outcome
            {
                Name = "BR09A_09",
                Description = "The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;",
                Error = "moistPerc is not between 0 and 100;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("moistPerc") == null)
            {
                //Ignore null
                outcome.Passed = true;
            }
            else
            {
                if ((decimal.Parse(sample.Element("moistPerc").Value.Replace(".", ",")) < 1) && (decimal.Parse(sample.Element("moistPerc").Value.Replace(".", ",")) > 99))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome BR12A_01(XElement sample)
        {
            // <checkedDataElements>;
            //analysisD;
            //analysisM;
            //analysisY;
            var outcome = new Outcome
            {
                Name = "BR12A_01",
                Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;",
                Error = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Skapa ett datum från analysår
            var str_dag = (string)sample.Element("analysisD");
            var str_manad = (string)sample.Element("analysisM");
            var str_ar = (string)sample.Element("analysisM");

            if (str_dag == null || str_manad == null || str_ar == null)
            {
                outcome.Passed = false;
                return outcome;
            }

            if (str_manad.Length == 1)
            {
                str_manad = "0" + str_manad;
            }

            var analysisDate = DateTime.Now.AddDays(7);
            DateTime.TryParseExact(str_ar + "-" + str_manad + "-" + str_dag, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out analysisDate);
            //Logik
            if (analysisDate > DateTime.Now)
            {
                outcome.Passed = false;

                Console.WriteLine("Analysisdate is {0}", analysisDate.ToString());
            }
            return outcome;
        }

        ///Borttagen?        
        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Sample analysis reference time' (sampAnRefTime), 'Year of analysis' (analysisY), 'Month of analysis' (analysisM), 'Day of analysis' (analysisD), 'Additional information on the sample analysed' (sampAnInfo), 'Coded description of the analysed matrix' (anMatCode), 'Text description of the matrix analysed' (anMatText), 'Additional information on the analysed matrix ' (anMatInfo), then a 'Sample analysed identification code' (sampAnId) must be reported;",
            ErrorMessage = "sampAnId is missing, though at least one descriptor for the sample analysed or the matrix analysed (sections F, G) or the sampId is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR4a(XElement sample)
        {
            // <checkedDataElements>;
            //sampAnId;
            //sampAnRefTime;
            //analysisY;
            //analysisM;
            //analysisD;
            //sampAnInfo;
            //anMatCode;
            //anMatText;
            //anMatInfo;

            var outcome = new Outcome
            {
                Name = "GBR4a",
                Description = "If a value is reported in at least one of the following data elements: 'Sample analysis reference time' (sampAnRefTime), 'Year of analysis' (analysisY), 'Month of analysis' (analysisM), 'Day of analysis' (analysisD), 'Additional information on the sample analysed' (sampAnInfo), 'Coded description of the analysed matrix' (anMatCode), 'Text description of the matrix analysed' (anMatText), 'Additional information on the analysed matrix ' (anMatInfo), then a 'Sample analysed identification code' (sampAnId) must be reported;",
                Error = "sampAnId is missing, though at least one descriptor for the sample analysed or the matrix analysed (sections F, G) or the sampId is reported;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("sampAnId") != null || sample.Element("sampAnRefTime") != null || sample.Element("analysisY") != null || sample.Element("analysisM") != null || sample.Element("analysisD") != null || sample.Element("sampAnInfo") != null || sample.Element("anMatCode") != null || sample.Element("anMatText") != null || sample.Element("anMatInfo") != null)
            {
                outcome.Passed = sample.Element("sampAnId") != null;
            }
            else
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed portion sequence' (anPortSeq) must be reported;",
            ErrorMessage = "anPortSeq is missing, though at least one descriptor for the sample analysed portion (section H) is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR5a(XElement sample)
        {
            // <checkedDataElements>;
            var anPortSeq = (string)sample.Element("anPortSeq");
            var anPortSize = (string)sample.Element("anPortSize");
            var anPortSizeUnit = (string)sample.Element("anPortSizeUnit");
            var anPortInfo = (string)sample.Element("anPortInfo");

            var outcome = new Outcome
            {
                Name = "GBR5a",
                Lastupdate = "2016-03-01",
                Description = "If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed portion sequence' (anPortSeq) must be reported;",
                Error = "anPortSeq is missing, though at least one descriptor for the sample analysed portion (section H) is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!(new List<string> { anPortSize, anPortSizeUnit, anPortInfo }).Any(i => i == null))
            {
                var anPortSizes = new List<string>();
                outcome.Passed = anPortSizes.Contains(anPortSize);
                var anPortSizeUnits = new List<string>();
                outcome.Passed = anPortSizeUnits.Contains(anPortSizeUnit);
                var anPortInfos = new List<string>();
                outcome.Passed = anPortInfos.Contains(anPortInfo);
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following descriptor data elements: 'Coded description of the isolate' (isolParamCode), 'Text description of the isolate' (isolParamText), 'Additional information on the isolate' (isolInfo), then a 'Isolate identification' (isolId) must be reported;",
            ErrorMessage = "isolId is missing, though at least one descriptor for the isolate (section I) is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR6a(XElement sample)
        {
            // <checkedDataElements>;
            //isolId;
            //isolParamCode;
            //isolParamText;
            //isolInfo;

            var outcome = new Outcome
            {
                Name = "GBR6a",
                Description = "If a value is reported in at least one of the following descriptor data elements: 'Coded description of the isolate' (isolParamCode), 'Text description of the isolate' (isolParamText), 'Additional information on the isolate' (isolInfo), then a 'Isolate identification' (isolId) must be reported;",
                Error = "isolId is missing, though at least one descriptor for the isolate (section I) is reported;",
                Type = "error",
                Passed = true,
                Lastupdate = "",
            };

            //Logik
            if (sample.Element("isolId") != null || sample.Element("isolParamCode") != null || sample.Element("isolParamText") != null || sample.Element("isolInfo") != null)
            {
                outcome.Passed = sample.Element("isolId") != null;
            }
            else
            {
                outcome.Passed = true;
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in 'Local organisation country' (localOrgCountry), then a 'Local organisation identification code' (localOrgId) must be reported;",
            ErrorMessage = "localOrgId is missing, though localOrgCountry is reported;", RuleType = "error", Deprecated = false)]
        public Outcome GBR7a(XElement sample)
        {
            // <checkedDataElements>;
            var localOrgId = (string)sample.Element("localOrgId");
            var localOrgCountry = (string)sample.Element("localOrgCountry");

            var outcome = new Outcome
            {
                Name = "GBR7a",
                Lastupdate = "2016-03-01",
                Description = "If a value is reported in 'Local organisation country' (localOrgCountry), then a 'Local organisation identification code' (localOrgId) must be reported;",
                Error = "localOrgId is missing, though localOrgCountry is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(localOrgCountry))
            {
                outcome.Passed = !String.IsNullOrEmpty(localOrgId);
            }

            return outcome;
        }

        [Rule(Description = "If a value is reported in 'Laboratory country' (labCountry), then a 'Laboratory identification code' (labId) must be reported;",
            ErrorMessage = "labId is missing, though labCountry is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR10a(XElement sample)
        {
            // <checkedDataElements>;
            var labId = (string)sample.Element("labId");
            var labCountry = (string)sample.Element("labCountry");

            var outcome = new Outcome
            {
                Name = "GBR10a",
                Lastupdate = "2016-03-01",
                Description = "If a value is reported in 'Laboratory country' (labCountry), then a 'Laboratory identification code' (labId) must be reported;",
                Error = "labId is missing, though labCountry is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(labCountry))
            {
                outcome.Passed = !String.IsNullOrEmpty(labId);
            }
            return outcome;
        }

        [Rule(Description = "If in the 'Coded description of the matrix of the sample taken' the generic-term facet (sampMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix of the sample taken' (sampMatText);",
            ErrorMessage = "sampMatText is missing, though mandatory if sampMatCode.gen is 'Other' (A07XE);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR15(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatCodegen = (string)sample.Element("sampMatCode.gen");
            var sampMatText = (string)sample.Element("sampMatText");

            var outcome = new Outcome
            {
                Name = "GBR15",
                Lastupdate = "2014-08-08",
                Description = "If in the 'Coded description of the matrix of the sample taken' the generic-term facet (sampMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix of the sample taken' (sampMatText);",
                Error = "sampMatText is missing, though mandatory if sampMatCode.gen is 'Other' (A07XE);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (sampMatCodegen == "A07XE")
            {
                outcome.Passed = !String.IsNullOrEmpty(sampMatText);
            }

            return outcome;
        }

        [Rule(Description = "If in the 'Coded description of the analysed matrix' the generic-term facet’ (anMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix analysed' (anMatText);",
            ErrorMessage = "anMatText is missing, though mandatory if anMatCode.gen is 'Other' (A07XE);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR16(XElement sample)
        {
            // <checkedDataElements>;
            var anMatCodegen = (string)sample.Element("anMatCode.gen");
            var anMatText = (string)sample.Element("anMatText");

            var outcome = new Outcome
            {
                Name = "GBR16",
                Lastupdate = "2014-08-08",
                Description = "If in the 'Coded description of the analysed matrix' the generic-term facet’ (anMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix analysed' (anMatText);",
                Error = "anMatText is missing, though mandatory if anMatCode.gen is 'Other' (A07XE);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);

            if (anMatCodegen == "A07XE")
            {
                outcome.Passed = !String.IsNullOrEmpty(anMatText);
            }
            return outcome;
        }

        [Rule(Description = "If the reported value in the 'Coded description of the parameter' (paramCode.base) is 'Not in list' (RF-XXXX-XXX-XXX), then a text must be reported in the 'Parameter text' (paramText);",
            ErrorMessage = "paramText is missing, though mandatory if paramCode.base is 'Not in list' (RF-XXXX-XXX-XXX);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR17(XElement sample)
        {
            // <checkedDataElements>;
            var paramCodebase = (string)sample.Element("paramCode").Element("base");
            var paramText = (string)sample.Element("paramText");

            var outcome = new Outcome
            {
                Name = "GBR17",
                Lastupdate = "2017-03-31",
                Description = "If the reported value in the 'Coded description of the parameter' (paramCode.base) is 'Not in list' (RF-XXXX-XXX-XXX), then a text must be reported in the 'Parameter text' (paramText);",
                Error = "paramText is missing, though mandatory if paramCode.base is 'Not in list' (RF-XXXX-XXX-XXX);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (paramCodebase == "RF-XXXX-XXX-XXX")
            {
                outcome.Passed = !String.IsNullOrEmpty(paramText);
            }
            return outcome;
        }

        [Rule(Description = "If the value in the 'Expression of result type' (exprResType) is 'Dry matter' (B002A), then a value must be reported in the 'Percentage of moisture ' (exprResPerc.moistPerc);",
            ErrorMessage = "exprResPerc.moistPerc is missing, though mandatory if exprResType is expressed on 'dry matter' basis;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR23(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercmoistPerc = (string)sample.Element("exprResPerc.moistPerc");
            var exprResType = (string)sample.Element("exprResType");

            var outcome = new Outcome
            {
                Name = "GBR23",
                Lastupdate = "2017-04-27",
                Description = "If the value in the 'Expression of result type' (exprResType) is 'Dry matter' (B002A), then a value must be reported in the 'Percentage of moisture ' (exprResPerc.moistPerc);",
                Error = "exprResPerc.moistPerc is missing, though mandatory if exprResType is expressed on 'dry matter' basis;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (exprResType == "B002A")
            {
                outcome.Passed = !String.IsNullOrEmpty(exprResPercmoistPerc);
            }
            return outcome;
        }

        [Rule(Description = "If a 'Sample taken size' (sampSize) is reported, then a 'Sample taken size unit' (sampSizeUnit) must be reported;",
            ErrorMessage = "sampSizeUnit is missing, though sampSize is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR25(XElement sample)
        {
            // <checkedDataElements>;
            var sampSize = (string)sample.Element("sampSize");
            var sampSizeUnit = (string)sample.Element("sampSizeUnit");

            var outcome = new Outcome
            {
                Name = "GBR25",
                Lastupdate = "2017-09-07",
                Description = "If a 'Sample taken size' (sampSize) is reported, then a 'Sample taken size unit' (sampSizeUnit) must be reported;",
                Error = "sampSizeUnit is missing, though sampSize is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampSize))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampSizeUnit);

            }
            return outcome;
        }

        [Rule(Description = "If the value reported in 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN) (i.e. a qualitative value), then a 'Result qualitative value' (resQualValue) must be reported;",
            ErrorMessage = "resQualValue is missing, though resType is 'Qualitative Value (Binary)' (BIN);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR28(XElement sample)
        {
            // <checkedDataElements>;
            var resQualValue = (string)sample.Element("resQualValue");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR28",
                Lastupdate = "2014-08-08",
                Description = "If the value reported in 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN) (i.e. a qualitative value), then a 'Result qualitative value' (resQualValue) must be reported;",
                Error = "resQualValue is missing, though resType is 'Qualitative Value (Binary)' (BIN);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "BIN")
            {
                outcome.Passed = !String.IsNullOrEmpty(resQualValue);
            }
            return outcome;
        }

        [Rule(Description = "If the reported value in the 'Analytical method code' (anMethCode.base) is 'Classification not possible' (F001A), then a text must be reported in the 'Analytical method text' (anMethText);",
            ErrorMessage = "anMethText is missing, though mandatory if anMethCode.base is 'Classification not possible' (F001A);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR18(XElement sample)
        {
            // <checkedDataElements>;
            var anMethCodebase = (string)sample.Element("anMethCode.base");
            var anMethText = (string)sample.Element("anMethText");

            var outcome = new Outcome
            {
                Name = "GBR18",
                Lastupdate = "2014-08-08",
                Description = "If the reported value in the 'Analytical method code' (anMethCode.base) is 'Classification not possible' (F001A), then a text must be reported in the 'Analytical method text' (anMethText);",
                Error = "anMethText is missing, though mandatory if anMethCode.base is 'Classification not possible' (F001A);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);

            if (anMethCodebase == "F001A")
            {
                outcome.Passed = !String.IsNullOrEmpty(anMethText);
            }
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of fat' (exprResPerc.fatPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
            ErrorMessage = "exprResPerc.fatPerc is not between '0' and '100';",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR19(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercfatPerc = (string)sample.Element("exprResPerc.fatPerc");

            var outcome = new Outcome
            {
                Name = "GBR19",
                Lastupdate = "2014-08-08",
                Description = "The value in the data element 'Percentage of fat' (exprResPerc.fatPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
                Error = "exprResPerc.fatPerc is not between '0' and '100';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(exprResPercfatPerc, out decimal result))
            {
                outcome.Passed = result > 0 && result <= 100;
            }

            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of moisture ' (exprResPerc.moistPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
            ErrorMessage = "exprResPerc.moistPerc is not between '0' and '100';",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR20(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercmoistPerc = (string)sample.Element("exprResPerc.moistPerc");

            var outcome = new Outcome
            {
                Name = "GBR20",
                Lastupdate = "2014-08-08",
                Description = "The value in the data element 'Percentage of moisture ' (exprResPerc.moistPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
                Error = "exprResPerc.moistPerc is not between '0' and '100';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(exprResPercmoistPerc))
            {
                outcome.Passed = decimal.TryParse(exprResPercmoistPerc, out decimal r);
            }
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of alcohol' (exprResPerc.alcoholPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
            ErrorMessage = "exprResPerc.alcoholPerc is not between '0' and '100';",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR21(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercalcoholPerc = (string)sample.Element("exprResPerc.alcoholPerc");

            var outcome = new Outcome
            {
                Name = "GBR21",
                Lastupdate = "2014-08-08",
                Description = "The value in the data element 'Percentage of alcohol' (exprResPerc.alcoholPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);",
                Error = "exprResPerc.alcoholPerc is not between '0' and '100';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(exprResPercalcoholPerc))
            {
                outcome.Passed = decimal.TryParse(exprResPercalcoholPerc, out decimal r);
            }
            return outcome;
        }

        [Rule(Description = "If the value in the 'Expression of result type' (exprResType) is 'Fat weight' (B003A), then a value must be reported in the 'Percentage of fat' (exprResPerc.fatPerc);",
            ErrorMessage = "exprResPerc.fatPerc is missing, though mandatory if exprResType is 'Fat weight' (B003A);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR22(XElement sample)
        {
            // <checkedDataElements>;
            //var exprResPercfatPerc = (string)sample.Element("exprResPerc.fatPerc");
            var exprResPercfatPerc = (string)sample.Element("exprResPerc");
            var exprResType = (string)sample.Element("exprResType");

            var outcome = new Outcome
            {
                Name = "GBR22",
                Lastupdate = "2014-08-08",
                Description = "If the value in the 'Expression of result type' (exprResType) is 'Fat weight' (B003A), then a value must be reported in the 'Percentage of fat' (exprResPerc.fatPerc);",
                Error = "exprResPerc.fatPerc is missing, though mandatory if exprResType is 'Fat weight' (B003A);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (exprResType == "B003A")
            {
                outcome.Passed = !String.IsNullOrEmpty(exprResPercfatPerc);
            }

            return outcome;
        }

        [Rule(Description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;",
            ErrorMessage = "sampUnitSizeUnit is missing, though sampUnitSize is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR24(XElement sample)
        {
            // <checkedDataElements>;
            var sampUnitSize = (string)sample.Element("sampUnitSize");
            var sampUnitSizeUnit = (string)sample.Element("sampUnitSizeUnit");

            var outcome = new Outcome
            {
                Name = "GBR24",
                Lastupdate = "2014-08-08",
                Description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;",
                Error = "sampUnitSizeUnit is missing, though sampUnitSize is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampUnitSize))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampUnitSizeUnit);
            }

            return outcome;
        }

        [Rule(Description = "If a 'Sample analysed portion size' (anPortSize) is reported, then a 'Sample analysed portion size unit' (anPortSizeUnit) must be reported;",
            ErrorMessage = "anPortSizeUnit is missing, though anPortSize is reported;", RuleType = "error", Deprecated = false)]
        public Outcome GBR26(XElement sample)
        {
            // <checkedDataElements>;
            var anPortSize = (string)sample.Element("anPortSize");
            var anPortSizeUnit = (string)sample.Element("anPortSizeUnit");

            var outcome = new Outcome
            {
                Name = "GBR26",
                Lastupdate = "2014-08-08",
                Description = "If a 'Sample analysed portion size' (anPortSize) is reported, then a 'Sample analysed portion size unit' (anPortSizeUnit) must be reported;",
                Error = "anPortSizeUnit is missing, though anPortSize is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(anPortSize))
            {
                outcome.Passed = !String.IsNullOrEmpty(anPortSizeUnit);
            }
            return outcome;
        }

        [Rule(Description = "If the value reported in 'Type of result' (resType) is different from 'Qualitative Value (Binary)' (BIN) (i.e. not a qualitative value), then a 'Result unit' (resUnit) must be reported;",
            ErrorMessage = "resUnit is missing, though resType is not 'Qualitative Value (Binary)' (BIN);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR27(XElement sample)
        {
            // <checkedDataElements>;
            var resUnit = (string)sample.Element("resUnit");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR27",
                Lastupdate = "2017-09-20",
                Description = "If the value reported in 'Type of result' (resType) is different from 'Qualitative Value (Binary)' (BIN) (i.e. not a qualitative value), then a 'Result unit' (resUnit) must be reported;",
                Error = "resUnit is missing, though resType is not 'Qualitative Value (Binary)' (BIN);",
                Type = "error",
                Passed = true
            };
            if (resType != "BIN")
            {
                outcome.Passed = !string.IsNullOrEmpty(resUnit);

            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'Result lower limit of the working range' (resLLWR), 'Result upper limit of the working range' (resULWR), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Limit for the result evaluation (Low limit)' (evalLowLimit), 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Result unit' (resUnit) must be reported;",
            ErrorMessage = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit) is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR29(XElement sample)
        {
            // <checkedDataElements>;
            var resUnit = (string)sample.Element("resUnit");
            var resLOD = (string)sample.Element("resLOD");
            var resLOQ = (string)sample.Element("resLOQ");
            var resLLWR = (string)sample.Element("resLLWR");
            var resULWR = (string)sample.Element("resULWR");
            var CCalpha = (string)sample.Element("CCalpha");
            var CCbeta = (string)sample.Element("CCbeta");
            var resVal = (string)sample.Element("resVal");
            var resValUncert = (string)sample.Element("resValUncert");
            var resValUncertSD = (string)sample.Element("resValUncertSD");
            var evalLowLimit = (string)sample.Element("evalLowLimit");
            var evalHighLimit = (string)sample.Element("evalHighLimit");

            var outcome = new Outcome
            {
                Name = "GBR29",
                Lastupdate = "2014-08-08",
                Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'Result lower limit of the working range' (resLLWR), 'Result upper limit of the working range' (resULWR), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Limit for the result evaluation (Low limit)' (evalLowLimit), 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Result unit' (resUnit) must be reported;",
                Error = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit) is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            var listOfNotEmpty = new List<string> { resUnit, resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                outcome.Passed = !String.IsNullOrEmpty(resUnit);

            }

            return outcome;
        }

        [Rule(Description = "If a value is reported in 'Limit for the result evaluation ' (evalLowLimit), then a 'Type of limit for the result evaluation' (evalLimitType) must be reported;",
            ErrorMessage = "evalLimitType is missing, though evalLowLimit is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR30(XElement sample)
        {
            // <checkedDataElements>;
            var evalLimitType = (string)sample.Element("evalLimitType");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome
            {
                Name = "GBR30",
                Lastupdate = "2014-08-08",
                Description = "If a value is reported in 'Limit for the result evaluation ' (evalLowLimit), then a 'Type of limit for the result evaluation' (evalLimitType) must be reported;",
                Error = "evalLimitType is missing, though evalLowLimit is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (decimal.TryParse(evalLowLimit, out decimal v))
            {
                outcome.Passed = !String.IsNullOrEmpty(evalLimitType);
            }

            return outcome;
        }

        [Rule(Description = "If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;",
            ErrorMessage = "evalLowLimit is missing, though evalHighLimit is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR31(XElement sample)
        {
            // <checkedDataElements>;
            var evalLowLimit = (string)sample.Element("evalLowLimit");
            var evalHighLimit = (string)sample.Element("evalHighLimit");
            var evalLimitType = (string)sample.Element("evalLimitType");

            var outcome = new Outcome
            {
                Name = "GBR31",
                Lastupdate = "2017-04-24",
                Description = "If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;",
                Error = "evalLowLimit is missing, though evalHighLimit is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (evalLimitType != "W001A" && decimal.TryParse(evalHighLimit, out decimal value))
            {
                outcome.Passed = decimal.TryParse(evalLowLimit, out decimal result);
            }
            return outcome;
        }

        [Rule(Description = "The value reported in 'Limit for the result evaluation (High limit)' (evalHighLimit) must be greater than the value reported in 'Limit for the result evaluation (Low limit)' (evalLowLimit);",
            ErrorMessage = "evalHighLimit is not greater than evalLowLimit;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR32(XElement sample)
        {
            // <checkedDataElements>;
            var evalLowLimit = (string)sample.Element("evalLowLimit");
            var evalHighLimit = (string)sample.Element("evalHighLimit");

            var outcome = new Outcome
            {
                Name = "GBR32",
                Lastupdate = "2014-08-08",
                Description = "The value reported in 'Limit for the result evaluation (High limit)' (evalHighLimit) must be greater than the value reported in 'Limit for the result evaluation (Low limit)' (evalLowLimit);",
                Error = "evalHighLimit is not greater than evalLowLimit;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(evalLowLimit, out decimal evallowlimit) && decimal.TryParse(evalHighLimit, out decimal evalhighlimit))
            {
                outcome.Passed = evalhighlimit > evallowlimit;
            }
            return outcome;
        }

        [Rule(Description = "If 'Result value' (resVal) is greater than 'Limit for the result evaluation ' (evalLowLimit), then the value in 'Evaluation of the result' (evalCode) must be different from 'below or equal to maximum permissible quantities' (J002A);",
            ErrorMessage = "evalCode is 'below or equal to maximum permissible quantities' (J002A), though resVal is greater than evalLowLimit;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR33(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");
            var resVal = (string)sample.Element("resVal");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome
            {
                Name = "GBR33",
                Lastupdate = "2014-08-08",
                Description = "If 'Result value' (resVal) is greater than 'Limit for the result evaluation ' (evalLowLimit), then the value in 'Evaluation of the result' (evalCode) must be different from 'below or equal to maximum permissible quantities' (J002A);",
                Error = "evalCode is 'below or equal to maximum permissible quantities' (J002A), though resVal is greater than evalLowLimit;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evallowlimit))
            {
                outcome.Passed = resval >= evallowlimit && evalCode != "J002A";
            }
            return outcome;
        }

        [Rule(Description = "If 'Evaluation of the result' (evalCode) is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A), then 'Result value' (resVal) must be greater than 'Limit for the result evaluation ' (evalLowLimit);",
            ErrorMessage = "resVal is lower than evalLowLimit, though evalCode is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR34(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var evalCode = (string)sample.Element("evalCode");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome
            {
                Name = "GBR34",
                Lastupdate = "2014-08-08",
                Description = "If 'Evaluation of the result' (evalCode) is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A), then 'Result value' (resVal) must be greater than 'Limit for the result evaluation ' (evalLowLimit);",
                Error = "resVal is lower than evalLowLimit, though evalCode is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (evalCode == "J003A" || evalCode == "J031A")
            {
                if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evalLowlimit))
                {
                    outcome.Passed = resval >= evalLowlimit;
                }
            }
            return outcome;
        }

        [Rule(Description = "If 'Evaluation of the result' (evalCode) is 'below or equal to maximum permissible quantities' (J002A), then 'Result value' (resVal) must be less than or equal to 'Limit for the result evaluation ' (evalLowLimit);",
            ErrorMessage = "resVal is greater than evalLowLimit, though evalCode is 'below or equal to maximum permissible quantities' (J002A);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR35(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");
            var resVal = (string)sample.Element("resVal");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome
            {
                Name = "GBR35",
                Lastupdate = "2014-08-08",
                Description = "If 'Evaluation of the result' (evalCode) is 'below or equal to maximum permissible quantities' (J002A), then 'Result value' (resVal) must be less than or equal to 'Limit for the result evaluation ' (evalLowLimit);",
                Error = "resVal is greater than evalLowLimit, though evalCode is 'below or equal to maximum permissible quantities' (J002A);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (evalCode == "J002A")
            {
                if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evalLowlimit))
                {
                    outcome.Passed = resval <= evalLowlimit;
                }
            }

            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then a value must be reported in the data element 'Result LOD' (resLOD);",
            ErrorMessage = "resLOD is missing, though resType is 'Non Detected Value (below LOD)' (LOD);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR36(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR36",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then a value must be reported in the data element 'Result LOD' (resLOD);",
                Error = "resLOD is missing, though resType is 'Non Detected Value (below LOD)' (LOD);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "LOD")
            {
                outcome.Passed = !String.IsNullOrEmpty(resLOD);
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);",
            ErrorMessage = "resLOD is not less than or equal to resLOQ;", RuleType = "error", Deprecated = false)]
        public Outcome GBR37(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resLOQ = (string)sample.Element("resLOQ");

            var outcome = new Outcome
            {
                Name = "GBR37",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);",
                Error = "resLOD is not less than or equal to resLOQ;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resLOD, out decimal reslod) && decimal.TryParse(resLOQ, out decimal resloq))
            {
                outcome.Passed = resloq <= reslod;
            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result LOD' (resLOD) must be greater than '0';",
            ErrorMessage = "resLOD is not greater than '0';",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR38(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");

            var outcome = new Outcome
            {
                Name = "GBR38",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result LOD' (resLOD) must be greater than '0';",
                Error = "resLOD is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resLOD))
            {
                if (decimal.TryParse(resLOD, out decimal result))
                {
                    outcome.Passed = result > 0;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Quantified Value (below LOQ)' (LOQ), then a value must be reported in the data element 'Result LOQ' (resLOQ);",
            ErrorMessage = "resLOQ is missing, though resType is 'Non Quantified Value (below LOQ)' (LOQ);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR39(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR39",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Non Quantified Value (below LOQ)' (LOQ), then a value must be reported in the data element 'Result LOQ' (resLOQ);",
                Error = "resLOQ is missing, though resType is 'Non Quantified Value (below LOQ)' (LOQ);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "LOQ")
            {
                outcome.Passed = !String.IsNullOrEmpty(resLOQ);
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;",
            ErrorMessage = "resLOQ is not greater than 0;", RuleType = "error", Deprecated = false)]
        public Outcome GBR40(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");

            var outcome = new Outcome
            {
                Name = "GBR40",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;",
                Error = "resLOQ is not greater than 0;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resLOQ))
            {
                outcome.Passed = false;
                resLOQ = resLOQ.Replace('.', ',');
                if (decimal.TryParse(resLOQ, out decimal result))
                {
                    outcome.Passed = result > 0;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);",
            ErrorMessage = "CCalpha is missing, though resType is 'Value below CCalpha (below CCα)' (CCA);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR41(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR41",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);",
                Error = "CCalpha is missing, though resType is 'Value below CCalpha (below CCα)' (CCA);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "CCA")
            {
                outcome.Passed = !String.IsNullOrEmpty(CCalpha);
            }
            return outcome;
        }

        [Rule(Description = "The value in 'CC alpha' (CCalpha) must be less than the value in 'CC beta' (CCbeta);",
           ErrorMessage = "WARNING: CCalpha is not less than CCbeta;",
           RuleType = "warning", Deprecated = false)]
        public Outcome GBR42(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");
            var CCbeta = (string)sample.Element("CCbeta");

            var outcome = new Outcome
            {
                Name = "GBR42",
                Lastupdate = "2017-11-16",
                Description = "The value in 'CC alpha' (CCalpha) must be less than the value in 'CC beta' (CCbeta);",
                Error = "WARNING: CCalpha is not less than CCbeta;",
                Type = "warning",
                Passed = true
            };

            if (decimal.TryParse(CCalpha, out decimal _ccalpha) && decimal.TryParse(CCbeta, out decimal _ccbeta))
            {
                outcome.Passed = _ccalpha < _ccbeta;
            }
            return outcome;
        }

        [Rule(Description = "The value in 'CC alpha' (CCalpha) must be greater than '0';",
            ErrorMessage = "CCalpha is not greater than '0';", RuleType = "error")]
        public Outcome GBR43(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");

            var outcome = new Outcome
            {
                Name = "GBR43",
                Lastupdate = "2014-08-08",
                Description = "The value in 'CC alpha' (CCalpha) must be greater than '0';",
                Error = "CCalpha is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(CCalpha))
            {
                outcome.Passed = false;
                if (decimal.TryParse(CCalpha, out decimal result))
                {
                    outcome.Passed = result > 0;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);",
            ErrorMessage = "CCbeta is missing, though resType is 'Value below CCbeta (below CCβ)' (CCB);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR44(XElement sample)
        {
            // <checkedDataElements>;
            var CCbeta = (string)sample.Element("CCbeta");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR44",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);",
                Error = "CCbeta is missing, though resType is 'Value below CCbeta (below CCβ)' (CCB);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "CCB")
            {
                outcome.Passed = !String.IsNullOrEmpty(CCbeta);
            }

            return outcome;
        }

        ///The value in 'CC beta' (CCbeta) must be greater than '0';
        [Rule(Description = "The value in 'CC beta' (CCbeta) must be greater than '0';", ErrorMessage = "CCbeta is not greater than '0';", RuleType = "error", Deprecated = false)]
        public Outcome GBR45(XElement sample)
        {
            // <checkedDataElements>;
            var CCbeta = (string)sample.Element("CCbeta");

            var outcome = new Outcome
            {
                Name = "GBR45",
                Lastupdate = "2014-08-08",
                Description = "The value in 'CC beta' (CCbeta) must be greater than '0';",
                Error = "CCbeta is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(CCbeta))
            {
                outcome.Passed = false;
                if (decimal.TryParse(CCbeta, out decimal result))
                {
                    outcome.Passed = result > 0;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Numerical Value' (VAL), then a value must be reported in the data element 'Result value' (resVal);",
            ErrorMessage = "resVal is missing, though resType is 'Numerical Value' (VAL);", RuleType = "error", Deprecated = false)]
        public Outcome GBR46(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome
            {
                Name = "GBR46",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Numerical Value' (VAL), then a value must be reported in the data element 'Result value' (resVal);",
                Error = "resVal is missing, though resType is 'Numerical Value' (VAL);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "VAL")
            {
                outcome.Passed = !String.IsNullOrEmpty(resVal);
            }

            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;",
            ErrorMessage = "resVal is reported, though resType is 'Non Detected Value (below LOD)' (LOD);",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR47(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome
            {
                Name = "GBR47",
                Lastupdate = "2014-08-08",
                Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;",
                Error = "resVal is reported, though resType is 'Non Detected Value (below LOD)' (LOD);",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (resType == "LOD")
            {
                outcome.Passed = String.IsNullOrEmpty(resVal);
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Result value' (resVal) must be greater than '0';", ErrorMessage = "resVal is not greater than '0';", RuleType = "error", Deprecated = false)]
        public Outcome GBR48(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome
            {
                Name = "GBR48",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result value' (resVal) must be greater than '0';",
                Error = "resVal is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resVal, out decimal result))
            {
                outcome.Passed = result > 0;
            }
            if (!String.IsNullOrEmpty(resVal))
            {

            }
            return outcome;
        }

        [Rule(Description = "The value in 'Result value recovery rate' (resValRec) must be greater than '0';",
            ErrorMessage = "resValRec is not greater than '0';", RuleType = "error", Deprecated = false)]
        public Outcome GBR49(XElement sample)
        {
            // <checkedDataElements>;
            var resValRec = (string)sample.Element("resValRec");

            var outcome = new Outcome
            {
                Name = "GBR49",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result value recovery rate' (resValRec) must be greater than '0';",
                Error = "resValRec is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValRec, out decimal result))
            {
                outcome.Passed = result > 0;
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than '0';",
            ErrorMessage = "resValUncertSD is not greater than '0';", RuleType = "error", Deprecated = false)]
        public Outcome GBR50(XElement sample)
        {
            // <checkedDataElements>;
            var resValUncertSD = (string)sample.Element("resValUncertSD");

            var outcome = new Outcome
            {
                Name = "GBR50",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than '0';",
                Error = "resValUncertSD is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValUncertSD, out decimal result))
            {
                outcome.Passed = result > 0;
            }

            return outcome;
        }

        [Rule(Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than '0';", ErrorMessage = "resValUncert is not greater than '0';", RuleType = "error", Deprecated = false)]
        public Outcome GBR51(XElement sample)
        {
            // <checkedDataElements>;
            var resValUncert = (string)sample.Element("resValUncert");

            var outcome = new Outcome
            {
                Name = "GBR51",
                Lastupdate = "2014-08-08",
                Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than '0';",
                Error = "resValUncert is not greater than '0';",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValUncert, out decimal result))
            {
                outcome.Passed = result > 0;
            }

            return outcome;
        }

        [Rule(Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be a valid date;",
            ErrorMessage = "The combination of values in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR53(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterY = (string)sample.Element("sampEventInfo.slaughterY");

            var outcome = new Outcome
            {
                Name = "GBR53",
                Lastupdate = "2014-08-08",
                Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be a valid date;",
                Error = "The combination of values in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampEventInfoslaughterD, sampEventInfoslaughterM, sampEventInfoslaughterY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                outcome.Passed = DateTime.TryParseExact(sampEventInfoslaughterY + "/" + sampEventInfoslaughterM + "/" + sampEventInfoslaughterD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }

            return outcome;
        }

        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be a valid date;",
            ErrorMessage = "The combination of values in sampD, sampM, and sampY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR54(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome
            {
                Name = "GBR54",
                Lastupdate = "2014-08-08",
                Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be a valid date;",
                Error = "The combination of values in sampD, sampM, and sampY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };

                outcome.Passed = DateTime.TryParseExact(sampY + "/" + sampM + "/" + sampD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }

            return outcome;
        }

        [Rule(Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be a valid date;",
            ErrorMessage = "The combination of values in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR55(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalY = (string)sample.Element("sampInfo.arrivalY");

            var outcome = new Outcome
            {
                Name = "GBR55",
                Lastupdate = "2014-08-08",
                Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be a valid date;",
                Error = "The combination of values in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampInfoarrivalD, sampInfoarrivalM, sampInfoarrivalY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                outcome.Passed = DateTime.TryParseExact(sampInfoarrivalY + "/" + sampInfoarrivalM + "/" + sampInfoarrivalD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }

        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be a valid date;",
            ErrorMessage = "The combination of values in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR56(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");

            var outcome = new Outcome
            {
                Name = "GBR56",
                Lastupdate = "2014-08-08",
                Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be a valid date;",
                Error = "The combination of values in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                outcome.Passed = DateTime.TryParseExact(sampMatInfoprodY + "/" + sampMatInfoprodM + "/" + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime dateone);

            }
            return outcome;
        }

        [Rule(Description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), must be a valid date;",
            ErrorMessage = "The combination of values in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR57(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome
            {
                Name = "GBR57",
                Lastupdate = "2014-08-08",
                Description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), must be a valid date;",
                Error = "The combination of values in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoexpiryD, sampMatInfoexpiryM, sampMatInfoexpiryY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                outcome.Passed = DateTime.TryParseExact(sampMatInfoexpiryY + "/" + sampMatInfoexpiryM + "/" + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }

        [Rule(Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be a valid date;",
            ErrorMessage = "The combination of values in analysisD, analysisM, and analysisY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR58(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome
            {
                Name = "GBR58",
                Lastupdate = "2014-08-08",
                Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be a valid date;",
                Error = "The combination of values in analysisD, analysisM, and analysisY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { analysisD, analysisM, analysisY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                outcome.Passed = DateTime.TryParseExact(analysisY + "/" + analysisM + "/" + analysisD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }

        [Rule(Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be a valid date;",
            ErrorMessage = "The combination of values in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY is not a valid date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR60(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome
            {
                Name = "GBR60",
                Lastupdate = "2014-08-08",
                Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be a valid date;",
                Error = "The combination of values in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY is not a valid date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { isolInfoisolD, isolInfoisolM, isolInfoisolY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(isolInfoisolY + "/" + isolInfoisolM + "/" + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = true;
                }
                else
                {
                    outcome.Passed = false;
                }
            }

            return outcome;
        }

        [Rule(Description = "The reporting year, reported in 'Reporting year' (repYear), must be less than or equal to the current year;",
            ErrorMessage = "The reporting year, reported in repYear, is greater than the current year;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR61(XElement sample)
        {
            // <checkedDataElements>;
            var repYear = (string)sample.Element("repYear");

            var outcome = new Outcome
            {
                Name = "GBR61",
                Lastupdate = "2014-08-08",
                Description = "The reporting year, reported in 'Reporting year' (repYear), must be less than or equal to the current year;",
                Error = "The reporting year, reported in repYear, is greater than the current year;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(repYear, out int result))
            {
                outcome.Passed = result <= DateTime.Now.Year;
            }
            return outcome;
        }

        [Rule(Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR62(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterY = (string)sample.Element("sampEventInfo.slaughterY");

            var outcome = new Outcome
            {
                Name = "GBR62",
                Lastupdate = "2014-08-08",
                Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be less than or equal to the current date;",
                Error = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampEventInfoslaughterD, sampEventInfoslaughterM, sampEventInfoslaughterY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(sampEventInfoslaughterY + "/" + sampEventInfoslaughterM + "/" + sampEventInfoslaughterD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR63(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome
            {
                Name = "GBR63",
                Lastupdate = "2014-08-08",
                Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the current date;",
                Error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(sampY + "/" + sampM + "/" + sampD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the arrival in the laboratory, reported in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR64(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalY = (string)sample.Element("sampInfo.arrivalY");

            var outcome = new Outcome
            {
                Name = "GBR64",
                Lastupdate = "2014-08-08",
                Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be less than or equal to the current date;",
                Error = "The date of the arrival in the laboratory, reported in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampInfoarrivalD, sampInfoarrivalM, sampInfoarrivalY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(sampInfoarrivalY + "/" + sampInfoarrivalM + "/" + sampInfoarrivalD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR65(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");

            var outcome = new Outcome
            {
                Name = "GBR65",
                Lastupdate = "2014-08-08",
                Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the current date;",
                Error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(sampMatInfoprodY + "/" + sampMatInfoprodM + "/" + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), should be less than or equal to the current date;",
            ErrorMessage = "WARNING: the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY, is not less than or equal to the current date;",
            RuleType = "warning", Deprecated = false)]
        public Outcome GBR66(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome
            {
                Name = "GBR66",
                Lastupdate = "2017-07-05",
                Description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), should be less than or equal to the current date;",
                Error = "WARNING: the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY, is not less than or equal to the current date;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: yes);
            string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
            if (DateTime.TryParseExact(sampMatInfoexpiryY + "/" + sampMatInfoexpiryM + "/" + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime _date))
            {
                outcome.Passed = _date < DateTime.Now;
            }
            return outcome;
        }

        [Rule(Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR67(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome
            {
                Name = "GBR67",
                Lastupdate = "2014-08-08",
                Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;",
                Error = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { analysisD, analysisM, analysisY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(analysisY + "/" + analysisM + "/" + analysisD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        ///The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;
        [Rule(Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;",
            ErrorMessage = "The date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY, is not less than or equal to the current date;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR69(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome
            {
                Name = "GBR69",
                Lastupdate = "2014-08-08",
                Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;",
                Error = "The date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { isolInfoisolD, isolInfoisolM, isolInfoisolY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                if (DateTime.TryParseExact(isolInfoisolY + "/" + isolInfoisolM + "/" + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.Passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        [Rule(Description = "The 'Day of slaughtering' (sampEventInfo.slaughterD) must be between 1 and 31;",
            ErrorMessage = "sampEventInfo.slaughterD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR70(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");

            var outcome = new Outcome
            {
                Name = "GBR70",
                Lastupdate = "2014-08-08",
                Description = "The 'Day of slaughtering' (sampEventInfo.slaughterD) must be between 1 and 31;",
                Error = "sampEventInfo.slaughterD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };

            if (int.TryParse(sampEventInfoslaughterD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Day of sampling' (sampD) must be between 1 and 31;", ErrorMessage = "sampD is not between 1 and 31;", RuleType = "error", Deprecated = false)]
        public Outcome GBR71(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");

            var outcome = new Outcome
            {
                Name = "GBR71",
                Lastupdate = "2014-08-08",
                Description = "The 'Day of sampling' (sampD) must be between 1 and 31;",
                Error = "sampD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };

            if (int.TryParse(sampD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Arrival Day' (sampInfo.arrivalD) must be between 1 and 31;", ErrorMessage = "sampInfo.arrivalD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR72(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");

            var outcome = new Outcome
            {
                Name = "GBR72",
                Lastupdate = "2014-08-08",
                Description = "The 'Arrival Day' (sampInfo.arrivalD) must be between 1 and 31;",
                Error = "sampInfo.arrivalD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };


            if (int.TryParse(sampInfoarrivalD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Day of production' (sampMatInfo.prodD) must be between 1 and 31;", ErrorMessage = "sampMatInfo.prodD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR73(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");

            var outcome = new Outcome
            {
                Name = "GBR73",
                Lastupdate = "2014-08-08",
                Description = "The 'Day of production' (sampMatInfo.prodD) must be between 1 and 31;",
                Error = "sampMatInfo.prodD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };

            if (int.TryParse(sampMatInfoprodD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Day of expiry' (sampMatInfo.expiryD) must be between 1 and 31;", ErrorMessage = "sampMatInfo.expiryD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR74(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");

            var outcome = new Outcome
            {
                Name = "GBR74",
                Lastupdate = "2014-08-08",
                Description = "The 'Day of expiry' (sampMatInfo.expiryD) must be between 1 and 31;",
                Error = "sampMatInfo.expiryD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };

            if (int.TryParse(sampMatInfoexpiryD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Day of analysis' (analysisD) must be between 1 and 31;", ErrorMessage = "analysisD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR75(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");

            var outcome = new Outcome
            {
                Name = "GBR75",
                Lastupdate = "2014-08-08",
                Description = "The 'Day of analysis' (analysisD) must be between 1 and 31;",
                Error = "analysisD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };

            if (int.TryParse(analysisD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Isolation day' (isolInfo.isolD) must be between 1 and 31;", ErrorMessage = "isolInfo.isolD is not between 1 and 31;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR77(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");

            var outcome = new Outcome
            {
                Name = "GBR77",
                Lastupdate = "2014-08-08",
                Description = "The 'Isolation day' (isolInfo.isolD) must be between 1 and 31;",
                Error = "isolInfo.isolD is not between 1 and 31;",
                Type = "error",
                Passed = true
            };
            //Logik (ignore null: yes);
            if (int.TryParse(isolInfoisolD, out int result))
            {
                outcome.Passed = result > 0 && result < 32;
            }
            return outcome;
        }

        [Rule(Description = "The 'Month of slaughtering' (sampEventInfo.slaughterM) must be between 1 and 12;", ErrorMessage = "sampEventInfo.slaughterM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR78(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");

            var outcome = new Outcome
            {
                Name = "GBR78",
                Lastupdate = "2014-08-08",
                Description = "The 'Month of slaughtering' (sampEventInfo.slaughterM) must be between 1 and 12;",
                Error = "sampEventInfo.slaughterM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(sampEventInfoslaughterM, out int result))
            {
                outcome.Passed = result > 0 && result < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Month of sampling' (sampM) must be between 1 and 12;", ErrorMessage = "sampM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR79(XElement sample)
        {
            // <checkedDataElements>;
            var sampM = (string)sample.Element("sampM");

            var outcome = new Outcome
            {
                Name = "GBR79",
                Lastupdate = "2014-08-08",
                Description = "The 'Month of sampling' (sampM) must be between 1 and 12;",
                Error = "sampM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(sampM, out int result))
            {
                outcome.Passed = result > 0 && result < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Arrival Month' (sampInfo.arrivalM) must be between 1 and 12;", ErrorMessage = "sampInfo.arrivalM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR80(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");

            var outcome = new Outcome
            {
                Name = "GBR80",
                Lastupdate = "2014-08-08",
                Description = "The 'Arrival Month' (sampInfo.arrivalM) must be between 1 and 12;",
                Error = "sampInfo.arrivalM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(sampInfoarrivalM, out int result))
            {
                outcome.Passed = result > 0 && result < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Month of production' (sampMatInfo.prodM) must be between 1 and 12;", ErrorMessage = "sampMatInfo.prodM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR81(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");

            var outcome = new Outcome
            {
                Name = "GBR81",
                Lastupdate = "2014-08-08",
                Description = "The 'Month of production' (sampMatInfo.prodM) must be between 1 and 12;",
                Error = "sampMatInfo.prodM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(sampMatInfoprodM, out int result))
            {
                outcome.Passed = result > 0 && result < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Month of expiry' (sampMatInfo.expiryM) must be between 1 and 12;", ErrorMessage = "sampMatInfo.expiryM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR82(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");

            var outcome = new Outcome
            {
                Name = "GBR82",
                Lastupdate = "2014-08-08",
                Description = "The 'Month of expiry' (sampMatInfo.expiryM) must be between 1 and 12;",
                Error = "sampMatInfo.expiryM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (int.TryParse(sampMatInfoexpiryM, out int result))
            {
                outcome.Passed = result > 0 && result < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Month of analysis' (analysisM) must be between 1 and 12;", ErrorMessage = "analysisM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR83(XElement sample)
        {
            // <checkedDataElements>;
            var analysisM = (string)sample.Element("analysisM");

            var outcome = new Outcome
            {
                Name = "GBR83",
                Lastupdate = "2014-08-08",
                Description = "The 'Month of analysis' (analysisM) must be between 1 and 12;",
                Error = "analysisM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(analysisM))
            {
                outcome.Passed = int.Parse(analysisM) > 0 && int.Parse(analysisM) < 13;
            }
            return outcome;
        }

        [Rule(Description = "The 'Isolation month' (isolInfo.isolM) must be between 1 and 12;", ErrorMessage = "isolInfo.isolM is not between 1 and 12;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR85(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");

            var outcome = new Outcome
            {
                Name = "GBR85",
                Lastupdate = "2014-08-08",
                Description = "The 'Isolation month' (isolInfo.isolM) must be between 1 and 12;",
                Error = "isolInfo.isolM is not between 1 and 12;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(isolInfoisolM))
            {
                outcome.Passed = int.Parse(isolInfoisolM) > 0 && int.Parse(isolInfoisolM) < 13;
            }
            return outcome;
        }

        [Rule(Description = "If the 'Day of slaughtering' (sampEventInfo.slaughterD) is reported, then the 'Month of slaughtering' (sampEventInfo.slaughterM) must be reported;",
            ErrorMessage = "sampEventInfo.slaughterM is missing, though sampEventInfo.slaughterD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR86(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");

            var outcome = new Outcome
            {
                Name = "GBR86",
                Lastupdate = "2014-08-08",
                Description = "If the 'Day of slaughtering' (sampEventInfo.slaughterD) is reported, then the 'Month of slaughtering' (sampEventInfo.slaughterM) must be reported;",
                Error = "sampEventInfo.slaughterM is missing, though sampEventInfo.slaughterD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampEventInfoslaughterD))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampEventInfoslaughterM);
            }

            return outcome;
        }

        [Rule(Description = "If the 'Day of sampling' (sampD) is reported, then the 'Month of sampling' (sampM) must be reported;",
            ErrorMessage = "sampM is missing, though sampD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR87(XElement sample)
        {
            // <checkedDataElements>;
            var sampM = (string)sample.Element("sampM");
            var sampD = (string)sample.Element("sampD");

            var outcome = new Outcome
            {
                Name = "GBR87",
                Lastupdate = "2014-08-08",
                Description = "If the 'Day of sampling' (sampD) is reported, then the 'Month of sampling' (sampM) must be reported;",
                Error = "sampM is missing, though sampD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampD))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampM);
            }

            return outcome;
        }

        [Rule(Description = "If the 'Arrival Day' (sampInfo.arrivalD) is reported, then the 'Arrival Month' (sampInfo.arrivalM) must be reported;",
            ErrorMessage = "sampInfo.arrivalM is missing, though sampInfo.arrivalD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR88(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");

            var outcome = new Outcome
            {
                Name = "GBR88",
                Lastupdate = "2014-08-08",
                Description = "If the 'Arrival Day' (sampInfo.arrivalD) is reported, then the 'Arrival Month' (sampInfo.arrivalM) must be reported;",
                Error = "sampInfo.arrivalM is missing, though sampInfo.arrivalD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampInfoarrivalD))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampInfoarrivalM);
            }

            return outcome;
        }

        [Rule(Description = "If the 'Day of production' (sampMatInfo.prodD) is reported, then the 'Month of production' (sampMatInfo.prodM) must be reported;",
            ErrorMessage = "sampMatInfo.prodM is missing, though sampMatInfo.prodD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR89(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");

            var outcome = new Outcome
            {
                Name = "GBR89",
                Lastupdate = "2014-08-08",
                Description = "If the 'Day of production' (sampMatInfo.prodD) is reported, then the 'Month of production' (sampMatInfo.prodM) must be reported;",
                Error = "sampMatInfo.prodM is missing, though sampMatInfo.prodD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampMatInfoprodD))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampMatInfoprodM);
            }
            return outcome;
        }

        [Rule(Description = "If the 'Day of expiry' (sampMatInfo.expiryD) is reported, then the 'Month of expiry' (sampMatInfo.expiryM) must be reported;",
            ErrorMessage = "sampMatInfo.expiryM is missing, though sampMatInfo.expiryD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR90(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");

            var outcome = new Outcome
            {
                Name = "GBR90",
                Lastupdate = "2014-08-08",
                Description = "If the 'Day of expiry' (sampMatInfo.expiryD) is reported, then the 'Month of expiry' (sampMatInfo.expiryM) must be reported;",
                Error = "sampMatInfo.expiryM is missing, though sampMatInfo.expiryD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampMatInfoexpiryM))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampMatInfoexpiryD);
            }

            return outcome;
        }

        [Rule(Description = "If the 'Day of analysis' (analysisD) is reported, then the 'Month of analysis' (analysisM) must be reported;",
            ErrorMessage = "analysisM is missing, though analysisD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR91(XElement sample)
        {
            // <checkedDataElements>;
            var analysisM = (string)sample.Element("analysisM");
            var analysisD = (string)sample.Element("analysisD");

            var outcome = new Outcome
            {
                Name = "GBR91",
                Lastupdate = "2014-08-08",
                Description = "If the 'Day of analysis' (analysisD) is reported, then the 'Month of analysis' (analysisM) must be reported;",
                Error = "analysisM is missing, though analysisD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(analysisD))
            {
                outcome.Passed = !String.IsNullOrEmpty(analysisM);
            }

            return outcome;
        }

        [Rule(Description = "If the 'Completion day of analysis' (sampAnInfo.compD) is reported, then the 'Completion month of analysis' (sampAnInfo.compM) must be reported;",
            ErrorMessage = "sampAnInfo.compM is missing, though sampAnInfo.compD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR92(XElement sample)
        {
            // <checkedDataElements>;
            var sampAnInfocompM = (string)sample.Element("sampAnInfo.compM");
            var sampAnInfocompD = (string)sample.Element("sampAnInfo.compD");

            var outcome = new Outcome
            {
                Name = "GBR92",
                Lastupdate = "2014-08-08",
                Description = "If the 'Completion day of analysis' (sampAnInfo.compD) is reported, then the 'Completion month of analysis' (sampAnInfo.compM) must be reported;",
                Error = "sampAnInfo.compM is missing, though sampAnInfo.compD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampAnInfocompD))
            {
                outcome.Passed = !String.IsNullOrEmpty(sampAnInfocompM);
            }
            return outcome;
        }

        [Rule(Description = "If the 'Isolation day' (isolInfo.isolD) is reported, then the 'Isolation month' (isolInfo.isolM) must be reported;",
            ErrorMessage = "isolInfo.isolM is missing, though isolInfo.isolD is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR93(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");

            var outcome = new Outcome
            {
                Name = "GBR93",
                Lastupdate = "2014-08-08",
                Description = "If the 'Isolation day' (isolInfo.isolD) is reported, then the 'Isolation month' (isolInfo.isolM) must be reported;",
                Error = "isolInfo.isolM is missing, though isolInfo.isolD is reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(isolInfoisolD))
            {
                outcome.Passed = !String.IsNullOrEmpty(isolInfoisolM);
            }
            return outcome;
        }

        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY);",
            ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR94(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome
            {
                Name = "GBR94",
                Lastupdate = "2014-08-08",
                Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY);",
                Error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY;",
                Type = "error",
                Passed = true
            };

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, sampMatInfoexpiryY, sampMatInfoexpiryM, sampMatInfoexpiryD };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + "/" + sampMatInfoprodM + "/" + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampInfo))
                {
                    if (DateTime.TryParseExact(sampMatInfoexpiryY + sampMatInfoexpiryM + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime sampExp))
                    {
                        outcome.Passed = sampInfo <= sampExp;
                    }
                }
            }
            return outcome;
        }

        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);",
            ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR95(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome
            {
                Name = "GBR95",
                Lastupdate = "2014-08-08",
                Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);",
                Error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;",
                Type = "error",
                Passed = true
            };

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, sampY, sampM, sampD };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampMatDate))
                {
                    if (DateTime.TryParseExact(sampY + "/" + sampM + "/" + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                    {
                        outcome.Passed = sampMatDate <= sampDate;
                    }
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);",
            ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR96(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome
            {
                Name = "GBR96",
                Lastupdate = "2014-08-08",
                Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);",
                Error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;",
                Type = "error",
                Passed = true
            };

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, analysisD, analysisM, analysisY };

            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(analysisY + "/" + analysisM + "/" + analysisD, formats, null, DateTimeStyles.None, out DateTime analysisDate))
                    {
                        outcome.Passed = sampDate <= analysisDate;
                    }
                }
            }

            return outcome;
        }

        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);",
            ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR97(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome
            {
                Name = "GBR97",
                Lastupdate = "2014-08-08",
                Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);",
                Error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY, analysisD, analysisM, analysisY };

            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(analysisY + "/" + analysisM + "/" + analysisD, formats, null, DateTimeStyles.None, out DateTime analysisDate))
                    {
                        outcome.Passed = sampDate <= analysisDate;
                    }
                }
            }
            return outcome;
        }

        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY);",
            ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR99(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome
            {
                Name = "GBR99",
                Lastupdate = "2014-08-08",
                Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY);",
                Error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY, isolInfoisolD, isolInfoisolM, isolInfoisolY };

            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(isolInfoisolY + "/" + isolInfoisolM + "/" + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime isolDate))
                    {
                        outcome.Passed = sampDate <= isolDate;
                    }
                }
            }
            return outcome;
        }

        [Rule(Description = "If a value is reported in 'Programme legal reference' (progLegalRef), then a 'Sampling programme identification code' (progId) must be reported;",
            ErrorMessage = "progId is missing, though progLegalRef is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR8a(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "GBR8a",
                Passed = true,
                Description = "If a value is reported in 'Programme legal reference' (progLegalRef), then a 'Sampling programme identification code' (progId) must be reported;",
                Error = "progId is missing, though progLegalRef is reported;",
                Lastupdate = "2016-03-01",
                Type = "error",
            };
            var progId = sample.Element("progId")?.Value;
            var progLegalRef = sample.Element("progLegalRef")?.Value;
            if (string.IsNullOrEmpty(progLegalRef))
                return outcome;
            if (string.IsNullOrWhiteSpace(progId))
                outcome.Passed = false;
            return outcome;
        }

        [Rule(Description = "If a value is reported in at least one of the following descriptor data elements: 'Analytical method reference code' (anMethRefCode), 'Analytical method code' (anMethCode), 'Analytical method text' (anMethText), and 'Additional information on the analytical method' (anMethInfo), then a 'Analytical method identification' (anMethRefId) must be reported;",
            ErrorMessage = "anMethRefId is missing, though at least one descriptor for the method(section L) is reported;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR11a(XElement sample)
        {
            //INACTIVE ????????????????????????????????
            var outcome = new Outcome
            {
                Name = "GBR11a",
                Passed = true,
                Description = "If a value is reported in at least one of the following descriptor data elements: 'Analytical method reference code' (anMethRefCode), 'Analytical method code' (anMethCode), 'Analytical method text' (anMethText), and 'Additional information on the analytical method' (anMethInfo), then a 'Analytical method identification' (anMethRefId) must be reported;",
                Error = "anMethRefId is missing, though at least one descriptor for the method(section L) is reported;",
                Lastupdate = "2017-03-01",
                Type = "error",
            };
            var anMethRefId = sample.Element("anMethRefId")?.Value;
            var anMethRefCode = sample.Element("anMethRefCode")?.Value;
            var anMethCode = sample.Element("anMethCode")?.Value;
            var anMethText = sample.Element("anMethText")?.Value;
            var anMethInfo = sample.Element("anMethInfo")?.Value;
            if (string.IsNullOrEmpty(anMethRefCode) && string.IsNullOrEmpty(anMethCode) && string.IsNullOrEmpty(anMethText) && string.IsNullOrEmpty(anMethInfo))
                return outcome;
            if (string.IsNullOrWhiteSpace(anMethRefId))
                outcome.Passed = false;
            return outcome;
        }

        [Rule(Description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);",
            ErrorMessage = "sampArea is not within sampCountry;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR12(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "GBR12",
                Passed = true,
                Description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);",
                Error = "sampArea is not within sampCountry;",
                Lastupdate = "",
                Type = "error",
            };
            var sampArea = sample.Element("sampArea");
            var sampCountry = sample.Element("sampCountry")?.Value;
            if (sampArea == null)
                return outcome;
            var sampAreaCode = sampArea.Attribute("countryCode")?.Value;
            if (sampAreaCode == null)
                return outcome;
            if (sampAreaCode != sampCountry)
                outcome.Passed = false;
            return outcome;
        }

        [Rule(Description = "The 'Area of origin of the sample taken' (origArea) must be within the 'Country of origin of the sample taken' (origCountry);",
            ErrorMessage = "origArea is not within origCountry;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR13(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "GBR13",
                Passed = true,
                Description = "The 'Area of origin of the sample taken' (origArea) must be within the 'Country of origin of the sample taken' (origCountry);",
                Error = "origArea is not within origCountry;",
                Lastupdate = "2014-08-08",
                Type = "error",
            };
            var origArea = sample.Element("origArea");
            var origCountry = sample.Element("origCountry")?.Value;
            if (origArea == null)
                return outcome;
            var origAreaCode = origArea.Attribute("countryCode")?.Value;
            if (origAreaCode == null)
                return outcome;
            if (origAreaCode != origCountry)
                outcome.Passed = false;
            return outcome;
        }

        [Rule(Description = "The 'Area of processing of the sample taken' (procArea) must be within the 'Country of processing of the sample taken' (procCountry);",
            ErrorMessage = "procArea is not within procCountry;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR14(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "GBR14",
                Passed = true,
                Description = "The 'Area of processing of the sample taken' (procArea) must be within the 'Country of processing of the sample taken' (procCountry);",
                Error = "procArea is not within procCountry;",
                Lastupdate = "",
                Type = "error",
            };
            var procArea = sample.Element("procArea");
            var procCountry = sample.Element("procCountry")?.Value;
            if (procArea == null)
                return outcome;
            var procAreaCode = procArea.Attribute("countryCode")?.Value;
            if (procAreaCode == null)
                return outcome;
            if (procAreaCode != procCountry)
                outcome.Passed = false;
            return outcome;
        }

        [Rule(Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);",
            ErrorMessage = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;",
            RuleType = "error", Deprecated = false)]
        public Outcome GBR100(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "GBR100",
                Passed = true,
                Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);",
                Error = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;",
                Lastupdate = "2014-08-08",
                Type = "error",
            };
            var sampEventInfo = sample.Element("sampEventInfo")?.Value;
            var sampD = sample.Element("sampD")?.Value;
            var sampM = sample.Element("sampM")?.Value;
            var sampY = sample.Element("sampY")?.Value;
            if (string.IsNullOrWhiteSpace(sampEventInfo))
                return outcome;
            var splitted = sampEventInfo.Split('$');
            var slaughtY = string.Empty;
            var slaughtM = string.Empty;
            var slaughtD = string.Empty;
            foreach (var item in splitted)
            {
                if (item.StartsWith("slaughterD"))
                {
                    slaughtD = item.Split('=').First();
                }
                else if (item.StartsWith("slaughterM"))
                {
                    slaughtM = item.Split('=').First();
                }
                else if (item.StartsWith("slaughterY"))
                {
                    slaughtY = item.Split('=').First();
                }
            }
            if (string.IsNullOrWhiteSpace(slaughtD) || string.IsNullOrWhiteSpace(slaughtM) || string.IsNullOrWhiteSpace(slaughtY))
                return outcome;
            string[] formats = { "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/dd", "yyyy/M/d" };

            if (DateTime.TryParseExact(sampY + "/" + sampM + "/" + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
            {
                if (DateTime.TryParseExact(slaughtY + "/" + slaughtM + "/" + slaughtD, formats, null, DateTimeStyles.None, out DateTime slaughtDate))
                {
                    outcome.Passed = sampDate >= slaughtDate;
                }
            }
            return outcome;
        }               

        /// <summary>
        /// ParseDec
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
        public decimal? PD(string s)
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
