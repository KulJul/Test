using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;

namespace TestXafSolution.Module.BusinessObjects.TestWork
{

    [DefaultClassOptions, ImageName("Bo_Area")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Area > null", BackColor = "MistyRose")]
    [RuleCriteria("Delete_Area >= Create_Area")]
    public partial class Area
    {
        public Area(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
		
		protected override void OnSaving()
        {
            try
            {
                if (this.Delete_Area != null)
                {
                    var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));
                    
                    foreach(var cargoElement in this.Cargoes)
                    {
                        cargoElement.Delete_Cargo = this.Delete_Area;
                    }

                    /*
                    if (CargoFilter.Count != 0)
                    {
                        Tracing.Tracer.LogText("Not Delete Cargo" + "BusinessObjects : Area");
                        return;
                    }*/


                    var sqlDelete = "DELETE FROM Picket Where NumberArea = " + this.Number;

                    var resultDt = this.Session.ExecuteNonQuery(sqlDelete);

                    if (resultDt < 1)
                        throw new Exception(" Error : " + sqlDelete);
                }

                base.OnSaving();
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogText(ex.ToString() + " BusinessObjects : Area");
            }
        }
    }

}

