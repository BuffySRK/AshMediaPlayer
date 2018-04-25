using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Media.Player.Models
{
    public class MediaModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MediaUrl { get; set; }
        public string Information { get; set; }
        public string MediaArtUrl { get; set; }

        public IList<string> TrackList { get; set; }
    }
}
