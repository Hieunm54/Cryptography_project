using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITemplate.Domain.Entities
{
    public class BlockData
    {
        public Guid BlockDataID { get; set; }
        public Guid BlockID { get; set; }
        public Guid DataUserID { get; set; }

    }
}
