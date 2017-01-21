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
using DevExpress.ExpressApp.DC;

namespace TestXAF.Module.BusinessObjects.TestWork
{
    [DefaultClassOptions, ImageName("Bo_Cargo")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Cargo > null", BackColor = "MistyRose")]
    [RuleCriteria("Delete_Cargo >= Create_Cargo")]
    public partial class Cargo 
    {
        public Cargo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
    }

}
