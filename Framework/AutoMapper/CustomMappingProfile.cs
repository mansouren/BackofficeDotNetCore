using AutoMapper;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.AutoMapper
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> customMappings)
        {
            foreach (var item in customMappings)
            {
                item.CreateMapping(this);
            }
        }
    }
}
