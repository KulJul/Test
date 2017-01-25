namespace TestXafSolution2.Module.Controllers
{
    partial class ClearFields
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
            this.ClearFieldsAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ClearFieldsAction
            // 
            this.ClearFieldsAction.Caption = "Clear Fields Action";
            this.ClearFieldsAction.Category = "View";
            this.ClearFieldsAction.ConfirmationMessage = null;
            this.ClearFieldsAction.Id = "ClearFieldsAction";
            this.ClearFieldsAction.ToolTip = null;
            this.ClearFieldsAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ClearFieldsAction_Execute);
            // 
            // ClearFields
            // 
            this.Actions.Add(this.ClearFieldsAction);
            this.Activated += new System.EventHandler(this.ClearFields_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ClearFieldsAction;
    }
}
