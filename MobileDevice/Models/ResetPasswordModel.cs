using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileDevice.Models
{
    public class ResetPasswordModel
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống!")]
        
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không trùng khớp !")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}