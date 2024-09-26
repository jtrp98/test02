using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline04 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"SELECT nTitleid, titleDescription FROM TTitleList WHERE SchoolID = {0} AND deleted <> '1' AND workStatus <> 'notworking'", schoolID);
                List<EntityRegisterStudentTitle> result = en.Database.SqlQuery<EntityRegisterStudentTitle>(sqlQuery).ToList();

                if (result.Count > 0)
                {
                    foreach (var r in result)
                    {
                        this.ltrStudentTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                    }
                }

                // Nation
                var listNation = en.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                foreach (var l in listNation)
                {
                    this.ltrStudentNation.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }

                // Religion
                var listReligion = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                foreach (var l in listReligion)
                {
                    this.ltrStudentReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }

                // Race
                var listRace = en.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                foreach (var l in listRace)
                {
                    this.ltrStudentRace.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }


                //Script Required Field
                RegisterOnline registerOnline = (RegisterOnline)this.Master;
                List<string> listElement = registerOnline.GetElementRequiredField(schoolID, 1);

                string script = "";
                foreach (var e in listElement)
                {
                    switch (e)
                    {
                        case "iptStudentCategory":
                            script += string.Format(@"AddRequiredRulesvalidation('input[name={0}]');", e);
                            break;
                        default:
                            script += string.Format(@"AddRequiredRulesvalidation('#{0}');", e);
                            break;
                    }

                }

                ltrScriptRequiredField.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0}
        }});
    </script>", script);
            }

        }

        [WebMethod(EnableSession = true)]
        public static object UploadFileBase64ToSession(DocumentFile documentFile)
        {
            bool success = true;
            string message = "Save Successfully";

            DocumentFile documentFileSession = null;

            try
            {
                // Prepare object on session
                List<DocumentFile> DocumentFiles = new List<DocumentFile>();
                if (HttpContext.Current.Session["RegisterOnlineFileBase64"] != null)
                {
                    DocumentFiles = (List<DocumentFile>)HttpContext.Current.Session["RegisterOnlineFileBase64"];
                }

                documentFileSession = DocumentFiles.Where(w => w.DocID == documentFile.DocID && w.TypeID == documentFile.TypeID).FirstOrDefault();
                if (documentFileSession == null)
                {
                    documentFileSession = new DocumentFile
                    {
                        ID = documentFile.ID,
                        ContentType = documentFile.ContentType,
                        FileName = documentFile.FileName,
                        DocID = documentFile.DocID,
                        TypeID = documentFile.TypeID,
                        VFIID = documentFile.VFIID,
                        ByteData = documentFile.ByteData
                    };
                    DocumentFiles.Add(documentFileSession);
                }
                else
                {
                    documentFileSession.ContentType = documentFile.ContentType;
                    documentFileSession.FileName = documentFile.FileName;
                    documentFileSession.ByteData = documentFile.ByteData;
                }

                // Save to session
                HttpContext.Current.Session["RegisterOnlineFileBase64"] = DocumentFiles;
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, documentFile = documentFileSession };
            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object RetreivedFileBase64FromSession(int docId, int typeId)
        {
            bool success = true;
            string message = "Save Successfully";

            DocumentFile documentFileSession = null;

            try
            {
                // Prepare object on session
                List<DocumentFile> DocumentFiles = new List<DocumentFile>();
                if (HttpContext.Current.Session["RegisterOnlineFileBase64"] != null)
                {
                    DocumentFiles = (List<DocumentFile>)HttpContext.Current.Session["RegisterOnlineFileBase64"];
                }

                documentFileSession = DocumentFiles.Where(w => w.DocID == docId && w.TypeID == typeId).FirstOrDefault();
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, documentFile = documentFileSession };
            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object ListFileBase64FromSession()
        {
            bool success = true;
            string message = "Save Successfully";

            List<DocumentFile> DocumentFiles = null;

            try
            {
                // Prepare object on session
                if (HttpContext.Current.Session["RegisterOnlineFileBase64"] != null)
                {
                    DocumentFiles = (List<DocumentFile>)HttpContext.Current.Session["RegisterOnlineFileBase64"];
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, documentFiles = DocumentFiles };
            return JsonConvert.SerializeObject(result);
        }

    }
}