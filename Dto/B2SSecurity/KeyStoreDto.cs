using B2SSecurity.Models;
using Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.B2SSecurity
{
    public class KeyStoreDto : BaseDto<KeyStoreDto,KeyStore>
    {
        public int? SwitchId { get; set; }
        public int KeyTypeId { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? Title { get; set; }

        [MaxLength(128, ErrorMessage = "CantBeMoreThan128Char")]
        public string KeyValue1 { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? CreatedBy { get; set; }

        public int EncKeyId { get; set; }
        public int? EncModel { get; set; }

        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string? KVC { get; set; }
    }
}
