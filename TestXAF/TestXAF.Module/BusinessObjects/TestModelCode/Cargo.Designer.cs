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
namespace TestXAF.Module.BusinessObjects.TestWork
{

    public partial class Cargo : XPLiteObject
    {
        int fNumber;
        [Key(true)]
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>("Number", ref fNumber, value); }
        }
        string fName;
        [Size(20)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        double fWeight;
        public double Weight
        {
            get { return fWeight; }
            set { SetPropertyValue<double>("Weight", ref fWeight, value); }
        }
        Area fNumberArea;
        [Association(@"CargoReferencesArea")]
        public Area NumberArea
        {
            get { return fNumberArea; }
            set { SetPropertyValue<Area>("NumberArea", ref fNumberArea, value); }
        }
        DateTime fCreate_Cargo;
        public DateTime Create_Cargo
        {
            get { return fCreate_Cargo; }
            set { SetPropertyValue<DateTime>("Create_Cargo", ref fCreate_Cargo, value); }
        }
        DateTime fDelete_Cargo;
        public DateTime Delete_Cargo
        {
            get { return fDelete_Cargo; }
            set { SetPropertyValue<DateTime>("Delete_Cargo", ref fDelete_Cargo, value); }
        }
    }

}
