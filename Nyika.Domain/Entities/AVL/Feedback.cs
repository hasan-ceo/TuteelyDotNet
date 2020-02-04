using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nyika.Domain.Entities.AVL
{
    public class Feedback
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public long FeedbackID { get; set; }

        [Display(Name = "Email address")]
        [MaxLength(50)]
        [DefaultValue("")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [MaxLength(512)]
        [DefaultValue("")]
        [Display(Name = "Select Topic")]
        public string TopicName { get; set; }

        [MaxLength(512)]
        [DataType(DataType.MultilineText)]
        [DefaultValue("")]
        [Display(Name = "Details of error")]
        public string Detailsoferror { get; set; }

        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "What did you like about this service?")]
        public string WebsiteLike { get; set; }

        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "What don't you like about this service?")]
        public string WebsiteNotLike { get; set; }

        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Do you have any suggestions for improving this service?")]
        public string Suggestions { get; set; }

        [MaxLength(512)]
        [DefaultValue("")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Do you have any other questions or comments about this service?")]
        public string Comments { get; set; }

        [MaxLength(30)]
        [DefaultValue("")]
        [Display(Name = "Do you visit this service for business purposes, private purposes or both?")]
        public string Purposes { get; set; }

        [MaxLength(30)]
        [DefaultValue("")]
        [Display(Name = " Which of the following best describes you?")]
        public string UserType { get; set; }

        [MaxLength(20)]
        [DefaultValue("")]
        [Display(Name = "How often do you use this service?")]
        public string VisitFrequency { get; set; }

        [Display(Name = "Entry Date(*)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntryDate { get; set; }

    }
}
