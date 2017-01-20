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

namespace TestXAF.Module.Controllers
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

        private void CreatePicketAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentObjectKey = View.ObjectSpace.GetKeyValue(View.CurrentObject);
            var key = Convert.ToInt32(currentObjectKey);
            var name = View.GetCurrentObjectCaption();

            var nameSplit = name.Split('-');
            if (nameSplit.Length >= 1)
            {
                try
                {
                    var nameElements = "";

                    if (nameSplit.Length == 1)
                    {
                        nameElements += string.Format(" ({0}, {1}),", nameSplit[0], key);
                    }
                    else
                    {
                        for (var count = Convert.ToInt32(nameSplit[0]); count <= Convert.ToInt32(nameSplit[1]); count++)
                        {
                            nameElements += string.Format(" ({0}, {1}),", count, key);
                        }
                    }

                    nameElements = nameElements.Trim(',');

                    var dtNames = "INSERT INTO Picket (Name, NumberArea) VALUES  " + nameElements;

                    //var resultDt = Session.ExecuteNonQuery(dtNames);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CreatePicket_Activated(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                var currentObjectKey = View.ObjectSpace.GetKeyValue(View.CurrentObject);

                CreatePicketAction.Active.SetItemValue("EditMode", Convert.ToInt32(currentObjectKey) != 0);
            }
            
        }
    }
}
