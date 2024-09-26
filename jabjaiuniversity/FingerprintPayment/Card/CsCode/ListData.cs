using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Card.CsCode
{
    public class ListData { }

    /// <summary>
    /// BackupCard display columm list
    /// </summary>
    public class ListDataBackupCard
    {
        public string no { get; set; }
        //รหัสบัตรสำรอง
        public string CardID { get; set; }
        //ชื่อบัตรสำรอง
        public string CardName { get; set; }
        //Barcode
        public string BarCode { get; set; }
        //NFC
        public string NFC { get; set; }
        //จำนวนเงินในบัตร
        public string Money { get; set; }
        //รหัสประวัติการใช้บัตร
        public string CardHistoryID { get; set; }
        //ประเภท
        public string UserType { get; set; }
        //ชื่อ-นามสกุล
        public string UserName { get; set; }
        //วันที่ยืม
        public string BorrowingDate { get; set; }
        public string action { get; set; }
        public string cid { get; set; }
        public string chid { get; set; }
        public string insurance { get; set; }
        public bool IsRemove { get; internal set; }
    }
    /// <summary>
    /// BackupCardHistory display columm list
    /// </summary>
    public class ListDataBackupCardHistory
    {
        public string no { get; set; }
        //วันที่ยืม
        public string BorrowingDate { get; set; }
        //วันที่คืน
        public string ReturnDate { get; set; }
        //ประเภท
        public string UserType { get; set; }
        //ชื่อ-นามสกุล
        public string UserName { get; set; }
    }

}