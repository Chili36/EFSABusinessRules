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
        private string _yearToTest;

        /// <summary>
        /// Constructor for <see cref="ChemBusinessRules"/>
        /// </summary>
        /// <param name="yearToTest">The year that the rules to test against, correct format is YYYY</param>
        public ChemBusinessRules(string yearToTest)
        {
            _yearToTest = yearToTest;
        }

        #region Testmetoder

        [Rule(Description = "This is a testmethod", ErrorMessage = "This method doesn´t return an error", RuleType = "Test", Deprecated = false)]
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

        [Rule(Description = "This is a testmethod that returns an error", ErrorMessage = "A test error", RuleType = "Test", Deprecated = false)]
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

        [Rule(Description = "The value in 'Reporting year' (repYear) must be the same as the data collection reporting year;",
            ErrorMessage = "repYear is not equal to the data collection reporting year;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM01(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM01",
                Passed = true,
                Description = "The value in 'Reporting year' (repYear) must be the same as the data collection reporting year;",
                Error = "repYear is not equal to the data collection reporting year;",
                Lastupdate = "2017-04-12",
                Type = "error",
            };
            var repYear = sample.Element("repYear")?.Value;
            if (string.IsNullOrEmpty(repYear))
                return outcome;
            outcome.Passed = int.Parse(repYear) == int.Parse(_yearToTest);
            return outcome;
        }

        [Rule(Description = "The data element 'Result qualitative value' (resQualValue) should be left empty since binary data are not accepted;",
            ErrorMessage = "resQualValue is reported, though it should be left empty since binary data are not accepted;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM02(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM02",
                Passed = true,
                Description = "The data element 'Result qualitative value' (resQualValue) should be left empty since binary data are not accepted;",
                Error = "resQualValue is reported, though it should be left empty since binary data are not accepted;",
                Lastupdate = "2017-01-20",
                Type = "error",
            };
            var resQualValue = sample.Element("resQualValue")?.Value;
            outcome.Passed = resQualValue == null;
            return outcome;
        }

        [Rule(Description = "A value in the data element 'Result LOQ' (resLOQ) should be reported;",
            ErrorMessage = "WARNING: resLOQ is missing, though strongly recommended;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM03(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM03",
                Passed = true,
                Description = "A value in the data element 'Result LOQ' (resLOQ) should be reported;",
                Error = "WARNING: resLOQ is missing, though strongly recommended;",
                Lastupdate = "2017-01-20",
                Type = "warning",
            };
            var resLOQ = sample.Element("resLOQ")?.Value;
            outcome.Passed = !string.IsNullOrEmpty(resLOQ);
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Result value recovery' (resValRec) should be greater than or equal to 1 (for 85%, the value 85 should be reported);",
            ErrorMessage = "WARNING: resValRec is less than 1. Please check whether the resValRec is correctly reported(e.g. for 85%, the value 85 should be reported);",
            RuleType = "warning", Deprecated = false)]
        public Outcome CHEM04(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM04",
                Passed = true,
                Description = "The value in the data element 'Result value recovery' (resValRec) should be greater than or equal to 1 (for 85%, the value 85 should be reported);",
                Error = "WARNING: resValRec is less than 1. Please check whether the resValRec is correctly reported(e.g. for 85%, the value 85 should be reported);",
                Lastupdate = "2016-04-27",
                Type = "warning",
            };
            var resValRec = sample.Element("resValRec")?.Value;
            if (string.IsNullOrEmpty(resValRec))
                return outcome;
            resValRec = resValRec.Replace('.', ',');
            outcome.Passed = decimal.Parse(resValRec) >= 1;
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Result value recovery' (resValRec) should be between 50 and 150;",
            ErrorMessage = "WARNING: resValRec is not between 50 and 150;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM05(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM05",
                Passed = true,
                Description = "The value in the data element 'Result value recovery' (resValRec) should be between 50 and 150;",
                Error = "WARNING: resValRec is not between 50 and 150;",
                Lastupdate = "2017-01-30",
                Type = "warning",
            };
            var resValRec = sample.Element("resValRec")?.Value;
            if (string.IsNullOrEmpty(resValRec))
                return outcome;
            resValRec = resValRec.Replace('.', ',');
            outcome.Passed = decimal.Parse(resValRec) >= 50 && decimal.Parse(resValRec) <= 150;           
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of fat in the original sample' (exprResPerc.fatPerc) should be greater than or equal to 1 (e.g. for 85%, the value 85 should be reported);",
            ErrorMessage = "WARNING: exprResPerc.fatPerc is less than 1. Please check whether the exprResPerc.fatPerc is correctly reported(e.g. for 85%, the value 85 should be reported);",
            RuleType = "warning", Deprecated = false)]
        public Outcome CHEM06(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM06",
                Passed = true,
                Description = "The value in the data element 'Percentage of fat in the original sample' (exprResPerc.fatPerc) should be greater than or equal to 1 (e.g. for 85%, the value 85 should be reported);",
                Error = "WARNING: exprResPerc.fatPerc is less than 1. Please check whether the exprResPerc.fatPerc is correctly reported(e.g. for 85%, the value 85 should be reported);",
                Lastupdate = "2017-04-12",
                Type = "warning",
            };
            var exprResPerc = sample.Element("exprResPerc")?.Value;
            if (string.IsNullOrEmpty(exprResPerc))
                return outcome;
            var split = exprResPerc.Split('=');
            if (split.Count() == 2 && split[0] == "fatPerc")
            {
                outcome.Passed = decimal.Parse(split[1].Replace('.', ',')) >= 1;
            }
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Percentage of moisture in the original sample' (exprResPerc.moistPerc) should be greater than or equal to 1 (e.g. for 85%, the value 85 should be reported);",
            ErrorMessage = "WARNING: exprResPerc.moistPerc is less than 1. Please check whether the exprResPerc.moistPerc is correctly reported(e.g. for 85%, the value 85 should be reported);", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM07(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM07",
                Passed = true,
                Description = "The value in the data element 'Percentage of moisture in the original sample' (exprResPerc.moistPerc) should be greater than or equal to 1 (e.g. for 85%, the value 85 should be reported);",
                Error = "WARNING: exprResPerc.moistPerc is less than 1. Please check whether the exprResPerc.moistPerc is correctly reported(e.g. for 85%, the value 85 should be reported);",
                Lastupdate = "2017-04-12",
                Type = "warning",
            };
            var exprResPerc = sample.Element("exprResPerc")?.Value;
            if (string.IsNullOrEmpty(exprResPerc))
                return outcome;
            var split = exprResPerc.Split('=');
            if (split.Count() == 2 && split[0] == "moistPerc")
            {
                outcome.Passed = decimal.Parse(split[1]) >= 1;
            }
            return outcome;
        }

        [Rule(Description = "The value in the data element 'Result value' (resVal) must be greater than or equal to the value in the data element 'Result LOD' (resLOD);",
            ErrorMessage = "resVal is not greater than or equal to resLOD;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM08(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM08",
                Passed = true,
                Description = "The value in the data element 'Result value' (resVal) must be greater than or equal to the value in the data element 'Result LOD' (resLOD);",
                Error = "resVal is not greater than or equal to resLOD;",
                Lastupdate = "2017-03-20",
                Type = "error",
            };
            var resVal = sample.Element("resVal")?.Value;
            var resLOD = sample.Element("resLOD")?.Value;
            if (string.IsNullOrEmpty(resVal) || string.IsNullOrEmpty(resLOD))
                return outcome;
            outcome.Passed = decimal.Parse(resVal) >= decimal.Parse(resLOD);
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Result value' (resVal) is equal to the value in the data element 'Result LOQ' (resLOQ), then the value in the data element 'Type of result' (resType) must be equal to 'Non Quantified Value (below LOQ)' (LOQ) or 'Result value' (resVal);",
            ErrorMessage = "resType is not equal to LOQ or VAL, though resVal is equal to resLOQ;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM09(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM09",
                Passed = true,
                Description = "If the value in the data element 'Result value' (resVal) is equal to the value in the data element 'Result LOQ' (resLOQ), then the value in the data element 'Type of result' (resType) must be equal to 'Non Quantified Value (below LOQ)' (LOQ) or 'Result value' (resVal);",
                Error = "resType is not equal to LOQ or VAL, though resVal is equal to resLOQ;",
                Lastupdate = "2016-09-01",
                Type = "error",
            };
            var resVal = sample.Element("resVal")?.Value;
            var resLOQ = sample.Element("resLOQ")?.Value;
            var resType = sample.Element("resType")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(resVal) || string.IsNullOrEmpty(resLOQ))
                return outcome;
            resLOQ = resLOQ.Replace('.', ',');
            resVal = resVal.Replace('.', ',');
            var equal = decimal.Parse(resVal) == decimal.Parse(resLOQ);
            if (equal)
            {
                outcome.Passed = resType == "LOQ" || resType == "VAL";
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Mineral oils' (RF-00000396-ORG), or 'Mycotoxins' (RF-00000132-TOX), then a value in the data element 'Percentage of moisture in the original sample' (exprResPerc.moistPerc) should be reported(regardless of whether the result value is expressed in whole weight, fat weight or dry matter);",
            ErrorMessage = "WARNING: exprResPerc.moistPerc is missing, though recommended when paramCode is a mineral oil or a mycotoxin;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM12(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM12",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Mineral oils' (RF-00000396-ORG), or 'Mycotoxins' (RF-00000132-TOX), then a value in the data element 'Percentage of moisture in the original sample' (exprResPerc.moistPerc) should be reported(regardless of whether the result value is expressed in whole weight, fat weight or dry matter);",
                Error = "WARNING: exprResPerc.moistPerc is missing, though recommended when paramCode is a mineral oil or a mycotoxin;",
                Lastupdate = "2017-01-20",
                Type = "warning",
            };
            var mineralOilCodes = new List<string>() { "RF-00000396-ORG", "RF-00000397-ORG", "RF-00000398-ORG" };
            var mycotoxins = new List<string>() { "RF-00000132-TOX","RF-00000149-TOX","RF-00000150-TOX","RF-00000151-TOX","RF-00000152-TOX","RF-00000153-TOX","RF-00000154-TOX","RF-00000155-TOX",
                                                    "RF-00000156-TOX","RF-00000435-TOX","RF-00000157-TOX","RF-00000158-TOX","RF-00000159-TOX","RF-00000160-TOX","RF-00000164-TOX","RF-00000163-TOX","RF-00000166-TOX","RF-00000174-TOX","RF-00000175-TOX",
                                                    "RF-00000176-TOX","RF-00000177-TOX","RF-00003058-PAR","RF-00000178-TOX","RF-00000179-TOX","RF-00000180-TOX","RF-00001337-PAR","RF-00003059-PAR","RF-00000181-TOX","RF-00000432-TOX","RF-00000161-TOX",
                                                    "RF-00000162-TOX","RF-00000320-TOX","RF-00004778-PAR","RF-00000433-TOX","RF-00000165-TOX","RF-00002849-PAR","RF-00002850-PAR","RF-00002851-PAR","RF-00002852-PAR","RF-00002853-PAR","RF-00002854-PAR",
                                                    "RF-00002855-PAR","RF-00000167-TOX","RF-00000168-TOX","RF-00000169-TOX","RF-00000460-TOX","RF-00000440-TOX","RF-00000170-TOX","RF-00003054-PAR","RF-00003055-PAR","RF-00000171-TOX","RF-00000172-TOX",
                                                    "RF-00000173-TOX","RF-00003056-PAR","RF-00003057-PAR","RF-00000182-TOX","RF-00000183-TOX","RF-00000184-TOX","RF-00000185-TOX","RF-00000448-VET","RF-00000449-VET","RF-00004779-PAR","RF-00000186-TOX",
                                                    "RF-00000187-TOX","RF-00000195-TOX","RF-00000196-TOX","RF-00000197-TOX","RF-00000198-TOX","RF-00000199-TOX","RF-00000188-TOX","RF-00000189-TOX","RF-00000190-TOX","RF-00000191-TOX","RF-00000192-TOX",
                                                    "RF-00000193-TOX","RF-00000194-TOX","RF-00000200-TOX","RF-00000201-TOX","RF-00000202-TOX","RF-00000203-TOX","RF-00000204-TOX","RF-00000205-TOX","RF-00000206-TOX","RF-00000372-TOX","RF-00000207-TOX",
                                                    "RF-00000208-TOX","RF-00000209-TOX","RF-00000211-TOX","RF-00000212-TOX","RF-00000213-TOX","RF-00000214-TOX","RF-00000215-TOX","RF-00000216-TOX","RF-00000217-TOX","RF-00000218-TOX","RF-00001345-PAR",
                                                    "RF-00001346-PAR","RF-00000210-TOX","RF-00000219-TOX","RF-00000220-TOX","RF-00000221-TOX","RF-00000222-TOX","RF-00000430-TOX","RF-00000148-TOX","RF-00000431-TOX","RF-00000434-TOX","RF-00004552-PAR",
                                                    "RF-00004690-PAR",};
            var allcodes = mineralOilCodes.Union(mycotoxins);
            var paramCode = sample.Element("paramCode")?.Value;
            var exprResPerc = sample.Element("exprResPerc")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(paramCode))
            {
                outcome.Passed = false;
            }
            else
            {
                if (allcodes.Any(x => x == paramCode))
                {
                    var split = exprResPerc.Split('=');
                    if (split.Count() == 2)
                    {
                        if (split[1] != "moistPerc")
                            outcome.Passed = false;
                    }
                    else
                    {
                        outcome.Passed = false;
                    }
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) is 'Acrylamide' (RF-00000410-ORG), then the value in the data element 'Legislative-classes' (sampMatCode.legis) must be reported and must contain the additional product classification(categories and sub-categories) based on Commission Recommendation 2010/307/EC on the monitoring of acrylamide and Commission Recommendation 2013/647/EU on investigations into the levels of acrylamide in food;",
            ErrorMessage = "sampMatCode.legis is not reported or does not contain specific product code, though paramCode is acrylamide(it is mandatory to provide additional product classification based on Commission Recommendation 2010/307/EC and 2013/647/EU on acrylamide);", RuleType = "error", Deprecated = false)]
        public Outcome CHEM13(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM13",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) is 'Acrylamide' (RF-00000410-ORG), then the value in the data element 'Legislative-classes' (sampMatCode.legis) must be reported and must contain the additional product classification(categories and sub-categories) based on Commission Recommendation 2010/307/EC on the monitoring of acrylamide and Commission Recommendation 2013/647/EU on investigations into the levels of acrylamide in food;",
                Error = "sampMatCode.legis is not reported or does not contain specific product code, though paramCode is acrylamide(it is mandatory to provide additional product classification based on Commission Recommendation 2010/307/EC and 2013/647/EU on acrylamide);",
                Lastupdate = "2017-04-12",
                Type = "error",
            };
            var paramCode = sample.Element("paramCode")?.Value;
            var sampMatCode = sample.Element("sampMatCode")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(paramCode))
            {
                outcome.Passed = false;
            }
            else
            {
                if (paramCode == "RF-00000410-ORG")
                {
                    if (!sampMatCode.Contains("F33"))
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) is equal to 'Furan' (RF-00000073-ORG), then the value in the data element 'Sample analysed identification code' (sampAnId) and 'Coded description of the analysed matrix' (anMatCode) must be reported to specify whether the sample has been analysed 'as purchased' or 'as consumed' (Commission Recommendation 2007/196/EC on the monitoring of the presence of furan in foodstuffs);",
            ErrorMessage = "sampAnId or anMatCode is missing, though mandatory when the parameter is furan(Commission Recommendation 2007/196/EC on the monitoring of the presence of furan in foodstuffs);", RuleType = "error", Deprecated = false)]
        public Outcome CHEM14(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM14",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) is equal to 'Furan' (RF-00000073-ORG), then the value in the data element 'Sample analysed identification code' (sampAnId) and 'Coded description of the analysed matrix' (anMatCode) must be reported to specify whether the sample has been analysed 'as purchased' or 'as consumed' (Commission Recommendation 2007/196/EC on the monitoring of the presence of furan in foodstuffs);",
                Error = "sampAnId or anMatCode is missing, though mandatory when the parameter is furan(Commission Recommendation 2007/196/EC on the monitoring of the presence of furan in foodstuffs);",
                Lastupdate = "2017-01-30",
                Type = "error",
            };
            var furanChilds = new List<string>() { "RF-00000073-ORG", "RF-00003384-PAR", "RF-00003385-PAR", "RF-00004529-PAR", };
            var paramCode = sample.Element("paramCode")?.Value;
            var sampAnId = sample.Element("sampAnId")?.Value;
            var anMatCode = sample.Element("anMatCode")?.Value;
            if (string.IsNullOrEmpty(paramCode))
            {
                outcome.Passed = false;
            }
            else
            {
                if (furanChilds.Any(x => x == paramCode))
                {
                    if (sampAnId == null || anMatCode == null)
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Bisphenol compounds' (RF-00001240-PAR), then a value in the data element 'Packaging-material' (sampMatCode.packmat) must be reported;",
            ErrorMessage = "sampMatCode.packmat is missing, though mandatory when paramCode is a bisphenol compound;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM15(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM15",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Bisphenol compounds' (RF-00001240-PAR), then a value in the data element 'Packaging-material' (sampMatCode.packmat) must be reported;",
                Error = "sampMatCode.packmat is missing, though mandatory when paramCode is a bisphenol compound;",
                Lastupdate = "2017-10-10",
                Type = "error",
            };
            var bisphenolChilds = new List<string>() { "RF-00001240-PAR", "RF-00000482-ORG", "RF-00001237-PAR", "RF-00001238-PAR", "RF-00001239-PAR" };
            var paramCode = sample.Element("paramCode")?.Value;
            var sampMatCode = sample.Element("sampMatCode")?.Value;
            if (string.IsNullOrEmpty(paramCode))
            {
                outcome.Passed = false;
            }
            else
            {
                if (bisphenolChilds.Any(x => x == paramCode))
                {
                    if (!sampMatCode.Contains("F19"))
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Polycyclic aromatic hydrocarbons' (RF-00000040-ORG), then a value in the data element 'Packaging-material' (sampMatCode.packmat) should be reported;",
            ErrorMessage = "WARNING: sampMatCode.packmat is missing, though recommended when paramCode is a PAH;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM15_a(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM15_a",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Polycyclic aromatic hydrocarbons' (RF-00000040-ORG), then a value in the data element 'Packaging-material' (sampMatCode.packmat) should be reported;",
                Error = "WARNING: sampMatCode.packmat is missing, though recommended when paramCode is a PAH;",
                Lastupdate = "2017-10-10",
                Type = "warning",
            };
            var polycyclicChilds = new List<string>() { "RF-00000040-ORG","RF-00000041-ORG","RF-00000042-ORG","RF-00000043-ORG","RF-00000044-ORG","RF-00000045-ORG","RF-00000046-ORG",
                                                        "RF-00000047-ORG","RF-00000048-ORG","RF-00000049-ORG","RF-00000050-ORG","RF-00000051-ORG","RF-00000052-ORG","RF-00000053-ORG",
                                                        "RF-00000054-ORG","RF-00000055-ORG","RF-00000056-ORG","RF-00000057-ORG","RF-00000058-ORG","RF-00000059-ORG","RF-00000060-ORG",
                                                        "RF-00000061-ORG","RF-00000062-ORG","RF-00000063-ORG","RF-00000064-ORG","RF-00000065-ORG","RF-00000066-ORG","RF-00000067-ORG",
                                                        "RF-00000448-ORG","RF-00000068-ORG","RF-00000069-ORG","RF-00000436-ORG","RF-00000437-ORG","RF-00000438-ORG","RF-00000446-ORG",
                                                        "RF-00004425-PAR","RF-00004426-PAR","RF-00004427-PAR","RF-00004691-PAR"};
            var paramCode = sample.Element("paramCode")?.Value;
            var sampMatCode = sample.Element("sampMatCode")?.Value;
            if (string.IsNullOrEmpty(paramCode))
            {
                outcome.Passed = false;
            }
            else
            {
                if (polycyclicChilds.Any(x => x == paramCode))
                {
                    if (!sampMatCode.Contains("F19"))
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) is equal to '3-MCPD esters (expressed as 3-MCPD moiety)' (RF-00000380-ORG), or '3-MCPD total (expressed as sum of 3-MCPD free and 3-MCPD esters expressed as 3-MCPD moiety)' (RF-00000378-ORG), or '3-MCPD free' (RF-00000377-ORG), then in the data element 'Analytical method text' (anMethText) it is mandatory to clarify whether the sample has been analysed with one of the three methods validated by American Oil Chemists’ Society(AOCS) in October 2013 (AOCS, 2013) (AOCS1, AOCS2, AOCS3, or a variant of them), or a different method;",
            ErrorMessage = "anMethText does not contain the method used for analysis of MCPDs or glycidyl esters;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM16(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM16",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) is equal to '3-MCPD esters (expressed as 3-MCPD moiety)' (RF-00000380-ORG), or '3-MCPD total (expressed as sum of 3-MCPD free and 3-MCPD esters expressed as 3-MCPD moiety)' (RF-00000378-ORG), or '3-MCPD free' (RF-00000377-ORG), then in the data element 'Analytical method text' (anMethText) it is mandatory to clarify whether the sample has been analysed with one of the three methods validated by American Oil Chemists’ Society(AOCS) in October 2013 (AOCS, 2013) (AOCS1, AOCS2, AOCS3, or a variant of them), or a different method;",
                Error = "anMethText does not contain the method used for analysis of MCPDs or glycidyl esters;",
                Lastupdate = "2016-04-27",
                Type = "error",
            };
            var paramCode = sample.Element("paramCode")?.Value;
            var anMethText = sample.Element("anMethText")?.Value;
            var codes = new List<string>() { "RF-00002833-PAR", "RF-00000380-ORG", "RF-00000378-ORG", "RF-00002834-PAR", "RF-00001344-PAR" };
            if (string.IsNullOrEmpty(paramCode))
                outcome.Passed = false;
            else
            {
                if (codes.Any(x => x == paramCode))
                {
                    outcome.Passed = !string.IsNullOrEmpty(anMethText);
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Mycotoxins' (RF-00000132-TOX), then the value in the data element 'Method of production' (sampMatCode.prod) should be reported;",
            ErrorMessage = "WARNING: sampMatCode.prod is missing though it is recommended to report whether the sample was obtained from the produce of traditional(non-organic) or organic farming when paramCode is a mycotoxin;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM17(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM17",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Mycotoxins' (RF-00000132-TOX), then the value in the data element 'Method of production' (sampMatCode.prod) should be reported;",
                Error = "WARNING: sampMatCode.prod is missing though it is recommended to report whether the sample was obtained from the produce of traditional(non-organic) or organic farming when paramCode is a mycotoxin;",
                Lastupdate = "2017-01-20",
                Type = "warning",
            };
            var mycotoxins = new List<string>() { "RF-00000132-TOX","RF-00000149-TOX","RF-00000150-TOX","RF-00000151-TOX","RF-00000152-TOX","RF-00000153-TOX","RF-00000154-TOX","RF-00000155-TOX",
                                                    "RF-00000156-TOX","RF-00000435-TOX","RF-00000157-TOX","RF-00000158-TOX","RF-00000159-TOX","RF-00000160-TOX","RF-00000164-TOX","RF-00000163-TOX","RF-00000166-TOX","RF-00000174-TOX","RF-00000175-TOX",
                                                    "RF-00000176-TOX","RF-00000177-TOX","RF-00003058-PAR","RF-00000178-TOX","RF-00000179-TOX","RF-00000180-TOX","RF-00001337-PAR","RF-00003059-PAR","RF-00000181-TOX","RF-00000432-TOX","RF-00000161-TOX",
                                                    "RF-00000162-TOX","RF-00000320-TOX","RF-00004778-PAR","RF-00000433-TOX","RF-00000165-TOX","RF-00002849-PAR","RF-00002850-PAR","RF-00002851-PAR","RF-00002852-PAR","RF-00002853-PAR","RF-00002854-PAR",
                                                    "RF-00002855-PAR","RF-00000167-TOX","RF-00000168-TOX","RF-00000169-TOX","RF-00000460-TOX","RF-00000440-TOX","RF-00000170-TOX","RF-00003054-PAR","RF-00003055-PAR","RF-00000171-TOX","RF-00000172-TOX",
                                                    "RF-00000173-TOX","RF-00003056-PAR","RF-00003057-PAR","RF-00000182-TOX","RF-00000183-TOX","RF-00000184-TOX","RF-00000185-TOX","RF-00000448-VET","RF-00000449-VET","RF-00004779-PAR","RF-00000186-TOX",
                                                    "RF-00000187-TOX","RF-00000195-TOX","RF-00000196-TOX","RF-00000197-TOX","RF-00000198-TOX","RF-00000199-TOX","RF-00000188-TOX","RF-00000189-TOX","RF-00000190-TOX","RF-00000191-TOX","RF-00000192-TOX",
                                                    "RF-00000193-TOX","RF-00000194-TOX","RF-00000200-TOX","RF-00000201-TOX","RF-00000202-TOX","RF-00000203-TOX","RF-00000204-TOX","RF-00000205-TOX","RF-00000206-TOX","RF-00000372-TOX","RF-00000207-TOX",
                                                    "RF-00000208-TOX","RF-00000209-TOX","RF-00000211-TOX","RF-00000212-TOX","RF-00000213-TOX","RF-00000214-TOX","RF-00000215-TOX","RF-00000216-TOX","RF-00000217-TOX","RF-00000218-TOX","RF-00001345-PAR",
                                                    "RF-00001346-PAR","RF-00000210-TOX","RF-00000219-TOX","RF-00000220-TOX","RF-00000221-TOX","RF-00000222-TOX","RF-00000430-TOX","RF-00000148-TOX","RF-00000431-TOX","RF-00000434-TOX","RF-00004552-PAR",
                                                    "RF-00004690-PAR",};
            var paramCode = sample.Element("paramCode")?.Value;
            var sampMatCode = sample.Element("sampMatCode")?.Value;
            if (string.IsNullOrEmpty(paramCode))
                outcome.Passed = false;
            else
            {
                if (mycotoxins.Any(x => x == paramCode))
                {
                    if (!sampMatCode.Contains("F21"))
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Arsenic and derivatives' (RF-00000127-CHE), and data are reported on rice(as grains for human consumption), then the value in the data element 'Process' (sampMatCode.process) should be reported;",
            ErrorMessage = "WARNING: sampMatCode.process is not reported, though it is recommended to specifiy at least the codes for 'processed' or 'unprocessed' when reporting data on rice and paramCode is arsenic;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM18(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM18",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Arsenic and derivatives' (RF-00000127-CHE), and data are reported on rice(as grains for human consumption), then the value in the data element 'Process' (sampMatCode.process) should be reported;",
                Error = "WARNING: sampMatCode.process is not reported, though it is recommended to specifiy at least the codes for 'processed' or 'unprocessed' when reporting data on rice and paramCode is arsenic;",
                Lastupdate = "2017-01-20",
                Type = "warning",
            };
            var arsenicChilds = new List<string>() { "RF-00000127-CHE","RF-00000128-CHE","RF-00000129-CHE","RF-00000132-CHE","RF-00000130-CHE","RF-00000131-CHE","RF-00000133-CHE",
                                            "RF-00000134-CHE","RF-00000136-CHE","RF-00000137-CHE","RF-00000140-CHE","RF-00000141-CHE","RF-00000135-CHE","RF-00000138-CHE","RF-00000139-CHE"};
            var paramCode = sample.Element("paramCode")?.Value;
            var sampMatCode = sample.Element("sampMatCode")?.Value;
            if (!string.IsNullOrEmpty(paramCode))
            {
                if (arsenicChilds.Any(x => x == paramCode))
                {
                    if (!sampMatCode.Contains("F28"))
                        outcome.Passed = false;
                }
            }
            return outcome;
        }

        //[Rule(Description = "If the value in the data element 'Parameter code' (paramCode) is equal to 'Chlorates' (RF-00000015-CHE), or 'Perchlorate' (RF-00001336-PAR), then the value in the data element 'Process' (sampMatCode.process) must be different from 'Unknown' (T899A);",
        //    ErrorMessage = "sampmatcode.process is unknown, though it is mandatory to specifiy at least the code for process or unprocessed when paramcode is chlorate or perchlorate;", 
        //    RuleType = "error", Deprecated = false)]
        //public Outcome CHEM19(XElement sample)
        //{
        //    var outcome = new Outcome
        //    {
        //        Name = "chem19",
        //        Passed = true,
        //        Description = "if the value in the data element 'parameter code' (paramcode) is equal to 'chlorates' (rf-00000015-che), or 'perchlorate' (rf-00001336-par), then the value in the data element 'process' (sampmatcode.process) must be different from 'unknown' (t899a);",
        //        Error = "sampmatcode.process is unknown, though it is mandatory to specifiy at least the code for process or unprocessed when paramcode is chlorate or perchlorate;",
        //        Lastupdate = "2017-01-20",
        //        Type = "error",
        //    };
        //    var paramcode = sample.Element("paramcode")?.Element("base")?.Value;
        //    var sampmatcode = sample.Element("sampmatcode")?.Element("base")?.Value;
        //    if (string.IsNullOrEmpty(paramcode))
        //    {
        //        outcome.Passed = true;
        //    }
        //    else
        //    {
        //        if (paramcode == "rf-00000015-che" || paramcode == "rf-00001336-par")
        //        {
        //            //f28                    
        //            var process = string.Empty;
        //            foreach (var item in sampmatcode.Elements("value"))
        //            {
        //                if (item.Attribute("code").Value == "F28")
        //                    process = item.Value;
        //            }
        //            if(process != null)
        //            {
        //                //if(process == "t899a" //Här ska det vara en facett???)
        //            }
        //        }
        //    }
        //    return outcome;
        //}   

        //[Rule(Description = "If the value in the data element 'Coded description of the matrix sampled' (sampMatCode) has as ancestor 'Fish and other seafood (including amphibians, reptiles, snails and insects' (A.01.000876), or 'Fish, other aquatic animals and products derived thereof' (G.10) and the value in the data element 'Parameter code' (paramCode) has as ancestor 'Brominated flame retardants' (RF-00000074-ORG), or 'Dioxins and PCBs' (RF-00000114-ORG), or 'Mercury and derivatives' (RF-00000169-CHE), then a value in 'Area of origin for fisheries or aquaculture activities code' (origFishAreaCode) should be reported;",
        //    ErrorMessage = "WARNING: origFishAreaCode is missing, though recommended when data are reported on fish, and paramCode is BFR, dioxins and PCBs, or mercury and derivatives;", RuleType = "warning", Deprecated = false)]
        //public Outcome CHEM20(XElement sample)
        //{
        //    var outcome = new Outcome
        //    {
        //        Name = "CHEM20",
        //        Passed = true,
        //        Description = "If the value in the data element 'Coded description of the matrix sampled' (sampMatCode) has as ancestor 'Fish and other seafood (including amphibians, reptiles, snails and insects' (A.01.000876), or 'Fish, other aquatic animals and products derived thereof' (G.10) and the value in the data element 'Parameter code' (paramCode) has as ancestor 'Brominated flame retardants' (RF-00000074-ORG), or 'Dioxins and PCBs' (RF-00000114-ORG), or 'Mercury and derivatives' (RF-00000169-CHE), then a value in 'Area of origin for fisheries or aquaculture activities code' (origFishAreaCode) should be reported;",
        //        Error = "WARNING: origFishAreaCode is missing, though recommended when data are reported on fish, and paramCode is BFR, dioxins and PCBs, or mercury and derivatives;",
        //        Lastupdate = "2017-01-20",
        //        Type = "warning",
        //    };
        //    var brominatedChilds = new List<string>() { "RF-00000074-ORG","RF-00000075-ORG", "RF-00000076-ORG", "RF-00000077-ORG", "RF-00000078-ORG", "RF-00000079-ORG",
        //                                    "RF-00000080-ORG", "RF-00004916-PAR", "RF-00000081-ORG", "RF-00000082-ORG", "RF-00000083-ORG", "RF-00000084-ORG", "RF-00000085-ORG",
        //                                    "RF-00000086-ORG", "RF-00000087-ORG", "RF-00000088-ORG", "RF-00000089-ORG", "RF-00000090-ORG", "RF-00000091-ORG", "RF-00000092-ORG",
        //                                    "RF-00000093-ORG", "RF-00000094-ORG", "RF-00000095-ORG", "RF-00000384-ORG", "RF-00000385-ORG", "RF-00000387-ORG", "RF-00000388-ORG",
        //                                    "RF-00000389-ORG", "RF-00000390-ORG", "RF-00000391-ORG", "RF-00000096-ORG", "RF-00000097-ORG", "RF-00000098-ORG", "RF-00000099-ORG",
        //                                    "RF-00000100-ORG", "RF-00000101-ORG", "RF-00000102-ORG", "RF-00000103-ORG", "RF-00000104-ORG", "RF-00000105-ORG", "RF-00000106-ORG",
        //                                    "RF-00000107-ORG", "RF-00000108-ORG", "RF-00000109-ORG", "RF-00000110-ORG", "RF-00000111-ORG", "RF-00000112-ORG", "RF-00000113-ORG",
        //                                    "RF-00000392-ORG", "RF-00000393-ORG", "RF-00000394-ORG", "RF-00001338-PAR",  "RF-00001339-PAR",  "RF-00004689-PAR", "RF-00004882-PAR",
        //                                    "RF-00004883-PAR" };
        //    var dioxinerChilds = new List<string>() { "RF-00000114-ORG","RF-00000003-REP","RF-00000381-ORG","RF-00000382-ORG","RF-00000383-ORG","RF-00000395-ORG","RF-00000463-ORG","RF-00000464-ORG","RF-00000465-ORG","RF-00000466-ORG","RF-00000467-ORG","RF-00000468-ORG","RF-00000471-ORG","RF-00000472-ORG","RF-00000115-ORG","RF-00000116-ORG","RF-00000117-ORG","RF-00000118-ORG","RF-00000119-ORG","RF-00000120-ORG",
        //                                                "RF-00000121-ORG","RF-00000122-ORG","RF-00000123-ORG","RF-00000124-ORG","RF-00000125-ORG","RF-00000126-ORG","RF-00000127-ORG","RF-00000128-ORG","RF-00000129-ORG","RF-00000130-ORG","RF-00000131-ORG","RF-00000132-ORG","RF-00000133-ORG","RF-00000134-ORG","RF-00000135-ORG","RF-00000136-ORG","RF-00000137-ORG","RF-00000138-ORG","RF-00000139-ORG","RF-00000140-ORG","RF-00000141-ORG","RF-00000142-ORG","RF-00000143-ORG",
        //                                                "RF-00000144-ORG","RF-00000145-ORG","RF-00000146-ORG","RF-00000147-ORG","RF-00000148-ORG","RF-00000149-ORG","RF-00000150-ORG","RF-00000151-ORG","RF-00000152-ORG","RF-00000153-ORG","RF-00000154-ORG","RF-00000155-ORG","RF-00000156-ORG","RF-00000157-ORG","RF-00000158-ORG","RF-00000159-ORG","RF-00000160-ORG","RF-00000161-ORG","RF-00000162-ORG","RF-00000163-ORG","RF-00000164-ORG","RF-00000165-ORG","RF-00000166-ORG",
        //                                                "RF-00000167-ORG","RF-00000168-ORG","RF-00000169-ORG","RF-00000170-ORG","RF-00000171-ORG","RF-00000172-ORG","RF-00000173-ORG","RF-00000174-ORG","RF-00000175-ORG","RF-00000176-ORG","RF-00000177-ORG","RF-00000178-ORG","RF-00000179-ORG","RF-00000180-ORG","RF-00000181-ORG","RF-00000182-ORG","RF-00000183-ORG","RF-00000184-ORG","RF-00000185-ORG","RF-00000186-ORG","RF-00000187-ORG","RF-00000188-ORG","RF-00000189-ORG",
        //                                                "RF-00000190-ORG","RF-00000191-ORG","RF-00000192-ORG","RF-00000193-ORG","RF-00000194-ORG","RF-00000195-ORG","RF-00000196-ORG","RF-00000197-ORG","RF-00000198-ORG","RF-00000199-ORG","RF-00000200-ORG","RF-00000201-ORG","RF-00000202-ORG","RF-00000203-ORG","RF-00000204-ORG","RF-00000205-ORG","RF-00000206-ORG","RF-00000207-ORG","RF-00000208-ORG","RF-00000209-ORG","RF-00000210-ORG","RF-00000211-ORG","RF-00000212-ORG",
        //                                                "RF-00000213-ORG","RF-00000214-ORG","RF-00000215-ORG","RF-00000216-ORG","RF-00000217-ORG","RF-00000218-ORG","RF-00000219-ORG","RF-00000220-ORG","RF-00000221-ORG","RF-00000222-ORG","RF-00000223-ORG","RF-00000224-ORG","RF-00000225-ORG","RF-00000226-ORG","RF-00000227-ORG","RF-00000228-ORG","RF-00000229-ORG","RF-00000230-ORG","RF-00000231-ORG","RF-00000232-ORG","RF-00000233-ORG","RF-00000234-ORG","RF-00000235-ORG",
        //                                                "RF-00000236-ORG","RF-00000237-ORG","RF-00000238-ORG","RF-00000239-ORG","RF-00000240-ORG","RF-00000241-ORG","RF-00000242-ORG","RF-00000243-ORG","RF-00000244-ORG","RF-00000245-ORG","RF-00000246-ORG","RF-00000247-ORG","RF-00000248-ORG","RF-00000249-ORG","RF-00000250-ORG","RF-00000251-ORG","RF-00000252-ORG","RF-00000253-ORG","RF-00000254-ORG","RF-00000255-ORG","RF-00000256-ORG","RF-00000257-ORG","RF-00000258-ORG",
        //                                                "RF-00000259-ORG","RF-00000260-ORG","RF-00000261-ORG","RF-00000262-ORG","RF-00000263-ORG","RF-00000264-ORG","RF-00000265-ORG","RF-00000266-ORG","RF-00000267-ORG","RF-00000268-ORG","RF-00000269-ORG","RF-00000270-ORG","RF-00000271-ORG","RF-00000272-ORG","RF-00000273-ORG","RF-00000274-ORG","RF-00000275-ORG","RF-00000276-ORG","RF-00000277-ORG","RF-00000278-ORG","RF-00000279-ORG","RF-00000280-ORG","RF-00000281-ORG",
        //                                                "RF-00000282-ORG","RF-00000283-ORG","RF-00000284-ORG","RF-00000285-ORG","RF-00000286-ORG","RF-00000287-ORG","RF-00000288-ORG","RF-00000289-ORG","RF-00000290-ORG","RF-00000291-ORG","RF-00000292-ORG","RF-00000293-ORG","RF-00000294-ORG","RF-00000295-ORG","RF-00000296-ORG","RF-00000297-ORG","RF-00000298-ORG","RF-00000299-ORG","RF-00000300-ORG","RF-00000301-ORG","RF-00000302-ORG","RF-00000303-ORG","RF-00000304-ORG",
        //                                                "RF-00000305-ORG","RF-00000306-ORG","RF-00000307-ORG","RF-00000308-ORG","RF-00000309-ORG","RF-00000310-ORG","RF-00000311-ORG","RF-00000312-ORG","RF-00000313-ORG","RF-00000314-ORG","RF-00000315-ORG","RF-00000316-ORG","RF-00000317-ORG","RF-00000318-ORG","RF-00000319-ORG","RF-00000320-ORG","RF-00000321-ORG","RF-00000322-ORG","RF-00000323-ORG","RF-00000324-ORG","RF-00000325-ORG","RF-00000326-ORG","RF-00000327-ORG",
        //                                                "RF-00000460-ORG","RF-00000470-ORG","RF-00000328-ORG","RF-00000329-ORG","RF-00000330-ORG","RF-00000331-ORG","RF-00000332-ORG","RF-00000333-ORG","RF-00000334-ORG","RF-00000335-ORG","RF-00000336-ORG","RF-00000337-ORG","RF-00000338-ORG","RF-00000339-ORG","RF-00000340-ORG","RF-00000341-ORG","RF-00000342-ORG","RF-00000343-ORG","RF-00000344-ORG","RF-00000345-ORG","RF-00000346-ORG","RF-00000347-ORG"};
        //    var mercuryChilds = new List<string>() { "RF-00000169-CHE", "RF-00000170-CHE", "RF-00000250-CHE", "RF-00000251-CHE", "RF-00001725-PAR" };
        //    var allchilds = brominatedChilds.Union(dioxinerChilds).Union(mercuryChilds);

        //    var paramCode = sample.Element("paramCode")?.Value;
        //    var sampMatCode = sample.Element("sampMatCode")?.Value;
        //    var origFishAreaCode = sample.Element("origFishAreaCode")?.Value;
        //    if(allchilds.Any(x => x == paramCode))
        //    {
        //        throw new NotImplementedException();
        //    }            
        //    return outcome;
        //}        

        [Rule(Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Brominated flame retardants' (RF-00000074-ORG), or 'Dioxins and PCBs' (RF-00000114-ORG), or '3-MCPDs' (RF-00000376-ORG), then a value in the data element 'Percentage of fat in the original sample' (exprResPerc.fatPerc) should be reported(regardless of whether the result value is expressed on whole weight, fat weight or dry matter);",
            ErrorMessage = "WARNING: exprResPerc.fatPerc is missing, though recommended when reporting data on BFR, dioxins and PCBs, or 3-MCPDs;", RuleType = "warning", Deprecated = false)]
        public Outcome CHEM21(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM21",
                Passed = true,
                Description = "If the value in the data element 'Parameter code' (paramCode) has as ancestor 'Brominated flame retardants' (RF-00000074-ORG), or 'Dioxins and PCBs' (RF-00000114-ORG), or '3-MCPDs' (RF-00000376-ORG), then a value in the data element 'Percentage of fat in the original sample' (exprResPerc.fatPerc) should be reported(regardless of whether the result value is expressed on whole weight, fat weight or dry matter);",
                Error = "WARNING: exprResPerc.fatPerc is missing, though recommended when reporting data on BFR, dioxins and PCBs, or 3-MCPDs;",
                Lastupdate = "2017-01-20",
                Type = "warning",
            };
            var brominatedChilds = new List<string>() { "RF-00000074-ORG","RF-00000075-ORG", "RF-00000076-ORG", "RF-00000077-ORG", "RF-00000078-ORG", "RF-00000079-ORG",
                                            "RF-00000080-ORG", "RF-00004916-PAR", "RF-00000081-ORG", "RF-00000082-ORG", "RF-00000083-ORG", "RF-00000084-ORG", "RF-00000085-ORG",
                                            "RF-00000086-ORG", "RF-00000087-ORG", "RF-00000088-ORG", "RF-00000089-ORG", "RF-00000090-ORG", "RF-00000091-ORG", "RF-00000092-ORG",
                                            "RF-00000093-ORG", "RF-00000094-ORG", "RF-00000095-ORG", "RF-00000384-ORG", "RF-00000385-ORG", "RF-00000387-ORG", "RF-00000388-ORG",
                                            "RF-00000389-ORG", "RF-00000390-ORG", "RF-00000391-ORG", "RF-00000096-ORG", "RF-00000097-ORG", "RF-00000098-ORG", "RF-00000099-ORG",
                                            "RF-00000100-ORG", "RF-00000101-ORG", "RF-00000102-ORG", "RF-00000103-ORG", "RF-00000104-ORG", "RF-00000105-ORG", "RF-00000106-ORG",
                                            "RF-00000107-ORG", "RF-00000108-ORG", "RF-00000109-ORG", "RF-00000110-ORG", "RF-00000111-ORG", "RF-00000112-ORG", "RF-00000113-ORG",
                                            "RF-00000392-ORG", "RF-00000393-ORG", "RF-00000394-ORG", "RF-00001338-PAR",  "RF-00001339-PAR",  "RF-00004689-PAR", "RF-00004882-PAR",
                                            "RF-00004883-PAR" };
            var dioxinerChilds = new List<string>() { "RF-00000114-ORG","RF-00000003-REP","RF-00000381-ORG","RF-00000382-ORG","RF-00000383-ORG","RF-00000395-ORG","RF-00000463-ORG","RF-00000464-ORG","RF-00000465-ORG","RF-00000466-ORG","RF-00000467-ORG","RF-00000468-ORG","RF-00000471-ORG","RF-00000472-ORG","RF-00000115-ORG","RF-00000116-ORG","RF-00000117-ORG","RF-00000118-ORG","RF-00000119-ORG","RF-00000120-ORG",
                                                        "RF-00000121-ORG","RF-00000122-ORG","RF-00000123-ORG","RF-00000124-ORG","RF-00000125-ORG","RF-00000126-ORG","RF-00000127-ORG","RF-00000128-ORG","RF-00000129-ORG","RF-00000130-ORG","RF-00000131-ORG","RF-00000132-ORG","RF-00000133-ORG","RF-00000134-ORG","RF-00000135-ORG","RF-00000136-ORG","RF-00000137-ORG","RF-00000138-ORG","RF-00000139-ORG","RF-00000140-ORG","RF-00000141-ORG","RF-00000142-ORG","RF-00000143-ORG",
                                                        "RF-00000144-ORG","RF-00000145-ORG","RF-00000146-ORG","RF-00000147-ORG","RF-00000148-ORG","RF-00000149-ORG","RF-00000150-ORG","RF-00000151-ORG","RF-00000152-ORG","RF-00000153-ORG","RF-00000154-ORG","RF-00000155-ORG","RF-00000156-ORG","RF-00000157-ORG","RF-00000158-ORG","RF-00000159-ORG","RF-00000160-ORG","RF-00000161-ORG","RF-00000162-ORG","RF-00000163-ORG","RF-00000164-ORG","RF-00000165-ORG","RF-00000166-ORG",
                                                        "RF-00000167-ORG","RF-00000168-ORG","RF-00000169-ORG","RF-00000170-ORG","RF-00000171-ORG","RF-00000172-ORG","RF-00000173-ORG","RF-00000174-ORG","RF-00000175-ORG","RF-00000176-ORG","RF-00000177-ORG","RF-00000178-ORG","RF-00000179-ORG","RF-00000180-ORG","RF-00000181-ORG","RF-00000182-ORG","RF-00000183-ORG","RF-00000184-ORG","RF-00000185-ORG","RF-00000186-ORG","RF-00000187-ORG","RF-00000188-ORG","RF-00000189-ORG",
                                                        "RF-00000190-ORG","RF-00000191-ORG","RF-00000192-ORG","RF-00000193-ORG","RF-00000194-ORG","RF-00000195-ORG","RF-00000196-ORG","RF-00000197-ORG","RF-00000198-ORG","RF-00000199-ORG","RF-00000200-ORG","RF-00000201-ORG","RF-00000202-ORG","RF-00000203-ORG","RF-00000204-ORG","RF-00000205-ORG","RF-00000206-ORG","RF-00000207-ORG","RF-00000208-ORG","RF-00000209-ORG","RF-00000210-ORG","RF-00000211-ORG","RF-00000212-ORG",
                                                        "RF-00000213-ORG","RF-00000214-ORG","RF-00000215-ORG","RF-00000216-ORG","RF-00000217-ORG","RF-00000218-ORG","RF-00000219-ORG","RF-00000220-ORG","RF-00000221-ORG","RF-00000222-ORG","RF-00000223-ORG","RF-00000224-ORG","RF-00000225-ORG","RF-00000226-ORG","RF-00000227-ORG","RF-00000228-ORG","RF-00000229-ORG","RF-00000230-ORG","RF-00000231-ORG","RF-00000232-ORG","RF-00000233-ORG","RF-00000234-ORG","RF-00000235-ORG",
                                                        "RF-00000236-ORG","RF-00000237-ORG","RF-00000238-ORG","RF-00000239-ORG","RF-00000240-ORG","RF-00000241-ORG","RF-00000242-ORG","RF-00000243-ORG","RF-00000244-ORG","RF-00000245-ORG","RF-00000246-ORG","RF-00000247-ORG","RF-00000248-ORG","RF-00000249-ORG","RF-00000250-ORG","RF-00000251-ORG","RF-00000252-ORG","RF-00000253-ORG","RF-00000254-ORG","RF-00000255-ORG","RF-00000256-ORG","RF-00000257-ORG","RF-00000258-ORG",
                                                        "RF-00000259-ORG","RF-00000260-ORG","RF-00000261-ORG","RF-00000262-ORG","RF-00000263-ORG","RF-00000264-ORG","RF-00000265-ORG","RF-00000266-ORG","RF-00000267-ORG","RF-00000268-ORG","RF-00000269-ORG","RF-00000270-ORG","RF-00000271-ORG","RF-00000272-ORG","RF-00000273-ORG","RF-00000274-ORG","RF-00000275-ORG","RF-00000276-ORG","RF-00000277-ORG","RF-00000278-ORG","RF-00000279-ORG","RF-00000280-ORG","RF-00000281-ORG",
                                                        "RF-00000282-ORG","RF-00000283-ORG","RF-00000284-ORG","RF-00000285-ORG","RF-00000286-ORG","RF-00000287-ORG","RF-00000288-ORG","RF-00000289-ORG","RF-00000290-ORG","RF-00000291-ORG","RF-00000292-ORG","RF-00000293-ORG","RF-00000294-ORG","RF-00000295-ORG","RF-00000296-ORG","RF-00000297-ORG","RF-00000298-ORG","RF-00000299-ORG","RF-00000300-ORG","RF-00000301-ORG","RF-00000302-ORG","RF-00000303-ORG","RF-00000304-ORG",
                                                        "RF-00000305-ORG","RF-00000306-ORG","RF-00000307-ORG","RF-00000308-ORG","RF-00000309-ORG","RF-00000310-ORG","RF-00000311-ORG","RF-00000312-ORG","RF-00000313-ORG","RF-00000314-ORG","RF-00000315-ORG","RF-00000316-ORG","RF-00000317-ORG","RF-00000318-ORG","RF-00000319-ORG","RF-00000320-ORG","RF-00000321-ORG","RF-00000322-ORG","RF-00000323-ORG","RF-00000324-ORG","RF-00000325-ORG","RF-00000326-ORG","RF-00000327-ORG",
                                                        "RF-00000460-ORG","RF-00000470-ORG","RF-00000328-ORG","RF-00000329-ORG","RF-00000330-ORG","RF-00000331-ORG","RF-00000332-ORG","RF-00000333-ORG","RF-00000334-ORG","RF-00000335-ORG","RF-00000336-ORG","RF-00000337-ORG","RF-00000338-ORG","RF-00000339-ORG","RF-00000340-ORG","RF-00000341-ORG","RF-00000342-ORG","RF-00000343-ORG","RF-00000344-ORG","RF-00000345-ORG","RF-00000346-ORG","RF-00000347-ORG"};
            var mcpdChilds = new List<string>() { "RF-00000376-ORG", "RF-00000377-ORG", "RF-00000378-ORG", "RF-00000380-ORG" };
            var allChilds = brominatedChilds.Union(dioxinerChilds).Union(mcpdChilds);

            var paramCode = sample.Element("paramCode")?.Value;
            var exprResPerc = sample.Element("exprResPerc")?.Value;

            if (string.IsNullOrEmpty(exprResPerc))
            {
                outcome.Passed = false;
            }
            else
            {
                if(allChilds.Any(x => x == paramCode))
                {
                    outcome.Passed = false;
                    var split = exprResPerc.Split('=');
                    foreach (var item in split)
                    {
                        if(item == "fatperc")
                            outcome.Passed = true;
                    }                    
                }                
            }                       
            return outcome;
        }       

        [Rule(Description = "If the value in 'Coded description of the matrix sampled' (sampMatCode) is different from the value in 'Coded description of the analysed matrix' (anMatCode), then a value in 'Sample analysed identification code' (sampAnId) must be reported;",
            ErrorMessage = "sampAnId is missing, though it is mandatory to be reported when anMatCode is different from sampMatCode;", RuleType = "error", Deprecated = false)]
        public Outcome CHEM22(XElement sample)
        {
            var outcome = new Outcome
            {
                Name = "CHEM22",
                Passed = true,
                Description = "If the value in 'Coded description of the matrix sampled' (sampMatCode) is different from the value in 'Coded description of the analysed matrix' (anMatCode), then a value in 'Sample analysed identification code' (sampAnId) must be reported;",
                Error = "sampAnId is missing, though it is mandatory to be reported when anMatCode is different from sampMatCode;",
                Lastupdate = "2017-04-28",
                Type = "error",
            };
            var sampMatCode = sample.Element("sampMatCode")?.Value;
            var anMatCode = sample.Element("anMatCode")?.Value;
            var sampAnId = sample.Element("sampAnId")?.Value;
            if (string.IsNullOrEmpty(sampMatCode) || string.IsNullOrEmpty(anMatCode))
                outcome.Passed = false;
            else
            {
                if (sampMatCode != anMatCode)
                    outcome.Passed = !string.IsNullOrWhiteSpace(sampAnId);
            }
            return outcome;
        }
    }
}
