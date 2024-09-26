using FingerprintPayment.ClassOnline.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using static FingerprintPayment.App_Code.StdCallingHub;

namespace FingerprintPayment.StudentCall.Handler
{
    /// <summary>
    /// Summary description for UploadFileBG
    /// </summary>
    public class PCard : IHttpHandler, IRequiresSessionState
    {
        public class PCardModel
        {
            public int sID { get; internal set; }
            public string Code { get; set; }
            public string Title { get; set; }
            public string NickName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Card1 { get; set; }
            public string Barcode1 { get; set; }
            public string EnCard1 { get; set; }
            public string Type1 { get; set; }
            public string Status1 { get; set; }
            public string Parent1 { get; internal set; }


            public string Card2 { get; set; }
            public string Barcode2 { get; set; }
            public string EnCard2 { get; set; }
            public string Type2 { get; set; }
            public string Status2 { get; set; }
            public string Parent2 { get; internal set; }

            public string Card3 { get; set; }
            public string Barcode3 { get; set; }
            public string EnCard3 { get; set; }
            public string Type3 { get; set; }
            public string Status3 { get; set; }
            public string Parent3 { get; internal set; }

            public string ParentAll { get; internal set; }
        }

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();

            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            var lvl1 = int.Parse(context.Request.QueryString["lvl1"] + "");
            var lvl2 = int.Parse(context.Request.QueryString["lvl2"] + "");
            var name = context.Request.QueryString["name"] + "";
            var schoolId = userData.CompanyID;


            //            string sql = $@"-- StudentCall/Handler/PCard.ashx
            //SELECT A.sID
            //, A.sNickName 'NickName' , ISNULL(F.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
            //, ISNULL(C.NFC1, '')+','+ISNULL(C.NFC2, '')+','+ISNULL(C.NFC3, '') 'Card'
            //, ISNULL(C.IsActive1, '')+','+ISNULL(C.IsActive2, '')+','+ISNULL(C.IsActive3, '') 'Status' 
            //, ISNULL(C.ParentName1, '')+','+ISNULL(C.ParentName2, '')+','+ISNULL(C.ParentName3, '') 'ParentName'

            //FROM JabjaiSchoolSingleDB.dbo.TUser A
            //LEFT JOIN
            //(
            //	SELECT  c.sID 
            //	, MAX(CASE WHEN c.[No] = 1 THEN c.ParentName END) 'ParentName1', MAX(CASE WHEN c.[No] = 2 THEN c.ParentName END) 'ParentName2', MAX(CASE WHEN c.[No] = 3 THEN c.ParentName END) 'ParentName3'
            //	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1', MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2', MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'
            //	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1', MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2', MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
            //	FROM JabjaiMasterSingleDB.dbo.TParent_Card c 
            //	WHERE c.SchoolID={schoolId} AND c.IsDel=0 AND c.IsActive = 1 --AND ISNULL(c.NFC, '') <> ''
            //	GROUP BY c.sID
            //) C ON  A.sID = C.sID 

            //INNER JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 D on A.SchoolID = D.SchoolID and A.nTermSubLevel2 = D.nTermSubLevel2
            //INNER JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E on D.SchoolID = E.SchoolID and D.nTSubLevel = E.nTSubLevel
            //LEFT JOIN  JabjaiSchoolSingleDB.dbo.TTitleList F on A.SchoolID = F.SchoolID and A.sStudentTitle   = CAST( F.nTitleid as nvarchar)

            //WHERE A.SchoolID={schoolId} ";


            //            if (lvl2 != 0)
            //            {
            //                sql += " AND A.nTermSubLevel2 = " + lvl2;
            //            }

            //            if (!string.IsNullOrEmpty(name))
            //            {
            //                sql += $" AND (A.sStudentID + ' ' + A.sName + ' ' + A.sLastname) like '%{name}%' ";
            //            }

