using B2SConfig.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Dto.B2SConfig.Tasks
{
    

    public class TaskDto : BaseDto<TaskDto, CfgTask>
    {
        //public int ID { get; set; }
        public int NodeID { get; set; }

        [MaxLength(100, ErrorMessage = "CantBeMoreThan100Char")]
        public string Title { get; set; }

        public int TaskModuleID { get; set; }
        public DateTime Runat { get; set; }
        public long? MaxDuration { get; set; }
        public long? PeriodMs { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param1 { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param2 { get; set; }


        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param3 { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param4 { get; set; }


        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param5 { get; set; }


        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Param6 { get; set; }

        public DateTime CreatedOn { get; set; }
        public int TaskID { get; set; }
        public DateTime NextRunTime { get; set; }
        public bool IsRunning { get; set; }
        public bool? LastTaskResult { get; set; }
        public string? LastTaskResultDescription { get; set; }

    }

   
}
