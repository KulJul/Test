namespace TestXAF.Module.Controllers
{
    partial class CreatePicket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CreatePicketAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreatePicketAction
            // 
            this.CreatePicketAction.Caption = "Create Picket Action";
            this.CreatePicketAction.Category = "Tools";
            this.CreatePicketAction.ConfirmationMessage = null;
            this.CreatePicketAction.Id = "CreatePicketAction";
            this.CreatePicketAction.TargetObjectType = typeof(TestXAF.Module.BusinessObjects.TestWork.Area);
            this.CreatePicketAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CreatePicketAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CreatePicketAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreatePicketAction_Execute);
            // 
            // CreatePicket
            // 
            this.Actions.Add(this.CreatePicketAction);
            this.Activated += new System.EventHandler(this.CreatePicket_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreatePicketAction;

    }
}
