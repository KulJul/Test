using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;

namespace TestXAF.Module.BusinessObjects.TestWork
{

    [DefaultClassOptions, ImageName("Bo_Area")]
    public partial class Area
    {
        public Area(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
