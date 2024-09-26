using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplateBuilder;

namespace FingerprintPayment.studentCard
{
    public partial class studentCardPrintAll2 : System.Web.UI.Page
    {
        public float Height { get; set; } = 8.5f;
        public float Width { get; set; } = 5.4f;

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var schoolID = userData.CompanyID;


                var cardInfo = _db.TStudentCardInfoes.Where(w => w.SchoolID == schoolID).ToList();

                var cardType = cardInfo.Where(w => w.elementName == "NewCardType").FirstOrDefault();
            
                using (var context = new TemplateBuilderContext())
                {
                    var form = context.TblReportForm
                        .Where(o => o.nFormID + "" == cardType.elementValue)
                        .FirstOrDefault();

                    if (form != null)
                    {
                        if (form.sPaperSize == "Card-Portrait")
                        {
                            Height = 485f;
                            Width = 310f;
                        }
                        else if (form.sPaperSize == "Card-Landscape")
                        {
                            Width = 485f;
                            Height = 310f;
                        }
                    }

                }
            }
                }
            }
}