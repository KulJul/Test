using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    interface ICargo
    {
        DateTime Create { get; set; }
        [Appearance("MarkUnsafePasswordInRed", "Len(Password) > 0", FontColor = "Red")]
        DateTime Delete1 { get; set; }
        string Name { get; set; }
        int Number { get; set; }
        TestXAF.Module.BusinessObjects.TestWork.Area NumberArea { get; set; }
        double Weight { get; set; }
    }
}
