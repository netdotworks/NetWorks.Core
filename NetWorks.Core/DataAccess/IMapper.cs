using Microsoft.EntityFrameworkCore;

namespace NetWorks.Core.DataAccess
{
    public interface IMapper
    {
        void ApplyConfiguration(ModelBuilder builder);
    }
}