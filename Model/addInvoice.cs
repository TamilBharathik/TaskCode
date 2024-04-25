using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backendtask.Model
{
    public class Addinvoice
    {
        public Guid InvoiceNo { get; set;}
        public string? invoicePeriod { get; set; }
        public int DepartmentID { get; set;}
        public string? DepartmentName { get; set; }
        public string? TotalUSDAmount { get; set;}
    }
}