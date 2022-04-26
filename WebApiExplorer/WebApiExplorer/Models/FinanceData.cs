using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiExplorer.Models
{
    public class FinanceData
    {
        public DateTime X { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
    }
}