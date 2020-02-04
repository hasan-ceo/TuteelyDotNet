using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Nyika.Domain.Entities.Setup;
using System.Web;

namespace Nyika.WebUI.Areas.BasicSetup.Models
{
    public class CompanyVM
    {
        public Company company { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

    }
}