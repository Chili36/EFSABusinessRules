# EFSA-BR-Validator-C-
Validator for EFSA Datacollection Business rules


# This is a work in progress. As of now it doesn't contain all current EFSA business rules but most of them. The project was born out of necessity to solve validation problems with the Swedish SSD reporting of pesticides, contaminants and VMPR.


The solution contains two projects, Efsas Business Rules Validator and ValidateXML. The rules are contained in the project EFSAS Business Rules Validator while the ValidateXML is a small consoleapplication used for verifying an XML-document. 

**usage**:
running a Xelement through a rule generates an Outcome. In the Outcome there are properties stating if the rule passed or not and what values were used. 
