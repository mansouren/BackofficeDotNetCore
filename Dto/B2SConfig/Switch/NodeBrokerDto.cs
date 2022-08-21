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
    public class NodeBrokerDto : BaseDto<NodeBrokerDto,CfgNodeBroker>
    {
        //public int ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int NodeID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int BrokerID { get; set; }

        public bool EnableLog { get; set; }
        public bool EnableConsole { get; set; }
    }

    
}
