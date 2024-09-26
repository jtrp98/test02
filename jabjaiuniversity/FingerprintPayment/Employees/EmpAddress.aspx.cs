using FingerprintPayment.Employees.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    public partial class EmpAddress : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";
        private static string SessionForeignKey1 = "FOREIGNKEY_EMPADDRESSID1";
        private static string SessionForeignKey2 = "FOREIGNKEY_EMPADDRESSID2";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0; break;
                case "form":
                    MvContent.ActiveViewIndex = 1; break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string SaveItem(List<string> data)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    if (HttpContext.Current.Session[SessionPrimaryKey] != null)
                    {
                        int empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                        string No = data[1];
                        string VillageNo = data[2];
                        string Village = data[3];
                        string Alley = data[4];
                        string Building = data[5];
                        string Road = data[6];
                        int? District = string.IsNullOrEmpty(data[7]) ? (int?)null : int.Parse(data[7]);
                        int? Amphur = string.IsNullOrEmpty(data[8]) ? (int?)null : int.Parse(data[8]);
                        int? Province = string.IsNullOrEmpty(data[9]) ? (int?)null : int.Parse(data[9]);
                        string PostalCode = data[10];
                        string PhoneNumber = data[11];

                        string No2 = data[12];
                        string VillageNo2 = data[13];
                        string Village2 = data[14];
                        string Alley2 = data[15];
                        string Building2 = data[16];
                        string Road2 = data[17];
                        int? District2 = string.IsNullOrEmpty(data[18]) ? (int?)null : int.Parse(data[18]);
                        int? Amphur2 = string.IsNullOrEmpty(data[19]) ? (int?)null : int.Parse(data[19]);
                        int? Province2 = string.IsNullOrEmpty(data[20]) ? (int?)null : int.Parse(data[20]);
                        string PostalCode2 = data[21];
                        string PhoneNumber2 = data[22];

                        // ที่อยู่ตามทะเบียนบ้าน
                        if (HttpContext.Current.Session[SessionForeignKey1] == null)
                        {
                            // Insert Section
                            int ID = (int)(en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                            // Get Item
                            TEmpAddress p = new TEmpAddress
                            {
                                sEmp = empID,
                                ID = ID,
                                Type = 1,
                                No = No,
                                VillageNo = VillageNo,
                                Village = Village,
                                Alley = Alley,
                                Building = Building,
                                Road = Road,
                                //DISTRICT_ID = District,
                                //AMPHUR_ID = Amphur,
                                //PROVINCE_ID = Province,
                                //PostalCode = PostalCode,
                                //PhoneNumber = PhoneNumber,

                                UpdateDate = DateTime.Now,
                                SchoolID = userData.CompanyID
                            };

                            en.TEmpAddresses.Add(p);

                            en.SaveChanges();
                        }
                        else
                        {
                            // Modify Section
                            int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey1]);

                            TEmpAddress pi = en.TEmpAddresses.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);
                            if (pi != null)
                            {
                                pi.No = No;
                                pi.VillageNo = VillageNo;
                                pi.Village = Village;
                                pi.Alley = Alley;
                                pi.Building = Building;
                                pi.Road = Road;
                                //pi.DISTRICT_ID = District;
                                //pi.AMPHUR_ID = Amphur;
                                //pi.PROVINCE_ID = Province;
                                //pi.PostalCode = PostalCode;
                                //pi.PhoneNumber = PhoneNumber;

                                pi.UpdateDate = DateTime.Now;

                                en.SaveChanges();
                            }
                            else
                            {
                                isComplete = "warning";
                            }
                        }

                        // ที่อยู่ที่ติดต่อได้
                        if (HttpContext.Current.Session[SessionForeignKey2] == null)
                        {
                            // Insert Section
                            int ID = (int)(en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);

                            // Get Item
                            TEmpAddress p = new TEmpAddress
                            {
                                sEmp = empID,
                                ID = ID,
                                Type = 2,
                                No = No2,
                                VillageNo = VillageNo2,
                                Village = Village2,
                                Alley = Alley2,
                                Building = Building2,
                                Road = Road2,
                                //DISTRICT_ID = District2,
                                //AMPHUR_ID = Amphur2,
                                //PROVINCE_ID = Province2,
                                //PostalCode = PostalCode2,
                                //PhoneNumber = PhoneNumber2,

                                UpdateDate = DateTime.Now,
                                SchoolID = userData.CompanyID
                            };

                            en.TEmpAddresses.Add(p);

                            en.SaveChanges();
                        }
                        else
                        {
                            // Modify Section
                            int ID = Convert.ToInt16(HttpContext.Current.Session[SessionForeignKey2]);

                            TEmpAddress pi = en.TEmpAddresses.First(f => f.SchoolID == schoolID && f.sEmp == empID && f.ID == ID);
                            if (pi != null)
                            {
                                pi.No = No2;
                                pi.VillageNo = VillageNo2;
                                pi.Village = Village2;
                                pi.Alley = Alley2;
                                pi.Building = Building2;
                                pi.Road = Road2;
                                //pi.DISTRICT_ID = District2;
                                //pi.AMPHUR_ID = Amphur2;
                                //pi.PROVINCE_ID = Province2;
                                //pi.PostalCode = PostalCode2;
                                //pi.PhoneNumber = PhoneNumber2;

                                pi.UpdateDate = DateTime.Now;

                                en.SaveChanges();
                            }
                            else
                            {
                                isComplete = "warning";
                            }
                        }
                    }
                    else
                    {
                        isComplete = "error";
                    }
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string GetItem(string empID)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iEmpID = 0;
                    if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                    List<TEmpAddress> lp = en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID).ToList();
                    if (lp.Count > 0)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 22; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        HttpContext.Current.Session[SessionPrimaryKey] = iEmpID;

                        // ที่อยู่ตามทะเบียนบ้าน
                        TEmpAddress p1 = lp.Where(w => w.Type == 1).OrderByDescending(o => o.ID).FirstOrDefault();
                        if (p1 != null)
                        {
                            HttpContext.Current.Session[SessionForeignKey1] = p1.ID;

                            dt.Rows[0]["F1"] = p1.No;
                            dt.Rows[0]["F2"] = p1.VillageNo;
                            dt.Rows[0]["F3"] = p1.Village;
                            dt.Rows[0]["F4"] = p1.Alley;
                            dt.Rows[0]["F5"] = p1.Building;
                            dt.Rows[0]["F6"] = p1.Road;
                            //dt.Rows[0]["F7"] = (p1.DISTRICT_ID == null ? null : p1.DISTRICT_ID.Value.ToString());
                            //dt.Rows[0]["F8"] = (p1.AMPHUR_ID == null ? null : p1.AMPHUR_ID.Value.ToString());
                            //dt.Rows[0]["F9"] = (p1.PROVINCE_ID == null ? null : p1.PROVINCE_ID.Value.ToString());
                            //dt.Rows[0]["F10"] = p1.PostalCode;
                            //dt.Rows[0]["F11"] = p1.PhoneNumber;
                        }

                        // ที่อยู่ที่ติดต่อได้
                        TEmpAddress p2 = lp.Where(w => w.Type == 2).OrderByDescending(o => o.ID).FirstOrDefault();
                        if (p2 != null)
                        {
                            HttpContext.Current.Session[SessionForeignKey2] = p2.ID;

                            dt.Rows[0]["F12"] = p2.No;
                            dt.Rows[0]["F13"] = p2.VillageNo;
                            dt.Rows[0]["F14"] = p2.Village;
                            dt.Rows[0]["F15"] = p2.Alley;
                            dt.Rows[0]["F16"] = p2.Building;
                            dt.Rows[0]["F17"] = p2.Road;
                            //dt.Rows[0]["F18"] = (p2.DISTRICT_ID == null ? null : p2.DISTRICT_ID.Value.ToString());
                            //dt.Rows[0]["F19"] = (p2.AMPHUR_ID == null ? null : p2.AMPHUR_ID.Value.ToString());
                            //dt.Rows[0]["F20"] = (p2.PROVINCE_ID == null ? null : p2.PROVINCE_ID.Value.ToString());
                            //dt.Rows[0]["F21"] = p2.PostalCode;
                            //dt.Rows[0]["F22"] = p2.PhoneNumber;
                        }

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                if (infor == "new" || infor == "error")
                {
                    HttpContext.Current.Session[SessionForeignKey1] = null;
                    HttpContext.Current.Session[SessionForeignKey2] = null;
                }

                return infor;
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            HttpContext.Current.Session[SessionForeignKey1] = null;
            HttpContext.Current.Session[SessionForeignKey2] = null;

            return "";
        }

        #endregion
    }
}