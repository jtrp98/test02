using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.Ashx
{
    /// <summary>
    /// Summary description for InitDropdownData
    /// </summary>
    public class InitDropdownData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string table = HttpContext.Current.Request.QueryString["table"];

            var json = "";
            List<object> result = new List<object>();

            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                switch (table)
                {
                    case "Category":
                        result = en.TAssetCategories.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.AssetCategoryId, value = s.Category, code = s.Code }).ToList<object>();
                        break;
                    case "Unit":
                        result = en.TUnits.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.UnitID, value = s.Unit }).ToList<object>();
                        break;
                    case "Product":
                        result = en.TAssetProducts.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.AssetProductId, value = s.Product }).ToList<object>();
                        break;
                    case "ProductForCate":
                        string cateID = HttpContext.Current.Request.QueryString["cateID"];
                        int iCateID = 0;
                        int.TryParse(cateID, out iCateID);

                        result = en.TAssetProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.AssetCategoryId == iCateID).Select(s => new { id = s.AssetProductId, value = s.Product }).ToList<object>();
                        break;
                    case "Department":
                        result = en.TDepartments.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.DepID, value = s.departmentName }).ToList<object>();
                        break;
                    case "Employee":
                        result = en.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.sEmp, value = s.sName + " " + s.sLastname }).ToList<object>();
                        break;
                    case "Year":
                        result = en.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Select(s => new { id = s.numberYear, value = s.numberYear }).ToList<object>();
                        break;

                }
            }

            json = JsonConvert.SerializeObject(result);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}