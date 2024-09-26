using AutoMapper;
using FingerprintPayment.ViewModel;
using JabjaiEntity.DB;
using MasterEntity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Common
{
    public static class GradeServices
    {
        public static List<GradeViewModel> GetGradeData(string nTerm, int? nTermSubLevel2)
        {
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            var gradeSqlData = dbschool.TGradeTemps.Where(w => w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<List<TGradeTemp>, List<GradeViewModel>>());
            IMapper iMapper = configuration.CreateMapper();
            var gradeViewModel = iMapper.Map<List<TGradeTemp>, List<GradeViewModel>>(gradeSqlData);

            foreach (var g in gradeSqlData)
            {

            }
            return gradeViewModel;
        }
    }
}