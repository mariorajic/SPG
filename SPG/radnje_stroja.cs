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
    
    public partial class radnje_stroja
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public System.DateTime datum { get; set; }
        public string trosak { get; set; }
        public string profit { get; set; }
        public Nullable<int> id_stroja { get; set; }
        public Nullable<int> tip_radnje_stroja { get; set; }
    
        public virtual strojevi strojevi { get; set; }
        public virtual tip_radnje_stroja tip_radnje_stroja1 { get; set; }
    }
}