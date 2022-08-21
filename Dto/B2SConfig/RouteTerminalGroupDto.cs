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
    public class RouteTerminalGroupDto : BaseDto<RouteTerminalGroupDto, CfgRouteTerminalGroup, long>
    {
        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string TerminalNo { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public long GroupID { get; set; }
    }
}
