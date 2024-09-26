using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.LINE
{
    public partial class LINEMessageReply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
            {
                string sqlQuery = string.Format(@"
SELECT TOP 50 sms.nSMS, sms.SMSTitle, sms.SMSDesp, sms.nActionType, mb.nMessageID, mu.R1, mu.R0, mu.RNULL, 
(CASE 
	WHEN sms.nActionType = 3 THEN 'เห็นด้วย' 
	WHEN sms.nActionType = 4 THEN 'ยินยอม' 
	WHEN sms.nActionType = 5 THEN 'สนใจ' 
	WHEN sms.nActionType = 6 THEN 'ชอบ' 
	WHEN sms.nActionType = 7 THEN 'สนใจ' 
	ELSE '' END) 'R1M', 
(CASE 
	WHEN sms.nActionType = 1 THEN 'รับทราบ' 
	WHEN sms.nActionType = 2 THEN 'ยืนยัน' 
	WHEN sms.nActionType = 3 THEN 'ไม่เห็นด้วย' 
	WHEN sms.nActionType = 4 THEN 'ไม่ยินยอม' 
	WHEN sms.nActionType = 5 THEN 'ไม่สนใจ' 
	WHEN sms.nActionType = 6 THEN 'ไม่ชอบ' 
	WHEN sms.nActionType = 7 THEN 'สนใจมาก' 
	ELSE '' END) 'R0M'
FROM TSMS sms 
LEFT JOIN TMessageBox mb ON sms.nSMS = mb.push_id
LEFT JOIN
(
	SELECT message_id, SUM(CASE WHEN nActionResult = 1 THEN 1 ELSE 0 END) 'R1'
	, SUM(CASE WHEN nActionResult = 0 THEN 1 ELSE 0 END) 'R0'
	, SUM(CASE WHEN nActionResult IS NULL THEN 1 ELSE 0 END) 'RNULL'
	FROM TMessage_User 
	GROUP BY message_id
) mu ON mb.nMessageID = mu.message_id
WHERE sms.isDel IS NULL AND sms.nActionType IS NOT NULL
ORDER BY sms.nSMS DESC
");

                List<EntityLINEMessageReply> result = en.Database.SqlQuery<EntityLINEMessageReply>(sqlQuery).ToList();
                foreach (var r in result)
                {
                    ltrMessageReply.Text += string.Format(@"
<tr>
    <td class=""text-center"">{0}</td>
    <td class=""text-center"">{1}</td>
    <td>{2}</td>
    <td>
        <p class=""truncate"" style=""margin: 0px;"">{3}</p>
    </td>
    <td class=""text-center"">
        <p class=""result"">{7}</p>
        {4}</td>
    <td class=""text-center"">
        <p class=""result"">{8}</p>
        {5}</td>
    <td class=""text-center"">
        <p class=""result"">{9}</p>
        {6}</td>
</tr>", r.nSMS, r.nMessageID, r.SMSTitle, r.SMSDesp, r.R1, r.R0, r.RNULL, r.R1M, r.R0M, (r.nActionType == 0 ? "" : "ยังไม่ตอบ"));
                }
            }
        }
    }

    public class EntityLINEMessageReply
    {
        public int nSMS { get; set; }
        public string SMSTitle { get; set; }
        public string SMSDesp { get; set; }
        public int nActionType { get; set; }
        public int nMessageID { get; set; }
        public int R1 { get; set; }
        public int R0 { get; set; }
        public int RNULL { get; set; }
        public string R1M { get; set; }
        public string R0M { get; set; }
    }

}