using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using FingerprintPayment.Class;
using Antlr.Runtime.Misc;
using iTextSharp.text.pdf.parser;
using System.Linq.Expressions;
using FingerprintPayment.Memory;
using static FingerprintPayment.App_Code.StdCallingHub;
using System.Web.Services;
using System.Web.Script.Services;
using Microsoft.AspNet.SignalR;

namespace FingerprintPayment.Exam
{
    //this code was copy from studentcardregister.aspx
    public partial class Manage : BaseExam
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var lst1 = db.TSubLevels
                     .Where(w => w.SchoolID == UserData.CompanyID && w.nWorkingStatus == 1)
                     .Select(o => new ListItem
                     {
                         Text = o.SubLevel,
                         Value = o.nTSubLevel + ""
                     })
                     .ToList();
                lst1.Insert(0, new ListItem { Text = "ทั้งหมด", Value = "" });
                ddlLevel1.DataSource = lst1;
                ddlLevel1.DataBind();

                var lst2 = db.TYears.Where(w => w.SchoolID == UserData.CompanyID && w.cDel == false)
                    .OrderByDescending(o => o.numberYear)
                    .Select(o => new ListItem
                    {
                        Text = o.numberYear + "",
                        Value = o.nYear + ""
                    })
                    .ToList();
                ddlYear.DataSource = lst2;
                ddlYear.DataBind();

                var lst3 = db.TCourseTypes.Where(o => o.SchoolID == UserData.CompanyID && o.cDel == false)
                     .Select(o => new ListItem
                     {
                         Text = o.Description,
                         Value = o.courseTypeId + ""
                     })
                    .ToList();
                lst3.Insert(0, new ListItem { Text = "ทั้งหมด", Value = "" });
                ddlCourse.DataSource = lst3;
                ddlCourse.DataBind();
            }
        }
    }
}