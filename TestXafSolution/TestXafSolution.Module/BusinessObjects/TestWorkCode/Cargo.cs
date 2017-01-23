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
            try
            {
                var AreaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.NumberArea.Number));
                               
                foreach (var areaElement in AreaFilter)
                {
                    if (this.Create_Cargo < areaElement.Create_Area)
                        throw new Exception(" Error : " + "Data create cargo < data create area");
                }
                
                base.OnSaving();
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogText(ex.ToString() + " BusinessObjects : Area");
            }
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
