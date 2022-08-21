using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SConfig.Channel
{
    public class ChannelBatchDto : BaseDto<ChannelBatchDto,CfgChannelBatch,long>
    {
        //public long ID { get; set; }
        
        [Required(ErrorMessage = "FieldIsRequired")]
        public int BatchMonth { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int BatchDay { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int InterfaceID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? TraceNo { get; set; }
        public int? TrTraceNo { get; set; }
        public DateTime? ServerDatetime { get; set; }
        public bool IsActive { get; set; }
    }

    
}
