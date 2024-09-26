using JabjaiMainClass;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterEntity;

namespace FingerprintPayment
{
    public class student4day
    {
        public string studentname { get; set; }
        public string studentlastname { get; set; }
        public int studentlevel2id { get; set; }
        public string studentlevel2 { get; set; }
        public int studentlevelid { get; set; }
        public string studentlevel { get; set; }
        public int studentid { get; set; }
        public DateTime dayscan { get; set; }

        public List<student4day> getListStudent(string sEntities, DateTime dStart, DateTime dEnd)
        {
            List<student4day> _student4day = new List<student4day>();
            JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities));
            var lUser = (from a in db.TUsers.Where(w => w.cDel == null).ToList()
                         join b in db.TTermSubLevel2.ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                         join c in db.TSubLevels.ToList() on b.nTSubLevel equals c.nTSubLevel
                         select new
                         {
                             sname = a.sName,
                             studentlastname = a.sLastname,
                             studentlevel2id = b.nTermSubLevel2,
                             studentlevel2 = c.SubLevel.Trim() + "/" + b.nTSubLevel2,
                             studentlevel = c.SubLevel.Trim(),
                             studentid = a.sID,
                             studentlevelid = c.nTSubLevel
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
                        studentname = datauser.sname
                    });
                }
            }

            return _student4day;
        }
    }
}