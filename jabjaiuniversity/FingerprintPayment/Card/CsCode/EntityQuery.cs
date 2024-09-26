using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Card.CsCode
{
    public class EntityQuery    {    }

    /// <summary>
    /// BackupCard entity query
    /// </summary>
    public class EntityBackupCardList
    {
        public System.Guid CardID { get; set; }
        public string CardName { get; set; }
        public string BarCode { get; set; }
        public string NFC { get; set; }
        public decimal? Money { get; set; }
        public System.Guid? CardHistoryID { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public DateTime? BorrowingDate { get; set; }
        public decimal? Insurance { get; set; }
        public int CountUse{ get; set; }
    }

    /// <summary>
    /// BackupCardHistory entity query
    /// </summary>
    public class EntityBackupCardHistoryList
    {
        public DateTime BorrowingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
    }

}