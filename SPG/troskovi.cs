//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPG
{
    using System;
    using System.Collections.Generic;
    
    public partial class troskovi
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public System.DateTime datum { get; set; }
        public int trosak { get; set; }
        public int id_parcele { get; set; }
    
        public virtual parcele parcele { get; set; }
    }
}