using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.App_Code
{
    internal class student4day
    {
        public string studentname { get; set; }
        public string studentlastname { get; set; }
        public int studentlevel2id { get; set; }
        public string studentlevel2 { get; set; }
        public int studentlevelid { get; set; }
        public string studentlevel { get; set; }
        public int studentid { get; set; }
        public DateTime dayscan { get; set; }
        public string sex { get; set; }

        private JWTToken.userData userData = new JWTToken.userData();
        public List<student4day> getListStudent(string sEntities, DateTime dStart, DateTime dEnd)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            List<student4day> _student4day = new List<student4day>();
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                //var quser = db.TUsers.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                //var qlevel2 = db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();
                //var qlevel = db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();

                var lUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                             join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                             select new
                             {
                                 sname = a.sName,
                                 studentlastname = a.sLastname,
                                 studentlevel2id = b.nTermSubLevel2,
                                 studentlevel2 = c.SubLevel.Trim() + "/" + b.nTSubLevel2,
                                 studentlevel = c.SubLevel.Trim(),
                                 studentid = a.sID,
                                 studentlevelid = c.nTSubLevel,
                                 sex = a.cSex
                             }).ToList();

                for (int i = 0; dStart <= dEnd.AddDays(-i); i++)
                {
                    DateTime _dayscan = dEnd.AddDays(-i);
                    foreach (var datauser in lUser)
                    {
                        _student4day.Add(new student4day
                        {
                            dayscan = _dayscan,
                            studentid = datauser.studentid,
                            studentlastname = datauser.studentlastname,
                            studentlevel = datauser.studentlevel,
                            studentlevelid = datauser.studentlevelid,
                            studentlevel2 = datauser.studentlevel2,
                            studentlevel2id = datauser.studentlevel2id,
                            studentname = datauser.sname,
                            sex = datauser.sex
                        });
                    }
                }

                return _student4day;
            }
        }
    }
}