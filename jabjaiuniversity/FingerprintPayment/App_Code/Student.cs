using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.App_Code
{
    internal class Student
    {
        private string studentname { get; set; }
        private string studentlevel2 { get; set; }
        private string studentlevel { get; set; }
        private int studentid { get; set; }

        private JWTToken.userData userData = new JWTToken.userData();
        public List<Student> getStudent(string sEntities)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                var tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null)
                             join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                             select new Student
                             {
                                 studentname = a.sName + " " + a.sLastname,
                                 studentlevel = c.SubLevel.Trim() + " / " + b.nTSubLevel2,
                                 studentlevel2 = c.SubLevel.Trim(),
                                 studentid = a.sID
                             }).ToList();
                return tUser;
            }
        }

        public List<Student> getStudent4level(string sEntities, int LevelId)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                var tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null)
                             join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                             where c.nTLevel == LevelId
                             select new Student
                             {
                                 studentname = a.sName + " " + a.sLastname,
                                 studentlevel = c.SubLevel.Trim() + " / " + b.nTSubLevel2,
                                 studentid = a.sID
                             }).ToList();

                return tUser;
            }
        }

        public List<Student> getStudent4level2(string sEntities, int Level2Id)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                var tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null)
                             join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                             where b.nTermSubLevel2 == Level2Id
                             select new Student
                             {
                                 studentname = a.sName + " " + a.sLastname,
                                 studentlevel = c.SubLevel.Trim() + " / " + b.nTSubLevel2,
                                 studentid = a.sID
                             }).ToList();

                return tUser;
            }
        }
    }

}