using FingerprintPayment.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

//using SchoolBright.DataAccess;

namespace FingerprintPayment.Helper
{

    public class LanguageHelper
    {
        public static List<Languages> AvailableLanguages = new List<Languages>()
    {
      new Languages()
      {
        LanguageFullName = Resource.English,
        LanguageCultureName = "en",
        LanguageCultureNameDir = ""

      },
      new Languages()
      {
        LanguageFullName = Resource.Thai,
        LanguageCultureName = "th",
        LanguageCultureNameDir = ""
      }
    };

        public static bool IsLanguageAvailable(string lang) => LanguageHelper.AvailableLanguages.Where<Languages>((Func<Languages, bool>)(a => a.LanguageCultureName.Equals(lang))).FirstOrDefault<Languages>() != null;

        public static string GetDefaultLanguage() => LanguageHelper.AvailableLanguages[1].LanguageCultureName;

        public static string GetDefaultLanguageDir() => LanguageHelper.AvailableLanguages[1].LanguageCultureNameDir;



        public  void SetLanguage(string lang)
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


    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
        public string LanguageCultureNameDir
        {
            get;
            set;
        }
    }

}