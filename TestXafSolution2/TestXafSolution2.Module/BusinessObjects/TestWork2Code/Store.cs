using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;

namespace TestXafSolution2.Module.TestWork2
{

    [DefaultClassOptions, ImageName("Bo_Store")]
    public partial class Store
    {
        public Store(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnDeleting()
        {
            //Проверка существования пикетов на складе при удалении
            var PicketFilter = new XPCollection<Picket>(this.Session, CriteriaOperator.Parse("NumberStore == " + this.Number));

            if (PicketFilter.Count != 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Picket is exist in Store"));

            base.OnDeleting();
        }



        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }
    }

}
