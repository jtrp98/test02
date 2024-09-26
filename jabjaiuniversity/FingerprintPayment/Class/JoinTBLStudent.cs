using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json.Linq;
using JabjaiMainClass;

namespace FingerprintPayment.Class
{

    class JoinDATAtable
    {

        public StudentData student(JabJaiEntities entities, int studentId, int SchoolId)
        {
            var studentData = (from a in entities.TUser.Where(w => w.SchoolID == SchoolId)
                               join b in entities.TTermSubLevel2.Where(w => w.SchoolID == SchoolId) on a.nTermSubLevel2 equals b.nTermSubLevel2
                               join c in entities.TSubLevels.Where(w => w.SchoolID == SchoolId) on b.nTSubLevel equals c.nTSubLevel
                               
                               where a.cDel == null && a.sID == studentId

                               select new StudentData
                               {
                                   studentID = a.sID,
                                   studentName = a.sName,
                                   studentLastname = a.sLastname,
                                   sStudentID = a.sStudentID,
                                   studentStatus = a.nStudentStatus,
                                   studentClass = c.SubLevel + "/" + b.nTSubLevel2,
                                   professorOfclass = 1111,
                                   studentNumber = a.nStudentNumber,
                                   studentSEX = a.cSex,
                                   studentPic = a.sStudentPicture
                               }).FirstOrDefault();

            return studentData;
        }


    }



    public class Question_Point
    {
        public int SID { get; set; }
        public int SDQ_ID { get; set; }
        public int? SDQ_POINT { get; set; }
        public int Question_ID { get; set; }
        public string Question_DES { get; set; }
    }


    public class StudentData
    {
        public int studentID { get; set; }
        public string studentName { get; set; }
        public string studentLastname { get; set; }
        public string sStudentID { get; set; }
        public int? studentStatus { get; set; }
        public string studentClass { get; set; }
        public int? professorOfclass { get; set; }
        public int? studentNumber { get; set; }
        public string studentSEX { get; set; }
        public string studentPic { get; set; }
    }
}