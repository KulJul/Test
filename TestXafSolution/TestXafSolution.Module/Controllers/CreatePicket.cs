using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using TestXafSolution.Module.BusinessObjects.TestWork;

namespace TestXafSolution.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CreatePicket : ViewController
    {
        public CreatePicket()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        // Автоматическая генерация пикетов на основе площадок
        private void CreatePicketAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                var session = ((XPObjectSpace)this.ObjectSpace).Session;

                var currentObjectKey = View.ObjectSpace.GetKeyValue(View.CurrentObject);
                var keyArea = currentObjectKey.ToString();
                var name = View.GetCurrentObjectCaption();

                var formatStrPicket = FormatParametr(name, keyArea, session);

                if (formatStrPicket.Length > 0)
                {
                    var sqlrInsertStr = "INSERT INTO Picket (Name, NumberArea) VALUES  " + formatStrPicket;

                    var resultDt = session.ExecuteNonQuery(sqlrInsertStr);

                    if (resultDt < 1)
                        throw new Exception("Error : " + sqlrInsertStr);
                }

            }
            catch (Exception ex)
            {
				Tracing.Tracer.LogText(ex.ToString() +  "Action : Create Picket Action");
            }
        }



        private string FormatParametr(string name, string keyArea, Session session)
        {
            var nameElements = "";
            var nameSplit = name.Split('-');

            if (nameSplit.Length >= 1)
            {
                var namePicketFirst = nameSplit[0];


                if (nameSplit.Length == 1)
                {
                    var pickets = new XPCollection<Picket>(session, CriteriaOperator.Parse("Name == " + namePicketFirst));

                    nameElements += (pickets.Count == 0) ? string.Format(" ({0}, {1}),", namePicketFirst, keyArea) : "";
                }
                else
                {
                    for (var countPicket = Convert.ToInt32(namePicketFirst); countPicket <= Convert.ToInt32(nameSplit[1]); countPicket++)
                    {
                        var pickets = new XPCollection<Picket>(session, CriteriaOperator.Parse("Name == " + countPicket));

                        nameElements += (pickets.Count == 0) ? string.Format(" ({0}, {1}),", countPicket, keyArea) : "";
                    }
                }

                nameElements = (nameElements.Length > 0) ? nameElements.Trim(',') : "";
            }

            return (nameElements.Length > 0) ? nameElements : "";
        }



        private void CreatePicket_Activated(object sender, EventArgs e)
        {
            try
            {
                if (View.GetType() == typeof(DetailView) )
                {
                    CreatePicketAction.Active.SetItemValue("EditMode", false);
                    var currentObjectKey = View.ObjectSpace.GetKeyValue(View.CurrentObject);

					CreatePicketAction.Active.SetItemValue("EditMode", Convert.ToInt32(currentObjectKey) != 0 && View.ObjectTypeInfo.Type == typeof(Area));
				}
			}
            catch (Exception ex)
            {
				Tracing.Tracer.LogText(ex.ToString() +  "Action : Create Picket Action");
            }
        }
    }
}
