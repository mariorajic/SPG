﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<berbe> berbe { get; set; }
        public virtual DbSet<biljke> biljke { get; set; }
        public virtual DbSet<farme> farme { get; set; }
        public virtual DbSet<gospodarstva> gospodarstva { get; set; }
        public virtual DbSet<gradovi> gradovi { get; set; }
        public virtual DbSet<mljekomati> mljekomati { get; set; }
        public virtual DbSet<oranice> oranice { get; set; }
        public virtual DbSet<parcele> parcele { get; set; }
        public virtual DbSet<prodaje_biljaka> prodaje_biljaka { get; set; }
        public virtual DbSet<prodaje_sirovina> prodaje_sirovina { get; set; }
        public virtual DbSet<punjenje_mljekomata> punjenje_mljekomata { get; set; }
        public virtual DbSet<radnje_stroja> radnje_stroja { get; set; }
        public virtual DbSet<sadnje> sadnje { get; set; }
        public virtual DbSet<servisi_mljekomata> servisi_mljekomata { get; set; }
        public virtual DbSet<sezone> sezone { get; set; }
        public virtual DbSet<sirovine> sirovine { get; set; }
        public virtual DbSet<stanje_tla> stanje_tla { get; set; }
        public virtual DbSet<strojevi> strojevi { get; set; }
        public virtual DbSet<tip_radnje_stroja> tip_radnje_stroja { get; set; }
        public virtual DbSet<troskovi> troskovi { get; set; }
        public virtual DbSet<vrste_tla> vrste_tla { get; set; }
        public virtual DbSet<zadruge> zadruge { get; set; }
        public virtual DbSet<zivotinje> zivotinje { get; set; }
    }
}
