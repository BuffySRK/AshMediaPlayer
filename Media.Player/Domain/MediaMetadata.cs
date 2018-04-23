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
        [DataType(DataType.Url)]
        public string MediaUrl { get; set; }

        [Required]
        [StringLength(500, MinimumLength=3,ErrorMessage ="Filename must be between 3 and 500 characters")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string Information { get; set; }

        [DataType(DataType.ImageUrl)]
        public string MediaArtUrl { get; set; }

        [DataType(DataType.Text)]
        public string MediaExtension { get; set; }
    }
}
