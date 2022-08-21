using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Node
{
    public class NodeDto : BaseDto<NodeDto,CfgNode>
    {
        //public int ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage ="CantBeMoreThan50Char")]
        public string NodeName { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string MonitoringIPAddress { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int MonitoringPort { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool HsmEnabled { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int PrSwitchCode { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int AcqSwitchCode { get; set; }
        public int? IssSwitchCode { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string CommandIPAddress { get; set; }

        public int? CommandPort { get; set; }

        [MaxLength(400, ErrorMessage = "CantBeMoreThan400Char")]
        public string BaseLogAddress { get; set; }
        public int? IssuerFITTableID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool TopupEnabled { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string TestTerminals { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int MaxThreadPool { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool MultiWorker { get; set; }
        public int? ZoneId { get; set; }


        [Required(ErrorMessage = "FieldIsRequired")]
        public int NodeId { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool RemoteUpdateEnabled { get; set; }
        public decimal? CurrentVersion { get; set; }


        [Required(ErrorMessage = "FieldIsRequired")]
        public bool UpgrageRequired { get; set; }

        public int? NextState { get; set; }
        public DateTime? LastUpgradeOn { get; set; }
        public DateTime? LastChangeStateOn { get; set; }
        public string? ExpceptionLogPath { get; set; }
        public string? BinaryAddress { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool IsNative { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public string? Cmd { get; set; }
        public int? PID { get; set; }

    }




}
