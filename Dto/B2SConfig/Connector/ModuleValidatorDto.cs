using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Connector
{
    public class ModuleValidatorDto : BaseDto<ModuleValidatorDto,CfgModule_Validator>
    {
        //public int ID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string AssemblyName { get; set; }

        [MaxLength(150, ErrorMessage = "CantBeMoreThan150Char")]
        public string ClassName { get; set; }

        public int ModuleType { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string Description { get; set; }

        public int Direction { get; set; }
        public int ValidatorGroupID { get; set; }
        public int Priority { get; set; }

    }

   
}
