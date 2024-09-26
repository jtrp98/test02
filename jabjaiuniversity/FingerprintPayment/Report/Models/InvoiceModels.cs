using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class InvoiceModels
    {
        public string studentName { get; set; }

        public string studentCode { get; set; }
        public string invoiceDay { get; set; }
        internal DateTime day { get; set; }
        public string invoiceCode { get; set; }
        public string ClassName { get; set; }

        public string employeesName { get; set; }
        public List<IProduct> products { get; set; }
        public decimal? TotalMoney { get; set; }

        public class IProduct
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Amount { get; set; }
        }
    }
}