using System;

namespace NetWorks.Core.Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public byte[] Timestamp { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime? DateDeleted { get; set; }

        public int? IsDeleted { get; set; }
    }
}