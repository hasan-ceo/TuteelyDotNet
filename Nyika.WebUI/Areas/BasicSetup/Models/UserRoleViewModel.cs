using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Nyika.WebUI.Areas.BasicSetup.Models
{
    public class UserRoleViewModel
    {
        [Display(Name = "User Id")]
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

    }
}