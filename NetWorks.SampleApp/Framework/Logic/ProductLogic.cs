using NetWorks.Core.DataAccess.Repositories;
using NetWorks.Core.Logic;
using NetWorks.SampleApp.Framework.Domain;

namespace NetWorks.SampleApp.Framework.Logic
{
    public class ProductLogic : DatabaseLogic<Product>, IProductLogic
    {
        public ProductLogic(IRepo<Product> repo) : base(repo)
        {
        }
    }
}