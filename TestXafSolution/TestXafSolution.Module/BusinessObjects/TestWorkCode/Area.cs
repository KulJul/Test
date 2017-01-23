using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using System.Linq;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;

namespace TestXafSolution.Module.BusinessObjects.TestWork
{
    [DefaultClassOptions, ImageName("Bo_Area")]
    [Appearance("Delete", TargetItems = "*", Criteria = "Delete_Area > null && Delete_Area >= Create_Area && Create_Area > null",
                                                                                       BackColor = "MistyRose", Enabled = false)]
    [RuleCriteria("Delete_Area >= Create_Area")]
    public partial class Area
    {
        public Area(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        protected override void OnDeleting()
        {
            //Проверка существования груза на площадке
            var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));

            if (CargoFilter.Count() != 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Cargoes is exist on Area"));


            base.OnDeleting();
        }


        protected override void OnSaving()
        {
            // Проверка наличия пикетов на площадке
            if (this.Pickets.Count() == 0)
                throw new UserFriendlyException(new Exception(" Error : " + "Pickets is empty"));

            //if (this.Pickets.Count() == 0 && this.)
            //CheckArea();
            /*
            CheckCargo();

            DeletePicket();*/

            base.OnSaving();
        }
        


        // Проверка площадки на уникальность
        private void CheckArea()
        {
            #region Формируем коллекцию введеных площадок

            var areasCollectionInput = new List<int>();

            GetCollectionFormatArea(this.Name, areasCollectionInput);

            #endregion


            #region Формируем коллекцию сохраненных площадок 

            var areasCollection = new List<int>();

            var areasXPCollection = new XPCollection<Area>(Session, CriteriaOperator.Parse("Number != " + this.Number));
            
            foreach (var areaXPCollection in areasXPCollection)
                GetCollectionFormatArea(areaXPCollection.Name, areasCollection);

            #endregion

            var intersect = areasCollection.Intersect(areasCollectionInput);

            if (intersect.Count() != 0)
                throw new Exception(" Error : already exsist item in Area ");
        }

        //Разбиение названия площадки на пикеты 
        private void GetCollectionFormatArea(string Name, List<int> areasCollection)
        {
            var namesArea = Name.Split('-');

            if (namesArea.Length == 1)
            {
                areasCollection.Add(Convert.ToInt32(namesArea[0]));
            }
            else if (namesArea.Length > 1 && namesArea.Length < 4)
            {
                var firstInputArea = Convert.ToInt32(namesArea[0]);
                var secondInputArea = Convert.ToInt32(namesArea[1]);

                if (Convert.ToInt32(secondInputArea) < Convert.ToInt32(firstInputArea))
                    throw new Exception(" Error : Name Area Second Value < Name Area First Value ");

                for (var countPicket = Convert.ToInt32(firstInputArea); countPicket <= Convert.ToInt32(secondInputArea); countPicket++)
                    areasCollection.Add(countPicket);
            }
            
        }


        // Проверка товаров зависимых от площадки
        private void CheckCargo()
        {
            if (this.Delete_Area != null)
            {
                var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));

                foreach (var cargoElement in this.Cargoes)
                {
                    cargoElement.Delete_Cargo = this.Delete_Area;
                }

                // Или ничего не добавляем и пишем в лог. Второй вариант
                /*
                if (CargoFilter.Count != 0)
                {
                    Tracing.Tracer.LogText("Not Delete Cargo" + "BusinessObjects : Area");
                    return;
                }*/
            }
        }


        // Удаление пикетов зависимых от площадки
        private void DeletePicket()
        {
            if (this.Delete_Area != null)
            {
                var sqlDelete = "DELETE FROM Picket Where NumberArea = " + this.Number;

                var resultDt = this.Session.ExecuteNonQuery(sqlDelete);

                if (resultDt < 1)
                    throw new UserFriendlyException(new Exception(" Error : " + sqlDelete));
            }
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

