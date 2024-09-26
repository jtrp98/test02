using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
{
    public class GlobalFunction
    {
        public GlobalFunction() { }

        public enum AssetTransactionType
        {
            GET = 1,
            WITHDRAW = 2,
            BORROW = 3,
            CUTTING = 4,
            TRANSFER = 5,
        }
        public enum AssetAction
        {
            INSERT,
            UPDATE,
            DELETE
        }
        // AssetAction.UPDATE-UD : Is update with unit change or department change

        public static void RecordTransaction(int transType, int transID, int? prodID, int? depID, int? oldDepID, int? amount, int? oldAmount, int? unitID, int? oldUnitID, int action)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {

                int empID = Convert.ToInt32(HttpContext.Current.Session["sEmpID"]);

                int Year = 0;
                int ID = 0;

                TAssetTransaction lastTrans = null;
                int lastTransID = 0;

                string actionName = Enum.GetName(typeof(AssetAction), action);
                string transTypeName = Enum.GetName(typeof(AssetTransactionType), transType);

                int? balance = 0;
                bool unitDepChange = false;

                int? multiplier = 1;
                switch (transTypeName)
                {
                    case "GET": multiplier = 1; break;
                    case "WITHDRAW":
                    case "BORROW":
                    case "CUTTING":
                    case "TRANSFER": multiplier = -1; break;
                }

                #region When update with unit not same or department not same

                if ((unitID != oldUnitID && oldUnitID != null) || (depID != oldDepID && oldDepID != null))
                {
                    Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                    ID = (int)(en.TAssetTransactions.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TAssetTransactions.Where(w => w.Year == Year).Max(m => m.AssetTransactionId) + 1);

                    var lastRowForProductOldUnit = en.TAssetTransactions.Where(w => w.Year == Year && w.DepID == oldDepID && w.AssetProductId == prodID && w.UnitID == oldUnitID);
                    if (lastRowForProductOldUnit.Count() != 0)
                    {
                        lastTransID = lastRowForProductOldUnit.Max(x => x.AssetTransactionId);
                        lastTrans = en.TAssetTransactions.Where(w => w.Year == Year && w.AssetTransactionId == lastTransID).FirstOrDefault();

                        balance = lastTrans.Balance;
                    }

                    balance -= (multiplier * oldAmount);

                    try
                    {
                        TAssetTransaction p = new TAssetTransaction
                        {
                            Year = Year,
                            AssetTransactionId = ID,
                            TransactionType = transType,
                            TransactionID = transID,
                            AssetProductId = prodID,
                            DepID = oldDepID,
                            Amount = (multiplier * (-1) * oldAmount),
                            Balance = balance,
                            UnitID = oldUnitID,
                            Action = actionName + "-UD",
                            UpdateDate = DateTime.Now,
                            UpdateBy = empID
                        };

                        en.TAssetTransactions.Add(p);

                        en.SaveChanges();

                        unitDepChange = true;
                    }
                    catch
                    {

                    }

                    lastTrans = null;

                    lastTransID = 0;
                    balance = 0;
                }

                #endregion

                Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                ID = (int)(en.TAssetTransactions.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TAssetTransactions.Where(w => w.Year == Year).Max(m => m.AssetTransactionId) + 1);

                var lastRowForProductUnit = en.TAssetTransactions.Where(w => w.Year == Year && w.DepID == depID && w.AssetProductId == prodID && w.UnitID == unitID);
                if (lastRowForProductUnit.Count() != 0)
                {
                    lastTransID = lastRowForProductUnit.Max(x => x.AssetTransactionId);
                    lastTrans = en.TAssetTransactions.Where(w => w.Year == Year && w.AssetTransactionId == lastTransID).FirstOrDefault();

                    balance = lastTrans.Balance;
                }

                // Balance Calculate : INSERT, UPDATE, DELETE
                int? sign = 1;
                switch (actionName)
                {
                    case "INSERT":
                        balance += (multiplier * amount);
                        sign = multiplier * 1; break;
                    case "UPDATE":
                        if (unitDepChange) { balance += (multiplier * amount); } else { balance = balance - (multiplier * oldAmount) + (multiplier * amount); }
                        sign = multiplier * 1; break;
                    case "DELETE":
                        balance -= (multiplier * amount);
                        sign = multiplier * -1; break;
                }

                try
                {
                    TAssetTransaction p = new TAssetTransaction
                    {
                        Year = Year,
                        AssetTransactionId = ID,
                        TransactionType = transType,
                        TransactionID = transID,
                        AssetProductId = prodID,
                        DepID = depID,
                        Amount = (sign * amount),
                        Balance = balance,
                        UnitID = unitID,
                        Action = actionName,
                        UpdateDate = DateTime.Now,
                        UpdateBy = empID
                    };

                    en.TAssetTransactions.Add(p);

                    en.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public static void RecordTransaction(JWTToken.userData userData, int transType, int transID, int? prodID, int? depID, int? oldDepID, int? amount, int? oldAmount, int? unitID, int? oldUnitID, int action)
        {
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                int empID = userData.UserID;

                int Year = 0;
                int ID = 0;

                TAssetTransaction lastTrans = null;
                int lastTransID = 0;

                string actionName = Enum.GetName(typeof(AssetAction), action);
                string transTypeName = Enum.GetName(typeof(AssetTransactionType), transType);

                int? balance = 0;
                bool unitDepChange = false;

                int? multiplier = 1;
                switch (transTypeName)
                {
                    case "GET": multiplier = 1; break;
                    case "WITHDRAW":
                    case "BORROW":
                    case "CUTTING":
                    case "TRANSFER": multiplier = -1; break;
                }

                #region When update with unit not same or department not same

                if ((unitID != oldUnitID && oldUnitID != null) || (depID != oldDepID && oldDepID != null))
                {
                    Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                    //ID = (int)(en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year).Count() == 0 ? 1 : en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year).Max(m => m.AssetTransactionId) + 1);

                    var lastRowForProductOldUnit = en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year && w.DepID == oldDepID && w.AssetProductId == prodID && w.UnitID == oldUnitID);
                    if (lastRowForProductOldUnit.Count() != 0)
                    {
                        lastTransID = lastRowForProductOldUnit.Max(x => x.AssetTransactionId);
                        lastTrans = en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year && w.AssetTransactionId == lastTransID).FirstOrDefault();

                        balance = lastTrans.Balance;
                    }

                    balance -= (multiplier * oldAmount);

                    try
                    {
                        TAssetTransaction p = new TAssetTransaction
                        {
                            Year = Year,
                            //AssetTransactionId = ID,
                            TransactionType = transType,
                            TransactionID = transID,
                            AssetProductId = prodID,
                            DepID = oldDepID,
                            Amount = (multiplier * (-1) * oldAmount),
                            Balance = balance,
                            UnitID = oldUnitID,
                            Action = actionName + "-UD",
                            UpdateDate = DateTime.Now,
                            UpdateBy = empID,
                            SchoolID = schoolID
                        };

                        en.TAssetTransactions.Add(p);

                        en.SaveChanges();

                        unitDepChange = true;
                    }
                    catch
                    {

                    }

                    lastTrans = null;

                    lastTransID = 0;
                    balance = 0;
                }

                #endregion

                Year = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                //ID = (int)(en.TAssetTransactions.Where(w => w.Year == Year).Count() == 0 ? 1 : en.TAssetTransactions.Where(w => w.Year == Year).Max(m => m.AssetTransactionId) + 1);

                var lastRowForProductUnit = en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year && w.DepID == depID && w.AssetProductId == prodID && w.UnitID == unitID);
                if (lastRowForProductUnit.Count() != 0)
                {
                    lastTransID = lastRowForProductUnit.Max(x => x.AssetTransactionId);
                    lastTrans = en.TAssetTransactions.Where(w => w.SchoolID == schoolID && w.Year == Year && w.AssetTransactionId == lastTransID).FirstOrDefault();

                    balance = lastTrans.Balance;
                }

                // Balance Calculate : INSERT, UPDATE, DELETE
                int? sign = 1;
                switch (actionName)
                {
                    case "INSERT":
                        balance += (multiplier * amount);
                        sign = multiplier * 1; break;
                    case "UPDATE":
                        if (unitDepChange) { balance += (multiplier * amount); } else { balance = balance - (multiplier * oldAmount) + (multiplier * amount); }
                        sign = multiplier * 1; break;
                    case "DELETE":
                        balance -= (multiplier * amount);
                        sign = multiplier * -1; break;
                }

                try
                {
                    TAssetTransaction p = new TAssetTransaction
                    {
                        Year = Year,
                        //AssetTransactionId = ID,
                        TransactionType = transType,
                        TransactionID = transID,
                        AssetProductId = prodID,
                        DepID = depID,
                        Amount = (sign * amount),
                        Balance = balance,
                        UnitID = unitID,
                        Action = actionName,
                        UpdateDate = DateTime.Now,
                        UpdateBy = empID,
                        SchoolID = schoolID
                    };

                    en.TAssetTransactions.Add(p);

                    en.SaveChanges();
                }
                catch
                {

                }
            }
        }
    }
}