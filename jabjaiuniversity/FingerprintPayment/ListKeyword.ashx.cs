using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using JabjaiEntity.DB;
using System.Web.SessionState;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for ListKeyword
    /// </summary>
    public class ListKeyword : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string str = "";
            string _q = context.Request.QueryString["q"].ToLower();
            string _mode = context.Request.QueryString["mode"];
            string _ctrl = context.Request.QueryString["ctrl"];
            string _nemp_id = context.Request.QueryString["nemp_id"];

            switch (_mode)
            {
                case "Employees":
                    var _Employees = _db.TEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(_q)).ToList();
                    if (_Employees.Count > 0)
                    {
                        foreach (var _data in _Employees)
                        {
                            str += string.Format("<li onselect=\"setValueAutoComplete('{0}','{1}'); this.setText('{2}');\">{2}</li>", _ctrl, _data.sEmp, _data.sName + " " + _data.sLastname);
                        }
                    }
                    else str += "<li >พบ<font color=\"red\"> 0 </font>รายการ</li>";
                    break;
                case "User":
                    break;
            }

            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(str.ToString());
            context.Response.End();
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