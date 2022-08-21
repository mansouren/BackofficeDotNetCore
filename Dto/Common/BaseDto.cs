using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Common
{
    public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
         where TDto : class, new()
         where TEntity : class, new()
    {
        public TKey ID { get; set; }

        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
        }

        public TEntity ToEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map(CastToDerivedClass(mapper, this), entity);
        }

        public static TDto FromEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map<TDto>(entity);
        }

        public TDto EntityToDTO(IMapper mapper, TEntity entity)
        {
            return mapper.Map<TDto>(entity);
        }

        protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity, TKey> baseDto)
        {
            return mapper.Map<TDto>(baseDto);
        }
        public void CreateMapping(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();
            var entityType = typeof(TEntity);
            var dtoType = typeof(TDto);

            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMapping(mappingExpression.ReverseMap());
        }

        //If Any Dto has Custom Mapping ,We can override this method to add our custommapping to Automapper
        public virtual void CustomMapping(IMappingExpression<TEntity, TDto> mappingExpression)
        {

        }
    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
       where TDto : class, new()
       where TEntity : class, new()
    {

    }
}
