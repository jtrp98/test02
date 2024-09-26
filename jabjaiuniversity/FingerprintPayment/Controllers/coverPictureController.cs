using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Controllers
{
    public class coverPictureController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        public string Getcover([FromUri]int userid)
        {

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {

                var quser = _dbMaster.TUsers.Where(w => w.sID == userid).FirstOrDefault();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.nCompany == quser.nCompany);



                var findpic = _dbMaster.TCompanies.Where(w => w.nCompany == qcompany.nCompany).FirstOrDefault();

                string result = "";
                if (findpic.schoolCoverPicture == null || findpic.schoolCoverPicture == "")
                    result = "FAIL";
                else
                {
                    result = findpic.schoolCoverPicture;
                }



                return result;
            }
        }

        

    }
}
