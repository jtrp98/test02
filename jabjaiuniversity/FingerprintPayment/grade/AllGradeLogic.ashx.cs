using System.Web;

namespace FingerprintPayment.grade
{
    /// <summary>
    /// Summary description for AllGradeLogic
    /// </summary>
    public class AllGradeLogic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

     

        //public Tuple<string, int> getSpecial(TGradeDetail gradeDetail, double score100, int? nTermSubLevel2)
        //{
        //    string label = "";
        //    int creditPass = 1;
        //    if (gradeDetail.getSpecial == "-1")
        //    {
        //        if (nTermSubLevel2 == null && string.IsNullOrEmpty(gradeDetail.getScore100))
        //        {
        //            label = gradeDetail.getGradeLabel;
        //        }
        //        else if (score100 < 50)
        //        {
        //            label = "0";
        //            creditPass = 0;
        //        }
        //        else if (score100 < 55)
        //            label = "1.0";
        //        else if (score100 < 60)
        //            label = "1.5";
        //        else if (score100 < 65)
        //            label = "2.0";
        //        else if (score100 < 70)
        //            label = "2.5";
        //        else if (score100 < 75)
        //            label = "3.0";
        //        else if (score100 < 80)
        //            label = "3.5";
        //        else if (score100 >= 80)
        //            label = "4.0";
        //    }
        //    else if (gradeDetail.getSpecial == "1")
        //    {
        //        label = "ร";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "2")
        //    {
        //        label = "มส";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "3")
        //    {
        //        label = "มก";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "4")
        //        label = "ผ";
        //    else if (gradeDetail.getSpecial == "5")
        //    {
        //        label = "มผ";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "6")
        //    {
        //        label = "อื่นๆ";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "7")
        //    {
        //        label = "ขร";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "8")
        //    {
        //        label = "ขส";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "9")
        //    {
        //        label = "ท";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "10")
        //    {
        //        label = "ดีเยี่ยม";
        //    }
        //    else if (gradeDetail.getSpecial == "11")
        //    {
        //        label = "ดี";
        //    }
        //    else if (gradeDetail.getSpecial == "12")
        //    {
        //        label = "พอใช้";
        //    }
        //    else if (gradeDetail.getSpecial == "13")
        //    {
        //        label = "ปรับปรุง";
        //    }
        //    else label = " ";

        //    return Tuple.Create(label, creditPass);
        //}

        //public Tuple<string, int> getSpecialHistory(GradeHistoryEntity.TGradeDetailHistory gradeDetail, double score100, int? nTermSubLevel2)
        //{
        //    string label = "";
        //    int creditPass = 1;
        //    if (gradeDetail.getSpecial == "-1")
        //    {
        //        if (nTermSubLevel2 == null && string.IsNullOrEmpty(gradeDetail.getScore100))
        //        {
        //            label = gradeDetail.getGradeLabel;
        //        }
        //        else if (score100 < 50)
        //        {
        //            label = "0";
        //            creditPass = 0;
        //        }
        //        else if (score100 < 55)
        //            label = "1.0";
        //        else if (score100 < 60)
        //            label = "1.5";
        //        else if (score100 < 65)
        //            label = "2.0";
        //        else if (score100 < 70)
        //            label = "2.5";
        //        else if (score100 < 75)
        //            label = "3.0";
        //        else if (score100 < 80)
        //            label = "3.5";
        //        else if (score100 >= 80)
        //            label = "4.0";
        //    }
        //    else if (gradeDetail.getSpecial == "1")
        //    {
        //        label = "ร";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "2")
        //    {
        //        label = "มส";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "3")
        //    {
        //        label = "มก";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "4")
        //        label = "ผ";
        //    else if (gradeDetail.getSpecial == "5")
        //    {
        //        label = "มผ";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "6")
        //    {
        //        label = "อื่นๆ";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "7")
        //    {
        //        label = "ขร";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "8")
        //    {
        //        label = "ขส";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "9")
        //    {
        //        label = "ท";
        //        creditPass = 0;
        //    }
        //    else if (gradeDetail.getSpecial == "10")
        //    {
        //        label = "ดีเยี่ยม";
        //    }
        //    else if (gradeDetail.getSpecial == "11")
        //    {
        //        label = "ดี";
        //    }
        //    else if (gradeDetail.getSpecial == "12")
        //    {
        //        label = "พอใช้";
        //    }
        //    else if (gradeDetail.getSpecial == "13")
        //    {
        //        label = "ปรับปรุง";
        //    }
        //    else label = " ";

        //    return Tuple.Create(label, creditPass);
        //}

        //public Tuple<string, string, string, int> Display(TGradeDetail gradeDetail, PlanCourseDTO plane, string grade, int creditPass)
        //{
        //    string planCreditDisplay;
        //    string gradexcredit;
        //    string gradexcreditDisplay;

        //    if (gradeDetail.getSpecial == "-1")
        //    {
        //        double gradedb = !string.IsNullOrEmpty(grade) ? double.Parse(grade) : 0;
        //        planCreditDisplay = plane.NCredit.ToString();
        //        gradexcredit = (plane.NCredit * gradedb).ToString();
        //        gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //        if (gradedb == 0) creditPass = 0;
        //    }

        //    else if (gradeDetail.getSpecial == "4")
        //    {
        //        planCreditDisplay = "-";
        //        gradexcredit = (plane.NCredit * 4).ToString();
        //        gradexcreditDisplay = "-";
        //    }
        //    else
        //    {
        //        double gradedb = 0;

