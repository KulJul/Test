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

    public partial class Picket : XPLiteObject
    {
        string fNumber;
        [Key]
        [Size(4)]
        public string Number
        {
            get { return fNumber; }
            set { SetPropertyValue<string>("Number", ref fNumber, value); }
        }
        string fName;
        [Size(10)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
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
