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
            // Проверка площадки на уникальность
            CheckAreaUniq();

            // Проверка наличия пикетов на площадке
            if (this.Pickets.Count() == 0)
            {
                // Формируем коллекцию введеных площадок
                var areasCollectionInput = new List<int>();
                GetCollectionFormatArea(this.Name, areasCollectionInput);


                var areasXPCollection = new XPCollection<Area>(Session);
                var numberMax = areasXPCollection.Select(p => p.Number).Max() + 1;

                foreach (var area in areasCollectionInput)
                { 
                    Picket pick = new Picket(this.Session);

                    pick.Number = numberMax;
                    pick.Name = area.ToString();
                    pick.NumberArea = this;

                    this.Pickets.Add(pick);
                }

            }


            // Если площадка удаляется, то происходит проверка на существование груза на площадке
            // Если площадка удаляется, то происходит удаление пикетов. Тк храненить пустые ячейки не имеет смысла.

            var CargoFilter = new XPCollection<Cargo>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));
            var PicketFilter = new XPCollection<Picket>(this.Session, CriteriaOperator.Parse("NumberArea == " + this.Number));

            if (this.Delete_Area.CompareTo(default(DateTime)) != 0)
            {
                if (CargoFilter.Count() != 0)
                    throw new UserFriendlyException(new Exception(" Error : " + "Cargoes is exist on Area"));

                this.Session.Delete(PicketFilter);
                this.Session.Save(PicketFilter);
            }

            


            base.OnSaving();
        }
        


        // Проверка площадки на уникальность
        private void CheckAreaUniq()
        {
            // Формируем коллекцию введеных площадок

            var areasCollectionInput = new List<int>();

            GetCollectionFormatArea(this.Name, areasCollectionInput);


            // Формируем коллекцию сохраненных площадок 
            var areasCollection = new List<int>();
            var areasXPCollection = new XPCollection<Area>(Session);

            foreach (var areaXP in areasXPCollection)
            {
                if (areaXP.Delete_Area == DateTime.MinValue)
                    GetCollectionFormatArea(areaXP.Name, areasCollection);
            }
                        

            var intersect = areasCollection.Intersect(areasCollectionInput);

            if (intersect.Count() != 0)
                throw new UserFriendlyException(new Exception(" Error : already exsist item in Area "));
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
                    throw new UserFriendlyException(new Exception(" Error : Name Area Second Value < Name Area First Value "));

                for (var countPicket = Convert.ToInt32(firstInputArea); countPicket <= Convert.ToInt32(secondInputArea); countPicket++)
                    areasCollection.Add(countPicket);
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

