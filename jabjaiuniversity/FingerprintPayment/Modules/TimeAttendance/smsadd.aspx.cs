using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;
using urbanairship;
using System.Data.Entity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class smsadd : SMSGateway
    {
        public string setEditSMS;
        protected void Page_Load(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read))) {
                if (!IsPostBack)
                {
                    ddlType.Items.Add(new ListItem("ส่งทันที", "0"));
                    ddlType.Items.Add(new ListItem("ตั้งเวลาการส่ง", "1"));

                    ddlStype.Items.Add(new ListItem("สแกนเข้าโรงเรียน", "0"));
                    ddlStype.Items.Add(new ListItem("สแกนออกโรงเรียน", "1"));
                    ddlStype.Items.Add(new ListItem("เข้าโรงเรียนสาย", "2"));
                    ddlStype.Items.Add(new ListItem("ขาดเรียน", "3"));

                    ddlDuration.Items.Add(new ListItem("แจ้งประกาศกิจกรรม", "0"));
                    ddlDuration.Items.Add(new ListItem("แจ้งประกาศข่าวสาร", "1"));

                    if (!String.IsNullOrEmpty(Request.QueryString["type"].ToString()))
                    {
                        setSMSSection(Request.QueryString["type"].ToString());
                    }
                }
                btnCancel.Click += new EventHandler(btnCancel_Click);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("smssettings.aspx");
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            DateTime scheduled_time = DateTime.Now;
            string ListUser = "";
            Double _span = (DateTime.Now - DateTime.UtcNow).TotalMinutes;
            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               

                //var tUser = await _db.TUsers.Where(w => w.SchoolID == schoolID && w.cDel == null && (w.nStudentStatus == null || w.nStudentStatus == 0)).ToListAsync();

                //StudentLogic logic = new StudentLogic(_db);
                //string termId = logic.GetTermId(DateTime.Today, new JWTToken.userData
                //{
                //    CompanyID = schoolID
                //});                

                //var tTermSubLevel2 = await _db.TTermSubLevel2.Where(w => w.SchoolID == schoolID).ToListAsync();
                var tCompany = await _dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefaultAsync();

                if (ddlType.SelectedIndex == 1)
                {
                    scheduled_time = DateTime.ParseExact(dateSMS.Value + " " + monstop1.Value.ToString().Trim(), "dd/MM/yyyy HH:mm", new CultureInfo("en-us"));
                }

                if (requireData())
                {
                    int nSMS = 0;
                    TSM _TSMS = new TSM();
                    //if (_db.TSMS.Where(w => w.SchoolID == schoolID).Count() == 0)
                    //{
                    //    _TSMS.nSMS = 1; nSMS = 1;
                    //}
                    //else
                    //{
                    //    _TSMS.nSMS = _db.TSMS.Max(o => o.nSMS) + 1;
                    //    nSMS = _db.TSMS.Max(o => o.nSMS) + 1;
                    //}
                    _TSMS.SMSType = Int32.Parse(ddlType.SelectedValue);

                    if (ddlType.SelectedValue == "0")
                    {
                        _TSMS.SMSSubType = Int32.Parse(ddlStype.SelectedValue);
                        _TSMS.SMSDate = DateTime.Now;
                    }
                    else
                    {
                        _TSMS.SMSSubType = -1;
                        _TSMS.SMSDate = DateTime.ParseExact(dateSMS.Value, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }

                    _TSMS.nActionType = Int32.Parse(ddlAcceptType.SelectedValue);

                    _TSMS.SMSDuration = Int32.Parse(ddlDuration.SelectedValue);
                    _TSMS.SMSStatus = "-";
                    _TSMS.SMSTitle = ddlDuration.SelectedItem.Text;
                    _TSMS.SMSDesp = txtDesp.Text;
                    _TSMS.dSend = scheduled_time;
                    _TSMS.useradd = int.Parse(Session["sEmpID"] + "");

                    bool chkAll = ddlSendType.SelectedIndex == 2 || ddlSendType.SelectedIndex == 0;
                    bool chkEmp = ddlSendType.SelectedIndex == 1 || ddlSendType.SelectedIndex == 0;
                    if (chkAll)
                    {
                        _TSMS.SMSAll = "1";
                    }
                    else
                    {
                        _TSMS.SMSAll = "0";

                    }

                    //_TSMS.scheduled_id = scheduled_id;
                    _TSMS.SMSEMP = chkEmp ? "1" : "0";
                    _TSMS.SchoolID = schoolID;
                    _db.TSMS.Add(_TSMS);
                    _db.SaveChanges();

                    nSMS = _TSMS.nSMS;

                    if (fudFile.HasFile)
                    {
                        foreach (HttpPostedFile files in fudFile.PostedFiles)
                        {
                            string linkfiles = await AzureStorage.UploadFile(files, "news", tCompany.nCompany);
                            _db.TNewsFiles.Add(new TNewsFile
                            {
                                ContentType = files.ContentType,
                                nSMS = nSMS,
                                sFileName = linkfiles,
                                SchoolID = schoolID
                            });
                        }
                    }

                    var user_messagebox = new List<messagebox.user_messagebox>();
                    int nMessageID = 0;
                    if (optradio_0.Checked)
                    {
                        #region
                        if (!chkAll)
                        {
                            string sublv = txtLv.Value.Replace("first,", "");
                            string[] arrSublv = sublv.Split(',');
                            var sort = from s in arrSublv
                                       where !string.IsNullOrEmpty(s)
                                       orderby s
                                       select s;

                            //string termId = "";
                            StudentLogic logic = new StudentLogic(_db);
                            var termId_0 = logic.GetTermDATA(DateTime.Today, new JWTToken.userData
                            {
                                CompanyID = schoolID
                            });

                            var termId_1 = _db.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termId_0.dStart && f.SchoolID == schoolID && f.cDel == null);

                            var tUser = new List<TB_StudentViews>();

                            tUser.AddRange(_db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_0.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList());
                            if (tUser.Count == 0) tUser.AddRange(_db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_1.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList());

                            foreach (string c in sort)
                            {
                                TSMSSubLevel _TSMSSubLV = new TSMSSubLevel();
                                _TSMSSubLV.nSMS = nSMS;

                                _TSMSSubLV.nTSubLevel = Int32.Parse(c);
                                _TSMSSubLV.SchoolID = schoolID;
                                _db.TSMSSubLevels.Add(_TSMSSubLV);

                                var q_user = (from a in tUser
                                              where a.nTermSubLevel2 == _TSMSSubLV.nTSubLevel && a.nTerm == termId_0.nTerm
                                              select a).ToList();

                                if (q_user.Count == 0)
                                {
                                    q_user = (from a in tUser
                                              where a.nTermSubLevel2 == _TSMSSubLV.nTSubLevel && a.nTerm == termId_1.nTerm
                                              select a).ToList();
                                }

                                foreach (var data in q_user)
                                {
                                    var tUserMaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.nSystemID == data.sID && w.cType == "0" && w.cDel == null).ToList();
                                    foreach (var dataMaster in tUserMaster)
                                    {
                                        ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
                                        int user_type = int.Parse(dataMaster.cType);
                                        user_messagebox.Add(new messagebox.user_messagebox
                                        {
                                            user_id = dataMaster.sID,
                                            user_type = user_type
                                        });
                                    }
                                    await _dbMaster.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            StudentLogic logic = new StudentLogic(_db);
                            var termId_0 = logic.GetTermDATA(DateTime.Today, new JWTToken.userData
                            {
                                CompanyID = schoolID
                            });

                            var termId_1 = _db.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termId_0.dStart && f.SchoolID == schoolID && f.cDel == null);

                            var tUser = _db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_0.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().AsQueryable().ToList();
                            if (tUser.Count == 0) tUser = _db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_1.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().AsQueryable().ToList();

                            foreach (var dataMaster in _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && w.cDel == null).ToList())
                            {
                                int c = tUser.Where(w => w.sID == dataMaster.sID).Count();
                                if (c != 0)
                                {
                                    ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
                                    int user_type = int.Parse(dataMaster.cType);
                                    user_messagebox.Add(new messagebox.user_messagebox
                                    {
                                        user_id = dataMaster.sID,
                                        user_type = user_type
                                    });
                                }
                            }
                            await _dbMaster.SaveChangesAsync();
                        }
                        if (chkEmp)
                        {
                            foreach (var dataMaster in _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType != "0" && w.cDel == null).ToList())
                            {
                                ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
                                int user_type = int.Parse(dataMaster.cType);
                                user_messagebox.Add(new messagebox.user_messagebox
                                {
                                    user_id = dataMaster.sID,
                                    user_type = user_type
                                });
                            }
                            await _dbMaster.SaveChangesAsync();
                        }
                        #endregion
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtUserType0.Value.ToString()))
                        {
                            int[] intUserId = StringToInt(txtUserType0.Value.ToString().Split(','));
                            var listStudent = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0" && intUserId.Contains(w.nSystemID.Value) && w.cDel == null).ToList();

                            StudentLogic logic = new StudentLogic(_db);
                            var termId_0 = logic.GetTermDATA(DateTime.Today, new JWTToken.userData
                            {
                                CompanyID = schoolID
                            });

                            var termId_1 = _db.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termId_0.dStart && f.SchoolID == schoolID && f.cDel == null);

                            var tUser = _db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_0.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();
                            if (tUser.Count == 0) tUser = _db.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termId_1.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();

                            foreach (var dataMaster in listStudent)
                            {
                                int c = tUser.Where(w => w.sID == dataMaster.sID).Count();
                                if (c != 0)
                                {
                                    ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
                                    int user_type = int.Parse(dataMaster.cType);
                                    user_messagebox.Add(new messagebox.user_messagebox
                                    {
                                        user_id = dataMaster.sID,
                                        user_type = user_type
                                    });
                                }
                            }
                            await _dbMaster.SaveChangesAsync();
                        }
                        if (!string.IsNullOrEmpty(txtUserType1.Value.ToString()))
                        {
                            int[] intUserId = StringToInt(txtUserType1.Value.ToString().Split(','));
                            var lEmp = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType != "0" && intUserId.Contains(w.nSystemID.Value) && w.cDel == null).ToList();
                            foreach (var dataMaster in lEmp)
                            {
                                ListUser += (string.IsNullOrEmpty(ListUser) ? "" : ",") + '"' + dataMaster.sID + '"';
                                int user_type = int.Parse(dataMaster.cType);
                                user_messagebox.Add(new messagebox.user_messagebox
                                {
                                    user_id = dataMaster.sID,
                                    user_type = user_type
                                });
                            }
                            await _dbMaster.SaveChangesAsync();
                        }
                    }

                    nMessageID = messagebox.insert_message(
                          new messagebox.MessageBox
                          {
                              message_type = 5,
                              message_type_id = nSMS,
                              school_id = tCompany.nCompany,
                              user_messagebox = user_messagebox,
                              send_time = _TSMS.dSend
                          });

                    string scheduled_id = "";
                    if (_TSMS.SMSType == 1)
                    {
                        //scheduled_id = await pushdata.scheduled("[" + ListUser + "]", txtDesp.Text, ddlDuration.SelectedItem.Text + " : " + tCompany.sCompany, scheduled_time.AddMinutes(-_span), nMessageID, tCompany.nCompany);
                        //if (!string.IsNullOrEmpty(scheduled_id))
                        //{
                        //    scheduled_id = scheduled_id.Split('"')[3];
                        //}
                    }
                    else
                    {
                        //scheduled_id = await pushdata.push("[" + ListUser + "]", txtDesp.Text, ddlDuration.SelectedItem.Text + " : " + tCompany.sCompany, scheduled_time.AddMinutes(-_span), nMessageID, tCompany.nCompany);
                        //if (!string.IsNullOrEmpty(scheduled_id))
                        //{
                        //    scheduled_id = scheduled_id.Split('"')[3];
                        //}
                    }

                    _TSMS.scheduled_id = scheduled_id;

                    _db.SaveChanges();


                    database.InsertLog(UserData.UserID.ToString(), "เพิ่มข้อมูล " + _TSMS.SMSTitle, UserData.Entities, Request, 2, 2, 0);


                    // LINE Push Message
                    var q_userMaster = new List<MasterEntity.TUser>();
                    var q_userLINEs = new List<MasterEntity.LINEUser>();
                    q_userMaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany).ToList();
                    q_userLINEs = _dbMaster.LINEUsers.Where(w => w.SchoolID == tCompany.nCompany).ToList();
                    _dbMaster.Dispose();

                    //new Thread(() =>
                    //{
                    //    int nCompany = tCompany.nCompany;
                    //    var listFiles = _db.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == _TSMS.nSMS).ToList();
                    //    List<MappingLINEUser> listMappingLINEUser = new List<MappingLINEUser>();
                    //    foreach (var user in user_messagebox)
                    //    {
                    //        var userMaster = q_userMaster.FirstOrDefault(w => w.sID == user.user_id);
                    //        if (userMaster != null)
                    //        {
                    //            var userLINEs = q_userLINEs.Where(w => w.StudentID == user.user_id).ToList();
                    //            foreach (var userLINE in userLINEs)
                    //            {
                    //                listMappingLINEUser.Add(new MappingLINEUser { studentID = userMaster.nSystemID, lineID = userLINE.LINEUserID });
                    //            }
                    //        }
                    //    }

                    //    // Chunk mapping LINE user list
                    //    int maxGroupAmount = 150;
                    //    var chunks = GetChunks(listMappingLINEUser, maxGroupAmount);
                    //    int groupID = 1;
                    //    foreach (var chunk in chunks)
                    //    {
                    //        string LINEUserIDs = "";
                    //        string studentIDs = "";
                    //        foreach (var user in chunk)
                    //        {
                    //            LINEUserIDs += string.Format(@", ""{0}""", user.lineID);
                    //            studentIDs += string.Format(@",{0}", user.studentID);
                    //        }

                    //        if (!string.IsNullOrEmpty(LINEUserIDs))
                    //        {
                    //            LINEUserIDs = LINEUserIDs.Remove(0, 2);
                    //            studentIDs += ",";

                    //            _db.TMessageLINEMulticasts.Add(new TMessageLINEMulticast
                    //            {
                    //                MessageID = nMessageID,
                    //                GroupID = groupID,
                    //                StreamID = studentIDs,
                    //                UpdateDate = DateTime.Now,
                    //                SchoolID = schoolID
                    //            });
                    //            _db.SaveChanges();

                    //            LINESendMulticastMessage(nMessageID, _TSMS.SMSDesp, nCompany, groupID, _TSMS.nActionType, ddlAcceptType.SelectedItem.Text, listFiles, LINEUserIDs);

                    //            groupID++;
                    //        }
                    //    }
                    //}).Start();
                    //--

                    Response.Redirect("smssettings.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "<script>$(function(){j_infosell('<span>กรุณากรอกข้อมูลให้ครบถ้วน</span>');return false;});</script>", false);
                }
            }
        }

        private HttpStatusCode LINESendPushMessage(JabJaiEntities dbSchool, int messageID, int smsID, string message, int? schoolID, int? userID, string lineUserID, int? actionType, string actionTypeText, List<TNewsFile> listFiles)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.line.me/v2/bot/message/push"))
                {
                    string accessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];

                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");

                    // PostbackData : smsreply-[schoolID]-[message_id]-[user_id]-[replyValue]
                    string colorButton = "#F49453";
                    string messageTitle = "";
                    string messagePicture = "";
                    string messageFile = "";
                    string messageButton = "";
                    switch (actionType)
                    {
                        case 0:
                            messageTitle = "ข่าวสารจาก School Bright";
                            break;
                        case 1:
                        case 2:
                            messageTitle = "แบบตอบรับจาก School Bright";
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply-{1}-{2}-{3}-0"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{4}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeText, schoolID, messageID, userID, colorButton);
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            messageTitle = "แบบตอบรับจาก School Bright";

                            string[] actionTypeTexts = actionTypeText.Split('/');
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply-{2}-{3}-{4}-1"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }},
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{1}"",
                                        ""data"": ""smsreply-{2}-{3}-{4}-0"",
                                        ""text"": ""{1}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeTexts[0], actionTypeTexts[1], schoolID, messageID, userID, colorButton);
                            break;
                        default: break;
                    }

                    int rowIndex = 1;
                    int numberFile = 1;
                    //var listFiles = dbSchool.TNewsFiles.Where(w => w.nSMS == smsID).ToList();
                    if (listFiles.Count > 0)
                    {
                        foreach (var f in listFiles)
                        {
                            Regex regex = new Regex(@"(image|jpe?g|png|gif)");
                            Match match = regex.Match(f.ContentType);
                            if (match.Success)
                            {
                                // Is image
                                if (string.IsNullOrEmpty(messagePicture))
                                {
                                    messagePicture = string.Format(@"
                ""hero"": {{
                      ""type"": ""image"",
                      ""url"": ""{0}"",
                      ""size"": ""full"",
                      ""aspectRatio"": ""4:3"",
                      ""aspectMode"": ""fit"",
                      ""action"": {{
                        ""type"": ""uri"",
                        ""label"": ""Picture 1"",
                        ""uri"": ""{0}""
                      }}
                }},
", f.sFileName);
                                }
                            }
                            else
                            {
                                // Is document
                                messageFile += string.Format(@"
                                        {{
                                            ""type"": ""button"",
                                            ""action"": {{
                                                ""type"": ""uri"",
                                                ""label"": ""File {0}"",
                                                ""uri"": ""{1}""
                                            }}
                                        }}
", numberFile, f.sFileName);
                                if (rowIndex < listFiles.Count) messageFile += ", ";

                                numberFile++;
                            }

                            rowIndex++;
                        }

                        if (!string.IsNullOrEmpty(messageFile))
                        {
                            messageFile = string.Format(@",
                                {{
                                    ""type"": ""box"",
                                    ""layout"": ""vertical"",
                                    ""margin"": ""lg"",
                                    ""contents"": [
                                        {0}
                                    ]
                                }}
", messageFile);
                        }
                    }


                    // Replacing escape characters from JSON : Prepare message data
                    message = message.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\r\n", "\\n");

                    string stringContent = "";
                    switch (actionType)
                    {
                        case 0:
                            stringContent = string.Format(@"
{{
    ""to"": ""{0}"",
    ""messages"":[
        {{
            ""type"":""text"",
            ""text"": ""{1}\n{2}""
        }}
    ]
}}
", lineUserID, messageTitle, message);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            stringContent = string.Format(@"
{{
    ""to"": ""{0}"",
    ""messages"": [
        {{
            ""type"": ""flex"",
            ""altText"": ""{1}"",
            ""contents"": {{
                ""type"": ""bubble"",
                ""direction"": ""ltr"",{5}
                ""body"": {{
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""vertical"",
                            ""contents"": [
                                {{
                                    ""type"": ""text"",
                                    ""text"": ""{2}"",
                                    ""align"": ""start"",
                                    ""wrap"": true
                                }}{3}
                            ]
                        }}
                    ]
                }}{4}
            }}
        }}
    ]
}}", lineUserID, messageTitle, message, messageFile, messageButton, messagePicture);
                            break;
                        default: break;
                    }

                    request.Content = new StringContent(stringContent);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    response = httpClient.SendAsync(request).Result;
                }
            }

            return response.StatusCode;
        }

        public class MappingLINEUser
        {
            //nTSubLevel
            public int? studentID { get; set; }
            public string lineID { get; set; }
        }
        public IEnumerable<IEnumerable<T>> GetChunks<T>(IEnumerable<T> elements, int size)
        {
            var list = elements.ToList();
            while (list.Count > 0)
            {
                var chunk = list.Take(size);
                yield return chunk;

                list = list.Skip(size).ToList();
            }
        }
        private HttpStatusCode LINESendMulticastMessage(int messageID, string message, int schoolID, int groupID, int? actionType, string actionTypeText, List<TNewsFile> listFiles, string lineUserIDs)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.line.me/v2/bot/message/multicast"))
                {
                    string accessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];

                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");

                    // PostbackData : smsreply2-[schoolID]-[message_id]-[group_id]-[replyValue]
                    string colorButton = "#F49453";
                    string messageTitle = "";
                    string messagePicture = "";
                    string flexMessageFile = "";
                    string normalMessageFile = "";
                    string messageButton = "";
                    switch (actionType)
                    {
                        case 0:
                            messageTitle = "ข่าวสารจาก School Bright";
                            break;
                        case 1:
                        case 2:
                            messageTitle = "แบบตอบรับจาก School Bright";
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply2-{1}-{2}-{3}-0"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{4}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeText, schoolID, messageID, groupID, colorButton);
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            messageTitle = "แบบตอบรับจาก School Bright";

                            string[] actionTypeTexts = actionTypeText.Split('/');
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply2-{2}-{3}-{4}-1"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }},
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{1}"",
                                        ""data"": ""smsreply2-{2}-{3}-{4}-0"",
                                        ""text"": ""{1}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeTexts[0], actionTypeTexts[1], schoolID, messageID, groupID, colorButton);
                            break;
                        default: break;
                    }

                    int rowIndex = 1;
                    int numberFile = 1;
                    if (listFiles.Count > 0)
                    {
                        foreach (var f in listFiles)
                        {
                            Regex regex = new Regex(@"(image|jpe?g|png|gif)");
                            Match match = regex.Match(f.ContentType);
                            if (match.Success)
                            {
                                // Is image
                                if (string.IsNullOrEmpty(messagePicture))
                                {
                                    messagePicture = string.Format(@"
                ""hero"": {{
                      ""type"": ""image"",
                      ""url"": ""{0}"",
                      ""size"": ""full"",
                      ""aspectRatio"": ""4:3"",
                      ""aspectMode"": ""fit"",
                      ""action"": {{
                        ""type"": ""uri"",
                        ""label"": ""Picture 1"",
                        ""uri"": ""{0}""
                      }}
                }},
", f.sFileName);
                                }
                            }
                            else
                            {
                                // Is document
                                flexMessageFile += string.Format(@"
                                        {{
                                            ""type"": ""button"",
                                            ""action"": {{
                                                ""type"": ""uri"",
                                                ""label"": ""File {0}"",
                                                ""uri"": ""{1}""
                                            }}
                                        }}
", numberFile, f.sFileName);
                                if (rowIndex < listFiles.Count) flexMessageFile += ", ";

                                numberFile++;
                            }

                            // For attach file to normal message
                            normalMessageFile += ", " + f.sFileName;

                            rowIndex++;
                        }

                        if (!string.IsNullOrEmpty(flexMessageFile))
                        {
                            flexMessageFile = string.Format(@",
                                {{
                                    ""type"": ""box"",
                                    ""layout"": ""vertical"",
                                    ""margin"": ""lg"",
                                    ""contents"": [
                                        {0}
                                    ]
                                }}
", flexMessageFile);
                        }

                        if (!string.IsNullOrEmpty(normalMessageFile))
                        {
                            normalMessageFile = normalMessageFile.Remove(0, 2);
                        }
                    }


                    // Replacing escape characters from JSON : Prepare message data
                    message = message.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\r\n", "\\n");

                    // Max Length LINE Message : 5000
                    if (message.Length > 4800) message = message.Substring(0, 4800);

                    string stringContent = "";
                    switch (actionType)
                    {
                        case 0:
                            stringContent = string.Format(@"
{{
    ""to"": [{0}],
    ""messages"":[
        {{
            ""type"":""text"",
            ""text"": ""{1}\n{2}\n{3}""
        }}
    ]
}}
", lineUserIDs, messageTitle, message, normalMessageFile);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            stringContent = string.Format(@"
{{
    ""to"": [{0}],
    ""messages"": [
        {{
            ""type"": ""flex"",
            ""altText"": ""{1}"",
            ""contents"": {{
                ""type"": ""bubble"",
                ""direction"": ""ltr"",{5}
                ""body"": {{
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""vertical"",
                            ""contents"": [
                                {{
                                    ""type"": ""text"",
                                    ""text"": ""{2}"",
                                    ""align"": ""start"",
                                    ""wrap"": true
                                }}{3}
                            ]
                        }}
                    ]
                }}{4}
            }}
        }}
    ]
}}", lineUserIDs, messageTitle, message, flexMessageFile, messageButton, messagePicture);
                            break;
                        default: break;
                    }

                    request.Content = new StringContent(stringContent);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    response = httpClient.SendAsync(request).Result;
                }
            }

            return response.StatusCode;
        }

        bool requireData()
        {
            bool boolData = true;

            if (ddlType.SelectedValue != "0")
            {
                if (String.IsNullOrEmpty(dateSMS.Value))
                {
                    boolData = false;
                }
            }

            txtTitle.Text = ddlStype.SelectedItem.Text;
            if (optradio_0.Checked)
            {
                if (String.IsNullOrEmpty(txtDesp.Text))
                {
                    boolData = false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtUserType0.Value.ToString()) && string.IsNullOrEmpty(txtUserType1.Value.ToString())) boolData = false;
            }

            return boolData;
        }

        #region Check Type SMS
        private void setSMSSection(string types)
        {
            setEditSMS += "$('#subtype').show();";
            setEditSMS += "$('#datesection').hide();";
            setEditSMS += " $('#timesection').hide();";
        }
        #endregion

        private void Sendnotification()
        {

        }
        private int[] StringToInt(string[] StrArray)
        {
            int[] intId = new int[StrArray.Length];
            for (int i = 0; i < StrArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(StrArray[i]))
                {
                    intId[i] = int.Parse(StrArray[i]);
                }
                else
                {
                    intId[i] = 0;
                }
            }
            return intId;
        }
    }
}