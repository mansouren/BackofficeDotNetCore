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
    public class ModuleTPDto : BaseDto<ModuleTPDto,CfgModule_TP>
    {
        //public int ID { get; set; }

        public long? ServiceBrokerID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string AssemblyName { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(150, ErrorMessage = "CantBeMoreThan150Char")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int ModuleType { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string Description { get; set; }

        public bool EnableReversal { get; set; }
        public bool IsFinancial { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? MTI { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? PrCode { get; set; }

        public bool EnableLog { get; set; }
        public bool EnableConsole { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int TxnType { get; set; }

        public bool MappingRequired { get; set; }
        public bool IsAllowed { get; set; }
        public int? DestFunctionCode { get; set; }

    }

  
}
