
   
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobileDevice.DTO
{
    public class AccountDTO
    {
        public int ID_Account { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [StringLength(255)]
        public string UserName { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(255)]
        public string ConfirmPassword { get; set; }
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address { get; set; }

        public List<PermissionDetailDTO> PermissionDetailDTOs { get; set; }

    }
}