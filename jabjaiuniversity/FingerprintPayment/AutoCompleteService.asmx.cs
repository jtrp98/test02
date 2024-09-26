using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for AutoCompleteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoCompleteService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string[] getAutoListTEmployees(string prefixText, int count)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<string> autoList = new List<string>(count);
                if (!prefixText.StartsWith("%"))
                {
                    foreach (var _data in _db.TEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(prefixText) && string.IsNullOrEmpty(w.cDel)))
                    {
                        autoList.Add(_data.sName + " " + _data.sLastname);
                    }
                    return autoList.ToArray();
                }
                else
                {
                    return new string[0];
                }
            }
        }

        [WebMethod]
        public string[] getAutoListTProduct(string prefixText, int count)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<string> autoList = new List<string>(count);
                if (!prefixText.StartsWith("%"))
                {
                    foreach (var _data in _db.TProducts.Where(w => ((w.sProduct).Contains(prefixText) || w.sBarCode.Contains(prefixText)) && string.IsNullOrEmpty(w.cDel)))
                    {
                        autoList.Add(_data.sProduct);
                    }
                    return autoList.ToArray();
                }
                else
                {
                    return new string[0];
                }
            }
        }

        [WebMethod]
        public string[] getAutoListTProduct(string prefixText, int count, string contextKey)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<string> autoList = new List<string>(count);
                int nType = contextKey == "" ? 0 : int.Parse(contextKey);
                if (!prefixText.StartsWith("%"))
                {
                    foreach (var _data in _db.TProducts.Where(w => ((w.sProduct).Contains(prefixText) || w.sBarCode.Contains(prefixText)) && string.IsNullOrEmpty(w.cDel)))
                    {
                        if (_data.nType == nType && nType != 0)
                        {
                            autoList.Add(_data.sProduct);
                        }
                        else if (nType == 0)
                        {
                            autoList.Add(_data.sProduct);
                        }
                    }
                    return autoList.ToArray();
                }
                else
                {
                    return new string[0];
                }
            }
        }

        [WebMethod]
        public string[] getAutoListTUser(string prefixText, int count)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<string> autoList = new List<string>(count);
                if (!prefixText.StartsWith("%"))
                {
                    foreach (var _data in _db.TUser.Where(w => ((w.sName + " " + w.sLastname).Contains(prefixText) || w.sIdentification.Contains(prefixText)) && string.IsNullOrEmpty(w.cDel)))
                    {
                        autoList.Add(_data.sName + " " + _data.sLastname);
                    }
                    return autoList.ToArray();
                }
                else
                {
                    return new string[0];
                }
            }
        }
    }
}
