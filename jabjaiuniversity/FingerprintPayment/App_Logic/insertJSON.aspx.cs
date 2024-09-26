using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    public partial class insertJSON : System.Web.UI.Page
    {
        private JWTToken.userData userData = new JWTToken.userData();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                switch (Request.QueryString["mode"])
                {
                    case "addlearn":
                        //try
                        //{
                        //    string[] strArr = null;
                        //    int days = Convert.ToInt32(Request.QueryString["days"]);

                        //    string teacher = Request.QueryString["teacher"];

                        //    string thisClass = Request.QueryString["classroom"];
                        //    strArr = teacher.Split(' ');
                        //    int sNoAdd = Convert.ToInt32(Request.QueryString["sNo"]);
                        //    string planeAdd = Request.QueryString["plane"];
                        //    int termlvs = Convert.ToInt32(Request.QueryString["sublv"]);
                        //    int teacherid = 0;
                        //    string learnlist = "";
                        //    string strT1 = Request.QueryString["time1"];
                        //    string strT2 = Request.QueryString["time2"];
                        //    DateTime timex1 = Convert.ToDateTime("01/01/1999 " + strT1);
                        //    DateTime timex2 = Convert.ToDateTime("01/01/1999 " + strT2);
                        //    TimeSpan time1 = TimeSpan.Parse(timex1.TimeOfDay.ToString());
                        //    TimeSpan time2 = TimeSpan.Parse(timex2.TimeOfDay.ToString());

                        //    DateTime timex11 = timex1.AddMinutes(15);
                        //    DateTime timex22 = timex2.AddMinutes(15);
                        //    TimeSpan time11 = TimeSpan.Parse(timex11.TimeOfDay.ToString());
                        //    TimeSpan time22 = TimeSpan.Parse(timex22.TimeOfDay.ToString());
                        //    string tName = "";
                        //    string tLastName = "";
                        //    if (strArr.Length > 1)
                        //    {
                        //        tName = strArr[0];
                        //    }
                        //    if (strArr.Length > 1)
                        //    {
                        //        tLastName = strArr[1];
                        //    }

                        //    try
                        //    {

                        //        DataTable dtteacher = fcommon.LinqToDataTable(_db.TUsers.Where(w => w.sName == tName && w.sLastname == tLastName));

                        //        foreach (DataRow row in dtteacher.Rows)
                        //        {
                        //            // ... Write value of first field as integer.
                        //            teacherid = Convert.ToInt32(row["sID"]);
                        //        }

                        //        try
                        //        {
                        //            var query = from ScheList in _db.TSchedules.Where(w => w.nPlaneDay == days
                        //                 && w.dTimeStart_IN.Value >= time1
                        //                 && w.dTimeEnd_IN <= time2
                        //                 && w.sPlaneID == planeAdd && (w.TermSublv == termlvs || w.sID == teacherid))
                        //                .DefaultIfEmpty()
                        //                        select new
                        //                        {
                        //                            sNo = ScheList.sNo,

                        //                        };
                        //            DataTable dtCheck = fcommon.LinqToDataTable(query);
                        //            foreach (DataRow row in dtCheck.Rows)
                        //            {
                        //                // ... Write value of first field as integer.
                        //                learnlist = row["sNo"].ToString();
                        //            }
                        //            if (learnlist != "")
                        //                Response.Write("duplicate");
                        //            else
                        //            {
                        //                Response.Write("success");
                        //                int newlearn = _db.TSchedules.Count();
                        //                if (newlearn > 0)
                        //                {
                        //                    newlearn += 1;
                        //                }
                        //                else { newlearn = 1; }
                        //                TSchedule _AddLearn = new TSchedule();
                        //                _AddLearn.sScheduleID = newlearn;
                        //                _AddLearn.sNo = sNoAdd;
                        //                _AddLearn.nPlaneDay = days;
                        //                _AddLearn.TermSublv = termlvs;
                        //                _AddLearn.sPlaneID = planeAdd;
                        //                _AddLearn.sID = teacherid;
                        //                _AddLearn.nPlaneDay = days;
                        //                _AddLearn.dTimeEnd_IN = time2;
                        //                _AddLearn.dTimeEnd_OUT = time22;
                        //                _AddLearn.dTimeStart_IN = time1;
                        //                _AddLearn.dTimeStart_OUT = time11;
                        //                _AddLearn.sClassID = thisClass;
                        //                _db.TSchedules.Add(_AddLearn);
                        //                _db.SaveChanges();
                        //            }

                        //        }
                        //        catch (Exception ex)
                        //        {


                        //        }

                        //    }
                        //    catch
                        //    {
                        //        Response.Write("found");
                        //    }



                        //}
                        //catch (Exception ex)
                        //{
                        //    Response.Write(ex);
                        //}
                        //Response.End();
                        break;
                    case "addplane":
                        try
                        {
                            string name = Request.Form["plane"];
                            int termlv = Convert.ToInt32(Request.QueryString["sublv"]);
                            string plandid = "";
                            string plandlist = "";
                            try
                            {

                                DataTable dtplane = fcommon.LinqToDataTable(_db.TPlanes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sPlaneName == name));

                                foreach (DataRow row in dtplane.Rows)
                                {
                                    // ... Write value of first field as integer.
                                    plandid = row["sPlaneID"].ToString();
                                }

                                try
                                {

                                    DataTable dt2 = fcommon.LinqToDataTable(_db.TPlanLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sPlaneID.ToString() == plandid && w.TTermSublv == termlv));
                                    foreach (DataRow row in dt2.Rows)
                                    {
                                        // ... Write value of first field as integer.
                                        plandlist = row["TTermSublv"].ToString();
                                    }
                                    if (plandlist != "")
                                        Response.Write("duplicate");

                                }
                                catch
                                {
                                    Response.Write("success");
                                    //int newlist = _db.TPlanLists.Count();
                                    //if (newlist > 0)
                                    //{
                                    //    newlist += 1;
                                    //}
                                    TPlanList _PLaneList = new TPlanList();
                                    int sPlaneId = 0;
                                    int.TryParse(plandid, out sPlaneId);
                                    //_PLaneList.PlanListID = newlist;
                                    _PLaneList.nPlaneDay = null;
                                    _PLaneList.sPlaneID = sPlaneId;
                                    _PLaneList.TTermSublv = termlv;
                                    _PLaneList.nPlaneDay = null;
                                    _PLaneList.dTimeEnd_IN = null;
                                    _PLaneList.dTimeEnd_OUT = null;
                                    _PLaneList.dTimeStart_IN = null;
                                    _PLaneList.dTimeStart_OUT = null;
                                    _PLaneList.SchoolID = userData.CompanyID;

                                    _db.TPlanLists.Add(_PLaneList);
                                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                        "ทำการเพิ่มวิชา ",
                                        HttpContext.Current.Session["sEntities"].ToString(),
                                        HttpContext.Current.Request, -1, 2, 0);

                                    _db.SaveChanges();
                                }

                            }
                            catch
                            {
                                Response.Write("found");
                            }



                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                        }
                        Response.End();
                        break;
                    case "termyear":
                        try
                        {
                            if (_db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.YearStatus == "1").Count() == 0)
                            {
                                int countLevel = 0;
                                countLevel = Request.Form.Count / 2;
                                InsertNewYears();
                                String termID = InsertNewTerm(1);
                                for (int i = 0; i < countLevel; i++)
                                {
                                    InsertSubLevel2(Convert.ToInt32(Request.Form["sublv" + (i + 1)].ToString()), Convert.ToInt32(Request.Form["txt" + (i + 1)].ToString()), termID);
                                }
                            }
                            Response.Write("success");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                        }
                        Response.End();
                        break;

                    case "updateHoliday":
                        try
                        {
                            int countLevel = 0, countdb = 0, countchecked = 0;
                            countLevel = Request.Form.Count / 2;
                            countdb = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Count();

                            for (int i = 0; i < countLevel; i++)
                            {
                                if (Request.Form["txt" + (i + 1)].ToString() == "1")
                                {
                                    countchecked += 1;
                                }
                            }
                            if (countchecked == countdb)
                            {

                                string sHol = Request.Form["nHol"];
                                _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == sHol).ToList().ForEach(f => f.sHolidayAll = "1");
                                _db.SaveChanges();
                                List<THolidaySome> _oldData2 = _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == sHol).ToList();



                                foreach (var detail in _oldData2.ToList())
                                { _db.THolidaySomes.Remove(detail); }

                                try
                                {
                                    _db.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex);
                                }
                            }
                            else
                            {

                                string sHol = Request.Form["nHol"];
                                _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == sHol).ToList().ForEach(f => f.sHolidayAll = null);
                                _db.SaveChanges();
                                List<THolidaySome> _oldData2 = _db.THolidaySomes.Where(w => w.nHoliday == sHol).ToList();


                                foreach (var detail in _oldData2.ToList())
                                {
                                    _db.THolidaySomes.Remove(detail);
                                }

                                try
                                {
                                    _db.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    Response.Write(ex);
                                }
                                for (int i = 0; i < countLevel; i++)
                                {
                                    if (Request.Form["txt" + (i + 1)].ToString() == "1")
                                    {
                                        InsertNewHolidaySome(Request.Form["nHol"].ToString(), Convert.ToInt32(Request.Form["sublv" + (i + 1)]));
                                    }
                                }
                            }
                            database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                                "ทำการแก้ไขวันหยุด " + _db.THolidays.FirstOrDefault(f => f.nHoliday == Request.Form["nHol"]).sHoliday,
                                HttpContext.Current.Session["sEntities"].ToString(),
                                HttpContext.Current.Request, 20, 3, 0);
                            Response.Write("success");

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                        }
                        Response.End();
                        break;

                    case "setstdlv":
                        int sID = Int32.Parse(Request.QueryString["stdid"]);
                        int sublv2 = Int32.Parse(Request.QueryString["sublv2"]);
                        // DataTable DtStudent = fcommon.LinqToDataTable(_db.TUsers.Where(w => w.sID == sID));
                        DataTable DtStudentLV = fcommon.LinqToDataTable(_db.TStudentLevels.Where(w => w.sID == sID));

                        if (DtStudentLV == null)
                        {
                            TStudentLevel _StudentLevel = new TStudentLevel();
                            // DateTime dt = new DateTime();
                            //_StudentLevel.nStdLvID = _db.TStudentLevels.Count() + 1;
                            _StudentLevel.sID = sID;
                            _StudentLevel.nTermSubLevel2 = sublv2;
                            _StudentLevel.SchoolID = userData.CompanyID;

                            _db.TStudentLevels.Add(_StudentLevel);
                            _db.SaveChanges();
                            Response.Write("success");
                            Response.End();
                        }
                        else
                        {
                            _db.TStudentLevels.Where(w => w.sID == sID).ToList().ForEach(f => f.nTermSubLevel2 = sublv2);
                            _db.SaveChanges();
                            Response.Write("success");

                        }
                        Response.End();
                        break;

                    case "setstdnewlv":
                        int sStdID = Int32.Parse(Request.QueryString["stdid"]);
                        int Stdsublv2 = Int32.Parse(Request.QueryString["sublv2"]);
                        int ResultLearn = Int32.Parse(Request.QueryString["status"]);

                        DataTable dataSubLv = fcommon.LinqToDataTable(_db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == Stdsublv2));
                        int newSubLV = Int32.Parse(dataSubLv.Rows[0]["nTSubLevel"].ToString());

                        if (newSubLV != 0)
                        {
                            //foreach (TStudentLevel _data in _db.TStudentLevels.Where(w => w.sID == sStdID).ToList())
                            //{

                            //    if (ResultLearn == 1)
                            //    {
                            //        newSubLV++;
                            //        _data.nTermSubLevel2 = 0;
                            //        _data.nTSubLevel = newSubLV;

                            //    }
                            //    else if (ResultLearn == 0)
                            //    {
                            //        _data.nTermSubLevel2 = 0;
                            //        _data.nTSubLevel = newSubLV;
                            //    }
                            //    else
                            //    {
                            //        _data.nTermSubLevel2 = -2;
                            //        _data.nTSubLevel = -2;

                            //    }
                            //    _db.SaveChanges();
                            //}
                            Response.Write("success");
                        }
                        else
                        {
                            Response.Write("error");
                        }
                        Response.End();
                        break;
                    case "setallnewlv":
                        //try
                        //{
                        //    DataTable DTAllNewLV = new DataTable();
                        //    var queryStd = from Student in _db.TUsers.Where(w => w.cType == "0" && w.cDel == null)
                        //                   join SubLevel in _db.TStudentLevel on Student.sID equals SubLevel.sID
                        //                   where SubLevel.nTermSubLevel2 != -2 && SubLevel.nTermSubLevel2 != -3 // into temp
                        //                                                                                        // from StdData in temp.Where(d => d.nTermSubLevel2 == null).DefaultIfEmpty()
                        //                   select new
                        //                   {
                        //                       sID = Student.sID,
                        //                       Name = Student.sName,
                        //                       LastName = Student.sLastname,
                        //                       SubLV2 = (SubLevel.nTermSubLevel2 == null ? -1 : SubLevel.nTermSubLevel2),
                        //                       SubLV = (SubLevel.nTSubLevel == null ? 0 : SubLevel.nTSubLevel)
                        //                   };
                        //    if (queryStd.ToList().Count > 0)
                        //    {
                        //        DTAllNewLV = fcommon.LinqToDataTable(queryStd);
                        //    }

                        //    foreach (DataRow DR in DTAllNewLV.Rows)
                        //    {
                        //        int DataStdLV = Int32.Parse(DR["SubLV2"].ToString());

                        //        DataTable dataNextSubLv = fcommon.LinqToDataTable(_db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == DataStdLV));
                        //        int DataLV = Int32.Parse(dataNextSubLv.Rows[0]["nTSubLevel"].ToString());
                        //        if (DataStdLV != -1 && DataLV != 0)
                        //        {
                        //            DataLV++;
                        //            int DTNextLV = _db.TSubLevels.Where(w => w.nTSubLevel == DataLV).Count();
                        //            int stdID = Int32.Parse(DR["sID"].ToString());

                        //            if (DTNextLV > 0)
                        //            {
                        //                foreach (TStudentLevel _data in _db.TStudentLevels.Where(w => w.sID == stdID).ToList())
                        //                {
                        //                    _data.nTermSubLevel2 = 0;
                        //                    _data.nTSubLevel = DataLV;
                        //                }
                        //            }
                        //            else
                        //            {
                        //                foreach (TStudentLevel _data in _db.TStudentLevels.Where(w => w.sID == stdID).ToList())
                        //                {
                        //                    _data.nTermSubLevel2 = -3;
                        //                    _data.nTSubLevel = -3;
                        //                }
                        //            }
                        //            _db.SaveChanges();
                        //        }
                        //    }

                        //    foreach (TYear _data in _db.TYears.Where(w => w.YearStatus == "1").ToList())
                        //    {
                        //        _data.YearStatus = "0";
                        //        _db.SaveChanges();
                        //    }

                        //    _db.TTerms.Where(w => w.TermStatus == "1").ToList().ForEach(f => { f.cDel = "1"; f.TermStatus = "0"; });
                        //    _db.SaveChanges();

                        //    _db.TPlanLists.Where(w => w.cDel == null).ToList().ForEach(f => f.cDel = "1");
                        //    _db.SaveChanges();

                        //    DateTime DateTimeCurrents = DateTime.Now;
                        //    _db.THolidays.Where(w => w.sHolidayType == "1" && w.dHolidayStart <= DateTimeCurrents).ToList().ForEach(f => f.cDel = "1");
                        //    _db.SaveChanges();

                        //    _db.TSchedules.Where(w => w.cDel == null).ToList().ForEach(f => f.cDel = "1");
                        //    _db.SaveChanges();

                        //    _db.TTermSubLevel2.Where(w => w.nTermSubLevel2Status == "1").ToList().ForEach(f => f.nTermSubLevel2Status = "0");
                        //    _db.SaveChanges();

                        //    Response.Write("success");
                        //}
                        //catch (Exception ex)
                        //{
                        //    Response.Write("error");
                        //}

                        //Response.End();
                        break;

                    case "congrateYear":
                        foreach (TYear _data in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.YearStatus == "1").ToList())
                        {
                            _data.YearStatus = "0";
                            _db.SaveChanges();

                            Response.Write("success");
                            Response.End();
                        }
                        break;

                    case "newterm":
                        try
                        {
                            DataTable DTTermSubLV2 = fcommon.LinqToDataTable(_db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2Status == "1"));
                            DataTable DTTermCurrent = fcommon.LinqToDataTable(_db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.TermStatus == "1"));
                            string YearNow = DTTermCurrent.Rows[0]["nYear"].ToString();
                            int YearNows = Int32.Parse(YearNow);
                            int countNewTerm = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nYear == YearNows).Count();
                            countNewTerm += 1;

                            string _newTermID = TransactionTerm(YearNows, countNewTerm);
                            foreach (DataRow row in DTTermSubLV2.Rows)
                            {
                                // DataTable DTcDelPlaneLists = fcommon.LinqToDataTable(_db.TPlanLists.Where(w => w.TTermSublv == Int32.Parse(row["nTermSubLevel2"].ToString())));
                                int rowTermSubLv2 = Int32.Parse(row["nTermSubLevel2"].ToString());
                                int rownTSubLevel = Int32.Parse(row["nTSubLevel"].ToString());
                                string rownTSubLevel2 = row["nTSubLevel2"].ToString();
                                TransactionPlaneList(rowTermSubLv2);
                                TransactionHoliday(rowTermSubLv2);
                                TransactionSchedule(rowTermSubLv2);
                                TransactionTermSublevel2(rowTermSubLv2, rownTSubLevel, rownTSubLevel2, _newTermID);
                            }
                            Response.Write("success");
                        }
                        catch
                        {
                            Response.Write("error");
                        }
                        Response.End();
                        break;
                    case "leaver":
                        if (Request.QueryString["stdID"] != null)
                        {
                            string _id = Request.QueryString["stdID"].ToString();
                            string _typeid = "";
                            if (_id.IndexOf("E") != -1)
                            {
                                _id = _id.Replace("E", "");
                                _typeid = "E";
                            }
                            else
                            {
                                _id = _id.Replace("U", "");
                                _typeid = "U";
                            }
                            int sIDLeaver = Int32.Parse(_id);



                            string detailLeave = Request.Form["detailLeave"];
                            string leaveType = Request.Form["typeLeave"];
                            string leaveTime = Request.Form["typeTime"];
                            string tStart = Request.Form["tstart"];
                            string tEnd = Request.Form["tend"];

                            DateTime dateLeave = DateTime.ParseExact(Request.Form["dateLeave"].ToString() + " " + tStart,
                                    "dd/MM/yyyy HH:mm",
                                    new CultureInfo("en-US"));

                            DateTime dateendLeave = DateTime.ParseExact(Request.Form[leaveTime == "0" ? "dateLeave" : "dateendLeave"].ToString() + " " + tEnd,
                                     "dd/MM/yyyy HH:mm",
                                     new CultureInfo("en-US"));

                            using (JabJaiEntities _dbLeave = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                            {

                                DataTable dtLeaver = fcommon.LinqToDataTable(_dbLeave.TLeaves.OrderByDescending(w => w.LeaveID));

                                //int nLeaver;
                                //if (dtLeaver != null)
                                //{
                                //    nLeaver = Int32.Parse(dtLeaver.Rows[0]["nLeave"].ToString());
                                //    nLeaver += 1;
                                //}
                                //else
                                //{
                                //    nLeaver = 1;
                                //}
                                TLeave _AddLeave = new TLeave();
                                //_AddLeave.LeaveID = nLeaver;
                                _AddLeave.LeaveStatus = "0";
                                _AddLeave.cLeaveTime = leaveTime;
                                _AddLeave.cTypeLeave = leaveType;
                                _AddLeave.dLeaveStart = dateLeave;
                                _AddLeave.dLeaveEnd = dateendLeave;
                                _AddLeave.sLeave = detailLeave;
                                _AddLeave.sID = sIDLeaver;
                                _AddLeave.cTypeID = _typeid;
                                _AddLeave.SchoolID = userData.CompanyID;

                                _db.TLeaves.Add(_AddLeave);
                                _db.SaveChanges();
                                Response.Write("success");
                            }
                        }
                        else
                        {
                            Response.Write("พบข้อผิดพลาด");
                        }
                        Response.End();
                        break;
                }
            }
        }

        public string TransactionTerm(int nCurrentYear, int numberTerm)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.TermStatus == "1").ToList().ForEach(f => { f.cDel = "1"; f.TermStatus = "0"; });
                _db.SaveChanges();

                String newTermID = GenID("TS", "TTerm", 8).ToString();

                TTerm _newTerm = new TTerm();
                _newTerm.nTerm = newTermID;
                _newTerm.nYear = nCurrentYear;
                _newTerm.numberTerm = numberTerm;
                _newTerm.TermStatus = "1";
                _newTerm.cDel = "0";
                _newTerm.SchoolID = userData.CompanyID;

                _db.TTerms.Add(_newTerm);
                _db.SaveChanges();
                return newTermID;
            }
        }

        public void TransactionTermSublevel2(int termsLv2, int sublv, string sublv2, string newTermData)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == termsLv2).ToList().ForEach(f => f.nTermSubLevel2Status = "0");
                _db.SaveChanges();

                //int currentTermLv2 = _db.TTermSubLevel2.Count() + 1;
                TTermSubLevel2 _newTermSubLV2 = new TTermSubLevel2();
                //_newTermSubLV2.nTermSubLevel2 = currentTermLv2;
                _newTermSubLV2.nTerm = newTermData;
                _newTermSubLV2.nTSubLevel = sublv;
                _newTermSubLV2.nTSubLevel2 = sublv2;
                _newTermSubLV2.nTermSubLevel2Status = "1";
                _newTermSubLV2.SchoolID = userData.CompanyID;

                _db.TTermSubLevel2.Add(_newTermSubLV2);
                _db.SaveChanges();

                _db.TStudentLevels.Where(w => w.nTermSubLevel2 == termsLv2).ToList().ForEach(f => f.nTermSubLevel2 = _newTermSubLV2.nTermSubLevel2);
                _db.SaveChanges();
            }
        }

        public void TransactionPlaneList(int termsLv2)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                _db.TPlanLists.Where(w => w.TTermSublv == termsLv2).ToList().ForEach(f => f.cDel = "1");
                _db.SaveChanges();
            }
        }

        public void TransactionSchedule(int termsLv2)
        {
            //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //_db.TSchedules.Where(w => w.TermSublv == termsLv2).ToList().ForEach(f => f.cDel = "1");
            //_db.SaveChanges();
        }

        public void TransactionHoliday(int termsLv2)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                DateTime DateTimeCurrent = DateTime.Now;
                _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sHolidayType == "1" && w.dHolidayStart <= DateTimeCurrent).ToList().ForEach(f => f.cDel = "1");
                _db.SaveChanges();
            }
        }

        public void InsertNewHolidaySome(string nHolidays, int SubLevel)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                THolidaySome _HolidayS = new THolidaySome();
                DateTime dt = DateTime.Now;
                //_HolidayS.nHolidaySomeID = GenID("HS", "THolidaySome", 48).ToString();
                _HolidayS.nHoliday = nHolidays;
                _HolidayS.nTSubLevel = SubLevel;
                _HolidayS.SchoolID = userData.CompanyID;

                _db.THolidaySomes.Add(_HolidayS);
                _db.SaveChanges();
            }
        }

        public void InsertNewYears()
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                DateTime dt = DateTime.Now;
                _db.TYears.Add(new TYear
                {
                    //nYear = _db.TYears.Count() + 1,
                    numberYear = dt.Year + 543,
                    YearStatus = "1",
                    SchoolID = userData.CompanyID
                });
                _db.SaveChanges();
            }
        }

        public string InsertNewTerm(int newTerm)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                TTerm _Term = new TTerm();
                DataTable _dt = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.YearStatus == "1"));
                DataRow _dr = _dt.Rows[0];
                String newsTerm = GenID("TS", "TTerm", 8).ToString();
                _Term.nTerm = newsTerm;
                // Response.Write();
                _Term.nYear = Convert.ToInt32(_dr["nYear"]);
                _Term.numberTerm = 1;
                _Term.TermStatus = "1";
                _Term.cDel = "0";
                _Term.SchoolID = userData.CompanyID;

                _db.TTerms.Add(_Term);
                _db.SaveChanges();
                return newsTerm;
            }
        }

        public void InsertSubLevel2(int Sublv, int perSublv2, String nTerm)
        {
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            //for (int i = 1; i <= perSublv2; i++)
            //{
            //    TTermSubLevel2 _SubLevel2 = new TTermSubLevel2();
            //    // DateTime dt = new DateTime();
            //    _SubLevel2.nTermSubLevel2 = _db.TTermSubLevel2.Count() + 1;
            //    _SubLevel2.nTerm = nTerm;
            //    _SubLevel2.nTSubLevel = Sublv;
            //    _SubLevel2.nTSubLevel2 = i;
            //    _SubLevel2.nTermSubLevel2Status = "1";
            //    _db.TTermSubLevel2.Add(_SubLevel2);
            //    _db.SaveChanges();
            //}
        }

        private string GenID(string txtFront, string nameTable, int lengthDB)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string nID = "";
                switch (txtFront)
                {
                    case "TS":
                        if (_db.TTerms.Count() > 0) { nID = "TS" + (_db.TTerms.Count() + 1).ToString().PadLeft(lengthDB, '0'); }
                        else { nID = "TS00000001"; }
                        break;
                    case "HS":
                        nID = "HS" + (DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") + _db.THolidaySomes.Count() + 1).ToString().PadLeft(lengthDB, '0');
                        // if (_db.THolidaySomes.Count() > 0) { nID = "HS" + (DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff")+_db.THolidaySomes.Count() + 1).ToString().PadLeft(lengthDB, '0'); }
                        // else { nID = "HS00000000000000000000000000000000000000000000001"; }
                        break;
                }
                return nID;
            }
        }
    }
}