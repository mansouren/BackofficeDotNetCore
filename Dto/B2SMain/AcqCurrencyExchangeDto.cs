using B2SMain.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SMain
{
    public class AcqCurrencyExchangeDto : BaseDto<AcqCurrencyExchangeDto, AcqCurrencyExchange, long>
    {
        [Required(ErrorMessage = "FieldIsRequired")]
        public long SourceCurrencyId { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public long DestCurrencyId { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public decimal ConstAmount { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public decimal Percent { get; set; }

        public long? TxnFeeId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? ReverseExchangeRate { get; set; }
    }
}
