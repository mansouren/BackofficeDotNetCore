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
    public class ConnectorBrokerDto : BaseDto<ConnectorBrokerDto,CfgConnectorBroker>
    {
        //public int ID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Description { get; set; }
        public int NodeID { get; set; }
        public int ChannelID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? ConnectorName { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? RemoteIP { get; set; }

        public int? RemotePort { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? LocalIP { get; set; }

        [MaxLength(10, ErrorMessage = "CantBeMoreThan10Char")]
        public string? LocalPort { get; set; }

        public int? ConnectionType { get; set; }
        public int? PosCondition { get; set; }
        public int HeaderLength { get; set; }
        public int TrailerLength { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Header { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Trailer { get; set; }

        public int? NotifierID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? NotifierName { get; set; }

        public int? BrokerID { get; set; }

        public int? Timeout { get; set; }

        public int? SecurityModel { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? PublicKey { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? PrivateKey { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? PKCS12 { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Passshare { get; set; }

        public int Mode { get; set; }
        public bool DualPort { get; set; }
        public bool IsActive { get; set; }
        public int ChannelModuleID { get; set; }
        public int PackagerModuleID { get; set; }
        public bool EnableLog { get; set; }
        public bool ConsoleLog { get; set; }
        public int SourceSwitchCode { get; set; }
        public int? ValidatorGroupID { get; set; }
        public int ModuleID { get; set; }
        public int LenType { get; set; }
        public int CoreID { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsPriority { get; set; }
        public bool SecurityModuleEnabled { get; set; }
        public bool? AdvancedMessagingEnable { get; set; }
        public int? ReplyHeaderLength { get; set; }

    }
}
