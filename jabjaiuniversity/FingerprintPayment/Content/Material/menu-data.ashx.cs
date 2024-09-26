using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Content.Material
{
    /// <summary>
    /// Summary description for menu_data
    /// </summary>
    public class menu_data : IHttpHandler
    {
        private static List<MenuData> MENUDATAS = new List<MenuData>();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string q = context.Request.QueryString["q"];

            if (MENUDATAS.Count() == 0)
            {
                using (JabJaiMasterEntities dbm = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string query = string.Format(@"
SELECT m.url, gm.groupmenu+' / '+sm.Name+' / '+m.MenuName 'menu'
FROM TMenu m 
LEFT JOIN TSegmentMenu sm ON m.SegmentID = sm.ID
LEFT JOIN TGroupMenu gm ON sm.GroupMenuID = gm.groupmenuid
WHERE m.SegmentID IS NOT NULL
ORDER BY gm.new_order, sm.nOrder, m.nMenuOrder2");
                    List<MenuData> menus = dbm.Database.SqlQuery<MenuData>(query).ToList();

                    MENUDATAS = menus;
                }
            }

            var md = MENUDATAS.Where(w => w.Menu.Contains(q)).ToList();

            context.Response.Write(JsonConvert.SerializeObject(md));
        }

        public bool IsReusable { get { return false; } }
    }

    public class MenuData
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "menu")]
        public string Menu { get; set; }
    }
}