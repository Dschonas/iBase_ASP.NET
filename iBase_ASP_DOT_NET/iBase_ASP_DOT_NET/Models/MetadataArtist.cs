using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iBase_ASP_DOT_NET.Models
{
    [MetadataType(typeof(MetadataArtist))]
    public partial class ArtistTable
    {
    }

    public class MetadataArtist
    {
        [Display(Name = "ID")]
        [Required]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public string Genre { get; set; }

        [Display(Name = "Type")]
        [Required]
        public string Type { get; set; }
    }
}