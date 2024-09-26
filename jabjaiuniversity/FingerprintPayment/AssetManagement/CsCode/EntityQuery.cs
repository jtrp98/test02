using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
{
    public class EntityQuery { }

    /// <summary>
    /// Category entity query
    /// </summary>
    public class EntityCategory
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
    }

    /// <summary>
    /// Product entity query
    /// </summary>
    public class EntityProduct
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Product { get; set; }
        public string Unit { get; set; }
    }

    /// <summary>
    /// Get entity query
    /// </summary>
    public class EntityGet
    {
        public int Year { get; set; }
        public int ID { get; set; }
        public DateTime? DateStamp { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Source { get; set; }
        public string Department { get; set; }
        public string Receiver { get; set; }
    }

    /// <summary>
    /// Withdraw entity query
    /// </summary>
    public class EntityWithdraw
    {
        public int Year { get; set; }
        public int ID { get; set; }
        public DateTime? DateStamp { get; set; }
        public string Code { get; set; }
        public string TransType { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Department { get; set; }
        public string Receiver { get; set; }
    }

    /// <summary>
    /// Cutting entity query
    /// </summary>
    public class EntityCutting
    {
        public int Year { get; set; }
        public int ID { get; set; }
        public DateTime? DateStamp { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Department { get; set; }
        public string Receiver { get; set; }
    }

    /// <summary>
    /// Transfer entity query
    /// </summary>
    public class EntityTransfer
    {
        public int Year { get; set; }
        public int ID { get; set; }
        public DateTime? DateStamp { get; set; }
        public string Code { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string DepRequest { get; set; }
        public string DepTransfer { get; set; }
    }

    /// 
    /// Report
    /// 

    /// <summary>
    /// Report 01 entity query
    /// </summary>
    public class EntityReport01
    {
        public int ProdID { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Department { get; set; }
    }

    /// <summary>
    /// Report 02 entity query
    /// </summary>
    public class EntityReport02
    {
        public string YearID { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Department { get; set; }
        public string ResponsibleBy { get; set; }
        public string Receiver { get; set; }
        public string DocumentNo { get; set; }
    }

}