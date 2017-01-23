using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using static System.Net.Mime.MediaTypeNames;


namespace TestXafSolution.Module.BusinessObjects.TestWork
{

    [DefaultClassOptions, ImageName("Bo_Cargo")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Cargo > null && Delete_Cargo >= Create_Cargo && Create_Cargo > null", 
                                                                                           BackColor = "MistyRose", Enabled = false)]
    [RuleCriteria("Delete_Cargo >= Create_Cargo")]
    public partial class Cargo
    {
        public Cargo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        protected override void OnSaving()
        {
            var AreaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.NumberArea.Number));

            // Нельзя добавлять груз на площадку без пикетов
            if (AreaFilter[0].Pickets.Count == 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Pickets is empty"));


            // Добавление груза не должно быть раньше, чем создание площадки                               
            if (this.Create_Cargo < AreaFilter[0].Create_Area)
                throw new UserFriendlyException(new Exception(" Error : " + "Data create cargo < data create area"));
                
            base.OnSaving();
        }
        

        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null && SecuritySystem.CurrentUserId != null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }

                return auditTrail;
            }
        }


    }

}
