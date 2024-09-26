using Jabjai.BusinessLogic;
using Jabjai.BusinessLogic.Master;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using JabjaiEntity.DB;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoiceCreationHandler
    /// </summary>
    public class InvoiceCreationHandler : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var jsonString = context.Request.Form["invoice"];
            int schoolId = 3;   //#HardcodeSchoolId
            string userName = context.Session["Emp_Name"].ToString();   //#HardcodeuserName
            BaseResult<bool> result = new BaseResult<bool>();

            try
            {
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                var invoice = JsonConvert.DeserializeObject<InvoiceDTO>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                invoice.SchoolId = schoolId;
                invoice.CreateBy = userName;
                invoice.UpdateBy = userName;
                InvoiceLogic logic = new InvoiceLogic();
                UserLogic userLogic = new UserLogic();
                var termLogic = new MasterDataLogic<Jabjai.Object.Entity.Jabjai.TTerm>();
                var subLevelLogic = new MasterDataLogic<Jabjai.Object.Entity.Jabjai.TSubLevel>();
                var temSubLevel2Logic = new MasterDataLogic<Jabjai.Object.Entity.Jabjai.TTermSubLevel2>();

                var invoiceDTO = logic.GetById(invoice.InvoiceId).Result;
                if (invoiceDTO != null && invoiceDTO.preRegisterId.HasValue)
                {
                    if (invoice.InvoiceId == 0)
                    {
                        invoice.CreateBy = userData.UserID.ToString();
                        invoice.CreateDate = DateTime.Now;
                        invoice.InvoiceItems.ForEach(f =>
                        {
                            f.CreateDate = DateTime.Now;
                            f.CreateBy = userData.UserID.ToString();
                        });

                        result = logic.Create(invoice);
                    }
                    else
                    {
                        invoice.CreateBy = userData.UserID.ToString();
                        invoice.CreateDate = DateTime.Now;
                        invoice.InvoiceItems.ForEach(f =>
                        {
                            f.CreateDate = DateTime.Now;
                            f.CreateBy = userData.UserID.ToString();
                        });

                        if (invoice.InvoiceStatus.ToLower() == "delete")
                        {
                            if (invoice.InvoiceId != 0)
                            {
                                PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read);
                                var f1 = entities.TInvoices.FirstOrDefault(f => f.invoices_Id == invoice.InvoiceId);
                                f1.UpdatedDate = DateTime.Now;
                                f1.isDel = true;
                                f1.UpdatedBy = userData.UserID;
                                entities.SaveChanges();
                            }
                        }
                        else
                        {
                            result = logic.Update(invoice);
                        }

                    }
                }
                else
                {
                    var resultUser = userLogic.GetById(invoice.StudentId);

                    invoice.UpdateBy = userData.UserID.ToString();
                    invoice.UpdateDate = DateTime.Now;

                    if (resultUser.Result == null)
                    {
                        result.Result = false;
                        result.Message = "ไม่พบข้อมูลนักเรียน";
                    }
                    else if (invoice.InvoiceStatus.ToLower() == "delete")
                    {
                        if (invoice.InvoiceId != 0)
                        {
                            PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read);
                            var f1 = entities.TInvoices.FirstOrDefault(f => f.invoices_Id == invoice.InvoiceId);
                            f1.UpdatedDate = DateTime.Now;
                            f1.isDel = true;
                            f1.UpdatedBy = userData.UserID;
                            entities.SaveChanges();
                        }
                    }
                    else if (invoiceDTO != null && invoice.ManualDiscount > invoiceDTO.InvoiceItems.Where(w => w.IsDelete == false).Sum(s => s.OutstandingAmount ?? 0))
                    {
                        result.Result = false;
                        result.Message = "ไม่สามารถใส่ส่วนลดมากกว่าค่าบริการได้";
                    }
                    else if (invoiceDTO == null && invoice.ManualDiscount > invoice.InvoiceItems.Sum(s => s.ProductAmount ?? 0))
                    {
                        result.Result = false;
                        result.Message = "ไม่สามารถใส่ส่วนลดมากกว่าค่าบริการได้";
                    }
                    else
                    {
                        var resultTerm = termLogic.GetById(invoice.TermId);
                        //var resultTermSubLv2 = temSubLevel2Logic.GetById(resultUser.Result.TermSubLevel2Id);
                        using (JabJaiEntities entity = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                        {
                            var studentViews = entity.TB_StudentViews.Where(f => f.sID == invoice.StudentId && f.nTerm == invoice.TermId).AsQueryable().FirstOrDefault();
                            invoice.Term = resultTerm.Result.sTerm;
                            invoice.TermYear = resultTerm.Result.Year.Number.ToString();
                            invoice.StudentName = resultUser.Result.Name + " " + resultUser.Result.LastName;
                            //invoice.Level = resultTermSubLv2.Result.TSubLevel.TLevel.Name;
                            //invoice.SubLevel = resultTermSubLv2.Result.TSubLevel.SubLevel;
                            //invoice.SubLevel2 = resultTermSubLv2.Result.TSubLevel2;

                            if (studentViews == null && !invoice.Fd_NewTermClass_id.HasValue && invoice.InvoiceId == 0)
                            {
                                result.Result = false;
                                result.Message = "ไม่สามารถทำการสร้างใบแจ้งหนี้ได้ เนื่องจากไม่พบข้อมูลนักเรียนในชั้นปีที่เลือก สำหรับปีการศึกษา " + resultTerm.Result.Year.Number;
                            }
                            else
                            {
                                if (studentViews == null)
                                {
                                    studentViews = entity.TB_StudentViews.Where(f => f.sID == invoice.StudentId).OrderByDescending(o => o.dStart).Take(1).AsQueryable().FirstOrDefault();
                                }

                                var f_subLevel = entity.TSubLevels.Where(f => f.nTSubLevel == studentViews.nTSubLevel).AsQueryable().FirstOrDefault();
                                var f_level = entity.TLevels.Where(f => f.LevelID == f_subLevel.nTLevel).AsQueryable().FirstOrDefault();
                                invoice.Level = f_level.LevelName;
                                invoice.SubLevel = studentViews.SubLevel;
                                invoice.SubLevel2 = studentViews.nTSubLevel2;

                                invoice.Fd_AppActive = true;

                                if (invoice.Fd_NewTermClass_id.HasValue || invoice.Fd_NewTermClass_id.HasValue)
                                {

                                    if (invoice.Fd_NewTermClass_id.HasValue)
                                    {
                                        var f_class = entity.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == invoice.Fd_NewTermClass_id);
                                        invoice.Fd_NewTermSubLevel2 = f_class.nTSubLevel2;
                                    }

                                    if (invoice.Fd_NewTermLevel_id.HasValue)
                                    {
                                        var f_class = entity.TSubLevels.FirstOrDefault(f => f.nTSubLevel == invoice.Fd_NewTermLevel_id);
                                        invoice.Fd_NewTermSubLevel = f_class.SubLevel;
                                    }
                                }

                                if (invoice.InvoiceId == 0)
                                    result = logic.Create(invoice);
                                else
                                    result = logic.Update(invoice);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Result = false;
                result.StatusCode = "500";
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}