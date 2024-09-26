using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FingerprintPayment.App_Code.StdCallingHub;

namespace FingerprintPayment.StudentCall
{
    public partial class PrintCard : BaseStudentCall
    {
        public CardModelMain ModelPage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var type = Request.QueryString["type"] + "";
            var level1 = Request.QueryString["lvl1"] + "";
            var level2 = Request.QueryString["lvl2"] + "";
            var sid = Request.QueryString["sid"] + "";
            var no = Request.QueryString["no"] + "";

            //if (type == "all" && level2 == "")
            //{
            //    ModelPage = new CardModelMain();
            //    return;
            //}

            using (var ctx = Connection.MasterEntities(ConnectionDB.Read))
            {
                var school = (from a in ctx.TCompanies.Where(o => o.nCompany == UserData.CompanyID)
                              from b in ctx.TStudentCall_Config.Where(o => o.SchoolId == a.nCompany).DefaultIfEmpty()
                              select new CardModelMain
                              {
                                  NameEN = a.sNameEN,
                                  NameTH = a.sCompany,
                                  Logo = a.sImage,
                                  Bg = b.BgCard,
                                  IsLevel = b.IsShowLevel,
                                  IsLastName = b.IsShowLastName,
                                  IsParent = b.IsShowParent,
                                  CardType = b.CardType,
                                  NameType = b.NameType
                              })
                             .FirstOrDefault();
                
                TTerm currentTerm;
                using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    StudentLogic logic = new StudentLogic(dbschool);
                    currentTerm = logic.GetTermDATA(DateTime.Today, new JWTToken.userData { CompanyID = UserData.CompanyID });
                }

                string sql = $@"-- StudentCall/PrintCard


SELECT A.SchoolID ,A.sID
, B.sNickName 'NickName' , A.titleDescription 'Title', A.sName 'FirstName' , A.sLastname  'LastName'   
,A.sStudentNameEN ,A.sStudentLastEN
, A.ClassFullName 'Level1', A.nTSubLevel2 'Level2' 
, A.sStudentID 'Code' , B.sStudentPicture 'Img'
, T.P{no}  'Parent'
, C.LevelName
FROM [JabjaiSchoolSingleDB].[dbo].[TB_StudentViews] A
JOIN  JabjaiSchoolSingleDB.dbo.TUser B ON A.sID = B.sID AND A.SchoolID = B.SchoolID
JOIN  JabjaiSchoolSingleDB.dbo.TLevel C ON A.SchoolID = C.SchoolID AND A.nTLevel = C.LevelID
LEFT JOIN (
SELECT c.sID , '' 'P'
	, MAX(CASE WHEN c.[No] = 1 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P1'
	, MAX(CASE WHEN c.[No] = 2 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P2'
	, MAX(CASE WHEN c.[No] = 3 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P3'	
	FROM JabjaiMasterSingleDB.dbo.TParent_Card c 
	LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TFamilyProfile] b on c.sid = b.sid
	WHERE c.SchoolID={UserData.CompanyID} AND c.IsDel=0  AND c.No = {no}  --AND ISNULL(c.NFC, '') <> '' --AND c.IsActive = 1 
	GROUP BY c.sID
)T ON A.sID = T.sID

WHERE A.SchoolID = {UserData.CompanyID} 
AND ISNULL(A.cDel  ,'0')= '0'
AND A.nTerm = '{currentTerm?.nTerm ?? ""}'
  ";

                //if (type.ToLower() == "all" &&)
                if(!string.IsNullOrEmpty(level2))
                {
                    sql += " and A.nTermSubLevel2 = " + level2;
                }
                else
                {
                    if (!string.IsNullOrEmpty(level1))
                    {
                        sql += " and A.nTSubLevel = " + level1;
                    }
                }

                if (!string.IsNullOrEmpty(sid))
                {
                    sql += " and A.sID = " + sid;
                }

                var lst = ctx.Database.SqlQuery<CardList>(sql).ToList();

                school.Cards = lst;

                ModelPage = school;
            }
        }

        public class CardModelMain
        {
            public string NameEN { get; internal set; }
            public string NameTH { get; internal set; }
            public string Logo { get; internal set; }
            public List<CardList> Cards { get; internal set; }
            public string Bg { get; internal set; }
            public bool? IsLevel { get; internal set; }
            public bool? IsLastName { get; internal set; }
            public bool? IsParent { get; internal set; }
            public byte? CardType { get; internal set; }
            public byte? NameType { get; internal set; }
        }

        public class CardList
        {
            public int SchoolID { get; set; }
            public string NickName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string sStudentNameEN { get; set; }
            public string sStudentLastEN { get; set; }
            public string Code { get; set; }
            public string Parent { get; set; }
            public int sID { get; internal set; }
            public int? nTermSubLevel2 { get; internal set; }
            public string Level { get; internal set; }
            public string Level1 { get; internal set; }
            public string Level2 { get; internal set; }
            public string Img { get; internal set; }
            public string Title { get; internal set; }
            public string LevelName { get; internal set; }

        }
    }
}