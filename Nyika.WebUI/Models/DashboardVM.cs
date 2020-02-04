using Nyika.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nyika.WebUI.Models
{
    public class DashboardVM
    {
        public List<CashSummary> CashSummary { get; set; }
        public bool DayClose { get; set; }
        public string WorkDate { get; set; }
        public string ExpireDate { get; set; }
    }
}