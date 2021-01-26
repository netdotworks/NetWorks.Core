using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetWorks.Core.Domain;

namespace NetWorks.Core.DataAccess
{
    public abstract class EntityTypeConfiguration<TEntity> : IMapper, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void ApplyConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(this);
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasQueryFilter(f => f.IsDeleted == 0);
            builder.Property(p => p.Timestamp).IsRowVersion();
        }
    }
}