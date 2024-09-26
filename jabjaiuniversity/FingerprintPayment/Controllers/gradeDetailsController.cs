using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    public class gradeDetailsController : ApiController
    {
        // GET api/<controller>
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<gradeDetail> GetschoolCalendar([FromUri]int userid, string term)
        {

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {

                var quser = _dbMaster.TUsers.Where(w => w.sID == userid).FirstOrDefault();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.nCompany == quser.nCompany);

                JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(qcompany.sEntities, ConnectionDB.Read));
                var qstudent = _db.TUser.Where(w => w.sID == quser.nSystemID).FirstOrDefault();

                List<gradeDetail> gradeDetail = new List<gradeDetail>();
                gradeDetail x = new gradeDetail();
                List<termgrade> termgradeList = new List<termgrade>();
                termgrade termgrade = new termgrade();

                var findyear = _db.TTerms.Where(w => w.nTerm == term).FirstOrDefault();

                int termcount = 0;
                Double? totalcredit = 0;
                Double? totalgrade = 0;

                foreach (var a in _db.TTerms.Where(w => w.nYear == findyear.nYear && w.cDel == null).ToList())
                {
                    Double? grade = 0;
                    Double? credit = 0;
                    termcount = termcount + 1;
                    var b = _db.TSchoolRecords.Where(w => w.nTsudentId == userid && w.nTerm == a.nTerm).FirstOrDefault();
                    if (b != null)
                    {
                        foreach (var c in _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == b.nSchoolRecordId).ToList())
                        {
                            int gg = Int32.Parse(c.Grade);
                            var planname = _db.TPlanes.Where(w => w.sPlaneID == c.sPlaneID).FirstOrDefault();

                            grade = grade + (gg * planname.nCredit.Value);
                            credit = credit + planname.nCredit;
                        }
                    }
                    totalcredit = totalcredit + credit;
                    totalgrade = totalgrade + grade;

                    termgrade = new termgrade();
                    termgrade.credit = credit;
                    termgrade.grade = grade;
                    termgradeList.Add(termgrade);

                }

                int termcount2 = 0;
                foreach (var a in _db.TTerms.Where(w => w.nYear == findyear.nYear && w.cDel == null).ToList())
                {

                    var b = _db.TSchoolRecords.Where(w => w.nTsudentId == userid && w.nTerm == a.nTerm).FirstOrDefault();
                    if (b != null)
                    {
                        foreach (var c in _db.TSchoolRecord_Detail.Where(w => w.nSchoolRecordId == b.nSchoolRecordId).ToList())
                        {
                            x = new gradeDetail();
                            var planname = _db.TPlanes.Where(w => w.sPlaneID == c.sPlaneID).FirstOrDefault();
                            x.scoreGet = c.Score;
                            if (c.ReGrade != null && c.ReGrade != "")
                                x.scoreGet = c.ReGrade;
                            x.scoreMax = c.MaxScore;
                            x.subjectCredit = planname.nCredit;
                            x.subjectId = planname.sPlaneID.ToString();
                            x.subjectName = planname.sPlaneName;
                            x.gradeGet = c.Grade;
                            Double? calculate = termgradeList[termcount2].grade / termgradeList[termcount2].credit;
                            x.gradeTerm = calculate.ToString();
                            Double? calculate2 = totalgrade / totalcredit;
                            x.gradeYear = calculate2.ToString();
                            if (a.nTerm.Trim() == term.Trim())
                            {
                                gradeDetail.Add(x);
                            }

                        }
                    }

                    termcount2 = termcount2 + 1;
                }




                return gradeDetail;
            }
        }

        public class gradeDetail
        {
            public String subjectId { get; set; }
            public String subjectName { get; set; }
            public Double? subjectCredit { get; set; }
            public string scoreMax { get; set; }
            public String scoreGet { get; set; }
            public String gradeGet { get; set; }
            public String gradeTerm { get; set; }
            public String gradeYear { get; set; }

        }
        public class termgrade
        {

            public Double? grade { get; set; }
            public Double? credit { get; set; }

        }
    }
}
