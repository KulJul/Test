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
using TestXafSolution2.Module.TestWork2;
using DevExpress.ExpressApp.Xpo;

namespace TestXafSolution2.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class LinkController2cs : DevExpress.ExpressApp.SystemModule.LinkUnlinkController
    {
        public LinkController2cs()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.GetType() == typeof(ListView) && !View.IsRoot && View.ObjectTypeInfo.Type.Name == "Picket")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
                    CriteriaOperator.Parse(string.Format("Contains([Number], '{0}')", 1)));
                if (obj != null)
                {
                    var temp = (Picket)obj;
                    this.Active.SetItemValue("Error", temp.NumberArea.Cargoes == null);
                }
            }
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

        private void LinkController2cs_Activated(object sender, EventArgs e)
        {

        }
    }
}
