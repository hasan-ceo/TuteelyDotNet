using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace Nyika.WebUI.Areas.AVL.Models
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

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Registration Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "InstanceID")]
        public string InstanceID { get; set; }

    }
}