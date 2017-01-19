using System;
namespace TestXAF.Module.BusinessObjects.TestModelCode
{
    interface ICargo
    {
        DateTime Create_Cargo { get; set; }
        DateTime Delete_Cargo { get; set; }
        string Name { get; set; }
        int Number { get; set; }
        TestXAF.Module.BusinessObjects.TestWork.Area NumberArea { get; set; }
        double Weight { get; set; }
    }
}
