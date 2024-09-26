using FingerprintPayment.Report.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{

    public partial class LogSystem : System.Web.UI.Page
    {
        public static string physicalPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            physicalPath = HostingEnvironment.ApplicationPhysicalPath;
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetFiles(string directory)
        {
            string physicalPath = HostingEnvironment.ApplicationPhysicalPath;
            string path = physicalPath + $"\\Log\\" + directory;

            string[] _files = System.IO.Directory.GetFiles(path);
            List<string> files = new List<string>();

            foreach (var values in _files)
            {
                string[] _s = values.Split('\\');
                files.Add(_s[_s.Length - 1]);
            }

            return files;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object ReadFiles(string files, string directory)
        {
            string physicalPath = HostingEnvironment.ApplicationPhysicalPath;
            string path = physicalPath + $"\\Log\\" + directory + "\\" + files;

            string _log = System.IO.File.ReadAllText(path);
            var logStatuses = JsonConvert.DeserializeObject<List<LogStatus>>(_log);

            return logStatuses.OrderByDescending(o => o.LogTime).ToList();
        }

        public class LogStatus
        {
            public string LogTime { get; set; }
            public List<LogDetail> details { get; set; }
            public class LogDetail
            {
                public string LogName { get; set; }
                public int Rows { get; set; }
                public string Status { get; set; }
                public string ErrorMessage { get; set; }
            }
        }
    }
}