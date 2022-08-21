using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig
{
    public class FITDto : BaseDto<FITDto,CfgFIT>
    {
        [MaxLength(50, ErrorMessage ="CantBeMoreThan50Char")]
        public string FITName { get; set; }   
    }
}
