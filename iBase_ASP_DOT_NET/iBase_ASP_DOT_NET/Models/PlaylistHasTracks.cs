//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iBase_ASP_DOT_NET.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlaylistHasTracks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlaylistHasTracks()
        {
            this.PlaylistTable = new HashSet<PlaylistTable>();
        }
    
        public int Id { get; set; }
        public string TrackId { get; set; }
        public Nullable<int> PlaylistId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaylistTable> PlaylistTable { get; set; }
        public virtual PlaylistTable PlaylistTable1 { get; set; }
        public virtual TrackTable TrackTable { get; set; }
    }
}
