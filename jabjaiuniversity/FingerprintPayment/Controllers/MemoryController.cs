using FingerprintPayment.Helper;
using JabjaiMainClass;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Linq;
using JabjaiMainClass.Autocompletes;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/memory")]
    public class MemoryController : ApiController
    {

        [HttpGet]
        [Route("refreshmemory")]
        public IHttpActionResult RefreshMemory()
        {
            try
            {
                Global.update_Autocompletes();
                dynamic rss = new JObject();
                rss.status = "Process Done";
                rss.data = new JObject();
                return Json(rss);
            }
            catch (Exception ex)
            {
                dynamic rss = new JObject();
                rss.status = ex.Message;
                rss.data = new JObject();
                return Json(rss);
            }
        }
        [HttpGet]
        [Route("getmemorydatabyschool/{SchoolID}")]
        public IHttpActionResult GetMemoryDataBySchool(int SchoolID)
        {
            try
            {

                dynamic rss = new JObject();

                if (TopupMoney.topupMoney.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.SchoolID).ThenBy(x => x.KEYWORD_ORDER).ToList().Count > 0)
                    rss.data = JsonConvert.SerializeObject(TopupMoney.topupMoney.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.SchoolID).ThenBy(x => x.User_Id).ToList());
                else
                    rss.data = new JObject();

                rss.status = "Success";
                return Json(rss);

            }
            catch (Exception ex)
            {
                dynamic rss = new JObject();
                rss.status = ex.Message;
                rss.data = new JObject();
                return Json(rss);
            }
        }
        [HttpGet]
        [Route("getmemorybyschool/{SchoolID}")]
        public IHttpActionResult GetMemoryBySchool(int SchoolID)
        {
            try
            {
                dynamic rss = new JObject();

                if (TopupMoney.topupMoney.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.SchoolID).ThenBy(x => x.KEYWORD_ORDER).ToList().Count > 0)
                    rss.data = JsonConvert.SerializeObject(TopupMoney.topupMoney.Where(x => x.SchoolID == SchoolID).OrderBy(x => x.SchoolID).ThenBy(x => x.User_Id).ToList());
                else
                    rss.data = new JObject();

                rss.status = "Success";
                return Json(rss);
            }
            catch (Exception ex)
            {
                dynamic rss = new JObject();
                rss.status = ex.Message;
                rss.data = new JObject();
                return Json(rss);
            }
        }
        [HttpGet]
        [Route("getmemorydata")]
        public IHttpActionResult GetMemoryData()
        {
            try
            {

                dynamic rss = new JObject();


                if (TopupMoney.topupMoney.OrderBy(x => x.SchoolID).ThenBy(x => x.KEYWORD_ORDER).ToList().Count > 0)
                    rss.data = JsonConvert.SerializeObject(TopupMoney.topupMoney.OrderBy(x => x.SchoolID).ThenBy(x => x.User_Id).ToList());
                else
                    rss.data = new JObject();


                rss.status = "Success";
                return Json(rss);
            }
            catch (Exception ex)
            {
                dynamic rss = new JObject();
                rss.status = ex.Message;
                rss.data = new JObject();
                return Json(rss);
            }
        }

    }
}
