using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRMSMvc.Domain.Entities
{
    [Table("TrBatchDetails")]
    public class TrBatchDetails
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int TrBatchDetailsID { get; set; }

        [Display(Name = "Batch Name (*)")]
        public int TrBatchID { get; set; }
        public virtual TrBatch TrBatch { get; set; }

        [Display(Name = "Staff Name (*)")]
        public long StaffInfoID { get; set; }
        public virtual StaffInfo StaffInfo { get; set; }


    }
}
