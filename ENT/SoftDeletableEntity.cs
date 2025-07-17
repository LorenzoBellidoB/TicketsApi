using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public abstract class SoftDeletableEntity
    {
        public DateTime DeletedAt { get; set; } = DateTime.Parse("1111-01-01T00:00:00Z");
    }

}
