using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public abstract class SoftDeletableEntity
    {
        public static readonly DateTime NotDeleted = DateTime.SpecifyKind(
            new DateTime(1111, 1, 1, 0, 0, 0), DateTimeKind.Utc
        );
        [Column("deleted_at")]
        public DateTime DeletedAt { get; set; } = NotDeleted;
    }

}
