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
    public class LoginDto : BaseDto<LoginDto, User>
    {
        [Required(ErrorMessage = "UserNameIsRequired")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PasswordIsRequired")]
        public string Password { get; set; }
    }
    public class UserJwtDto : BaseDto<UserJwtDto, User>
    {
        public string UserName { get; set; }
        public List<UserRole> UserRoles { get; set; }

    }
    public class UserDto : BaseDto<UserDto, User>
    {
        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "MaxLengthCantBeMoreThan50Character")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "MaxLengthCantBeMoreThan50Character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        [MaxLength(50, ErrorMessage = "MaxLengthCantBeMoreThan50Character")]
        public string FullName { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLengthCantBeMoreThan50Character")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "MaxLengthCantBeMoreThan500Character")]
        public string? Key { get; set; }

        public bool Status { get; set; }
        public int? DefaultTownId { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsManager { get; set; }

        [MaxLength(50, ErrorMessage = "MaxLengthCantBeMoreThan50Character")]
        public string? Mac { get; set; }

        public int? Action { get; set; }

        [Required(ErrorMessage = "FieldIsRequired")]
        public DateTime ExpirationPassDate { get; set; }
        
        public bool IsChangedByUser { get; set; }

        public List<int> RoleIds { get; set; }
        public bool HasChangeInPermissions { get; set; }

    }
}
