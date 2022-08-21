using B2SMain.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SMain
{
    public class RoleDto : BaseDto<RoleDto,Role>
    {
        [Required(ErrorMessage = "TitleIsRequired")]
        public string Title { get; set; }


        [Required(ErrorMessage = "PermissionsIsRequired")]
        public List<int> PrivilegeIds { get; set; }
        public bool HasChangeInPrivileges { get; set; } = false;
    }
}
