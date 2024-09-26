using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    public static class GlobalVariable
    {
        // Employee Module
        public const string EMPLOYEE_INFO_TMEP_FOLDER = @"~/upload/Employee/tmp/{0}/Info/"; // ~/upload/Employee/tmp/[SessionID]/Info/
        public const string EMPLOYEE_INFO_TMEP_FILE = @"~/upload/Employee/tmp/{0}/Info/{1}"; // ~/upload/Employee/tmp/[SessionID]/Info/[FileName]
        public const string EMPLOYEE_INFO_FOLDER = @"~/upload/Employee/{0}/Info/"; // ~/upload/Employee/[EmpID]/Info/
        public const string EMPLOYEE_INFO_FILE = @"~/upload/Employee/{0}/Info/{1}"; // ~/upload/Employee/[EmpID]/Info/[FileName]

        public const string EMPLOYEE_HONOR_TMEP_FOLDER = @"~/upload/Employee/tmp/{0}/Honor/"; // ~/upload/Employee/tmp/[SessionID]/Honor/
        public const string EMPLOYEE_HONOR_TMEP_FILE = @"~/upload/Employee/tmp/{0}/Honor/{1}"; // ~/upload/Employee/tmp/[SessionID]/Honor/[FileName]
        public const string EMPLOYEE_HONOR_FOLDER = @"~/upload/Employee/{0}/Honor/"; // ~/upload/Employee/[EmpID]/Honor/
        public const string EMPLOYEE_HONOR_FILE = @"~/upload/Employee/{0}/Honor/{1}"; // ~/upload/Employee/[EmpID]/Honor/[HonorID]_[FileName]

        public const string VISITHOUSE_TMEP_FOLDER = @"~/upload/VisitHouse/tmp/{0}/"; // ~/upload/VisitHouse/tmp/[SessionID]/
        public const string VISITHOUSE_TMEP_OUT_FILE = @"~/upload/VisitHouse/tmp/{0}/OUT_{1}"; // ~/upload/VisitHouse/tmp/[SessionID]/OUT_[FileName]
        public const string VISITHOUSE_TMEP_IN_FILE = @"~/upload/VisitHouse/tmp/{0}/IN_{1}"; // ~/upload/VisitHouse/tmp/[SessionID]/OUT_[FileName]
        public const string VISITHOUSE_FOLDER = @"~/upload/VisitHouse/{0}/{1}/"; // ~/upload/VisitHouse/[YearID]/[ID]/
        public const string VISITHOUSE_OUT_FILE = @"~/upload/VisitHouse/{0}/{1}/OUT_{2}"; // ~/upload/VisitHouse/[YearID]/[ID]/OUT_[FileName]
        public const string VISITHOUSE_IN_FILE = @"~/upload/VisitHouse/{0}/{1}/IN_{2}"; // ~/upload/VisitHouse/[YearID]/[ID]/IN_[FileName]

    }
}