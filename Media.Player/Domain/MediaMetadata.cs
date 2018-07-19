using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Media.Player.Domain
{
    public class MediaMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediaMetadataId { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        [Required]
        public string MediaExtension { get; set; }

        [Required]
        [MaxLength(80)]
        public string FileName{ get; set; }
    }
}
