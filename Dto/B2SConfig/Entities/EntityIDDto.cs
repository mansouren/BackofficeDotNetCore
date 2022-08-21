using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Entities
{
    public class EntityIDDto : BaseDto<EntityIDDto,CfgEntityID>
    {
       // public int ID { get; set; }
        public long LastID { get; set; }
        public int IncrementCounter { get; set; }
        public long DefaultSeed { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int Id1 { get; set; }

    }

    
}
