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

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for gradeTranscriptAll
    /// </summary>
    public class gradeTranscriptAll : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();


            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
                string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
                string year = fcommon.ReplaceInjection(context.Request["year"]);
                string term = fcommon.ReplaceInjection(context.Request["term"]);
                string mode = fcommon.ReplaceInjection(context.Request["mode"]);

                List<studentlist2> unique = new List<studentlist2>();


                int? idlv2n = Int32.Parse(idlv2);
                int? useryear = Int32.Parse(year);
                string username = "";
                int? ntermsublv2 = 0;
                int nyear = 0;
                string nterm = "";
                int ntermtable = 0;
                List<string> planIdlist = new List<string>();


                foreach (var ff in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.numberYear == useryear && w.cDel == false))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.sTerm == term && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                var provincelist = dbmaster.provinces.ToList();
                var titlelist = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var nterm2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                string bSpec = "";
                string bSubject = "";
                string branchName = "";
                if (nterm2.nBranchSpecId != null)
                {
                    var spec = _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchSpecId == nterm2.nBranchSpecId).FirstOrDefault();
                    var subject = _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchSubjectId == spec.BranchSubjectId).FirstOrDefault();
                    var brach = _db.TBranches.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.BranchId == subject.BranchId).FirstOrDefault();

                    bSpec = spec.BranchSpecName;
                    bSubject = subject.BranchSubjectName;
                    branchName = brach.BranchName;
                }

                var room = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                var sub = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                unique.AddRange((from a in _db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                 join c in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals c.nTermSubLevel2
                                 where a.nTermSubLevel2 == idlv2n && (a.nStudentStatus == null || a.nStudentStatus == 0) && ((a.cDel ?? "0") != "1")

                                 select new studentlist2
                                 {
                                     idlv = idlv,
                                     idlv2 = idlv2,
                                     oldClass = a.oldSchoolName,
                                     oldGPA = a.oldSchoolGPA.ToString(),
                                     oldName = a.oldSchoolName,
                                     oldProvince = a.oldSchoolProvince,
                                     schName = tCompany.sCompany,
                                     schProvince = tCompany.sProvince,
                                     sID = a.sID,
                                     sName = a.sName + " " + a.sLastname,
                                     title = a.sStudentTitle,

                                     sort1txt = a.sStudentID,
                                     sstudentid = a.sStudentID,
                                     stdB1 = branchName,
                                     stdB2 = bSubject,
                                     stdB3 = bSpec,
                                     dBirth = a.dBirth,
                                     stdCityId = a.sIdentification,
                                     stdClass = sub.SubLevel,
                                     stdId = a.sID.ToString(),
                                     stdNation = a.sStudentNation,
                                     stdRace = a.sStudentRace.Trim(),
                                     stdReligion = a.sStudentReligion,
                                     stdRoom = room.nTSubLevel2.Trim(),
                                     stdTerm = term,
                                     stdYear = year,
                                     term = term,
                                     year = year

                                 }
                                 ).ToList());

                List<studentlist2> unique2 = new List<studentlist2>();

                var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

                foreach (var b in unique)
                {
                    var TUserMaster = q_usermaster.Where(w => w.nSystemID == b.sID).FirstOrDefault();
                    if (TUserMaster != null)
                    {
                        var fam = _db.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == b.sID).FirstOrDefault();
                        if (fam != null)
                        {
                            b.fName = fam.sFatherTitle + "" + fam.sFatherFirstName + " " + fam.sFatherLastName;
                            b.mName = fam.sMotherTitle + "" + fam.sMotherFirstName + " " + fam.sMotherLastName;
                        }
                        else
                        {
                            b.fName = "";
                            b.mName = "";
                        }

                        b.oldClass = oldgraduate(b.oldClass);
                        b.oldProvince = txtprovince(b.oldProvince, provincelist);
                        b.sName = txttitle(b.title, titlelist) + b.sName;
                        b.sort1int = txtsort(b.sort1txt);
                        b.stdBirth = b.dBirth.Value.Day + " " + monthtxt(b.dBirth.Value.Month) + " " + b.dBirth.Value.AddYears(543).Year;
                        unique2.Add(b);
                    }
                }


                var newSortList4 = unique2;
                newSortList4 = unique2.OrderBy(x => x.sort1int).ThenBy(x => x.sort1txt).ToList();

                int counter = 1;

                foreach (var a in newSortList4)
                {
                    a.number = counter.ToString();
                    counter = counter + 1;
                    a.href = "/grade/gradeTranscriptiframe.aspx?year=" + a.year + "&idlv=" + a.idlv + "&idlv2=" + a.idlv2 + "&term=" + a.term + "&id=" + a.sID + "&mode=all&sName=" + a.sName + "&schName=" + a.schName + "&schProvince=" + a.schProvince + "&stdB1=" + a.stdB1 + "&stdB2=" + a.stdB2 + "&stdB3=" + a.stdB3 + "&stdId=" + a.stdId + "&stdBirth=" + a.stdBirth + "&stdCityId=" + a.stdCityId + "&stdNation=" + a.stdNation + "&stdRace=" + a.stdRace + "&stdReligion=" + a.stdReligion + "&fName=" + a.fName + "&mName=" + a.mName + "&stdYear=" + a.stdYear + "&stdTerm=" + a.stdTerm + "&stdRoom=" + a.stdRoom + "&stdClass=" + a.stdClass.Trim() + "&oldName=" + a.oldName + "&oldProvince=" + a.oldProvince + "&oldClass=" + a.oldClass + "&oldGPA=" + a.oldGPA;
                }

                unique = newSortList4;




                rss = new JArray(from a in unique
                                 select new JObject(

                       new JProperty("href", a.href)
                    ));

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

        string oldgraduate(string index)
        {
            string txt = "";
            if (index == null)
                txt = "";
            else if (index == "1")
                txt = "ประถมศึกษาปีที่ 1";
            else if (index == "2")
                txt = "ประถมศึกษาปีที่ 2";
            else if (index == "3")
                txt = "ประถมศึกษาปีที่ 3";
            else if (index == "4")
                txt = "ประถมศึกษาปีที่ 4";
            else if (index == "5")
                txt = "ประถมศึกษาปีที่ 5";
            else if (index == "6")
                txt = "ประถมศึกษาปีที่ 6";
            else if (index == "7")
                txt = "มัธยมศึกษาปีที่ 3";
            else if (index == "8")
                txt = "มัธยมศึกษาปีที่ 6";
            return txt;
        }

        string txtprovince(string index, List<province> list)
        {
            string txt = "";
            if (index != null)
            {
                int indexn = int.Parse(index);
                var x = list.Where(w => w.PROVINCE_ID == indexn).FirstOrDefault();
                txt = x.PROVINCE_NAME;
            }

            return txt;
        }
        string txttitle(string index, List<TTitleList> list)
        {
            string txt = index;
            int n;
            bool isNumeric = int.TryParse(index, out n);
            if (isNumeric == true)
            {
                var a = list.Where(w => w.nTitleid == n).FirstOrDefault();
                txt = a.titleDescription;
            }
            return txt;
        }
        int txtsort(string sort)
        {
            int txt = 99999;
            int n;
            bool isNumeric = int.TryParse(sort, out n);
            if (isNumeric == true)
            {
                txt = n;
            }
            return txt;
        }
        string monthtxt(int month)
        {
            string txt = "";
            if (month == 1)
                txt = "มกราคม";
            else if (month == 2)
                txt = "กุมภาพันธ์";
            else if (month == 3)
                txt = "มีนาคม";
            else if (month == 4)
                txt = "เมษายน";
            else if (month == 5)
                txt = "พฤษภาคม";
            else if (month == 6)
                txt = "มิถุนายน";
            else if (month == 7)
                txt = "กรกฎาคม";
            else if (month == 8)
                txt = "สิงหาคม";
            else if (month == 9)
                txt = "กันยายน";
            else if (month == 10)
                txt = "ตุลาคม";
            else if (month == 11)
                txt = "พฤศจิกายน";
            else if (month == 12)
                txt = "ธันวาคม";
            return txt;
        }

        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public string sstudentid { get; set; }

            public string number { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public string idlv2 { get; set; }
            public string term { get; set; }
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public string schName { get; set; }
            public string schProvince { get; set; }
            public string stdB1 { get; set; }
            public string stdB2 { get; set; }
            public string stdB3 { get; set; }
            public string stdId { get; set; }
            public string stdBirth { get; set; }
            public string stdCityId { get; set; }
            public string stdNation { get; set; }
            public string stdRace { get; set; }
            public string stdReligion { get; set; }
            public string fName { get; set; }
            public string mName { get; set; }
            public string stdYear { get; set; }
            public string stdTerm { get; set; }
            public string stdRoom { get; set; }
            public string stdClass { get; set; }
            public string oldName { get; set; }
            public string oldProvince { get; set; }
            public string oldClass { get; set; }
            public string oldGPA { get; set; }
            public string title { get; set; }
            public DateTime? dBirth { get; set; }
            public string href { get; set; }
        }



        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }


}