            TTerm currentTerm;
            using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                StudentLogic logic = new StudentLogic(dbschool);
                currentTerm = logic.GetTermDATA(DateTime.Today, new JWTToken.userData { CompanyID = schoolId });
            }


            var sql = $@"-- StudentCall/Handler/PCard.ashx
SELECT A.sID
 ,  ISNULL(F.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
--, E.fullName 'Level1', D.nTSubLevel2 'Level2' 
, A.sStudentID 'Code' 
  , ISNULL(C.NFC1, '') 'Card1' ,ISNULL(C.NFC2, '') 'Card2',ISNULL(C.NFC3, '') 'Card3'
  , ISNULL(C.ENNFC1, '') 'EnCard1' ,ISNULL(C.ENNFC2, '') 'EnCard2',ISNULL(C.ENNFC3, '') 'EnCard3'
  , ISNULL(C.Barcode1, '') 'Barcode1' ,ISNULL(C.Barcode2, '') 'Barcode2',ISNULL(C.Barcode3, '') 'Barcode3'
  , ISNULL(C.IsActive1, '') 'Status1' , ISNULL(C.IsActive2, '') 'Status2' , ISNULL(C.IsActive3, '') 'Status3'
  , ISNULL(C.P1, '') 'Parent1' ,ISNULL(C.P2, '') 'Parent2',ISNULL(C.P3, '') 'Parent3'
  , ISNULL(C.Type1, '') 'Type1' ,ISNULL(C.Type2, '') 'Type2',ISNULL(C.Type3, '') 'Type3'
  , C.ParentAll
FROM [JabjaiSchoolSingleDB].[dbo].[TB_StudentViews] A
--JabjaiSchoolSingleDB.dbo.TUser A 
--INNER JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 D on A.SchoolID = D.SchoolID and A.nTermSubLevel2 = D.nTermSubLevel2
--INNER JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E on D.SchoolID = E.SchoolID and D.nTSubLevel = E.nTSubLevel
--LEFT JOIN JabjaiMasterSingleDB.dbo.TParent_Card B on A.SchoolID = B.SchoolID and A.sID = B.sID  and B.IsDel = 0 and B.IsActive = 1 
LEFT JOIN  JabjaiSchoolSingleDB.dbo.TTitleList F on A.SchoolID = F.SchoolID and A.sStudentTitle = CAST( F.nTitleid as nvarchar)
LEFT JOIN (
	
	SELECT b.sID 
	, MAX(CASE WHEN c.[No] = 1 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P1'
	, MAX(CASE WHEN c.[No] = 2 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P2'
	, MAX(CASE WHEN c.[No] = 3 THEN 
	(CASE WHEN c.Type = 1 THEN b.sFatherFirstName + ' ' + b.sFatherLastName WHEN c.Type = 2 THEN b.sMotherFirstName + ' ' + b.sMotherLastName WHEN c.Type = 3 THEN b.sFamilyName + ' ' + b.sFamilyLast END) END) 'P3'
    , MAX(CASE WHEN c.[No] = 1 THEN CAST(c.[Type] AS VARCHAR(1)) END) 'Type1'
	, MAX(CASE WHEN c.[No] = 2 THEN CAST(c.[Type] AS VARCHAR(1)) END) 'Type2'
	, MAX(CASE WHEN c.[No] = 3 THEN CAST(c.[Type] AS VARCHAR(1)) END) 'Type3'

	, MAX(CASE WHEN c.[No] = 1 THEN c.NFC END) 'NFC1'
	, MAX(CASE WHEN c.[No] = 2 THEN c.NFC END) 'NFC2'
	, MAX(CASE WHEN c.[No] = 3 THEN c.NFC END) 'NFC3'

	, MAX(CASE WHEN c.[No] = 1 THEN c.NFCEncrypt END) 'ENNFC1'
	, MAX(CASE WHEN c.[No] = 2 THEN c.NFCEncrypt END) 'ENNFC2'
	, MAX(CASE WHEN c.[No] = 3 THEN c.NFCEncrypt END) 'ENNFC3'

	, MAX(CASE WHEN c.[No] = 1 THEN c.Barcode END) 'Barcode1'
	, MAX(CASE WHEN c.[No] = 2 THEN c.Barcode END) 'Barcode2'
	, MAX(CASE WHEN c.[No] = 3 THEN c.Barcode END) 'Barcode3'

	, MAX(CASE WHEN c.[No] = 1 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive1'
	, MAX(CASE WHEN c.[No] = 2 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive2'
	, MAX(CASE WHEN c.[No] = 3 THEN CAST(c.IsActive AS VARCHAR(1)) END) 'IsActive3'
	, MAX(ISNULL(b.sFatherFirstName,'') + ' ' + ISNULL(b.sFatherLastName,'') +','+ ISNULL(b.sMotherFirstName ,'')+ ' ' + ISNULL(b.sMotherLastName,'')+ ',' +  ISNULL(b.sFamilyName,'') + ' ' +  ISNULL(b.sFamilyLast,'')) 'ParentAll'
	FROM JabjaiMasterSingleDB.dbo.TParent_Card c 
	RIGHT JOIN [JabjaiSchoolSingleDB].[dbo].[TFamilyProfile] b on c.sid = b.sid and c.SchoolID = b.SchoolID
	WHERE b.SchoolID=  {schoolId}  
    --and c.SchoolID=  {schoolId} --AND c.IsDel=0 --AND c.IsActive = 1 --AND ISNULL(c.NFC, '') <> ''
	GROUP BY b.sID
) C ON A.sID = C.sID 
WHERE A.SchoolID = {schoolId}  
AND ISNULL(A.cDel  ,'0')= '0'
AND A.nTerm = '{currentTerm?.nTerm ?? ""}'

{(lvl2 > 0 ? " AND A.nTermSubLevel2 = " + lvl2 : "")} 
{(lvl1 > 0 ? " AND A.nTSubLevel = " + lvl1 : "")} 
{(!string.IsNullOrEmpty(name) ? " AND ( A.sName  + ' ' + A.sLastname like '%" + name + "%' OR  A.sStudentID like '%" + name + "%' )" : "")} 

";
            //{ (lvl2 > 0 ? " AND A.nTermSubLevel2 = " + lvl2 : (lvl1 > 0 ? " AND D.nTSubLevel = " + lvl1 : "") )} 
            using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))//OR A.sLastname like '%" + name + "%'  OR A.sStudentID LIKE '%" + name + "%' 
            {
                var lst = dbmaster.Database.SqlQuery<PCardModel>(sql).ToList();

                context.Response.ContentType = "application/json";
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = lst.Select((o, i) => new
                    {
                        no = i + 1,
                        sid = o.sID,
                        title = o.Title,
                        code = o.Code,
                        fullName = o.FirstName + " " + o.LastName,

                        type1 = o.Type1,
                        card1 = o.Card1,
                        encard1 = o.EnCard1,
                        status1 = o.Status1,
                        parent1 = o.Parent1,

                        type2 = o.Type2,
                        card2 = o.Card2,
                        encard2 = o.EnCard2,
                        status2 = o.Status2,
                        parent2 = o.Parent2,

                        type3 = o.Type3,
                        card3 = o.Card3,
                        encard3 = o.EnCard3,
                        status3 = o.Status3,
                        parent3 = o.Parent3,

                        barcode1 = o.Barcode1,
                        barcode2 = o.Barcode2,
                        barcode3 = o.Barcode3,
                        parentall = o.ParentAll,
                    })
                }));
            }



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
//if (context.Request.Files.Count > 0)
//{
//    HttpFileCollection files = context.Request.Files;
//    for (int i = 0; i < files.Count; i++)
//    {
//        HttpPostedFile file = files[i];
//        string fname = context.Server.MapPath("~/uploads/" + file.FileName);
//        file.SaveAs(fname);
//    }
//}
