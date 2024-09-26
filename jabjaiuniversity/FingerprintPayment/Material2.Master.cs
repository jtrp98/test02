using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Web.Services;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using JabjaiMainClass.Models;
using FingerprintPayment.Class;

namespace FingerprintPayment
{

    public partial class BaseMasterPage : System.Web.UI.MasterPage
    {
        protected List<TokenModel.MenuList> MENU = new List<TokenModel.MenuList>();
        protected bool IS_ADMIN = false;
        protected bool IS_DEV = false;
        protected string NOTE_HEAD = "";
        protected List<string> ANNOUNCE_LIST = new List<string>();

        private List<string> NoAuthPage = new List<string>() {
            "/AdminMain.aspx",
            "/AdminMain2.aspx",
            "/Default.aspx",
            "/NoPermission.aspx",
            "/UnderMaintenance.aspx",
            "/import/downloadList.aspx",
            "/UpdateLog.aspx",
            "/grade/AssessmentScoreExport.aspx",
            "/grade/AssessmentScoreHistoryExport.aspx",
            "/grade/graderankingexport.aspx",
            "/StudentInfo/StudentListQRCode.aspx"
        };

        private List<string> SupperAdminPage = new List<string>() {
            "/Modules/Invoices/InvoicesList.aspx",
        };

        protected void Page_Init(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            //(!string.IsNullOrEmpty(HttpContext.Current.Session["menus"]+""))//
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
                //var menus = HttpCountext.Current.Session["menus"] as List<JabjaiMainClass.Models.TokenModel.MenuList>;

                //using (var dbsmaster = Connection.MasterEntities(ConnectionDB.Read))
                //{ }
                MENU = userData.Menus;
                IS_ADMIN = userData.IsAdmin;

                var url = Request.Url.AbsolutePath.ToLower();

                if (NoAuthPage.Any(o => o.ToLower() == url))
                {
                    return;
                }

                if (SupperAdminPage.Any(o => o.ToLower() == url))
                {
                    if (IS_ADMIN)
                        return;
                }
                else
                {
                    var menuid = new List<int>(); //(100, 109).Contains(menuid)
                    if (url == "/grade/GradeRoomList.aspx".ToLower())
                    {
                        string mode = Request.QueryString["mode"];
                        if (mode == "1")
                        {
                            menuid.Add(109);
                        }
                        else if (mode == "2")
                        {
                            menuid.Add(100);
                        }
                    }
                    else if ("/grade/bp5detailallrooms.aspx" == url)
                    {
                        menuid = userData.Menus.Where(o => o.url.ToLower() == "/grade/bp5detail.aspx")
                           .Select(o => o.id)
                           .ToList();
                    }
                    else
                    {
                        menuid = userData.Menus.Where(o => o.url.ToLower() == url)
                            .Select(o => o.id)
                            .ToList();
                    }

                    var role = userData.Menus.Where(o => menuid.Contains(o.id)).Min(o => o.role);

                    if (role.HasValue)
                    {
                        //Modify = 0,
                        //View = 1,
                        //NoPermission = 2,
                        if (role == 2)
                        {
                            Response.Redirect("~/NoPermission.aspx");
                        }
                    }
                    else//in case not check role will null
                    {
                        Response.Redirect("~/NoPermission.aspx");
                    }



                }

            }
            else
            {
                Response.Redirect("~/Default.aspx?returnUrl=" + Request.Url.PathAndQuery);
            }

