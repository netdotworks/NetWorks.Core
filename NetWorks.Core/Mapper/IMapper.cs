using System.Collections.Generic;

namespace NetWorks.Core.Mapper
{
    public interface IMapper<TModel, TDto>
    {
        TModel Map(TDto dto);

        IList<TModel> Map(IList<TDto> dtos);

        TDto Map(TModel model);

        TDto Map(TModel model, TDto dto);

        IList<TDto> Map(IList<TModel> models);
    }
}