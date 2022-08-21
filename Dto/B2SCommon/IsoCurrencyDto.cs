using B2SCommon.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SCommon
{
    public class IsoCurrencyDto : BaseDto<IsoCurrencyDto,ISOCurrency>
    {
        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage ="CantBeMoreThan50Char")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int SeparatorPoints { get; set; }

    }
}
