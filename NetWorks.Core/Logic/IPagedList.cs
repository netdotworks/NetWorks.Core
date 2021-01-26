using System.Collections.Generic;

namespace NetWorks.Core.Logic
{
    public interface IPagedList<T> : IList<T>, IPagedData
    {
    }
}