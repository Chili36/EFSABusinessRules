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
/// Version 0.01 October 2016
/// </summary>
    public class Validator
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
                outcome.passed =  GetCountryFromAreaCode((string) sample.Element("sampArea")) == (string)sample.Element("sampCountry");
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
                outcome.passed = GetCountryFromAreaCode((string)sample.Element("origArea")) == (string)sample.Element("sampCountry");
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
                outcome.passed = GetCountryFromAreaCode((string)sample.Element("procArea")) == (string)sample.Element("sampCountry");
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




        ///If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported;
        public Outcome PEST01(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode").Value;
            var prodText = sample.Element("prodText").Value;

            var outcome = new Outcome();
            outcome.name = "PEST01";
            outcome.lastupdate = "2017-04-11";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is equal to 'Honey and other apicultural products' (P1040000A) and a sample different from 'Honey' is analysed, then a value in the data element 'Product text' (prodText) should be reported;";
            outcome.error = "WARNING: when prodCode reported is honey and other apicultural products and the concerned sample is not honey at such (e.g. royal jelly, pollen, etc.), then prodText should be provided;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (prodCode == "P1040000A")
            {
                outcome.passed = String.IsNullOrEmpty(prodText) == false;
            }
           
            return outcome;
        }
        ///A value in the data element 'Result LOQ' (resLOQ) must be reported;
        public Outcome PEST02_OLD(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "PEST02";
            outcome.description = "A value in the data element 'Result LOQ' (resLOQ) must be reported;";
            outcome.error = "resLOQ is missing, though mandatory;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                outcome.passed = false;
            }
            return outcome;
        }
        ///A value in the data element 'Evaluation of the result' (resEvaluation) must be reported;
        public Outcome PEST03_OBSOLETE(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "PEST03";
            outcome.description = "A value in the data element 'Evaluation of the result' (resEvaluation) must be reported;";
            outcome.error = "resEvaluation is missing, though mandatory;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resEvaluation") == null)
            {
                outcome.passed = false;
            }
            return outcome;
        }

        ///A value in the data element 'Method of production' (prodProdMeth) must be reported;
        public Outcome PEST04_OLD(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "PEST04";
            outcome.description = "A value in the data element 'Method of production' (prodProdMeth) must be reported;";
            outcome.error = "prodProdMeth is missing, though mandatory;";
            outcome.passed = true;

            //Logik
            if (sample.Element("prodProdMeth") == null)
            {
                outcome.passed = false;
            }
            return outcome;
        }

        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';
        public Outcome PEST05_OLD(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "PEST05";
            outcome.description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';";
            outcome.error = "resType is different from VAL, though resEvaluation is greater than maximum permissible quantities or compliant due to measurement uncertainty;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resEvaluation").Value == "J003A" || sample.Element("resEvaluation").Value == "J031A")
            {
                outcome.passed = sample.Element("resType").Value == "VAL";
            }
            return outcome;
        }

        ///If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);
        public Outcome PEST06_OLD(XElement sample)
        {
            var outcome = new Outcome();
            outcome.name = "PEST06";
            outcome.description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);";
            outcome.error = "resVal is less than resLegalLimit, though resEvaluation is greater than maximum permissible quantities, or compliant due to measurement uncertainty;";
            outcome.passed = true;

            //Logik
            if (sample.Element("resEvaluation").Value == "J003A" || sample.Element("resEvaluation").Value == "J031A")
            {
                outcome.passed = decimal.Parse(sample.Element("resVal").Value.Replace(".", ",")) > decimal.Parse(sample.Element("resLegalLimit").Value.Replace(".", ","));
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);
        public Outcome PEST04(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.name = "PEST04";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);";
            outcome.error = "prodTreat is not processed, though prodCode is a baby food;";
            outcome.type = "error";
            outcome.passed = true;

            var prodCodes = new List<string>();

            prodCodes.Add("PX100000A");
            prodCodes.Add("PX100001A");
            prodCodes.Add("PX100003A");
            prodCodes.Add("PX100004A");
            prodCodes.Add("PX100005A");

            if (prodCodes.Contains(prodCode))

            {
                outcome.passed = prodTreat == "T100A";
            }
            return outcome;
        }
        ///If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A);
        public Outcome PEST07(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = (string)sample.Element("prodCode");
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.name = "PEST07";
            outcome.lastupdate = "2017-04-11";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is equal to 'Pulses (dry)' (P0300000A), or 'Beans (dry)' (P0300010A), or 'Lentils (dry)' (P0300020A), or 'Peas (dry)' (P0300030A), or 'Lupins (dry)' (P0300040A), or 'Other pulses (dry)' (P0300990A), then the value in the data element 'Product treatment' (prodTreat) can't be equal to 'Dehydration' (T131A);";
            outcome.error = "prodTreat is dehydration, though this value is not allowed when prodCode belongs to the 'Pulses (dry seeds)' food group;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            var prodCodes = new List<string>();
            prodCodes.Add("P0300000A");
            prodCodes.Add("P0300010A");
            prodCodes.Add("P0300020A");
            prodCodes.Add("P0300030A");
            prodCodes.Add("P0300040A");
            prodCodes.Add("P0300990A");
            if (prodCodes.Contains(prodCode))
            {
                var prodTreats = new List<string>();
                prodTreats.Add("T131A");
                if (prodTreats.Contains(prodTreat))
                {
                    outcome.passed = false;
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



            var list = new List<string>();
            list.Add("PX100000A");
            list.Add("PX100001A");
            list.Add("PX100003A");
            list.Add("PX100004A");
            list.Add("PX100005A");


            var outcome = new Outcome();
            outcome.name = "PEST07";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Processed' (T100A);";
            outcome.error = "prodTreat is not processed, though prodCode is a baby food;";
            outcome.passed = true;

            if (list.Any(l => l == sample.Element("prodCode").Value))
            {
                outcome.passed = sample.Element("prodTreat").Value == "T100A";
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
            outcome.values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.values.Add(Tuple.Create("prodTreat", (string)sample.Element("prodTreat")));
            outcome.name = "PEST05";
            outcome.lastupdate = "2017-04-11";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Milk pasteurisation' (T150A), or 'Freezing' (T998A), or 'Concentration' (T136A), or 'Unprocessed' (T999A);";
            outcome.error = "prodTreat is not dehydration, churning, milk pasteurisation, freezing, concentration or unprocessed, though prodCode is milk of animal origin;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            var prodCodes = new List<string>();
            prodCodes.Add("P1020000A");
            prodCodes.Add("P1020010A");
            prodCodes.Add("P1020020A");
            prodCodes.Add("P1020030A");
            prodCodes.Add("P1020040A");
            prodCodes.Add("P1020990A");
            if (prodCodes.Contains(prodCode))
            {
                var prodTreats = new List<string>();
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
                outcome.passed = prodTreats.Contains(prodTreat);

            }
            return outcome;
        }

        ///If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        public Outcome PEST08(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = (string)sample.Element("progLegalRef");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome();
            outcome.name = "PEST08";
            outcome.lastupdate = "2016-04-06";
            outcome.description = "If the value in the data element 'Programme legal reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);";
            outcome.error = "prodCode is not a baby food, though progLegalRef is samples of food products falling under Directive 2006/125/EC or 2006/141/EC;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progLegalRef == "N028A")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("PX100000A");
                prodCodes.Add("PX100001A");
                prodCodes.Add("PX100003A");
                prodCodes.Add("PX100004A");
                prodCodes.Add("PX100005A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST08";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), then the value in the data element 'Product treatment' (prodTreat) must be equal to 'Dehydration' (T131A), or 'Churning' (T134A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A);";
            outcome.error = "prodTreat is not dehydration, churning, milk pasteurisation, or unprocessed, though prodCode is milk of animal origin;";
            outcome.passed = true;

            var a = new List<string>();
            a.Add("P1020000A");
            a.Add("P1020010A");
            a.Add("P1020020A");
            a.Add("P1020030A");
            a.Add("P1020040A");
            a.Add("P1020990A");

            var b = new List<string>();
            b.Add("T131A");
            b.Add("T134A");
            b.Add("T150A");
            b.Add("T999A");

            if (a.Any(l => l == sample.Element("prodCode").Value))
            {
                outcome.passed = b.Any(l => l == sample.Element("prodTreat").Value);
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
     

            var outcome = new Outcome();
            outcome.name = "PEST06";
            outcome.lastupdate = "2017-07-04";
            outcome.description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), or 'Churning' (T134A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), or 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or 'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);";
            outcome.error = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik

            var prodtreats = new List<string>();
            prodtreats.Add("T150A");
            prodtreats.Add("T134A");
            prodtreats.Add("T152A");
            prodtreats.Add("T153A");
            prodtreats.Add("T154A");
            prodtreats.Add("T155A");


            if (prodtreats.Contains(prodTreat))
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


        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);
        public Outcome PEST09(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = (string)sample.Element("progLegalRef");
            var prodCode = (string)sample.Element("prodCode");

            var outcome = new Outcome();
            outcome.name = "PEST09";
            outcome.values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.lastupdate = "2016-04-06";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);";
            outcome.error = "progLegalRef is not samples of food products falling under Directive 2006/125/EC or 2006/141/EC, though prodCode is a baby food;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
           
                var prodCodes = new List<string>();
                prodCodes.Add("PX100000A");
                prodCodes.Add("PX100001A");
                prodCodes.Add("PX100003A");
                prodCodes.Add("PX100004A");
                prodCodes.Add("PX100005A");
                if (prodCodes.Contains(prodCode))
                {
                    var progLegalRefs = new List<string>();
                    progLegalRefs.Add("N028A");
                    if (!progLegalRefs.Contains(progLegalRef))
                    {
                        outcome.passed = false;
                    }                
                }
            return outcome;
        }
        ///The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);
        public Outcome PEST10(XElement sample)
        {
            // <checkedDataElements>;
            var labAccred = (string)sample.Element("labAccred");

            var outcome = new Outcome();
            outcome.name = "PEST10";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);";
            outcome.error = "labAccred is not accredited or none;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            var labAccreds = new List<string>();
            labAccreds.Add("L001A");
            labAccreds.Add("L003A");
            if (!labAccreds.Contains(labAccred))
            {
                outcome.passed = false;
            }
            return outcome;
        }

       
        ///The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);
        public Outcome PEST11(XElement sample)
        {
            // <checkedDataElements>;
            var resUnit = (string)sample.Element("resUnit");

            var outcome = new Outcome();
            outcome.name = "PEST11";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);";
            outcome.error = "resUnit is not reported in milligram per kilogram;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
           
                var resUnits = new List<string>();
                resUnits.Add("G061A");
                if (!resUnits.Contains(resUnit))
                {
                    outcome.passed = false;
                }

            return outcome;
        }

        ///If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);
        public Outcome PEST09_OBSOLETE(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //prodTreat;

            var outcome = new Outcome();
            outcome.name = "PEST09";
            outcome.description = "If the value in the data element 'Product treatment' (prodTreat) is 'Milk pasteurisation' (T150A), then the value in the data element 'Product code' (prodCode) must be equal to 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A);";
            outcome.error = "prodCode is not milk of animal origin, though prodTreat is milk pasteurisation;";
            outcome.passed = true;



            var a = new List<string>();
            a.Add("P1020000A");
            a.Add("P1020010A");
            a.Add("P1020020A");
            a.Add("P1020030A");
            a.Add("P1020040A");
            a.Add("P1020990A");

            if (sample.Element("prodTreat").Value == "T150A")
            {
                outcome.passed = a.Any(l => l == sample.Element("prodCode").Value);
            }

            return outcome;
        }

        ///The value in the data element (exprRes) can only be equal to 'Whole weight' (B001A), or 'Fat basis' (B003A), or 'Reconstituted product' (B007A);
        public Outcome PEST12(XElement sample)
        {
            // <checkedDataElements>;
            var exprRes = (string)sample.Element("exprRes");

            var outcome = new Outcome();
            outcome.name = "PEST12";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "The value in the data element (exprRes) can only be equal to 'Whole weight' (B001A), or 'Fat basis' (B003A), or 'Reconstituted product' (B007A);";
            outcome.error = "exprRes is not whole weight, or fat basis, or reconstituted product;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (!string.IsNullOrEmpty(exprRes))
            { 
                var exprRess = new List<string>();
                exprRess.Add("B001A");
                exprRess.Add("B003A");
                exprRess.Add("B007A");
                if (!exprRess.Contains(exprRes) )
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST13";
            outcome.lastupdate = "2016-04-06";
            outcome.description = "If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby foods other than processed cereal-based foods' (PX100001A), or 'Processed cereal-based foods for infants and young children' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);";
            outcome.error = "WARNING: prodCode is not a baby food, though exprRes is reconstituted product;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (exprRes == "B007A")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("PX100000A");
                prodCodes.Add("PX100001A");
                prodCodes.Add("PX100003A");
                prodCodes.Add("PX100004A");
                prodCodes.Add("PX100005A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST14";
            outcome.lastupdate = "2016-04-06";
            outcome.description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);";
            outcome.error = "prodCode is not milk of animal origin or egg samples, though exprRes is fat weight;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (exprRes == "B003A")
            {
                var prodCodes = new List<string>();
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
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST15";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;";
            outcome.error = "WARNING: fat percentage in milk of animal origin on whole weight basis is not reported; EFSA will assume a fat content equal to 4%;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
                var prodCodes = new List<string>();
                prodCodes.Add("P1020000A");
                prodCodes.Add("P1020010A");
                prodCodes.Add("P1020020A");
                prodCodes.Add("P1020030A");
                prodCodes.Add("P1020040A");
                prodCodes.Add("P1020990A");
                if (prodCodes.Contains(prodCode) && exprRes == "B001A")
                {
                    outcome.passed = String.IsNullOrEmpty(fatPerc) == false;

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

            var outcome = new Outcome();
            outcome.name = "PEST16";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;";
            outcome.error = "WARNING: fat percentage in egg samples on whole weight basis is not reported; EFSA will assume a fat content equal to 10%;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
             var prodCodes = new List<string>();
                prodCodes.Add("P1030000A");
                prodCodes.Add("P1030010A");
                prodCodes.Add("P1030020A");
                prodCodes.Add("P1030030A");
                prodCodes.Add("P1030040A");
                prodCodes.Add("P1030990A");
                if (prodCodes.Contains(prodCode) && exprRes == "B001A")
                {
                    outcome.passed = String.IsNullOrEmpty(fatPerc) == false; ;
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

            outcome.values.Add(Tuple.Create("resEvaluation", (string)sample.Element("resEvaluation")));
            outcome.values.Add(Tuple.Create("resType", (string)sample.Element("resType")));

            outcome.name = "PEST17";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Type of result' (resType) must be equal to 'VAL';";
            outcome.error = "resType is different from VAL, though resEvaluation is 'greater than maximum permissible quantities' or 'compliant due to measurement uncertainty';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var resEvaluations = new List<string>();
            resEvaluations.Add("J003A");
            resEvaluations.Add("J031A");
            if (resEvaluations.Contains(resEvaluation))
            {
                outcome.passed = resType == "VAL";
            }
            return outcome;
        }

        ///The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);
        public Outcome PEST18(XElement sample)
        {
            // <checkedDataElements>;
            var resLegalLimitType = (string)sample.Element("resLegalLimitType");

            var outcome = new Outcome();
            outcome.values.Add(Tuple.Create("resLegalLimitType", (string)sample.Element("resLegalLimitType")));
            outcome.name = "PEST18";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);";
            outcome.error = "WARNING: resLegalLimitType is different from MRL and national or local limit;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
           if (!string.IsNullOrEmpty(resLegalLimitType))
            { 
                var resLegalLimitTypes = new List<string>();
                resLegalLimitTypes.Add("W002A");
                resLegalLimitTypes.Add("W990A");
                if (!resLegalLimitTypes.Contains(resLegalLimitType))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST19";
            outcome.lastupdate = "2016-04-25";
            outcome.description = "If the value in the data element 'Evaluation of the result' (resEvaluation) is equal to 'greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), then the value in 'Result value' (resVal) must be greater than 'Legal Limit for the result' (resLegalLimit);";
            outcome.error = "resVal is less than or equal to resLegalLimit, though resEvaluation is 'greater than maximum permissible quantities', or 'compliant due to measurement uncertainty';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var resEvaluations = new List<string>();
            resEvaluations.Add("J003A");
            resEvaluations.Add("J031A");
            if (resEvaluations.Contains(resEvaluation))
            {
                outcome.passed = PD(resVal) > PD(resLegalLimit) ;
            }
          
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;
        public Outcome PEST20(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome();
            outcome.name = "PEST20";
            outcome.lastupdate = "2016-04-06";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;";
            outcome.error = "resVal is reported, though resType is qualitative value (binary);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "BIN")
            {
                outcome.passed = string.IsNullOrEmpty(resVal);
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
            outcome.values.Add(Tuple.Create("resType", (string)sample.Element("resType")));
            outcome.values.Add(Tuple.Create("resLOQ", (string)sample.Element("resLOQ")));
            outcome.values.Add(Tuple.Create("resVal", (string)sample.Element("resVal")));
            outcome.name = "PEST21";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Result type' (resType) is equal to 'Non Quantified Value (below LOQ)' (LOQ), then the value in the data element 'Result value' (resVal) should not be greater than the value in the data element 'Result LOQ' (resLOQ);";
            outcome.error = "WARNING: resType is LOQ for a result that contains a value greater than the reported LOQ;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (resType == "LOQ")
            {
                if (!String.IsNullOrEmpty(resVal))
                {
                    outcome.passed = PD(resLOQ) > PD(resVal);
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

            var outcome = new Outcome();
            outcome.name = "PEST22";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical Value' (VAL), then the value in the data element 'Result LOQ' (resLOQ) should not be greater than the value in the data element 'Result value' (resVal) (if the result is a positive detection, the result value cannot be below the reported LOQ);";
            outcome.error = "WARNING: resType is VAL for a result that contains a value less than the reported LOQ;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (resType == "VAL")
            {
                outcome.passed = PD(resVal) >= PD(resLOQ);
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

            var outcome = new Outcome();
            outcome.name = "PEST23";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Type of parameter' (paramType) is different from 'Part of a sum' (P002A) and the value in the data element 'Result value' (resVal) is greater than or equal to the value in the data element 'Legal Limit for the result' (resLegalLimit), then the value in the data element 'Evaluation of the result' (resEvaluation) should be different from 'Result not evaluated' (J029A);";
            outcome.error = "WARNING: where resVal greater than or equal to resLegalLimit, then the resEvaluation is not expected to be not evaluated;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resVal))
            { 
                if (paramType != "P002A" && PD(resVal) > PD(resLegalLimit))
                {
                    outcome.passed = resEvaluation != "J029A";

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

            var outcome = new Outcome();
            outcome.name = "PEST24";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Result value recovery corrected' (resValRecCorr) is equal to 'Yes' (Y), then a value in the data element 'Result value recovery' (resValRec) should be reported;";
            outcome.error = "WARNING: resValRec is missing, though resValRecCorr is reported; if the result is corrected for recovery the corrected value should be reported (mean recovery out of 70-120%);";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resValRecCorr == "Y")
            {
                outcome.passed = string.IsNullOrEmpty(resValRecCorr) == false;
            }
            
            return outcome;
        }

        ///The value in the data element 'Sampling year' (sampY) should be equal to 2016;
        public Outcome PEST25(XElement sample)
        {
            // <checkedDataElements>;
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome();
            outcome.name = "PEST25";
            outcome.lastupdate = "2017-04-11";
            outcome.description = "The value in the data element 'Sampling year' (sampY) should be equal to 2016;";
            outcome.error = "sampY is different from 2016;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            outcome.passed = sampY == "2016";
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;
        public Outcome PEST10_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodCode;
            //exprRes;
            //fatPerc;



            var outcome = new Outcome();
            outcome.name = "PEST10";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 4%;";
            outcome.error = "WARNING: fat percentage in milk of animal origin on whole weight basis is not reported; EFSA will assume a fat content equal to 4%;";
            outcome.type = "warning";
            outcome.passed = true;


            var c = new List<string>();
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
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST11";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN), then the data element 'Result value' (resVal) must be empty;";
            outcome.error = "resVal is reported, though resType is qualitative value (binary);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("resType").Value == "BIN")
            {
                outcome.passed = sample.Element("resVal") == null;
            }
            return outcome;
        }

        ///If the value in the data element 'Programme legal  reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        public Outcome PEST12_OBSOLETE(XElement sample)
        {
            // <checkedDataElements>;
            //progLegalRef;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST12";
            outcome.description = "If the value in the data element 'Programme legal  reference' (progLegalRef) is 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A), then the value in the data element 'Product code' (prodCode) must be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);";
            outcome.error = "prodCode is not a baby food, though progLegalRef is samples of food products falling under Directive 2006/125/EC;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("progLegalRef").Value == "N028A")
            {
                var c = new List<string>();
                c.Add("PX100000A");
                c.Add("PX100001A");
                c.Add("PX100003A");
                c.Add("PX100004A");
                c.Add("PX100005A");

                outcome.passed = c.Any(x => x == sample.Element("prodCode").Value);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);
        public Outcome PEST13_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //progLegalRef;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST13";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Programme legal  reference' (progLegalRef) must be 'Samples of food products falling under Directive 2006/125/EC or 2006/141/EC' (N028A);";
            outcome.error = "progLegalRef is not samples of food products falling under Directive 2006/125/EC, though prodCode is a baby food;";
            outcome.type = "error";
            outcome.passed = true;


            var c = new List<string>();
            c.Add("PX100000A");
            c.Add("PX100001A");
            c.Add("PX100003A");
            c.Add("PX100004A");
            c.Add("PX100005A");

            if (c.Any(x => x == sample.Element("prodCode").Value) && sample.Element("progLegalRef").Value != "N028A")
            {
                outcome.passed = false;
            }

            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then a value in the data element 'Percentage of fat in the original sample' (fatPerc) must be reported;
        public Outcome PEST14_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //fatPerc;

            var outcome = new Outcome();
            outcome.name = "PEST14";
            outcome.description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then a value in the data element 'Percentage of fat in the original sample' (fatPerc) must be reported;";
            outcome.error = "fatPerc is missing, though exprRes is fat weight;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("exprRes").Value == "B003A")
            {
                outcome.passed = sample.Element("fatPerc") != null;
            }

            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);
        public Outcome PEST15_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST15";
            outcome.description = "If the value in the data element 'Expression of result' (exprRes) is 'Reconstituted product' (B007A), then the value in the data element 'Product code' (prodCode) should be 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A);";
            outcome.error = "WARNING: prodCode is not a baby food, though exprRes is reconstituted product;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (sample.Element("exprRes").Value == "B007A")
            {
                var c = new List<string>();
                c.Add("PX100000A");
                c.Add("PX100001A");
                c.Add("PX100003A");
                c.Add("PX100004A");
                c.Add("PX100005A");
                outcome.passed = c.Any(x => x == sample.Element("prodCode").Value);
            }

            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Expression of result' (exprRes) should be 'Reconstituted product' (B007A);
        public Outcome PEST16_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST16";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Food for infants and young children' (PX100000A), or 'Baby food for infants and young childern' (PX100001A), or 'Processed cereal-based baby foods (e.g. cereal and pastas to be reconstituted with milk or other liquids)' (PX100003A), or 'Infant formulae' (PX100004A), or 'Follow-on formulae' (PX100005A), then the value in the data element 'Expression of result' (exprRes) should be 'Reconstituted product' (B007A);";
            outcome.error = "WARNING: exprRes is not reconstituted product, though prodCode is a baby food. Please verify that the sample taken is ready-for-consumption and does not require reconstitution/dilution before consumption;";
            outcome.type = "warning";
            outcome.passed = true;


            var c = new List<string>();
            c.Add("PX100000A");
            c.Add("PX100001A");
            c.Add("PX100003A");
            c.Add("PX100004A");
            c.Add("PX100005A");
            outcome.passed = c.Any(x => x == sample.Element("prodCode").Value);


            if (c.Any(x => x == sample.Element("prodCode").Value))
            {
                outcome.passed = sample.Element("exprRes").Value == "B007A";
            }


            return outcome;
        }

        ///If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);
        public Outcome PEST17_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST17";
            outcome.description = "If the value in the data element 'Expression of result' (exprRes) is 'Fat weight' (B003A), then the value in the data element 'Product code' (prodCode) must be 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A);";
            outcome.error = "prodCode is not milk of animal origin or egg samples, though exprRes is fat weight;";
            outcome.type = "error";
            outcome.passed = true;

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
                var c = new List<String>();
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

                outcome.passed = c.Any(x => x == sample.Element("prodCode").Value);
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), then the value in the data element 'Expression of result' (exprRes) should be 'Fat weight' (B003A);
        public Outcome PEST18_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;
            //prodCode;

            var outcome = new Outcome();
            outcome.name = "PEST18";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is 'Milk' (P1020000A), or  'Milk Cattle' (P1020010A), or 'Milk Sheep' (P1020020A), or 'Milk Goat' (P1020030A), or 'Milk Horse' (P1020040A), or 'Milk Others' (P1020990A), or 'Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), then the value in the data element 'Expression of result' (exprRes) should be 'Fat weight' (B003A);";
            outcome.error = "exprRes is not fat weight, though prodCode is milk of animal origin or egg samples;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            var c = new List<String>();
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
                outcome.passed = sample.Element("exprRes").Value == "B003A";
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

            var outcome = new Outcome();
            outcome.name = "PEST19";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is ''Bird eggs' (P1030000A), or 'Eggs Chicken' (P1030010A), or 'Eggs Duck' (P1030020A), or 'Eggs Goose' (P1030030A), or 'Eggs Quail' (P1030040A), or 'Eggs Others' (P1030990A), and the value in the data element 'Expression of result' (exprRes) is 'Whole weight' (B001A), and the value in 'Percentage of fat in the original sample' (fatPerc) is not reported, then EFSA will assume a fat content equal to 10%;";
            outcome.error = "WARNING: fat percentage in egg samples on whole weight basis is not reported; EFSA will assume a fat content equal to 10%;";
            outcome.type = "warning";
            outcome.passed = true;

            var c = new List<string>();
            c.Add("P1030000A");
            c.Add("P1030010A");
            c.Add("P1030020A");
            c.Add("P1030030A");
            c.Add("P1030040A");
            c.Add("P1030990A");

            if (c.Any(x => x == sample.Element("prodCode").Value) && sample.Element("exprRes").Value == "B001A")
            {
                outcome.passed = sample.Element("fatPerc") != null;
            }

            return outcome;
        }

        ///The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);
        public Outcome PEST03(XElement sample)
        {
            // <checkedDataElements>;
            var prodTreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.name = "PEST03";
            outcome.lastupdate = "2017-04-11";
            outcome.description = "The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Churning - butter' (T152A), or 'Churning - cheese' (T153A), 'Churning - cream' (T154A), or 'Churning - yougurt' (T155A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "WARNING: prodTreat is not among those recommended in EFSA guidance;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (!string.IsNullOrEmpty(prodTreat))
            {
                var tillstand = new List<string>();
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

                outcome.passed = tillstand.Contains(prodTreat);

            }
            return outcome;
        }

        ///The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);
        public Outcome PEST20_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //prodTreat;

            var outcome = new Outcome();
            outcome.name = "PEST20";
            outcome.description = "The value in the data element 'Product Treatment Code' (prodTreat) should be 'Processed' (T100A), or 'Peeling (inedible peel)' (T101A), or 'Peeling (edible peel)' (T102A), or 'Juicing' (T103A), or 'Oil production (Not Specified)' (T104A), or 'Milling (Not Specified)' (T110A), or 'Milling - unprocessed flour' (T111A), or 'Milling - refined flour' (T112A), or  'Milling - bran production' (T113A), or 'Polishing' (T114A), or Sugar production (Not Specified)' (T116A), or 'Canning' (T120A), or Preserving' (T121A), or 'Production of alcoholic beverages (Not Specified)' (T122A), or 'Wine production (Not Specified)' (T123A), or 'Wine production - white wine' (T124A), or 'Wine production - red wine cold process' (T125A), 'Cooking in water' (T128A), or 'Cooking in oil (Frying)' (T129A), or 'Cooking in air (Baking)' (T130A), or 'Dehydration' (T131A), or 'Fermentation' (T132A), or 'Churning' (T134A), or 'Concentration' (T136A), 'Wet-milling' (T148A), or 'Milk pasteurisation' (T150A), or 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "WARNING: prodTreat is not among those recommended in EFSA guidance;";
            outcome.type = "warning";
            outcome.passed = true;
            var c = new List<string>();
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

            outcome.passed = c.Any(x => x == sample.Element("prodTreat").Value);

            return outcome;
        }

        ///The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A);
        public Outcome PEST02(XElement sample)
        {
            // <checkedDataElements>;
            //prodTreat;

            var prodtreat = (string)sample.Element("prodTreat");

            var outcome = new Outcome();
            outcome.name = "PEST02";
            outcome.description = "The value in the data element 'Product treatment' (prodTreat) must be different from 'Unknown' (T899A);";
            outcome.error = "prodTreat is unknown;";
            outcome.type = "error";
            outcome.passed = true;

            outcome.passed = prodtreat != "T899A";

            return outcome;
        }
        ///The value in the data element 'Expression of result' (exprRes) must be equal to 'Whole weight' (B001A);
        public Outcome PEST22_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;

            var outcome = new Outcome();
            outcome.name = "PEST22";
            outcome.description = "The value in the data element 'Expression of result' (exprRes) must be equal to 'Whole weight' (B001A);";
            outcome.error = "exprRes is different from whole weight;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            outcome.passed = sample.Element("exprRes").Value == "B001A";
            return outcome;
        }

        ///The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);
        public Outcome PEST23_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //resUnit;

            var outcome = new Outcome();
            outcome.name = "PEST23";
            outcome.description = "The value in the data element 'Result unit' (resUnit) must be equal to 'Milligram per kilogram' (G061A);";
            outcome.error = "resUnit is not reported in milligram per kilogram;";
            outcome.type = "error";
            outcome.passed = true;

            outcome.passed = sample.Element("resUnit").Value == "G061A";

            return outcome;
        }

        ///The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);
        public Outcome PEST24_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //resLegalLimitType;

            var outcome = new Outcome();
            outcome.name = "PEST24";
            outcome.description = "The value in the data element 'Type of legal limit' (resLegalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'National or local limit' (W990A);";
            outcome.error = "WARNING: resLegalLimitType is different from MRL and national or local limit;";
            outcome.type = "warning";
            outcome.passed = true;

            var resLegalLimitType = sample.Element("resLegalLimitType");
            outcome.passed = resLegalLimitType == null || resLegalLimitType.Value == "W002A" || resLegalLimitType.Value == "W990A";

            return outcome;
        }
        ///The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);
        public Outcome PEST25_OLD(XElement sample)
        {
            // <checkedDataElements>;
            //labAccred;

            var outcome = new Outcome();
            outcome.name = "PEST25";
            outcome.description = "The value in the data element 'Laboratory accreditation' (labAccred) must be equal to 'Accredited' (L001A), or 'None' (L003A);";
            outcome.error = "labAccred is not accredited or none;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            var labAccred = sample.Element("labAccred").Value;
            outcome.passed = labAccred == "L001A" || labAccred == "L003A";

            return outcome;
        }

        ///The value in the data element 'Parameter code' (paramCode) should be different from 'Not in list' (RF-XXXX-XXX-XXX);
        public Outcome PEST26(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string) sample.Element("paramCode");

            var outcome = new Outcome();
            outcome.values.Add(Tuple.Create("paramCode", (string)sample.Element("paramCode")));
            outcome.name = "PEST26";
            outcome.lastupdate = "2017-04-17";
            outcome.description = "The value in the data element 'Parameter code' (paramCode) should be different from 'Not in list' (RF-XXXX-XXX-XXX);";
            outcome.error = "paramCode should be different from 'not in list'. Please contact catalogue@efsa.europa.eu to add the missing term;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (paramCode == "RF-XXXX-XXX-XXX")
            {
                outcome.passed = false;
            }
            return outcome;
        }

        ///If the value in the data element 'Programme type' (progType) is equal to 'Official (National) programme' (K005A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A), or 'Suspect sampling' (ST30A);
        public Outcome PEST_sampInfo005(XElement sample)
        {
            var progType = sample.Element("progType").Value;
            var progLegalRef = sample.Element("progLegalRef").Value;
            var progSampStrategy = sample.Element("progSampStrategy").Value; 
           

            var outcome = new Outcome();
            outcome.name = "PEST_sampInfo005";
            outcome.values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.values.Add(Tuple.Create("progSampStrategy", (string)sample.Element("progSampStrategy")));
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (National) programme' (K005A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A), or 'Suspect sampling' (ST30A);";
            outcome.error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K005A")
            {
                var list = new List<string>();
                list.Add("N027A");
                list.Add("N028A");
                list.Add("N247A");
                list.Add("N018A");

                var list2 = new List<string>();
                list2.Add("ST10A");
                list2.Add("ST20A");
                list2.Add("ST30A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.passed = false;
                }

            
                if (!list2.Contains(progSampStrategy))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST_sampInfo009";
            outcome.values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.values.Add(Tuple.Create("progLegalRef", (string)sample.Element("progLegalRef")));
            outcome.values.Add(Tuple.Create("progSampStrategy", (string)sample.Element("progSampStrategy")));
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (EU) programme' (K009A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);";
            outcome.error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K009A")
            {
                var list = new List<string>();
                list.Add("N027A");
                list.Add("N028A");

                var list2 = new List<string>();
                list2.Add("ST10A");
                list2.Add("ST20A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.passed = false;
                }
                if (!list2.Contains(progSampStrategy))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST_sampInfo018";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'Official (National and EU) programme' (K018A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), or 'Commission Directive (EC) No 125/2006/EC and 2006/141/EC' (N028A), or 'Council Directive (EC) No 23/1996 (amended)' (N247A), or 'Regulation (EC) No 882/2004 (amended)' (N018A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Objective sampling' (ST10A), or 'Selective sampling' (ST20A);";
            outcome.error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.type = "error";
            outcome.passed = true;

            if (progType == "K018A")
            {
                var list = new List<string>();
                list.Add("N027A");
                list.Add("N028A");
                list.Add("N247A");
                list.Add("N018A");

                var list2 = new List<string>();
                list.Add("ST10A");
                list.Add("ST20A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.passed = false;
                }
                if (list2.Contains(progSampStrategy))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST_sampInfo019";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in 'Programme legal reference' (progLegalRef) can only be equal to 'Regulation (EC) No 396/2005 (amended)' (N027A), and the value in 'Sampling strategy' (progSampStrategy) can only be equal to 'Suspect sampling' (ST30A);";
            outcome.error = "The combination of codes for progType, progLegalRef and progSampStrategy is not valid;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A")
            {
                var list = new List<string>();
                list.Add("N027A");
                
                var list2 = new List<string>();
                list.Add("ST30A");

                if (!list.Contains(progLegalRef))
                {
                    outcome.passed = false;
                }
                if (list2.Contains(progSampStrategy))
                {
                    outcome.passed = false;
                }
            }
            return outcome;
        }

        ///A value in the data element 'Expression of result' (exprRes) must be reported;
        public Outcome CHEM01(XElement sample)
        {
            // <checkedDataElements>;
            //exprRes;

            var outcome = new Outcome();
            outcome.description = "A value in the data element 'Expression of result' (exprRes) must be reported;";
            outcome.error = "exprRes is missing, though mandatory;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("exprRes") == null)
            {
                outcome.passed = false;

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
            outcome.values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.values.Add(Tuple.Create("origCountry", (string)sample.Element("origCountry")));
            outcome.lastupdate = "2017-06-16";
            outcome.name = "PEST669_1";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), then the value in (origCountry) can only be equal to 'China' (CN), or 'Dominican Republic' (DO), or 'Egypt' (EG), or 'Kenya' (KE), or 'Cambodia' (KH), or 'Thailand' (TH), or 'Turkey' (TR), or 'Viet Nam' (VN);";
            outcome.error = "origCountry is not a valid country code when progType reported is EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A")
            {
                var list = new List<string>();
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
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_CN";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'China' (CN), then the value in 'Product code' (prodCode) can only be equal to 'Broccoli' (P0241010A), or 'Teas' (P0610000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;
            outcome.lastupdate = "2017-03-15";



            //Logik

           if (progType == "K019A" && origCountry == "CN")
            {
                var produktkoder = new List<string>();
                produktkoder.Add("P0241010A");
                produktkoder.Add("P0610000A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                if (prodTreat != "T999A")
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_DO";
            outcome.lastupdate = "2017-03-15";
            outcome.values.Add(Tuple.Create("progType", (string)sample.Element("progType")));
            outcome.values.Add(Tuple.Create("origCountry", (string)sample.Element("origCountry")));
            outcome.values.Add(Tuple.Create("prodCode", (string)sample.Element("prodCode")));
            outcome.values.Add(Tuple.Create("prodTreat", (string)sample.Element("prodTreat")));
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Dominican Republic' (DO), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants'  (P0231030A), or 'Courgettes' (P0232030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "DO")
            {
                var produktkoder = new List<string>();
                produktkoder.Add("P0231030A");
                produktkoder.Add("P0232030A");
                produktkoder.Add("P0231020A");
                produktkoder.Add("P0260010A");

                var tillstand = new List<string>();
                tillstand.Add("T999A");
                tillstand.Add("T998A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.passed = false;
                }
               
                if (!tillstand.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_DO_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Dominican Republic' (DO), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "DO" && prodCode == "P0231020A")
            {
                outcome.passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
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

            var outcome = new Outcome();
            outcome.name = "PEST669_EG";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Strawberries'  (P0152000A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "EG")
            {
                var produktkoder = new List<string>();
                produktkoder.Add("P0231020A");
                produktkoder.Add("P0152000A");
                
                var tillstand = new List<string>();
                tillstand.Add("T999A");
                tillstand.Add("T998A");

                if (!produktkoder.Contains(prodCode))
                {
                    outcome.passed = false;
                }

                if (tillstand.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_KE";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Kenya' (KE), then the value in 'Product code' (prodCode) can only be equal to 'Peas (with pods)' (P0260030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "KE")
            {
                var produktkoder = new List<string>();
                var tillstand = new List<string>();
                tillstand.Add("T999A");
                produktkoder.Add("P0260030A");
                if (!produktkoder.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                if (tillstand.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_EG_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Egypt' (EG), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "EG" && prodCode == "P0231020A")
            {
                outcome.passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
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

            var outcome = new Outcome();
            outcome.name = "PEST669_KH";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Celery leaves' (P0256030A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "KH")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("P0231030A");
                prodCodes.Add("P0260010A");
                prodCodes.Add(" P0256030A");
               
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                var prodTreats = new List<string>();
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_KH_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Cambodia' (KH), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Chinese celery leaves';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is celery leaves and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "KH" && prodCode == "P0256030A")
            {
                outcome.passed = prodText.Contains("Chinese celery leaves");
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

            var outcome = new Outcome();
            outcome.name = "PEST669_TH";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), then the value in 'Product code' (prodCode) can only be equal to 'Aubergines/egg plants' (P0231030A), or 'Sweet peppers/bell peppers' (P0231020A), or 'Beans (with pods)' (P0260010A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "TH")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("P0231030A");
                prodCodes.Add("P0260010A");
                prodCodes.Add("P0231020A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                var prodTreats = new List<string>();
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_TH_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TH), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Chili peppers';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "TH" && prodCode == "P0231020A")
            {
                outcome.passed = prodText.Contains("Chili peppers");
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

            var outcome = new Outcome();
            outcome.name = "PEST669_TR";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), then the value in 'Product code' (prodCode) can only be equal to 'Sweet peppers/bell peppers' (P0231020A), or 'Grape leaves and similar species' (P0253000A), or 'Lemons' (P0110030A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), or 'Processed' (T100A), or 'Dehydration' (T131A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "TR")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("P0231020A");
                prodCodes.Add("P0253000A");
                prodCodes.Add("P0110030A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                var prodTreats = new List<string>();
                prodTreats.Add("T999A");
                prodTreats.Add("T998A");
                prodTreats.Add("T100A");
                prodTreats.Add("T131A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "PEST669_TR_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Thailand' (TR), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "TR" && prodCode == "P0231020A")
            {
                outcome.passed = prodText.Contains("Sweet/bell peppers");
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

            var outcome = new Outcome();
            outcome.name = "PEST669_VN";
            outcome.lastupdate = "2016-07-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), then the value in 'Product code' (prodCode) can only be equal to 'Basil and edible flowers' (P0256080A), or 'Celery leaves'  (P0256030A), or 'Prickly pears/cactus fruits' (P0163040A), or 'Okra/lady’s fingers' (P0231040A), or 'Parsley' (P0256040A), or 'Sweet peppers/bell peppers' (P0231020A), and the value in 'Product treatment' (prodTreat) can only be equal to 'Unprocessed' (T999A);";
            outcome.error = "The combination of codes for origCountry, prodCode and prodTreat is not valid for progType reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "VN")
            {
                var prodCodes = new List<string>();
                prodCodes.Add("P0256080A");
                prodCodes.Add("P0256030A");
                prodCodes.Add("P0163040A");
                prodCodes.Add("P0256080A");
                prodCodes.Add("P0231040A");
                prodCodes.Add("P0256040A");
                prodCodes.Add("P0231020A");
                if (!prodCodes.Contains(prodCode))
                {
                    outcome.passed = false;
                }
                var prodTreats = new List<string>();
                prodTreats.Add("T999A");
                if (!prodTreats.Contains(prodTreat))
                {
                    outcome.passed = false;

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

            var outcome = new Outcome();
            outcome.name = "PEST669_VN_a";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Basil and edible flowers' (P0256080A), then the value in 'Product text' (prodText) mus contain the string 'Basil' or 'Mint';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0256080A")
            {
                outcome.passed = (prodText.Contains("Basil") || prodText.Contains("Mint"));

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

            var outcome = new Outcome();
            outcome.name = "PEST669_VN_b";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Celery leaves' (P0256030A), then the value in 'Product text' (prodText) mus contain the string 'Coriander leaves';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0256030A")
            {
                outcome.passed = (prodText.Contains("Coriander leaves"));
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

            var outcome = new Outcome();
            outcome.name = "PEST669_VN_c";
            outcome.lastupdate = "2017-03-15";
            outcome.description = "If the value in the data element 'Programme type' (progType) is equal to 'EU increased control programme on imported food' (K019A), and the value in (origCountry) is 'Viet Nam' (VN), and the value in 'Product code' (prodCode) is 'Sweet peppers/bell peppers' (P0231020A), then the value in 'Product text' (prodText) mus contain the string 'Sweet/bell peppers' or 'Chili peppers';";
            outcome.error = "prodText doesn't contain the appropriate string when prodCode is peppers and progType is reported as EU increased control programme on imported food (Reg 669/2009);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (progType == "K019A" && origCountry == "VN" && prodCode == "P0231020A")
            {
                outcome.passed = (prodText.Contains("Sweet/bell peppers") || prodText.Contains("Chili peppers"));
            }
            return outcome;
        }

        ///If the value in the data element 'Product code' (prodCode) is equal to 'Not in list' (XXXXXXA), or the value in the data element 'Parameter code' (paramCode) is equal to 'Not in list' (RF-XXXX-XXX-XXX), then the validation of the matrix tool is not possible;
        public Outcome MTX_W06(XElement sample)
        {
            // <checkedDataElements>;
            var prodCode = sample.Element("prodCode").Value;
            var paramCode = sample.Element("paramCode").Value;

            var outcome = new Outcome();
            outcome.name = "MTX_W06";
            outcome.lastupdate = "2017-04-18";
            outcome.description = "If the value in the data element 'Product code' (prodCode) is equal to 'Not in list' (XXXXXXA), or the value in the data element 'Parameter code' (paramCode) is equal to 'Not in list' (RF-XXXX-XXX-XXX), then the validation of the matrix tool is not possible;";
            outcome.error = "WARNING: validation not possible (paramCode or prodCode are reported as Not in list);";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (prodCode == "XXXXXXA" || paramCode == "RF-XXXX-XXX-XXX")
            {
                outcome.passed = false;

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

            var outcome = new Outcome();
            outcome.name = "MRL_01";
            outcome.lastupdate = "2017-04-28";
            outcome.description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is greater than the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Less than or equal to maximum permissible quantities' (J002A);";
            outcome.error = "result evaluation is incorrect; result value exceeds the result legal limit;";
            outcome.type = "error";
            outcome.passed = true;

            

            //Logik
            if (resType == "VAL" && (prodTreat  == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".",",") ) >= decimal.Parse(legalLimit.Replace(".",",")))
            {
                outcome.passed = resEvaluation != "J002A";

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

            var outcome = new Outcome();
            outcome.name = "MRL_02";
            outcome.lastupdate = "2017-04-28";
            outcome.description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has not changed during the year, and the value in 'Result value' (resVal) is less than or equal to the MRL, then the value in the data element 'Result evaluation' (resEvaluation) must be different from 'Greater than maximum permissible quantities' (J003A) and 'Compliant due to measurement uncertainty' (J031A);";
            outcome.error = "result evaluation is incorrect; result value is equal or below the result legal limit;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            //Logik
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A") && decimal.Parse(resVal.Replace(".", ",")) <= decimal.Parse(legalLimit.Replace(".", ",")))
            {
                outcome.passed = (resEvaluation != "(J003A" && resEvaluation != "J031A");

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

            var outcome = new Outcome();
            outcome.name = "MRL_03";
            outcome.lastupdate = "2017-04-28";
            outcome.description = "If the value in the data element 'Result type' (resType) is equal to 'Numerical value' (VAL), and the value in the data element 'Product treatment' (prodTreat) is equal to 'Unprocessed' (T999A), or 'Freezing' (T998A), and the MRL has changed during the year, then a value in the data element 'Result legal limit' (resLegalLimit) must be reported;";
            outcome.error = "resLegalLimit is missing, though it is mandatory to be reported when the MRL changed during 2016;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (resType == "VAL" && (prodTreat == "T999A" || prodTreat == "T998A"))
            {
                outcome.passed = String.IsNullOrEmpty(resLegalLimit) == false;

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

            var outcome = new Outcome();
            outcome.description = "A value in the data element 'EFSA Product Code' (EFSAProdCode) must be reported;";
            outcome.error = "EFSAProdCode is missing, though mandatory";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("EFSAProdCode") == null)
            {
                outcome.passed = false;
            }
            return outcome;
        }

        ///A value in the data element 'Product full text description' (prodText) must be reported;
        public Outcome CHEM03(XElement sample)
        {
            // <checkedDataElements>;
            //prodText;

            var outcome = new Outcome();
            outcome.description = "A value in the data element 'Product full text description' (prodText) must be reported;";
            outcome.error = "prodText is missing, though mandatory;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("prodText") == null)
            {
                outcome.passed = false;
            }

            return outcome;
        }

        ///A value in the data element 'Analytical method code' (anMethCode) must be reported;
        public Outcome CHEM04(XElement sample)
        {
            // <checkedDataElements>;
            //anMethCode;

            var outcome = new Outcome();
            outcome.description = "A value in the data element 'Analytical method code' (anMethCode) must be reported;";
            outcome.error = "anMethCode is missing, though mandatory;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("anMethCode") == null)
            {
                outcome.passed = false;
            }

            return outcome;
        }

        ///A value in the data element 'Result LOQ' (resLOQ) should be reported;
        public Outcome CHEM05(XElement sample)
        {
            // <checkedDataElements>;
            //resLOQ;

            var outcome = new Outcome();
            outcome.description = "A value in the data element 'Result LOQ' (resLOQ) should be reported;";
            outcome.error = "WARNING: resLOQ is missing, though recommended;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (sample.Element("resLOQ") == null)
            {
                outcome.passed = false;
            }

            return outcome;
        }

        ///The value in the data element 'Result value recovery' (resValRec) must be less than 200;
        public Outcome CHEM06(XElement sample)
        {
            // <checkedDataElements>;
            //resValRec;

            var outcome = new Outcome();
            outcome.description = "The value in the data element 'Result value recovery' (resValRec) must be less than 200;";
            outcome.error = "resValRec is greater than 200;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("resValRec") == null)
            {
                return outcome;
            }
            else
            {
                if (decimal.Parse(sample.Element("resValRec").Value.Replace(".", ",")) > 200)
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.description = "If the value in the data element 'Parameter code' (paramCode) is 'Acrylamide' (RF-00000410-ORG), then the value in the data element 'Product comment' (prodCom) must contain a specific product code;";
            outcome.error = "prodCom does not contain specific code, though the parameter is acrylamide;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("paramCode").Value == "RF-00000410-ORG")
            {

                outcome.passed = sample.Element("prodComm") != null;
            }

            return outcome;
        }

        ///If the value in the data element 'Parameter code' (paramCode) is 'Furan' (RF-00000073-ORG), then the value in the data element 'Product comment' (prodCom) must contain the text 'purchase' or 'consume';
        public Outcome CHEM08(XElement sample)
        {
            // <checkedDataElements>;
            //paramCode;
            //prodCom;

            var outcome = new Outcome();
            outcome.description = "If the value in the data element 'Parameter code' (paramCode) is 'Furan' (RF-00000073-ORG), then the value in the data element 'Product comment' (prodCom) must contain the text 'purchase' or 'consume';";
            outcome.error = "prodCom does not contain 'purchase' or 'consume', though the parameter is furan;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("paramCode").Value == "RF-00000073-ORG")
            {
                outcome.passed = sample.Element("prodComm") != null && (sample.Element("prodComm").Value == "purchase" || sample.Element("prodComm").Value == "consume");
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

            var outcome = new Outcome();
            outcome.description = "If a value is reported in at least one of the following data elements: 'Reporting country' (repCountry), 'Country of sampling' (sampCountry), 'Area of sampling' (sampArea), 'Reporting year' (repYear), 'Year of sampling' (sampY), 'Month of sampling' (sampM), 'Day of sampling' (sampD), 'Sample taken size' (sampSize), 'Sample taken size unit' (sampUnitSize), 'Additional Sample taken information' (sampInfo), 'Type of matrix' (sampMatType), 'Coded description of the matrix of the sample taken' (sampMatCode), 'Text description of the matrix of the sample taken' (sampMatText), 'Country of origin of the sample taken' (origCountry), 'Area of origin of the sample taken' (origArea), 'Area of origin for fisheries or aquaculture activities code of the sample taken' (origFishAreaCode), 'Area of origin for fisheries or aquaculture activities text of the sample taken' (origFishAreaText), 'Country of processing of the sample taken' (procCountry), 'Area of processing of the sample taken' (procArea), 'Additional information on the matrix sampled' (sampMatInfo), then a 'Sample taken identification code' (sampId) must be reported;";
            outcome.error = "sampId is missing, though at least one descriptor for the sample taken or the matrix sampled (sections D, E) or the sampEventId is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (sample.Element("sampId") != null || sample.Element("sampEventId") != null || sample.Element("repCountry") != null || sample.Element("sampCountry") != null || sample.Element("sampArea") != null || sample.Element("repYear") != null || sample.Element("sampY") != null || sample.Element("sampM") != null || sample.Element("sampD") != null || sample.Element("sampSize") != null || sample.Element("sampUnitSize") != null || sample.Element("sampInfo") != null || sample.Element("sampMatType") != null || sample.Element("sampMatCode") != null || sample.Element("sampMatText") != null || sample.Element("origCountry") != null || sample.Element("origArea") != null || sample.Element("origFishAreaCode") != null || sample.Element("origFishAreaText") != null || sample.Element("procCountry") != null || sample.Element("procArea") != null || sample.Element("sampMatInfo") != null)
            {
                outcome.passed = sample.Element("sampId") != null;
            }
            else
            {
                outcome.passed = false;
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


        ///The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);
        ///The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);
        public Outcome VMPR001(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = sample.Element("progLegalRef").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR001";
            outcome.lastupdate = "2017-01-09";
            outcome.description = "The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);";
            outcome.error = "WARNING: progLegalRef is different from Council Directive (EC) No 23/1996;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (progLegalRef != "N247A")
            {
                outcome.passed = false;
            }
            
            return outcome;
        }

        ///The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A) and 'Not specified' (STXXA);
        public Outcome VMPR002(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");

            var outcome = new Outcome();
            outcome.name = "VMPR002";
            outcome.lastupdate = "2017-01-10";
            outcome.description = "The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A) and 'Not specified' (STXXA);";
            outcome.error = "sampStrategy is not specified, or equal to census, though these values should not be reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
                var sampStrategys = new List<string>();
                sampStrategys.Add("ST50A");
                sampStrategys.Add("STXXA");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.passed = false;
                }

            return outcome;
        }
        ///The value in the data element 'Sampling Strategy' (sampStrategy) should be different from 'Objective sampling' (ST10A), and 'Convenient sampling' (ST40A);
        public Outcome VMPR003(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");

            var outcome = new Outcome();
            outcome.name = "VMPR003";
            outcome.lastupdate = "2017-01-10";
            outcome.description = "The value in the data element 'Sampling Strategy' (sampStrategy) should be different from 'Objective sampling' (ST10A), and 'Convenient sampling' (ST40A);";
            outcome.error = "WARNING: sampStrategy is objective or conveniente sampling, though these values should not be reported;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(sampStrategy))
            {
                var sampStrategys = new List<string>();
                sampStrategys.Add("ST10A");
                sampStrategys.Add("ST40A");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.passed = false;
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

            var outcome = new Outcome();
            outcome.name = "VMPR004";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in 'Link To Original Sample’ (sampInfo.origSampId) is reported, i.e. a follow-up sample, then the value in 'Sampling Strategy’ (sampStrategy) must be 'suspect sampling' (ST30A);";
            outcome.error = "sampStrategy is not suspect sampling, though sampInfo.origSampId is reported;";
            outcome.type = "error";
            outcome.passed = true;

           
            if (!String.IsNullOrEmpty(sampInforigSampId))
            {
                var sampStrategys = new List<string>();
                sampStrategys.Add("ST30A");
                if (!sampStrategys.Contains(sampStrategy))
                {
                    outcome.passed = false;
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
            decimal tmp;
            s = s.Replace(',', '.');
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp) ? tmp: default(decimal?);
        }

        public List<Outcome> RunChemRules(XElement result)
        {
            var list = new List<Outcome>();
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



    public class Outcome
    {
        public bool passed { get; set; }
        public string description { get; set; }
        public string error { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string lastupdate { get; set; }
        public List<Tuple<string, string>> values { get; set; } = new List<Tuple<string, string>>();
        
    }

}
