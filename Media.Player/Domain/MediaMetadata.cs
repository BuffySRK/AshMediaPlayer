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
        [MaxLength(500)]
        public string Title { get; set; }
    }
}
