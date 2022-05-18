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
        
        
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

       
        
        [StringLength(255)]
        [DataType(DataType.Password)]
        
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}