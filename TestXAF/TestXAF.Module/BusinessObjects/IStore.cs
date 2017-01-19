using System;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    interface IStore
    {
        DevExpress.Xpo.XPCollection<TestXAF.Module.BusinessObjects.TestWork.Area> Areas { get; }
        string Name { get; set; }
        int Number { get; set; }
    }
}
