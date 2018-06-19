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
/// The GOD-class. Validates EFSAs business rules with one method per Rule. Each method takes an Xelement (result) coded in workflow 2
/// 
/// Test
/// Version 0.01 October 2016
/// </summary>
    public class OldBusinessRules
    {
        ///If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique;
        public Outcome BR01A(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR01A"
            };

            if (sample.Element("paramCode").Value != "RF-XXXX-XXX-XXX")
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
            outcome.Description = "If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique";
            outcome.Error = "The combination of values in paramCode, labSampCode and labSubSampCode is not unique";
            return outcome;
        }


        ///If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;
        public Outcome BR02A_01(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_01",

                Description = "If the value in 'Day of analysis' (analysisD) is reported, then a value in 'Month of analysis' (analysisM) must be reported;",
                Error = "analysisM is missing, though analysisD is reported;"
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


        public Outcome BR02A_02(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_02",
                Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;",
                Error = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, resLegalLimit) is reported;"
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
                outcome.Passed = sample.Element("resUnit") != null && sample.Element("resUnit").Value != null;
            }
            else
            {
                outcome.Passed = true;
            }

            return outcome;
        }

        ///If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;
        public Outcome BR02A_03(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_03",
                Description = "If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;",
                Error = "prodM is missing, though prodD is reported;",
                Passed = true
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

        ///If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;
        public Outcome BR02A_04(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_04",
                Description = "If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;",
                Error = "expiryM is missing, though expiryD is reported;",
                Passed = true
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

        // Define other methods and classes here
        ///If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;

        ///If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;
        public Outcome BR02A_05(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "BR02A_05",
                Description = "If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;",
                Error = "sampM is missing, though sampD is reported;",
                Passed = true
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

        ///If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;
        public Outcome BR02A_06(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;",
                Error = "lotSizeUnit is missing, though lotSize is reported;",
                Passed = true
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

        ///If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;
        public Outcome BR02A_07(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;",
                Error = "WARNING: resLegalLimitType is missing, though resLegalLimit is reported;",
                Passed = true
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

        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        public Outcome BR03A_01(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;",
                Error = "analysisY is greater than the current year;",
                Passed = true
            };

            //Logik
            var analysisY = sample.Element("analysisY").Value;
            if (int.Parse(analysisY) <= 2016)
            {
                //Condition is true
                outcome.Passed = true;
            }

            return outcome;
        }

        ///The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);
        public Outcome BR03A_02(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);",
                Error = "resLOD is greater than resLOQ;",
                Passed = true
            };

            //Logik

            if (sample.Element("resLOD") == null || sample.Element("resLOQ") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOD").Value.Replace(".", ",")) > decimal.Parse(sample.Element("resLOQ").Value.Replace(".", ",")))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);
        public Outcome BR03A_03(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);",
                Error = "CCalpha is greater than CCbeta;",
                Passed = true
            };

            //Logik

            if (sample.Element("CCalpha") == null || sample.Element("CCbeta") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha").Value) > decimal.Parse(sample.Element("CCbeta").Value))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Result value recovery' (resValRec) must be greater than 0;
        public Outcome BR03A_04(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Result value recovery' (resValRec) must be greater than 0;",
                Error = "resValRec is less than or equal to 0;",
                Passed = true
            };

            //Logik
            if (sample.Element("resValRec") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValRec").Value.Replace(".", ",")) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Year of production' (prodY) must be less than or equal to the current year;
        public Outcome BR03A_05(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the current year;",
                Error = "prodY is greater than the current year;",
                Passed = true
            };

            //Logik


            //Logik
            if (sample.Element("prodY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) <= 0)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);
        public Outcome BR03A_06(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);",
                Error = "prodY is greater than expiryY;",
                Passed = true
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
        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);
        public Outcome BR03A_07(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);",
                Error = "prodY is greater than sampY;",
                Passed = true
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

        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);
        public Outcome BR03A_08(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
                Error = "prodY is greater than analysisY;",
                Passed = true
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
        public Outcome BR03A_09(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the current year;",
                Error = "sampY is greater than the current year;",
                Passed = true
            };

            //Logik
            if (sample.Element("sampY") == null)
            {
                return outcome;
            }
            else
            {
                if (int.Parse(sample.Element("sampY").Value) > 2016)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }
        ///The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);
        public Outcome BR03A_10(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);",
                Error = "sampY is greater than analysisY;",
                Passed = true
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


        ///The value in 'Result LOD' (resLOD) must be greater than 0;
        public Outcome BR03A_11(XElement sample)
        {
            var outcome = new Outcome
            {
                Description = "The value in 'Result LOD' (resLOD) must be greater than 0;",
                Error = "resLOD is less than or equal to 0;",
                Passed = true
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

        ///The value in 'Result LOQ' (resLOQ) must be greater than 0;
        public Outcome BR03A_12(XElement sample)
        {


            var outcome = new Outcome
            {
                Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;",
                Error = "resLOQ is less than or equal to 0;",
                Passed = true
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

        ///The value in 'CC alpha' (CCalpha) must be greater than 0;
        public Outcome BR03A_13(XElement sample)
        {
            // <checkedDataElements>;
            //CCalpha;

            var outcome = new Outcome
            {
                Description = "The value in 'CC alpha' (CCalpha) must be greater than 0;",
                Error = "CCalpha is less than or equal to 0;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("CCalpha") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha").Value) <= 0 )
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'CC beta' (CCbeta) must be greater than 0;
        public Outcome BR03A_14(XElement sample)
        {
            // <checkedDataElements>;
            //CCbeta;

            var outcome = new Outcome
            {
                Description = "The value in 'CC beta' (CCbeta) must be greater than 0;",
                Error = "CCbeta is less than or equal to 0;",
                Type = "error",
                Passed = true
            };

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

        ///The value in 'Result value' (resVal) must be greater than 0;
        public Outcome BR03A_15(XElement sample)
        {
            // <checkedDataElements>;
            //resVal;

            var outcome = new Outcome
            {
                Description = "The value in 'Result value' (resVal) must be greater than 0;",
                Error = "resVal is less than or equal to 0;",
                Type = "error",
                Passed = true
            };

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

        ///The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;
        public Outcome BR03A_16(XElement sample)
        {
            // <checkedDataElements>;
            //resValUncertSD;

            var outcome = new Outcome
            {
                Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;",
                Error = "resValUncertSD is less than or equal to 0;",
                Type = "error",
                Passed = true
            };

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


        ///The value in 'Result value uncertainty' (resValUncert) must be greater than 0;
        public Outcome BR03A_17(XElement sample)
        {
            // <checkedDataElements>;
            //resValUncert;

            var outcome = new Outcome
            {
                Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than 0;",
                Error = "resValUncert is less than or equal to 0;",
                Type = "error",
                Passed = true
            };


            //Logik
            if (sample.Element("resValUncert") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValUncert").Value) >= 0 )
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;
        public Outcome BR04A(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resVal;

            var outcome = new Outcome
            {
                Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;",
                Error = "resVal is reported, though resType is non detected value (below LOD);",
                Type = "error",
                Passed = true
            };

            //Logik
            if ((string) sample.Element("resType") == "LOD" )
            {
                outcome.Passed = String.IsNullOrEmpty((string)sample.Element("resVal"));
            }
            
            return outcome;
        }


        void Main()
        {

        }

        ///If the value in 'Result value' (resVal) is greater than the value in 'Legal Limit for the result' (resLegalLimit), then the value in 'Evaluation of the result' (resEvaluation) must be different from 'less than or equal to maximum permissible quantities' (J002A);
        public Outcome BR05A(XElement sample)
        {
            // <checkedDataElements>;
            //resVal;
            //resLegalLimit;
            //resEvaluation;

            var outcome = new Outcome
            {
                Description = "If the value in 'Result value' (resVal) is greater than the value in 'Legal Limit for the result' (resLegalLimit), then the value in 'Evaluation of the result' (resEvaluation) must be different from 'less than or equal to maximum permissible quantities' (J002A);",
                Error = "resEvaluation is less than or equal to maximum permissible quantities, though resVal is greater than resLegalLimit;",
                Type = "error",
                Passed = true
            };

            //Logik
            if ( String.IsNullOrEmpty((string)sample.Element("resType")))
            {
                outcome.Passed = false;
            }
            else
            {
                outcome.Passed = (string)sample.Element("resEvaluation") != "J002A";
            }
            return outcome;
        }




        ///The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);
        public Outcome BR07A_01(XElement sample)
        {
            // <checkedDataElements>;
            //sampArea;
            //sampCountry;

            var outcome = new Outcome
            {
                Description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);",
                Error = "sampArea is not within sampCountry;",
                Type = "error",
                Passed = true
            };



            //Logik
            if ( sample.Element("sampArea") == null )
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed =  GetCountryFromAreaCode((string) sample.Element("sampArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }

        ///The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);
        public Outcome BR07A_02(XElement sample)
        {
            // <checkedDataElements>;
            //origArea;
            //origCountry;

            var outcome = new Outcome
            {
                Description = "The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);",
                Error = "origArea is not within origCountry;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("origArea") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = GetCountryFromAreaCode((string)sample.Element("origArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }


        ///The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);
        public Outcome BR07A_03(XElement sample)
        {
            // <checkedDataElements>;
            //procArea;
            //procCountry;

            var outcome = new Outcome
            {
                Description = "The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);",
                Error = "procArea is not within procCountry;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("procArea") == null)
            {
                outcome.Passed = true;
            }
            else
            {
                outcome.Passed = GetCountryFromAreaCode((string)sample.Element("procArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }


        ///If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;
        public Outcome BR08A_05(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resLOQ;

            var outcome = new Outcome
            {
                Description = "If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;",
                Error = "resLOQ is missing, though resType is non quantified value;",
                Type = "error",
                Passed = true
            };

            //Logik
            if ((string)sample.Element("resType") == "LOQ" && sample.Element("resLOQ") == null)
            {
                outcome.Passed = false;
            }

            return outcome;
        }



        ///The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;
        public Outcome BR09A_09(XElement sample)
        {
            // <checkedDataElements>;
            //moistPerc;

            var outcome = new Outcome
            {
                Description = "The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;",
                Error = "moistPerc is not between 0 and 100;",
                Type = "error",
                Passed = true
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



        ///The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;
        public Outcome BR12A_01(XElement sample)
        {
            // <checkedDataElements>;
            //analysisD;
            //analysisM;
            //analysisY;

            var outcome = new Outcome
            {
                Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;",
                Error = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;",
                Type = "error",
                Passed = true
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

            DateTime.TryParseExact(str_ar + "-" + str_manad + "-" + str_dag, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture,
                                           System.Globalization.DateTimeStyles.None, out analysisDate);


            //Logik
            if (analysisDate > DateTime.Now)
            {
                outcome.Passed = false;

                Console.WriteLine("Analysisdate is {0}", analysisDate.ToString());
            }

            return outcome;
        }




        ///If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported;
        public Outcome PEST01(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        ///A value in the data element 'Result LOQ' (resLOQ) must be reported;
        public Outcome PEST02_OLD(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST02",
                Description = "A value in the data element 'Result LOQ' (resLOQ) must be reported;",
                Error = "resLOQ is missing, though mandatory;",
                Passed = true
            };

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                outcome.Passed = false;
            }
            return outcome;
        }
        ///A value in the data element 'Evaluation of the result' (resEvaluation) must be reported;
        public Outcome PEST03_OBSOLETE(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST03",
                Description = "A value in the data element 'Evaluation of the result' (resEvaluation) must be reported;",
                Error = "resEvaluation is missing, though mandatory;",
                Passed = true
            };

            //Logik
            if (sample.Element("resEvaluation") == null)
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        ///A value in the data element 'Method of production' (prodProdMeth) must be reported;
        public Outcome PEST04_OLD(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST04",
                Description = "A value in the data element 'Method of production' (prodProdMeth) must be reported;",
                Error = "prodProdMeth is missing, though mandatory;",
                Passed = true
            };

            //Logik
            if (sample.Element("prodProdMeth") == null)
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';
        public Outcome PEST05_OLD(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST05",
                Description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';",
                Error = "resType is different from VAL, though resEvaluation is greater than maximum permissible quantities or compliant due to measurement uncertainty;",
                Passed = true
            };

            //Logik
            if (sample.Element("resEvaluation").Value == "J003A" || sample.Element("resEvaluation").Value == "J031A")
            {
                outcome.Passed = sample.Element("resType").Value == "VAL";
            }
            return outcome;
        }

        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);
        public Outcome PEST06_OLD(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "PEST06",
                Description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);",
                Error = "resVal is less than resLegalLimit, though resEvaluation is greater than maximum permissible quantities, or compliant due to measurement uncertainty;",
                Passed = true
            };

            //Logik
            if (sample.Element("resEvaluation").Value == "J003A" || sample.Element("resEvaluation").Value == "J031A")
            {
                outcome.Passed = decimal.Parse(sample.Element("resVal").Value.Replace(".", ",")) > decimal.Parse(sample.Element("resLegalLimit").Value.Replace(".", ","));
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);
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
        ///If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A);
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

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);
        public Outcome PEST07_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //prodTreat;



#pragma warning disable IDE0028 // Simplify collection initialization
            var list = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            list.Add("PX100000A");
            list.Add("PX100001A");
            list.Add("PX100003A");
            list.Add("PX100004A");
            list.Add("PX100005A");


            var outcome = new Outcome
            {
                Name = "PEST07",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);",
                Error = "prodTreat is not processed, though prodCode is a baby food;",
                Passed = true
            };

            if (list.Any(l => l == sample.Element("prodCode").Value))
            {
                outcome.Passed = sample.Element("prodTreat").Value == "T100A";
            }

            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Milk pasteurisation' (T150A), or 'Freezing' (T998A), or 'Concentration' (T136A), or 'Unprocessed' (T999A);
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

        ///If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
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
        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A);
        public Outcome PEST08_OBSOLETE(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //prodTreat;

            var outcome = new Outcome
            {
                Name = "PEST08",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A);",
                Error = "prodTreat is not dehydration, churning, milk pasteurisation, or unprocessed, though prodCode is milk of animal origin;",
                Passed = true
            };

#pragma warning disable IDE0028 // Simplify collection initialization
            var a = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            a.Add("P1020000A");
            a.Add("P1020010A");
            a.Add("P1020020A");
            a.Add("P1020030A");
            a.Add("P1020040A");
            a.Add("P1020990A");

#pragma warning disable IDE0028 // Simplify collection initialization
            var b = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            b.Add("T131A");
            b.Add("T134A");
            b.Add("T150A");
            b.Add("T999A");

            if (a.Any(l => l == sample.Element("prodCode").Value))
            {
                outcome.Passed = b.Any(l => l == sample.Element("prodTreat").Value);
            }


            //Logik

            return outcome;
        }

        /*
        ///If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);
        public Outcome PEST06(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.name = "PEST06";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);";
            outcome.error = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (prodTreat == "T150A")
            {
                var produktkoder = new List<string>();
                produktkoder.Add("P1020000A");
                produktkoder.Add("P1020010A");
                produktkoder.Add("P1020020A");
                produktkoder.Add("P1020030A");
                produktkoder.Add("P1020040A");
                produktkoder.Add("P1020990A");
                outcome.passed = produktkoder.Contains(prodCode);
            }
       
            return outcome;
        }
        */

        ///If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), or 'Churning' (T134A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), or 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or 'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);
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


        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);
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

        ///If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);
        public Outcome PEST09_OBSOLETE(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //prodTreat;

            var outcome = new Outcome
            {
                Name = "PEST09",
                Description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);",
                Error = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation;",
                Passed = true
            };



#pragma warning disable IDE0028 // Simplify collection initialization
            var a = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            a.Add("P1020000A");
            a.Add("P1020010A");
            a.Add("P1020020A");
            a.Add("P1020030A");
            a.Add("P1020040A");
            a.Add("P1020990A");

            if (sample.Element("prodTreat").Value == "T150A")
            {
                outcome.Passed = a.Any(l => l == sample.Element("prodCode").Value);
            }

            return outcome;
        }

        ///The value in the data element (exprRes) can only be equal to 'Whole weight' (B001A), or 'Fat basis' (B003A), or 'Reconstituted product' (B007A);
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
                if (!exprRess.Contains(exprRes) )
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }
        ///If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
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
                outcome.Passed = PD(resVal) > PD(resLegalLimit) ;
            }
          
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;
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
        public Outcome PEST25(XElement sample)
        {
            // <checkedDataElements>;
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome
            {
                Name = "PEST25",
                Lastupdate = "2017-04-11",
                Description = "The value in the data element 'Sampling year' (sampY) should be equal to 2016;",
                Error = "sampY is different from 2016;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: yes);
            outcome.Passed = sampY == "2016";
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;
        public Outcome PEST10_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //exprRes;
            //fatPerc;



            var outcome = new Outcome
            {
                Name = "PEST10",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;",
                Error = "WARNING: fat percentage in milk of animal origin on whole weight basis is not reported; EFSA will assume a fat content equal to 4%;",
                Type = "warning",
                Passed = true
            };


#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("P1020000A");
            c.Add("P1020010A");
            c.Add("P1020020A");
            c.Add("P1020030A");
            c.Add("P1020040A");
            c.Add("P1020990A");

            if (c.Any(l => l == sample.Element("prodCode").Value))
            {
                if (sample.Element("exprRes").Value == "B001A" && sample.Element("fatPerc") == null)
                {
                    outcome.Passed = false;
                }



            }


            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;
        public Outcome PEST11_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resVal;

            var outcome = new Outcome
            {
                Name = "PEST11",
                Description = "If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;",
                Error = "resVal is reported, though resType is qualitative value (binary);",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("resType").Value == "BIN")
            {
                outcome.Passed = sample.Element("resVal") == null;
            }
            return outcome;
        }

        ///If the value in the data element 'Programme legal  reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        public Outcome PEST12_OBSOLETE(XElement sample)
        {
            // <checkedDataElements>;
            //progLegalRef;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST12",
                Description = "If the value in the data element 'Programme legal  reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);",
                Error = "prodCode is not a baby food, though progLegalRef is samples of food products falling under Directive 2006/125/EC;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("progLegalRef").Value == "N028A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                c.Add("PX100000A");
                c.Add("PX100001A");
                c.Add("PX100003A");
                c.Add("PX100004A");
                c.Add("PX100005A");

                outcome.Passed = c.Any(x => x == sample.Element("prodCode").Value);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);
        public Outcome PEST13_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //progLegalRef;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST13",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);",
                Error = "progLegalRef is not samples of food products falling under Directive 2006/125/EC, though prodCode is a baby food;",
                Type = "error",
                Passed = true
            };


#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("PX100000A");
            c.Add("PX100001A");
            c.Add("PX100003A");
            c.Add("PX100004A");
            c.Add("PX100005A");

            if (c.Any(x => x == sample.Element("prodCode").Value) && sample.Element("progLegalRef").Value != "N028A")
            {
                outcome.Passed = false;
            }

            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then a value in the data element 'Percentage of fat in the original sample' (fatPerc) must be reported;
        public Outcome PEST14_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //fatPerc;

            var outcome = new Outcome
            {
                Name = "PEST14",
                Description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then a value in the data element 'Percentage of fat in the original sample' (fatPerc) must be reported;",
                Error = "fatPerc is missing, though exprRes is fat weight;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("exprRes").Value == "B003A")
            {
                outcome.Passed = sample.Element("fatPerc") != null;
            }

            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        public Outcome PEST15_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST15",
                Description = "If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);",
                Error = "WARNING: prodCode is not a baby food, though exprRes is reconstituted product;",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (sample.Element("exprRes").Value == "B007A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                c.Add("PX100000A");
                c.Add("PX100001A");
                c.Add("PX100003A");
                c.Add("PX100004A");
                c.Add("PX100005A");
                outcome.Passed = c.Any(x => x == sample.Element("prodCode").Value);
            }

            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Expression of result' (exprRes) should be 'Reconstituted product' (B007A);
        public Outcome PEST16_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST16",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Expression of result' (exprRes) should be 'Reconstituted product' (B007A);",
                Error = "WARNING: exprRes is not reconstituted product, though prodCode is a baby food. Please verify that the sample taken is ready-for-consumption and does not require reconstitution/dilution before consumption;",
                Type = "warning",
                Passed = true
            };


#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("PX100000A");
            c.Add("PX100001A");
            c.Add("PX100003A");
            c.Add("PX100004A");
            c.Add("PX100005A");
            outcome.Passed = c.Any(x => x == sample.Element("prodCode").Value);


            if (c.Any(x => x == sample.Element("prodCode").Value))
            {
                outcome.Passed = sample.Element("exprRes").Value == "B007A";
            }


            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);
        public Outcome PEST17_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST17",
                Description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);",
                Error = "prodCode is not milk of animal origin or egg samples, though exprRes is fat weight;",
                Type = "error",
                Passed = true
            };

            /*
            <value>P1020000A</value>
              <value>P1020010A</value>
              <value>P1020020A</value>
              <value>P1020030A</value>
              <value>P1020040A</value>
              <value>P1020990A</value>
              <value>P1030000A</value>
              <value>P1030010A</value>
              <value>P1030020A</value>
              <value>P1030030A</value>
              <value>P1030040A</value>
              <value>P1030990A</value>


            */


            if (sample.Element("exprRes").Value == "B003A")
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var c = new List<String>();
#pragma warning restore IDE0028 // Simplify collection initialization
                c.Add("P1020000A");
                c.Add("P1020010A");
                c.Add("P1020020A");
                c.Add("P1020030A");
                c.Add("P1020040A");
                c.Add("P1020990A");
                c.Add("P1030000A");
                c.Add("P1030010A");
                c.Add("P1030020A");
                c.Add("P1030030A");
                c.Add("P1030040A");
                c.Add("P1030990A");

                outcome.Passed = c.Any(x => x == sample.Element("prodCode").Value);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), then the value in the data element 'Expression of result' (exprRes) should be 'Fat weight' (B003A);
        public Outcome PEST18_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome
            {
                Name = "PEST18",
                Description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), then the value in the data element 'Expression of result' (exprRes) should be 'Fat weight' (B003A);",
                Error = "exprRes is not fat weight, though prodCode is milk of animal origin or egg samples;",
                Type = "warning",
                Passed = true
            };

            //Logik
#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<String>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("P1020000A");
            c.Add("P1020010A");
            c.Add("P1020020A");
            c.Add("P1020030A");
            c.Add("P1020040A");
            c.Add("P1020990A");
            c.Add("P1030000A");
            c.Add("P1030010A");
            c.Add("P1030020A");
            c.Add("P1030030A");
            c.Add("P1030040A");
            c.Add("P1030990A");



            if (c.Any(x => x == sample.Element("prodCode").Value))
            {
                outcome.Passed = sample.Element("exprRes").Value == "B003A";
            }

            return outcome;
        }


        ///If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;
        public Outcome PEST19_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //exprRes;
            //fatPerc;

            var outcome = new Outcome
            {
                Name = "PEST19",
                Description = "If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;",
                Error = "WARNING: fat percentage in egg samples on whole weight basis is not reported; EFSA will assume a fat content equal to 10%;",
                Type = "warning",
                Passed = true
            };

#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("P1030000A");
            c.Add("P1030010A");
            c.Add("P1030020A");
            c.Add("P1030030A");
            c.Add("P1030040A");
            c.Add("P1030990A");

            if (c.Any(x => x == sample.Element("prodCode").Value) && sample.Element("exprRes").Value == "B001A")
            {
                outcome.Passed = sample.Element("fatPerc") != null;
            }

            return outcome;
        }

        ///The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);
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

        ///The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);
        public Outcome PEST20_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodTreat;

            var outcome = new Outcome
            {
                Name = "PEST20",
                Description = "The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);",
                Error = "WARNING: prodTreat is not among those recommended in EFSA guidance;",
                Type = "warning",
                Passed = true
            };
#pragma warning disable IDE0028 // Simplify collection initialization
            var c = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
            c.Add("T100A");
            c.Add("T101A");
            c.Add("T102A");
            c.Add("T103A");
            c.Add("T104A");
            c.Add("T110A");
            c.Add("T111A");
            c.Add("T112A");
            c.Add("T113A");
            c.Add("T114A");
            c.Add("T116A");
            c.Add("T120A");
            c.Add("T121A");
            c.Add("T122A");
            c.Add("T123A");
            c.Add("T124A");
            c.Add("T125A");
            c.Add("T128A");
            c.Add("T129A");
            c.Add("T130A");
            c.Add("T131A");
            c.Add("T132A");
            c.Add("T134A");
            c.Add("T136A");
            c.Add("T148A");
            c.Add("T150A");
            c.Add("T999A");
            c.Add("T998A");

            outcome.Passed = c.Any(x => x == sample.Element("prodTreat").Value);

            return outcome;
        }

        ///The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A);
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
        ///The value in the data element 'Expression of result' (exprRes) must be equal to 'Whole weight' (B001A);
        public Outcome PEST22_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;

            var outcome = new Outcome
            {
                Name = "PEST22",
                Description = "The value in the data element 'Expression of result' (exprRes) must be equal to 'Whole weight' (B001A);",
                Error = "exprRes is different from whole weight;",
                Type = "error",
                Passed = true
            };

            //Logik
            outcome.Passed = sample.Element("exprRes").Value == "B001A";
            return outcome;
        }

        ///The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);
        public Outcome PEST23_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //resUnit;

            var outcome = new Outcome
            {
                Name = "PEST23",
                Description = "The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);",
                Error = "resUnit is not reported in milligram per kilogram;",
                Type = "error",
                Passed = true
            };

            outcome.Passed = sample.Element("resUnit").Value == "G061A";

            return outcome;
        }

        ///The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);
        public Outcome PEST24_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //resLegalLimitType;

            var outcome = new Outcome
            {
                Name = "PEST24",
                Description = "The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);",
                Error = "WARNING: resLegalLimitType is different from MRL and national or local limit;",
                Type = "warning",
                Passed = true
            };

            var resLegalLimitType = sample.Element("resLegalLimitType");
            outcome.Passed = resLegalLimitType == null || resLegalLimitType.Value == "W002A" || resLegalLimitType.Value == "W990A";

            return outcome;
        }
        ///The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);
        public Outcome PEST25_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //labAccred;

            var outcome = new Outcome
            {
                Name = "PEST25",
                Description = "The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);",
                Error = "labAccred is not accredited or none;",
                Type = "error",
                Passed = true
            };

            //Logik
            var labAccred = sample.Element("labAccred").Value;
            outcome.Passed = labAccred == "L001A" || labAccred == "L003A";

            return outcome;
        }

        ///The value in the data element 'Parameter code' (paramCode) should be different from 'Not in list' (RF-XXXX-XXX-XXX);
        public Outcome PEST26(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string) sample.Element("paramCode");

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

        ///If the value in the data element 'Programme type' (progType) is equal to 'Official (National) programme' (K005A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A), or 'Suspect sampling' (ST30A);
        public Outcome PEST_sampInfo005(XElement sample)
        {
            var progType = sample.Element("progType").Value;
            var progLegalRef = sample.Element("progLegalRef").Value;
            var progSampStrategy = sample.Element("progSampStrategy").Value;


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
        public Outcome PEST_sampInfo009(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var progLegalRef = sample.Element("progLegalRef").Value;
            var progSampStrategy = sample.Element("progSampStrategy").Value;

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
        public Outcome PEST_sampInfo018(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var progLegalRef = sample.Element("progLegalRef").Value;
            var progSampStrategy = sample.Element("progSampStrategy").Value;

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
        public Outcome PEST_sampInfo019(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var progLegalRef = sample.Element("progLegalRef").Value;
            var progSampStrategy = sample.Element("progSampStrategy").Value;

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

        ///A value in the data element 'Expression of result' (exprRes) must be reported;
        public Outcome CHEM01(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;

            var outcome = new Outcome
            {
                Description = "A value in the data element 'Expression of result' (exprRes) must be reported;",
                Error = "exprRes is missing, though mandatory;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("exprRes") == null)
            {
                outcome.Passed = false;

            }
            return outcome;
        }
        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in (origCountry) can only be equal to 'China' (CN), or 'Dominican Republic' (DO), or 'Egypt' (EG), or 'Kenya' (KE), or 'Cambodia' (KH), or 'Thailand' (TH), or 'Turkey' (TR), or 'Viet Nam' (VN);
        public Outcome PEST669_1(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;

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
        public Outcome PEST669_CN(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_DO(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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


        public Outcome PEST669_DO_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_EG(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_KE(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_EG_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Kenya' (KE), then the value in 'Product code' (prodCode) can only be equal to 'Peas (with pods)' (P0260030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);

        ///If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Celery leaves' (P0256030A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);
        public Outcome PEST669_KH(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_KH_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_TH(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_TH_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_TR(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_TR_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_VN(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodTreat = sample.Element("prodTreat").Value;

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
        public Outcome PEST669_VN_a(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_VN_b(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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
        public Outcome PEST669_VN_c(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;
            var origCountry = sample.Element("origCountry").Value;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

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

        ///If the value in the data element 'Product code' (prodCode) is equal to 'Not in list' (XXXXXXA), or the value in the data element 'Parameter code' (paramCode) is equal to 'Not in list' (RF-XXXX-XXX-XXX), then the validation of the matrix tool is not possible;
        public Outcome MTX_W06(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode").Value;
            var paramCode = sample.Element("paramCode").Value;

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


        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is greater than the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Less than or equal to maximum permissible quantities' (J002A);
        public Outcome MRL_01(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode").Value;
            var paramCode = sample.Element("paramCode").Value;
            var resType = sample.Element("resType").Value;
            var prodTreat = sample.Element("prodTreat").Value;
            var resVal = sample.Element("resVal").Value;
            var resEvaluation = sample.Element("resEvaluation").Value;
            var legalLimit = (string) sample.Element("resLegalLimit");

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
            if (resType == "VAL" && (prodTreat  == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".",",") ) >= decimal.Parse(legalLimit.Replace(".",",")))
            {
                outcome.Passed = resEvaluation != "J002A";

            }
            return outcome;
        }

        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is less than or equal to the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Greater than maximum permissible quantities' (J003A) and 'Compliant due to measurement uncertainty' (J031A);
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
            //Logik
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".", ",")) <= decimal.Parse(legalLimit.Replace(".", ",")))
            {
                outcome.Passed = (resEvaluation != "(J003A" && resEvaluation != "J031A");

            }
            return outcome;
        }


        ///If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has changed during the year, then a value in the data element 'Result legal limit' (resLegalLimit) must be reported;
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
                outcome.Passed = String.IsNullOrEmpty(resLegalLimit) == false;

            }
            return outcome;
        }

        ///A value i
        ///
        /// n the data element 'EFSA Product Code' (EFSAProdCode) must be reported;
        public Outcome CHEM02(XElement sample)
        {
            // <checkedDataElements>;
            //EFSAProdCode;

            var outcome = new Outcome
            {
                Description = "A value in the data element 'EFSA Product Code' (EFSAProdCode) must be reported;",
                Error = "EFSAProdCode is missing, though mandatory",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("EFSAProdCode") == null)
            {
                outcome.Passed = false;
            }
            return outcome;
        }

        ///A value in the data element 'Product full text description' (prodText) must be reported;
        public Outcome CHEM03(XElement sample)
        {
            // <checkedDataElements>;
            //prodText;

            var outcome = new Outcome
            {
                Description = "A value in the data element 'Product full text description' (prodText) must be reported;",
                Error = "prodText is missing, though mandatory;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("prodText") == null)
            {
                outcome.Passed = false;
            }

            return outcome;
        }

        ///A value in the data element 'Analytical method code' (anMethCode) must be reported;
        public Outcome CHEM04(XElement sample)
        {
            // <checkedDataElements>;
            //anMethCode;

            var outcome = new Outcome
            {
                Description = "A value in the data element 'Analytical method code' (anMethCode) must be reported;",
                Error = "anMethCode is missing, though mandatory;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("anMethCode") == null)
            {
                outcome.Passed = false;
            }

            return outcome;
        }

        ///A value in the data element 'Result LOQ' (resLOQ) should be reported;
        public Outcome CHEM05(XElement sample)
        {
            // <checkedDataElements>;
            //resLOQ;

            var outcome = new Outcome
            {
                Description = "A value in the data element 'Result LOQ' (resLOQ) should be reported;",
                Error = "WARNING: resLOQ is missing, though recommended;",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                outcome.Passed = false;
            }

            return outcome;
        }

        ///The value in the data element 'Result value recovery' (resValRec) must be less than 200;
        public Outcome CHEM06(XElement sample)
        {
            // <checkedDataElements>;
            //resValRec;

            var outcome = new Outcome
            {
                Description = "The value in the data element 'Result value recovery' (resValRec) must be less than 200;",
                Error = "resValRec is greater than 200;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("resValRec") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValRec").Value.Replace(".", ",")) > 200)
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Parameter code' (paramCode) is 'Acrylamide' (RF-00000410-ORG), then the value in the data element 'Product comment' (prodCom) must contain a specific product code;
        public Outcome CHEM07(XElement sample)
        {
            // <checkedDataElements>;
            //paramCode;
            //prodCom;

            var outcome = new Outcome
            {
                Description = "If the value in the data element 'Parameter code' (paramCode) is 'Acrylamide' (RF-00000410-ORG), then the value in the data element 'Product comment' (prodCom) must contain a specific product code;",
                Error = "prodCom does not contain specific code, though the parameter is acrylamide;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("paramCode").Value == "RF-00000410-ORG")
            {

                outcome.Passed = sample.Element("prodComm") != null;
            }

            return outcome;
        }

        ///If the value in the data element 'Parameter code' (paramCode) is 'Furan' (RF-00000073-ORG), then the value in the data element 'Product comment' (prodCom) must contain the text 'purchase' or 'consume';
        public Outcome CHEM08(XElement sample)
        {
            // <checkedDataElements>;
            //paramCode;
            //prodCom;

            var outcome = new Outcome
            {
                Description = "If the value in the data element 'Parameter code' (paramCode) is 'Furan' (RF-00000073-ORG), then the value in the data element 'Product comment' (prodCom) must contain the text 'purchase' or 'consume';",
                Error = "prodCom does not contain 'purchase' or 'consume', though the parameter is furan;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("paramCode").Value == "RF-00000073-ORG")
            {
                outcome.Passed = sample.Element("prodComm") != null && (sample.Element("prodComm").Value == "purchase" || sample.Element("prodComm").Value == "consume");
            }

            return outcome;
        }
        /// <summary>
        /// SSD2
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public Outcome GBR3a(XElement sample)
        {
            // <checkedDataElements>;
            //sampId;
            //sampEventId;
            //repCountry;
            //sampCountry;
            //sampArea;
            //repYear;
            //sampY;
            //sampM;
            //sampD;
            //sampSize;
            //sampUnitSize;
            //sampInfo;
            //sampMatType;
            //sampMatCode;
            //sampMatText;
            //origCountry;
            //origArea;
            //origFishAreaCode;
            //origFishAreaText;
            //procCountry;
            //procArea;
            //sampMatInfo;

            var outcome = new Outcome
            {
                Description = "If a value is reported in at least one of the following data elements: 'Reporting country' (repCountry), 'Country of sampling' (sampCountry), 'Area of sampling' (sampArea), 'Reporting year' (repYear), 'Year of sampling' (sampY), 'Month of sampling' (sampM), 'Day of sampling' (sampD), 'Sample taken size' (sampSize), 'Sample taken size unit' (sampUnitSize), 'Additional Sample taken information' (sampInfo), 'Type of matrix' (sampMatType), 'Coded description of the matrix of the sample taken' (sampMatCode), 'Text description of the matrix of the sample taken' (sampMatText), 'Country of origin of the sample taken' (origCountry), 'Area of origin of the sample taken' (origArea), 'Area of origin for fisheries or aquaculture activities code of the sample taken' (origFishAreaCode), 'Area of origin for fisheries or aquaculture activities text of the sample taken' (origFishAreaText), 'Country of processing of the sample taken' (procCountry), 'Area of processing of the sample taken' (procArea), 'Additional information on the matrix sampled' (sampMatInfo), then a 'Sample taken identification code' (sampId) must be reported;",
                Error = "sampId is missing, though at least one descriptor for the sample taken or the matrix sampled (sections D, E) or the sampEventId is reported;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("sampId") != null || sample.Element("sampEventId") != null || sample.Element("repCountry") != null || sample.Element("sampCountry") != null || sample.Element("sampArea") != null || sample.Element("repYear") != null || sample.Element("sampY") != null || sample.Element("sampM") != null || sample.Element("sampD") != null || sample.Element("sampSize") != null || sample.Element("sampUnitSize") != null || sample.Element("sampInfo") != null || sample.Element("sampMatType") != null || sample.Element("sampMatCode") != null || sample.Element("sampMatText") != null || sample.Element("origCountry") != null || sample.Element("origArea") != null || sample.Element("origFishAreaCode") != null || sample.Element("origFishAreaText") != null || sample.Element("procCountry") != null || sample.Element("procArea") != null || sample.Element("sampMatInfo") != null)
            {
                outcome.Passed = sample.Element("sampId") != null;
            }
            else
            {
                outcome.Passed = false;
            }
            return outcome;
        }
        ///If a value is reported in at least one of the following data elements: 'Sample analysis reference time' (sampAnRefTime), 'Year of analysis' (analysisY), 'Month of analysis' (analysisM), 'Day of analysis' (analysisD), 'Additional information on the sample analysed' (sampAnInfo), 'Coded description of the analysed matrix' (anMatCode), 'Text description of the matrix analysed' (anMatText), 'Additional information on the analysed matrix ' (anMatInfo), then a 'Sample analysed identification code' (sampAnId) must be reported;
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
                Description = "If a value is reported in at least one of the following data elements: 'Sample analysis reference time' (sampAnRefTime), 'Year of analysis' (analysisY), 'Month of analysis' (analysisM), 'Day of analysis' (analysisD), 'Additional information on the sample analysed' (sampAnInfo), 'Coded description of the analysed matrix' (anMatCode), 'Text description of the matrix analysed' (anMatText), 'Additional information on the analysed matrix ' (anMatInfo), then a 'Sample analysed identification code' (sampAnId) must be reported;",
                Error = "sampAnId is missing, though at least one descriptor for the sample analysed or the matrix analysed (sections F, G) or the sampId is reported;",
                Type = "error",
                Passed = true
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

        ///If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed identification code' (sampAnId) and 'Sample analysed portion sequence' (anPortSeq) must be reported;
        public Outcome GBR5a(XElement sample)
        {
            // <checkedDataElements>;
            //sampAnId;
            //anPortSeq;
            //anPortSize;
            //anPortSizeUnit;
            //anPortInfo;

            var outcome = new Outcome
            {
                Description = "If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed identification code' (sampAnId) and 'Sample analysed portion sequence' (anPortSeq) must be reported;",
                Error = "sampAnId or anPortSeq is missing, though at least one descriptor for the sample analysed portion (section H) is reported;",
                Type = "error",
                Passed = true
            };

            //Logik
            if (sample.Element("sampAnId") != null || sample.Element("anPortSeq") != null || sample.Element("anPortSize") != null || sample.Element("anPortSizeUnit") != null || sample.Element("anPortInfo") != null)
            {
                outcome.Passed = sample.Element("sampAnId") != null;
            }
            else
            {
                outcome.Passed = false;
            }
            return outcome;
        }
        ///If a value is reported in at least one of the following descriptor data elements: 'Coded description of the isolate' (isolParamCode), 'Text description of the isolate' (isolParamText), 'Additional information on the isolate' (isolInfo), then a 'Isolate identification' (isolId) must be reported;
        public Outcome GBR6a(XElement sample)
        {
            // <checkedDataElements>;
            //isolId;
            //isolParamCode;
            //isolParamText;
            //isolInfo;

            var outcome = new Outcome
            {
                Description = "If a value is reported in at least one of the following descriptor data elements: 'Coded description of the isolate' (isolParamCode), 'Text description of the isolate' (isolParamText), 'Additional information on the isolate' (isolInfo), then a 'Isolate identification' (isolId) must be reported;",
                Error = "isolId is missing, though at least one descriptor for the isolate (section I) is reported;",
                Type = "error",
                Passed = true
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


        ///The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);
        ///The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);
        public Outcome VMPR001(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = sample.Element("progLegalRef").Value;

            var outcome = new Outcome
            {
                Name = "VMPR001",
                Lastupdate = "2017-01-09",
                Description = "The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);",
                Error = "WARNING: progLegalRef is different from Council Directive (EC) No 23/1996;",
                Type = "warning",
                Passed = true
            };

            //Logik
            if (progLegalRef != "N247A")
            {
                outcome.Passed = false;
            }
            
            return outcome;
        }

        ///The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A) and 'Not specified' (STXXA);
        public Outcome VMPR002(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");

            var outcome = new Outcome
            {
                Name = "VMPR002",
                Lastupdate = "2017-01-10",
                Description = "The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A) and 'Not specified' (STXXA);",
                Error = "sampStrategy is not specified, or equal to census, though these values should not be reported;",
                Type = "error",
                Passed = true
            };

            //Logik (ignore null: no);
#pragma warning disable IDE0028 // Simplify collection initialization
            var sampStrategys = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                sampStrategys.Add("ST50A");
                sampStrategys.Add("STXXA");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.Passed = false;
                }

            return outcome;
        }
        ///The value in the data element 'Sampling Strategy' (sampStrategy) should be different from 'Objective sampling' (ST10A), and 'Convenient sampling' (ST40A);
        public Outcome VMPR003(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");

            var outcome = new Outcome
            {
                Name = "VMPR003",
                Lastupdate = "2017-01-10",
                Description = "The value in the data element 'Sampling Strategy' (sampStrategy) should be different from 'Objective sampling' (ST10A), and 'Convenient sampling' (ST40A);",
                Error = "WARNING: sampStrategy is objective or conveniente sampling, though these values should not be reported;",
                Type = "warning",
                Passed = true
            };

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(sampStrategy))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var sampStrategys = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                sampStrategys.Add("ST10A");
                sampStrategys.Add("ST40A");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.Passed = false;
                }
            }
            return outcome;
        }

        ///If the value in 'Link To Original Sample’ (sampInfo.origSampId) is reported, i.e. a follow-up sample, then the value in 'Sampling Strategy’ (sampStrategy) must be 'suspect sampling' (ST30A);
        public Outcome VMPR004(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");
            var sampInforigSampId = (string)sample.Element("sampInfo.origSampId");

            var outcome = new Outcome
            {
                Name = "VMPR004",
                Lastupdate = "2016-05-10",
                Description = "If the value in 'Link To Original Sample’ (sampInfo.origSampId) is reported, i.e. a follow-up sample, then the value in 'Sampling Strategy’ (sampStrategy) must be 'suspect sampling' (ST30A);",
                Error = "sampStrategy is not suspect sampling, though sampInfo.origSampId is reported;",
                Type = "error",
                Passed = true
            };


            if (!String.IsNullOrEmpty(sampInforigSampId))
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                var sampStrategys = new List<string>();
#pragma warning restore IDE0028 // Simplify collection initialization
                sampStrategys.Add("ST30A");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.Passed = false;
                }

            }
            return outcome;
        }
        public string GetCountryFromAreaCode(string code)
        {

            XDocument xDoc = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("Nuts_rule_check"));
            var country = xDoc.Descendants("area").Where(x => x.Attribute("code").Value == code).Select(x => x.Attribute("country").Value).FirstOrDefault();
            return country;

        }


        public decimal? ParseDec(string s)
        {
            s = s.Replace(',', '.');
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmp) ? tmp: default(decimal?);
        }

        public List<Outcome> RunChemRules(XElement result)
        {
#pragma warning disable IDE0028 // Simplify collection initialization
            var list = new List<Outcome>();
#pragma warning restore IDE0028 // Simplify collection initialization
            list.Add(CHEM01(result));
            list.Add(CHEM02(result));
            list.Add(CHEM03(result));
            list.Add(CHEM04(result));
            list.Add(CHEM05(result));
            list.Add(CHEM06(result));
            list.Add(CHEM07(result));
            list.Add(CHEM08(result));
            return list;
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
