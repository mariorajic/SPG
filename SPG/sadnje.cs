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

    public partial class sadnje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sadnje()
        {
            this.berbe = new HashSet<berbe>();
        }
    
        public int id { get; set; }
        public Nullable<int> biljka { get; set; }
        public Nullable<int> id_oranice { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime datum { get; set; }
        [Required]
        public string kolicina { get; set; }
        [Required]
        public string troskovi { get; set; }
        public Nullable<int> sezona { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<berbe> berbe { get; set; }
        public virtual biljke biljke { get; set; }
        public virtual oranice oranice { get; set; }
        public virtual sezone sezone { get; set; }
    }
}
