using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace FingerprintPayment.Class
{
    public class UpdateMemory
    {

        string AuthKey = "";
        string AuthValue = "";
        public UpdateMemory(string _AuthKey, string _AuthValue)
        {
            AuthKey = _AuthKey;
            AuthValue = _AuthValue;
        }

        public void Student(JabjaiEntity.DB.TUser SchoolTUser, MasterEntity.TUser MasterTUser)
        {
            StudentData data = new StudentData();
            data.SchoolTUser = new JabjaiEntity.DB.TUser
            {
                sID = SchoolTUser.sID,
                sStudentTitle = SchoolTUser.sStudentTitle,
                sName = SchoolTUser.sName,
                sLastname = SchoolTUser.sLastname,
                sIdentification = SchoolTUser.sIdentification,
                dBirth = SchoolTUser.dBirth,
                cSex = SchoolTUser.cSex,
                sPhone = SchoolTUser.sPhone,
                sEmail = SchoolTUser.sEmail,
                sPassword = SchoolTUser.sPassword,
                sSubtopic = SchoolTUser.sSubtopic,
                sAddress = SchoolTUser.sAddress,
                sCity = SchoolTUser.sCity,
                sPostalcode = SchoolTUser.sPostalcode,
                sCountry = SchoolTUser.sCountry,
                dUpdate = SchoolTUser.dUpdate,
                sFinger = SchoolTUser.sFinger,
                nMoney = SchoolTUser.nMoney,
                sFinger2 = SchoolTUser.sFinger2,
                nMax = SchoolTUser.nMax,
                cType = SchoolTUser.cType,
                cDel = SchoolTUser.cDel,
                cSMS = SchoolTUser.cSMS,
                baseSalary = SchoolTUser.baseSalary,
                cTelSMS = SchoolTUser.cTelSMS,
                nTermSubLevel2 = SchoolTUser.nTermSubLevel2,
                sToken = SchoolTUser.sToken,
                sStudentID = SchoolTUser.sStudentID,
                sStudentNameEN = SchoolTUser.sStudentNameEN,
                sStudentLastEN = SchoolTUser.sStudentLastEN,
                sStudentRace = SchoolTUser.sStudentRace,
                sStudentNation = SchoolTUser.sStudentNation,
                sStudentReligion = SchoolTUser.sStudentReligion,
                sStudentIdCardNumber = SchoolTUser.sStudentIdCardNumber,
                sStudentHomeNumber = SchoolTUser.sStudentHomeNumber,
                sStudentSoy = SchoolTUser.sStudentSoy,
                sStudentTumbon = SchoolTUser.sStudentTumbon,
                sStudentProvince = SchoolTUser.sStudentProvince,
                sStudentMuu = SchoolTUser.sStudentMuu,
                sStudentRoad = SchoolTUser.sStudentRoad,
                sStudentAumpher = SchoolTUser.sStudentAumpher,
                sStudentPost = SchoolTUser.sStudentPost,
                sStudentPicture = SchoolTUser.sStudentPicture,
                sNickName = SchoolTUser.sNickName,
                nSonNumber = SchoolTUser.nSonNumber,
                dPicUpdate = SchoolTUser.dPicUpdate,
                nPicversion = SchoolTUser.nPicversion,
                nStudentNumber = SchoolTUser.nStudentNumber,
                nStudentStatus = SchoolTUser.nStudentStatus,
                ContactPeak = SchoolTUser.ContactPeak,
                DayQuit = SchoolTUser.DayQuit,
                Note = SchoolTUser.Note,
                oldSchoolName = SchoolTUser.oldSchoolName,
                oldSchoolProvince = SchoolTUser.oldSchoolProvince,
                oldSchoolAumpher = SchoolTUser.oldSchoolAumpher,
                oldSchoolTumbon = SchoolTUser.oldSchoolTumbon,
                oldSchoolGPA = SchoolTUser.oldSchoolGPA,
                oldSchoolGraduated = SchoolTUser.oldSchoolGraduated,
                moveInDate = SchoolTUser.moveInDate,
                addressLng = SchoolTUser.addressLng,
                addressLat = SchoolTUser.addressLat,
                moveOutReason = SchoolTUser.moveOutReason,
                sNickNameEN = SchoolTUser.sNickNameEN,
                sStudentHomeRegisterCode = SchoolTUser.sStudentHomeRegisterCode,
                sStudentHousePhone = SchoolTUser.sStudentHousePhone,
                sStudentNameOther = SchoolTUser.sStudentNameOther,
                sStudentLastOther = SchoolTUser.sStudentLastOther,
                SchoolID = SchoolTUser.SchoolID
            };
            data.MasterTUser = MasterTUser;
            string json = fcommon.EntityToJson(data);
            //addormodifystudent
            var thread = new Thread(() => UpdataBalance(json, "addormodifystudent", MasterTUser.nCompany, AuthKey, AuthValue));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        public void Student(JabjaiEntity.DB.TUser SchoolTUser, MasterEntity.TUser MasterTUser, string PaymentAPIUrl)
        {
            StudentData data = new StudentData();

            data.SchoolTUser = new JabjaiEntity.DB.TUser
            {
                sID = SchoolTUser.sID,
                sStudentTitle = SchoolTUser.sStudentTitle,
                sName = SchoolTUser.sName,
                sLastname = SchoolTUser.sLastname,
                sIdentification = SchoolTUser.sIdentification,
                dBirth = SchoolTUser.dBirth,
                cSex = SchoolTUser.cSex,
                sPhone = SchoolTUser.sPhone,
                sEmail = SchoolTUser.sEmail,
                sPassword = SchoolTUser.sPassword,
                sSubtopic = SchoolTUser.sSubtopic,
                sAddress = SchoolTUser.sAddress,
                sCity = SchoolTUser.sCity,
                sPostalcode = SchoolTUser.sPostalcode,
                sCountry = SchoolTUser.sCountry,
                dUpdate = SchoolTUser.dUpdate,
                sFinger = SchoolTUser.sFinger,
                nMoney = SchoolTUser.nMoney,
                sFinger2 = SchoolTUser.sFinger2,
                nMax = SchoolTUser.nMax,
                cType = SchoolTUser.cType,
                cDel = SchoolTUser.cDel,
                cSMS = SchoolTUser.cSMS,
                baseSalary = SchoolTUser.baseSalary,
                cTelSMS = SchoolTUser.cTelSMS,
                nTermSubLevel2 = SchoolTUser.nTermSubLevel2,
                sToken = SchoolTUser.sToken,
                sStudentID = SchoolTUser.sStudentID,
                sStudentNameEN = SchoolTUser.sStudentNameEN,
                sStudentLastEN = SchoolTUser.sStudentLastEN,
                sStudentRace = SchoolTUser.sStudentRace,
                sStudentNation = SchoolTUser.sStudentNation,
                sStudentReligion = SchoolTUser.sStudentReligion,
                sStudentIdCardNumber = SchoolTUser.sStudentIdCardNumber,
                sStudentHomeNumber = SchoolTUser.sStudentHomeNumber,
                sStudentSoy = SchoolTUser.sStudentSoy,
                sStudentTumbon = SchoolTUser.sStudentTumbon,
                sStudentProvince = SchoolTUser.sStudentProvince,
                sStudentMuu = SchoolTUser.sStudentMuu,
                sStudentRoad = SchoolTUser.sStudentRoad,
                sStudentAumpher = SchoolTUser.sStudentAumpher,
                sStudentPost = SchoolTUser.sStudentPost,
                sStudentPicture = SchoolTUser.sStudentPicture,
                sNickName = SchoolTUser.sNickName,
                nSonNumber = SchoolTUser.nSonNumber,
                dPicUpdate = SchoolTUser.dPicUpdate,
                nPicversion = SchoolTUser.nPicversion,
                nStudentNumber = SchoolTUser.nStudentNumber,
                nStudentStatus = SchoolTUser.nStudentStatus,
                ContactPeak = SchoolTUser.ContactPeak,
                DayQuit = SchoolTUser.DayQuit,
                Note = SchoolTUser.Note,
                oldSchoolName = SchoolTUser.oldSchoolName,
                oldSchoolProvince = SchoolTUser.oldSchoolProvince,
                oldSchoolAumpher = SchoolTUser.oldSchoolAumpher,
                oldSchoolTumbon = SchoolTUser.oldSchoolTumbon,
                oldSchoolGPA = SchoolTUser.oldSchoolGPA,
                oldSchoolGraduated = SchoolTUser.oldSchoolGraduated,
                moveInDate = SchoolTUser.moveInDate,
                addressLng = SchoolTUser.addressLng,
                addressLat = SchoolTUser.addressLat,
                moveOutReason = SchoolTUser.moveOutReason,
                sNickNameEN = SchoolTUser.sNickNameEN,
                sStudentHomeRegisterCode = SchoolTUser.sStudentHomeRegisterCode,
                sStudentHousePhone = SchoolTUser.sStudentHousePhone,
                sStudentNameOther = SchoolTUser.sStudentNameOther,
                sStudentLastOther = SchoolTUser.sStudentLastOther
            };

            data.MasterTUser = MasterTUser;
            string json = fcommon.EntityToJson(data);
            //addormodifystudent
            var thread = new Thread(() => UpdataBalance(json, "addormodifystudent", PaymentAPIUrl, MasterTUser.nCompany, AuthKey, AuthValue));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public void Employee(JabjaiEntity.DB.TEmployee SchoolTEmployee, MasterEntity.TUser MasterTUser)
        {
            EmployeesData data = new EmployeesData();
            data.SchoolTEmployee = new TEmployee
            {
                sEmp = SchoolTEmployee.sEmp,
                sName = SchoolTEmployee.sName,
                sLastname = SchoolTEmployee.sLastname,
                sIdentification = SchoolTEmployee.sIdentification,
                dBirth = SchoolTEmployee.dBirth,
                cSex = SchoolTEmployee.cSex,
                sPhone = SchoolTEmployee.sPhone,
                sEmail = SchoolTEmployee.sEmail,
                sPassword = SchoolTEmployee.sPassword,
                sSubtopic = SchoolTEmployee.sSubtopic,
                sAddress = SchoolTEmployee.sAddress,
                sCity = SchoolTEmployee.sCity,
                sPostalcode = SchoolTEmployee.sPostalcode,
                sCountry = SchoolTEmployee.sCountry,
                dUpdate = SchoolTEmployee.dUpdate,
                sFinger = SchoolTEmployee.sFinger,
                nMoney = SchoolTEmployee.nMoney,
                sFinger2 = SchoolTEmployee.sFinger2,
                sClaim = SchoolTEmployee.sClaim,
                cDel = SchoolTEmployee.cDel,
                sStatusReport = SchoolTEmployee.sStatusReport,
                nTimeType = SchoolTEmployee.nTimeType,
                cType = SchoolTEmployee.cType,
                sToken = SchoolTEmployee.sToken,
                sPicture = SchoolTEmployee.sPicture,
                sProvince = SchoolTEmployee.sProvince,
                sTumbon = SchoolTEmployee.sTumbon,
                sSoy = SchoolTEmployee.sSoy,
                sHomeNumber = SchoolTEmployee.sHomeNumber,
                sMuu = SchoolTEmployee.sMuu,
                sRoad = SchoolTEmployee.sRoad,
                sAumpher = SchoolTEmployee.sAumpher,
                sPost = SchoolTEmployee.sPost,
                sTitle = SchoolTEmployee.sTitle,
                dPicUpdate = SchoolTEmployee.dPicUpdate,
                nPicversion = SchoolTEmployee.nPicversion,
                leavecheck = SchoolTEmployee.leavecheck,
                nJobid = SchoolTEmployee.nJobid,
                nDepartmentId = SchoolTEmployee.nDepartmentId,
                gradeSystemAdmin = SchoolTEmployee.gradeSystemAdmin,
                SchoolID = SchoolTEmployee.SchoolID
            };
            data.MasterTUser = MasterTUser;
            string json = fcommon.EntityToJson(data);

            var thread = new Thread(() => UpdataBalance(json, "addormodifyemployee", MasterTUser.nCompany, AuthKey, AuthValue));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public void Employee(JabjaiEntity.DB.TEmployee SchoolTEmployee, MasterEntity.TUser MasterTUser, string PaymentAPIUrl)
        {
            EmployeesData data = new EmployeesData();
            data.SchoolTEmployee = new TEmployee
            {
                sEmp = SchoolTEmployee.sEmp,
                sName = SchoolTEmployee.sName,
                sLastname = SchoolTEmployee.sLastname,
                sIdentification = SchoolTEmployee.sIdentification,
                dBirth = SchoolTEmployee.dBirth,
                cSex = SchoolTEmployee.cSex,
                sPhone = SchoolTEmployee.sPhone,
                sEmail = SchoolTEmployee.sEmail,
                sPassword = SchoolTEmployee.sPassword,
                sSubtopic = SchoolTEmployee.sSubtopic,
                sAddress = SchoolTEmployee.sAddress,
                sCity = SchoolTEmployee.sCity,
                sPostalcode = SchoolTEmployee.sPostalcode,
                sCountry = SchoolTEmployee.sCountry,
                dUpdate = SchoolTEmployee.dUpdate,
                sFinger = SchoolTEmployee.sFinger,
                nMoney = SchoolTEmployee.nMoney,
                sFinger2 = SchoolTEmployee.sFinger2,
                sClaim = SchoolTEmployee.sClaim,
                cDel = SchoolTEmployee.cDel,
                sStatusReport = SchoolTEmployee.sStatusReport,
                nTimeType = SchoolTEmployee.nTimeType,
                cType = SchoolTEmployee.cType,
                sToken = SchoolTEmployee.sToken,
                sPicture = SchoolTEmployee.sPicture,
                sProvince = SchoolTEmployee.sProvince,
                sTumbon = SchoolTEmployee.sTumbon,
                sSoy = SchoolTEmployee.sSoy,
                sHomeNumber = SchoolTEmployee.sHomeNumber,
                sMuu = SchoolTEmployee.sMuu,
                sRoad = SchoolTEmployee.sRoad,
                sAumpher = SchoolTEmployee.sAumpher,
                sPost = SchoolTEmployee.sPost,
                sTitle = SchoolTEmployee.sTitle,
                dPicUpdate = SchoolTEmployee.dPicUpdate,
                nPicversion = SchoolTEmployee.nPicversion,
                leavecheck = SchoolTEmployee.leavecheck,
                nJobid = SchoolTEmployee.nJobid,
                nDepartmentId = SchoolTEmployee.nDepartmentId,
                gradeSystemAdmin = SchoolTEmployee.gradeSystemAdmin,
                SchoolID = SchoolTEmployee.SchoolID
            };
            data.MasterTUser = MasterTUser;
            string json = fcommon.EntityToJson(data);

            var thread = new Thread(() => UpdataBalance(json, "addormodifyemployee", PaymentAPIUrl, MasterTUser.nCompany, AuthKey, AuthValue));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void UpdataBalance(string json, string Mode, int? SchoolID, string _AuthKey, string _AuthValue)
        {
            string PaymentApi = ConfigurationManager.AppSettings["PaymentApi"].ToString();
            var result = fcommon.send_req(PaymentApi + "/api/shop/payment/" + Mode, fcommon.MethodPost, json, null, _AuthKey, _AuthValue);

            ////string _Server = "jabjaidatabase-a2.carstkp5z4gl.ap-southeast-1.rds.amazonaws.com";
            //string _MasterServer = ConfigurationManager.AppSettings["DataSource"].ToString();
            //string _User = ConfigurationManager.AppSettings["db_userid"].ToString();
            //string _PWD = ConfigurationManager.AppSettings["db_password"].ToString();


            //SqlConnection connection = new SqlConnection($"Server={_MasterServer};database=JabJaiMaster;uid={_User};pwd={_PWD};");
            //string _comm = $"INSERT INTO [dbo].[TB_Send_API_Log] ([Info],[Result],[SchoolID]) VALUES ('{json}','{result}',{SchoolID})";
            ////var result = fcommon.send_req("https://shopapi-test.schoolbright.co/api/shop/payment/" + Mode, fcommon.MethodPost, json, null);
            //fcommon.ExecuteNonQuery(connection, _comm);


            InsertLogAPI.PaymentLog(Mode, json, result, SchoolID ?? 0);
        }

        private static void UpdataBalance(string json, string Mode, string PaymentAPIUrl, int? SchoolID, string _AuthKey, string _AuthValue)
        {
            var result = fcommon.send_req(PaymentAPIUrl + "/api/shop/payment/" + Mode, fcommon.MethodPost, json, null, _AuthKey, _AuthValue);


            InsertLogAPI.PaymentLog(Mode, json, result, SchoolID ?? 0);

            ////string _Server = "jabjaidatabase-a2.carstkp5z4gl.ap-southeast-1.rds.amazonaws.com";
            //string _MasterServer = ConfigurationManager.AppSettings["DataSource"].ToString();
            //string _User = ConfigurationManager.AppSettings["db_userid"].ToString();
            //string _PWD = ConfigurationManager.AppSettings["db_password"].ToString();
            //SqlConnection connection = new SqlConnection($"Server={_MasterServer};database=JabJaiMaster;uid={_User};pwd={_PWD};");
            //string _comm = $"INSERT INTO [dbo].[TB_Send_API_Log] ([Info],[Result],[SchoolID]) VALUES ('{json}','{result}',{SchoolID})";
            ////var result = fcommon.send_req("https://shopapi-test.schoolbright.co/api/shop/payment/" + Mode, fcommon.MethodPost, json, null);
            //fcommon.ExecuteNonQuery(connection, _comm);
        }

        internal class StudentData
        {
            public JabjaiEntity.DB.TUser SchoolTUser { get; set; }
            public MasterEntity.TUser MasterTUser { get; set; }
        }

        internal class EmployeesData
        {
            public TEmployee SchoolTEmployee { get; set; }
            public MasterEntity.TUser MasterTUser { get; set; }
        }

        public partial class TEmployee
        {
            public int sEmp { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sIdentification { get; set; }
            public Nullable<System.DateTime> dBirth { get; set; }
            public string cSex { get; set; }
            public string sPhone { get; set; }
            public string sEmail { get; set; }
            public string sPassword { get; set; }
            public string sSubtopic { get; set; }
            public string sAddress { get; set; }
            public string sCity { get; set; }
            public string sPostalcode { get; set; }
            public string sCountry { get; set; }
            public Nullable<System.DateTime> dUpdate { get; set; }
            public string sFinger { get; set; }
            public Nullable<decimal> nMoney { get; set; }
            public string sFinger2 { get; set; }
            public string sClaim { get; set; }
            public string cDel { get; set; }
            public string sStatusReport { get; set; }
            public Nullable<int> nTimeType { get; set; }
            public string cType { get; set; }
            public string sToken { get; set; }
            public string sPicture { get; set; }
            public string sProvince { get; set; }
            public string sTumbon { get; set; }
            public string sSoy { get; set; }
            public string sHomeNumber { get; set; }
            public string sMuu { get; set; }
            public string sRoad { get; set; }
            public string sAumpher { get; set; }
            public string sPost { get; set; }
            public string sTitle { get; set; }
            public Nullable<System.DateTime> dPicUpdate { get; set; }
            public Nullable<int> nPicversion { get; set; }
            public Nullable<int> leavecheck { get; set; }
            public Nullable<int> nJobid { get; set; }
            public Nullable<int> nDepartmentId { get; set; }
            public Nullable<int> gradeSystemAdmin { get; set; }
            public Nullable<int> SchoolID { get; set; }
        }

    }
}