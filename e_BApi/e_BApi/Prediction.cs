//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace e_BApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prediction
    {
        public int Id { get; set; }
        public int ExpID { get; set; }
        public int DateID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    
        public virtual Date Date { get; set; }
        public virtual Exporter Exporter { get; set; }
    }
}
