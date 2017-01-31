using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace TestXafSolution2.Module.TestWork2
{

    [DefaultClassOptions, ImageName("Bo_Cargo")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Cargo > null && Delete_Cargo >= Create_Cargo && Create_Cargo > null",
                                                                                           BackColor = "MistyRose", Enabled = false)]
    [RuleCriteria("Delete_Cargo >= Create_Cargo")]
    public partial class Cargo
    {
        public Cargo(Session session) : base(session) { this.Create_Cargo = DateTime.Now; }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnSaving()
        {
            if (!this.IsDeleted)
            {
                // При создании/изменения груза без площадки выдаем ошибку                               
                if (this.Number_Area == null && this.Delete_Cargo == DateTime.MinValue)
                    throw new UserFriendlyException(new Exception(" Error : " + "Груз не должен существовать без площадки"));                
            }


            base.OnSaving();
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
