using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.PreRegister
{
    /// <summary>
    /// Summary description for preRegisterChangeTitle
    /// </summary>
    public class preRegisterChangeTitle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                dynamic rss = new JObject();

                foreach (var data in _db.TUser.Where(w => w.SchoolID == userData.CompanyID))
                {
                    if (data.sStudentTitle != null)
                    {
                        int n;
                        bool isNumeric = int.TryParse(data.sStudentTitle, out n);
                        if (isNumeric == false)
                        {
                            var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == data.sStudentTitle).FirstOrDefault();
                            if (fix != null)
                                data.sStudentTitle = fix.nTitleid.ToString();
                        }

                        if (data.sStudentTitle == "")
                        {
                            if (data.cSex != null)
                            {
                                if (data.cSex == "0")
                                {
                                    var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == "นาย").FirstOrDefault();
                                    if (fix != null)
                                        data.sStudentTitle = fix.nTitleid.ToString();
                                }
                                else if (data.cSex == "1")
                                {
                                    var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == "นางสาว").FirstOrDefault();
                                    if (fix != null)
                                        data.sStudentTitle = fix.nTitleid.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (data.cSex != null)
                        {
                            if (data.cSex == "0")
                            {
                                var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == "นาย").FirstOrDefault();
                                if (fix != null)
                                    data.sStudentTitle = fix.nTitleid.ToString();
                            }
                            else if (data.cSex == "1")
                            {
                                var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == "นางสาว").FirstOrDefault();
                                if (fix != null)
                                    data.sStudentTitle = fix.nTitleid.ToString();
                            }
                        }
                    }
                }

                _db.SaveChanges();


                foreach (var data in _db.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sFamilyTitle != null && w.sMotherTitle != null && w.sFatherTitle != null))
                {
                    int n;
                    bool isNumeric = int.TryParse(data.sFamilyTitle, out n);
                    if (isNumeric == false)
                    {
                        var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == data.sFamilyTitle).FirstOrDefault();
                        if (fix != null)
                            data.sFamilyTitle = fix.nTitleid.ToString();
                    }

                    int n2;
                    isNumeric = int.TryParse(data.sFatherTitle, out n2);
                    if (isNumeric == false)
                    {
                        var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == data.sFatherTitle).FirstOrDefault();
                        if (fix != null)
                            data.sFatherTitle = fix.nTitleid.ToString();
                    }

                    int n3;
                    isNumeric = int.TryParse(data.sMotherTitle, out n3);
                    if (isNumeric == false)
                    {
                        var fix = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.titleDescription == data.sMotherTitle).FirstOrDefault();
                        if (fix != null)
                            data.sMotherTitle = fix.nTitleid.ToString();
                    }

                }

                _db.SaveChanges();

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
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