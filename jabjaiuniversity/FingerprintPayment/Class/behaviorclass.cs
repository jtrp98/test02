using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JabjaiEntity.DB;
using urbanairship;
using JabjaiMainClass;

namespace FingerprintPayment.Class
{
    public class behaviorclass
    {
        public static async void sendnotification(JWTToken.userData userData, int StudentId, TBehavior tBehaviors, decimal ResidualScore)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = userData.CompanyID;
                var dbmasteruser = dbmaster.TUsers.Where(w => w.sID == StudentId && w.nCompany == schoolID).FirstOrDefault();

                int message_id = 0;
                notification _notification = new notification();
                _notification.title = "แจ้งคะแนนพฤติกรรม";
                if (tBehaviors.Type == 0) _notification.message = string.Format("ยินดีด้วยค่ะ\nมีรายงานการเพิ่มคะแนนพฤติกรรม {0} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);
                else _notification.message = string.Format("เสียใจด้วยค่ะ\nมีรายงานการตัดคะแนนพฤติกรรม {0} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);
                _notification.action = "vnd.jabjai.jabjaiapp://deeplink/behavior?message_id=" + message_id;
                _notification.user = dbmasteruser.sID.ToString();

                //dbmaster.TMessages.Add(new TMessage
                //{
                //    UserID = dbmasteruser.sID,
                //    sTitle = _notification.title,
                //    dSend = DateTime.Now,
                //    sMessage = _notification.message,
                //    nType = 1,
                //    nStatus = 0,
                //    nMessageID = message_id
                //});

                database.InsertLog(userData.UserID.ToString(),
                          "ทำการตัดคะแนนความประพฤติ ",
                          userData.Entities,
                          HttpContext.Current.Request, 4, 2, 0);

                //await pushdata.push('"' + _notification.user + '"', _notification.message, _notification.title, DateTime.Now, message_id, schoolID);
                dbmaster.SaveChanges();
            }
        }

        public static async void sendnotification(string sEntities, int StudentId, TBehavior tBehaviors, int ResidualScore)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcomapny = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var dbmasteruser = dbmaster.TUsers.Where(w => w.nSystemID == StudentId && w.nCompany == qcomapny.nCompany).FirstOrDefault();

                int message_id = 0;
                notification _notification = new notification();
                _notification.title = "แจ้งคะแนนพฤติกรรม";
                if (tBehaviors.Type == 0) _notification.message = string.Format("ยินดีด้วยค่ะ\nมีรายงานการเพิ่มคะแนนพฤติกรรม {0} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);
                else _notification.message = string.Format("เสียใจด้วยค่ะ\nมีรายงานการตัดคะแนนพฤติกรรม {0} คะแนน\nเนื่องจาก {1}\nคะแนนพฤติกรรมคงเหลือ {2} คะแนน", tBehaviors.Score, tBehaviors.BehaviorName, ResidualScore);
                _notification.action = "vnd.jabjai.jabjaiapp://deeplink/behavior?message_id=" + message_id;
                _notification.user = dbmasteruser.sID.ToString();

                //dbmaster.TMessages.Add(new TMessage
                //{
                //    UserID = dbmasteruser.sID,
                //    sTitle = _notification.title,
                //    dSend = DateTime.Now,
                //    sMessage = _notification.message,
                //    nType = 1,
                //    nStatus = 0,
                //    nMessageID = message_id
                //});

                List<messagebox.user_messagebox> user_messagebox = new List<messagebox.user_messagebox>();
                user_messagebox.Add(new messagebox.user_messagebox { message_receive = DateTime.Now, user_id = dbmasteruser.sID, user_type = 0 });

                message_id = messagebox.insert_message(
                    new messagebox.MessageBox
                    {
                        message_type = messagebox.Behaviors,
                        school_id = qcomapny.nCompany,
                        user_messagebox = user_messagebox,
                        send_time = DateTime.Now,
                        message = _notification.message,
                        title = _notification.title
                    });

                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                          "ทำการตัดคะแนนความประพฤติ ",
                          HttpContext.Current.Session["sEntities"].ToString(),
                          HttpContext.Current.Request, 4, 2, 0);

                //await pushdata.push('"' + _notification.user + '"', _notification.message, _notification.title, DateTime.Now, message_id, qcomapny.nCompany);
                dbmaster.SaveChanges();
            }
        }

        internal class notification
        {
            public string action { get; internal set; }
            public string message { get; internal set; }
            public string title { get; internal set; }
            public string user { get; internal set; }
        }
    }
}