﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace TestXafSolution2.Module.TestWork2
{

    public partial class Area : XPLiteObject
    {
        int fNumber;
        [Key(true)]
        [Browsable(false)]
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>("Number", ref fNumber, value); }
        }
        string fName;
        [Size(10)]
        [DevExpress.Persistent.Validation.RuleRequiredField, DevExpress.Persistent.Validation.RuleUniqueValue("", DevExpress.Persistent.Validation.DefaultContexts.Save,
   CriteriaEvaluationBehavior = DevExpress.Persistent.Validation.CriteriaEvaluationBehavior.BeforeTransaction)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        DateTime fCreate_Area;
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0: ddd, dd MMMM yyyy hh:mm:ss tt}"), DevExpress.ExpressApp.Model.ModelDefault("EditMask", "ddd, dd MMMM yyyy hh:mm:ss tt"), DevExpress.ExpressApp.Model.ModelDefault("PropertyEditorType", "WinSolution.Module.Win.MyDatePropertyEditor")]
        public DateTime Create_Area
        {
            get { return fCreate_Area; }
            set { SetPropertyValue<DateTime>("Create_Area", ref fCreate_Area, value); }
        }
        DateTime fDelete_Area;
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0: ddd, dd MMMM yyyy hh:mm:ss tt}"), DevExpress.ExpressApp.Model.ModelDefault("EditMask", "ddd, dd MMMM yyyy hh:mm:ss tt"), DevExpress.ExpressApp.Model.ModelDefault("PropertyEditorType", "WinSolution.Module.Win.MyDatePropertyEditor")]
        public DateTime Delete_Area
        {
            get { return fDelete_Area; }
            set { SetPropertyValue<DateTime>("Delete_Area", ref fDelete_Area, value); }
        }
        [Association(@"CargoReferencesArea")]
        public XPCollection<Cargo> Cargoes { get { return GetCollection<Cargo>("Cargoes"); } }
        [Association(@"PicketReferencesArea")]
        public XPCollection<Picket> Pickets { get { return GetCollection<Picket>("Pickets"); } }
    }

}
