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
    
    public partial class parcele
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parcele()
        {
            this.farme = new HashSet<farme>();
            this.oranice = new HashSet<oranice>();
            this.strojevi = new HashSet<strojevi>();
            this.troskovi = new HashSet<troskovi>();
        }
    
        public int id { get; set; }
        public int id_korisnika { get; set; }
        public string koordinate { get; set; }
        public string dimenzije { get; set; }
        public int id_grada { get; set; }
        public string lokacija { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<farme> farme { get; set; }
        public virtual gospodarstva gospodarstva { get; set; }
        public virtual gradovi gradovi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oranice> oranice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<strojevi> strojevi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<troskovi> troskovi { get; set; }
    }
}
