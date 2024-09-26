using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using JabjaiMainClass;
using Ninject;
using OfficeOpenXml;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/language")]
    public class LanguageController : ApiController
    {
        public static void SetLanguage(string lang)
        {
            try
            {
                if (!LanguageHelper.IsLanguageAvailable(lang))
                    lang = LanguageHelper.GetDefaultLanguage();
                CultureInfo cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                //GregorianCalendar gregorianCalendar = new GregorianCalendar(GregorianCalendarTypes.USEnglish);
                //cultureInfo.DateTimeFormat.Calendar = (Calendar)gregorianCalendar;
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("culture", lang)
                {
                    Expires = DateTime.Now.AddYears(1)
                });

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("culturedir", "ltl")
                {
                    Expires = DateTime.Now.AddYears(1)
                });



            }
            catch //(Exception ex)
            {
            }
        }
    }
}
