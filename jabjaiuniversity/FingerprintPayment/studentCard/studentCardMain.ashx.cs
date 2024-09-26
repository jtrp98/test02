﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.studentCard
{
    /// <summary>
    /// Summary description for studentCardMain1
    /// </summary>
    public class studentCardMain1 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                dynamic rss = new JObject();

                string date = fcommon.ReplaceInjection(context.Request["date"]);
                TStudentCardInfo card = new TStudentCardInfo();

                var check = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardExpireDate" && w.cDel == null).FirstOrDefault();
                if (check == null)
                {
                    //int count = _db.TStudentCardInfoes.Select(s => s.nStudentCardInfoId).DefaultIfEmpty(0).Max();
                    //count = count + 1;
                    card = new TStudentCardInfo();
                    //card.nStudentCardInfoId = count;
                    card.cDel = null;
                    card.elementName = "CardExpireDate";
                    card.SchoolID = userData.CompanyID;
                    card.CreatedDate = DateTime.Today;
                    card.CreatedBy = userData.UserID;


                    if (date != "")
                    {
                        DateTime data = DateTime.ParseExact(date, "dd/MM/yyyy", new CultureInfo("th-TH"));
                        card.date = data;
                    }
                    _db.TStudentCardInfoes.Add(card);
                }
                else
                {
                    if (date != "")
                    {
                        DateTime data = DateTime.ParseExact(date, "dd/MM/yyyy", new CultureInfo("th-TH"));
                        check.date = data;
                        check.UpdatedBy = userData.UserID;
                        check.UpdatedDate = DateTime.Today;
                    }
                }

                _db.SaveChanges();

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