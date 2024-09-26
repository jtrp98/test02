using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.LINE
{
    public partial class LINESummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiMasterEntities dbMaster = Connection.MasterEntities();

            var schools = (from t in dbMaster.LINEUsers
                           group t by new
                           {
                               t.SchoolID
                           } into g
                           select new
                           {
                               SchoolID = (System.Int32?)g.Key.SchoolID
                           }).ToList();

            foreach (var school in schools)
            {
                var schoolMasterData = dbMaster.TCompanies.Where(w => w.nCompany == school.SchoolID).FirstOrDefault();
                var schoolLogo = string.IsNullOrEmpty(schoolMasterData.sImage) ? "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" : schoolMasterData.sImage;
                var schoolName = schoolMasterData.sCompany;

                var schoolData = dbMaster.LINEUsers.Where(w => w.SchoolID == school.SchoolID).ToList();
                int lineUserAll = schoolData.Count;
                int lineUserUnique = schoolData.Select(s => s.LINEUserID).Distinct().Count();
                int studentAll = schoolData.Count;
                int studentUnique = schoolData.Select(s => s.StudentID).Distinct().Count();

                string schoolCard = string.Format(@"<div class=""card shadow-sm rounded"" style=""width: 500px; display: inline-block; margin-left: 5px;"">
            <div class=""row no-gutters"">
                <div class=""col-md-3"">
                    <img src=""{0}"" class=""card-img-top h-100"" alt=""..."" style=""width: 100px; height: 100px !important; top: 50%; margin-top: -50px; position: absolute; left: 50%; margin-left: -40px;"">
                </div>
                <div class=""col-md-9"">
                    <div class=""card-body"">
                        <h5 class=""card-title truncate"">{1}</h5>
                        <div class=""row detail"">
                            <div class=""col-6"">
                                <p class=""card-text"">LINE User</p>
                                <div class=""row"">
                                    <div class=""col-7"">
                                        <p class=""card-text""><i class=""fa fa-users"" aria-hidden=""true""></i>All</p>
                                    </div>
                                    <div class=""col-5"">
                                        <p class=""card-text"">{2}</p>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <div class=""col-7"">
                                        <p class=""card-text""><i class=""fa fa-user"" aria-hidden=""true""></i>Unique</p>
                                    </div>
                                    <div class=""col-5"">
                                        <p class=""card-text"">{3}</p>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-6"">
                                <p class=""card-text"">Student</p>
                                <div class=""row"">
                                    <div class=""col-7"">
                                        <p class=""card-text""><i class=""fa fa-users"" aria-hidden=""true""></i>All</p>
                                    </div>
                                    <div class=""col-5"">
                                        <p class=""card-text"">{4}</p>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <div class=""col-7"">
                                        <p class=""card-text""><i class=""fa fa-user"" aria-hidden=""true""></i>Unique</p>
                                    </div>
                                    <div class=""col-5"">
                                        <p class=""card-text"">{5}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>", schoolLogo, schoolName, lineUserAll, lineUserUnique, studentAll, studentUnique);

                ltrSummary.Text += schoolCard;
            }

        }
    }
}