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
using TestXafSolution.Module.BusinessObjects.TestWork;

namespace TestXafSolution.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FindFieldData : ViewController
    {
        public FindFieldData()
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

        private void FindFieldDataAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            try
            {
				IObjectSpace objectSpace = Application.CreateObjectSpace();
				string paramValue = e.ParameterCurrentValue as string;
				object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
					CriteriaOperator.Parse(string.Format("Contains([Name], '{0}')", paramValue)));
				if (obj != null)
				{
					DetailView detailView = Application.CreateDetailView(objectSpace, obj);
					detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
					e.ShowViewParameters.CreatedView = detailView;
				}
			}
            catch (Exception ex)
            {
				Tracing.Tracer.LogText(ex.ToString() +  "Action : Find Field Data");
            }
        }

        private void FindFieldData_Activated(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(ListView))
            {
                if (View.ObjectTypeInfo.Type == typeof(Cargo) || View.ObjectTypeInfo.Type == typeof(Area) ||
                    View.ObjectTypeInfo.Type == typeof(Picket) || View.ObjectTypeInfo.Type == typeof(Store))
                {
                    FindFieldDataAction.Enabled.SetItemValue("EditMode", true);
                }
            }
        }
    }
}
