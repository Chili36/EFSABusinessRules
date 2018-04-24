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
        ///If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique;</description>
        public Outcome BR01A(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name= "BR01A";
    
            if (sample.Element("paramCode").Value != "RF-XXXX-XXX-XXX")
            {
                var list = new List<XElement>();
                list.Add(sample.Element("paramCode"));
                list.Add(sample.Element("labSampCode"));
                list.Add(sample.Element("labSubSampCode"));
                outcome.passed = UniqueValues(list); //Alla värden är unika;
            }
            else
            {
                outcome.passed = true;
            }
            outcome.description = "If the value in the data element 'Parameter code' (paramCode) is different from 'Not in list' (RF-XXXX-XXX-XXX), then the combination of values in the data elements 'Parameter code' (paramCode), 'Laboratory sample code' (labSampCode), 'Laboratory sub-sample code' (labSubSampCode) must be unique";
            outcome.error = "The combination of values in paramCode, labSampCode and labSubSampCode is not unique";
            return outcome;
        }


        ///If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;
        public Outcome BR02A_01(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name= "BR02A_01";

            outcome.description = "If the value in 'Day of analysis' (analysisD) is reported, then a value in 'Month of analysis' (analysisM) must be reported;";
            outcome.error = "analysisM is missing, though analysisD is reported;";

            //Element att kontrollera
            var elementAttKontrollera = new List<XElement>();
            elementAttKontrollera.Add(sample.Element("analysisD"));
            elementAttKontrollera.Add(sample.Element("analysisM"));

            //Logik

            if (sample.Element("analysisD") != null)
            {
                outcome.passed = (sample.Element("analysisM") != null);
            }
            else
            {
                outcome.passed = true;
            }
            return outcome;
        }


        public Outcome BR02A_02(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "BR02A_02";
            outcome.description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;";
            outcome.error = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, resLegalLimit) is reported;";

            //Element att kontrollera
            var elementAttKontrollera = new List<XElement>();
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
                outcome.passed = sample.Element("resUnit") != null && sample.Element("resUnit").Value != null;
            }
            else
            {
                outcome.passed = true;
            }

            return outcome;
        }

        ///If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;
        public Outcome BR02A_03(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "BR02A_03";
            outcome.description = "If the value in 'Day of production' (prodD) is reported, then a value in 'Month of Production' (prodM) must be reported;";
            outcome.error = "prodM is missing, though prodD is reported;";
            outcome.passed = true;

            //Logik
            var prodD = sample.Element("prodD");
            if (prodD != null)
            {
                //Verify
                var prodM = sample.Element("prodM");
                if (prodM == null)
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }

        ///If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;
        public Outcome BR02A_04(XElement sample)
        {
            var outcome = new Outcome();

            outcome.name = "BR02A_04";
            outcome.description = "If the value in 'Day of expiry' (expiryD) is reported, then a value in 'Month of expiry' (expiryM) must be reported;";
            outcome.error = "expiryM is missing, though expiryD is reported;";
            outcome.passed = true;

            //Logik
            var expiryD = sample.Element("expiryD");
            if (expiryD != null)
            {
                //Verify
                var expiryM = sample.Element("expiryM");
                if (expiryM == null)
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }

        // Define other methods and classes here
        ///If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Legal Limit for the result' (resLegalLimit), then a value in 'Result unit' (resUnit) must be reported;

        ///If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;
        public Outcome BR02A_05(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "BR02A_05";
            outcome.description = "If the value in 'Day of sampling' (sampD) is reported, then a value in 'Month of sampling' (sampM) must be reported;";
            outcome.error = "sampM is missing, though sampD is reported;";
            outcome.passed = true;

            //Logik
            var sampD = sample.Element("sampD");
            if (sampD != null)
            {
                //Verify
                var sampM = sample.Element("sampM");
                if (sampM == null)
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }

        ///If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;
        public Outcome BR02A_06(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "If the value in 'Lot size' (lotSize) is reported, then a value in 'Lot size unit' (lotSizeUnit) must be reported;";
            outcome.error = "lotSizeUnit is missing, though lotSize is reported;";
            outcome.passed = true;

            //Logik
            var lotSize = sample.Element("lotSize");
            if (lotSize != null)
            {
                //Verify
                var lotSizeUnit = sample.Element("lotSizeUnit");
                if (lotSizeUnit == null)
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }

        ///If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;
        public Outcome BR02A_07(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "If the value in 'Legal Limit for the result' (resLegalLimit) is reported, then a value in 'Type of legal limit' (resLegalLimitType) should be reported;";
            outcome.error = "WARNING: resLegalLimitType is missing, though resLegalLimit is reported;";
            outcome.passed = true;

            //Villkor
            var resLegalLimit = sample.Element("resLegalLimit");
            if (resLegalLimit != null)
            {
                //Verify
                var resLegalLimitType = sample.Element("resLegalLimitType");
                if (resLegalLimitType == null)
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }

        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        ///The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;
        public Outcome BR03A_01(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of analysis' (analysisY) must be less than or equal to the current year;";
            outcome.error = "analysisY is greater than the current year;";
            outcome.passed = true;

            //Logik
            var analysisY = sample.Element("analysisY").Value;
            if (int.Parse(analysisY) <= 2016)
            {
                //Condition is true
                outcome.passed = true;
            }

            return outcome;
        }

        ///The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);
        public Outcome BR03A_02(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);";
            outcome.error = "resLOD is greater than resLOQ;";
            outcome.passed = true;

            //Logik

            if (sample.Element("resLOD") == null || sample.Element("resLOQ") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOD").Value.Replace(".", ",")) > decimal.Parse(sample.Element("resLOQ").Value.Replace(".", ",")))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);
        public Outcome BR03A_03(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'CC alpha' (CCalpha) must be less than or equal to the value in 'CC beta' (CCbeta);";
            outcome.error = "CCalpha is greater than CCbeta;";
            outcome.passed = true;

            //Logik

            if (sample.Element("CCalpha") == null || sample.Element("CCbeta") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha").Value) > decimal.Parse(sample.Element("CCbeta").Value))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Result value recovery' (resValRec) must be greater than 0;
        public Outcome BR03A_04(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Result value recovery' (resValRec) must be greater than 0;";
            outcome.error = "resValRec is less than or equal to 0;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resValRec") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValRec").Value.Replace(".", ",")) <= 0)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Year of production' (prodY) must be less than or equal to the current year;
        public Outcome BR03A_05(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of production' (prodY) must be less than or equal to the current year;";
            outcome.error = "prodY is greater than the current year;";
            outcome.passed = true;

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
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);
        public Outcome BR03A_06(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of expiry' (expiryY);";
            outcome.error = "prodY is greater than expiryY;";
            outcome.passed = true;

            //Logik
            if (sample.Element("prodY") == null || sample.Element("expiryY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("expiryY").Value))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }
        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);
        public Outcome BR03A_07(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of sampling' (sampY);";
            outcome.error = "prodY is greater than sampY;";
            outcome.passed = true;

            //Logik
            if (sample.Element("prodY") == null || sample.Element("sampY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("sampY").Value))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);
        public Outcome BR03A_08(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of production' (prodY) must be less than or equal to the value in 'Year of analysis' (analysisY);";
            outcome.error = "prodY is greater than analysisY;";
            outcome.passed = true;

            //Logik
            if (sample.Element("prodY") == null || sample.Element("analysisY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("prodY").Value) > decimal.Parse(sample.Element("analysisY").Value))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }
        public Outcome BR03A_09(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of sampling' (sampY) must be less than or equal to the current year;";
            outcome.error = "sampY is greater than the current year;";
            outcome.passed = true;

            //Logik
            if (sample.Element("sampY") == null)
            {
                return outcome;
            }
            else
            {
                if (int.Parse(sample.Element("sampY").Value) > 2016)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }
        ///The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);
        public Outcome BR03A_10(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Year of sampling' (sampY) must be less than or equal to the value in 'Year of analysis' (analysisY);";
            outcome.error = "sampY is greater than analysisY;";
            outcome.passed = true;

            //Logik
            if (sample.Element("sampY") == null || sample.Element("analysisY") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("sampY").Value) > decimal.Parse(sample.Element("analysisY").Value))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }


        ///The value in 'Result LOD' (resLOD) must be greater than 0;
        public Outcome BR03A_11(XElement sample)
        {
            var outcome = new Outcome();
            outcome.description = "The value in 'Result LOD' (resLOD) must be greater than 0;";
            outcome.error = "resLOD is less than or equal to 0;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resLOD") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOD").Value.Replace(".", ",")) <= 0)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Result LOQ' (resLOQ) must be greater than 0;
        public Outcome BR03A_12(XElement sample)
        {


            var outcome = new Outcome();
            outcome.description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;";
            outcome.error = "resLOQ is less than or equal to 0;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resLOQ").Value.Replace(".", ",")) <= 0)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'CC alpha' (CCalpha) must be greater than 0;
        public Outcome BR03A_13(XElement sample)
        {
            // <checkedDataElements>;
            //CCalpha;

            var outcome = new Outcome();
            outcome.description = "The value in 'CC alpha' (CCalpha) must be greater than 0;";
            outcome.error = "CCalpha is less than or equal to 0;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("CCalpha") == null)
            {
                outcome.passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("CCalpha").Value) <= 0 )
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'CC beta' (CCbeta) must be greater than 0;
        public Outcome BR03A_14(XElement sample)
        {
            // <checkedDataElements>;
            //CCbeta;

            var outcome = new Outcome();
            outcome.description = "The value in 'CC beta' (CCbeta) must be greater than 0;";
            outcome.error = "CCbeta is less than or equal to 0;";
            outcome.type = "error";
            outcome.passed = true;

            if (sample.Element("CCbeta") == null)
            {
                outcome.passed = true;
            }
            else
            {
                    outcome.passed = decimal.Parse(sample.Element("CCbeta").Value) <= 0;
            }
            return outcome;
        }

        ///The value in 'Result value' (resVal) must be greater than 0;
        public Outcome BR03A_15(XElement sample)
        {
            // <checkedDataElements>;
            //resVal;

            var outcome = new Outcome();
            outcome.description = "The value in 'Result value' (resVal) must be greater than 0;";
            outcome.error = "resVal is less than or equal to 0;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("resVal") == null)
            {
                outcome.passed = true;
            }
            else
            {
                if (ParseDec((string)sample.Element("resVal")) <= 0)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;
        public Outcome BR03A_16(XElement sample)
        {
            // <checkedDataElements>;
            //resValUncertSD;

            var outcome = new Outcome();
            outcome.description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than 0;";
            outcome.error = "resValUncertSD is less than or equal to 0;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("resValUncertSD") == null)
            {
                outcome.passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValUncertSD").Value) <= 0)
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }


        ///The value in 'Result value uncertainty' (resValUncert) must be greater than 0;
        public Outcome BR03A_17(XElement sample)
        {
            // <checkedDataElements>;
            //resValUncert;

            var outcome = new Outcome();
            outcome.description = "The value in 'Result value uncertainty' (resValUncert) must be greater than 0;";
            outcome.error = "resValUncert is less than or equal to 0;";
            outcome.type = "error";
            outcome.passed = true;
            

            //Logik
            if (sample.Element("resValUncert") == null)
            {
                outcome.passed = true;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValUncert").Value) >= 0 )
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;";
            outcome.error = "resVal is reported, though resType is non detected value (below LOD);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if ((string) sample.Element("resType") == "LOD" )
            {
                outcome.passed = String.IsNullOrEmpty((string)sample.Element("resVal"));
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

            var outcome = new Outcome();
            outcome.description = "If the value in 'Result value' (resVal) is greater than the value in 'Legal Limit for the result' (resLegalLimit), then the value in 'Evaluation of the result' (resEvaluation) must be different from 'less than or equal to maximum permissible quantities' (J002A);";
            outcome.error = "resEvaluation is less than or equal to maximum permissible quantities, though resVal is greater than resLegalLimit;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if ( String.IsNullOrEmpty((string)sample.Element("resType")))
            {
                outcome.passed = false;
            }
            else
            {
                outcome.passed = (string)sample.Element("resEvaluation") != "J002A";
            }
            return outcome;
        }




        ///The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);
        public Outcome BR07A_01(XElement sample)
        {
            // <checkedDataElements>;
            //sampArea;
            //sampCountry;

            var outcome = new Outcome();
            outcome.description = "The 'Area of sampling' (sampArea) must be within the 'Country of sampling' (sampCountry);";
            outcome.error = "sampArea is not within sampCountry;";
            outcome.type = "error";
            outcome.passed = true;

            

            //Logik
            if ( sample.Element("sampArea") == null )
            {
                outcome.passed = true;
            }
            else
            {
                outcome.passed = new Validator().GetCountryFromAreaCode((string) sample.Element("sampArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }

        ///The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);
        public Outcome BR07A_02(XElement sample)
        {
            // <checkedDataElements>;
            //origArea;
            //origCountry;

            var outcome = new Outcome();
            outcome.description = "The 'Area of origin of the product' (origArea) must be within the 'Country of origin of the product' (origCountry);";
            outcome.error = "origArea is not within origCountry;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("origArea") == null)
            {
                outcome.passed = true;
            }
            else
            {
                outcome.passed = new Validator().GetCountryFromAreaCode((string)sample.Element("origArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }


        ///The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);
        public Outcome BR07A_03(XElement sample)
        {
            // <checkedDataElements>;
            //procArea;
            //procCountry;

            var outcome = new Outcome();
            outcome.description = "The 'Area of processing' (procArea) must be within the 'Country of processing' (procCountry);";
            outcome.error = "procArea is not within procCountry;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("procArea") == null)
            {
                outcome.passed = true;
            }
            else
            {
                outcome.passed = new Validator().GetCountryFromAreaCode((string)sample.Element("procArea")) == (string)sample.Element("sampCountry");
            }
            return outcome;
        }


        ///If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;
        public Outcome BR08A_05(XElement sample)
        {
            // <checkedDataElements>;
            //resType;
            //resLOQ;

            var outcome = new Outcome();
            outcome.description = "If the value in the data element 'Type of result' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then a value in 'Result LOQ' (resLOQ) must be reported;";
            outcome.error = "resLOQ is missing, though resType is non quantified value;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if ((string)sample.Element("resType") == "LOQ" && sample.Element("resLOQ") == null)
            {
                outcome.passed = false;
            }

            return outcome;
        }



        ///The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;
        public Outcome BR09A_09(XElement sample)
        {
            // <checkedDataElements>;
            //moistPerc;

            var outcome = new Outcome();
            outcome.description = "The value in the data element 'Percentage of moisture in the original sample' (moistPerc) must be between 0 and 100;";
            outcome.error = "moistPerc is not between 0 and 100;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("moistPerc") == null)
            {
                //Ignore null
                outcome.passed = true;
            }
            else
            {
                if ((decimal.Parse(sample.Element("moistPerc").Value.Replace(".", ",")) < 1) && (decimal.Parse(sample.Element("moistPerc").Value.Replace(".", ",")) > 99))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;";
            outcome.error = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Skapa ett datum från analysår

            var str_dag = (string)sample.Element("analysisD");
            var str_manad = (string)sample.Element("analysisM");
            var str_ar = (string)sample.Element("analysisM");

            if (str_dag == null || str_manad == null || str_ar == null)
            {
                outcome.passed = false;
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
                outcome.passed = false;

                Console.WriteLine("Analysisdate is {0}", analysisDate.ToString());
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

            var outcome = new Outcome();
            outcome.description = "If a value is reported in at least one of the following data elements: 'Sample analysis reference time' (sampAnRefTime), 'Year of analysis' (analysisY), 'Month of analysis' (analysisM), 'Day of analysis' (analysisD), 'Additional information on the sample analysed' (sampAnInfo), 'Coded description of the analysed matrix' (anMatCode), 'Text description of the matrix analysed' (anMatText), 'Additional information on the analysed matrix ' (anMatInfo), then a 'Sample analysed identification code' (sampAnId) must be reported;";
            outcome.error = "sampAnId is missing, though at least one descriptor for the sample analysed or the matrix analysed (sections F, G) or the sampId is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("sampAnId") != null || sample.Element("sampAnRefTime") != null || sample.Element("analysisY") != null || sample.Element("analysisM") != null || sample.Element("analysisD") != null || sample.Element("sampAnInfo") != null || sample.Element("anMatCode") != null || sample.Element("anMatText") != null || sample.Element("anMatInfo") != null)
            {
                outcome.passed = sample.Element("sampAnId") != null;
            }
            else
            {
                outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.description = "If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed identification code' (sampAnId) and 'Sample analysed portion sequence' (anPortSeq) must be reported;";
            outcome.error = "sampAnId or anPortSeq is missing, though at least one descriptor for the sample analysed portion (section H) is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("sampAnId") != null || sample.Element("anPortSeq") != null || sample.Element("anPortSize") != null || sample.Element("anPortSizeUnit") != null || sample.Element("anPortInfo") != null)
            {
                outcome.passed = sample.Element("sampAnId") != null;
            }
            else
            {
                outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.description = "If a value is reported in at least one of the following descriptor data elements: 'Coded description of the isolate' (isolParamCode), 'Text description of the isolate' (isolParamText), 'Additional information on the isolate' (isolInfo), then a 'Isolate identification' (isolId) must be reported;";
            outcome.error = "isolId is missing, though at least one descriptor for the isolate (section I) is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("isolId") != null || sample.Element("isolParamCode") != null || sample.Element("isolParamText") != null || sample.Element("isolInfo") != null)
            {
                outcome.passed = sample.Element("isolId") != null;
            }
            else
            {
                outcome.passed = true;
            }
            return outcome;
        }
        public Outcome GBR17(XElement sample)
        {
            // <checkedDataElements>;
            var paramCodebase = (string)sample.Element("paramCode").Element("base");
            var paramText = (string)sample.Element("paramText");

            var outcome = new Outcome();
            outcome.name = "GBR17";
            outcome.lastupdate = "2017-03-31";
            outcome.description = "If the reported value in the 'Coded description of the parameter' (paramCode.base) is 'Not in list' (RF-XXXX-XXX-XXX), then a text must be reported in the 'Parameter text' (paramText);";
            outcome.error = "paramText is missing, though mandatory if paramCode.base is 'Not in list' (RF-XXXX-XXX-XXX);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (paramCodebase == "RF-XXXX-XXX-XXX")
            {
                outcome.passed = !String.IsNullOrEmpty(paramText);
            }
            return outcome;
        }
        ///If the value in the 'Expression of result type' (exprResType) is 'Dry matter' (B002A), then a value must be reported in the 'Percentage of moisture ' (exprResPerc.moistPerc);
        public Outcome GBR23(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercmoistPerc = (string)sample.Element("exprResPerc.moistPerc");
            var exprResType = (string)sample.Element("exprResType");

            var outcome = new Outcome();
            outcome.name = "GBR23";
            outcome.lastupdate = "2017-04-27";
            outcome.description = "If the value in the 'Expression of result type' (exprResType) is 'Dry matter' (B002A), then a value must be reported in the 'Percentage of moisture ' (exprResPerc.moistPerc);";
            outcome.error = "exprResPerc.moistPerc is missing, though mandatory if exprResType is expressed on 'dry matter' basis;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (exprResType == "B002A")
            {
                outcome.passed = !String.IsNullOrEmpty(exprResPercmoistPerc);
            }
            return outcome;
        }


        ///If a 'Sample taken size' (sampSize) is reported, then a 'Sample taken size unit' (sampSizeUnit) must be reported;
        public Outcome GBR25(XElement sample)
        {
            // <checkedDataElements>;
            var sampSize = (string)sample.Element("sampSize");
            var sampSizeUnit = (string)sample.Element("sampSizeUnit");

            var outcome = new Outcome();
            outcome.name = "GBR25";
            outcome.lastupdate = "2017-09-07";
            outcome.description = "If a 'Sample taken size' (sampSize) is reported, then a 'Sample taken size unit' (sampSizeUnit) must be reported;";
            outcome.error = "sampSizeUnit is missing, though sampSize is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampSize))
            {
                outcome.passed = !String.IsNullOrEmpty(sampSizeUnit);

            }
            return outcome;
        }



        ///If the value reported in 'Type of result' (resType) is different from 'Qualitative Value (Binary)' (BIN) (i.e. not a qualitative value), then a 'Result unit' (resUnit) must be reported;
        public Outcome GBR27(XElement sample)
        {
            // <checkedDataElements>;
            var resUnit = (string)sample.Element("resUnit");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR27";
            outcome.lastupdate = "2017-09-20";
            outcome.description = "If the value reported in 'Type of result' (resType) is different from 'Qualitative Value (Binary)' (BIN) (i.e. not a qualitative value), then a 'Result unit' (resUnit) must be reported;";
            outcome.error = "resUnit is missing, though resType is not 'Qualitative Value (Binary)' (BIN);";
            outcome.type = "error";
            outcome.passed = true;
            if (resType != "BIN")
            {
                outcome.passed = !string.IsNullOrEmpty(resUnit);

            }
            return outcome;
        }


        ///If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;
        public Outcome GBR31(XElement sample)
        {
            // <checkedDataElements>;
            var evalLowLimit = (string)sample.Element("evalLowLimit");
            var evalHighLimit = (string)sample.Element("evalHighLimit");
            var evalLimitType = (string)sample.Element("evalLimitType");

            var outcome = new Outcome();
            outcome.name = "GBR31";
            outcome.lastupdate = "2017-04-24";
            outcome.description = "If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;";
            outcome.error = "evalLowLimit is missing, though evalHighLimit is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (evalLimitType != "W001A" && !string.IsNullOrEmpty(evalHighLimit))
            {

                outcome.passed = String.IsNullOrEmpty(evalLowLimit);

            }
            return outcome;
        }

        ///The value in 'CC alpha' (CCalpha) must be less than the value in 'CC beta' (CCbeta);
        public Outcome GBR42(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");
            var CCbeta = (string)sample.Element("CCbeta");

            var outcome = new Outcome();
            outcome.name = "GBR42";
            outcome.lastupdate = "2017-11-16";
            outcome.description = "The value in 'CC alpha' (CCalpha) must be less than the value in 'CC beta' (CCbeta);";
            outcome.error = "WARNING: CCalpha is not less than CCbeta;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            decimal _ccalpha;
            decimal _ccbeta;
        

    if (decimal.TryParse(CCalpha, out _ccalpha) && decimal.TryParse(CCbeta, out _ccbeta))
            {
                outcome.passed = _ccalpha < _ccbeta;

            }


            return outcome;
        }


        ///The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), should be less than or equal to the current date;
        public Outcome GBR66(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome();
            outcome.name = "GBR66";
            outcome.lastupdate = "2017-07-05";
            outcome.description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), should be less than or equal to the current date;";
            outcome.error = "WARNING: the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY, is not less than or equal to the current date;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);

            DateTime _date;
            string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };

            //DateTime.TryParseExact("2018" + "02" + "65","yyyyMMdd", null,  DateTimeStyles.None, out _date).Dump();

            if (DateTime.TryParseExact(sampMatInfoexpiryY + sampMatInfoexpiryM + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out _date))
            {
                outcome.passed = _date < DateTime.Now;

            }
            return outcome;
        }




        public decimal? ParseDec(string s)
        {
            decimal tmp;
            s = s.Replace(',', '.');
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp) ? tmp: default(decimal?);
        }

      

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

            decimal r;

            decimal.TryParse(s.Replace(".",","), out r);
            return r;
        }
    }


}
