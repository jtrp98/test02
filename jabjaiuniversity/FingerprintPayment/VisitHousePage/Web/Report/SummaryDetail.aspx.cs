using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Web.Report
{



    //public class WebFormController : Controller { }
    //public static class WebFormMVCUtil
    //{

    //    public static void RenderPartial(string partialName, object model)
    //    {
    //        //get a wrapper for the legacy WebForm context
    //        var httpCtx = new HttpContextWrapper(System.Web.HttpContext.Current);

    //        //create a mock route that points to the empty controller
    //        var rt = new RouteData();
    //        rt.Values.Add("controller", "WebFormController");

    //        //create a controller context for the route and http context
    //        var ctx = new ControllerContext(
    //            new RequestContext(httpCtx, rt), new WebFormController());

    //        //find the partial view using the viewengine
    //        var view = ViewEngines.Engines.FindPartialView(ctx, partialName).View;

    //        //create a view context and assign the model
    //        var vctx = new ViewContext(ctx, view,
    //            new ViewDataDictionary { Model = model },
    //            new TempDataDictionary());

    //        //render the partial view
    //        view.Render(vctx, System.Web.HttpContext.Current.Response.Output);
    //    }

    //}

    public partial class SummaryDetail : BehaviorGateway
    {


        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }

        }

        public class RelationModel
        {
            public int relation { get; set; }
            public int relationLevel { get; set; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string RenderPartialToString(string controlName, UCSummaryDetail.ModelDetail viewData)
        {
            ViewDataDictionary vd = new ViewDataDictionary(viewData);
            ViewPage vp = new ViewPage { ViewData = vd };
            var control = vp.LoadControl(controlName) as UCSummaryDetail;
            control.ModelData = viewData;
            vp.Controls.Add(control);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    vp.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        protected static double ToPercentage(int num1, int numAll)
        {
            if (numAll == 0)
                return 0;
            else
                return ((double)num1 / numAll) * 100;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(string term, int? level1, int? level2)
        {

            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new VisitHomeRepository(ctx);
                var data = logic.LoadReportSummaryDetail(userData.CompanyID, term, level1, level2);

                var model = new UCSummaryDetail.ModelDetail();
                model.CountAll = data.Count();
                model.CountAllMale = data.Where(o => o.cSex == "0").Count();
                model.CountAllMalePercent = ToPercentage(model.CountAllMale, model.CountAll);
                model.CountAllFemale = data.Where(o => o.cSex == "1").Count();
                model.CountAllFemalePercent = ToPercentage(model.CountAllFemale, model.CountAll);

                model.CountVisit = data.Where(o => o.Status > 1).Count();
                model.CountVisitMale = data.Where(o => o.Status > 1 && o.cSex == "0").Count();
                model.CountVisitMalePercent = ToPercentage(model.CountVisitMale, model.CountAll);
                model.CountVisitFemale = data.Where(o => o.Status > 1 && o.cSex == "1").Count();
                model.CountVisitFemalePercent = ToPercentage(model.CountVisitFemale, model.CountAll);
                model.CountVisitPercent = ToPercentage(model.CountVisitMale + model.CountVisitFemale, model.CountAll);
                model.CountNotVisit = model.CountAll - model.CountVisit;
                model.CountNotVisitPercent = 100 - model.CountVisitPercent;

                #region ตารางที่ 2.1 new
                model.ChoiseNew21_01 = data.Where(o => o.ResidentialHouse == 1).Count();
                model.ChoiseNew21_02 = data.Where(o => o.ResidentialHouse == 2).Count();
                model.ChoiseNew21_03 = data.Where(o => o.ResidentialHouse == 3).Count();
                model.ChoiseNew21_04 = data.Where(o => o.ResidentialHouse == 4).Count();
                model.ChoiseNew21_05 = data.Where(o => o.ResidentialHouse == 5).Count();
                model.ChoiseNew21_99 = data.Where(o => o.ResidentialHouse == 99).Count();
                model.ChoiseNew21_01Percent = ToPercentage(model.ChoiseNew21_01, model.CountAll);
                model.ChoiseNew21_02Percent = ToPercentage(model.ChoiseNew21_02, model.CountAll);
                model.ChoiseNew21_03Percent = ToPercentage(model.ChoiseNew21_03, model.CountAll);
                model.ChoiseNew21_04Percent = ToPercentage(model.ChoiseNew21_04, model.CountAll);
                model.ChoiseNew21_05Percent = ToPercentage(model.ChoiseNew21_05, model.CountAll);
                model.ChoiseNew21_99Percent = ToPercentage(model.ChoiseNew21_99, model.CountAll);
                #endregion

                #region ตารางที่ 2.2 new
                model.ChoiseNew22_1_01 = data.Where(o => o.OwnHome == 1).Count();
                model.ChoiseNew22_1_02 = data.Where(o => o.OwnHome == 2).Count();
                model.ChoiseNew22_1_03 = data.Where(o => o.OwnHome == 3).Count();
                model.ChoiseNew22_1_04 = data.Where(o => o.OwnHome == 4).Count();
                model.ChoiseNew22_1_05 = data.Where(o => o.OwnHome == 5).Count();
                model.ChoiseNew22_1_01Percent = ToPercentage(model.ChoiseNew22_1_01, model.CountAll);
                model.ChoiseNew22_1_02Percent = ToPercentage(model.ChoiseNew22_1_02, model.CountAll);
                model.ChoiseNew22_1_03Percent = ToPercentage(model.ChoiseNew22_1_03, model.CountAll);
                model.ChoiseNew22_1_04Percent = ToPercentage(model.ChoiseNew22_1_04, model.CountAll);
                model.ChoiseNew22_1_05Percent = ToPercentage(model.ChoiseNew22_1_05, model.CountAll);

                model.ChoiseNew22_2_01 = data.Where(o => o.Cleanliness == 1).Count();
                model.ChoiseNew22_2_02 = data.Where(o => o.Cleanliness == 2).Count();
                model.ChoiseNew22_2_03 = data.Where(o => o.Cleanliness == 3).Count();
                model.ChoiseNew22_2_04 = data.Where(o => o.Cleanliness == 99).Count();              
                model.ChoiseNew22_2_01Percent = ToPercentage(model.ChoiseNew22_2_01, model.CountAll);
                model.ChoiseNew22_2_02Percent = ToPercentage(model.ChoiseNew22_2_02, model.CountAll);
                model.ChoiseNew22_2_03Percent = ToPercentage(model.ChoiseNew22_2_03, model.CountAll);
                model.ChoiseNew22_2_04Percent = ToPercentage(model.ChoiseNew22_2_04, model.CountAll);

                model.ChoiseNew22_3_01 = data.Where(o => o.UtilitiesElectricity == 1).Count();
                model.ChoiseNew22_3_02 = data.Where(o => o.UtilitiesElectricity == 2).Count();
                model.ChoiseNew22_3_01Percent = ToPercentage(model.ChoiseNew22_3_01, model.CountAll);
                model.ChoiseNew22_3_02Percent = ToPercentage(model.ChoiseNew22_3_02, model.CountAll);

                model.ChoiseNew22_4_01 = data.Where(o => o.WaterForConsumption == 1).Count();
                model.ChoiseNew22_4_02 = data.Where(o => o.WaterForConsumption == 2).Count();
                model.ChoiseNew22_4_01Percent = ToPercentage(model.ChoiseNew22_4_01, model.CountAll);
                model.ChoiseNew22_4_02Percent = ToPercentage(model.ChoiseNew22_4_02, model.CountAll);

                model.ChoiseNew22_5_01 = data.Where(o => o.Toilet == 1).Count();
                model.ChoiseNew22_5_02 = data.Where(o => o.Toilet == 2).Count();
                model.ChoiseNew22_5_01Percent = ToPercentage(model.ChoiseNew22_5_01, model.CountAll);
                model.ChoiseNew22_5_02Percent = ToPercentage(model.ChoiseNew22_5_02, model.CountAll);
                #endregion

                #region ตารางที่ 2.4 new

                model.ChoiseNew2_4_01 = data.Where(o => o.FamilyRelationship == 1).Count();
                model.ChoiseNew2_4_02 = data.Where(o => o.FamilyRelationship == 2).Count();
                model.ChoiseNew2_4_03 = data.Where(o => o.FamilyRelationship == 3).Count();
                model.ChoiseNew2_4_04 = data.Where(o => o.FamilyRelationship == 4).Count();
                model.ChoiseNew2_4_05 = data.Where(o => o.FamilyRelationship == 5).Count();
                model.ChoiseNew2_4_99 = data.Where(o => o.FamilyRelationship == 99).Count();
                model.ChoiseNew2_4_01Percent = ToPercentage(model.ChoiseNew2_4_01, model.CountAll);
                model.ChoiseNew2_4_02Percent = ToPercentage(model.ChoiseNew2_4_02, model.CountAll);
                model.ChoiseNew2_4_03Percent = ToPercentage(model.ChoiseNew2_4_03, model.CountAll);
                model.ChoiseNew2_4_04Percent = ToPercentage(model.ChoiseNew2_4_04, model.CountAll);
                model.ChoiseNew2_4_05Percent = ToPercentage(model.ChoiseNew2_4_05, model.CountAll);
                model.ChoiseNew2_4_99Percent = ToPercentage(model.ChoiseNew2_4_99, model.CountAll);

                #endregion

                #region ตารางที่ 2.5

                var relationList = data.Where(o => o.RelationshipMember != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<RelationModel>>(o.RelationshipMember));
                //บิดา
                var relationlst = relationList.Where(o => o.relation == 1);
                model.Choise22_R11 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R12 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R13 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R14 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R15 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R11Percent = ToPercentage(model.Choise22_R11, model.CountAll);
                model.Choise22_R12Percent = ToPercentage(model.Choise22_R12, model.CountAll);
                model.Choise22_R13Percent = ToPercentage(model.Choise22_R13, model.CountAll);
                model.Choise22_R14Percent = ToPercentage(model.Choise22_R14, model.CountAll);
                model.Choise22_R15Percent = ToPercentage(model.Choise22_R15, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 2);
                model.Choise22_R21 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R22 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R23 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R24 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R25 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R21Percent = ToPercentage(model.Choise22_R21, model.CountAll);
                model.Choise22_R22Percent = ToPercentage(model.Choise22_R22, model.CountAll);
                model.Choise22_R23Percent = ToPercentage(model.Choise22_R23, model.CountAll);
                model.Choise22_R24Percent = ToPercentage(model.Choise22_R24, model.CountAll);
                model.Choise22_R25Percent = ToPercentage(model.Choise22_R25, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 3);
                model.Choise22_R31 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R32 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R33 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R34 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R35 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R31Percent = ToPercentage(model.Choise22_R31, model.CountAll);
                model.Choise22_R32Percent = ToPercentage(model.Choise22_R32, model.CountAll);
                model.Choise22_R33Percent = ToPercentage(model.Choise22_R33, model.CountAll);
                model.Choise22_R34Percent = ToPercentage(model.Choise22_R34, model.CountAll);
                model.Choise22_R35Percent = ToPercentage(model.Choise22_R35, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 4);
                model.Choise22_R41 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R42 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R43 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R44 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R45 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R41Percent = ToPercentage(model.Choise22_R41, model.CountAll);
                model.Choise22_R42Percent = ToPercentage(model.Choise22_R42, model.CountAll);
                model.Choise22_R43Percent = ToPercentage(model.Choise22_R43, model.CountAll);
                model.Choise22_R44Percent = ToPercentage(model.Choise22_R44, model.CountAll);
                model.Choise22_R45Percent = ToPercentage(model.Choise22_R45, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 5);
                model.Choise22_R51 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R52 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R53 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R54 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R55 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R51Percent = ToPercentage(model.Choise22_R51, model.CountAll);
                model.Choise22_R52Percent = ToPercentage(model.Choise22_R52, model.CountAll);
                model.Choise22_R53Percent = ToPercentage(model.Choise22_R53, model.CountAll);
                model.Choise22_R54Percent = ToPercentage(model.Choise22_R54, model.CountAll);
                model.Choise22_R55Percent = ToPercentage(model.Choise22_R55, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 6);
                model.Choise22_R61 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R62 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R63 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R64 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R65 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R61Percent = ToPercentage(model.Choise22_R61, model.CountAll);
                model.Choise22_R62Percent = ToPercentage(model.Choise22_R62, model.CountAll);
                model.Choise22_R63Percent = ToPercentage(model.Choise22_R63, model.CountAll);
                model.Choise22_R64Percent = ToPercentage(model.Choise22_R64, model.CountAll);
                model.Choise22_R65Percent = ToPercentage(model.Choise22_R65, model.CountAll);

                relationlst = relationList.Where(o => o.relation == 99);
                model.Choise22_R991 = relationlst.Where(o => o.relationLevel == 1).Count();
                model.Choise22_R992 = relationlst.Where(o => o.relationLevel == 2).Count();
                model.Choise22_R993 = relationlst.Where(o => o.relationLevel == 3).Count();
                model.Choise22_R994 = relationlst.Where(o => o.relationLevel == 4).Count();
                model.Choise22_R995 = relationlst.Where(o => o.relationLevel == 5).Count();

                model.Choise22_R991Percent = ToPercentage(model.Choise22_R991, model.CountAll);
                model.Choise22_R992Percent = ToPercentage(model.Choise22_R992, model.CountAll);
                model.Choise22_R993Percent = ToPercentage(model.Choise22_R993, model.CountAll);
                model.Choise22_R994Percent = ToPercentage(model.Choise22_R994, model.CountAll);
                model.Choise22_R995Percent = ToPercentage(model.Choise22_R995, model.CountAll);

                #endregion

                #region  ตารางที่ 2.1 สมาชิกในครอบครัวมีเวลาอยู่ด้วยกันกี่ชมต่อวัน
                model.Choise21_02 = data.Where(o => o.SpendTimeWithFamily >= 0 && o.SpendTimeWithFamily <= 2).Count();
                model.Choise21_34 = data.Where(o => o.SpendTimeWithFamily >= 3 && o.SpendTimeWithFamily <= 4).Count();
                model.Choise21_56 = data.Where(o => o.SpendTimeWithFamily >= 5 && o.SpendTimeWithFamily <= 6).Count();
                model.Choise21_78 = data.Where(o => o.SpendTimeWithFamily >= 7 && o.SpendTimeWithFamily <= 8).Count();
                model.Choise21_810 = data.Where(o => o.SpendTimeWithFamily >= 9 && o.SpendTimeWithFamily <= 10).Count();
                model.Choise21_10 = data.Where(o => o.SpendTimeWithFamily > 10).Count();
                model.Choise21_02Percent = ToPercentage(model.Choise21_02, model.CountAll);
                model.Choise21_34Percent = ToPercentage(model.Choise21_34, model.CountAll);
                model.Choise21_56Percent = ToPercentage(model.Choise21_56, model.CountAll);
                model.Choise21_78Percent = ToPercentage(model.Choise21_78, model.CountAll);
                model.Choise21_810Percent = ToPercentage(model.Choise21_810, model.CountAll);
                model.Choise21_10Percent = ToPercentage(model.Choise21_10, model.CountAll);
                #endregion

           

                #region 2.3

                model.Choise23_1 = data.Where(o => o.LeaveStudent == 1).Count();
                model.Choise23_2 = data.Where(o => o.LeaveStudent == 2).Count();
                model.Choise23_3 = data.Where(o => o.LeaveStudent == 3).Count();
                model.Choise23_99 = data.Where(o => o.LeaveStudent == 99).Count();
                model.Choise23_1Percent = ToPercentage(model.Choise23_1, model.CountAll);
                model.Choise23_2Percent = ToPercentage(model.Choise23_2, model.CountAll);
                model.Choise23_3Percent = ToPercentage(model.Choise23_3, model.CountAll);
                model.Choise23_99Percent = ToPercentage(model.Choise23_99, model.CountAll);

                #endregion

                #region 2.4

                model.Choise24_1 = data.Where(o => o.MedianHouseholdIncome == 1).Count();
                model.Choise24_2 = data.Where(o => o.MedianHouseholdIncome == 2).Count();
                model.Choise24_3 = data.Where(o => o.MedianHouseholdIncome == 3).Count();
                model.Choise24_4 = data.Where(o => o.MedianHouseholdIncome == 4).Count();
                model.Choise24_1Percent = ToPercentage(model.Choise24_1, model.CountAll);
                model.Choise24_2Percent = ToPercentage(model.Choise24_2, model.CountAll);
                model.Choise24_3Percent = ToPercentage(model.Choise24_3, model.CountAll);
                model.Choise24_4Percent = ToPercentage(model.Choise24_4, model.CountAll);
                #endregion

                #region 2.5

                model.Choise251_1 = data.Where(o => o.ReceiveExpensesFrom == 1).Count();
                model.Choise251_2 = data.Where(o => o.ReceiveExpensesFrom == 2).Count();
                model.Choise251_3 = data.Where(o => o.ReceiveExpensesFrom == 3).Count();
                model.Choise251_99 = data.Where(o => o.ReceiveExpensesFrom == 99).Count();
                model.Choise251_1Percent = ToPercentage(model.Choise251_1, model.CountAll);
                model.Choise251_2Percent = ToPercentage(model.Choise251_2, model.CountAll);
                model.Choise251_3Percent = ToPercentage(model.Choise251_3, model.CountAll);
                model.Choise251_99Percent = ToPercentage(model.Choise251_99, model.CountAll);

                model.Choise252_1 = data.Where(o => o.StudentWorkIncome == 1).Count();
                model.Choise252_2 = data.Where(o => o.StudentWorkIncome == 2).Count();
                model.Choise252_3 = data.Where(o => o.StudentWorkIncome == 3).Count();
                model.Choise252_4 = data.Where(o => o.StudentWorkIncome == 4).Count();
                model.Choise252_99 = data.Where(o => o.StudentWorkIncome == 99).Count();
                model.Choise252_1Percent = ToPercentage(model.Choise252_1, model.CountAll);
                model.Choise252_2Percent = ToPercentage(model.Choise252_2, model.CountAll);
                model.Choise252_3Percent = ToPercentage(model.Choise252_3, model.CountAll);
                model.Choise252_4Percent = ToPercentage(model.Choise252_4, model.CountAll);
                model.Choise252_99Percent = ToPercentage(model.Choise252_99, model.CountAll);

                model.Choise253_1 = data.Where(o => o.DailyIncome == 1).Count();
                model.Choise253_2 = data.Where(o => o.DailyIncome == 2).Count();
                model.Choise253_3 = data.Where(o => o.DailyIncome == 3).Count();
                model.Choise253_4 = data.Where(o => o.DailyIncome == 4).Count();
                model.Choise253_1Percent = ToPercentage(model.Choise253_1, model.CountAll);
                model.Choise253_2Percent = ToPercentage(model.Choise253_2, model.CountAll);
                model.Choise253_3Percent = ToPercentage(model.Choise253_3, model.CountAll);
                model.Choise253_4Percent = ToPercentage(model.Choise253_4, model.CountAll);

                model.Choise254_1 = data.Where(o => o.PaidComeDay == 1).Count();
                model.Choise254_2 = data.Where(o => o.PaidComeDay == 2).Count();
                model.Choise254_3 = data.Where(o => o.PaidComeDay == 3).Count();
                model.Choise254_1Percent = ToPercentage(model.Choise254_1, model.CountAll);
                model.Choise254_2Percent = ToPercentage(model.Choise254_2, model.CountAll);
                model.Choise254_3Percent = ToPercentage(model.Choise254_3, model.CountAll);

                #endregion

                #region 2.6

                var parentWantSchoolsHelp = data.Where(o => o.ParentWantSchoolsHelp != null)
                   .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.ParentWantSchoolsHelp));
                model.Choise26_1 = parentWantSchoolsHelp.Where(o => o == 1).Count();
                model.Choise26_2 = parentWantSchoolsHelp.Where(o => o == 2).Count();
                model.Choise26_3 = parentWantSchoolsHelp.Where(o => o == 3).Count();
                model.Choise26_99 = parentWantSchoolsHelp.Where(o => o == 99).Count();              
                model.Choise26_1Percent = ToPercentage(model.Choise26_1, model.CountAll);
                model.Choise26_2Percent = ToPercentage(model.Choise26_2, model.CountAll);
                model.Choise26_3Percent = ToPercentage(model.Choise26_3, model.CountAll);
                model.Choise26_99Percent = ToPercentage(model.Choise26_99, model.CountAll);

                #endregion

                #region 2.7

                var parentWantAgencyHelp = data.Where(o => o.ParentWantAgencyHelp != null)
                   .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.ParentWantAgencyHelp));
                model.Choise27_No = data.Where(o => o.ParentWantAgencyHelp == null || o.ParentWantAgencyHelp == "[]").Count();
                model.Choise27_1 = parentWantAgencyHelp.Where(o => o == 1).Count();
                model.Choise27_2 = parentWantAgencyHelp.Where(o => o == 2).Count();
                model.Choise27_99 = parentWantAgencyHelp.Where(o => o == 99).Count();
                model.Choise27_NoPercent = ToPercentage(model.Choise27_No, model.CountAll);
                model.Choise27_1Percent = ToPercentage(model.Choise27_1, model.CountAll);
                model.Choise27_2Percent = ToPercentage(model.Choise27_2, model.CountAll);
                model.Choise27_99Percent = ToPercentage(model.Choise27_99, model.CountAll);

                #endregion

                #region 2.11

                var parentWantAgencyHelp2 = data.Where(o => o.ParentWantAgencyHelp2 != null)
                   .SelectMany(o => JsonConvert.DeserializeObject<List<int?>>(o.ParentWantAgencyHelp2));
                model.Choise2_11_01 = parentWantAgencyHelp2.Where(o => o == 1).Count();
                model.Choise2_11_02 = parentWantAgencyHelp2.Where(o => o == 2).Count();
                model.Choise2_11_03 = parentWantAgencyHelp2.Where(o => o == 3).Count();
                model.Choise2_11_04 = parentWantAgencyHelp2.Where(o => o == 4).Count();
                model.Choise2_11_99 = parentWantAgencyHelp2.Where(o => o == 5).Count();
                
                model.Choise2_11_01Percent = ToPercentage(model.Choise2_11_01, model.CountAll);
                model.Choise2_11_02Percent = ToPercentage(model.Choise2_11_02, model.CountAll);
                model.Choise2_11_03Percent = ToPercentage(model.Choise2_11_03, model.CountAll);
                model.Choise2_11_04Percent = ToPercentage(model.Choise2_11_04, model.CountAll);
                model.Choise2_11_99Percent = ToPercentage(model.Choise2_11_99, model.CountAll);
                #endregion

                #region 3.11

                var healtList = data.Where(o => o.Health != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.Health));
                model.Choise31_Normal = data.Where(o => o.Health == null || o.Health == "[]").Count();
                model.Choise31_NormalPercent = ToPercentage(model.Choise31_Normal, model.CountAll);

                model.Choise31_1 = healtList.Where(o => o == 1).Count();
                model.Choise31_2 = healtList.Where(o => o == 2).Count();
                model.Choise31_3 = healtList.Where(o => o == 3).Count();
                model.Choise31_4 = healtList.Where(o => o == 4).Count();
                model.Choise31_5 = healtList.Where(o => o == 5).Count();
                model.Choise31_1Percent = ToPercentage(model.Choise31_1, model.CountAll);
                model.Choise31_2Percent = ToPercentage(model.Choise31_2, model.CountAll);
                model.Choise31_3Percent = ToPercentage(model.Choise31_3, model.CountAll);
                model.Choise31_4Percent = ToPercentage(model.Choise31_4, model.CountAll);
                model.Choise31_5Percent = ToPercentage(model.Choise31_5, model.CountAll);

                #endregion

                #region 3.2

                var walfareList = data.Where(o => o.WelfareSafety != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.WelfareSafety));

                model.Choise32_Normal = data.Where(o => o.WelfareSafety == null || o.WelfareSafety == "[]").Count();
                model.Choise32_NormalPercent = ToPercentage(model.Choise32_Normal, model.CountAll);
                model.Choise32_1 = walfareList.Where(o => o == 1).Count();
                model.Choise32_2 = walfareList.Where(o => o == 2).Count();
                model.Choise32_3 = walfareList.Where(o => o == 3).Count();
                model.Choise32_4 = walfareList.Where(o => o == 4).Count();
                model.Choise32_5 = walfareList.Where(o => o == 5).Count();
                model.Choise32_6 = walfareList.Where(o => o == 6).Count();
                model.Choise32_7 = walfareList.Where(o => o == 7).Count();
                model.Choise32_8 = walfareList.Where(o => o == 8).Count();
                model.Choise32_9 = walfareList.Where(o => o == 9).Count();
                model.Choise32_10 = walfareList.Where(o => o == 10).Count();
                model.Choise32_11 = walfareList.Where(o => o == 11).Count();
                model.Choise32_1Percent = ToPercentage(model.Choise32_1, model.CountAll);
                model.Choise32_2Percent = ToPercentage(model.Choise32_2, model.CountAll);
                model.Choise32_3Percent = ToPercentage(model.Choise32_3, model.CountAll);
                model.Choise32_4Percent = ToPercentage(model.Choise32_4, model.CountAll);
                model.Choise32_5Percent = ToPercentage(model.Choise32_5, model.CountAll);
                model.Choise32_6Percent = ToPercentage(model.Choise32_6, model.CountAll);
                model.Choise32_7Percent = ToPercentage(model.Choise32_7, model.CountAll);
                model.Choise32_8Percent = ToPercentage(model.Choise32_8, model.CountAll);
                model.Choise32_9Percent = ToPercentage(model.Choise32_9, model.CountAll);
                model.Choise32_10Percent = ToPercentage(model.Choise32_10, model.CountAll);
                model.Choise32_11Percent = ToPercentage(model.Choise32_11, model.CountAll);

                #endregion

                #region 3.3 

                model.Choise331_1 = data.Where(o => o.DistanceHomeToSchool == 1).Count();
                model.Choise331_2 = data.Where(o => o.DistanceHomeToSchool == 2).Count();
                model.Choise331_3 = data.Where(o => o.DistanceHomeToSchool == 3).Count();
                model.Choise331_4 = data.Where(o => o.DistanceHomeToSchool == 4).Count();
                model.Choise331_5 = data.Where(o => o.DistanceHomeToSchool == 5).Count();
                model.Choise331_1Percent = ToPercentage(model.Choise331_1, model.CountAll);
                model.Choise331_2Percent = ToPercentage(model.Choise331_2, model.CountAll);
                model.Choise331_3Percent = ToPercentage(model.Choise331_3, model.CountAll);
                model.Choise331_4Percent = ToPercentage(model.Choise331_4, model.CountAll);
                model.Choise331_5Percent = ToPercentage(model.Choise331_5, model.CountAll);

                model.Choise332_1 = data.Where(o => o.TravelTime == 1).Count();
                model.Choise332_2 = data.Where(o => o.TravelTime == 2).Count();
                model.Choise332_3 = data.Where(o => o.TravelTime == 3).Count();
                model.Choise332_1Percent = ToPercentage(model.Choise332_1, model.CountAll);
                model.Choise332_2Percent = ToPercentage(model.Choise332_2, model.CountAll);
                model.Choise332_3Percent = ToPercentage(model.Choise332_3, model.CountAll);

                model.Choise333_1 = data.Where(o => o.StudentTravel == 1).Count();
                model.Choise333_2 = data.Where(o => o.StudentTravel == 2).Count();
                model.Choise333_3 = data.Where(o => o.StudentTravel == 3).Count();
                model.Choise333_4 = data.Where(o => o.StudentTravel == 4).Count();
                model.Choise333_5 = data.Where(o => o.StudentTravel == 5).Count();
                model.Choise333_6 = data.Where(o => o.StudentTravel == 6).Count();
                model.Choise333_7 = data.Where(o => o.StudentTravel == 7).Count();
                model.Choise333_99 = data.Where(o => o.StudentTravel == 99).Count();
                model.Choise333_1Percent = ToPercentage(model.Choise333_1, model.CountAll);
                model.Choise333_2Percent = ToPercentage(model.Choise333_2, model.CountAll);
                model.Choise333_3Percent = ToPercentage(model.Choise333_3, model.CountAll);
                model.Choise333_4Percent = ToPercentage(model.Choise333_4, model.CountAll);
                model.Choise333_5Percent = ToPercentage(model.Choise333_5, model.CountAll);
                model.Choise333_6Percent = ToPercentage(model.Choise333_6, model.CountAll);
                model.Choise333_7Percent = ToPercentage(model.Choise333_7, model.CountAll);
                model.Choise333_99Percent = ToPercentage(model.Choise333_99, model.CountAll);
                #endregion

                #region 3.4 

                var livingList = data.Where(o => o.LivingConditions != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.LivingConditions));
                model.Choise34_1 = livingList.Where(o => o == 1).Count();
                model.Choise34_2 = livingList.Where(o => o == 2).Count();
                model.Choise34_3 = livingList.Where(o => o == 3).Count();
                model.Choise34_4 = livingList.Where(o => o == 4).Count();
                model.Choise34_5 = livingList.Where(o => o == 5).Count();
                model.Choise34_99 = livingList.Where(o => o == 99).Count();
                model.Choise34_1Percent = ToPercentage(model.Choise34_1, model.CountAll);
                model.Choise34_2Percent = ToPercentage(model.Choise34_2, model.CountAll);
                model.Choise34_3Percent = ToPercentage(model.Choise34_3, model.CountAll);
                model.Choise34_4Percent = ToPercentage(model.Choise34_4, model.CountAll);
                model.Choise34_5Percent = ToPercentage(model.Choise34_5, model.CountAll);
                model.Choise34_99Percent = ToPercentage(model.Choise34_99, model.CountAll);
                #endregion

                #region 3.5 

                var responsList = data.Where(o => o.StudentResponsibilities != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.StudentResponsibilities));
                model.Choise35_1 = responsList.Where(o => o == 1).Count();
                model.Choise35_2 = responsList.Where(o => o == 2).Count();
                model.Choise35_3 = responsList.Where(o => o == 3).Count();
                model.Choise35_4 = responsList.Where(o => o == 4).Count();
                model.Choise35_5 = responsList.Where(o => o == 5).Count();
                model.Choise35_99 = responsList.Where(o => o == 99).Count();
                model.Choise35_1Percent = ToPercentage(model.Choise35_1, model.CountAll);
                model.Choise35_2Percent = ToPercentage(model.Choise35_2, model.CountAll);
                model.Choise35_3Percent = ToPercentage(model.Choise35_3, model.CountAll);
                model.Choise35_4Percent = ToPercentage(model.Choise35_4, model.CountAll);
                model.Choise35_5Percent = ToPercentage(model.Choise35_5, model.CountAll);
                model.Choise35_99Percent = ToPercentage(model.Choise35_99, model.CountAll);
                #endregion

                #region 3.6 

                var hobbyList = data.Where(o => o.Hobbies != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.Hobbies));
                model.Choise36_1 = hobbyList.Where(o => o == 1).Count();
                model.Choise36_2 = hobbyList.Where(o => o == 2).Count();
                model.Choise36_3 = hobbyList.Where(o => o == 3).Count();
                model.Choise36_4 = hobbyList.Where(o => o == 4).Count();
                model.Choise36_5 = hobbyList.Where(o => o == 5).Count();
                model.Choise36_6 = hobbyList.Where(o => o == 6).Count();
                model.Choise36_7 = hobbyList.Where(o => o == 7).Count();
                model.Choise36_8 = hobbyList.Where(o => o == 8).Count();
                model.Choise36_99 = hobbyList.Where(o => o == 99).Count();
                model.Choise36_1Percent = ToPercentage(model.Choise36_1, model.CountAll);
                model.Choise36_2Percent = ToPercentage(model.Choise36_2, model.CountAll);
                model.Choise36_3Percent = ToPercentage(model.Choise36_3, model.CountAll);
                model.Choise36_4Percent = ToPercentage(model.Choise36_4, model.CountAll);
                model.Choise36_5Percent = ToPercentage(model.Choise36_5, model.CountAll);
                model.Choise36_6Percent = ToPercentage(model.Choise36_6, model.CountAll);
                model.Choise36_7Percent = ToPercentage(model.Choise36_7, model.CountAll);
                model.Choise36_8Percent = ToPercentage(model.Choise36_8, model.CountAll);
                model.Choise36_99Percent = ToPercentage(model.Choise36_99, model.CountAll);

                #endregion

                #region 3.7 

                var abuseList = data.Where(o => o.SubstanceAbuseBehavior != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.SubstanceAbuseBehavior));

                model.Choise37_Normal = data.Where(o => o.SubstanceAbuseBehavior == null || o.SubstanceAbuseBehavior == "[]").Count();
                model.Choise37_NormalPercent = ToPercentage(model.Choise37_Normal, model.CountAll);

                model.Choise37_1 = abuseList.Where(o => o == 1).Count();
                model.Choise37_2 = abuseList.Where(o => o == 2).Count();
                model.Choise37_3 = abuseList.Where(o => o == 3).Count();
                model.Choise37_4 = abuseList.Where(o => o == 4).Count();
                model.Choise37_5 = abuseList.Where(o => o == 5).Count();
                model.Choise37_1Percent = ToPercentage(model.Choise37_1, model.CountAll);
                model.Choise37_2Percent = ToPercentage(model.Choise37_2, model.CountAll);
                model.Choise37_3Percent = ToPercentage(model.Choise37_3, model.CountAll);
                model.Choise37_4Percent = ToPercentage(model.Choise37_4, model.CountAll);
                model.Choise37_5Percent = ToPercentage(model.Choise37_5, model.CountAll);

                #endregion


                #region 3.8 

                var violentList = data.Where(o => o.ViolentBehavior != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.ViolentBehavior));

                model.Choise38_Normal = data.Where(o => o.ViolentBehavior == null || o.ViolentBehavior == "[]").Count();
                model.Choise38_NormalPercent = ToPercentage(model.Choise38_Normal, model.CountAll);

                model.Choise38_1 = violentList.Where(o => o == 1).Count();
                model.Choise38_2 = violentList.Where(o => o == 2).Count();
                model.Choise38_3 = violentList.Where(o => o == 3).Count();
                model.Choise38_4 = violentList.Where(o => o == 4).Count();
                model.Choise38_5 = violentList.Where(o => o == 5).Count();
                model.Choise38_99 = violentList.Where(o => o == 99).Count();
                model.Choise38_1Percent = ToPercentage(model.Choise38_1, model.CountAll);
                model.Choise38_2Percent = ToPercentage(model.Choise38_2, model.CountAll);
                model.Choise38_3Percent = ToPercentage(model.Choise38_3, model.CountAll);
                model.Choise38_4Percent = ToPercentage(model.Choise38_4, model.CountAll);
                model.Choise38_5Percent = ToPercentage(model.Choise38_5, model.CountAll);
                model.Choise38_99Percent = ToPercentage(model.Choise38_99, model.CountAll);
                #endregion

                #region 3.9 

                var sexualList = data.Where(o => o.SexualBehavior != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.SexualBehavior));

                model.Choise39_Normal = data.Where(o => o.SexualBehavior == null || o.SexualBehavior == "[]").Count();
                model.Choise39_NormalPercent = ToPercentage(model.Choise39_Normal, model.CountAll);

                model.Choise39_1 = sexualList.Where(o => o == 1).Count();
                model.Choise39_2 = sexualList.Where(o => o == 2).Count();
                model.Choise39_3 = sexualList.Where(o => o == 3).Count();
                model.Choise39_4 = sexualList.Where(o => o == 4).Count();
                model.Choise39_5 = sexualList.Where(o => o == 5).Count();
                model.Choise39_6 = sexualList.Where(o => o == 6).Count();
                model.Choise39_1Percent = ToPercentage(model.Choise39_1, model.CountAll);
                model.Choise39_2Percent = ToPercentage(model.Choise39_2, model.CountAll);
                model.Choise39_3Percent = ToPercentage(model.Choise39_3, model.CountAll);
                model.Choise39_4Percent = ToPercentage(model.Choise39_4, model.CountAll);
                model.Choise39_5Percent = ToPercentage(model.Choise39_5, model.CountAll);
                model.Choise39_6Percent = ToPercentage(model.Choise39_6, model.CountAll);

                #endregion

                #region 3.10 

                var gameList = data.Where(o => o.GameAddiction != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.GameAddiction));

                model.Choise310_Normal = data.Where(o => o.GameAddiction == null || o.GameAddiction == "[]").Count();
                model.Choise310_NormalPercent = ToPercentage(model.Choise310_Normal, model.CountAll);

                model.Choise310_1 = gameList.Where(o => o == 1).Count();
                model.Choise310_2 = gameList.Where(o => o == 2).Count();
                model.Choise310_3 = gameList.Where(o => o == 3).Count();
                model.Choise310_4 = gameList.Where(o => o == 4).Count();
                model.Choise310_5 = gameList.Where(o => o == 5).Count();
                model.Choise310_6 = gameList.Where(o => o == 6).Count();
                model.Choise310_7 = gameList.Where(o => o == 7).Count();
                model.Choise310_8 = gameList.Where(o => o == 8).Count();
                model.Choise310_99 = gameList.Where(o => o == 99).Count();

                model.Choise310_1Percent = ToPercentage(model.Choise310_1, model.CountAll);
                model.Choise310_2Percent = ToPercentage(model.Choise310_2, model.CountAll);
                model.Choise310_3Percent = ToPercentage(model.Choise310_3, model.CountAll);
                model.Choise310_4Percent = ToPercentage(model.Choise310_4, model.CountAll);
                model.Choise310_5Percent = ToPercentage(model.Choise310_5, model.CountAll);
                model.Choise310_6Percent = ToPercentage(model.Choise310_6, model.CountAll);
                model.Choise310_7Percent = ToPercentage(model.Choise310_7, model.CountAll);
                model.Choise310_8Percent = ToPercentage(model.Choise310_8, model.CountAll);
                model.Choise310_99Percent = ToPercentage(model.Choise310_99, model.CountAll);

                #endregion

                #region 3.11 

                model.Choise311_1 = data.Where(o => o.AccessComputerInternet == 1).Count();
                model.Choise311_2 = data.Where(o => o.AccessComputerInternet == 2).Count();
                model.Choise311_1Percent = ToPercentage(model.Choise311_1, model.CountAll);
                model.Choise311_2Percent = ToPercentage(model.Choise311_2, model.CountAll);

                #endregion

                #region 3.12 
                var electronicList = data.Where(o => o.UseElectronicTools != null)
                    .SelectMany(o => JsonConvert.DeserializeObject<List<int>>(o.UseElectronicTools));
                model.Choise312_1 = electronicList.Where(o => o == 1).Count();
                model.Choise312_2 = electronicList.Where(o => o == 2).Count();
                model.Choise312_1Percent = ToPercentage(model.Choise312_1, model.CountAll);
                model.Choise312_2Percent = ToPercentage(model.Choise312_2, model.CountAll);

                #endregion

                var html = RenderPartialToString("~/VisitHousePage/Web/Report/UCSummaryDetail.ascx", model);
                return new { html = html };
            }



            //var userData = GetUserData();
            //using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            //{
            //    var logic = new VisitHomeRepository(ctx);

            //    var result = logic.LoadReportSummary(userData.CompanyID, search.term, search.level1, search.level2);

            //    var d = result//.GroupBy( o => new { o.Room})
            //        .Select(o => new
            //        {
            //            o.Room,
            //            o.CountAll,
            //            o.Status1,
            //            o.Status2,
            //            o.Status3,
            //        });

            //    return new
            //    {
            //        data = d,
            //        percent = new
            //        {
            //            Status1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status1) * 100) / d.Sum(o => o.CountAll) : 0,
            //            Status2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status2) * 100) / d.Sum(o => o.CountAll) : 0,
            //            Status3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Status3) * 100) / d.Sum(o => o.CountAll) : 0,
            //        },
            //        summary = new
            //        {
            //            CountAll = d.Sum(o => o.CountAll),
            //            Status1 = d.Sum(o => o.Status1),
            //            Status2 = d.Sum(o => o.Status2),
            //            Status3 = d.Sum(o => o.Status3),
            //        },
            //    };
            //}
        }

    }
}