using Jabjai.BusinessLogic;
using Jabjai.BusinessLogic.Master;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using Jabjai.Object.Entity.Peak;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using entityPeak = Jabjai.Object.Entity.Peak;

namespace FingerprintPayment.Handles.Utilities
{
    public class MasterDataHandle : IHttpHandler, IRequiresSessionState
    {
        JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string json = string.Empty;
            int schoolId = userData.CompanyID;   //#HardcodeSchoolId

            if (!string.IsNullOrEmpty(context.Request.QueryString["entity"]))
            {
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                switch (context.Request.QueryString["entity"])
                {
                    case "accounting":
                        var q_Accounting = this.GetPeakDataJSON<entityPeak.Accounting>();
                        q_Accounting.Result.RemoveAll(re => re.IsDelete == true);
                        json = new JavaScriptSerializer().Serialize(q_Accounting);
                        break;
                    case "tlevel":
                        BaseResult<List<TSubLevel>> _resultSubLevel = this.GetSubLevel();
                        _resultSubLevel.Result = _resultSubLevel.Result.Where(w => w.SchoolID == userData.CompanyID).ToList();
                        json = new JavaScriptSerializer().Serialize(_resultSubLevel);
                        break;
                    case "bank":
                        //using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities(ConnectionDB.Read))
                        //{
                        //    var q = peakengineEntities.Banks.Select(s => new entityPeak.Bank { BankId = s.BankId, BankName = s.BankName, IsDelete = s.IsDelete, IsActive = s.IsActive }).ToList();
                        //    var q1 = this.GetPeakDataJSON<entityPeak.Bank>();
                        //    q1.Result = q.Where(w => w.IsActive == true && w.IsDelete == false).OrderBy(o => o.BankName).ToList();
                        //    json = new JavaScriptSerializer().Serialize(q1);
                        //}
                        var q = this.GetPeakDataJSON<entityPeak.Bank>();
                        q.Result.RemoveAll(re => re.IsDelete == true);
                        json = new JavaScriptSerializer().Serialize(q);
                        break;
                    case "tsublevel":
                        int levelId = Convert.ToInt32(context.Request.QueryString["id"]);
                        BaseResult<List<TTermSubLevel2>> _resultLevel = this.GetSubLevel2(levelId);
                        _resultLevel.Result = _resultLevel.Result.Where(w => w.SchoolID == userData.CompanyID).ToList();
                        json = new JavaScriptSerializer().Serialize(_resultLevel);
                        break;
                    case "year":
                        BaseResult<List<TYear>> _resultYear = this.GetDataJSON<TYear>();
                        _resultYear.Result = _resultYear.Result.Where(w => w.SchoolID == userData.CompanyID).OrderByDescending(o => o.Number).ToList();
                        json = new JavaScriptSerializer().Serialize(_resultYear);
                        break;
                    case "term":
                        BaseResult<List<TTerm>> result = this.GetTerm();
                        result.Result = result.Result.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).OrderByDescending(o => o.sTerm).OrderByDescending(o => o.Year.Number).ToList();
                        json = new JavaScriptSerializer().Serialize(result);
                        break;
                    case "paymentmethod":
                        json = new JavaScriptSerializer().Serialize(this.GetPaymentMethod(schoolId));
                        break;
                }
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }

        private BaseResult<List<TEntity>> GetDataJSON<TEntity>() where TEntity : MasterDataObject
        {
            MasterDataLogic<TEntity> masterLogic = new MasterDataLogic<TEntity>();
            return masterLogic.GetAll();
        }

        private BaseResult<List<TEntity>> GetPeakDataJSON<TEntity>() where TEntity : MasterDataObject
        {
            PeakMasterDataLogic<TEntity> masterLogic = new PeakMasterDataLogic<TEntity>();
            return masterLogic.GetAll();
        }

        private BaseResult<List<TTerm>> GetTerm()
        {
            TermLogic logic = new TermLogic();
            return logic.GetAll();
        }

        private BaseResult<List<TSubLevel>> GetSubLevel()
        {
            SubLevelLogic logic = new SubLevelLogic();
            return logic.GetAll();
        }

        private BaseResult<List<TTermSubLevel2>> GetSubLevel2(int sublevelId)
        {
            var logic = new SubLevel2Logic();
            var _result = logic.GetBySubLevel2Id(sublevelId);

            _result.Result.Where(w => w.SchoolID == userData.CompanyID).ToList().ForEach(f =>
                  {
                      int TSubLevel2 = -1;
                      int.TryParse(f.TSubLevel2, out TSubLevel2);
                      if (TSubLevel2 != -1) f.Order = string.Format("{0:000000}", TSubLevel2);
                      else f.Order = f.TSubLevel2;
                  });

            _result.Result = _result.Result.OrderBy(o => o.Order).ToList();

            return _result;
        }

        private BaseResult<List<PaymentMethodDTO>> GetPaymentMethod(int schoolId)
        {
            PaymentMethodLogic logic = new PaymentMethodLogic();

            return logic.GetBySchoolId(schoolId);
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