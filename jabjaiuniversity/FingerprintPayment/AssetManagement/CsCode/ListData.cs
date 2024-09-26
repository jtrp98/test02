using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
{
    public class ListData { }

    /// <summary>
    /// Category display columm list
    /// </summary>
    public class ListDataCategory
    {
        public string no { get; set; }
        //โค้ดประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Product display columm list
    /// </summary>
    public class ListDataProduct
    {
        public string no { get; set; }
        //โค้ดประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        //รหัสชนิดสินค้า
        public string Type { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //ชื่อหน่วยนับ
        public string Unit { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Get display columm list
    /// </summary>
    public class ListDataGet
    {
        public string no { get; set; }
        //วันที่
        public string DateStamp { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แหล่งที่มา
        public string Source { get; set; }
        //แผนก/หน่วยงาน
        public string Department { get; set; }
        //ผู้รับสินค้า
        public string Receiver { get; set; }
        public string action { get; set; }
        public string year { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Withdraw display columm list
    /// </summary>
    public class ListDataWithdraw
    {
        public string no { get; set; }
        //วันที่
        public string DateStamp { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ประเภทการทำรายการ
        public string TransType { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แผนก/หน่วยงาน
        public string Department { get; set; }
        //ผู้รับสินค้า
        public string Receiver { get; set; }
        public string action { get; set; }
        public string year { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Cutting display columm list
    /// </summary>
    public class ListDataCutting
    {
        public string no { get; set; }
        //วันที่
        public string DateStamp { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แผนก/หน่วยงาน
        public string Department { get; set; }
        //ผู้รับสินค้า
        public string Receiver { get; set; }
        public string action { get; set; }
        public string year { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Transfer display columm list
    /// </summary>
    public class ListDataTransfer
    {
        public string no { get; set; }
        //วันที่
        public string DateStamp { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แผนก/หน่วยงานที่โอน
        public string DepRequest { get; set; }
        //แผนก/หน่วยงานที่รับโอน
        public string DepTransfer { get; set; }
        public string action { get; set; }
        public string year { get; set; }
        public string id { get; set; }
    }

    /// 
    /// Report
    /// 

    /// <summary>
    /// Report 01 display columm list
    /// </summary>
    public class ListDataReport01
    {
        public string no { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แผนก/หน่วยงาน
        public string Department { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Report 02 display columm list
    /// </summary>
    public class ListDataReport02
    {
        public string no { get; set; }
        //รหัสประเภทครุภัณฑ์
        public string Code { get; set; }
        //ชื่อประเภทครุภัณฑ์
        public string Category { get; set; }
        //ชื่อสินค้า
        public string Product { get; set; }
        //จำนวน
        public int Amount { get; set; }
        //หน่วยนับ
        public string Unit { get; set; }
        //แผนก/หน่วยงาน
        public string Department { get; set; }
        //ผู้รับผิดชอบ
        public string ResponsibleBy { get; set; }
        //ผู้เบิก
        public string Receiver { get; set; }
        //เลขที่เอกสาร
        public string DocumentNo { get; set; }
        public string YearID { get; set; }
    }

}