namespace TestXafSolution2.Module.Controllers
{
    partial class FindFieldData
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
            this.FindFieldDataAction = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // FindFieldDataAction
            // 
            this.FindFieldDataAction.Caption = "Find Field Data Action";
            this.FindFieldDataAction.ConfirmationMessage = null;
            this.FindFieldDataAction.Id = "FindFieldDataAction";
            this.FindFieldDataAction.NullValuePrompt = null;
            this.FindFieldDataAction.ShortCaption = null;
            this.FindFieldDataAction.ToolTip = null;
            this.FindFieldDataAction.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.FindFieldDataAction_Execute);
            // 
            // FindFieldData
            // 
            this.Actions.Add(this.FindFieldDataAction);
            this.Activated += new System.EventHandler(this.FindFieldData_Activated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction FindFieldDataAction;
    }
}
