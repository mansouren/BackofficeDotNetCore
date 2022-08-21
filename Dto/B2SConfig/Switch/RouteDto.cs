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
    public class RouteDto :BaseDto<RouteDto,CfgRoute>
    {
        //public int ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int NodeID { get; set; }

        public int? SourceChannelID { get; set; }
        public int? DestConnector { get; set; }
        public int? ServiceBrokerID { get; set; }
        public int? ManagerID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? IIN { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? FromPan { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? ToPan { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? FromTerm { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? ToTerm { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Mti { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? PrCode { get; set; }

        public int? PosCondition { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int Priority { get; set; }

        public int? SourceConnector { get; set; }
        public int? GroupId { get; set; }
        public int? CardGroupId { get; set; }
    }
}
