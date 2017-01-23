using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;

namespace TestXafSolution.Module.BusinessObjects.TestWork
{

    [DefaultClassOptions, ImageName("Bo_Picket")]
    public partial class Picket
    {
        public Picket(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnDeleting()
        {
            //Проверка существования груза на площадке
            var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.NumberArea.Number));

            if (CargoFilter.Count != 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Cargoes is exist on Area"));


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
