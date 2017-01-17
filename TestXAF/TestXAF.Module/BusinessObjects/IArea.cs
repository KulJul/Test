using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    interface IArea
    {
        DevExpress.Xpo.XPCollection<TestXAF.Module.BusinessObjects.TestWork.Cargo> Cargoes { get; }
        DateTime Create { get; set; }
        [FieldSize(25)]
        [ImmediatePostData]
        [Appearance("MarkUnsafePasswordInRed", "Len(Password) > 0", FontColor = "Red")]
        DateTime Delete1 { get; set; }
        string Name { get; set; }
        int Number { get; set; }
        TestXAF.Module.BusinessObjects.TestWork.Store Number_Store { get; set; }
        DevExpress.Xpo.XPCollection<TestXAF.Module.BusinessObjects.TestWork.Picket> Pickets { get; }
    }
}