        //        if (!string.IsNullOrEmpty(grade))
        //        {
        //            bool convertgradedb = double.TryParse(grade, out gradedb);
        //            if (convertgradedb)
        //            {
        //                gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //            }
        //            else
        //            {
        //                gradexcreditDisplay = "-";
        //            }
        //        }
        //        else
        //        {
        //            gradexcreditDisplay = "-";
        //        }

        //        planCreditDisplay = plane.NCredit.ToString();
        //        gradexcredit = "0";
        //        creditPass = 0;
        //    }

        //    return Tuple.Create(planCreditDisplay, gradexcredit, gradexcreditDisplay, creditPass);
        //}

        //public Tuple<string, string, string, int> DisplayHistory(GradeHistoryEntity.TGradeDetailHistory gradeDetail, PlanCourseDTO plane, string grade, int creditPass)
        //{
        //    string planCreditDisplay;
        //    string gradexcredit;
        //    string gradexcreditDisplay;

        //    if (gradeDetail.getSpecial == "-1")
        //    {
        //        double gradedb = !string.IsNullOrEmpty(grade) ? double.Parse(grade) : 0;
        //        planCreditDisplay = plane.NCredit.ToString();
        //        gradexcredit = (plane.NCredit * gradedb).ToString();
        //        gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //        if (gradedb == 0) creditPass = 0;
        //    }

        //    else if (gradeDetail.getSpecial == "4")
        //    {
        //        planCreditDisplay = "-";
        //        gradexcredit = (plane.NCredit * 4).ToString();
        //        gradexcreditDisplay = "-";
        //    }
        //    else
        //    {
        //        double gradedb = 0;

        //        if (!string.IsNullOrEmpty(grade))
        //        {
        //            bool convertgradedb = double.TryParse(grade, out gradedb);
        //            if (convertgradedb)
        //            {
        //                gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //            }
        //            else
        //            {
        //                gradexcreditDisplay = "-";
        //            }
        //        }
        //        else
        //        {
        //            gradexcreditDisplay = "-";
        //        }

        //        planCreditDisplay = plane.NCredit.ToString();
        //        gradexcredit = "0";
        //        creditPass = 0;
        //    }

        //    return Tuple.Create(planCreditDisplay, gradexcredit, gradexcreditDisplay, creditPass);
        //}


        //AdjustedOrderPrint.ashx file
        //public Tuple<string, string, string, int> Display(TGradeDetail gradeDetail, PlanCourseAdjustedOrderDTO plane, string grade, int creditPass)
        //{
        //    string planCreditDisplay;
        //    string gradexcredit;
        //    string gradexcreditDisplay;

        //    if (gradeDetail.getSpecial == "-1")
        //    {
        //        double gradedb = !string.IsNullOrEmpty(grade) ? double.Parse(grade) : 0;
        //        planCreditDisplay = plane.NCredit.ToString();
        //        gradexcredit = (plane.NCredit * gradedb).ToString();
        //        gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //        if (gradedb == 0) creditPass = 0;
        //    }

        //    else if (gradeDetail.getSpecial == "4")
        //    {
        //        planCreditDisplay = "-";
        //        gradexcredit = (plane.NCredit * 4).ToString();
        //        gradexcreditDisplay = "-";
        //    }
        //    else
        //    {
        //        double gradedb = 0;

        //        if (!string.IsNullOrEmpty(grade))
        //        {
        //            bool convertgradedb = double.TryParse(grade, out gradedb);
        //            if (convertgradedb)
        //            {
        //                gradexcreditDisplay = (plane.NCredit * gradedb).ToString();
        //                gradexcredit = gradexcreditDisplay;
        //            }
        //            else
        //            {
        //                gradexcreditDisplay = "-";
        //                gradexcredit = "0";
        //            }
        //        }
        //        else
        //        {
        //            gradexcreditDisplay = "-";
        //            gradexcredit = "0";
        //        }

        //        planCreditDisplay = plane.NCredit.ToString();
               
        //        creditPass = 0;
        //    }

        //    return Tuple.Create(planCreditDisplay, gradexcredit, gradexcreditDisplay, creditPass);
        //}

        //public Tuple<string, int, int> Regrade(TGradeDetail gradeDetail, List<TGradeEdit> listGradeEdits, int chkRegrade)
        //{
        //    string label = "-";
        //    int creditPass = 1;

        //    for (int i = 0; i < listGradeEdits.Count(); i++)
        //    {
        //        if (gradeDetail.nGradeDetailId == listGradeEdits[i].GradeDetailID)
        //        {
        //            if (listGradeEdits[i].UseGradeSet == 1)
        //            {
        //                label = String.Format("{0:0.0}", listGradeEdits[i].GradeSet);
        //                chkRegrade = 1;
        //                if (listGradeEdits[i].GradeSet == 0) creditPass = 0;
        //                break;
        //            }
        //        }
        //    }

        //    return Tuple.Create(label, creditPass, chkRegrade);
        //}

        //public Tuple<string, int, int> RegradeHistory(GradeHistoryEntity.TGradeDetailHistory gradeDetail, List<GradeHistoryEntity.TGradeEditsHistory> listGradeEdits, int chkRegrade)
        //{
        //    string label = "-";
        //    int creditPass = 1;

        //    for (int i = 0; i < listGradeEdits.Count(); i++)
        //    {
        //        if (gradeDetail.nGradeDetailId == listGradeEdits[i].GradeDetailID)
        //        {
        //            if (listGradeEdits[i].UseGradeSet == 1)
        //            {
        //                label = String.Format("{0:0.0}", listGradeEdits[i].GradeSet);
        //                chkRegrade = 1;
        //                if (listGradeEdits[i].GradeSet == 0) creditPass = 0;
        //                break;
        //            }
        //        }
        //    }

        //    return Tuple.Create(label, creditPass, chkRegrade);
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}