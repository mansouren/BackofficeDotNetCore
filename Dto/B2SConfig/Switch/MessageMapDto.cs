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
    public class MessageMapDto : BaseDto<MessageMapDto,CfgMessageMap>
    {
        //public int ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? ChannelIdentifier { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int? Mti { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int? PrCode { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int? MappedMTI { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int? MappedPrCode { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public bool? IsAllowed { get; set; }

    }

   
}
