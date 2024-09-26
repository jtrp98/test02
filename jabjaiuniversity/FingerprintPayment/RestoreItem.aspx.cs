using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class RestoreItem : Page
    {
        private static JWTToken.userData UserData = new JWTToken.userData();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object RestoreData(int sID, int type)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { UserData = token.getTokenValues(HttpContext.Current); }

            try
            {

                switch (type)
                {
                    case 1:
                        {
                            var entityUser = new JabjaiEntity.DB.TUser();
                            var masterUser = new MasterEntity.TUser();

                            string name = "";

                            using (var db1 = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                            {
                                entityUser = db1.TUser.Where(o => o.sID == sID && o.SchoolID == UserData.CompanyID).FirstOrDefault();
                              
                                if (entityUser != null)
                                {
                                    name = entityUser.sName + " " + entityUser.sLastname;

                                    var c = db1.TUser.Where(o => o.SchoolID == UserData.CompanyID 
                                        && o.sStudentID == entityUser.sStudentID 
                                        && o.sIdentification == entityUser.sIdentification
                                        && (o.cDel == "0" || o.cDel == null || o.cDel == "G")
                                        )
                                        .Count();

                                    if (c > 0)
                                    {
                                        return new { 
                                            status = false ,
                                            text = "duplicate"
                                        };
                                    }
                                    else
                                    {
                                        entityUser.cDel = null;
                                        entityUser.dUpdate = DateTime.Now;
                                        db1.SaveChanges();

                                        using (var db2 = Connection.MasterEntities(ConnectionDB.Read))
                                        {
                                            masterUser = db2.TUsers.Where(o => o.sID == sID && o.nCompany == UserData.CompanyID).FirstOrDefault();
                                            masterUser.cDel = null;
                                            masterUser.dUpdate = DateTime.Now;

                                            db2.SaveChanges();
                                        }

                                        return new
                                        {
                                            status = true,
                                            text = "success"
                                        };
                                    }
                                }
                            }
                           
                            database.InsertLog(UserData.UserID + "", $"กู้ข้อมูลนักเรียน {name}", UserData.Entities, HttpContext.Current.Request, 26, 0, 0);

                            //using (var db1 = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                            //{
                            //    var d = db1.TUsers.Where(o => o.sID == sID && o.SchoolID == UserData.CompanyID).FirstOrDefault();
                            //    d.cDel = null;
                            //    d.dUpdate = DateTime.Now;
                            //    name = d.sName + " " + d.sLastname;
                            //    db1.SaveChanges();

                            //}

                            //using (var db2 = Connection.MasterEntities(ConnectionDB.Read))
                            //{
                            //    var d = db2.TUsers.Where(o => o.sID == sID && o.nCompany == UserData.CompanyID).FirstOrDefault();
                            //    d.cDel = null;
                            //    d.dUpdate = DateTime.Now;

                            //    db2.SaveChanges();
                            //}
                        }

                        break;
                    default:
                        break;
                }

                return true;

            }
            catch 
            {
                return false;
            }

        }
    }
}