            // Be sure to call the base class's OnLoad method!
            //base.(e);
        }
    }

    public partial class Material2 : BaseMasterPage
    {
        public static RoleStatus Permission_Page = new RoleStatus();

        protected void Page_Load(object sender, EventArgs e)
        {
            IS_DEV = ConfigurationManager.AppSettings["Menu_demo"] + "" == "1";

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            if (!IsPostBack)
            {
                int empID = Convert.ToInt32(Session["sEmpID"]);
                int schoolID = Convert.ToInt32(Session["nCompany"]);

                RenderMenu(schoolID, empID);

                RenderResponsiveMenu(schoolID, empID);

                RenderNoteHead(schoolID);
            }
        }

        private void RenderNoteHead(int schoolID)
        {
            using (var dbm = Connection.MasterEntities(ConnectionDB.Read))
            {
                var dateNow = DateTime.Today;
                NOTE_HEAD = dbm.TNews2.Where(o => (o.SchoolID == schoolID || o.SchoolID == null) && o.IsDelete == false
                            && o.IsNoteHead == true && dateNow >= o.StartDate && dateNow <= o.EndDate)
                    .OrderBy(o => o.StartDate)
                    .Select(o => o.NoteHead)
                    .FirstOrDefault();
            }
        }

        private void RenderMenu(int schoolID, int empID)
        {
            using (JabJaiMasterEntities dbm = Connection.MasterEntities(ConnectionDB.Read))
            {
                string query = ($@"
SELECT *
FROM
(
	SELECT m.MenuId 'MenuID', m.MenuName 'MenuName',{(IS_DEV ? " ISNULL( m.urlDev, m.url ) " : " m.url ")}  'MenuUrl', m.class 'MenuClass', m.title 'MenuTitle', m.target 'MenuTarget'
	, (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	, sm.ID 'SegmentID', m.nMenuOrder2 'MenuOrder2', sm.Name 'SegmentName', sm.GroupMenuID, sm.nOrder 'SegmentOrder', sm.Class 'SegmentIcon', gm.groupmenu 'GroupMenu', gm.new_order 'GroupOrder', gm.icon 'Icon'
	FROM  TMenu m 
	LEFT JOIN TSegmentMenu sm ON m.SegmentID = sm.ID
	LEFT JOIN TGroupMenu gm ON sm.GroupMenuID = gm.groupmenuid
	WHERE m.SegmentID IS NOT NULL AND sm.Active = 1 AND gm.active = 1 AND m.showmenu = 1 AND m.active = 1 {(IS_DEV ? "" : " AND m.demo = 0 ")}
	UNION
	SELECT m.MenuId 'MenuID', m.MenuName 'MenuName', {(IS_DEV ? " ISNULL( m.urlDev, m.url ) " : " m.url ")}  'MenuUrl', m.class 'MenuClass', m.title 'MenuTitle', m.target 'MenuTarget'
	, (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	, 0 'SegmentID', m.nMenuOrder2 'MenuOrder2', NULL 'SegmentName', 0 'GroupMenuID', NULL 'SegmentOrder', NULL 'SegmentIcon', m.MenuName 'GroupMenu', 10 'GroupOrder', 'delete' 'Icon'
	FROM  TMenu m 
	WHERE MenuId = 209
) A
ORDER BY A.GroupOrder, A.SegmentOrder, A.MenuOrder2");
                var _menu = dbm.Database.SqlQuery<EntityMenu>(query).ToList();
                var menus = from a in _menu.Where(o => o.SegmentID != null)
                            from b in MENU.Where(o => o.id == a.MenuID).DefaultIfEmpty()
                            orderby a.GroupOrder, a.SegmentOrder, a.MenuOrder2
                            select new
                            {
                                a.GroupMenuID,
                                a.GroupMenu,
                                a.GroupOrder,
                                a.Icon,
                                a.SegmentID,
                                a.SegmentName,
                                a.SegmentOrder,
                                a.SegmentIcon,
                                a.MenuUrl,
                                a.MenuName,
                                a.Target,
                                Role = (a.GroupMenuID == 27 ? 0 : ((b?.role) ?? 2)),
                            };//GroupMenuID == 27 download form

                int[] AddMorePaddingLeft5ForSegmentID = new int[] { 22, 38 };

                string iMenu = "";
                foreach (var groupMenu in menus.Where(w => w.SegmentID != null).GroupBy(m => new { m.GroupMenuID, m.GroupMenu, m.GroupOrder, m.Icon }))
                {
                    if (groupMenu.Key.GroupMenuID == 0)
                    {
                        var menuObj = menus.Where(w => w.GroupOrder == groupMenu.Key.GroupOrder).FirstOrDefault();

                        if (menuObj.Role == 2)
                        {
                            iMenu += string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link menu-link disabled"">
                                                    <i class=""material-icons"">{0}</i>
                                                    <p style=""font-weight: bold;"">{1}</p>
                                                </a>
                                            </li>", groupMenu.Key.Icon, groupMenu.Key.GroupMenu);
                        }
                        else
                        {
                            iMenu += string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link nav-menu-a"" href=""{2}"">
                                                    <i class=""material-icons"">{0}</i>
                                                    <p style=""font-weight: bold;"">{1}</p>
                                                </a>
                                            </li>", groupMenu.Key.Icon, groupMenu.Key.GroupMenu, menuObj?.MenuUrl);
                        }
                    }
                    else
                    {
                        string jMenu = "";

                        string[] cMenu = new string[3] { "", "", "" };

                        foreach (var segmentMenu in groupMenu.Where(w => w.GroupMenuID == groupMenu.Key.GroupMenuID).GroupBy(m => new { m.SegmentID, m.SegmentName, m.SegmentOrder, m.SegmentIcon }))
                        {
                            string kMenu = "";
                            foreach (var menu in segmentMenu.Where(w => w.GroupMenuID == groupMenu.Key.GroupMenuID && w.SegmentID == segmentMenu.Key.SegmentID).GroupBy(m => new { m.MenuUrl, m.MenuName, m.Target, m.Role }))
                            {
                                if (menu.Key.Role == 2)
                                {
                                    kMenu += string.Format(@"<li class=""nav-item"" style=""""><span class=""nav-link menu-link disabled"" >{1}</span></li>", menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                }
                                else
                                {
                                    if (menu.Key.MenuUrl?.IndexOf("#CR") == -1)
                                    {
                                        kMenu += string.Format(@"<li class=""nav-item"" style=""""><a class=""nav-link menu-link"" href=""{0}"" target=""{2}"">{1}</a></li>", menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                    }
                                    else
                                    {
                                        kMenu += CustomRenderMenu(menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                    }
                                }

                            }

                            jMenu = string.Format(@"<div class=""row row-section{3}"">
                                                    <div class=""col-md-12 col-section"">
                                                        <ul class=""nav nav-section"">
                                                            <li><i class=""material-icons menu-icon"">{0}</i><p class=""menu-head"" style=""font-weight: bold;"">{1}</p>
                                                            </li>
                                                            {2}
                                                        </ul>
                                                    </div>
                                                </div>", segmentMenu.Key.SegmentIcon, segmentMenu.Key.SegmentName, kMenu, Array.IndexOf(AddMorePaddingLeft5ForSegmentID, segmentMenu.Key.SegmentID) != -1 ? " pl-5" : "");

                            cMenu[(segmentMenu.Key.SegmentOrder == null ? 0 : segmentMenu.Key.SegmentOrder.Value - 1) % 3] += jMenu;
                        }

                        iMenu += string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link nav-menu-a"" href=""#menu{2}"">
                                                    <i class=""material-icons"">{0}</i>
                                                    <p style=""font-weight: bold;"">{1} <b class=""caret-right""></b></p>
                                                </a>
                                                <div class=""collapse"" id=""menu{2}"">
                                                    <ul class=""nav nav-drop-right"" style="""">
                                                        <li class=""nav-item "">
                                                            <div class=""container flex-row"">
                                                                <div class=""col-md-4"">
                                                                    {3}
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </li>", groupMenu.Key.Icon, groupMenu.Key.GroupMenu, groupMenu.Key.GroupMenuID, string.Join(string.Format(@"</div><div class=""col-md-4"">"), cMenu));
                    }
                }

                ltrMenu.Text = iMenu;
            }
        }

        private void RenderResponsiveMenu(int schoolID, int empID)
        {
            using (JabJaiMasterEntities dbm = Connection.MasterEntities(ConnectionDB.Read))
            {
                string query = ($@"
SELECT *
FROM
(
	SELECT m.MenuId 'MenuID', m.MenuName 'MenuName',{(IS_DEV ? " ISNULL( m.urlDev, m.url ) " : " m.url ")}  'MenuUrl', m.class 'MenuClass', m.title 'MenuTitle', m.target 'MenuTarget'
	, (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	, sm.ID 'SegmentID', m.nMenuOrder2 'MenuOrder2', sm.Name 'SegmentName', sm.GroupMenuID, sm.nOrder 'SegmentOrder', sm.Class 'SegmentIcon', gm.groupmenu 'GroupMenu', gm.new_order 'GroupOrder', gm.icon 'Icon'
	FROM  TMenu m 
	LEFT JOIN TSegmentMenu sm ON m.SegmentID = sm.ID
	LEFT JOIN TGroupMenu gm ON sm.GroupMenuID = gm.groupmenuid
	WHERE m.SegmentID IS NOT NULL AND sm.Active = 1 AND m.active = 1 {(IS_DEV ? "" : " AND m.demo = 0 ")}
	UNION
	SELECT m.MenuId 'MenuID', m.MenuName 'MenuName', {(IS_DEV ? " ISNULL( m.urlDev, m.url ) " : " m.url ")}  'MenuUrl', m.class 'MenuClass', m.title 'MenuTitle', m.target 'MenuTarget'
	, (CASE WHEN m.target = '0' THEN '_self' WHEN m.target = '1' THEN '_blank' WHEN m.target = '2' THEN '_top' WHEN m.target = '3' THEN '_parent' ELSE '' END) 'Target'
	, 0 'SegmentID', m.nMenuOrder2 'MenuOrder2', NULL 'SegmentName', 0 'GroupMenuID', NULL 'SegmentOrder', NULL 'SegmentIcon', m.MenuName 'GroupMenu', 10 'GroupOrder', 'delete' 'Icon'
	FROM  TMenu m 
	WHERE MenuId = 209
) A
ORDER BY A.GroupOrder, A.SegmentOrder, A.MenuOrder2");
                var _menu = dbm.Database.SqlQuery<EntityMenu>(query).ToList();
                var menus = from a in _menu.Where(o => o.SegmentID != null)
                            from b in MENU.Where(o => o.id == a.MenuID).DefaultIfEmpty()
                            orderby a.GroupOrder, a.SegmentOrder, a.MenuOrder2
                            select new
                            {
                                a.GroupMenuID,
                                a.GroupMenu,
                                a.GroupOrder,
                                a.Icon,
                                a.SegmentID,
                                a.SegmentName,
                                a.SegmentOrder,
                                a.SegmentIcon,
                                a.MenuUrl,
                                a.MenuName,
                                a.Target,
                                Role = b?.role,
                            };

                string iMenu = "";
                foreach (var groupMenu in menus.Where(w => w.SegmentID != null).GroupBy(m => new { m.GroupMenuID, m.GroupMenu, m.GroupOrder, m.Icon }))
                {
                    if (groupMenu.Key.GroupMenuID == 0)
                    {
                        var menuObj = _menu.Where(w => w.GroupOrder == groupMenu.Key.GroupOrder).FirstOrDefault();

                        iMenu += string.Format(@"<li class=""nav-item"">
                                                    <a class=""nav-link nav-menu-a"" href=""{2}"">
                                                        <i class=""material-icons"">{0}</i>
                                                        <p style=""font-weight: bold;"">{1}</p>
                                                    </a>
                                                </li>", groupMenu.Key.Icon, groupMenu.Key.GroupMenu, menuObj?.MenuUrl);
                    }
                    else
                    {
                        string jMenu = "";
                        foreach (var segmentMenu in groupMenu.Where(w => w.GroupMenuID == groupMenu.Key.GroupMenuID).GroupBy(m => new { m.SegmentID, m.SegmentName, m.SegmentOrder, m.SegmentIcon }))
                        {
                            string kMenu = "";
                            foreach (var menu in segmentMenu.Where(w => w.GroupMenuID == groupMenu.Key.GroupMenuID && w.SegmentID == segmentMenu.Key.SegmentID).GroupBy(m => new { m.MenuUrl, m.MenuName, m.Target, m.Role }))
                            {
                                if (menu.Key.Role == 2)
                                {
                                    kMenu += string.Format(@"<li class=""nav-item disabled"">
                                                                <a class=""nav-link"" href=""#"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">{1}</span>
                                                                </a>
                                                             </li>", menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                }
                                else
                                {
                                    if (menu.Key.MenuUrl?.IndexOf("#CR") == -1)
                                    {
                                        kMenu += string.Format(@"<li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{0}"" target=""{2}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">{1}</span>
                                                                </a>
                                                             </li>", menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                    }
                                    else
                                    {
                                        kMenu += CustomRenderResponsiveMenu(menu.Key.MenuUrl, menu.Key.MenuName, menu.Key.Target);
                                    }
                                }

                            }

                            jMenu += string.Format(@"<li class=""nav-item"">
                                                        <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#sMenu{0}"" aria-expanded=""false"">
                                                            <i class=""material-icons"">{1}</i>
                                                            <p> {2}<b class=""caret""></b></p>
                                                        </a>
                                                        <div class=""collapse"" id=""sMenu{0}"">
                                                            <ul class=""nav"">
                                                                {3}
                                                            </ul>
                                                        </div>
                                                    </li>", segmentMenu.Key.SegmentID, segmentMenu.Key.SegmentIcon, segmentMenu.Key.SegmentName, kMenu);
                        }

                        iMenu += string.Format(@"<li class=""nav-item"">
                                                        <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#gMenu{0}"" aria-expanded=""false"">
                                                            <i class=""material-icons"">{1}</i>
                                                            <p> {2}<b class=""caret""></b></p>
                                                        </a>
                                                        <div class=""collapse"" id=""gMenu{0}"">
                                                            <ul class=""nav"">
                                                                {3}
                                                            </ul>
                                                        </div>
                                                    </li>", groupMenu.Key.GroupMenuID, groupMenu.Key.Icon, groupMenu.Key.GroupMenu, jMenu);

                    }
                }

                ltrResponsiveMenu.Text = iMenu;
            }
        }

        private string CustomRenderMenu(string menuUrl, string menuName, string target)
        {
            string kMenu = "";

            var url = "";

            var url32Bit = "";
            var url64Bit = "";

            var urlTH = "";
            var urlEN = "";

            switch (menuUrl)
            {
                case "#CR001":
                    kMenu = string.Format(@"<li class=""nav-item""><a class=""nav-link menu-link"" href=""#"" onclick=""DOWNLOAD_IMPORT_INVOICE_EXCEL_FILE();"">{0}</a></li>", menuName);
                    break;
                case "#CR002":
                    TempScanLastVersion lastVersion = ComFunction.ReadTempScanVersionFromApi();
                    if (lastVersion.IsSuccess)
                    {
                        kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto interstellar"">
          <span class=""nav-link menu-link"">{0} <span>{4}</span></span>
        </div>
        <div class=""ml-auto x86"">
          <a class=""nav-link menu-link"" href=""{1}"" target=""{3}"">32 bit</a>
        </div>
        <div class=""ml-auto x64"">
          <a class=""nav-link menu-link"" href=""{2}"" target=""{3}"">64 bit</a>
        </div>
    </div>                                                
</li>", menuName, lastVersion.setupx86.url, lastVersion.setupx64.url, target, lastVersion.version);
                    }
                    else
                    {
                        kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto interstellar"">
          <span class=""nav-link menu-link"">{0} <span>?</span></span>
        </div>
        <div class=""ml-auto x86"">
          <a class=""nav-link menu-link"" href=""{1}"" target=""{3}"">32 bit</a>
        </div>
        <div class=""ml-auto x64"">
          <a class=""nav-link menu-link"" href=""{2}"" target=""{3}"">64 bit</a>
        </div>
    </div>                                                
</li>", menuName, "#", "#", target);
                    }
                    break;
                case "#CR003":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 30 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 30 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto answer-sheet"">
          <span class=""nav-link menu-link"">{0}</span>
        </div>
        <div class=""ml-auto th-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{1}"" target=""{3}"">ไทย</a>
        </div>
        <div class=""ml-auto en-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{2}"" target=""{3}"">EN</a>
        </div>
    </div>                                                
</li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);

                    break;
                case "#CR004":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 50 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 50 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto answer-sheet"">
          <span class=""nav-link menu-link"">{0}</span>
        </div>
        <div class=""ml-auto th-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{1}"" target=""{3}"">ไทย</a>
        </div>
        <div class=""ml-auto en-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{2}"" target=""{3}"">EN</a>
        </div>
    </div>                                                
</li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR005":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 60 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 60 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto answer-sheet"">
          <span class=""nav-link menu-link"">{0}</span>
        </div>
        <div class=""ml-auto th-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{1}"" target=""{3}"">ไทย</a>
        </div>
        <div class=""ml-auto en-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{2}"" target=""{3}"">EN</a>
        </div>
    </div>                                                
</li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR006":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 100 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 100 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto answer-sheet"">
          <span class=""nav-link menu-link"">{0}</span>
        </div>
        <div class=""ml-auto th-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{1}"" target=""{3}"">ไทย</a>
        </div>
        <div class=""ml-auto en-version"">
          <a class=""nav-link menu-link"" style=""padding-left: 0px; margin-left: 10px; margin-right: 0px;"" href=""{2}"" target=""{3}"">EN</a>
        </div>
    </div>                                                
</li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR007":
                    var sbShop = ComFunction.GetSBShopLastVersion();
                    url32Bit = "https://windows-install.obs.ap-southeast-2.myhuaweicloud.com/SchoolBrightShop/x32/SBShopx32_102.exe";
                    url64Bit = "https://windows-install.obs.ap-southeast-2.myhuaweicloud.com/SchoolBrightShop/x64/SBShopx64_102.exe";

                    if (sbShop.IsSuccess)
                    {
                        url32Bit = sbShop.setupx86.url;
                        url64Bit = sbShop.setupx64.url;
                    }

                    kMenu = string.Format(@"
<li class=""nav-item"" style=""display: flex;"">
    <div class=""d-flex"">
        <div class=""mr-auto interstellar"">
          <span class=""nav-link menu-link"">{0}</span>
        </div>
        <div class=""ml-auto x86"" style=""height:32px;"">
          <a class=""nav-link menu-link"" href=""{1}"" target=""{3}"">32 bit</a>
        </div>
        <div class=""ml-auto x64""  style=""height:32px;"">
          <a class=""nav-link menu-link"" href=""{2}"" target=""{3}"">64 bit</a>
        </div>
    </div>                                                
</li>", menuName + " " + sbShop.version, url32Bit, url64Bit, target);
                    break;
            }

            return kMenu;
        }

        private string CustomRenderResponsiveMenu(string menuUrl, string menuName, string target)
        {
            string kMenu = "";

            var url = "";

            var url32Bit = "";
            var url64Bit = "";

            var urlTH = "";
            var urlEN = "";

            switch (menuUrl)
            {
                case "#CR001":
                    kMenu = string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link"" href=""#"" onclick=""DOWNLOAD_IMPORT_INVOICE_EXCEL_FILE();"">
                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                    <span class=""sidebar-normal"">{0}</span>
                                                </a>
                                             </li>", menuName);
                    break;
                case "#CR002":
                    TempScanLastVersion lastVersion = ComFunction.ReadTempScanVersionFromApi();
                    if (lastVersion.IsSuccess)
                    {
                        kMenu = string.Format(@"<li class=""nav-item"">
                                                    <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR002"" aria-expanded=""false"">
                                                        <span class=""sidebar-mini""> &nbsp; </span>
                                                        <span class=""sidebar-normal"">{0} <span>{4}</span> <b class=""caret""></b></span>
                                                    </a>
                                                    <div class=""collapse"" id=""mCR002"">
                                                        <ul class=""nav"">
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">32 bit</span>
                                                                </a>
                                                             </li>
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">64 bit</span>
                                                                </a>
                                                             </li>
                                                        </ul>
                                                    </div>
                                                </li>", menuName, lastVersion.setupx86.url, lastVersion.setupx64.url, target, lastVersion.version);
                    }
                    else
                    {
                        kMenu = string.Format(@"<li class=""nav-item"">
                                                    <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR002"" aria-expanded=""false"">
                                                        <span class=""sidebar-mini""> &nbsp; </span>
                                                        <span class=""sidebar-normal"">{0} <span>?</span> <b class=""caret""></b></span>
                                                    </a>
                                                    <div class=""collapse"" id=""mCR002"">
                                                        <ul class=""nav"">
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">32 bit</span>
                                                                </a>
                                                             </li>
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">64 bit</span>
                                                                </a>
                                                             </li>
                                                        </ul>
                                                    </div>
                                                </li>", menuName, "#", "#", target);

                    }
                    break;
                case "#CR003":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 30 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 30 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR003"" aria-expanded=""false"">
                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                    <span class=""sidebar-normal"">{0} <b class=""caret""></b></span>
                                                </a>
                                                <div class=""collapse"" id=""mCR003"">
                                                    <ul class=""nav"">
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">ไทย</span>
                                                            </a>
                                                            </li>
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">Eng</span>
                                                            </a>
                                                            </li>
                                                    </ul>
                                                </div>
                                            </li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR004":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 50 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 50 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR004"" aria-expanded=""false"">
                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                    <span class=""sidebar-normal"">{0} <b class=""caret""></b></span>
                                                </a>
                                                <div class=""collapse"" id=""mCR004"">
                                                    <ul class=""nav"">
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">ไทย</span>
                                                            </a>
                                                            </li>
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">Eng</span>
                                                            </a>
                                                            </li>
                                                    </ul>
                                                </div>
                                            </li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR005":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 60 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 60 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR005"" aria-expanded=""false"">
                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                    <span class=""sidebar-normal"">{0} <b class=""caret""></b></span>
                                                </a>
                                                <div class=""collapse"" id=""mCR005"">
                                                    <ul class=""nav"">
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">ไทย</span>
                                                            </a>
                                                            </li>
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">Eng</span>
                                                            </a>
                                                            </li>
                                                    </ul>
                                                </div>
                                            </li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR006":
                    urlTH = "/import/pair_answe_sheet/pair answer sheet 100 TH 4key [22032023].pdf";
                    urlEN = "/import/pair_answe_sheet/pair answer sheet 100 EN 4key [22032023].pdf";
                    kMenu = string.Format(@"<li class=""nav-item"">
                                                <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR006"" aria-expanded=""false"">
                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                    <span class=""sidebar-normal"">{0} <b class=""caret""></b></span>
                                                </a>
                                                <div class=""collapse"" id=""mCR006"">
                                                    <ul class=""nav"">
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">ไทย</span>
                                                            </a>
                                                            </li>
                                                        <li class=""nav-item"">
                                                            <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                <span class=""sidebar-mini""> &nbsp; </span>
                                                                <span class=""sidebar-normal"">Eng</span>
                                                            </a>
                                                            </li>
                                                    </ul>
                                                </div>
                                            </li>", menuName.Replace("ไทย EN", ""), urlTH, urlEN, target);
                    break;
                case "#CR007":

                    var sbShop = ComFunction.GetSBShopLastVersion();
                    url32Bit = "https://windows-install.obs.ap-southeast-2.myhuaweicloud.com/SchoolBrightShop/x32/SBShopx32_102.exe";
                    url64Bit = "https://windows-install.obs.ap-southeast-2.myhuaweicloud.com/SchoolBrightShop/x64/SBShopx64_102.exe";

                    if (sbShop.IsSuccess)
                    {
                        url32Bit = sbShop.setupx86.url;
                        url64Bit = sbShop.setupx64.url;
                    }

                    kMenu = string.Format(@"<li class=""nav-item"">
                                                    <a class=""nav-link collapsed"" data-toggle=""collapse"" href=""#mCR007"" aria-expanded=""false"">
                                                        <span class=""sidebar-mini""> &nbsp; </span>
                                                        <span class=""sidebar-normal"">{0} <b class=""caret""></b></span>
                                                    </a>
                                                    <div class=""collapse"" id=""mCR007"">
                                                        <ul class=""nav"">
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{1}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">32 bit</span>
                                                                </a>
                                                             </li>
                                                            <li class=""nav-item"">
                                                                <a class=""nav-link"" href=""{2}"" target=""{3}"">
                                                                    <span class=""sidebar-mini""> &nbsp; </span>
                                                                    <span class=""sidebar-normal"">64 bit</span>
                                                                </a>
                                                             </li>
                                                        </ul>
                                                    </div>
                                                </li>", menuName + " " + sbShop.version, url32Bit, url64Bit, target);
                    break;
            }

            return kMenu;
        }

        public class EntityMenu
        {
            public int MenuID { get; set; }
            public string MenuName { get; set; }
            public string MenuUrl { get; set; }
            public string MenuClass { get; set; }
            public string MenuTitle { get; set; }
            public int MenuTarget { get; set; }
            public string Target { get; set; }
            public int? SegmentID { get; set; }
            public int? MenuOrder2 { get; set; }
            public string SegmentName { get; set; }
            public int? GroupMenuID { get; set; }
            public int? SegmentOrder { get; set; }
            public string SegmentIcon { get; set; }
            public string GroupMenu { get; set; }
            public int? GroupOrder { get; set; }
            public string Icon { get; set; }
        }
    }
}