using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace NetWorks.Core.Mapper
{
    public abstract class Mapper<TModel, TDto> : IMapper<TModel, TDto>
    {
        protected IMapper _mapper;

        public TModel Map(TDto dto)
        {
            return _mapper.Map<TModel>(dto);
        }

        public IList<TModel> Map(IList<TDto> dtos)
        {
            return dtos?.Select(Map).ToList();
        }

        public TDto Map(TModel model)
        {
            return _mapper.Map<TDto>(model);
        }

        public IList<TDto> Map(IList<TModel> models)
        {
            return models?.Select(Map).ToList();
        }

        public TDto Map(TModel model, TDto dto)
        {
            return _mapper.Map<TModel, TDto>(model, dto);
        }
    }
}