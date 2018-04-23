using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EfsaBusinessRuleValidator
{
    public class VmprBusinessRules
    {


        ///The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);
        public Outcome VMPR001(XElement sample)
        {
            // <checkedDataElements>;
            var progLegalRef = sample.Element("progLegalRef").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR001";
            outcome.lastupdate = "2017-12-06";
            outcome.description = "The value in the data element 'Programme Legal Reference' (progLegalRef) should be equal to 'Council Directive (EC) No 23/1996 (amended)' (N247A);";
            outcome.error = "WARNING: progLegalRef is different from Council Directive (EC) No 23/1996;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            outcome.passed = progLegalRef == "N247A";

            return outcome;
        }


        ///The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A), 'Convenient sampling' (ST40A), and 'Not specified' (STXXA);
        public Outcome VMPR002(XElement sample)
        {
            // <checkedDataElements>;
            var sampStrategy = (string)sample.Element("sampStrategy");

            var outcome = new Outcome();
            outcome.name = "VMPR002";
            outcome.lastupdate = "2017-11-27";
            outcome.description = "The value in the data element 'Sampling Strategy' (sampStrategy) must be different from 'Census' (ST50A), 'Convenient sampling' (ST40A), and 'Not specified' (STXXA);";
            outcome.error = "sampStrategy is not specified, or equal to census, or convenient sampling, though these values may not be reported;";
            outcome.type = "error";
            outcome.passed = true;


            var sampStrategys = new List<string>();
            sampStrategys.Add("ST50A");
            sampStrategys.Add("ST40A");
            sampStrategys.Add("STXXA");
            outcome.passed = sampStrategys.Contains(sampStrategy);
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
                if (sampStrategys.Contains(sampStrategy))
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


        ///The value in the data element 'Programme type' (progType) should be equal to 'Official (National and EU) programme' (K018A);
        public Outcome VMPR005(XElement sample)
        {
            // <checkedDataElements>;
            var progType = sample.Element("progType").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR005";
            outcome.lastupdate = "2017-12-06";
            outcome.description = "The value in the data element 'Programme type' (progType) should be equal to 'Official (National and EU) programme' (K018A);";
            outcome.error = "WARNING: progType is not Official (National and EU) programme;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (!string.IsNullOrEmpty(progType))
            {
                outcome.passed = progType == "K018A";
            }

            return outcome;
        }
        ///The value in the data element 'Sampling method' (sampMethod) should be equal to 'According to Dir. 2002/63/EC' (N009A), or 'According to 97/747/EC' (N010A), or 'According to Reg 1883/2006' (N015A), or 'According to 98/179/EC' (N021A), or 'Individual' (N030A), or 'According to Commission Regulation (EU) No 589/201' (N039A);
        public Outcome VMPR006(XElement sample)
        {
            // <checkedDataElements>;
            var sampMethod = (string)sample.Element("sampMethod");

            var outcome = new Outcome();
            outcome.name = "VMPR006";
            outcome.lastupdate = "2017-01-20";
            outcome.description = "The value in the data element 'Sampling method' (sampMethod) should be equal to 'According to Dir. 2002/63/EC' (N009A), or 'According to 97/747/EC' (N010A), or 'According to Reg 1883/2006' (N015A), or 'According to 98/179/EC' (N021A), or 'Individual' (N030A), or 'According to Commission Regulation (EU) No 589/201' (N039A);";
            outcome.error = "WARNING: sampMethod is not in the recommend list of codes;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(sampMethod))
            {
                var sampMethods = new List<string>();
                sampMethods.Add("N009A");
                sampMethods.Add("N010A");
                sampMethods.Add("N015A");
                sampMethods.Add("N030A");
                sampMethods.Add("N021A");
                sampMethods.Add("N039A");
                if (!sampMethods.Contains(sampMethod))
                {
                    outcome.passed = false;
                }

            }
            return outcome;
        }

        ///The value in the data element 'Sampler' (sampler) should be equal to 'Official sampling' (CX02A);
        public Outcome VMPR007(XElement sample)
        {
            // <checkedDataElements>;
            var sampler = (string)sample.Element("sampler");

            var outcome = new Outcome();
            outcome.name = "VMPR007";
            outcome.lastupdate = "2017-11-16";
            outcome.description = "The value in the data element 'Sampler' (sampler) should be equal to 'Official sampling' (CX02A);";
            outcome.error = "sampler is different from Official sampling;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(sampler))
            {
                outcome.passed = sampler == "CX02A";
            }
            return outcome;
        }


        ///Only recommended 'Sampling point' (sampPoint) codes should be reported;
        public Outcome VMPR008(XElement sample)
        {
            // <checkedDataElements>;
            var sampPoint = sample.Element("sampPoint").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR008";
            outcome.lastupdate = "2017-12-06";
            outcome.description = "Only recommended 'Sampling point' (sampPoint) codes should be reported;";
            outcome.error = "WARNING: sampPoint is not in the recommended list of codes;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (!string.IsNullOrEmpty(sampPoint))
            {
                var list = new List<string>();
                list.Add("E010A");
                list.Add("E100A");
                list.Add("E101A");
                list.Add("E112A");
                list.Add("E150A");
                list.Add("E152A");
                list.Add("E170A");
                list.Add("E180A");
                list.Add("E300A");
                list.Add("E311A");
                list.Add("E310A");
                list.Add("E301A");
                list.Add("E320A");
                list.Add("E510A");
                list.Add("E520A");
                list.Add("E600A");
                list.Add("E700A");

                outcome.passed = list.Contains(sampPoint);
            }

            return outcome;
        }

        public Outcome VMPR009(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatText = (string)sample.Element("sampMatText");



            var outcome = new Outcome();
            outcome.name = "VMPR009";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "A value in the data element 'Text description of the matrix of the sample taken' (sampMatText) should be reported;";
            outcome.error = "WARNING: sampMatText is missing, though recommended;";
            outcome.type = "warning";
            outcome.passed = true;
            outcome.values.Add(Tuple.Create<string, string>(nameof(sampMatText), sampMatText));
            //Logik (ignore null: no);

            outcome.passed = !String.IsNullOrEmpty(sampMatText);
            return outcome;
        }

        ///If the value in 'Parameter code' (paramCode) doesn't belong to the group B3c (chemical elements used in vmpr), then the value in the data element 'Type of parameter' (paramType) should be equal to 'Full legal marker residue definition analysed' (P005A), or 'Sum based on a subset' (P004A), or 'Part of a sum' (P002A);
        public Outcome VMPR010(XElement sample)
        {
            // <checkedDataElements>;
            var paramType = (string)sample.Element("paramType");
            var paramCode = (string)sample.Element("paramCode");

            var outcome = new Outcome();
            outcome.name = "VMPR010";
            outcome.lastupdate = "2018-01-03";
            outcome.description = "If the value in 'Parameter code' (paramCode) doesn't belong to the group B3c (chemical elements used in vmpr), then the value in the data element 'Type of parameter' (paramType) should be equal to 'Full legal marker residue definition analysed' (P005A), or 'Sum based on a subset' (P004A), or 'Part of a sum' (P002A);";
            outcome.error = "WARNING: paramType is not in the recommended list of codes;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);

            var b3c = new List<string> { "RF-00000124-CHE", "RF-00000128-CHE", "RF-00000150-CHE", "RF-00000170-CHE", "RF-00000191-CHE", "RF-00000126-CHE", "RF-00000203-CHE", "RF-00000168-CHE", "RF-00000067-CHE", "RF-00000185-CHE", "RF-00000176-CHE", "RF-00000179-CHE", "RF-00000182-CHE", "RF-00000152-CHE", "RF-00000161-CHE", "RF-00000174-CHE", "RF-00000193-CHE", "RF-00000205-CHE", "RF-00000060-CHE", "RF-00000135-CHE", "RF-00000167-CHE", "RF-00000250-CHE", "RF-00000143-CHE", "RF-00000189-CHE", "RF-00000007-CHE", "RF-00000147-CHE", "RF-00000186-CHE", "RF-00000164-CHE", "RF-00000184-CHE", "RF-00000053-CHE", "RF-00000251-CHE", };

            if (!b3c.Any(b => b == paramCode))
            {
                var paramTypes = new List<string>();
                paramTypes.Add("P005A");
                paramTypes.Add("P004A");
                paramTypes.Add("P002A");
                outcome.passed = paramTypes.Contains(paramType);

            }
            return outcome;
        }

        ///Only recommended values of 'Parameter code' (paramCode) should be combined with 'Type of parameter' (paramType) equal to 'Part of a sum' (P002A);
        public Outcome VMPR011(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string)sample.Element("paramCode");
            var paramType = (string)sample.Element("paramType");

            var outcome = new Outcome();
            outcome.name = "VMPR011";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "Only recommended values of 'Parameter code' (paramCode) should be combined with 'Type of parameter' (paramType) equal to 'Part of a sum' (P002A);";
            outcome.error = "WARNING: paramType is not part of a sum, though paramCode is a code for which the type should be part of sum;";
            outcome.type = "warning";
            outcome.values.Add(Tuple.Create<string, string>(nameof(paramCode), paramCode));
            outcome.values.Add(Tuple.Create<string, string>(nameof(paramType), paramType));
            outcome.passed = true;

            //Logik (ignore null: yes);

            if (!String.IsNullOrEmpty(paramCode))
            {
                var paramCodes = new List<string>();
                paramCodes.Add("RF-0108-003-PPP");
                paramCodes.Add("RF-0108-002-PPP");
                paramCodes.Add("RF-00000646-VET");
                paramCodes.Add("RF-00000648-VET");
                paramCodes.Add("RF-00000638-VET");
                paramCodes.Add("RF-00000601-VET");
                paramCodes.Add("RF-00000602-VET");
                paramCodes.Add("RF-00000603-VET");
                paramCodes.Add("RF-00000604-VET");
                paramCodes.Add("RF-00000605-VET");
                paramCodes.Add("RF-00000606-VET");
                paramCodes.Add("RF-00000607-VET");
                paramCodes.Add("RF-00000608-VET");
                paramCodes.Add("RF-00000610-VET");
                paramCodes.Add("RF-00000624-VET");
                paramCodes.Add("RF-00000612-VET");
                paramCodes.Add("RF-00000600-VET");
                paramCodes.Add("RF-00000615-VET");
                paramCodes.Add("RF-00000617-VET");
                paramCodes.Add("RF-00000649-VET");
                paramCodes.Add("RF-00000621-VET");
                paramCodes.Add("RF-00000622-VET");
                paramCodes.Add("RF-0900-001-PPP");
                paramCodes.Add("RF-00000679-VET");
                paramCodes.Add("RF-00000779-VET");
                paramCodes.Add("RF-00000781-VET");
                paramCodes.Add("RF-00000647-VET");
                paramCodes.Add("RF-00000614-VET");
                paramCodes.Add("RF-00000611-VET");
                paramCodes.Add("RF-00000682-VET");
                paramCodes.Add("RF-00000681-VET");
                paramCodes.Add("RF-00000045-VET");
                paramCodes.Add("RF-00000041-VET");
                paramCodes.Add("RF-00000040-VET");
                paramCodes.Add("RF-00000629-VET");
                paramCodes.Add("RF-00002895-PAR");
                paramCodes.Add("RF-00000178-VET");
                paramCodes.Add("RF-00000166-VET");
                paramCodes.Add("RF-00000575-VET");
                paramCodes.Add("RF-00000579-VET");
                paramCodes.Add("RF-00000551-VET");
                paramCodes.Add("RF-00000695-VET");
                paramCodes.Add("RF-00000594-VET");
                paramCodes.Add("RF-00000590-VET");
                paramCodes.Add("RF-00000007-VET");
                paramCodes.Add("RF-00002865-PAR");
                paramCodes.Add("RF-00002908-PAR");
                paramCodes.Add("RF-00002909-PAR");
                paramCodes.Add("RF-00002910-PAR");
                paramCodes.Add("RF-00002911-PAR");
                paramCodes.Add("RF-00002919-PAR");
                paramCodes.Add("RF-00001727-PAR");
                paramCodes.Add("RF-00004638-PAR");
                paramCodes.Add("RF-00004639-PAR");
                paramCodes.Add("RF-00002915-PAR");
                paramCodes.Add("RF-00000049-VET");
                paramCodes.Add("RF-00000543-VET");
                paramCodes.Add("RF-00000586-VET");
                paramCodes.Add("RF-00000642-VET");
                paramCodes.Add("RF-00000532-VET");
                paramCodes.Add("RF-00000670-VET");
                paramCodes.Add("RF-00000588-VET");
                paramCodes.Add("RF-00000043-VET");
                paramCodes.Add("RF-00000037-VET");
                paramCodes.Add("RF-00000038-VET");
                paramCodes.Add("RF-00002917-PAR");
                paramCodes.Add("RF-0416-001-PPP");
                paramCodes.Add("RF-0926-001-PPP");
                paramCodes.Add("RF-00002955-PAR");
                paramCodes.Add("RF-00000571-VET");
                paramCodes.Add("RF-00002888-PAR");
                paramCodes.Add("RF-00002889-PAR");

                if (paramCodes.Contains(paramCode))
                {
                    outcome.passed = paramType == "P002A";
                }

            }
            return outcome;
        }
        ///The value in the data element 'Analytical Method Type' (anMethType) must be equal to 'Screening' (AT06A), or 'Confirmation' (AT08A);
        public Outcome VMPR012(XElement sample)
        {
            // <checkedDataElements>;
            var anMethType = (string)sample.Element("anMethType");

            var outcome = new Outcome();
            outcome.name = "VMPR012";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "The value in the data element 'Analytical Method Type' (anMethType) must be equal to 'Screening' (AT06A), or 'Confirmation' (AT08A);";
            outcome.error = "anMethType is not screening or confirmation;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(anMethType))
            {
                var anMethTypes = new List<string>();
                anMethTypes.Add("AT06A");
                anMethTypes.Add("AT08A");
                outcome.passed = anMethTypes.Contains(anMethType);
            }
            return outcome;
        }
        ///If the value in the data element 'Evaluation of the result' (evalCode) is 'Detected' (J041A) or 'Above maximum permissible quantities' (J003A), then the value in the data element 'Analytical Method Type' (anMethType) must be equal to 'Confirmation' (AT08A);
        public Outcome VMPR013(XElement sample)
        {
            // <checkedDataElements>;
            var anMethType = (string)sample.Element("anMethType");
            var evalCode = (string)sample.Element("evalCode");

            var outcome = new Outcome();
            outcome.name = "VMPR013";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in the data element 'Evaluation of the result' (evalCode) is 'Detected' (J041A) or 'Above maximum permissible quantities' (J003A), then the value in the data element 'Analytical Method Type' (anMethType) must be equal to 'Confirmation' (AT08A);";
            outcome.error = "anMethType is not confirmation, though evalCode is detected or above maximum permissible quantities;";
            outcome.type = "error";
            outcome.values.Add(Tuple.Create<string, string>(nameof(anMethType), anMethType));
            outcome.values.Add(Tuple.Create<string, string>(nameof(evalCode), evalCode));
            outcome.passed = true;

            //Logik (ignore null: yes);

            if (!String.IsNullOrEmpty(anMethType))
            {
                var evalCodes = new List<string>();
                evalCodes.Add("J041A");
                evalCodes.Add("J003A");
                ///TESTING

                if (evalCodes.Contains(evalCode))
                {
                    var anMethTypes = new List<string>();
                    anMethTypes.Add("AT08A");
                    outcome.passed = anMethTypes.Contains(anMethType);
                }
            }
            return outcome;
        }
        ///The value in the data element 'Accreditation procedure for the analytical method' (accredProc) should be equal to 'Accredited according to ISO/IEC17025' (V001A), or 'Accredited and validated according to Com.Dec. 2002/657/EC' (V007A), or 'Validated according to Commission Decision 2002/657/EC, but not accredited under ISO/IEC17025' (V008A);
        public Outcome VMPR014(XElement sample)
        {
            // <checkedDataElements>;
            var accredProc = sample.Element("accredProc").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR014";
            outcome.lastupdate = "2017-12-06";
            outcome.description = "The value in the data element 'Accreditation procedure for the analytical method' (accredProc) should be equal to 'Accredited according to ISO/IEC17025' (V001A), or 'Accredited and validated according to Com.Dec. 2002/657/EC' (V007A), or 'Validated according to Commission Decision 2002/657/EC, but not accredited under ISO/IEC17025' (V008A);";
            outcome.error = "WARNING: accredProc is not one of the recommended procedures;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik
            if (!String.IsNullOrEmpty(accredProc))
            {
                var list = new List<String>();
                list.Add("V001A");
                list.Add("V007A");
                list.Add("V008A");
                list.Add("V999A");
                outcome.passed = list.Contains(accredProc);
            }
            return outcome;
        }

        ///If a value is reported in 'Result LOD' (resLOD), or 'Result LOQ' (resLOQ), or 'Result value' (resVal), or 'CC alpha' (CCalpha), or 'CC beta' (CCbeta), and the value in 'Parameter code' (paramCode) is not in the groups B2c, B3a, B3B, B3c or B3f  then the value in the data element 'Result unit' (resUnit) must be equal to 'Microgram/kilogram' (G050A) or 'Microgram/litre' (G051A) to ensure that values are comparable;
        public Outcome VMPR015(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resLOQ = (string)sample.Element("resLOQ");
            var resVal = (string)sample.Element("resVal");
            var CCalpha = (string)sample.Element("CCalpha");
            var CCbeta = (string)sample.Element("CCbeta");
            var paramCode = (string)sample.Element("paramCode");
            var resUnit = (string)sample.Element("resUnit");

            var outcome = new Outcome();
            outcome.name = "VMPR015";
            outcome.lastupdate = "2017-11-27";
            outcome.description = "If a value is reported in 'Result LOD' (resLOD), or 'Result LOQ' (resLOQ), or 'Result value' (resVal), or 'CC alpha' (CCalpha), or 'CC beta' (CCbeta), and the value in 'Parameter code' (paramCode) is not in the groups B2c, B3a, B3B, B3c or B3f  then the value in the data element 'Result unit' (resUnit) must be equal to 'Microgram/kilogram' (G050A) or 'Microgram/litre' (G051A) to ensure that values are comparable;";
            outcome.error = "resUnit is not microgram/kilogram or microgram/litre, though a numerical value is reported;";
            outcome.type = "error";
            outcome.passed = true;


            //Logik (ignore null: no);
            if (String.IsNullOrEmpty(resLOD) && String.IsNullOrEmpty(resLOQ) && String.IsNullOrEmpty(resVal) && String.IsNullOrEmpty(CCalpha) && String.IsNullOrEmpty(CCbeta))
            {
                //Nothing reported which surely is an error
            }
            else
            {
                var allGroups = new List<string> { "RF-0605-001-PPP", "RF-0684-001-PPP", "RF-0061-001-PPP", "RF-0084-001-PPP", "RF-0156-001-PPP", "RF-0237-001-PPP", "RF-0246-001-PPP", "RF-0311-001-PPP", "RF-0406-001-PPP", "RF-0128-003-PPP", "RF-0383-003-PPP", "RF-00000460-ORG", "RF-00000337-ORG", "RF-00000339-ORG", "RF-00000342-ORG", "RF-00000343-ORG", "RF-00000182-ORG", "RF-00000201-ORG", "RF-00000243-ORG", "RF-00000251-ORG", "RF-00000328-ORG", "RF-00002896-PAR", "RF-00000143-ORG", "RF-00000169-ORG", "RF-00000211-ORG", "RF-00000212-ORG", "RF-00000327-ORG", "RF-0453-001-PPP", "RF-0060-001-PPP", "RF-0329-001-PPP", "RF-0410-001-PPP", "RF-0010-003-PPP", "RF-0130-002-PPP", "RF-00000470-ORG", "RF-0075-001-PPP", "RF-0653-001-PPP", "RF-0819-001-PPP", "RF-0052-001-PPP", "RF-0082-001-PPP", "RF-0093-001-PPP", "RF-0130-003-PPP", "RF-00000338-ORG", "RF-00000119-ORG", "RF-00000124-ORG", "RF-00000126-ORG", "RF-00000128-ORG", "RF-00000158-ORG", "RF-00000253-ORG", "RF-00000072-TOX", "RF-0001-001-PPP", "RF-0240-004-PPP", "RF-0349-002-PPP", "RF-0515-001-PPP", "RF-0603-001-PPP", "RF-0736-001-PPP", "RF-0755-001-PPP", "RF-0878-001-PPP", "RF-0923-001-PPP", "RF-0997-001-PPP", "RF-0999-001-PPP", "RF-0090-001-PPP", "RF-0295-001-PPP", "RF-0364-001-PPP", "RF-0187-002-PPP", "RF-0383-002-PPP", "RF-00000330-ORG", "RF-00000333-ORG", "RF-00000334-ORG", "RF-00000346-ORG", "RF-00000117-ORG", "RF-00000226-ORG", "RF-00000307-ORG", "RF-0119-001-PPP", "RF-0155-001-PPP", "RF-00000020-ORG", "RF-00000341-ORG", "RF-00000345-ORG", "RF-00000134-ORG", "RF-00000147-ORG", "RF-00000255-ORG", "RF-0559-001-PPP", "RF-0889-001-PPP", "RF-0998-001-PPP", "RF-1008-001-PPP", "RF-0008-001-PPP", "RF-0214-001-PPP", "RF-0221-001-PPP", "RF-0355-001-PPP", "RF-00002898-PAR", "RF-00000030-ORG", "RF-0837-001-PPP", "RF-0045-001-PPP", "RF-0078-001-PPP", "RF-0174-001-PPP", "RF-0255-001-PPP", "RF-0350-001-PPP", "RF-00000336-ORG", "RF-00000347-ORG", "RF-00000121-ORG", "RF-00000127-ORG", "RF-00000173-ORG", "RF-00000178-ORG", "RF-00000181-ORG", "RF-00000208-ORG", "RF-00000472-ORG", "RF-00000006-PAR", "RF-00002897-PAR", "RF-00000186-ORG", "RF-00000215-ORG", "RF-00000219-ORG", "RF-00000278-ORG", "RF-00000323-ORG", "RF-0557-001-PPP", "RF-0600-001-PPP", "RF-0838-001-PPP", "RF-0994-001-PPP", "RF-0995-001-PPP", "RF-0073-001-PPP", "RF-0129-001-PPP", "RF-0264-001-PPP", "RF-0450-003-PPP", "RF-0236-001-PPP", "RF-0059-003-PPP", "RF-0548-001-PPP", "RF-0556-001-PPP", "RF-0797-001-PPP", "RF-0996-001-PPP", "RF-00000331-ORG", "RF-00000332-ORG", "RF-00000335-ORG", "RF-00000344-ORG", "RF-00000118-ORG", "RF-00000120-ORG", "RF-00000122-ORG", "RF-00000123-ORG", "RF-00000129-ORG", "RF-00000131-ORG", "RF-00000184-ORG", "RF-0059-001-PPP", "RF-0021-001-PPP", "RF-00000471-ORG", "RF-0633-001-PPP", "RF-0801-001-PPP", "RF-0260-001-PPP", "RF-0428-003-PPP", "RF-00000104-ORG", "RF-00000005-ORG", "RF-00002920-PAR", "RF-00000052-ORG", "RF-00000006-ORG", "RF-00000079-ORG", "RF-00000080-ORG", "RF-00000075-ORG", "RF-00000362-ORG", "RF-0952-001-PPP", "RF-00002890-PAR", "RF-00002891-PAR", "RF-00002930-PAR", "RF-00002957-PAR", "RF-0133-001-PPP", "RF-0281-002-PPP", "RF-00000099-ORG", "RF-00000100-ORG", "RF-00000046-ORG", "RF-00000360-ORG", "RF-0194-002-PPP", "RF-00002864-PAR", "RF-00002866-PAR", "RF-00002933-PAR", "RF-00002949-PAR", "RF-0810-001-PPP", "RF-0029-001-PPP", "RF-0241-001-PPP", "RF-00000098-ORG", "RF-00000102-ORG", "RF-00000050-ORG", "RF-00000004-ORG", "RF-00000078-ORG", "RF-00000354-ORG", "RF-00000358-ORG", "RF-0422-001-PPP", "RF-00002863-PAR", "RF-00002931-PAR", "RF-00000097-ORG", "RF-00000047-ORG", "RF-00000009-ORG", "RF-0616-001-PPP", "RF-0035-001-PPP", "RF-0113-001-PPP", "RF-00000401-ORG", "RF-00002929-PAR", "RF-0014-001-PPP", "RF-0539-001-PPP", "RF-0101-001-PPP", "RF-0390-001-PPP", "RF-00000005-RAD", "RF-00000101-ORG", "RF-00000003-ORG", "RF-00000008-ORG", "RF-00000357-ORG", "RF-00000359-ORG", "RF-00000361-ORG", "RF-0049-001-PPP", "RF-00000461-ORG", "RF-00002860-PAR", "RF-00002892-PAR", "RF-00002932-PAR", "RF-00000004-RAD", "RF-00000103-ORG", "RF-00000045-ORG", "RF-00000056-ORG", "RF-00000007-ORG", "RF-0250-001-PPP", "RF-0285-001-PPP", "RF-0358-001-PPP", "RF-0415-001-PPP", "RF-0417-001-PPP", "RF-0418-001-PPP", "RF-0439-001-PPP", "RF-00000049-ORG", "RF-00000061-ORG", "RF-0048-001-PPP", "RF-0165-001-PPP", "RF-0218-001-PPP", "RF-0403-001-PPP", "RF-0425-002-PPP", "RF-00000124-CHE", "RF-00000128-CHE", "RF-00000150-CHE", "RF-00000170-CHE", "RF-00000191-CHE", "RF-00000126-CHE", "RF-00000203-CHE", "RF-00000168-CHE", "RF-00000067-CHE", "RF-00000185-CHE", "RF-00000176-CHE", "RF-00000179-CHE", "RF-00000182-CHE", "RF-00000152-CHE", "RF-00000161-CHE", "RF-00000174-CHE", "RF-00000193-CHE", "RF-00000205-CHE", "RF-00000060-CHE", "RF-00000135-CHE", "RF-00000167-CHE", "RF-00000250-CHE", "RF-00000143-CHE", "RF-00000189-CHE", "RF-00000007-CHE", "RF-00000147-CHE", "RF-00000186-CHE", "RF-00000164-CHE", "RF-00000184-CHE", "RF-00000053-CHE", "RF-00000251-CHE", "RF-0517-001-PPP", "RF-0724-001-PPP", "RF-0828-001-PPP", "RF-0844-001-PPP", "RF-0911-001-PPP", "RF-0032-001-PPP", "RF-0127-001-PPP", "RF-0146-001-PPP", "RF-0224-001-PPP", "RF-0290-001-PPP", "RF-0139-003-PPP", "RF-0328-002-PPP", "RF-0336-002-PPP", "RF-0178-002-PPP", "RF-0187-007-PPP", "RF-00002952-PAR", "RF-0528-001-PPP", "RF-0851-001-PPP", "RF-0877-001-PPP", "RF-0903-001-PPP", "RF-0985-001-PPP", "RF-0051-001-PPP", "RF-0302-001-PPP", "RF-0337-001-PPP", "RF-0339-001-PPP", "RF-0351-001-PPP", "RF-0149-003-PPP", "RF-0640-001-PPP", "RF-0756-001-PPP", "RF-0846-001-PPP", "RF-0920-001-PPP", "RF-0957-001-PPP", "RF-0305-001-PPP", "RF-0342-001-PPP", "RF-0373-001-PPP", "RF-0149-002-PPP", "RF-0266-002-PPP", "RF-0435-001-PPP", "RF-0599-001-PPP", "RF-0612-001-PPP", "RF-0647-001-PPP", "RF-0670-001-PPP", "RF-0936-001-PPP", "RF-0937-001-PPP", "RF-0946-001-PPP", "RF-0012-001-PPP", "RF-0079-001-PPP", "RF-0088-001-PPP", "RF-0272-001-PPP", "RF-0331-001-PPP", "RF-0348-001-PPP", "RF-0139-002-PPP", "RF-0187-004-PPP", "RF-0432-001-PPP", "RF-0535-001-PPP", "RF-0560-001-PPP", "RF-0575-001-PPP", "RF-0668-001-PPP", "RF-0768-001-PPP", "RF-0161-001-PPP", "RF-0380-001-PPP", "RF-0187-005-PPP", "RF-0338-002-PPP", "RF-0561-001-PPP", "RF-0597-001-PPP", "RF-0737-001-PPP", "RF-0781-001-PPP", "RF-0800-001-PPP", "RF-0866-001-PPP", "RF-0905-001-PPP", "RF-0685-002-PPP", "RF-0160-001-PPP", "RF-0327-001-PPP", "RF-0419-001-PPP", "RF-0187-006-PPP", "RF-0266-003-PPP", "RF-0328-003-PPP", "RF-0412-002-PPP", "RF-00002887-PAR", "RF-0484-001-PPP", "RF-0554-001-PPP", "RF-0571-001-PPP", "RF-0574-001-PPP", "RF-0654-001-PPP", "RF-0759-001-PPP", "RF-0087-001-PPP", "RF-0123-001-PPP", "RF-0125-001-PPP", "RF-0164-001-PPP", "RF-0289-001-PPP", "RF-0149-004-PPP", "RF-0173-004-PPP", "RF-0187-003-PPP", "RF-0323-004-PPP", "RF-0336-005-PPP", "RF-0424-001-PPP", "RF-0442-001-PPP", "RF-0577-001-PPP", "RF-0578-001-PPP", "RF-0821-001-PPP", "RF-0869-001-PPP", "RF-0033-001-PPP", "RF-0594-002-PPP", "RF-0180-001-PPP", "RF-0288-001-PPP", "RF-0336-003-PPP", "RF-0525-001-PPP", "RF-0587-001-PPP", "RF-0677-001-PPP", "RF-0705-001-PPP", "RF-0721-001-PPP", "RF-0040-001-PPP", "RF-0068-001-PPP", "RF-0261-001-PPP", "RF-0374-001-PPP", "RF-0402-001-PPP", "RF-0065-003-PPP", "RF-0690-006-PPP", "RF-1025-001-PPP", "RF-0522-001-PPP", "RF-0046-001-PPP", "RF-0132-001-PPP", "RF-0201-001-PPP", "RF-0251-001-PPP", "RF-0256-001-PPP", "RF-0366-001-PPP", "RF-0660-001-PPP", "RF-0762-001-PPP", "RF-0824-001-PPP", "RF-0028-001-PPP", "RF-0182-001-PPP", "RF-0228-001-PPP", "RF-0335-001-PPP", "RF-0408-001-PPP", "RF-0041-002-PPP", "RF-0347-003-PPP", "RF-00000161-VET", "RF-00003017-PAR", "RF-0690-002-PPP", "RF-0464-001-PPP", "RF-0489-001-PPP", "RF-0860-001-PPP", "RF-0320-001-PPP", "RF-0361-001-PPP", "RF-0291-002-PPP", "RF-0043-003-PPP", "RF-0792-001-PPP", "RF-0304-001-PPP", "RF-0334-001-PPP", "RF-0347-002-PPP", "RF-0354-002-PPP", "RF-0636-001-PPP", "RF-0835-001-PPP", "RF-0922-001-PPP", "RF-0933-001-PPP", "RF-0975-001-PPP", "RF-0018-001-PPP", "RF-0183-001-PPP", "RF-0385-001-PPP", "RF-0020-003-PPP", "RF-0065-002-PPP", "RF-0112-001-PPP", "RF-0293-003-PPP", "RF-0962-002-PPP", "RF-00000132-VET", "RF-00000067-ORG", "RF-0524-001-PPP", "RF-0586-001-PPP", "RF-00000141-VET", "RF-0062-001-PPP", "RF-0120-001-PPP", "RF-0291-004-PPP", "RF-0842-002-PPP", "RF-0430-001-PPP", "RF-0469-001-PPP", "RF-0584-001-PPP", "RF-0662-001-PPP", "RF-0663-001-PPP", "RF-0842-001-PPP", "RF-0122-001-PPP", "RF-0420-001-PPP", "RF-0020-002-PPP", "RF-0020-004-PPP", "RF-0108-001-PPP", "RF-0112-004-PPP", "RF-0291-003-PPP", "RF-0293-002-PPP", "RF-0690-003-PPP", };

                if (!allGroups.Any(g => g == paramCode))
                {
                    var resUnits = new List<string>();
                    resUnits.Add("G050A");
                    resUnits.Add("G051A");
                    outcome.passed = resUnits.Contains(resUnit);
                }
            }
            return outcome;
        }

        ///If a value is reported in 'Result LOD' (resLOD), or 'Result LOQ' (resLOQ), or 'Result value' (resVal), or 'CC alpha' (CCalpha), or 'CC beta' (CCbeta), and the value in 'Parameter code' (paramCode) is not in the groups B2c, B3a, B3B, B3c or B3f  then the value in the data element 'Result unit' (resUnit) must be equal to 'Microgram/kilogram' (G050A) or 'Microgram/litre' (G051A) to ensure that values are comparable;
        public Outcome VMPR015a(XElement sample)
        {
            // <checkedDataElements>;
            var resLOD = (string)sample.Element("resLOD");
            var resLOQ = (string)sample.Element("resLOQ");
            var resVal = (string)sample.Element("resVal");
            var CCalpha = (string)sample.Element("CCalpha");
            var CCbeta = (string)sample.Element("CCbeta");
            var paramCode = (string)sample.Element("paramCode");
            var resUnit = (string)sample.Element("resUnit");

            var outcome = new Outcome();
            outcome.name = "VMPR015a";
            outcome.lastupdate = "2017-11-27";
            outcome.description = "If a value is reported in 'Result LOD' (resLOD), or 'Result LOQ' (resLOQ), or 'Result value' (resVal), or 'CC alpha' (CCalpha), or 'CC beta' (CCbeta), and the value in 'Parameter code' (paramCode) and belongs to the groups B2c, B3a, B3B, B3c or B3f, then the value in the data element 'Result unit' (resUnit) can only be one of the unit used to report chemical occurence data;";
            outcome.error = "resUnit is not in the list allowed when reporting chemical occurence data;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (String.IsNullOrEmpty(resLOD) && String.IsNullOrEmpty(resLOQ) && String.IsNullOrEmpty(resVal) && String.IsNullOrEmpty(CCalpha) && String.IsNullOrEmpty(CCbeta))
            {
                //Nothing reported which surely is an error
            }
            else
            {
                var allGroups = new List<string> { "RF-0605-001-PPP", "RF-0684-001-PPP", "RF-0061-001-PPP", "RF-0084-001-PPP", "RF-0156-001-PPP", "RF-0237-001-PPP", "RF-0246-001-PPP", "RF-0311-001-PPP", "RF-0406-001-PPP", "RF-0128-003-PPP", "RF-0383-003-PPP", "RF-00000460-ORG", "RF-00000337-ORG", "RF-00000339-ORG", "RF-00000342-ORG", "RF-00000343-ORG", "RF-00000182-ORG", "RF-00000201-ORG", "RF-00000243-ORG", "RF-00000251-ORG", "RF-00000328-ORG", "RF-00002896-PAR", "RF-00000143-ORG", "RF-00000169-ORG", "RF-00000211-ORG", "RF-00000212-ORG", "RF-00000327-ORG", "RF-0453-001-PPP", "RF-0060-001-PPP", "RF-0329-001-PPP", "RF-0410-001-PPP", "RF-0010-003-PPP", "RF-0130-002-PPP", "RF-00000470-ORG", "RF-0075-001-PPP", "RF-0653-001-PPP", "RF-0819-001-PPP", "RF-0052-001-PPP", "RF-0082-001-PPP", "RF-0093-001-PPP", "RF-0130-003-PPP", "RF-00000338-ORG", "RF-00000119-ORG", "RF-00000124-ORG", "RF-00000126-ORG", "RF-00000128-ORG", "RF-00000158-ORG", "RF-00000253-ORG", "RF-00000072-TOX", "RF-0001-001-PPP", "RF-0240-004-PPP", "RF-0349-002-PPP", "RF-0515-001-PPP", "RF-0603-001-PPP", "RF-0736-001-PPP", "RF-0755-001-PPP", "RF-0878-001-PPP", "RF-0923-001-PPP", "RF-0997-001-PPP", "RF-0999-001-PPP", "RF-0090-001-PPP", "RF-0295-001-PPP", "RF-0364-001-PPP", "RF-0187-002-PPP", "RF-0383-002-PPP", "RF-00000330-ORG", "RF-00000333-ORG", "RF-00000334-ORG", "RF-00000346-ORG", "RF-00000117-ORG", "RF-00000226-ORG", "RF-00000307-ORG", "RF-0119-001-PPP", "RF-0155-001-PPP", "RF-00000020-ORG", "RF-00000341-ORG", "RF-00000345-ORG", "RF-00000134-ORG", "RF-00000147-ORG", "RF-00000255-ORG", "RF-0559-001-PPP", "RF-0889-001-PPP", "RF-0998-001-PPP", "RF-1008-001-PPP", "RF-0008-001-PPP", "RF-0214-001-PPP", "RF-0221-001-PPP", "RF-0355-001-PPP", "RF-00002898-PAR", "RF-00000030-ORG", "RF-0837-001-PPP", "RF-0045-001-PPP", "RF-0078-001-PPP", "RF-0174-001-PPP", "RF-0255-001-PPP", "RF-0350-001-PPP", "RF-00000336-ORG", "RF-00000347-ORG", "RF-00000121-ORG", "RF-00000127-ORG", "RF-00000173-ORG", "RF-00000178-ORG", "RF-00000181-ORG", "RF-00000208-ORG", "RF-00000472-ORG", "RF-00000006-PAR", "RF-00002897-PAR", "RF-00000186-ORG", "RF-00000215-ORG", "RF-00000219-ORG", "RF-00000278-ORG", "RF-00000323-ORG", "RF-0557-001-PPP", "RF-0600-001-PPP", "RF-0838-001-PPP", "RF-0994-001-PPP", "RF-0995-001-PPP", "RF-0073-001-PPP", "RF-0129-001-PPP", "RF-0264-001-PPP", "RF-0450-003-PPP", "RF-0236-001-PPP", "RF-0059-003-PPP", "RF-0548-001-PPP", "RF-0556-001-PPP", "RF-0797-001-PPP", "RF-0996-001-PPP", "RF-00000331-ORG", "RF-00000332-ORG", "RF-00000335-ORG", "RF-00000344-ORG", "RF-00000118-ORG", "RF-00000120-ORG", "RF-00000122-ORG", "RF-00000123-ORG", "RF-00000129-ORG", "RF-00000131-ORG", "RF-00000184-ORG", "RF-0059-001-PPP", "RF-0021-001-PPP", "RF-00000471-ORG", "RF-0633-001-PPP", "RF-0801-001-PPP", "RF-0260-001-PPP", "RF-0428-003-PPP", "RF-00000104-ORG", "RF-00000005-ORG", "RF-00002920-PAR", "RF-00000052-ORG", "RF-00000006-ORG", "RF-00000079-ORG", "RF-00000080-ORG", "RF-00000075-ORG", "RF-00000362-ORG", "RF-0952-001-PPP", "RF-00002890-PAR", "RF-00002891-PAR", "RF-00002930-PAR", "RF-00002957-PAR", "RF-0133-001-PPP", "RF-0281-002-PPP", "RF-00000099-ORG", "RF-00000100-ORG", "RF-00000046-ORG", "RF-00000360-ORG", "RF-0194-002-PPP", "RF-00002864-PAR", "RF-00002866-PAR", "RF-00002933-PAR", "RF-00002949-PAR", "RF-0810-001-PPP", "RF-0029-001-PPP", "RF-0241-001-PPP", "RF-00000098-ORG", "RF-00000102-ORG", "RF-00000050-ORG", "RF-00000004-ORG", "RF-00000078-ORG", "RF-00000354-ORG", "RF-00000358-ORG", "RF-0422-001-PPP", "RF-00002863-PAR", "RF-00002931-PAR", "RF-00000097-ORG", "RF-00000047-ORG", "RF-00000009-ORG", "RF-0616-001-PPP", "RF-0035-001-PPP", "RF-0113-001-PPP", "RF-00000401-ORG", "RF-00002929-PAR", "RF-0014-001-PPP", "RF-0539-001-PPP", "RF-0101-001-PPP", "RF-0390-001-PPP", "RF-00000005-RAD", "RF-00000101-ORG", "RF-00000003-ORG", "RF-00000008-ORG", "RF-00000357-ORG", "RF-00000359-ORG", "RF-00000361-ORG", "RF-0049-001-PPP", "RF-00000461-ORG", "RF-00002860-PAR", "RF-00002892-PAR", "RF-00002932-PAR", "RF-00000004-RAD", "RF-00000103-ORG", "RF-00000045-ORG", "RF-00000056-ORG", "RF-00000007-ORG", "RF-0250-001-PPP", "RF-0285-001-PPP", "RF-0358-001-PPP", "RF-0415-001-PPP", "RF-0417-001-PPP", "RF-0418-001-PPP", "RF-0439-001-PPP", "RF-00000049-ORG", "RF-00000061-ORG", "RF-0048-001-PPP", "RF-0165-001-PPP", "RF-0218-001-PPP", "RF-0403-001-PPP", "RF-0425-002-PPP", "RF-00000124-CHE", "RF-00000128-CHE", "RF-00000150-CHE", "RF-00000170-CHE", "RF-00000191-CHE", "RF-00000126-CHE", "RF-00000203-CHE", "RF-00000168-CHE", "RF-00000067-CHE", "RF-00000185-CHE", "RF-00000176-CHE", "RF-00000179-CHE", "RF-00000182-CHE", "RF-00000152-CHE", "RF-00000161-CHE", "RF-00000174-CHE", "RF-00000193-CHE", "RF-00000205-CHE", "RF-00000060-CHE", "RF-00000135-CHE", "RF-00000167-CHE", "RF-00000250-CHE", "RF-00000143-CHE", "RF-00000189-CHE", "RF-00000007-CHE", "RF-00000147-CHE", "RF-00000186-CHE", "RF-00000164-CHE", "RF-00000184-CHE", "RF-00000053-CHE", "RF-00000251-CHE", "RF-0517-001-PPP", "RF-0724-001-PPP", "RF-0828-001-PPP", "RF-0844-001-PPP", "RF-0911-001-PPP", "RF-0032-001-PPP", "RF-0127-001-PPP", "RF-0146-001-PPP", "RF-0224-001-PPP", "RF-0290-001-PPP", "RF-0139-003-PPP", "RF-0328-002-PPP", "RF-0336-002-PPP", "RF-0178-002-PPP", "RF-0187-007-PPP", "RF-00002952-PAR", "RF-0528-001-PPP", "RF-0851-001-PPP", "RF-0877-001-PPP", "RF-0903-001-PPP", "RF-0985-001-PPP", "RF-0051-001-PPP", "RF-0302-001-PPP", "RF-0337-001-PPP", "RF-0339-001-PPP", "RF-0351-001-PPP", "RF-0149-003-PPP", "RF-0640-001-PPP", "RF-0756-001-PPP", "RF-0846-001-PPP", "RF-0920-001-PPP", "RF-0957-001-PPP", "RF-0305-001-PPP", "RF-0342-001-PPP", "RF-0373-001-PPP", "RF-0149-002-PPP", "RF-0266-002-PPP", "RF-0435-001-PPP", "RF-0599-001-PPP", "RF-0612-001-PPP", "RF-0647-001-PPP", "RF-0670-001-PPP", "RF-0936-001-PPP", "RF-0937-001-PPP", "RF-0946-001-PPP", "RF-0012-001-PPP", "RF-0079-001-PPP", "RF-0088-001-PPP", "RF-0272-001-PPP", "RF-0331-001-PPP", "RF-0348-001-PPP", "RF-0139-002-PPP", "RF-0187-004-PPP", "RF-0432-001-PPP", "RF-0535-001-PPP", "RF-0560-001-PPP", "RF-0575-001-PPP", "RF-0668-001-PPP", "RF-0768-001-PPP", "RF-0161-001-PPP", "RF-0380-001-PPP", "RF-0187-005-PPP", "RF-0338-002-PPP", "RF-0561-001-PPP", "RF-0597-001-PPP", "RF-0737-001-PPP", "RF-0781-001-PPP", "RF-0800-001-PPP", "RF-0866-001-PPP", "RF-0905-001-PPP", "RF-0685-002-PPP", "RF-0160-001-PPP", "RF-0327-001-PPP", "RF-0419-001-PPP", "RF-0187-006-PPP", "RF-0266-003-PPP", "RF-0328-003-PPP", "RF-0412-002-PPP", "RF-00002887-PAR", "RF-0484-001-PPP", "RF-0554-001-PPP", "RF-0571-001-PPP", "RF-0574-001-PPP", "RF-0654-001-PPP", "RF-0759-001-PPP", "RF-0087-001-PPP", "RF-0123-001-PPP", "RF-0125-001-PPP", "RF-0164-001-PPP", "RF-0289-001-PPP", "RF-0149-004-PPP", "RF-0173-004-PPP", "RF-0187-003-PPP", "RF-0323-004-PPP", "RF-0336-005-PPP", "RF-0424-001-PPP", "RF-0442-001-PPP", "RF-0577-001-PPP", "RF-0578-001-PPP", "RF-0821-001-PPP", "RF-0869-001-PPP", "RF-0033-001-PPP", "RF-0594-002-PPP", "RF-0180-001-PPP", "RF-0288-001-PPP", "RF-0336-003-PPP", "RF-0525-001-PPP", "RF-0587-001-PPP", "RF-0677-001-PPP", "RF-0705-001-PPP", "RF-0721-001-PPP", "RF-0040-001-PPP", "RF-0068-001-PPP", "RF-0261-001-PPP", "RF-0374-001-PPP", "RF-0402-001-PPP", "RF-0065-003-PPP", "RF-0690-006-PPP", "RF-1025-001-PPP", "RF-0522-001-PPP", "RF-0046-001-PPP", "RF-0132-001-PPP", "RF-0201-001-PPP", "RF-0251-001-PPP", "RF-0256-001-PPP", "RF-0366-001-PPP", "RF-0660-001-PPP", "RF-0762-001-PPP", "RF-0824-001-PPP", "RF-0028-001-PPP", "RF-0182-001-PPP", "RF-0228-001-PPP", "RF-0335-001-PPP", "RF-0408-001-PPP", "RF-0041-002-PPP", "RF-0347-003-PPP", "RF-00000161-VET", "RF-00003017-PAR", "RF-0690-002-PPP", "RF-0464-001-PPP", "RF-0489-001-PPP", "RF-0860-001-PPP", "RF-0320-001-PPP", "RF-0361-001-PPP", "RF-0291-002-PPP", "RF-0043-003-PPP", "RF-0792-001-PPP", "RF-0304-001-PPP", "RF-0334-001-PPP", "RF-0347-002-PPP", "RF-0354-002-PPP", "RF-0636-001-PPP", "RF-0835-001-PPP", "RF-0922-001-PPP", "RF-0933-001-PPP", "RF-0975-001-PPP", "RF-0018-001-PPP", "RF-0183-001-PPP", "RF-0385-001-PPP", "RF-0020-003-PPP", "RF-0065-002-PPP", "RF-0112-001-PPP", "RF-0293-003-PPP", "RF-0962-002-PPP", "RF-00000132-VET", "RF-00000067-ORG", "RF-0524-001-PPP", "RF-0586-001-PPP", "RF-00000141-VET", "RF-0062-001-PPP", "RF-0120-001-PPP", "RF-0291-004-PPP", "RF-0842-002-PPP", "RF-0430-001-PPP", "RF-0469-001-PPP", "RF-0584-001-PPP", "RF-0662-001-PPP", "RF-0663-001-PPP", "RF-0842-001-PPP", "RF-0122-001-PPP", "RF-0420-001-PPP", "RF-0020-002-PPP", "RF-0020-004-PPP", "RF-0108-001-PPP", "RF-0112-004-PPP", "RF-0291-003-PPP", "RF-0293-002-PPP", "RF-0690-003-PPP", };

                if (allGroups.Any(g => g == paramCode))
                {
                    var list = new List<String>();
                    list.Add("G191A");
                    list.Add("G171A");
                    list.Add("G145A");
                    list.Add("G138A");
                    list.Add("G081A");
                    list.Add("G080A");
                    list.Add("G079A");
                    list.Add("G078A");
                    list.Add("G077A");
                    list.Add("G076A");
                    list.Add("G063A");
                    list.Add("G062A");
                    list.Add("G061A");
                    list.Add("G060A");
                    list.Add("G058A");
                    list.Add("G057A");
                    list.Add("G052A");
                    list.Add("G051A");
                    list.Add("G050A");
                    list.Add("G049A");
                    list.Add("G047A");
                    list.Add("G046A");
                    list.Add("G017A");
                    list.Add("G016A");
                    list.Add("G015A");
                    list.Add("G014A");
                    list.Add("G013A");
                    outcome.passed = list.Contains(resUnit);
                }
            }
            return outcome;
        }


        ///If the value in 'Parameter code' (paramCode) belongs to the group B3c (chemical elements used in vmpr), then a value in the data element 'Result LOQ' (resLOQ) should be reported;
        public Outcome VMPR016(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");
            var paramCode = (string)sample.Element("paramCode");
            string[] grupp = { "RF-00000001-RAD", "RF-00000002-CHE", "RF-00000006-CHE", "RF-00000013-CHE", "RF-00000020-CHE", "RF-00000025-CHE", "RF-00000029-CHE", "RF-00000048-CHE", "RF-00000052-CHE", "RF-00000059-CHE", "RF-00000066-CHE", "RF-00000071-CHE", "RF-00000080-CHE", "RF-00000103-CHE", "RF-00000110-CHE", "RF-00000123-CHE", "RF-00000125-CHE", "RF-00000127-CHE", "RF-00000142-CHE", "RF-00000144-CHE", "RF-00000146-CHE", "RF-00000149-CHE", "RF-00000151-CHE", "RF-00000160-CHE", "RF-00000163-CHE", "RF-00000166-CHE", "RF-00000169-CHE", "RF-00000171-CHE", "RF-00000173-CHE", "RF-00000175-CHE", "RF-00000178-CHE", "RF-00000181-CHE", "RF-00000183-CHE", "RF-00000188-CHE", "RF-00000190-CHE", "RF-00000192-CHE", "RF-00000194-CHE", "RF-00000196-CHE", "RF-00000198-CHE", "RF-00000200-CHE", "RF-00000202-CHE", "RF-00000204-CHE", "RF-00001449-PAR" };

            var outcome = new Outcome();
            outcome.name = "VMPR016";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "If the value in 'Parameter code' (paramCode) belongs to the group B3c (chemical elements used in vmpr), then a value in the data element 'Result LOQ' (resLOQ) should be reported;";
            outcome.error = "WARNING: resLOQ is missing, though paramCode belongs to the group B3c (chemical elements used in vmpr);";
            outcome.type = "warning";
            outcome.values.Add(Tuple.Create<string, string>(nameof(resLOQ), resLOQ));
            outcome.values.Add(Tuple.Create<string, string>(nameof(paramCode), paramCode));

            outcome.passed = true;


            if (grupp.Contains(paramCode))
            {
                outcome.passed = !string.IsNullOrEmpty(resLOQ);

            }

            return outcome;
        }
        ///A value in at least one of the following data elements must be reported: 'Result LOQ' (resLOQ) or 'Result LOD' (resLOD) or 'CC beta' (CCbeta) or 'CC alpha' (CCalpha) if it is not in the groups B3a, B3f or an inhibitor;
        public Outcome VMPR017(XElement sample)
        {
            // <checkedDataElements>;
            var resLOQ = (string)sample.Element("resLOQ");
            var resLOD = (string)sample.Element("resLOD");
            var CCbeta = (string)sample.Element("CCbeta");
            var CCalpha = (string)sample.Element("CCalpha");
            var paramCode = (string)sample.Element("paramCode");

            var outcome = new Outcome();
            outcome.name = "VMPR017";
            outcome.lastupdate = "2018-01-08";
            outcome.description = "A value in at least one of the following data elements must be reported: 'Result LOQ' (resLOQ) or 'Result LOD' (resLOD) or 'CC beta' (CCbeta) or 'CC alpha' (CCalpha) if it is not in the groups B3a, B3f or an inhibitor;";
            outcome.error = "One of resLOQ, resLOD, CCbeta or CCalpha must be reported (excluding B3a, B3f substances or inhibitors);";
            outcome.type = "error";
            outcome.passed = true;


            //Logik

            var b3a = new List<string> { "RF-0605-001-PPP", "RF-0684-001-PPP", "RF-0061-001-PPP", "RF-0084-001-PPP", "RF-0156-001-PPP", "RF-0237-001-PPP", "RF-0246-001-PPP", "RF-0311-001-PPP", "RF-0406-001-PPP", "RF-0128-003-PPP", "RF-0383-003-PPP", "RF-00000460-ORG", "RF-00000337-ORG", "RF-00000339-ORG", "RF-00000342-ORG", "RF-00000343-ORG", "RF-00000182-ORG", "RF-00000201-ORG", "RF-00000243-ORG", "RF-00000251-ORG", "RF-00000328-ORG", "RF-00002896-PAR", "RF-00000143-ORG", "RF-00000169-ORG", "RF-00000211-ORG", "RF-00000212-ORG", "RF-00000327-ORG", "RF-0453-001-PPP", "RF-0060-001-PPP", "RF-0329-001-PPP", "RF-0410-001-PPP", "RF-0010-003-PPP", "RF-0130-002-PPP", "RF-00000470-ORG", "RF-0075-001-PPP", "RF-0653-001-PPP", "RF-0819-001-PPP", "RF-0052-001-PPP", "RF-0082-001-PPP", "RF-0093-001-PPP", "RF-0130-003-PPP", "RF-00000338-ORG", "RF-00000119-ORG", "RF-00000124-ORG", "RF-00000126-ORG", "RF-00000128-ORG", "RF-00000158-ORG", "RF-00000253-ORG", "RF-00000072-TOX", "RF-0001-001-PPP", "RF-0240-004-PPP", "RF-0349-002-PPP", "RF-0515-001-PPP", "RF-0603-001-PPP", "RF-0736-001-PPP", "RF-0755-001-PPP", "RF-0878-001-PPP", "RF-0923-001-PPP", "RF-0997-001-PPP", "RF-0999-001-PPP", "RF-0090-001-PPP", "RF-0295-001-PPP", "RF-0364-001-PPP", "RF-0187-002-PPP", "RF-0383-002-PPP", "RF-00000330-ORG", "RF-00000333-ORG", "RF-00000334-ORG", "RF-00000346-ORG", "RF-00000117-ORG", "RF-00000226-ORG", "RF-00000307-ORG", "RF-0119-001-PPP", "RF-0155-001-PPP", "RF-00000020-ORG", "RF-00000341-ORG", "RF-00000345-ORG", "RF-00000134-ORG", "RF-00000147-ORG", "RF-00000255-ORG", "RF-0559-001-PPP", "RF-0889-001-PPP", "RF-0998-001-PPP", "RF-1008-001-PPP", "RF-0008-001-PPP", "RF-0214-001-PPP", "RF-0221-001-PPP", "RF-0355-001-PPP", "RF-00002898-PAR", "RF-00000030-ORG", "RF-0837-001-PPP", "RF-0045-001-PPP", "RF-0078-001-PPP", "RF-0174-001-PPP", "RF-0255-001-PPP", "RF-0350-001-PPP", "RF-00000336-ORG", "RF-00000347-ORG", "RF-00000121-ORG", "RF-00000127-ORG", "RF-00000173-ORG", "RF-00000178-ORG", "RF-00000181-ORG", "RF-00000208-ORG", "RF-00000472-ORG", "RF-00000006-PAR", "RF-00002897-PAR", "RF-00000186-ORG", "RF-00000215-ORG", "RF-00000219-ORG", "RF-00000278-ORG", "RF-00000323-ORG", "RF-0557-001-PPP", "RF-0600-001-PPP", "RF-0838-001-PPP", "RF-0994-001-PPP", "RF-0995-001-PPP", "RF-0073-001-PPP", "RF-0129-001-PPP", "RF-0264-001-PPP", "RF-0450-003-PPP", "RF-0236-001-PPP", "RF-0059-003-PPP", "RF-0548-001-PPP", "RF-0556-001-PPP", "RF-0797-001-PPP", "RF-0996-001-PPP", "RF-00000331-ORG", "RF-00000332-ORG", "RF-00000335-ORG", "RF-00000344-ORG", "RF-00000118-ORG", "RF-00000120-ORG", "RF-00000122-ORG", "RF-00000123-ORG", "RF-00000129-ORG", "RF-00000131-ORG", "RF-00000184-ORG", "RF-0059-001-PPP", "RF-0021-001-PPP", "RF-00000471-ORG", };
            var b3f = new List<string> { "RF-0633-001-PPP", "RF-0801-001-PPP", "RF-0260-001-PPP", "RF-0428-003-PPP", "RF-00000104-ORG", "RF-00000005-ORG", "RF-00002920-PAR", "RF-00000052-ORG", "RF-00000006-ORG", "RF-00000079-ORG", "RF-00000080-ORG", "RF-00000075-ORG", "RF-00000362-ORG", "RF-0952-001-PPP", "RF-00002890-PAR", "RF-00002891-PAR", "RF-00002930-PAR", "RF-00002957-PAR", "RF-0133-001-PPP", "RF-0281-002-PPP", "RF-00000099-ORG", "RF-00000100-ORG", "RF-00000046-ORG", "RF-00000360-ORG", "RF-0194-002-PPP", "RF-00002864-PAR", "RF-00002866-PAR", "RF-00002933-PAR", "RF-00002949-PAR", "RF-0810-001-PPP", "RF-0029-001-PPP", "RF-0241-001-PPP", "RF-00000098-ORG", "RF-00000102-ORG", "RF-00000050-ORG", "RF-00000004-ORG", "RF-00000078-ORG", "RF-00000354-ORG", "RF-00000358-ORG", "RF-0422-001-PPP", "RF-00002863-PAR", "RF-00002931-PAR", "RF-00000097-ORG", "RF-00000047-ORG", "RF-00000009-ORG", "RF-0616-001-PPP", "RF-0035-001-PPP", "RF-0113-001-PPP", "RF-00000401-ORG", "RF-00002929-PAR", "RF-0014-001-PPP", "RF-0539-001-PPP", "RF-0101-001-PPP", "RF-0390-001-PPP", "RF-00000005-RAD", "RF-00000101-ORG", "RF-00000003-ORG", "RF-00000008-ORG", "RF-00000357-ORG", "RF-00000359-ORG", "RF-00000361-ORG", "RF-0049-001-PPP", "RF-00000461-ORG", "RF-00002860-PAR", "RF-00002892-PAR", "RF-00002932-PAR", "RF-00000004-RAD", "RF-00000103-ORG", "RF-00000045-ORG", "RF-00000056-ORG", "RF-00000007-ORG", "RF-0250-001-PPP", "RF-0285-001-PPP", "RF-0358-001-PPP", "RF-0415-001-PPP", "RF-0417-001-PPP", "RF-0418-001-PPP", "RF-0439-001-PPP", "RF-00000049-ORG", "RF-00000061-ORG", "RF-0048-001-PPP", "RF-0165-001-PPP", "RF-0218-001-PPP", "RF-0403-001-PPP", "RF-0425-002-PPP", };

            if (!b3a.Union(b3f).Any(b => b == paramCode) && paramCode != "RF-00000585-VET")
            {
                outcome.passed = !String.IsNullOrEmpty(resLOD) || !String.IsNullOrEmpty(resLOD) || !String.IsNullOrEmpty(CCalpha) || !String.IsNullOrEmpty(CCbeta);
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
        ///If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A) and the value in the data element 'Analytical method type' (anMethType) is 'Confirmation' (AT08A) and the value in 'Typer of parameter' (paramType) is not equal to 'Part of a sum' (P002A), and the value in 'Parameter code' (paramCode) doesn't belong to group B3c (chemical elements used in vmpr), then a value in the data element 'CC alpha' (CCalpha) must be reported;
        public Outcome VMPR018(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string)sample.Element("paramCode");
            var accredProc = (string)sample.Element("accredProc");
            var anMethType = (string)sample.Element("anMethType");
            var paramType = (string)sample.Element("paramType");
            var CCalpha = (string)sample.Element("CCalpha");

            string[] b3c = { "RF-00000001-RAD", "RF-00000002-CHE", "RF-00000006-CHE", "RF-00000013-CHE", "RF-00000020-CHE", "RF-00000025-CHE", "RF-00000029-CHE", "RF-00000048-CHE", "RF-00000052-CHE", "RF-00000059-CHE", "RF-00000066-CHE", "RF-00000071-CHE", "RF-00000080-CHE", "RF-00000103-CHE", "RF-00000110-CHE", "RF-00000123-CHE", "RF-00000125-CHE", "RF-00000127-CHE", "RF-00000142-CHE", "RF-00000144-CHE", "RF-00000146-CHE", "RF-00000149-CHE", "RF-00000151-CHE", "RF-00000160-CHE", "RF-00000163-CHE", "RF-00000166-CHE", "RF-00000169-CHE", "RF-00000171-CHE", "RF-00000173-CHE", "RF-00000175-CHE", "RF-00000178-CHE", "RF-00000181-CHE", "RF-00000183-CHE", "RF-00000188-CHE", "RF-00000190-CHE", "RF-00000192-CHE", "RF-00000194-CHE", "RF-00000196-CHE", "RF-00000198-CHE", "RF-00000200-CHE", "RF-00000202-CHE", "RF-00000204-CHE", "RF-00001449-PAR" };
            var outcome = new Outcome();
            outcome.name = "VMPR018";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A) and the value in the data element 'Analytical method type' (anMethType) is 'Confirmation' (AT08A) and the value in 'Typer of parameter' (paramType) is not equal to 'Part of a sum' (P002A), and the value in 'Parameter code' (paramCode) doesn't belong to group B3c (chemical elements used in vmpr), then a value in the data element 'CC alpha' (CCalpha) must be reported;";
            outcome.error = "CCalpha is missing, though mandatory if accredProc is accredited and validated according to Com. Dec. 2002/657/EC and anMethType is confirmation and paramType is not part of a sum and paramCode is not in group B3c (chemical elements used in vmpr);";
            outcome.type = "error";
            outcome.passed = true;
            if (accredProc == "V007A" && anMethType == "AT08A" && paramType != "P002A" && b3c.Contains(paramCode) == false)
            {
                outcome.passed = string.IsNullOrEmpty(CCalpha);
            }
            return outcome;
        }
        ///If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A) and the value in the data element 'Analytical method type' (anMethType) is 'Screening' (AT06A), and the value in 'Parameter code' (paramCode) doesn't belong to group B3c (chemical elements used in vmpr), then a value in the data element 'CC beta' (CCbeta) must be reported
        public Outcome VMPR019(XElement sample)
        {
            // <checkedDataElements>;
            var paramCode = (string)sample.Element("paramCode");
            var accredProc = (string)sample.Element("accredProc");
            var anMethType = (string)sample.Element("anMethType");
            var CCbeta = (string)sample.Element("CCbeta");
            string[] b3c = { "RF-00000001-RAD", "RF-00000002-CHE", "RF-00000006-CHE", "RF-00000013-CHE", "RF-00000020-CHE", "RF-00000025-CHE", "RF-00000029-CHE", "RF-00000048-CHE", "RF-00000052-CHE", "RF-00000059-CHE", "RF-00000066-CHE", "RF-00000071-CHE", "RF-00000080-CHE", "RF-00000103-CHE", "RF-00000110-CHE", "RF-00000123-CHE", "RF-00000125-CHE", "RF-00000127-CHE", "RF-00000142-CHE", "RF-00000144-CHE", "RF-00000146-CHE", "RF-00000149-CHE", "RF-00000151-CHE", "RF-00000160-CHE", "RF-00000163-CHE", "RF-00000166-CHE", "RF-00000169-CHE", "RF-00000171-CHE", "RF-00000173-CHE", "RF-00000175-CHE", "RF-00000178-CHE", "RF-00000181-CHE", "RF-00000183-CHE", "RF-00000188-CHE", "RF-00000190-CHE", "RF-00000192-CHE", "RF-00000194-CHE", "RF-00000196-CHE", "RF-00000198-CHE", "RF-00000200-CHE", "RF-00000202-CHE", "RF-00000204-CHE", "RF-00001449-PAR" };

            var outcome = new Outcome();
            outcome.name = "VMPR019";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A) and the value in the data element 'Analytical method type' (anMethType) is 'Screening' (AT06A), and the value in 'Parameter code' (paramCode) doesn't belong to group B3c (chemical elements used in vmpr), then a value in the data element 'CC beta' (CCbeta) must be reported";
            outcome.error = "CCbeta is missing, though mandatory if accredProc is accredited and validated according to Com. Dec. 2002/657/EC and anMethType is screening and paramCode is not in group B3c (chemical elements used in vmpr);";
            outcome.type = "error";
            outcome.passed = true;

            if (accredProc == "V007A" && anMethType == "AT06A" && b3c.Contains(paramCode) == false)
            {
                outcome.passed = string.IsNullOrEmpty(CCbeta);
            }

            return outcome;
        }

        ///If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A), then the value in the data element 'CC beta' (CCbeta) should be reported;
        public Outcome VMPR020(XElement sample)
        {
            // <checkedDataElements>;
            var accredProc = (string)sample.Element("accredProc");
            var CCbeta = (string)sample.Element("CCbeta");

            var outcome = new Outcome();
            outcome.name = "VMPR020";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in the data element 'Accreditation procedure for the analytical method' (accredProc) is 'Accredited and validated according to Com. Dec. 2002/657/EC' (V007A), then the value in the data element 'CC beta' (CCbeta) should be reported;";
            outcome.error = "WARNING: CCbeta is missing, though recommended if accredProc is accredited and validated according to Com. Dec. 2002/657/E;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (accredProc == "V007A")
            {
                outcome.passed = !String.IsNullOrEmpty(CCbeta);
            }
            return outcome;
        }
        ///The value in the data element 'Result qualitative value' (resQualValue) must be equal to 'negative/absent' (NEG), because neither positive screening results nor qualitative confirmation results should be reported;
        public Outcome VMPR021(XElement sample)
        {
            // <checkedDataElements>;
            var resQualValue = (string)sample.Element("resQualValue");

            var outcome = new Outcome();
            outcome.name = "VMPR021";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "The value in the data element 'Result qualitative value' (resQualValue) must be equal to 'negative/absent' (NEG), because neither positive screening results nor qualitative confirmation results should be reported;";
            outcome.error = "resQualValue is different from negative/absent;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resQualValue))
            {
                var resQualValues = new List<string>();
                resQualValues.Add("NEG");
                outcome.passed = resQualValues.Contains(resQualValue);

            }
            return outcome;
        }

        ///If the value in 'Type of results' (resType) is equal to 'Qualitative value (binary)' (BIN), then the value in the data element 'Analytical method type' (anMethType) should be equal to 'Screening' (AT06A), and the value 'Result qualitative value' (resQualValue) should be equal to 'Negative' (NEG);
        public Outcome VMPR022(XElement sample)
        {
            // <checkedDataElements>;
            var anMethType = sample.Element("anMethType").Value;
            var resType = sample.Element("resType").Value;
            var resQualValue = sample.Element("resQualValue").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR022";
            outcome.lastupdate = "2017-11-27";
            outcome.description = "If the value in 'Type of results' (resType) is equal to 'Qualitative value (binary)' (BIN), then the value in the data element 'Analytical method type' (anMethType) should be equal to 'Screening' (AT06A), and the value 'Result qualitative value' (resQualValue) should be equal to 'Negative' (NEG);";
            outcome.error = "resType is BIN and anMethType is  screening or resQualValue is not NEG;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            if (resType == "BIN")
            {
                outcome.passed = anMethType == "AT06A" && resQualValue == "NEG";
            }

            return outcome;
        }

        ///If the value in the data element 'Analytical method type' (anMethType) is  'Confirmation' (AT08A), then the value in 'Type of results' (resType) should be equal to 'non detected value (below LOD)' (LOD), or 'non quantified value (below LOQ)' (LOQ), or 'numerical value' (VAL), or 'value below CCalpha (below CC alpha)' (CCA), and the value in 'Result qualitative value' (resQualValue) should not be reported;
        public Outcome VMPR023(XElement sample)
        {
            // <checkedDataElements>;
            var anMethType = (string)sample.Element("anMethType");
            var resType = (string)sample.Element("resType");
            var resQualValue = (string)sample.Element("resQualValue");

            var outcome = new Outcome();
            outcome.name = "VMPR023";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "If the value in the data element 'Analytical method type' (anMethType) is  'Confirmation' (AT08A), then the value in 'Type of results' (resType) should be equal to 'non detected value (below LOD)' (LOD), or 'non quantified value (below LOQ)' (LOQ), or 'numerical value' (VAL), or 'value below CCalpha (below CC alpha)' (CCA), and the value in 'Result qualitative value' (resQualValue) should not be reported;";
            outcome.error = "WARNING: resType is different from LOD, LOQ, VAL, CCA or resQualValue is reported, though anMethType is confirmation;";
            outcome.type = "warning";
            outcome.values.Add(Tuple.Create<string, string>(nameof(anMethType), anMethType));
            outcome.values.Add(Tuple.Create<string, string>(nameof(resType), resType));
            outcome.values.Add(Tuple.Create<string, string>(nameof(resQualValue), resQualValue));

            outcome.passed = true;

            //Logik (ignore null: no);
            if (anMethType == "AT08A")
            {
                var resTypes = new List<string>();
                resTypes.Add("LOD");
                resTypes.Add("LOQ");
                resTypes.Add("VAL");
                resTypes.Add("CCA");
                outcome.passed = resTypes.Contains(resType) && String.IsNullOrEmpty(resQualValue);
            }

            return outcome;
        }
        ///The value in the data element 'Type of result' (resType), must be equal to 'non detected value (below LOD)' (LOD), or 'non quantified value (below LOQ)' (LOQ), or 'numerical value' (VAL), or 'value below CCalpha (below CC alpha)' (CCA), or 'value below CCbeta (below CC beta)' (CCB), or 'qualitative value (Binary)' (BIN), or 'Value above the upper limit of the working range' (AWR);
        public Outcome VMPR024(XElement sample)
        {
            // <checkedDataElements>;
            var resType = (string)sample.Element("resType");

            var outcome = new Outcome();
            outcome.name = "VMPR024";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "The value in the data element 'Type of result' (resType), must be equal to 'non detected value (below LOD)' (LOD), or 'non quantified value (below LOQ)' (LOQ), or 'numerical value' (VAL), or 'value below CCalpha (below CC alpha)' (CCA), or 'value below CCbeta (below CC beta)' (CCB), or 'qualitative value (Binary)' (BIN), or 'Value above the upper limit of the working range' (AWR);";
            outcome.error = "resType is different from LOD, LOQ, VAL, CCA, CCB, BIN, and AWR;";
            outcome.type = "error";
            outcome.values.Add(Tuple.Create<string, string>(nameof(resType), resType));

            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(resType))
            {
                var resTypes = new List<string>();
                resTypes.Add("LOD");
                resTypes.Add("LOQ");
                resTypes.Add("VAL");
                resTypes.Add("CCA");
                resTypes.Add("CCB");
                resTypes.Add("BIN");
                resTypes.Add("AWR");
                outcome.passed = resTypes.Contains(resType);

            }
            return outcome;
        }
        ///If a value in the data element 'Result value' (resVal) is reported and 'CC alpha' (CCalpha) is not reported, then a value in at least one of the following data elements should be reported: 'Result value uncertainty' (resValUncert) or 'Result value uncertainty Standard deviation' (resValUncertSD), i.e. precision must be determined for quantitative results;
        public Outcome VMPR025(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var CCalpha = (string)sample.Element("CCalpha");
            var resValUncert = (string)sample.Element("resValUncert");
            var resValUncertSD = (string)sample.Element("resValUncertSD");

            var outcome = new Outcome();
            outcome.name = "VMPR025";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "If a value in the data element 'Result value' (resVal) is reported and 'CC alpha' (CCalpha) is not reported, then a value in at least one of the following data elements should be reported: 'Result value uncertainty' (resValUncert) or 'Result value uncertainty Standard deviation' (resValUncertSD), i.e. precision must be determined for quantitative results;";
            outcome.error = "WARNING: resValUncert and resValUncertSD are missing, though at least one is recommended when resVal is reported and CCalpha is missing;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (string.IsNullOrEmpty(CCalpha) && !string.IsNullOrEmpty(resVal))
            {
                var resVals = new List<string>();
                ///TESTING
                outcome.passed = !string.IsNullOrEmpty(resValUncert) || !string.IsNullOrEmpty(resValUncertSD);
            }
            return outcome;
        }

        ///The value in the data element 'Type of limit for the result evaluation' (evalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'Minimum Required Performance Limit (MRPL)' (W005A), or 'Reference point of action (RPA)' (W006A), or 'Presence' (W012A), or 'Maximum Limit' (W001A), or 'Action level' (W007A);
        public Outcome VMPR026(XElement sample)
        {
            // <checkedDataElements>;
            var evalLimitType = (string)sample.Element("evalLimitType");

            var outcome = new Outcome();
            outcome.name = "VMPR026";
            outcome.lastupdate = "2017-11-16";
            outcome.description = "The value in the data element 'Type of limit for the result evaluation' (evalLimitType) should be equal to 'Maximum Residue Level (MRL)' (W002A), or 'Minimum Required Performance Limit (MRPL)' (W005A), or 'Reference point of action (RPA)' (W006A), or 'Presence' (W012A), or 'Maximum Limit' (W001A), or 'Action level' (W007A);";
            outcome.error = "WARNING: evalLimitType is not in the list of recommended codes;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(evalLimitType))
            {
                var evalLimitTypes = new List<string>();
                evalLimitTypes.Add("W002A");
                evalLimitTypes.Add("W005A");
                evalLimitTypes.Add("W006A");
                evalLimitTypes.Add("W012A");
                evalLimitTypes.Add("W001A");
                evalLimitTypes.Add("W007A");
                outcome.passed = evalLimitTypes.Contains(evalLimitType);

            }
            return outcome;
        }

        ///If a value in the data element 'Result value' (resVal) is reported and the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is different from 'Presence' (W012A), then a a value in the data element 'Limit for the result evaluation' (evalLowLimit) should be reported;
        public Outcome VMPR027(XElement sample)
        {
            // <checkedDataElements>;
            var resVal = (string)sample.Element("resVal");
            var evalLimitType = (string)sample.Element("evalLimitType");
            var evalLowLimit = (string)sample.Element("evalLowLimit");

            var outcome = new Outcome();
            outcome.name = "VMPR027";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If a value in the data element 'Result value' (resVal) is reported and the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is different from 'Presence' (W012A), then a a value in the data element 'Limit for the result evaluation' (evalLowLimit) should be reported;";
            outcome.error = "WARNING: evalLowLimit is missing, though recommended when resVal is reported and evalLimitType is different from presence;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: no);
            if (!string.IsNullOrEmpty(resVal) && evalLimitType == "W012A")
            {
                outcome.passed = !String.IsNullOrEmpty(evalLowLimit);
            }
            return outcome;
        }
        ///If the value in the data element 'Evaluation of the result' (evalCode) is different from 'Not detected' (J040A) and 'Result not evaluated' (J029A), then a value in the data element 'Type of limit for the result evaluation' (evalLimitType) should be reported;
        public Outcome VMPR028(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");
            var evalLimitType = (string)sample.Element("evalLimitType");

            var outcome = new Outcome();
            outcome.name = "VMPR028";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in the data element 'Evaluation of the result' (evalCode) is different from 'Not detected' (J040A) and 'Result not evaluated' (J029A), then a value in the data element 'Type of limit for the result evaluation' (evalLimitType) should be reported;";
            outcome.error = "WARNING: evalLimitType is missing, though recommended when evalCode is neither 'not detected' nor 'result not evaluated';";
            outcome.type = "warning";
            outcome.passed = true;


            var evalCodes = new List<string>();
            evalCodes.Add("J040A");
            evalCodes.Add("J029A");
            ///TESTING
            if (evalCodes.Contains(evalCode))
            {
                outcome.passed = !String.IsNullOrEmpty(evalLimitType);
            }
            return outcome;
        }
        ///The value in the data element 'Evaluation of the result' (evalCode) must be equal to 'Detected' (J041A), or 'Not detected' (J040A), or 'Above maximum permissible quantities' (J003A), or 'Less than or equal to maximum permissible quantities' (J002A), or 'Compliant due to measurement uncertainty' (J031A), or 'Result not evaluated' (J029A);
        public Outcome VMPR029(XElement sample)
        {
            // <checkedDataElements>;
            var evalCode = (string)sample.Element("evalCode");

            var outcome = new Outcome();
            outcome.name = "VMPR029";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "The value in the data element 'Evaluation of the result' (evalCode) must be equal to 'Detected' (J041A), or 'Not detected' (J040A), or 'Above maximum permissible quantities' (J003A), or 'Less than or equal to maximum permissible quantities' (J002A), or 'Compliant due to measurement uncertainty' (J031A), or 'Result not evaluated' (J029A);";
            outcome.error = "evalCode is not in the allowed list of codes;";
            outcome.type = "error";
            outcome.passed = true;
            outcome.values.Add(Tuple.Create<string, string>(nameof(evalCode), evalCode));
            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(evalCode))
            {
                var evalCodes = new List<string>();
                evalCodes.Add("J041A");
                evalCodes.Add("J040A");
                evalCodes.Add("J003A");
                evalCodes.Add("J002A");
                evalCodes.Add("J031A");
                evalCodes.Add("J029A");
                outcome.passed = evalCodes.Contains(evalCode);

            }
            return outcome;
        }
        ///If the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is equal to 'Maximum Residue Level (MRL)' (W002A), then the value in 'Evaluation of the result' (evalCode) should be equal to 'Less than or equal to maximum permissible quantities' (J002A), or 'Greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), or 'result not evaluated' (J029A);
        public Outcome VMPR030(XElement sample)
        {
            // <checkedDataElements>;
            var evalLimitType = (string)sample.Element("evalLimitType");
            var evalCode = (string)sample.Element("evalCode");

            var outcome = new Outcome();
            outcome.name = "VMPR030";
            outcome.lastupdate = "2017-03-17";
            outcome.description = "If the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is equal to 'Maximum Residue Level (MRL)' (W002A), then the value in 'Evaluation of the result' (evalCode) should be equal to 'Less than or equal to maximum permissible quantities' (J002A), or 'Greater than maximum permissible quantities' (J003A), or 'Compliant due to measurement uncertainty' (J031A), or 'result not evaluated' (J029A);";
            outcome.error = "WARNING: evalCode is not in the recommended list of codes when evalLimitType is MRL;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (evalLimitType == "W002A")
            {
                var evalCodes = new List<string>();
                evalCodes.Add("J002A");
                evalCodes.Add("J003A");
                evalCodes.Add("J031A");
                evalCodes.Add("J029A");
                outcome.passed = evalCodes.Contains(evalCode);

            }

            return outcome;
        }
        ///If the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is equal to 'Presence' (W012A), then the value in 'Evaluation of the result' (evalCode) should be equal to 'Not detected' (J040A), or 'Detected' (J041A);
        public Outcome VMPR031(XElement sample)
        {
            // <checkedDataElements>;
            var evalLimitType = (string)sample.Element("evalLimitType");
            var evalCode = (string)sample.Element("evalCode");

            var outcome = new Outcome();
            outcome.name = "VMPR031";
            outcome.lastupdate = "2017-03-17";
            outcome.description = "If the value in the data element 'Type of limit for the result evaluation' (evalLimitType) is equal to 'Presence' (W012A), then the value in 'Evaluation of the result' (evalCode) should be equal to 'Not detected' (J040A), or 'Detected' (J041A);";
            outcome.error = "WARNING: evalCode is not in the recommended list of codes when evalLimitType is presence;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (evalLimitType == "W012A")
            {
                var evalCodes = new List<string>();
                evalCodes.Add("J040A");
                evalCodes.Add("J041A");
                outcome.passed = evalCodes.Contains(evalCode);
            }

            return outcome;
        }
        ///A value in 'Sample taken assessment' (evalInfo.sampTkAsses) must be reported;
        public Outcome VMPR032(XElement sample)
        {

            /*
             Då EFSA har problem med XML-schemat kommer detta värde rapporteras som en textsträng i 
             formatet 
             <evalInfo>
                sampTkAsses=J002A
                </evalInfo>
             
             */

            // <checkedDataElements>;



            var evalInfosampTkAsses = (string)sample.Element("evalInfo");

            var outcome = new Outcome();
            outcome.name = "VMPR032";
            outcome.lastupdate = "2017-03-16";
            outcome.description = "A value in 'Sample taken assessment' (evalInfo.sampTkAsses) must be reported;";
            outcome.error = "evalInfo.sampTkAsses and evalInfo.sampEventAsses are missing, though at least one is mandatory;";
            outcome.type = "error";
            outcome.values.Add(Tuple.Create<string, string>("evalInfo", evalInfosampTkAsses));
            outcome.passed = evalInfosampTkAsses.Contains("sampTkAsses");

            //Logik (ignore null: no);
            if (1 == 1)
            {

                outcome.passed = !String.IsNullOrEmpty(evalInfosampTkAsses);

            }
            return outcome;
        }
        ///If the value in 'Evaluation of the result' (evalCode) is equal to 'Detected' (J041A), or 'greater than maximum permissible quantities' (J003A), and the values in 'Sample taken assessment' (evalInfo.sampTkAsses) and 'Sampling event assessment' (evalInfo.sampEventAsses) are not equal to 'Compliant' (J037A), then a value in the data element 'Action taken' (actTakenCode) must be reported;
        public Outcome VMPR034(XElement sample)
        {
            var evalInfosampTkAsses = sample.Element("evalInfo.sampTkAsses").Value;
            var evalInfosampEventAsses = sample.Element("evalInfo.sampEventAsses").Value;
            var evalCode = sample.Element("evalCode").Value;
            var actTakenCode = sample.Element("actTakenCode").Value;

            var outcome = new Outcome();
            outcome.name = "VMPR034";
            outcome.lastupdate = "2018-01-22";
            outcome.description = "If the value in 'Evaluation of the result' (evalCode) is equal to 'Detected' (J041A), or 'greater than maximum permissible quantities' (J003A), and the values in 'Sample taken assessment' (evalInfo.sampTkAsses) and 'Sampling event assessment' (evalInfo.sampEventAsses) are not equal to 'Compliant' (J037A), then a value in the data element 'Action taken' (actTakenCode) must be reported;";
            outcome.error = "actTakenCode is missing, though mandatory when evalCode is detected or greater than maximum permissible quantities and evalInfo.sampTkAsses and evalInfo.sampEventAsses are non-compliant;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik
            var list = new List<string> { "J041A", "J003A" };

            if (evalInfosampTkAsses != "J037A" && evalInfosampEventAsses != "J037A" && list.Contains(evalCode))
            {
                outcome.passed = actTakenCode != null;
            }

            return outcome;
        }




        ///The value in the data element 'Sample taken assessment' (evalInfo.sampTkAsses) and the value in the data element 'Sampling event assessment' (evalInfo.sampEventAsses) must be equal to 'Compliant' (J037A), or 'Non-compliant' (J038A);
        public Outcome VMPR035(XElement sample)
        {
            // <checkedDataElements>;
            var evalInfosampTkAsses = (string)sample.Element("evalInfo.sampTkAsses");
            var evalInfosampEventAsses = (string)sample.Element("evalInfo.sampEventAsses");

            var outcome = new Outcome();
            outcome.name = "VMPR035";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "The value in the data element 'Sample taken assessment' (evalInfo.sampTkAsses) and the value in the data element 'Sampling event assessment' (evalInfo.sampEventAsses) must be equal to 'Compliant' (J037A), or 'Non-compliant' (J038A);";
            outcome.error = "Neither evalInfo.sampTkAsses nor evalInfo.sampEventAsses are compliant or non-compliant;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(evalInfosampTkAsses) || !String.IsNullOrEmpty(evalInfosampEventAsses))
            {
                var evalInfosampTkAssess = new List<string>();
                evalInfosampTkAssess.Add("J037A");
                evalInfosampTkAssess.Add("J038A");
                outcome.passed = evalInfosampTkAssess.Contains(evalInfosampTkAsses);

                var evalInfosampEventAssess = new List<string>();
                evalInfosampEventAssess.Add("J037A");
                evalInfosampEventAssess.Add("J038A");
                outcome.passed = evalInfosampEventAssess.Contains(evalInfosampEventAsses);

            }
            return outcome;
        }
        ///If the value in the data elemente 'Action Taken' (actTakenCode) is equal to 'Follow-up investigation' (I), then a value in the data element 'Conclusion of follow-up investigation' (evalInfo.conclusion) must be reported;
        public Outcome VMPR036(XElement sample)
        {
            // <checkedDataElements>;
            var actTakenCode = (string)sample.Element("actTakenCode");
            var evalInfoconclusion = (string)sample.Element("evalInfo");


            var outcome = new Outcome();
            outcome.name = "VMPR036";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "If the value in the data elemente 'Action Taken' (actTakenCode) is equal to 'Follow-up investigation' (I), then a value in the data element 'Conclusion of follow-up investigation' (evalInfo.conclusion) must be reported;";
            outcome.error = "evalInfo.conclusion is missing, though actTakenCode is follow-up investigation;";
            outcome.type = "error";
            outcome.values.Add(Tuple.Create<string, string>(nameof(actTakenCode), actTakenCode));
            outcome.values.Add(Tuple.Create<string, string>(nameof(evalInfoconclusion), evalInfoconclusion));

            outcome.passed = true;

            //Logik (ignore null: no);
            if (actTakenCode == "I")
            {
                outcome.passed = evalInfoconclusion.Contains("conclusion");
            }

            return outcome;
        }
        ///The value in the data element 'Sampling point' (sampPoint) should be different from 'Unspecified', 'Others' and 'Unknown';
        public Outcome VMPR037(XElement sample)
        {
            // <checkedDataElements>;
            var sampPoint = (string)sample.Element("sampPoint");

            var outcome = new Outcome();
            outcome.name = "VMPR037";
            outcome.lastupdate = "2016-05-10";
            outcome.description = "The value in the data element 'Sampling point' (sampPoint) should be different from 'Unspecified', 'Others' and 'Unknown';";
            outcome.error = "WARNING: sampPoint is reported as unspecified, or others, or unknown, though these values should not be reported;";
            outcome.type = "warning";
            outcome.passed = true;

            //Logik (ignore null: yes);
            if (!String.IsNullOrEmpty(sampPoint))
            {
                var sampPoints = new List<string>();
                sampPoints.Add("E098A");
                sampPoints.Add("E099A");
                sampPoints.Add("E980A");
                outcome.passed = sampPoints.Contains(sampPoint);

            }
            return outcome;
        }
        ///The value in 'Coded description of the matrix of the sample taken' (sampMatCode) should be equal to the value in 'Coded description of the analysed matrix' (anMatCode);
        public Outcome VMPR038(XElement sample)
        {
            // <checkedDataElements>;
            var sampMatCode = (string)sample.Element("sampMatCode");
            var anMatCode = (string)sample.Element("anMatCode");

            var outcome = new Outcome();
            outcome.name = "VMPR038";
            outcome.lastupdate = "2017-04-20";
            outcome.description = "The value in 'Coded description of the matrix of the sample taken' (sampMatCode) should be equal to the value in 'Coded description of the analysed matrix' (anMatCode);";
            outcome.error = "anMatCode is different from sampMatCode, though the matrix sampled should be equal to the analysed matrix;";
            outcome.type = "error";
            outcome.values.Add(Tuple.Create<string, string>(nameof(sampMatCode), sampMatCode));
            outcome.values.Add(Tuple.Create<string, string>(nameof(anMatCode), anMatCode));

            outcome.passed = true;

            //Logik (ignore null: yes);
            outcome.passed = sampMatCode == anMatCode;

            return outcome;
        }

        ///If the value in 'Coded description of the analysed matrix' (anMatCode) is reported, then the value in sampAnId should be reported;
        public Outcome VMPR039(XElement sample)
        {
            // <checkedDataElements>;
            var sampAnId = (string)sample.Element("sampAnId");
            var anMatCode = (string)sample.Element("anMatCode");

            var outcome = new Outcome();
            outcome.name = "VMPR039";
            outcome.lastupdate = "2018-01-09";
            outcome.description = "If the value in 'Coded description of the analysed matrix' (anMatCode) is reported, then the value in sampAnId should be reported;";
            outcome.error = "sampAnId is missing, though anMatCode is reported;";
            outcome.type = "error";
            outcome.passed = true;

            //Logik (ignore null: no);

            if (!String.IsNullOrEmpty(anMatCode))
            {
                outcome.passed = !String.IsNullOrEmpty(sampAnId);
            }
            return outcome;
        }

    }



}
