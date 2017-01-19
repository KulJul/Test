using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;

namespace TestXAF.Module.BusinessObjects.TestWork
{
    [DefaultClassOptions, ImageName("Bo_Area")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Area > null", BackColor = "MistyRose")]
    [RuleCriteria("Delete_Area >= Create_Area")]
    public partial class Area : TestXAF.Module.BusinessObjects.TestModelCode.IArea
    {
        public Area(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }

}
