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
            outcome.name = "BR01A";

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
            outcome.name = "BR02A_01";

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
                if (decimal.Parse(sample.Element("CCalpha").Value) <= 0)
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
                if (decimal.Parse(sample.Element("resValUncert").Value) >= 0)
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
            if ((string)sample.Element("resType") == "LOD")
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
            if (String.IsNullOrEmpty((string)sample.Element("resType")))
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
            if (sample.Element("sampArea") == null)
            {
                outcome.passed = true;
            }
            else
            {
                outcome.passed = new Validator().GetCountryFromAreaCode((string)sample.Element("sampArea")) == (string)sample.Element("sampCountry");
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


        ///Borttagen?
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


        ///If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed portion sequence' (anPortSeq) must be reported;
        public Outcome GBR5a(XElement sample)
        {
            // <checkedDataElements>;
            var anPortSeq = (string)sample.Element("anPortSeq");
            var anPortSize = (string)sample.Element("anPortSize");
            var anPortSizeUnit = (string)sample.Element("anPortSizeUnit");
            var anPortInfo = (string)sample.Element("anPortInfo");

            var outcome = new Outcome();
            outcome.name = "GBR5a";
            outcome.lastupdate = "2016-03-01";
            outcome.description = "If a value is reported in at least one of the following data elements: 'Sample analysed portion size' (anPortSize), 'Sample analysed portion size unit' (anPortSizeUnit), 'Additional information on the sample analysed portion  (anPortInfo), then a 'Sample analysed portion sequence' (anPortSeq) must be reported;";
            outcome.error = "anPortSeq is missing, though at least one descriptor for the sample analysed portion (section H) is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!(new List<string> { anPortSize, anPortSizeUnit, anPortInfo }).Any(i => i == null))
            {
                var anPortSizes = new List<string>();
                outcome.passed = anPortSizes.Contains(anPortSize);
                var anPortSizeUnits = new List<string>();
                outcome.passed = anPortSizeUnits.Contains(anPortSizeUnit);
                var anPortInfos = new List<string>();
                outcome.passed = anPortInfos.Contains(anPortInfo);

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
      

        ///If a value is reported in 'Local organisation country' (localOrgCountry), then a 'Local organisation identification code' (localOrgId) must be reported;
        [Rule(Description = "If a value is reported in 'Local organisation country' (localOrgCountry), then a 'Local organisation identification code' (localOrgId) must be reported;", ErrorMessage = "localOrgId is missing, though localOrgCountry is reported;", RuleType = "error")]
        public Outcome GBR7a(XElement sample)
        {
            // <checkedDataElements>;
            var localOrgId = (string)sample.Element("localOrgId");
            var localOrgCountry = (string)sample.Element("localOrgCountry");

            var outcome = new Outcome();
            outcome.name = "GBR7a";
            outcome.lastupdate = "2016-03-01";
            outcome.description = "If a value is reported in 'Local organisation country' (localOrgCountry), then a 'Local organisation identification code' (localOrgId) must be reported;";
            outcome.error = "localOrgId is missing, though localOrgCountry is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(localOrgCountry))
            {
                outcome.passed = !String.IsNullOrEmpty(localOrgId);
            }

            return outcome;
        }

        ///If a value is reported in 'Laboratory country' (labCountry), then a 'Laboratory identification code' (labId) must be reported;
        public Outcome GBR10a(XElement sample)
        {
            // <checkedDataElements>;
            var labId = (string)sample.Element("labId");
            var labCountry = (string)sample.Element("labCountry");

            var outcome = new Outcome();
            outcome.name = "GBR10a";
            outcome.lastupdate = "2016-03-01";
            outcome.description = "If a value is reported in 'Laboratory country' (labCountry), then a 'Laboratory identification code' (labId) must be reported;";
            outcome.error = "labId is missing, though labCountry is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(labCountry))
            {
                outcome.passed = !String.IsNullOrEmpty(labId);
            }
            return outcome;
        }

        ///If in the 'Coded description of the matrix of the sample taken' the generic-term facet (sampMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix of the sample taken' (sampMatText);
        [Rule(Description = "If in the 'Coded description of the matrix of the sample taken' the generic-term facet (sampMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix of the sample taken' (sampMatText);", ErrorMessage = "sampMatText is missing, though mandatory if sampMatCode.gen is 'Other' (A07XE);", RuleType = "error")]
        public Outcome GBR15(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatCodegen = (string)sample.Element("sampMatCode.gen");
            var sampMatText = (string)sample.Element("sampMatText");

            var outcome = new Outcome();
            outcome.name = "GBR15";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If in the 'Coded description of the matrix of the sample taken' the generic-term facet (sampMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix of the sample taken' (sampMatText);";
            outcome.error = "sampMatText is missing, though mandatory if sampMatCode.gen is 'Other' (A07XE);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (sampMatCodegen == "A07XE")
            {
                outcome.passed = !String.IsNullOrEmpty(sampMatText);
            }

            return outcome;
        }


        ///If in the 'Coded description of the analysed matrix' the generic-term facet’ (anMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix analysed' (anMatText);
        [Rule(Description = "If in the 'Coded description of the analysed matrix' the generic-term facet’ (anMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix analysed' (anMatText);", ErrorMessage = "anMatText is missing, though mandatory if anMatCode.gen is 'Other' (A07XE);", RuleType = "error")]
        public Outcome GBR16(XElement sample)
        {
            // <checkedDataElements>;
            var anMatCodegen = (string)sample.Element("anMatCode.gen");
            var anMatText = (string)sample.Element("anMatText");

            var outcome = new Outcome();
            outcome.name = "GBR16";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If in the 'Coded description of the analysed matrix' the generic-term facet’ (anMatCode.gen) is reported with the descriptor 'Other' (A07XE), then a text must be reported in the 'Text description of the matrix analysed' (anMatText);";
            outcome.error = "anMatText is missing, though mandatory if anMatCode.gen is 'Other' (A07XE);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);

            if (anMatCodegen == "A07XE")
            {
                outcome.passed = !String.IsNullOrEmpty(anMatText);
            }
            return outcome;
        }
        public Outcome  GBR17(XElement sample)
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


        ///If the value reported in 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN) (i.e. a qualitative value), then a 'Result qualitative value' (resQualValue) must be reported;
        [Rule(Description = "If the value reported in 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN) (i.e. a qualitative value), then a 'Result qualitative value' (resQualValue) must be reported;", ErrorMessage = "resQualValue is missing, though resType is 'Qualitative Value (Binary)' (BIN);", RuleType = "error")]
        public Outcome GBR28(XElement sample)
        {
            // <checkedDataElements>;
            var resQualValue = (string)sample.Element("resQualValue");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR28";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value reported in 'Type of result' (resType) is 'Qualitative Value (Binary)' (BIN) (i.e. a qualitative value), then a 'Result qualitative value' (resQualValue) must be reported;";
            outcome.error = "resQualValue is missing, though resType is 'Qualitative Value (Binary)' (BIN);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "BIN")
            {
                outcome.passed = !String.IsNullOrEmpty(resQualValue);
            }
            return outcome;
        }

        ///If the reported value in the 'Analytical method code' (anMethCode.base) is 'Classification not possible' (F001A), then a text must be reported in the 'Analytical method text' (anMethText);
        [Rule(Description = "If the reported value in the 'Analytical method code' (anMethCode.base) is 'Classification not possible' (F001A), then a text must be reported in the 'Analytical method text' (anMethText);", ErrorMessage = "anMethText is missing, though mandatory if anMethCode.base is 'Classification not possible' (F001A);", RuleType = "error")]
        public Outcome GBR18(XElement sample)
        {
            // <checkedDataElements>;
            var anMethCodebase = (string)sample.Element("anMethCode.base");
            var anMethText = (string)sample.Element("anMethText");

            var outcome = new Outcome();
            outcome.name = "GBR18";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the reported value in the 'Analytical method code' (anMethCode.base) is 'Classification not possible' (F001A), then a text must be reported in the 'Analytical method text' (anMethText);";
            outcome.error = "anMethText is missing, though mandatory if anMethCode.base is 'Classification not possible' (F001A);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);

            if (anMethCodebase == "F001A")
            {
                outcome.passed = !String.IsNullOrEmpty(anMethText);
            }
            return outcome;
        }
        ///The value in the data element 'Percentage of fat' (exprResPerc.fatPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);
        [Rule(Description = "The value in the data element 'Percentage of fat' (exprResPerc.fatPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);", ErrorMessage = "exprResPerc.fatPerc is not between '0' and '100';", RuleType = "error")]
        public Outcome GBR19(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercfatPerc = (string)sample.Element("exprResPerc.fatPerc");

            var outcome = new Outcome();
            outcome.name = "GBR19";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in the data element 'Percentage of fat' (exprResPerc.fatPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);";
            outcome.error = "exprResPerc.fatPerc is not between '0' and '100';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(exprResPercfatPerc, out decimal result))
            {
                outcome.passed = result > 0 && result <= 100;
            }

            return outcome;
        }

        ///The value in the data element 'Percentage of moisture ' (exprResPerc.moistPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);
        [Rule(Description = "The value in the data element 'Percentage of moisture ' (exprResPerc.moistPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);", ErrorMessage = "exprResPerc.moistPerc is not between '0' and '100';", RuleType = "error")]
        public Outcome GBR20(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercmoistPerc = (string)sample.Element("exprResPerc.moistPerc");

            var outcome = new Outcome();
            outcome.name = "GBR20";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in the data element 'Percentage of moisture ' (exprResPerc.moistPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);";
            outcome.error = "exprResPerc.moistPerc is not between '0' and '100';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(exprResPercmoistPerc))
            {
                outcome.passed = decimal.TryParse(exprResPercmoistPerc, out decimal r);
            }
            return outcome;
        }
        ///The value in the data element 'Percentage of alcohol' (exprResPerc.alcoholPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);
        [Rule(Description = "The value in the data element 'Percentage of alcohol' (exprResPerc.alcoholPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);", ErrorMessage = "exprResPerc.alcoholPerc is not between '0' and '100';", RuleType = "error")]
        public Outcome GBR21(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercalcoholPerc = (string)sample.Element("exprResPerc.alcoholPerc");

            var outcome = new Outcome();
            outcome.name = "GBR21";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in the data element 'Percentage of alcohol' (exprResPerc.alcoholPerc) must be expressed as a percentage and so be between '0' and '100' (e.g. '40' must be reported for 40 %);";
            outcome.error = "exprResPerc.alcoholPerc is not between '0' and '100';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(exprResPercalcoholPerc))
            {
                outcome.passed = decimal.TryParse(exprResPercalcoholPerc, out decimal r);
            }
            return outcome;
        }

        ///If the value in the 'Expression of result type' (exprResType) is 'Fat weight' (B003A), then a value must be reported in the 'Percentage of fat' (exprResPerc.fatPerc);
        [Rule(Description = "If the value in the 'Expression of result type' (exprResType) is 'Fat weight' (B003A), then a value must be reported in the 'Percentage of fat' (exprResPerc.fatPerc);", ErrorMessage = "exprResPerc.fatPerc is missing, though mandatory if exprResType is 'Fat weight' (B003A);", RuleType = "error")]
        public Outcome GBR22(XElement sample)
        {
            // <checkedDataElements>;
            var exprResPercfatPerc = (string)sample.Element("exprResPerc.fatPerc");
            var exprResType = (string)sample.Element("exprResType");

            var outcome = new Outcome();
            outcome.name = "GBR22";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the 'Expression of result type' (exprResType) is 'Fat weight' (B003A), then a value must be reported in the 'Percentage of fat' (exprResPerc.fatPerc);";
            outcome.error = "exprResPerc.fatPerc is missing, though mandatory if exprResType is 'Fat weight' (B003A);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (exprResType == "B003A")
            {
                outcome.passed = !String.IsNullOrEmpty(exprResPercfatPerc);
            }

            return outcome;
        }

        ///If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;
        [Rule(Description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;", ErrorMessage = "sampUnitSizeUnit is missing, though sampUnitSize is reported;", RuleType = "error")]
        public Outcome GBR24(XElement sample)
        {
            // <checkedDataElements>;
            var sampUnitSize = (string)sample.Element("sampUnitSize");
            var sampUnitSizeUnit = (string)sample.Element("sampUnitSizeUnit");

            var outcome = new Outcome();
            outcome.name = "GBR24";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;";
            outcome.error = "sampUnitSizeUnit is missing, though sampUnitSize is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampUnitSize))
            {
                outcome.passed = !String.IsNullOrEmpty(sampUnitSizeUnit);
            }

            return outcome;
        }

        /////If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;
        //[Rule(Description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;", ErrorMessage = "sampUnitSizeUnit is missing, though sampUnitSize is reported;", RuleType = "error")]
        //public Outcome GBR24(XElement sample)
        //{
        //    // <checkedDataElements>;
        //    var sampUnitSize = (string)sample.Element("sampUnitSize");
        //    var sampUnitSizeUnit = (string)sample.Element("sampUnitSizeUnit");

        //    var outcome = new Outcome();
        //    outcome.name = "GBR24";
        //    outcome.lastupdate = "2014-08-08";
        //    outcome.description = "If a 'Sampling unit size' (sampUnitSize) is reported, then a 'Sampling unit size unit' (sampUnitSizeUnit) must be reported;";
        //    outcome.error = "sampUnitSizeUnit is missing, though sampUnitSize is reported;";
        //    outcome.type = "error";
        //    outcome.passed = true;

        //    //Logik (ignore null: no);

        //    if (!String.IsNullOrEmpty(sampUnitSize))
        //    {
        //        outcome.passed = !String.IsNullOrEmpty(sampUnitSizeUnit);
        //    }
        //    return outcome;
        //}



        ///If a 'Sample analysed portion size' (anPortSize) is reported, then a 'Sample analysed portion size unit' (anPortSizeUnit) must be reported;
        [Rule(Description = "If a 'Sample analysed portion size' (anPortSize) is reported, then a 'Sample analysed portion size unit' (anPortSizeUnit) must be reported;", ErrorMessage = "anPortSizeUnit is missing, though anPortSize is reported;", RuleType = "error")]
        public Outcome GBR26(XElement sample)
        {
            // <checkedDataElements>;
            var anPortSize = (string)sample.Element("anPortSize");
            var anPortSizeUnit = (string)sample.Element("anPortSizeUnit");

            var outcome = new Outcome();
            outcome.name = "GBR26";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If a 'Sample analysed portion size' (anPortSize) is reported, then a 'Sample analysed portion size unit' (anPortSizeUnit) must be reported;";
            outcome.error = "anPortSizeUnit is missing, though anPortSize is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(anPortSize))
            {
                outcome.passed = !String.IsNullOrEmpty(anPortSizeUnit);
            }
            return outcome;
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

        ///If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'Result lower limit of the working range' (resLLWR), 'Result upper limit of the working range' (resULWR), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Limit for the result evaluation (Low limit)' (evalLowLimit), 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Result unit' (resUnit) must be reported;
        [Rule(Description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'Result lower limit of the working range' (resLLWR), 'Result upper limit of the working range' (resULWR), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Limit for the result evaluation (Low limit)' (evalLowLimit), 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Result unit' (resUnit) must be reported;", ErrorMessage = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit) is reported;", RuleType = "error")]
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

            var outcome = new Outcome();
            outcome.name = "GBR29";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If a value is reported in at least one of the following data elements: 'Result LOD' (resLOD), 'Result LOQ' (resLOQ), 'Result lower limit of the working range' (resLLWR), 'Result upper limit of the working range' (resULWR), 'CC alpha' (CCalpha), 'CC beta' (CCbeta), 'Result value' (resVal), 'Result value uncertainty' (resValUncert), 'Result value uncertainty Standard deviation' (resValUncertSD), 'Limit for the result evaluation (Low limit)' (evalLowLimit), 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Result unit' (resUnit) must be reported;";
            outcome.error = "resUnit is missing, though at least one numeric data element (resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit) is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            var listOfNotEmpty = new List<string> { resUnit, resLOD, resLOQ, resLLWR, resULWR, CCalpha, CCbeta, resVal, resValUncert, resValUncertSD, evalLowLimit, evalHighLimit };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                outcome.passed = !String.IsNullOrEmpty(resUnit);

            }

            return outcome;
        }
        ///If a value is reported in 'Limit for the result evaluation ' (evalLowLimit), then a 'Type of limit for the result evaluation' (evalLimitType) must be reported;
        [Rule(Description = "If a value is reported in 'Limit for the result evaluation ' (evalLowLimit), then a 'Type of limit for the result evaluation' (evalLimitType) must be reported;", ErrorMessage = "evalLimitType is missing, though evalLowLimit is reported;", RuleType = "error")]
        public Outcome GBR30(XElement sample)
        {
            // <checkedDataElements>;
            var evalLimitType = (string)sample.Element("evalLimitType");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome();
            outcome.name = "GBR30";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If a value is reported in 'Limit for the result evaluation ' (evalLowLimit), then a 'Type of limit for the result evaluation' (evalLimitType) must be reported;";
            outcome.error = "evalLimitType is missing, though evalLowLimit is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (decimal.TryParse(evalLowLimit, out decimal v))
            {
                outcome.passed = !String.IsNullOrEmpty(evalLimitType);
            }

            return outcome;
        }

        ///If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;
        [Rule(Description = "If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;", ErrorMessage = "evalLowLimit is missing, though evalHighLimit is reported;", RuleType = "error")]
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
            if (evalLimitType != "W001A" && decimal.TryParse(evalHighLimit, out decimal value))
            {
                outcome.passed = decimal.TryParse(evalLowLimit, out decimal result);
            }
            return outcome;
        }


        ///The value reported in 'Limit for the result evaluation (High limit)' (evalHighLimit) must be greater than the value reported in 'Limit for the result evaluation (Low limit)' (evalLowLimit);
        [Rule(Description = "The value reported in 'Limit for the result evaluation (High limit)' (evalHighLimit) must be greater than the value reported in 'Limit for the result evaluation (Low limit)' (evalLowLimit);", ErrorMessage = "evalHighLimit is not greater than evalLowLimit;", RuleType = "error")]
        public Outcome GBR32(XElement sample)
        {
            // <checkedDataElements>;
            var evalLowLimit = (string)sample.Element("evalLowLimit");
            var evalHighLimit = (string)sample.Element("evalHighLimit");

            var outcome = new Outcome();
            outcome.name = "GBR32";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value reported in 'Limit for the result evaluation (High limit)' (evalHighLimit) must be greater than the value reported in 'Limit for the result evaluation (Low limit)' (evalLowLimit);";
            outcome.error = "evalHighLimit is not greater than evalLowLimit;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(evalLowLimit, out decimal evallowlimit) && decimal.TryParse(evalHighLimit, out decimal evalhighlimit))
            {
                outcome.passed = evalhighlimit > evallowlimit;
            }
            return outcome;
        }


        ///If 'Result value' (resVal) is greater than 'Limit for the result evaluation ' (evalLowLimit), then the value in 'Evaluation of the result' (evalCode) must be different from 'below or equal to maximum permissible quantities' (J002A);
        [Rule(Description = "If 'Result value' (resVal) is greater than 'Limit for the result evaluation ' (evalLowLimit), then the value in 'Evaluation of the result' (evalCode) must be different from 'below or equal to maximum permissible quantities' (J002A);", ErrorMessage = "evalCode is 'below or equal to maximum permissible quantities' (J002A), though resVal is greater than evalLowLimit;", RuleType = "error")]
        public Outcome GBR33(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");
            var resVal = (string)sample.Element("resVal");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome();
            outcome.name = "GBR33";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If 'Result value' (resVal) is greater than 'Limit for the result evaluation ' (evalLowLimit), then the value in 'Evaluation of the result' (evalCode) must be different from 'below or equal to maximum permissible quantities' (J002A);";
            outcome.error = "evalCode is 'below or equal to maximum permissible quantities' (J002A), though resVal is greater than evalLowLimit;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evallowlimit))
            {
                outcome.passed = resval >= evallowlimit && evalCode != "J002A";
            }
          

            return outcome;
        }

        ///If 'Evaluation of the result' (evalCode) is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A), then 'Result value' (resVal) must be greater than 'Limit for the result evaluation ' (evalLowLimit);
        [Rule(Description = "If 'Evaluation of the result' (evalCode) is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A), then 'Result value' (resVal) must be greater than 'Limit for the result evaluation ' (evalLowLimit);", ErrorMessage = "resVal is lower than evalLowLimit, though evalCode is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A);", RuleType = "error")]
        public Outcome GBR34(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var evalCode = (string)sample.Element("evalCode");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome();
            outcome.name = "GBR34";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If 'Evaluation of the result' (evalCode) is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A), then 'Result value' (resVal) must be greater than 'Limit for the result evaluation ' (evalLowLimit);";
            outcome.error = "resVal is lower than evalLowLimit, though evalCode is either 'above maximum permissible quantities' (J003A) or 'Compliant due to measurement uncertainty' (J031A);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            //Logik (ignore null: yes);
            if (evalCode == "J003A" || evalCode == "J031A")
            {
                if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evalLowlimit))
                {
                    outcome.passed = resval >= evalLowlimit;
                }
            }
            return outcome;
        }

        ///If 'Evaluation of the result' (evalCode) is 'below or equal to maximum permissible quantities' (J002A), then 'Result value' (resVal) must be less than or equal to 'Limit for the result evaluation ' (evalLowLimit);
        [Rule(Description = "If 'Evaluation of the result' (evalCode) is 'below or equal to maximum permissible quantities' (J002A), then 'Result value' (resVal) must be less than or equal to 'Limit for the result evaluation ' (evalLowLimit);", ErrorMessage = "resVal is greater than evalLowLimit, though evalCode is 'below or equal to maximum permissible quantities' (J002A);", RuleType = "error")]
        public Outcome GBR35(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");
            var resVal = (string)sample.Element("resVal");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome();
            outcome.name = "GBR35";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If 'Evaluation of the result' (evalCode) is 'below or equal to maximum permissible quantities' (J002A), then 'Result value' (resVal) must be less than or equal to 'Limit for the result evaluation ' (evalLowLimit);";
            outcome.error = "resVal is greater than evalLowLimit, though evalCode is 'below or equal to maximum permissible quantities' (J002A);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (evalCode == "J002A")
            {
                if (decimal.TryParse(resVal, out decimal resval) && decimal.TryParse(evalLowLimit, out decimal evalLowlimit))
                {
                    outcome.passed = resval <= evalLowlimit;
                }
            }

            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then a value must be reported in the data element 'Result LOD' (resLOD);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then a value must be reported in the data element 'Result LOD' (resLOD);", ErrorMessage = "resLOD is missing, though resType is 'Non Detected Value (below LOD)' (LOD);", RuleType = "error")]
        public Outcome GBR36(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR36";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then a value must be reported in the data element 'Result LOD' (resLOD);";
            outcome.error = "resLOD is missing, though resType is 'Non Detected Value (below LOD)' (LOD);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "LOD")
            {
                outcome.passed = !String.IsNullOrEmpty(resLOD);
            }
            return outcome;
        }
        ///The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);
        [Rule(Description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);", ErrorMessage = "resLOD is not less than or equal to resLOQ;", RuleType = "error")]
        public Outcome GBR37(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resLOQ = (string)sample.Element("resLOQ");

            var outcome = new Outcome();
            outcome.name = "GBR37";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result LOD' (resLOD) must be less than or equal to the value in 'Result LOQ' (resLOQ);";
            outcome.error = "resLOD is not less than or equal to resLOQ;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resLOD, out decimal reslod) && decimal.TryParse(resLOQ, out decimal resloq))
            {
                outcome.passed = resloq <= reslod;
            }
            return outcome;
        }
        ///If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;
        //public Outcome GBR31(XElement sample)
        //{
        //    // <checkedDataElements>;
        //    var evalLowLimit = (string)sample.Element("evalLowLimit");
        //    var evalHighLimit = (string)sample.Element("evalHighLimit");
        //    var evalLimitType = (string)sample.Element("evalLimitType");

        //    var outcome = new Outcome();
        //    outcome.name = "GBR31";
        //    outcome.lastupdate = "2017-04-24";
        //    outcome.description = "If the value in the data element ‘Type of limit for the result evaluation’ (evalLimitType) is different from 'Maximum limit (ML)' (W001A), and a value is reported in 'Limit for the result evaluation (High limit)' (evalHighLimit), then a 'Limit for the result evaluation ' (evalLowLimit) must be reported;";
        //    outcome.error = "evalLowLimit is missing, though evalHighLimit is reported;";
        //    outcome.type = "error";
        //    outcome.passed = true;

        //    //Logik (ignore null: no);
        //    if (evalLimitType != "W001A" && !string.IsNullOrEmpty(evalHighLimit))
        //    {

        //        outcome.passed = String.IsNullOrEmpty(evalLowLimit);

        //    }
        //    return outcome;
        //}

        ///The value in 'Result LOD' (resLOD) must be greater than '0';
        [Rule(Description = "The value in 'Result LOD' (resLOD) must be greater than '0';", ErrorMessage = "resLOD is not greater than '0';", RuleType = "error")]
        public Outcome GBR38(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");

            var outcome = new Outcome();
            outcome.name = "GBR38";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result LOD' (resLOD) must be greater than '0';";
            outcome.error = "resLOD is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resLOD))
            {
                if (decimal.TryParse(resLOD, out decimal result))
                {
                    outcome.passed = result > 0;
                }
            }
            return outcome;
        }
        ///If the value in the data element 'Type of result' (resType) is 'Non Quantified Value (below LOQ)' (LOQ), then a value must be reported in the data element 'Result LOQ' (resLOQ);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Quantified Value (below LOQ)' (LOQ), then a value must be reported in the data element 'Result LOQ' (resLOQ);", ErrorMessage = "resLOQ is missing, though resType is 'Non Quantified Value (below LOQ)' (LOQ);", RuleType = "error")]
        public Outcome GBR39(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR39";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Non Quantified Value (below LOQ)' (LOQ), then a value must be reported in the data element 'Result LOQ' (resLOQ);";
            outcome.error = "resLOQ is missing, though resType is 'Non Quantified Value (below LOQ)' (LOQ);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "LOQ")
            {
                outcome.passed = !String.IsNullOrEmpty(resLOQ);
            }

            return outcome;
        }
        ///The value in 'Result LOQ' (resLOQ) must be greater than 0;
        [Rule(Description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;", ErrorMessage = "resLOQ is not greater than 0;", RuleType = "error")]
        public Outcome GBR40(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");

            var outcome = new Outcome();
            outcome.name = "GBR40";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result LOQ' (resLOQ) must be greater than 0;";
            outcome.error = "resLOQ is not greater than 0;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resLOQ))
            {
                outcome.passed = false;
                if (decimal.TryParse(resLOQ, out decimal result))
                {
                    outcome.passed = result > 0;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);", ErrorMessage = "CCalpha is missing, though resType is 'Value below CCalpha (below CCα)' (CCA);", RuleType = "error")]
        public Outcome GBR41(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR41";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Value below CCalpha (below CCα)' (CCA), then a value must be reported in the data element 'CC alpha' (CCalpha);";
            outcome.error = "CCalpha is missing, though resType is 'Value below CCalpha (below CCα)' (CCA);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "CCA")
            {
                outcome.passed = !String.IsNullOrEmpty(CCalpha);
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


            if (decimal.TryParse(CCalpha, out decimal _ccalpha) && decimal.TryParse(CCbeta, out decimal _ccbeta))
            {
                outcome.passed = _ccalpha < _ccbeta;

            }


            return outcome;
        }


        ///The value in 'CC alpha' (CCalpha) must be greater than '0';
        [Rule(Description = "The value in 'CC alpha' (CCalpha) must be greater than '0';", ErrorMessage = "CCalpha is not greater than '0';", RuleType = "error")]
        public Outcome GBR43(XElement sample)
        {
            // <checkedDataElements>;
            var CCalpha = (string)sample.Element("CCalpha");

            var outcome = new Outcome();
            outcome.name = "GBR43";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'CC alpha' (CCalpha) must be greater than '0';";
            outcome.error = "CCalpha is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(CCalpha))
            {
                outcome.passed = false;
                if (decimal.TryParse(CCalpha, out decimal result))
                {
                    outcome.passed = result > 0;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);", ErrorMessage = "CCbeta is missing, though resType is 'Value below CCbeta (below CCβ)' (CCB);", RuleType = "error")]
        public Outcome GBR44(XElement sample)
        {
            // <checkedDataElements>;
            var CCbeta = (string)sample.Element("CCbeta");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR44";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Value below CCbeta (below CCβ)' (CCB), then a value must be reported in the data element 'CC beta' (CCbeta);";
            outcome.error = "CCbeta is missing, though resType is 'Value below CCbeta (below CCβ)' (CCB);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "CCB")
            {
                outcome.passed = !String.IsNullOrEmpty(CCbeta);
            }

            return outcome;
        }
        ///The value in 'CC beta' (CCbeta) must be greater than '0';
        [Rule(Description = "The value in 'CC beta' (CCbeta) must be greater than '0';", ErrorMessage = "CCbeta is not greater than '0';", RuleType = "error")]
        public Outcome GBR45(XElement sample)
        {
            // <checkedDataElements>;
            var CCbeta = (string)sample.Element("CCbeta");

            var outcome = new Outcome();
            outcome.name = "GBR45";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'CC beta' (CCbeta) must be greater than '0';";
            outcome.error = "CCbeta is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(CCbeta))
            {
                outcome.passed = false;
                if (decimal.TryParse(CCbeta, out decimal result))
                {
                    outcome.passed = result > 0;
                }
            }
            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Numerical Value' (VAL), then a value must be reported in the data element 'Result value' (resVal);
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Numerical Value' (VAL), then a value must be reported in the data element 'Result value' (resVal);", ErrorMessage = "resVal is missing, though resType is 'Numerical Value' (VAL);", RuleType = "error")]
        public Outcome GBR46(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "GBR46";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Numerical Value' (VAL), then a value must be reported in the data element 'Result value' (resVal);";
            outcome.error = "resVal is missing, though resType is 'Numerical Value' (VAL);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "VAL")
            {
                outcome.passed = !String.IsNullOrEmpty(resVal);
            }

            return outcome;
        }

        ///If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;
        [Rule(Description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;", ErrorMessage = "resVal is reported, though resType is 'Non Detected Value (below LOD)' (LOD);", RuleType = "error")]
        public Outcome GBR47(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome();
            outcome.name = "GBR47";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the value in the data element 'Type of result' (resType) is 'Non Detected Value (below LOD)' (LOD), then the data element 'Result value' (resVal) must be empty;";
            outcome.error = "resVal is reported, though resType is 'Non Detected Value (below LOD)' (LOD);";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (resType == "LOD")
            {
                outcome.passed = String.IsNullOrEmpty(resVal);
            }

            return outcome;
        }
        ///The value in 'Result value' (resVal) must be greater than '0';
        [Rule(Description = "The value in 'Result value' (resVal) must be greater than '0';", ErrorMessage = "resVal is not greater than '0';", RuleType = "error")]
        public Outcome GBR48(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");

            var outcome = new Outcome();
            outcome.name = "GBR48";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result value' (resVal) must be greater than '0';";
            outcome.error = "resVal is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resVal, out decimal result))
            {
                outcome.passed = result > 0;
            }
            if (!String.IsNullOrEmpty(resVal))
            {

            }
            return outcome;
        }
        ///The value in 'Result value recovery rate' (resValRec) must be greater than '0';
        [Rule(Description = "The value in 'Result value recovery rate' (resValRec) must be greater than '0';", ErrorMessage = "resValRec is not greater than '0';", RuleType = "error")]
        public Outcome GBR49(XElement sample)
        {
            // <checkedDataElements>;
            var resValRec = (string)sample.Element("resValRec");

            var outcome = new Outcome();
            outcome.name = "GBR49";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result value recovery rate' (resValRec) must be greater than '0';";
            outcome.error = "resValRec is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValRec, out decimal result))
            {
                outcome.passed = result > 0;
            }

            return outcome;
        }

        ///The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than '0';
        [Rule(Description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than '0';", ErrorMessage = "resValUncertSD is not greater than '0';", RuleType = "error")]
        public Outcome GBR50(XElement sample)
        {
            // <checkedDataElements>;
            var resValUncertSD = (string)sample.Element("resValUncertSD");

            var outcome = new Outcome();
            outcome.name = "GBR50";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result value uncertainty Standard deviation' (resValUncertSD) must be greater than '0';";
            outcome.error = "resValUncertSD is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValUncertSD, out decimal result))
            {
                outcome.passed = result > 0;
            }

            return outcome;
        }
        ///The value in 'Result value uncertainty' (resValUncert) must be greater than '0';
        [Rule(Description = "The value in 'Result value uncertainty' (resValUncert) must be greater than '0';", ErrorMessage = "resValUncert is not greater than '0';", RuleType = "error")]
        public Outcome GBR51(XElement sample)
        {
            // <checkedDataElements>;
            var resValUncert = (string)sample.Element("resValUncert");

            var outcome = new Outcome();
            outcome.name = "GBR51";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The value in 'Result value uncertainty' (resValUncert) must be greater than '0';";
            outcome.error = "resValUncert is not greater than '0';";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (decimal.TryParse(resValUncert, out decimal result))
            {
                outcome.passed = result > 0;
            }

            return outcome;
        }
    
        ///The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be a valid date;
        [Rule(Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be a valid date;", ErrorMessage = "The combination of values in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY is not a valid date;", RuleType = "error")]
        public Outcome GBR53(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterY = (string)sample.Element("sampEventInfo.slaughterY");

            var outcome = new Outcome();
            outcome.name = "GBR53";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be a valid date;";
            outcome.error = "The combination of values in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampEventInfoslaughterD, sampEventInfoslaughterM, sampEventInfoslaughterY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                outcome.passed = DateTime.TryParseExact(sampEventInfoslaughterY + sampEventInfoslaughterM + sampEventInfoslaughterD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }

            return outcome;
        }
        ///The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be a valid date;
        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be a valid date;", ErrorMessage = "The combination of values in sampD, sampM, and sampY is not a valid date;", RuleType = "error")]
        public Outcome GBR54(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome();
            outcome.name = "GBR54";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be a valid date;";
            outcome.error = "The combination of values in sampD, sampM, and sampY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };

                outcome.passed = DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime dateone);

            }

            return outcome;
        }
        ///The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be a valid date;
        [Rule(Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be a valid date;", ErrorMessage = "The combination of values in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY is not a valid date;", RuleType = "error")]
        public Outcome GBR55(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalY = (string)sample.Element("sampInfo.arrivalY");

            var outcome = new Outcome();
            outcome.name = "GBR55";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be a valid date;";
            outcome.error = "The combination of values in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampInfoarrivalD, sampInfoarrivalM, sampInfoarrivalY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                outcome.passed = DateTime.TryParseExact(sampInfoarrivalY + sampInfoarrivalM + sampInfoarrivalD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }

        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be a valid date;
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be a valid date;", ErrorMessage = "The combination of values in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY is not a valid date;", RuleType = "error")]
        public Outcome GBR56(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");

            var outcome = new Outcome();
            outcome.name = "GBR56";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be a valid date;";
            outcome.error = "The combination of values in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                outcome.passed = DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime dateone);

            }
            return outcome;
        }
        ///The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), must be a valid date;
        [Rule(Description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), must be a valid date;", ErrorMessage = "The combination of values in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY is not a valid date;", RuleType = "error")]
        public Outcome GBR57(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome();
            outcome.name = "GBR57";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY), must be a valid date;";
            outcome.error = "The combination of values in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoexpiryD, sampMatInfoexpiryM, sampMatInfoexpiryY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                outcome.passed = DateTime.TryParseExact(sampMatInfoexpiryY + sampMatInfoexpiryM + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }

        ///The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be a valid date;
        [Rule(Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be a valid date;", ErrorMessage = "The combination of values in analysisD, analysisM, and analysisY is not a valid date;", RuleType = "error")]
        public Outcome GBR58(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome();
            outcome.name = "GBR58";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be a valid date;";
            outcome.error = "The combination of values in analysisD, analysisM, and analysisY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { analysisD, analysisM, analysisY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                outcome.passed = DateTime.TryParseExact(analysisY + analysisM + analysisD, formats, null, DateTimeStyles.None, out DateTime dateone);
            }
            return outcome;
        }


        ///The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be a valid date;
        [Rule(Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be a valid date;", ErrorMessage = "The combination of values in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY is not a valid date;", RuleType = "error")]
        public Outcome GBR60(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome();
            outcome.name = "GBR60";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be a valid date;";
            outcome.error = "The combination of values in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY is not a valid date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { isolInfoisolD, isolInfoisolM, isolInfoisolY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(isolInfoisolY + isolInfoisolM + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = true;
                }
                else
                {
                    outcome.passed = false;
                }
            }

            return outcome;
        }


        ///The reporting year, reported in 'Reporting year' (repYear), must be less than or equal to the current year;
        [Rule(Description = "The reporting year, reported in 'Reporting year' (repYear), must be less than or equal to the current year;", ErrorMessage = "The reporting year, reported in repYear, is greater than the current year;", RuleType = "error")]
        public Outcome GBR61(XElement sample)
        {
            // <checkedDataElements>;
            var repYear = (string)sample.Element("repYear");

            var outcome = new Outcome();
            outcome.name = "GBR61";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The reporting year, reported in 'Reporting year' (repYear), must be less than or equal to the current year;";
            outcome.error = "The reporting year, reported in repYear, is greater than the current year;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(repYear, out int result))
            {
                outcome.passed = result > DateTime.Now.Year;
            }
            return outcome;
        }

        ///The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be less than or equal to the current date;
        [Rule(Description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be less than or equal to the current date;", ErrorMessage = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR62(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterY = (string)sample.Element("sampEventInfo.slaughterY");

            var outcome = new Outcome();
            outcome.name = "GBR62";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the slaughtering, reported in 'Day of slaughtering' (sampEventInfo.slaughterD), 'Month of slaughtering' (sampEventInfo.slaughterM), and 'Year of slaughtering' (sampEventInfo.slaughterY), must be less than or equal to the current date;";
            outcome.error = "The date of the slaughtering, reported in sampEventInfo.slaughterD, sampEventInfo.slaughterM, and sampEventInfo.slaughterY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampEventInfoslaughterD, sampEventInfoslaughterM, sampEventInfoslaughterY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(sampEventInfoslaughterY + sampEventInfoslaughterM + sampEventInfoslaughterD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        ///The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the current date;
        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the current date;", ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR63(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome();
            outcome.name = "GBR63";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the current date;";
            outcome.error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        ///The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be less than or equal to the current date;
        [Rule(Description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be less than or equal to the current date;", ErrorMessage = "The date of the arrival in the laboratory, reported in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR64(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalY = (string)sample.Element("sampInfo.arrivalY");

            var outcome = new Outcome();
            outcome.name = "GBR64";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the arrival in the laboratory, reported in 'Arrival Day' (sampInfo.arrivalD), 'Arrival Month' (sampInfo.arrivalM), and 'Arrival Year' (sampInfo.arrivalY), must be less than or equal to the current date;";
            outcome.error = "The date of the arrival in the laboratory, reported in sampInfo.arrivalD, sampInfo.arrivalM, and sampInfo.arrivalY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampInfoarrivalD, sampInfoarrivalM, sampInfoarrivalY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(sampInfoarrivalY + sampInfoarrivalM + sampInfoarrivalD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the current date;
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the current date;", ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR65(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");

            var outcome = new Outcome();
            outcome.name = "GBR65";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the current date;";
            outcome.error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
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

            string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };

            //DateTime.TryParseExact("2018" + "02" + "65","yyyyMMdd", null,  DateTimeStyles.None, out _date).Dump();

            if (DateTime.TryParseExact(sampMatInfoexpiryY + sampMatInfoexpiryM + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime _date))
            {
                outcome.passed = _date < DateTime.Now;

            }
            return outcome;
        }


        ///The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;
        [Rule(Description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;", ErrorMessage = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR67(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome();
            outcome.name = "GBR67";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY), must be less than or equal to the current date;";
            outcome.error = "The date of the analysis, reported in analysisD, analysisM, and analysisY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { analysisD, analysisM, analysisY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(analysisY + analysisM + analysisD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }

        ///The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;
        [Rule(Description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;", ErrorMessage = "The date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY, is not less than or equal to the current date;", RuleType = "error")]
        public Outcome GBR69(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome();
            outcome.name = "GBR69";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY), must be less than or equal to the current date;";
            outcome.error = "The date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY, is not less than or equal to the current date;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { isolInfoisolD, isolInfoisolM, isolInfoisolY };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                if (DateTime.TryParseExact(isolInfoisolY + isolInfoisolM + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime dateone))
                {
                    outcome.passed = dateone <= DateTime.Now;
                }
            }

            return outcome;
        }
        ///The 'Day of slaughtering' (sampEventInfo.slaughterD) must be between 1 and 31;
        [Rule(Description = "The 'Day of slaughtering' (sampEventInfo.slaughterD) must be between 1 and 31;", ErrorMessage = "sampEventInfo.slaughterD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR70(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");

            var outcome = new Outcome();
            outcome.name = "GBR70";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Day of slaughtering' (sampEventInfo.slaughterD) must be between 1 and 31;";
            outcome.error = "sampEventInfo.slaughterD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;

            if (int.TryParse(sampEventInfoslaughterD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }
        ///The 'Day of sampling' (sampD) must be between 1 and 31;
        [Rule(Description = "The 'Day of sampling' (sampD) must be between 1 and 31;", ErrorMessage = "sampD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR71(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");

            var outcome = new Outcome();
            outcome.name = "GBR71";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Day of sampling' (sampD) must be between 1 and 31;";
            outcome.error = "sampD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;


            if (int.TryParse(sampD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }
        ///The 'Arrival Day' (sampInfo.arrivalD) must be between 1 and 31;
        [Rule(Description = "The 'Arrival Day' (sampInfo.arrivalD) must be between 1 and 31;", ErrorMessage = "sampInfo.arrivalD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR72(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");

            var outcome = new Outcome();
            outcome.name = "GBR72";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Arrival Day' (sampInfo.arrivalD) must be between 1 and 31;";
            outcome.error = "sampInfo.arrivalD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;


            if (int.TryParse(sampInfoarrivalD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }

        ///The 'Day of production' (sampMatInfo.prodD) must be between 1 and 31;
        [Rule(Description = "The 'Day of production' (sampMatInfo.prodD) must be between 1 and 31;", ErrorMessage = "sampMatInfo.prodD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR73(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");

            var outcome = new Outcome();
            outcome.name = "GBR73";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Day of production' (sampMatInfo.prodD) must be between 1 and 31;";
            outcome.error = "sampMatInfo.prodD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;

            if (int.TryParse(sampMatInfoprodD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }

        ///The 'Day of expiry' (sampMatInfo.expiryD) must be between 1 and 31;
        [Rule(Description = "The 'Day of expiry' (sampMatInfo.expiryD) must be between 1 and 31;", ErrorMessage = "sampMatInfo.expiryD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR74(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");

            var outcome = new Outcome();
            outcome.name = "GBR74";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Day of expiry' (sampMatInfo.expiryD) must be between 1 and 31;";
            outcome.error = "sampMatInfo.expiryD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;


            if (int.TryParse(sampMatInfoexpiryD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }

        ///The 'Day of analysis' (analysisD) must be between 1 and 31;
        [Rule(Description = "The 'Day of analysis' (analysisD) must be between 1 and 31;", ErrorMessage = "analysisD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR75(XElement sample)
        {
            // <checkedDataElements>;
            var analysisD = (string)sample.Element("analysisD");

            var outcome = new Outcome();
            outcome.name = "GBR75";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Day of analysis' (analysisD) must be between 1 and 31;";
            outcome.error = "analysisD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;

            if (int.TryParse(analysisD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }
        ///The 'Isolation day' (isolInfo.isolD) must be between 1 and 31;
        [Rule(Description = "The 'Isolation day' (isolInfo.isolD) must be between 1 and 31;", ErrorMessage = "isolInfo.isolD is not between 1 and 31;", RuleType = "error")]
        public Outcome GBR77(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");

            var outcome = new Outcome();
            outcome.name = "GBR77";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Isolation day' (isolInfo.isolD) must be between 1 and 31;";
            outcome.error = "isolInfo.isolD is not between 1 and 31;";
            outcome.type = "error";
            outcome.passed = true;


            //Logik (ignore null: yes);
            if (int.TryParse(isolInfoisolD, out int result))
            {
                outcome.passed = result > 0 && result < 32;
            }
            return outcome;
        }

        ///The 'Month of slaughtering' (sampEventInfo.slaughterM) must be between 1 and 12;
        [Rule(Description = "The 'Month of slaughtering' (sampEventInfo.slaughterM) must be between 1 and 12;", ErrorMessage = "sampEventInfo.slaughterM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR78(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");

            var outcome = new Outcome();
            outcome.name = "GBR78";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Month of slaughtering' (sampEventInfo.slaughterM) must be between 1 and 12;";
            outcome.error = "sampEventInfo.slaughterM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(sampEventInfoslaughterM, out int result))
            {
                outcome.passed = result > 0 && result < 13;
            }
            return outcome;
        }

        ///The 'Month of sampling' (sampM) must be between 1 and 12;
        [Rule(Description = "The 'Month of sampling' (sampM) must be between 1 and 12;", ErrorMessage = "sampM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR79(XElement sample)
        {
            // <checkedDataElements>;
            var sampM = (string)sample.Element("sampM");

            var outcome = new Outcome();
            outcome.name = "GBR79";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Month of sampling' (sampM) must be between 1 and 12;";
            outcome.error = "sampM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(sampM, out int result))
            {
                outcome.passed = result > 0 && result < 13;
            }
            return outcome;
        }
        ///The 'Arrival Month' (sampInfo.arrivalM) must be between 1 and 12;
        [Rule(Description = "The 'Arrival Month' (sampInfo.arrivalM) must be between 1 and 12;", ErrorMessage = "sampInfo.arrivalM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR80(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");

            var outcome = new Outcome();
            outcome.name = "GBR80";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Arrival Month' (sampInfo.arrivalM) must be between 1 and 12;";
            outcome.error = "sampInfo.arrivalM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(sampInfoarrivalM, out int result))
            {
                outcome.passed = result > 0 && result < 13;
            }
            return outcome;
        }

        ///The 'Month of production' (sampMatInfo.prodM) must be between 1 and 12;
        [Rule(Description = "The 'Month of production' (sampMatInfo.prodM) must be between 1 and 12;", ErrorMessage = "sampMatInfo.prodM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR81(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");

            var outcome = new Outcome();
            outcome.name = "GBR81";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Month of production' (sampMatInfo.prodM) must be between 1 and 12;";
            outcome.error = "sampMatInfo.prodM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(sampMatInfoprodM, out int result))
            {
                outcome.passed = result > 0 && result < 13;
            }
            return outcome;
        }
        ///The 'Month of expiry' (sampMatInfo.expiryM) must be between 1 and 12;
        [Rule(Description = "The 'Month of expiry' (sampMatInfo.expiryM) must be between 1 and 12;", ErrorMessage = "sampMatInfo.expiryM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR82(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");

            var outcome = new Outcome();
            outcome.name = "GBR82";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Month of expiry' (sampMatInfo.expiryM) must be between 1 and 12;";
            outcome.error = "sampMatInfo.expiryM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (int.TryParse(sampMatInfoexpiryM, out int result))
            {
                outcome.passed = result > 0 && result < 13;
            }
            return outcome;
        }

        ///The 'Month of analysis' (analysisM) must be between 1 and 12;
        [Rule(Description = "The 'Month of analysis' (analysisM) must be between 1 and 12;", ErrorMessage = "analysisM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR83(XElement sample)
        {
            // <checkedDataElements>;
            var analysisM = (string)sample.Element("analysisM");

            var outcome = new Outcome();
            outcome.name = "GBR83";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Month of analysis' (analysisM) must be between 1 and 12;";
            outcome.error = "analysisM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(analysisM))
            {
                outcome.passed = int.Parse(analysisM) > 0 && int.Parse(analysisM) < 13;
            }
            return outcome;
        }
        ///The 'Isolation month' (isolInfo.isolM) must be between 1 and 12;
        [Rule(Description = "The 'Isolation month' (isolInfo.isolM) must be between 1 and 12;", ErrorMessage = "isolInfo.isolM is not between 1 and 12;", RuleType = "error")]
        public Outcome GBR85(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");

            var outcome = new Outcome();
            outcome.name = "GBR85";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The 'Isolation month' (isolInfo.isolM) must be between 1 and 12;";
            outcome.error = "isolInfo.isolM is not between 1 and 12;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(isolInfoisolM))
            {
                outcome.passed = int.Parse(isolInfoisolM) > 0 && int.Parse(isolInfoisolM) < 13;
            }
            return outcome;
        }
        ///If the 'Day of slaughtering' (sampEventInfo.slaughterD) is reported, then the 'Month of slaughtering' (sampEventInfo.slaughterM) must be reported;
        [Rule(Description = "If the 'Day of slaughtering' (sampEventInfo.slaughterD) is reported, then the 'Month of slaughtering' (sampEventInfo.slaughterM) must be reported;", ErrorMessage = "sampEventInfo.slaughterM is missing, though sampEventInfo.slaughterD is reported;", RuleType = "error")]
        public Outcome GBR86(XElement sample)
        {
            // <checkedDataElements>;
            var sampEventInfoslaughterM = (string)sample.Element("sampEventInfo.slaughterM");
            var sampEventInfoslaughterD = (string)sample.Element("sampEventInfo.slaughterD");

            var outcome = new Outcome();
            outcome.name = "GBR86";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Day of slaughtering' (sampEventInfo.slaughterD) is reported, then the 'Month of slaughtering' (sampEventInfo.slaughterM) must be reported;";
            outcome.error = "sampEventInfo.slaughterM is missing, though sampEventInfo.slaughterD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampEventInfoslaughterD))
            {
                outcome.passed = !String.IsNullOrEmpty(sampEventInfoslaughterM);
            }

            return outcome;
        }
        ///If the 'Day of sampling' (sampD) is reported, then the 'Month of sampling' (sampM) must be reported;
        [Rule(Description = "If the 'Day of sampling' (sampD) is reported, then the 'Month of sampling' (sampM) must be reported;", ErrorMessage = "sampM is missing, though sampD is reported;", RuleType = "error")]
        public Outcome GBR87(XElement sample)
        {
            // <checkedDataElements>;
            var sampM = (string)sample.Element("sampM");
            var sampD = (string)sample.Element("sampD");

            var outcome = new Outcome();
            outcome.name = "GBR87";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Day of sampling' (sampD) is reported, then the 'Month of sampling' (sampM) must be reported;";
            outcome.error = "sampM is missing, though sampD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampD))
            {
                outcome.passed = !String.IsNullOrEmpty(sampM);
            }

            return outcome;
        }
        ///If the 'Arrival Day' (sampInfo.arrivalD) is reported, then the 'Arrival Month' (sampInfo.arrivalM) must be reported;
        [Rule(Description = "If the 'Arrival Day' (sampInfo.arrivalD) is reported, then the 'Arrival Month' (sampInfo.arrivalM) must be reported;", ErrorMessage = "sampInfo.arrivalM is missing, though sampInfo.arrivalD is reported;", RuleType = "error")]
        public Outcome GBR88(XElement sample)
        {
            // <checkedDataElements>;
            var sampInfoarrivalM = (string)sample.Element("sampInfo.arrivalM");
            var sampInfoarrivalD = (string)sample.Element("sampInfo.arrivalD");

            var outcome = new Outcome();
            outcome.name = "GBR88";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Arrival Day' (sampInfo.arrivalD) is reported, then the 'Arrival Month' (sampInfo.arrivalM) must be reported;";
            outcome.error = "sampInfo.arrivalM is missing, though sampInfo.arrivalD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampInfoarrivalD))
            {
                outcome.passed = !String.IsNullOrEmpty(sampInfoarrivalM);
            }

            return outcome;
        }
        ///If the 'Day of production' (sampMatInfo.prodD) is reported, then the 'Month of production' (sampMatInfo.prodM) must be reported;
        [Rule(Description = "If the 'Day of production' (sampMatInfo.prodD) is reported, then the 'Month of production' (sampMatInfo.prodM) must be reported;", ErrorMessage = "sampMatInfo.prodM is missing, though sampMatInfo.prodD is reported;", RuleType = "error")]
        public Outcome GBR89(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");

            var outcome = new Outcome();
            outcome.name = "GBR89";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Day of production' (sampMatInfo.prodD) is reported, then the 'Month of production' (sampMatInfo.prodM) must be reported;";
            outcome.error = "sampMatInfo.prodM is missing, though sampMatInfo.prodD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampMatInfoprodD))
            {
                outcome.passed = !String.IsNullOrEmpty(sampMatInfoprodM);
            }
            return outcome;
        }

        ///If the 'Day of expiry' (sampMatInfo.expiryD) is reported, then the 'Month of expiry' (sampMatInfo.expiryM) must be reported;
        [Rule(Description = "If the 'Day of expiry' (sampMatInfo.expiryD) is reported, then the 'Month of expiry' (sampMatInfo.expiryM) must be reported;", ErrorMessage = "sampMatInfo.expiryM is missing, though sampMatInfo.expiryD is reported;", RuleType = "error")]
        public Outcome GBR90(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");

            var outcome = new Outcome();
            outcome.name = "GBR90";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Day of expiry' (sampMatInfo.expiryD) is reported, then the 'Month of expiry' (sampMatInfo.expiryM) must be reported;";
            outcome.error = "sampMatInfo.expiryM is missing, though sampMatInfo.expiryD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampMatInfoexpiryM))
            {
                outcome.passed = !String.IsNullOrEmpty(sampMatInfoexpiryD);
            }

            return outcome;
        }
        ///If the 'Day of analysis' (analysisD) is reported, then the 'Month of analysis' (analysisM) must be reported;
        [Rule(Description = "If the 'Day of analysis' (analysisD) is reported, then the 'Month of analysis' (analysisM) must be reported;", ErrorMessage = "analysisM is missing, though analysisD is reported;", RuleType = "error")]
        public Outcome GBR91(XElement sample)
        {
            // <checkedDataElements>;
            var analysisM = (string)sample.Element("analysisM");
            var analysisD = (string)sample.Element("analysisD");

            var outcome = new Outcome();
            outcome.name = "GBR91";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Day of analysis' (analysisD) is reported, then the 'Month of analysis' (analysisM) must be reported;";
            outcome.error = "analysisM is missing, though analysisD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(analysisD))
            {
                outcome.passed = !String.IsNullOrEmpty(analysisM);
            }

            return outcome;
        }

        ///If the 'Completion day of analysis' (sampAnInfo.compD) is reported, then the 'Completion month of analysis' (sampAnInfo.compM) must be reported;
        [Rule(Description = "If the 'Completion day of analysis' (sampAnInfo.compD) is reported, then the 'Completion month of analysis' (sampAnInfo.compM) must be reported;", ErrorMessage = "sampAnInfo.compM is missing, though sampAnInfo.compD is reported;", RuleType = "error")]
        public Outcome GBR92(XElement sample)
        {
            // <checkedDataElements>;
            var sampAnInfocompM = (string)sample.Element("sampAnInfo.compM");
            var sampAnInfocompD = (string)sample.Element("sampAnInfo.compD");

            var outcome = new Outcome();
            outcome.name = "GBR92";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Completion day of analysis' (sampAnInfo.compD) is reported, then the 'Completion month of analysis' (sampAnInfo.compM) must be reported;";
            outcome.error = "sampAnInfo.compM is missing, though sampAnInfo.compD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!String.IsNullOrEmpty(sampAnInfocompD))
            {
                outcome.passed = !String.IsNullOrEmpty(sampAnInfocompM);
            }
            return outcome;
        }


        ///If the 'Isolation day' (isolInfo.isolD) is reported, then the 'Isolation month' (isolInfo.isolM) must be reported;
        [Rule(Description = "If the 'Isolation day' (isolInfo.isolD) is reported, then the 'Isolation month' (isolInfo.isolM) must be reported;", ErrorMessage = "isolInfo.isolM is missing, though isolInfo.isolD is reported;", RuleType = "error")]
        public Outcome GBR93(XElement sample)
        {
            // <checkedDataElements>;
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");

            var outcome = new Outcome();
            outcome.name = "GBR93";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "If the 'Isolation day' (isolInfo.isolD) is reported, then the 'Isolation month' (isolInfo.isolM) must be reported;";
            outcome.error = "isolInfo.isolM is missing, though isolInfo.isolD is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);

            if (!String.IsNullOrEmpty(isolInfoisolD))
            {
                outcome.passed = !String.IsNullOrEmpty(isolInfoisolM);
            }
            return outcome;
        }


        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY);
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY);", ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY;", RuleType = "error")]
        public Outcome GBR94(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var sampMatInfoexpiryD = (string)sample.Element("sampMatInfo.expiryD");
            var sampMatInfoexpiryM = (string)sample.Element("sampMatInfo.expiryM");
            var sampMatInfoexpiryY = (string)sample.Element("sampMatInfo.expiryY");

            var outcome = new Outcome();
            outcome.name = "GBR94";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the expiry, reported in 'Day of expiry' (sampMatInfo.expiryD), 'Month of expiry' (sampMatInfo.expiryM), and 'Year of expiry' (sampMatInfo.expiryY);";
            outcome.error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the expiry, reported in sampMatInfo.expiryD, sampMatInfo.expiryM, and sampMatInfo.expiryY;";
            outcome.type = "error";
            outcome.passed = true;

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, sampMatInfoexpiryY, sampMatInfoexpiryM, sampMatInfoexpiryD };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampInfo))
                {
                    if (DateTime.TryParseExact(sampMatInfoexpiryY + sampMatInfoexpiryM + sampMatInfoexpiryD, formats, null, DateTimeStyles.None, out DateTime sampExp))
                    {
                        outcome.passed = sampInfo <= sampExp;
                    }
                }
            }
            return outcome;
        }

        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);", ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;", RuleType = "error")]
        public Outcome GBR95(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");

            var outcome = new Outcome();
            outcome.name = "GBR95";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY);";
            outcome.error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the sampling, reported in sampD, sampM, and sampY;";
            outcome.type = "error";
            outcome.passed = true;

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, sampY, sampM, sampD };
            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampMatDate))
                {
                    if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                    {
                        outcome.passed = sampMatDate <= sampDate;
                    }
                }
            }

            return outcome;
        }

        ///The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);
        [Rule(Description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);", ErrorMessage = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;", RuleType = "error")]
        public Outcome GBR96(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatInfoprodD = (string)sample.Element("sampMatInfo.prodD");
            var sampMatInfoprodM = (string)sample.Element("sampMatInfo.prodM");
            var sampMatInfoprodY = (string)sample.Element("sampMatInfo.prodY");
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome();
            outcome.name = "GBR96";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the production, reported in 'Day of production' (sampMatInfo.prodD), 'Month of production' (sampMatInfo.prodM), and 'Year of production' (sampMatInfo.prodY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);";
            outcome.error = "The date of the production, reported in sampMatInfo.prodD, sampMatInfo.prodM, and sampMatInfo.prodY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;";
            outcome.type = "error";
            outcome.passed = true;

            var listOfNotEmpty = new List<string> { sampMatInfoprodD, sampMatInfoprodM, sampMatInfoprodY, analysisD, analysisM, analysisY };

            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampMatInfoprodY + sampMatInfoprodM + sampMatInfoprodD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(analysisY + analysisM + analysisD, formats, null, DateTimeStyles.None, out DateTime analysisDate))
                    {
                        outcome.passed = sampDate <= analysisDate;
                    }
                }
            }

            return outcome;
        }
        ///The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);
        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);", ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;", RuleType = "error")]
        public Outcome GBR97(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");
            var analysisD = (string)sample.Element("analysisD");
            var analysisM = (string)sample.Element("analysisM");
            var analysisY = (string)sample.Element("analysisY");

            var outcome = new Outcome();
            outcome.name = "GBR97";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the analysis, reported in 'Day of analysis' (analysisD), 'Month of analysis' (analysisM), and 'Year of analysis' (analysisY);";
            outcome.error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the analysis, reported in analysisD, analysisM, and analysisY;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY, analysisD, analysisM, analysisY };

            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(analysisY + analysisM + analysisD, formats, null, DateTimeStyles.None, out DateTime analysisDate))
                    {
                        outcome.passed = sampDate <= analysisDate;
                    }
                }
            }
            return outcome;
        }
        ///The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY);
        [Rule(Description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY);", ErrorMessage = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY;", RuleType = "error")]
        public Outcome GBR99(XElement sample)
        {
            // <checkedDataElements>;
            var sampD = (string)sample.Element("sampD");
            var sampM = (string)sample.Element("sampM");
            var sampY = (string)sample.Element("sampY");
            var isolInfoisolD = (string)sample.Element("isolInfo.isolD");
            var isolInfoisolM = (string)sample.Element("isolInfo.isolM");
            var isolInfoisolY = (string)sample.Element("isolInfo.isolY");

            var outcome = new Outcome();
            outcome.name = "GBR99";
            outcome.lastupdate = "2014-08-08";
            outcome.description = "The date of the sampling, reported in 'Day of sampling' (sampD), 'Month of sampling' (sampM), and 'Year of sampling' (sampY), must be less than or equal to the date of the isolation, reported in 'Isolation day' (isolInfo.isolD), 'Isolation month' (isolInfo.isolM), and 'Isolation year' (isolInfo.isolY);";
            outcome.error = "The date of the sampling, reported in sampD, sampM, and sampY, is not less than or equal to the date of the isolation, reported in isolInfo.isolD, isolInfo.isolM, and isolInfo.isolY;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            var listOfNotEmpty = new List<string> { sampD, sampM, sampY, isolInfoisolD, isolInfoisolM, isolInfoisolY };


            if (listOfNotEmpty.All(one => !string.IsNullOrEmpty(one)))
            {
                string[] formats = { "yyyyMMdd", "yyyyMMd", "yyyyMMd" };
                //This is NEVER going to happen
                if (DateTime.TryParseExact(sampY + sampM + sampD, formats, null, DateTimeStyles.None, out DateTime sampDate))
                {
                    if (DateTime.TryParseExact(isolInfoisolY + isolInfoisolM + isolInfoisolD, formats, null, DateTimeStyles.None, out DateTime isolDate))
                    {

                        outcome.passed = sampDate <= isolDate;
                    }
                }
            }
            return outcome;
        }




        public decimal? ParseDec(string s)
        {
            s = s.Replace(',', '.');
            return decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmp) ? tmp : default(decimal?);
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


            decimal.TryParse(s.Replace(".", ","), out decimal r);
            return r;
        }
    }


}
