using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace TestXAF.Module.BusinessObjects.TestWork
{
    [DefaultClassOptions, ImageName("Bo_Store")]
    public partial class Store
    {
        public Store(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
