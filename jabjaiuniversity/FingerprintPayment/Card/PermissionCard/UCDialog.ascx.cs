using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Card.PermissionCard
{
    public partial class UCDialog : System.Web.UI.UserControl
    {
        public class Model
        {
            public int StudentID { get; set; }
            public string Term { get;  set; }
            public int No { get; set; }
            public DateTime Date { get; set; }
            public string StudentCode { get; set; }
            public string StudentName { get; set; }
            public string Class { get; set; }

            public List<SelectModel> TypeList { get; set; } = new List<SelectModel>();
            public int SelectedType { get; set; }
            public int Time{ get; set; }
            public bool IsAttach{ get; set; }
            public string AttachUrl { get; set; }
            public DateTime? StartDate { get; set; }
            public TimeSpan? StartTime { get; set; }
            public DateTime? EndDate { get; set; }
            public TimeSpan? EndTime { get; set; }
            public string Cause { get; set; }
            public string Note { get; set; }
         

            public class SelectModel
            {
                public int Value { get; set; }
                public string Text { get; set; }
            }
        }

        public Model ModelData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            //MODEL = new ModelDetail();
        }
    }
}