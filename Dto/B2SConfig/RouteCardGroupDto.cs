using B2SConfig.Models;
using Dto.Common;
using System.ComponentModel.DataAnnotations;


namespace Dto.B2SConfig
{
    public class RouteCardGroupDto : BaseDto<RouteCardGroupDto, CfgRouteCardGroup,long>
    {
        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "CantBeMoreThan50Char")]
        public string Pan { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public long GroupID { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }

        [MaxLength(100, ErrorMessage = "CantBeMoreThan100Char")]
        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [MaxLength(100, ErrorMessage = "CantBeMoreThan100Char")]
        public string? ModifiedBy { get; set; }

    }
}
