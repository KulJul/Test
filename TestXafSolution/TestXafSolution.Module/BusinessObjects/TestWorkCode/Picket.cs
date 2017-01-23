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
using System.Linq;

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

        protected override void OnSaving()
        {
            // При создании/изменения пикета без площадки выдаем ошибку
            if (this.NumberArea == null)
                throw new UserFriendlyException(new Exception(" Error : " + "Number Area is empty"));

            // Проверка площадки на не разрывность
            CheckAreaUniq()

            base.OnSaving();
        }


        // Проверка площадки на уникальность
        private void CheckAreaUniq()
        {
            // Формируем коллекцию введеных площадок

            var areasCollectionInput = new List<int>();

            GetCollectionFormatArea(Session., areasCollectionInput);



            // Формируем коллекцию сохраненных площадок 

            var areasCollection = new List<int>();

            var areasXPCollection = new XPCollection<Area>(Session);

            foreach (var areaXPCollection in areasXPCollection)
                GetCollectionFormatArea(areaXPCollection.Name, areasCollection);


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
