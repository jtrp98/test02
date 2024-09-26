using JabjaiMainClass;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterEntity;

namespace FingerprintPayment
{
    public class Student
    {
        private string studentname { get; set; }
        private string studentlevel2 { get; set; }
        private string studentlevel { get; set; }
        private int studentid { get; set; }

        public List<Student> getStudent(string sEntities)
        {
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
            {
                var tUser = (from a in db.TUsers.Where(w => w.cDel == null).ToList()
                             join b in db.TTermSubLevel2.ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.ToList() on b.nTSubLevel equals c.nTSubLevel
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
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
            {
                var tUser = (from a in db.TUsers.Where(w => w.cDel == null).ToList()
                             join b in db.TTermSubLevel2.ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.ToList() on b.nTSubLevel equals c.nTSubLevel
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
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
            {
                var tUser = (from a in db.TUsers.Where(w => w.cDel == null).ToList()
                             join b in db.TTermSubLevel2.ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                             join c in db.TSubLevels.ToList() on b.nTSubLevel equals c.nTSubLevel
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