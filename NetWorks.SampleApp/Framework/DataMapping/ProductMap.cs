using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetWorks.Core.DataAccess;
using NetWorks.SampleApp.Framework.Domain;

namespace NetWorks.SampleApp.Framework.DataMapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            base.Configure(builder);
        }
    }
}