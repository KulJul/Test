using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using System.Linq;

namespace TestXafSolution2.Module.TestWork2
{

    [DefaultClassOptions, ImageName("Bo_Picket")]
    public partial class Picket
    {
        public Picket(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }


        protected override void OnSaving()
        {
            if(!this.IsDeleted)
            {
                // Проверки существования склада
                if (this.NumberStore == null)
                    throw new UserFriendlyException(new Exception(" Error : " + "Нет склада"));


                // Делаем проверку на вход. данные, либо 1 элемент
                int number = 0;
                if (!int.TryParse(this.Name, out number))
                    throw new UserFriendlyException(new Exception(" Error : Не верный формат пикета." + 
                                                                  " Он должен быть числом и соответствовать номеру склада + порядковому номеру пикета"));

                

                // Проверка площадки на не пересекаемость
                CheckAreaIntersect();
            }


            base.OnSaving();
        }

        protected override void OnDeleting()
        {
            if (this.NumberArea != null)
            {
                //Проверка операции удаления пикетов с площадок
                var AreaFilter = new XPCollection<Area>(this.Session, CriteriaOperator.Parse("Number == " + this.NumberArea.Number));

                
                if (AreaFilter.Count != 0 && AreaFilter[0].Cargoes.Count != 0)
                    throw new UserFriendlyException(new Exception(" Error : " + "На площадке есть груз, уменьшение кол-ва пикетов невозможно"));


                if (AreaFilter[0].Pickets.Count < 2)
                    throw new UserFriendlyException(new Exception(" Error : " + "На площадке должен быть хотя бы 1 пикет, уменьшение кол-ва пикетов невозможно"));
                
            }

            base.OnDeleting();
        }



        // Проверка площадки на не пересекаемость
        private void CheckAreaIntersect()
        {
            if (this.fNumberArea == null)
                return;

            // Формируем коллекцию введеных площадок
            var areasCollectionInput = new List<double>();
            int number = Convert.ToInt32(this.Name);

            areasCollectionInput.Add(number);


            // Формируем коллекцию сохраненных площадок 
            var areasCollection = new List<double>();

            GetCollectionFormatArea(this.fNumberArea.Name, areasCollection);

            var intersect = areasCollectionInput.Intersect(areasCollection);

            if (intersect.Count() == 0)
                throw new UserFriendlyException(new Exception(" Error : Значения пикета и площадки не пересекаются"));
        }



        //Разбиение названия площадки на пикеты 
        private void GetCollectionFormatArea(string Name, List<double> areasCollection)
        {
            var namesArea = Name.Split('-');
            double number = 0;
            double numberNext = 1;
            
            // Проверка на число
            if (!double.TryParse(namesArea[0], out number))
                throw new UserFriendlyException(new Exception(" Error : " + "Площадка имеет не верный формат." + 
                                                              " Должный формат : X или X-X1, где X и X1 - это номер пикета"));



            if (namesArea.Length == 1)
            {
                areasCollection.Add(Convert.ToDouble(number));
            }
            else if (namesArea.Length > 1 && namesArea.Length < 4)
            {
                if (!double.TryParse(namesArea[1], out numberNext))
                    throw new UserFriendlyException(new Exception(" Error : " + "Площадка имеет не верный формат." +
                                                                  " Должный формат : X или X-X1, где X и X1 - это номер пикета"));

                if (numberNext < number)
                    throw new UserFriendlyException(new Exception(" Error : Неправильный диапазон площадок "));

                for (var countPicket = number; countPicket <= numberNext; countPicket++)
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
