using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Channel
{
    public class ChannelKeyDto : BaseDto<ChannelKeyDto, CfgChannelKey>
    {
        //public int ID { get; set; }
        public int ChannelID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Title { get; set; }
        public int? KeyIndex { get; set; }
        public int? KeyUsage { get; set; }
        public long KeyEntryID { get; set; }
        public long? OldKeyEntryID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }


}
