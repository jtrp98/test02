using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JabjaiEntity.DB;
using System.Web.SessionState;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataGeneric02
    /// </summary>
    public class dataGeneric02 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string _str = fcommon.ReplaceInjection(context.Request["ID"]);
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                int nTimeType = int.Parse(_str == "" ? "0" : _str);
                switch (_mode)
                {
                    case "time":
                        DataTable dt = fcommon.LinqToDataTable(_db.TTimes.Where(w => w.nTimeType == nTimeType));
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        context.Response.Expires = -1;
                        context.Response.ContentType = "application/json";
                        //context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write(serializer.Serialize(rows).ToString());
                        context.Response.End();
                        break;
                    case "JobScan":
                        DataTable dtUser = fcommon.LinqToDataTable(_db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sIdentification.EndsWith(_str)));
                        foreach (DataRow dr in dtUser.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dtUser.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        break;
                    case "tabtimetype":
                        DataTable dttabtimetype = fcommon.LinqToDataTable(_db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cType == "1" && w.cUserType == "1"));
                        if (dttabtimetype.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dttabtimetype.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dttabtimetype.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    case "listlevel":
                        DataTable dtListlevel = fcommon.LinqToDataTable(_db.TLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTimeType == nTimeType));
                        if (dtListlevel != null)
                        {
                            foreach (DataRow dr in dtListlevel.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtListlevel.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                    case "EmpScan":
                        DataTable dtEmp = fcommon.LinqToDataTable(_db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sIdentification.EndsWith(_str)).Select(s => new { s.sName, s.sLastname, s.sFinger, s.sFinger2, s.sEmp }));
                        if (dtEmp != null)
                        {
                            foreach (DataRow dr in dtEmp.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dtEmp.Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                        }
                        break;
                }
                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(serializer.Serialize(rows).ToString());
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}