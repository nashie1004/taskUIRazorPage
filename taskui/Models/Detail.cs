using System;
using System.Collections.Generic;

namespace taskui.Models
{
    public partial class Detail
    {
        public int DetailId { get; set; }
        public int? HeaderId { get; set; }
        public short? Type { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual Header? Header { get; set; }

        // 1 - enhacnemnt
        // 2- bugs
    }
}
