using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline01 : RegisterGateway
    {
        protected string ExplanationData = "";

        protected string registerEnable = "";
        protected string printEnable = "";
        protected string registerYearBE = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterOnlineEntities"] != null)
            {
                int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (Session["RegisterOnlineYearBE"] != null)
                    {
                        registerYearBE = Convert.ToString(Session["RegisterOnlineYearBE"]);
                    }

                    string sqlQuery = string.Format(@"
SELECT reg.RegisterRegularityID 'ID', sub.SubLevel, reg.Filename
FROM TRegisterRegularity reg 
LEFT JOIN TSubLevel sub ON reg.nTSubLevel = sub.nTSubLevel AND reg.SchoolID = sub.SchoolID
LEFT JOIN TLevel lev ON sub.nTLevel = lev.LevelID AND sub.SchoolID = lev.SchoolID
LEFT JOIN (
	SELECT ROW_NUMBER() OVER(PARTITION BY nTSubLevel ORDER BY Year DESC) 'No', * 
	FROM TRegisterSetup 
	WHERE SchoolID = {0} AND cDel = 0 AND Year >= YEAR(DATEADD(YEAR, 543, GETDATE())) AND EndDate >= GETDATE()
) rs ON reg.nTSubLevel = rs.nTSubLevel AND reg.SchoolID = rs.SchoolID AND rs.No = 1
WHERE reg.SchoolID = {0} AND reg.cDel = 0 --AND rs.EndDate >= GETDATE()
ORDER BY lev.sortValue, sub.SubLevel", schoolID);
                    List<EntityRegularitySubLevel> result = en.Database.SqlQuery<EntityRegularitySubLevel>(sqlQuery).ToList();

                    if (result.Count > 0)
                    {
                        int rowIndex = 1;
                        foreach (var r in result)
                        {
                            this.ltrDocument.Text += string.Format(@"
<tr>
    <td>{0}</td>
    <td class=""text-center""><i class=""material-icons text-info"">description</i> ระเบียบการชั้น {1}</td>
    <td>
        <button class=""btn btn-info btn-sm btn-round"" style=""font-size: 0.85rem;"" onclick=""window.open('{2}', '_blank'); return false;"">ดาวน์โหลด</button></td>
</tr>", rowIndex, r.SubLevel, r.Filename);

                            rowIndex++;
                        }
                    }

                    var explanation = en.TRegisterExplanations.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                    if (explanation != null)
                    {
                        ExplanationData = explanation.Description;
                    }

                    if (string.IsNullOrEmpty(this.ltrDocument.Text) && string.IsNullOrEmpty(ExplanationData))
                    {
                        registerEnable = @"disabled=""disabled""";
                        printEnable = @"disabled=""disabled""";
                    }
                }
            }
        }
    }

}