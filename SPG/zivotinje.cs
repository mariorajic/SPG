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
    using System.ComponentModel.DataAnnotations;

    public partial class zivotinje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public zivotinje()
        {
            this.sirovine = new HashSet<sirovine>();
        }
    
        public int id { get; set; }
        public int id_farme { get; set; }
        [Required]
        public string vrsta { get; set; }
        [Required]
        public int broj_taga { get; set; }
        [Required]
        public int kolicina { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public string uzrok_smrti { get; set; }
    
        public virtual farme farme { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sirovine> sirovine { get; set; }
    }
}
