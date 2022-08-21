using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Switch
{
    public class SwitchDto : BaseDto<SwitchDto,CfgSwitch,long>
    {
        //public long ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int SwitchCode { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string SwitchName { get; set; }

        public long? SwitchAccountID { get; set; }
        public int? TraceEntityID { get; set; }
        public int? TerminalEntityID { get; set; }
        public int? MerchantEntityID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int NodeType { get; set; }

        [MaxLength(250, ErrorMessage = "CantBeMoreThan250Char")]
        public string? WatchOutAddress { get; set; }

        public bool IssuerMonitorSupport { get; set; }
        public bool RemoteAccessSupport { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? RemoteAccessPassword { get; set; }

        public bool ATMSupport { get; set; }
        public bool IVRSupport { get; set; }
        public bool POSSupport { get; set; }
        public bool KIOSKSupport { get; set; }
        public bool MobileSupport { get; set; }
        public bool NETSupport { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int SettlementModel { get; set; }

        public int? TerminalKeyID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? IIN { get; set; }

        public int? IssuerFITTableID { get; set; }


    }
}
