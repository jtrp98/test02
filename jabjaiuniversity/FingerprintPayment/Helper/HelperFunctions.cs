using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace FingerprintPayment.Helper
{
    public static class HelperFunctions
    {
        //public static bool GetResolvedConnecionIpAddress(string serverNameOrUrl, out string resolvedIpAddress)
        //{
        //    var isResolved = false;
        //    IPAddress resolvIp = null;
        //    try
        //    {
        //        if (!IPAddress.TryParse(serverNameOrUrl, out resolvIp))
        //        {
        //            var hostEntry = Dns.GetHostEntry(serverNameOrUrl);

        //            if (hostEntry != null && hostEntry.AddressList != null
        //                && hostEntry.AddressList.Length > 0)
        //            {
        //                if (hostEntry.AddressList.Length == 1)
        //                {
        //                    resolvIp = hostEntry.AddressList[0];
        //                    isResolved = true;
        //                }
        //                else
        //                {
        //                    foreach (var var in hostEntry.AddressList.Where(var => var.AddressFamily == AddressFamily.InterNetwork))
        //                    {
        //                        resolvIp = var;
        //                        isResolved = true;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            isResolved = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        isResolved = false;
        //        resolvIp = null;
        //    }
        //    finally
        //    {
        //        if (resolvIp != null) resolvedIpAddress = resolvIp.ToString();
        //    }

        //    resolvedIpAddress = null;
        //    return isResolved;
        //}

        public static string SerializeObject<T>(T source)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                serializer.Serialize(writer, source);
                return sw.ToString();
            }
        }

        public static T DeSerializeObject<T>(string xml)
        {
            using (var sr = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        public static object ReturnZeroIfNull(this object value)
        {
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }

        public static object ReturnZeroIfNullDouble(this object value)
        {
            if (value == DBNull.Value)
                return 0.0;
            if (value == null)
                return 0.0;
            return value;
        }

        public static object ReturnEmptyIfNull(this object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        public static object ReturnFalseIfNull(this object value)
        {
            if (value == DBNull.Value)
                return false;
            if (value == null)
                return false;
            return value;
        }

        public static object ReturnDateTimeMinIfNull(this object value)
        {
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public static object ReturnDateTimeSpan(this object value)
        {
            if (value == DBNull.Value)
                return TimeSpan.MinValue;
            if (value == null)
                return TimeSpan.MinValue;
            return value;
        }

        public static object ReturnNullIfDbNull(this object value)
        {
            if (value == DBNull.Value)
                return '\0';
            if (value == null)
                return '\0';
            return value;
        }

        //This function formats the display-name of a user,
        //and removes unnecessary extra information.
        public static string FormatUserDisplayName(string displayName = null, string defaultValue = "tBill Users",
            bool returnNameIfExists = false, bool returnAddressPartIfExists = false)
        {
            //Get the first part of the Users's Display Name if s/he has a name like this: "firstname lastname (extra text)"
            //removes the "(extra text)" part
            if (!string.IsNullOrEmpty(displayName))
            {
                if (returnNameIfExists)
                    return Regex.Replace(displayName, @"\ \(\w{1,}\)", "");
                return (displayName.Split(' '))[0];
            }
            if (returnAddressPartIfExists)
            {
                var emailParts = defaultValue.Split('@');
                return emailParts[0];
            }
            return defaultValue;
        }

        public static string FormatUserTelephoneNumber(this string telephoneNumber)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(telephoneNumber))
            {
                //result = telephoneNumber.ToLower().Trim().Trim('+').Replace("tel:", "");
                result = telephoneNumber.ToLower().Trim().Replace("tel:", "");

                if (result.Contains(";"))
                {
                    if (!result.ToLower().Contains(";ext="))
                        result = result.Split(';')[0];
                }
            }

            return result;
        }

        public static bool IsValidEmail(this string emailAddress)
        {
            const string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(emailAddress, pattern);
        }

        /// <summary>
        /// Convert DateTime to string
        /// </summary>
        /// <param name="datetTime"></param>
        /// <param name="excludeHoursAndMinutes">if true it will execlude time from datetime string. Default is false</param>
        /// <returns></returns>
        public static string ConvertDate(this DateTime datetTime, bool excludeHoursAndMinutes = false)
        {
            if (datetTime != DateTime.MinValue)
            {
                if (excludeHoursAndMinutes)
                    return datetTime.ToString("yyyy-MM-dd");
                return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return null;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public static string ConvertSecondsToReadable(this int secondsParam)
        {
            var hours = Convert.ToInt32(Math.Floor((double)(secondsParam / 3600)));
            var minutes = Convert.ToInt32(Math.Floor((double)(secondsParam - (hours * 3600)) / 60));
            var seconds = secondsParam - (hours * 3600) - (minutes * 60);

            var hoursStr = hours.ToString();
            var minsStr = minutes.ToString();
            var secsStr = seconds.ToString();

            if (hours < 10)
            {
                hoursStr = "0" + hoursStr;
            }

            if (minutes < 10)
            {
                minsStr = "0" + minsStr;
            }
            if (seconds < 10)
            {
                secsStr = "0" + secsStr;
            }

            return hoursStr + ':' + minsStr + ':' + secsStr;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public static string ConvertSecondsToReadable(this long secondsParam)
        {
            var hours = Convert.ToInt32(Math.Floor((double)(secondsParam / 3600)));
            var minutes = Convert.ToInt32(Math.Floor((double)(secondsParam - (hours * 3600)) / 60));
            var seconds = Convert.ToInt32(secondsParam - (hours * 3600) - (minutes * 60));

            var hoursStr = hours.ToString();
            var minsStr = minutes.ToString();
            var secsStr = seconds.ToString();

            if (hours < 10)
            {
                hoursStr = "0" + hoursStr;
            }

            if (minutes < 10)
            {
                minsStr = "0" + minsStr;
            }
            if (seconds < 10)
            {
                secsStr = "0" + secsStr;
            }

            return hoursStr + ':' + minsStr + ':' + secsStr;
        }

        //public static string ConvertToDateString(object date)
        //{
        //    if (date == null)
        //        return string.Empty;

        //    return SpecialDateTime.ConvertDate(Convert.ToDateTime(date));
        //}

        public static string ConvertToString(object value)
        {
            return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
        }

        public static int ConvertToInt(object value)
        {
            return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static long ConvertToLong(object value)
        {
            return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
        }

        public static DateTime convertToDateTime(object date)
        {
            return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
        }

        public static DateTime? StringToDateTime(string stringDate)
        {
            DateTime? resultDate = null;

            try
            {
                // 17/11/2526 23:59:59
                // 17-พ.ย.-2526
                // 17 พ.ย. 2526
                // 17 พ.ย. 26

                stringDate = stringDate.Trim();
                string[] adt = stringDate.Split(' ');
                string date = adt[0];
                string time = adt.Length > 1 ? adt[1] : "";

                string[] dmy = null;
                if (date.Split('/').Length == 3)
                {
                    dmy = date.Split('/');
                }
                else if (date.Split('-').Length == 3)
                {
                    dmy = date.Split('-');
                }
                else if (date.Split(' ').Length == 1)
                {
                    dmy = new string[3] { adt[0], adt[1], adt[2] };
                    time = (adt.Length > 3 ? adt[3] : "") + (adt.Length > 4 ? " " + adt[4] : "");
                }

                string cultureString = "en-US";
                if (dmy != null)
                {
                    int y = Convert.ToInt32(dmy[2]);

                    // 2393 = 1850 + 543
                    if (y > 2393 || y < 100)
                    {
                        cultureString = "th-TH";
                    }

                    stringDate = string.Join("/", dmy) + (!string.IsNullOrEmpty(time) ? " " + time : "");

                    CultureInfo cultureInfo = new CultureInfo(cultureString);
                    string[] formatStringDate = new[] {
                    "d/MM/yy", "d/MMM/yy", "d/MM/yyyy", "d/MMM/yyyy",
                    "dd/MM/yy", "dd/MMM/yy", "dd/MM/yyyy", "dd/MMM/yyyy",
                    "d/M/yyyy", "dd/M/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yyyy",

                    "d/MM/yy HH:mm:ss", "d/MMM/yy HH:mm:ss", "d/MM/yyyy HH:mm:ss", "d/MMM/yyyy HH:mm:ss",
                    "dd/MM/yy HH:mm:ss", "dd/MMM/yy HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "dd/MMM/yyyy HH:mm:ss",
                    "d/M/yyyy HH:mm:ss", "dd/M/yyyy HH:mm:ss", "MM/dd/yyyy HH:mm:ss", "M/dd/yyyy HH:mm:ss", "M/d/yyyy HH:mm:ss",

                    "d/MM/yy hh:mm:ss tt", "d/MMM/yy hh:mm:ss tt", "d/MM/yyyy hh:mm:ss tt", "d/MMM/yyyy hh:mm:ss tt",
                    "dd/MM/yy hh:mm:ss tt", "dd/MMM/yy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "dd/MMM/yyyy hh:mm:ss tt",
                    "d/M/yyyy hh:mm:ss tt", "dd/M/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt", "M/dd/yyyy hh:mm:ss", "M/d/yyyy hh:mm:ss tt" };

                    foreach (var formatDate in formatStringDate)
                    {
                        if (DateTime.TryParseExact(stringDate, formatDate, cultureInfo, DateTimeStyles.None, out DateTime outDate))
                        {
                            resultDate = outDate;
                            break;
                        }
                    }
                }
            }
            catch { }

            return resultDate;
        }
    }
}