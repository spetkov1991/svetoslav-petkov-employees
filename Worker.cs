using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPeriod
{
    public class Worker
    {
        public int EmpID { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int WorkingDays
        {
            get { return (int)(DateTo - DateFrom).Value.TotalDays; }
        }
    }
}
