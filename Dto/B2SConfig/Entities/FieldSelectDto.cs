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
    public class FieldSelectDto : BaseDto<FieldSelectDto,CfgFieldSelect>
    {
        //public int ID { get; set; }

        public long GroupID { get; set; }
        public int? Mti { get; set; }
        public int? PrCode { get; set; }

        [MaxLength(500, ErrorMessage ="CantBeMoreThan500Char")]
        public string FieldMap { get; set; }
        public bool AllowMac { get; set; }

        [MaxLength(50, ErrorMessage ="CantBeMoreThan50Char")]
        public string ChannelIdentifier { get; set; }

    }

   
}
