using JabjaiMainClass;
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
    /// Summary description for getListProduct
    /// </summary>
    public class getListProduct : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            string sList = fcommon.ReplaceInjection(context.Request["List"]);
            string sHtml = "";
            string sProduct = _str;
            int nTypeProduct = 0;
            string script = "";
            //            sHtml += @"<script language=""javascript"" type=""text/javascript""> 
            //             $(document).ready(function () {
            //                        $('div[id*=Product_]').onclick(function (event) {
            //                            CheckDat(this.name);
            //                        });
            //                    });
            //            </script>";
            foreach (var _data in _db.TProducts.Where(w => w.sProduct.Contains(sProduct) && string.IsNullOrEmpty(w.cDel)))
            {
                string schecked = "";
                if (sList.IndexOf("Product_" + _data.nProductID + ",") > -1)
                {
                    schecked = "color : red;";
                }
                sHtml += string.Format(@"<div id='Product_{0}' style=""cursor:pointer; {2}"" onclick='", _data.nProductID, _data.sProduct, schecked);
                sHtml += @" if(this.css(""color"") != ""red"") this.css(""color"", ""Red"");";
                sHtml += string.Format("'>{1}</div>", _data.nProductID, _data.sProduct, schecked);

            }

            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sHtml.ToString());
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