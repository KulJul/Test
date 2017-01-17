using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    interface IStore
    {
        DevExpress.Xpo.XPCollection<TestXAF.Module.BusinessObjects.TestWork.Area> Areas { get; }
        [RuleRequiredField, RuleUniqueValue]
        string Name { get; set; }
        int Number { get; set; }
    }
}
