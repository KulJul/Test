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
namespace TestXafSolution.Module.TestWork2
{

    public partial class Picket : XPLiteObject
    {
        int fNumber;
        [Key]
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>("Number", ref fNumber, value); }
        }
        string fName;
        [Size(10)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }
        Store fNumberStore;
        [Association(@"PicketReferencesStore")]
        public Store NumberStore
        {
            get { return fNumberStore; }
            set { SetPropertyValue<Store>("NumberStore", ref fNumberStore, value); }
        }
        Area fNumberArea;
        [Association(@"PicketReferencesArea")]
        public Area NumberArea
        {
            get { return fNumberArea; }
            set { SetPropertyValue<Area>("NumberArea", ref fNumberArea, value); }
        }
    }

}
