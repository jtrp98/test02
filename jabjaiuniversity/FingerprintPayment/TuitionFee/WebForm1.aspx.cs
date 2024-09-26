using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using PeakengineAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.TuitionFee
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            var _listslv = _db.TSubLevels.OrderBy(o => o.SubLevel).ToList();
            fcommon.LinqToDropDownList(_listslv, ddlSubLV, "- ทั้งหมด -", "nTSubLevel", "SubLevel");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getContactId(int class_id)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities";
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                    {
                        //var qXML = PeakengineAPI.ReadXML(HttpContext.Current.Server.MapPath("~/PeakengineAPI.xml"));
                        string connectId = ConfigurationManager.AppSettings["connectId"];
                        string password = ConfigurationManager.AppSettings["password"];
                        var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);


                        if (qXML.Client_Token == null || qXML.Client_Time_Stamp.Value.AddDays(1) <= DateTime.Now.AddMinutes(-1).ToUniversalTime())
                        {
                            var clientToken = ClientToken.SendRequest(connectId, password);
                            qXML.Client_Token = clientToken.token;
                            qXML.Client_Time_Stamp = DateTime.Now.ToUniversalTime();
                        }

                        var contact_add = new List<ContactsAPI.Contacts>();
                        var qstudent = dbschool.TUsers.Where(w => string.IsNullOrEmpty(w.ContactPeak) && w.nTermSubLevel2 == class_id && w.cDel == null).ToList();
                        var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType.Contains("0")).ToList();
                        var qdistrict = dbmaster.districts.ToList();
                        var qamphurs = dbmaster.amphurs.ToList();
                        var qprovinces = dbmaster.provinces.ToList();
                        foreach (var student_data in qstudent)
                        {
                            string district = "", province = "";
                            //if (!string.IsNullOrEmpty(student_data.sStudentTumbon))
                            //    district = qdistrict.FirstOrDefault(f => f.DISTRICT_ID == int.Parse(student_data.sStudentTumbon)).DISTRICT_NAME;
                            //if (!string.IsNullOrEmpty(province))
                            //    province = qprovinces.FirstOrDefault(f => f.PROVINCE_ID == int.Parse(student_data.sStudentProvince)).PROVINCE_NAME;
                            int taxNumber;
                            int.TryParse(student_data.sIdentification, out taxNumber);
                            contact_add.Add(new ContactsAPI.Contacts
                            {
                                name = student_data.sName + " " + student_data.sLastname,
                                type = 5,
                                taxNumber = taxNumber,
                                code = string.Format("J{0:000}-{1:00000}", qcompany.nCompany, student_data.sID),
                                //branchCode = string.Format("JB{0:0000}-{1:000000}", qcompany.nCompany, student_data.sID),
                                address = student_data.sAddress,
                                subDistrict = "Tarang",
                                district = district,
                                province = province,
                                country = "ไทย",
                                postCode = student_data.sPostalcode,
                                callCenterNumber = student_data.sPhone,
                                faxNumber = "",
                                email = student_data.sEmail,
                                website = "",
                                contactFirstName = student_data.sName,
                                contactLastName = student_data.sLastname,
                                contactNickName = student_data.sNickName,
                                contactPosition = "",
                                contactPhoneNumber = student_data.sPhone,
                                contactEmail = student_data.sEmail
                            });
                        }

                        //var contact_result = ContactsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, contact_add);
                        //foreach (var result_data in contact_result)
                        //{
                        //    var q_data = qstudent.FirstOrDefault(f => string.Format("J{0:000}-{1:00000}", qcompany.nCompany, f.sID) == result_data.code);
                        //    if (q_data != null)
                        //    {
                        //        q_data.ContactPeak = result_data.id;
                        //        var q_update = qusermaster.FirstOrDefault(f => f.nSystemID == q_data.sID);
                        //        if (q_update != null) q_update.ContactPeak = result_data.id;
                        //    }
                        //}

                        dbschool.SaveChanges();
                        dbmaster.SaveChanges();

                    }
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                    ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getStudent_ContactId(int student_id)
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities";
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                    {
                        //var qXML = PeakengineAPI.ReadXML(HttpContext.Current.Server.MapPath("~/PeakengineAPI.xml"));
                        string connectId = ConfigurationManager.AppSettings["connectId"];
                        string password = ConfigurationManager.AppSettings["password"];
                        var qXML = ConfigPeakenineAPI.GetToken(qcompany.nCompany, connectId, password);


                        if (qXML.Client_Token == null || qXML.Client_Time_Stamp.Value.AddDays(1) <= DateTime.Now.AddMinutes(-1).ToUniversalTime())
                        {
                            var clientToken = ClientToken.SendRequest(connectId, password);
                            qXML.Client_Token = clientToken.token;
                            qXML.Client_Time_Stamp = DateTime.Now.ToUniversalTime();
                        }

                        var contact_add = new List<ContactsAPI.Contacts>();
                        var student_data = dbschool.TUsers.Where(w => w.sID == student_id && w.cDel == null).FirstOrDefault();
                        if (!string.IsNullOrEmpty(student_data.ContactPeak))
                        {
                            var fuser = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType.Contains("0") && w.nSystemID == student_id).FirstOrDefault();
                            if (fuser != null)
                            {
                                fuser.ContactPeak = student_data.ContactPeak;
                                dbmaster.SaveChanges();
                            }
                            return student_data.ContactPeak;
                        }
                        var qusermaster = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType.Contains("0")).ToList();
                        var qdistrict = dbmaster.districts.ToList();
                        var qamphurs = dbmaster.amphurs.ToList();
                        var qprovinces = dbmaster.provinces.ToList();
                        string district = "", province = "";
                        //if (!string.IsNullOrEmpty(student_data.sStudentTumbon))
                        //    district = qdistrict.FirstOrDefault(f => f.DISTRICT_ID == int.Parse(student_data.sStudentTumbon)).DISTRICT_NAME;
                        //if (!string.IsNullOrEmpty(province))
                        //    province = qprovinces.FirstOrDefault(f => f.PROVINCE_ID == int.Parse(student_data.sStudentProvince)).PROVINCE_NAME;
                        int taxNumber;
                        int.TryParse(student_data.sIdentification, out taxNumber);

                        contact_add.Add(new ContactsAPI.Contacts
                        {
                            name = student_data.sName + " " + student_data.sLastname,
                            type = 5,
                            taxNumber = taxNumber,
                            code = string.Format("J{0:000}-{1:00000}", qcompany.nCompany, student_data.sID),
                            //branchCode = string.Format("JB{0:0000}-{1:000000}", qcompany.nCompany, student_data.sID),
                            address = student_data.sAddress,
                            subDistrict = "Tarang",
                            district = district,
                            province = province,
                            country = "ไทย",
                            postCode = student_data.sPostalcode,
                            callCenterNumber = student_data.sPhone,
                            faxNumber = "",
                            email = student_data.sEmail,
                            website = "",
                            contactFirstName = student_data.sName,
                            contactLastName = student_data.sLastname,
                            contactNickName = student_data.sNickName,
                            contactPosition = "",
                            contactPhoneNumber = student_data.sPhone,
                            contactEmail = student_data.sEmail
                        });

                        var contact_result = ContactsAPI.SendRequest(connectId, password, qXML.Client_Token, qXML.User_Token, contact_add, "").FirstOrDefault();

                        if (contact_result != null)
                        {
                            student_data.ContactPeak = contact_result.id;
                            var q_update = qusermaster.FirstOrDefault(f => f.nSystemID == student_data.sID);
                            if (q_update != null) q_update.ContactPeak = contact_result.id;
                            q_update.cDel = null;
                        }

                        dbschool.SaveChanges();
                        dbmaster.SaveChanges();
                        return contact_result.id;
                    }
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                    ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string CopyContactId()
        {
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    //string entities = "JabJaiEntities";
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities)))
                    {

                        var qstudent = dbschool.TUsers.Where(w => !string.IsNullOrEmpty(w.ContactPeak));
                        var qusermaster = dbmaster.TUsers.Where(w => w.cType == "0" && w.nCompany == qcompany.nCompany).ToList();
                        foreach (var result_data in qstudent)
                        {
                            var q_update = qusermaster.FirstOrDefault(f => f.nSystemID == result_data.sID);
                            if (q_update != null) q_update.ContactPeak = result_data.ContactPeak;
                        }

                        dbmaster.SaveChanges();
                    }
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
                    ex.Message, ex.StackTrace, ex.Source) + "}";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static String getStudent(int class_id)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                //string entities = "JabJaiEntities";
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                var qmaster_user = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType == "0").ToList();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(qcompany)))
                {
                    dynamic rss = new JArray(from a in dbschool.TUsers.ToList()
                                             join b in qmaster_user on a.sID equals b.nSystemID
                                             where /*a.nTermSubLevel2 == class_id &&*/ a.cDel == null && b.ContactPeak == null
                                             select new JObject {
                                                 new JProperty("student_id",a.sID),
                                                 new JProperty("student_name",a.sName + " " + a.sLastname),
                                                 new JProperty("contact_id",b.ContactPeak)
                                             });

                    return rss.ToString();
                }
            }
        }
    }
}