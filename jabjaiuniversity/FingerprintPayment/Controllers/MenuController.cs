using FingerprintPayment.Class;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>?schoolId=849&empId=423381
        public IHttpActionResult Get(int schoolId, int empId)
        {
            List<EntityMenu> menus = new List<EntityMenu>();

            try
            {
                using (JabJaiMasterEntities dbm = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string query = string.Format(@"
SELECT *
FROM
(
	SELECT sm.GroupMenuID, gm.groupmenu 'GroupMenuName', gm.new_order 'GroupOrder', gm.icon 'GroupIcon', NULL 'GroupUrl'
	, sm.ID 'SegmentID', sm.Name 'SegmentName', sm.nOrder 'SegmentOrder', sm.Class 'SegmentIcon', NULL 'SegmentUrl', ((sm.nOrder-1)%3)+1 'SegmentColumnLayout'
	, m.MenuId 'MenuID', m.MenuName 'MenuName', m.nMenuOrder2 'MenuOrder', m.url 'MenuUrl', m.url 'MenuUrls', (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	FROM  TMenu m 
	LEFT JOIN TSegmentMenu sm ON m.SegmentID = sm.ID
	LEFT JOIN TGroupMenu gm ON sm.GroupMenuID = gm.groupmenuid
	WHERE m.SegmentID IS NOT NULL
	UNION
	SELECT NULL 'GroupMenuID', m.MenuName 'GroupMenuName', 10 'GroupOrder', 'delete' 'GroupIcon', m.url 'GroupUrl'
	, NULL 'SegmentID', NULL 'SegmentName', NULL 'SegmentOrder', NULL 'SegmentIcon', NULL 'SegmentUrl', 1 'SegmentColumnLayout'
	, m.MenuId 'MenuID', NULL 'MenuName', NULL 'MenuOrder', NULL 'MenuUrl', NULL 'MenuUrls', (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	FROM  TMenu m 
	WHERE MenuId = 209
) A
ORDER BY A.GroupOrder, A.SegmentOrder, A.MenuOrder");
                    menus = dbm.Database.SqlQuery<EntityMenu>(query).ToList();

                    int[] AddMorePaddingLeft5ForSegmentID = new int[] { 22, 38 };

                    // Custom menu data
                    foreach (var m in menus)
                    {
                        if (string.IsNullOrEmpty(m.MenuUrl)) continue;

                        if (Array.IndexOf(AddMorePaddingLeft5ForSegmentID, m.SegmentID) != -1)
                        {
                            m.SegmentMoreClass += "pl-5";
                        }


                        m.MenuUrls = new List<EntityUrls>();

                        string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                        var url32Bit = "";
                        var url64Bit = "";

                        var urlTH = "";
                        var urlEN = "";

                        switch (m.MenuUrl)
                        {
                            case "#CR001":
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Url = domainName + "/import/ImportInvoice.xlsx" });
                                break;
                            case "#CR002":
                                AWSTempScanFunction awsTempScanFunction = new AWSTempScanFunction(307);
                                TempScanLastVersion lastVersion = awsTempScanFunction.ReadTempScanVersionS3Object();
                                if (lastVersion.IsSuccess)
                                {
                                    m.MenuName += " " + lastVersion.version;
                                    m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "32 bit", Url = lastVersion.setupx86.url });
                                    m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "64 bit", Url = lastVersion.setupx64.url });
                                }
                                else
                                {
                                    m.MenuName += " ?";
                                    m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "32 bit", Url = "#" });
                                    m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "64 bit", Url = "#" });
                                }
                                break;
                            case "#CR003":
                                urlTH = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2030%20TH%204key%20[05042021].pdf";
                                urlEN = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2030%20ENG%204key%20[05042021].pdf";

                                m.MenuName = m.MenuName.Replace("ไทย EN", "");
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "ไทย", Url = urlTH });
                                m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "EN", Url = urlEN });
                                break;
                            case "#CR004":
                                urlTH = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2050%20TH%204key%20[05042021].pdf";
                                urlEN = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2050%20ENG%204key%20[05042021].pdf";

                                m.MenuName = m.MenuName.Replace("ไทย EN", "");
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "ไทย", Url = urlTH });
                                m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "EN", Url = urlEN });
                                break;
                            case "#CR005":
                                urlTH = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2060%20th%201-4%204key%20[16042021].pdf";
                                urlEN = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%2060%20EN%204key%20[16042021].pdf";

                                m.MenuName = m.MenuName.Replace("ไทย EN", "");
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "ไทย", Url = urlTH });
                                m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "EN", Url = urlEN });
                                break;
                            case "#CR006":
                                urlTH = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%20100%20TH%204key%20[05042021].pdf";
                                urlEN = domainName + "/import/pair_answe_sheet/pair%20answer%20sheet%20100%20ENG%204key%20[05042021].pdf";

                                m.MenuName = m.MenuName.Replace("ไทย EN", "");
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "ไทย", Url = urlTH });
                                m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "EN", Url = urlEN });
                                break;
                            case "#CR007":
                                url32Bit = "https://schoolbrightapp.s3.ap-southeast-1.amazonaws.com/SchoolBrightShop/x32/SB_Shop_32_10016.exe";
                                url64Bit = "https://schoolbrightapp.s3.ap-southeast-1.amazonaws.com/SchoolBrightShop/x64/SB_Shop_64_10016.exe";

                                m.MenuUrls.Add(new EntityUrls { Order = 1, Label = "32 bit", Url = url32Bit });
                                m.MenuUrls.Add(new EntityUrls { Order = 2, Label = "64 bit", Url = url64Bit });
                                break;
                            default:
                                m.MenuUrls.Add(new EntityUrls { Order = 1, Url = domainName + m.MenuUrl });
                                break;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return Ok(new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = err.Message
                });
            }

            return Ok(new
            {
                statusCode = HttpStatusCode.OK,
                message = "OK",
                menus
            });
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }


        public class EntityMenu
        {
            [JsonProperty(PropertyName = "groupMenuId")]
            public int? GroupMenuID { get; set; }

            [JsonProperty(PropertyName = "groupMenuName")]
            public string GroupMenuName { get; set; }

            [JsonProperty(PropertyName = "groupOrder")]
            public int? GroupOrder { get; set; }

            [JsonProperty(PropertyName = "groupIcon")]
            public string GroupIcon { get; set; }

            [JsonProperty(PropertyName = "groupUrl")]
            public string GroupUrl { get; set; }



            [JsonProperty(PropertyName = "segmentId")]
            public int? SegmentID { get; set; }

            [JsonProperty(PropertyName = "segmentName")]
            public string SegmentName { get; set; }

            [JsonProperty(PropertyName = "segmentOrder")]
            public int? SegmentOrder { get; set; }

            [JsonProperty(PropertyName = "segmentIcon")]
            public string SegmentIcon { get; set; }

            [JsonProperty(PropertyName = "segmentUrl")]
            public string SegmentUrl { get; set; }

            [JsonProperty(PropertyName = "segmentColumnLayout")]
            public int SegmentColumnLayout { get; set; }

            [JsonProperty(PropertyName = "segmentMoreClass")]
            public string SegmentMoreClass { get; set; }



            [JsonProperty(PropertyName = "menuId")]
            public int MenuID { get; set; }

            [JsonProperty(PropertyName = "menuName")]
            public string MenuName { get; set; }

            [JsonProperty(PropertyName = "menuOrder")]
            public int? MenuOrder { get; set; }

            [JsonProperty(PropertyName = "menuUrl")]
            public string MenuUrl { get; set; }

            [JsonProperty(PropertyName = "menuUrls")]
            public List<EntityUrls> MenuUrls { get; set; }

            [JsonProperty(PropertyName = "target")]
            public string Target { get; set; }
        }

        public class EntityUrls
        {
            [JsonProperty(PropertyName = "order")]
            public int Order { get; set; }

            [JsonProperty(PropertyName = "label")]
            public string Label { get; set; }

            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }
        }

    }
}