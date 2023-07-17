using System;
using System.Collections.Generic;

namespace taskui.Models
{
    public partial class Header
    {
        public Header()
        {
            Detail = new HashSet<Detail>();
        }

        public int HeaderId { get; set; }
        public string? ReleaseName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }

        public virtual ICollection<Detail> Detail { get; set; }
    }
}
