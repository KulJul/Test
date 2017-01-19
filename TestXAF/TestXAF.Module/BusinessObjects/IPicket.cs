using System;
using DevExpress.Persistent.Validation;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    public interface IPicket
    {        
        //[RuleRequiredField, RuleUniqueValue]
        [RuleRegularExpression(@"^[0-9]$", CustomMessageTemplate = "Введите число")]
        string Name { get; set; }
        int Number { get; set; }
        TestXAF.Module.BusinessObjects.TestWork.Area NumberArea { get; set; }
    }
}
