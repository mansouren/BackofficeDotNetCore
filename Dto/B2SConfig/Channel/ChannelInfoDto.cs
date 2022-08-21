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
    public class ChannelInfoDto : BaseDto<ChannelInfoDto,CfgChannelInfo,long>
    {
        //public long ID { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int SwitchCode { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string Identifier { get; set; }
        public long? ZMKEntryID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? IIN { get; set; }
        public int? KeyCount { get; set; }
        public long? InterchangeFeeTableID { get; set; }
        public long? RRNSeedID { get; set; }
        public long? FITID { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public DateTime CreatedOn { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int SecurityType { get; set; }
        
        [Required(ErrorMessage = "FieldIsRequired")]
        public int EncyptionMethodType { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int MacMethodType { get; set; }
       
        [Required(ErrorMessage = "FieldIsRequired")] 
        public int MacFormat { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public int ChannelType { get; set; }
        public int? DefaultCurrency { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? SupportedCurrencies { get; set; }
        public int? CardExchnageTableId { get; set; }
        public int? SettlementExchangeTableId { get; set; }
        public int? TransactionExchangeTableId { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? SupportedSettlementCurrencies { get; set; }
        public bool IsSettlementSupported { get; set; }
        public long? ChannelBankAccountId { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? SupportedCardHolderCurrencies { get; set; }

        [MaxLength(500, ErrorMessage = "CantBeMoreThan500Char")]
        public string? SupportedTxnTypes { get; set; }
    }

   
}
