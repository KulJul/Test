using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace TestXafSolution.Module.TestWork2
{

    public partial class Store
    {
        public Store(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
