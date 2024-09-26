using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
{
    public class CollectionData<T>
    {
        public int draw { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
}