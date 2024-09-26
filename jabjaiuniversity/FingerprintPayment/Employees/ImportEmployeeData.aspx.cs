using FingerprintPayment.Class;
using FingerprintPayment.Employees.CsCode;
using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    public partial class ImportEmployeeData : EmployeeGateway
    {
        //public static decimal EmployeeImportPercentage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EmployeeImportPercentage"] = 0M;
        }

        [WebMethod(EnableSession = true)]
        public static string UpdateMonitor()
        {
            bool isComplete = false;
            decimal percentage = 0;

            percentage = Convert.ToDecimal(HttpContext.Current.Session["EmployeeImportPercentage"].ToString());
            if (percentage >= 100)
            {
                isComplete = true;
            }

            var result = new { isComplete, percentage };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string UploadAndPrepareData(string byteData)
        {
            bool success = true;
            string code = "200";
            string message = "Success to upload data.";

            int ProgressStep = 1;

            HttpContext.Current.Session["EmployeeImportPercentage"] = 0M;

            List<EmployeeImportData> listEmployee = new List<EmployeeImportData>();

            List<ProcessData> processDatas = new List<ProcessData>();

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;

            byte[] bytes = Convert.FromBase64String(byteData);
            string fileName = string.Format(@"{0}_{1}_{2}.xls", schoolID, userID, DateTime.Now.Ticks);
            string filePath = HttpContext.Current.Server.MapPath("~/Upload/Employee/" + fileName);

            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Employee")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Employee"));
                }

                // Save file in File folder.
                File.WriteAllBytes(filePath, bytes);

                ProgressStep++; // [2] Complete upload excel file
                HttpContext.Current.Session["EmployeeImportPercentage"] = 5M;


                FileInfo fileInfo = new FileInfo(filePath);
                using (var excelPackage = new ExcelPackage(fileInfo))
                {
                    string[] sheetNames = new string[] { "ข้อมูลบุคลากร" };
                    int[] headerRows = new int[] { 1 };

                    DataSet dataSet = excelPackage.ToDataSet(sheetNames, headerRows);

                    #region Set Columns Name & Employee Map Data

                    //### Tab ข้อมูลบุคลากร
                    string tableName = sheetNames[0];

                    // [A0] รหัสพนักงาน
                    // [B1] คำนำหน้า - required
                    // [C2] เพศ - required
                    // [D3] ประเภทบุคลากร
                    // [E4] แผนก
                    // [F5] ตำแหน่ง
                    // [G6] ชื่อ - required
                    // [H7] นามสกุล - required
                    // [I8] Firstname
                    // [J9] Lastname
                    // [K10] วันเดือนปีเกิด - required
                    // [L11] เลขบัตรประชาชน
                    // [M12] เบอร์โทรศัพท์ - required
                    // [N13] บ้านเลขที่
                    // [O14] ซอย
                    // [P15] หมู่
                    // [Q16] ถนน
                    // [R17] แขวง/ตำบล
                    // [S18] เขต/อำเภอ
                    // [T19] จังหวัด
                    // [U20] รหัสไปรษณีย์
                    // [V21] เชื้อชาติ
                    // [W22] สัญชาติ
                    // [X23] ศาสนา

                    dataSet.Tables[tableName].Columns[0].ColumnName = "Code";
                    dataSet.Tables[tableName].Columns[1].ColumnName = "Title";
                    dataSet.Tables[tableName].Columns[2].ColumnName = "Sex";
                    dataSet.Tables[tableName].Columns[3].ColumnName = "EmployeeType";
                    dataSet.Tables[tableName].Columns[4].ColumnName = "Department";
                    dataSet.Tables[tableName].Columns[5].ColumnName = "Job"; // int?
                    dataSet.Tables[tableName].Columns[6].ColumnName = "FirstnameTh";
                    dataSet.Tables[tableName].Columns[7].ColumnName = "LastnameTh";
                    dataSet.Tables[tableName].Columns[8].ColumnName = "FirstnameEn";
                    dataSet.Tables[tableName].Columns[9].ColumnName = "LastnameEn";
                    dataSet.Tables[tableName].Columns[10].ColumnName = "Birthday"; // DateTime?
                    dataSet.Tables[tableName].Columns[11].ColumnName = "Identification"; // length = 13
                    dataSet.Tables[tableName].Columns[12].ColumnName = "Phone";
                    dataSet.Tables[tableName].Columns[13].ColumnName = "HouseNumber";
                    dataSet.Tables[tableName].Columns[14].ColumnName = "Alley";
                    dataSet.Tables[tableName].Columns[15].ColumnName = "Village";
                    dataSet.Tables[tableName].Columns[16].ColumnName = "Road";
                    dataSet.Tables[tableName].Columns[17].ColumnName = "SubDistrict";
                    dataSet.Tables[tableName].Columns[18].ColumnName = "District";
                    dataSet.Tables[tableName].Columns[19].ColumnName = "Province";
                    dataSet.Tables[tableName].Columns[20].ColumnName = "PostalCode";
                    dataSet.Tables[tableName].Columns[21].ColumnName = "Race";
                    dataSet.Tables[tableName].Columns[22].ColumnName = "Nationality";
                    dataSet.Tables[tableName].Columns[23].ColumnName = "Religion";

                    listEmployee = dataSet.Tables[tableName].DataTableToList<EmployeeImportData>().Where(w => !string.IsNullOrEmpty(w.Phone)).ToList();
                    ProgressStep++; // [3] Complete excel to object (Tab ข้อมูลบุคลากร)
                    HttpContext.Current.Session["EmployeeImportPercentage"] = 25M;


                    #endregion

                }
                // End using (var excelPackage = new ExcelPackage(fileInfo))
            }
            catch (Exception err)
            {
                // 501 : Error upload excel file
                // 502 : Error excel to object (Tab ข้อมูลบุคลากร)

                success = false;
                code = "50" + ProgressStep.ToString();
                message = "error[" + code + "(" + ErrorCodeMessage(code) + ")]: " + err.Message + ", :line " + ComFunction.GetLineNumberError(err);

                string logMessagePattern = @"[SchoolEntities:{0}], [Filename:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                string errorMessage = err.Message;
                string innerExceptionMessage = "";
                while (err.InnerException != null) { innerExceptionMessage += ", " + err.InnerException.Message; err = err.InnerException; }
                string logMessageDebug = string.Format(logMessagePattern, userData.Entities, fileName, ComFunction.GetLineNumberError(err), errorMessage + ", " + innerExceptionMessage);

                int? schID = schoolID;
                int? empID = userID;

                ComFunction.InsertLogDebug(schID, null, empID, logMessageDebug);
            }

            if (success)
            {
                // Load Data from MasterDB
                List<province> listProvince = new List<province>();
                List<amphur> listAmphur = new List<amphur>();
                List<district> listDistrict = new List<district>();
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var schoolObj = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        schoolID = schoolObj.nCompany;

                        listProvince = dbMaster.provinces.ToList();
                        listAmphur = dbMaster.amphurs.ToList();
                        listDistrict = dbMaster.districts.ToList();
                    }
                }

                // Load Data from SchoolDB
                province provinceObj = new province();
                amphur amphurObj = new amphur();
                district districtObj = new district();

                TTitleList titleObj = new TTitleList();
                TJobList jobObj = new TJobList();

                TEmployeeType employeeTypeObj = new TEmployeeType();
                TDepartment departmentObj = new TDepartment();

                // Race - เชื้อชาติ
                List<TMasterData> listMasterData9 = new List<TMasterData>();
                // Nation - สัญชาติ
                List<TMasterData> listMasterData3 = new List<TMasterData>();
                // Religion - ศาสนา
                List<TMasterData> listMasterData6 = new List<TMasterData>();

                // Title
                List<TTitleList> listTitle = new List<TTitleList>();

                // Job
                List<TJobList> listJob = new List<TJobList>();

                // Employee Type
                List<TEmployeeType> listEmployeeTypes = new List<TEmployeeType>();

                // Department
                List<TDepartment> listDepartments = new List<TDepartment>();

                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    listMasterData9 = dbSchool.TMasterDatas.Where(w => w.MasterType == "9").ToList();
                    listMasterData3 = dbSchool.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                    listMasterData6 = dbSchool.TMasterDatas.Where(w => w.MasterType == "6").ToList();

                    listTitle = dbSchool.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                    listJob = dbSchool.TJobLists.Where(w => w.SchoolID == schoolID && w.deleted != "1").ToList();

                    listEmployeeTypes = dbSchool.TEmployeeTypes.Where(w => w.SchoolID == schoolID && w.IsActive == true && w.IsDel == false).ToList();
                    listDepartments = dbSchool.TDepartments.Where(w => w.SchoolID == schoolID && w.deleted != 1).ToList();
                }


                // Prepare data
                int rowIndex = 1;
                foreach (var employeeData in listEmployee)
                {
                    //Thread.Sleep(3000);
                    HttpContext.Current.Session["EmployeeImportPercentage"] = 25M + Convert.ToDecimal((rowIndex / (listEmployee.Count * 1M)) * 75);

                    double stepLog = 0;

                    ProcessData processData = new ProcessData { No = rowIndex, Code = employeeData.Code, Name = employeeData.FirstnameTh, Lastname = employeeData.LastnameTh, IDCardNumber = employeeData.Identification, Errors = new List<Error>() };

                    try
                    {
                        // Prepare Employee Data
                        //#ข้อมูลบุคลากร
                        stepLog++; // [1] Prepare Employee Data #ข้อมูลบุคลากร

                        employeeData.Code = employeeData.Code.Trim(); // required
                        if (string.IsNullOrEmpty(employeeData.Code)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: A]: กรุณาระบุข้อมูล(รหัสพนักงาน (Username))" });
                        if (employeeData.Code.Length > 20) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: A]: ข้อมูลไม่ถูกต้อง(รหัสพนักงาน (Username) ยาวเกิน 20 ตัวอักษร)" });

                        if (!string.IsNullOrEmpty(employeeData.Code))
                        {
                            Match match = Regex.Match(employeeData.Code, @"^[A-Za-z0-9]+$");
                            if (!match.Success)
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: A]: ข้อมูลไม่ถูกต้อง(รหัสพนักงาน (Username))" });
                            }
                        }

                        employeeData.Title = employeeData.Title.Trim(); // required
                        if (!string.IsNullOrEmpty(employeeData.Title))
                        {
                            titleObj = listTitle.Where(w => w.titleDescription == employeeData.Title).FirstOrDefault();
                            if (titleObj != null)
                            {
                                employeeData.Title = titleObj.nTitleid.ToString();
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: B]: ข้อมูลไม่ถูกต้อง(คำนำหน้า)" });
                            }
                        }
                        else
                        {
                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: B]: กรุณาระบุข้อมูล(คำนำหน้า)" });
                        }

                        if (!string.IsNullOrEmpty(employeeData.Sex))
                        {
                            switch (employeeData.Sex) // required
                            {
                                case "ชาย": employeeData.Sex = "0"; break;
                                case "หญิง": employeeData.Sex = "1"; break;
                                default:
                                    employeeData.Sex = null;
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: C]: ข้อมูลไม่ถูกต้อง(เพศ)" }); break;
                            }
                        }
                        else
                        {
                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: C]: กรุณาระบุข้อมูล(เพศ)" });
                        }

                        employeeTypeObj = listEmployeeTypes.Where(w => w.Title == employeeData.EmployeeType).FirstOrDefault();
                        if (employeeTypeObj != null)
                        {
                            employeeData.cEmployeeType = (employeeTypeObj.nTypeId2 ?? employeeTypeObj.nTypeId) + "";
                        }
                        else
                        {
                            processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลบุคลากร: D]: ข้อมูลไม่ถูกต้อง(ประเภทบุคลากร)" });
                        }

                        departmentObj = listDepartments.Where(w => w.departmentName == employeeData.Department).FirstOrDefault();
                        if (departmentObj != null)
                        {
                            employeeData.iDepartment = departmentObj.DepID;
                        }
                        else
                        {
                            processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลบุคลากร: E]: ข้อมูลไม่ถูกต้อง(แผนก)" });
                        }

                        jobObj = listJob.Where(w => w.jobDescription == employeeData.Job).FirstOrDefault();
                        if (jobObj != null)
                        {
                            employeeData.iJob = jobObj.nJobid;
                        }
                        else
                        {
                            processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: F]: ข้อมูลไม่ถูกต้อง(ตำแหน่ง)" });
                        }

                        employeeData.FirstnameTh = employeeData.FirstnameTh.Trim(); // required
                        if (string.IsNullOrEmpty(employeeData.FirstnameTh)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: G]: กรุณาระบุข้อมูล(ชื่อ)" });

                        employeeData.LastnameTh = employeeData.LastnameTh.Trim(); // required
                        if (string.IsNullOrEmpty(employeeData.LastnameTh)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: H]: กรุณาระบุข้อมูล(นามสกุล)" });

                        employeeData.FirstnameEn = employeeData.FirstnameEn.Trim();
                        employeeData.LastnameEn = employeeData.LastnameEn.Trim();

                        if (!string.IsNullOrEmpty(employeeData.Birthday))
                        {
                            if (DateTime.TryParseExact(employeeData.Birthday, "d/M/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime outputDateTime))
                            {
                                employeeData.dBirthday = HelperFunctions.StringToDateTime(employeeData.Birthday); // required
                                if (employeeData.dBirthday == null) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: K]: ข้อมูลไม่ถูกต้อง(วันเดือนปีเกิด)" });
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: K]: ข้อมูลไม่ถูกต้อง(วันเดือนปีเกิด)*" });
                            }
                        }
                        else processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: K]: กรุณาระบุข้อมูล(วันเดือนปีเกิด)" });

                        employeeData.Identification = employeeData.Identification.Replace(" ", "").Replace("-", "").Trim();
                        if (employeeData.Identification.Length != 13 && employeeData.Identification.Length > 1) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: L]: ข้อมูลไม่ถูกต้อง(เลขบัตรประชาชน)" });

                        employeeData.Phone = employeeData.Phone.Replace(" ", "").Replace("-", "").Trim(); // required
                        if (string.IsNullOrEmpty(employeeData.Phone)) processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: M]: กรุณาระบุข้อมูล(เบอร์โทรศัพท์)" });
                        if (!string.IsNullOrEmpty(employeeData.Phone))
                        {
                            Match match = Regex.Match(employeeData.Phone, @"^[0-9]{9,10}$");
                            if (!match.Success)
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: M]: ข้อมูลไม่ถูกต้อง(เบอร์โทรศัพท์)" });
                            }
                        }

                        // Check exist username from db.
                        using (JabJaiMasterEntities ctxm = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            int checkExistUsername = ctxm.TUsers.Where(w => w.nCompany == schoolID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == employeeData.Code) && (w.sName != employeeData.FirstnameTh || w.sLastname != employeeData.LastnameTh || w.sIdentification != employeeData.Identification)).Count();
                            if (checkExistUsername != 0)
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: A]: ข้อมูลห้ามซ้ำ(รหัสพนักงาน (Username))" });
                            }
                        }

                        employeeData.HouseNumber = employeeData.HouseNumber.Trim();
                        employeeData.Alley = employeeData.Alley.Trim();
                        employeeData.Village = employeeData.Village.Trim();
                        employeeData.Road = employeeData.Road.Trim();

                        if (!string.IsNullOrEmpty(employeeData.HouseNumber)) employeeData.Address += "บ้านเลขที่ " + employeeData.HouseNumber;
                        if (!string.IsNullOrEmpty(employeeData.Alley)) employeeData.Address += " ซอย " + employeeData.Alley;
                        if (!string.IsNullOrEmpty(employeeData.Road)) employeeData.Address += " ถนน " + employeeData.Road;
                        if (!string.IsNullOrEmpty(employeeData.Village)) employeeData.Address += " หมู่ " + employeeData.Village;

                        employeeData.Province = employeeData.Province.Replace("ฯ", "").Trim();
                        if (!string.IsNullOrEmpty(employeeData.Province))
                        {
                            provinceObj = listProvince.Where(w => w.PROVINCE_NAME.Contains(employeeData.Province)).FirstOrDefault();
                            if (provinceObj != null)
                            {
                                employeeData.iProvince = provinceObj.PROVINCE_ID;
                                amphurObj = listAmphur.Where(w => (w.AMPHUR_NAME.Contains(employeeData.District.Trim()) || w.AMPHUR_NAME.Contains(employeeData.District.Trim().Replace("เขต", ""))) && w.PROVINCE_ID == provinceObj.PROVINCE_ID).FirstOrDefault();
                                if (amphurObj != null)
                                {
                                    employeeData.iDistrict = amphurObj.AMPHUR_ID;
                                    districtObj = listDistrict.Where(w => (w.DISTRICT_NAME.Contains(employeeData.SubDistrict.Trim()) || w.DISTRICT_NAME.Contains(employeeData.SubDistrict.Trim().Replace("เขต", ""))) && w.AMPHUR_ID == amphurObj.AMPHUR_ID).FirstOrDefault();
                                    if (districtObj != null)
                                    {
                                        employeeData.iSubDistrict = districtObj.DISTRICT_ID;
                                    }
                                    else
                                    {
                                        processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: R]: ข้อมูลไม่ถูกต้อง(แขวง/ตำบล)" });
                                    }
                                }
                                else
                                {
                                    processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: S]: ข้อมูลไม่ถูกต้อง(เขต/อำเภอ)" });
                                }
                            }
                            else
                            {
                                processData.Errors.Add(new Error { Status = "error", Message = "[ข้อมูลบุคลากร: T]: ข้อมูลไม่ถูกต้อง(จังหวัด)" });
                            }
                        }

                        employeeData.PostalCode = employeeData.PostalCode.Trim();

                        employeeData.Race = employeeData.Race.Trim();
                        if (!string.IsNullOrEmpty(employeeData.Race))
                        {
                            var raceObj = listMasterData9.Where(w => w.MasterDes == employeeData.Race).FirstOrDefault();
                            if (raceObj != null)
                            {
                                employeeData.Race = raceObj.MasterCode;
                            }
                            else
                            {
                                employeeData.Race = null;
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลบุคลากร: V]: ข้อมูลไม่ถูกต้อง(เชื้อชาติ)" });
                            }
                        }
                        else
                        {
                            employeeData.Race = null;
                        }
                        employeeData.Nationality = employeeData.Nationality.Trim();
                        if (!string.IsNullOrEmpty(employeeData.Nationality))
                        {
                            var nationObj = listMasterData3.Where(w => w.MasterDes == employeeData.Nationality).FirstOrDefault();
                            if (nationObj != null)
                            {
                                employeeData.Nationality = nationObj.MasterCode;
                            }
                            else
                            {
                                employeeData.Nationality = null;
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลบุคลากร: W]: ข้อมูลไม่ถูกต้อง(สัญชาติ)" });
                            }
                        }
                        else
                        {
                            employeeData.Nationality = null;
                        }
                        employeeData.Religion = employeeData.Religion.Trim();
                        if (!string.IsNullOrEmpty(employeeData.Religion))
                        {
                            var religionObj = listMasterData6.Where(w => w.MasterDes == employeeData.Religion).FirstOrDefault();
                            if (religionObj != null)
                            {
                                employeeData.Religion = religionObj.MasterCode;
                            }
                            else
                            {
                                employeeData.Religion = null;
                                processData.Errors.Add(new Error { Status = "warning", Message = "[ข้อมูลบุคลากร: X]: ข้อมูลไม่ถูกต้อง(ศาสนา)" });
                            }
                        }
                        else
                        {
                            employeeData.Religion = null;
                        }

                    }
                    catch (Exception ex)
                    {
                        processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = PrepareDataStepLogMessage(stepLog), Message = ex.Message });
                        processData.Status = "error";
                    }

                    processDatas.Add(processData);

                    rowIndex++;
                }

                HttpContext.Current.Session["EmployeeImportPercentage"] = 100M;
                //Thread.Sleep(2000);


                // Final check status
                int warningCountSum = 0;
                int errorCountSum = 0;
                foreach (var processData in processDatas)
                {
                    int warningCount = processData.Errors.Where(w => w.Status == "warning").Count();
                    int errorCount = processData.Errors.Where(w => w.Status == "error").Count();

                    warningCountSum += warningCount;
                    errorCountSum += errorCount;

                    if (errorCount == 0)
                    {
                        processData.Status = "ready";
                    }
                    else
                    {
                        processData.Status = "error";
                    }
                }

                // For all check status
                if (errorCountSum == 0)
                {
                    if (warningCountSum != 0) code = "201"; // ready + warning
                }
                else
                {
                    code = "202";
                    message = "Some data is missing or some data is inaccurate.";
                }

                try
                {
                    if (!File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    code = "203";
                    message = "The file cannot be deleted.[" + ex.Message + "]";
                }

                HttpContext.Current.Session["ImportEmployeeData_" + schoolID + "_" + userID + "_" + fileName] = listEmployee;

            }

            // success[true], 200 : ready
            // success[true], 201 : ready + warning
            // success[true], 202 : Some data is missing or some data is inaccurate.
            // success[true], 203 : The file cannot be deleted.
            // success[false], 501 : Error upload excel file
            // success[false], 502 : Error excel to object (Tab ข้อมูลบุคลากร)

            var result = new { success, code, message, processDatas, uploadedFileName = fileName };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string SaveData(string fileName, string[] readyCodes)
        {
            bool success = true;
            string code = "200";
            string message = "Successfully imported employee data.";

            List<ProcessData> processDatas = new List<ProcessData>();

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            int userID = userData.UserID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    List<EmployeeImportData> listEmployee = (List<EmployeeImportData>)HttpContext.Current.Session["ImportEmployeeData_" + schoolID + "_" + userID + "_" + fileName];

                    listEmployee = listEmployee.Where(w => readyCodes.Contains(w.Code)).ToList();

                    HttpContext.Current.Session["EmployeeImportPercentage"] = 0M;

                    var passwordAllUsers = (from a in dbMaster.TUsers
                                            where string.IsNullOrEmpty(a.sFinger) && a.cDel == null
                                            select a.sPassword
                                            ).ToList();

                    int rowIndex = 1;
                    foreach (var employeeData in listEmployee)
                    {
                        HttpContext.Current.Session["EmployeeImportPercentage"] = Convert.ToDecimal((rowIndex / (listEmployee.Count * 1M)) * 100);

                        double stepLog = 0;

                        ProcessData processData = new ProcessData { No = rowIndex, Code = employeeData.Code, Name = employeeData.FirstnameTh, Lastname = employeeData.LastnameTh, IDCardNumber = employeeData.Identification, Errors = new List<Error>() };

                       
                            try
                            {
                                // Save Employee Data
                                string sPassword = "999999"; // RandomNumber(dbMaster, passwordAllUsers);

                                TEmployee employeeObj = new TEmployee();

                                var userMasterObj = dbMaster.TUsers.Where(w => w.username == employeeData.Code && w.nCompany == schoolID && w.cType == "1" && w.cDel == null).FirstOrDefault();
                                if (userMasterObj == null)
                                {
                                    stepLog = 1; // Step : New master user
                                    processData.Method = "insert";
                                    userMasterObj = new MasterEntity.TUser
                                    {
                                        sName = employeeData.FirstnameTh,
                                        sLastname = employeeData.LastnameTh,
                                        sIdentification = employeeData.Identification,
                                        cSex = employeeData.Sex,
                                        sPhone = employeeData.Phone,
                                        //sEmail = employeeData.email,
                                        sPassword = sPassword,
                                        cType = "1",
                                        nCompany = schoolID,
                                        dBirth = employeeData.dBirthday,
                                        sAddress = employeeData.Address,
                                        DISTRICT_ID = employeeData.iSubDistrict,
                                        AMPHUR_ID = employeeData.iDistrict,
                                        PROVINCE_ID = employeeData.iProvince,
                                        username = employeeData.Code,
                                        userpassword = employeeData.dBirthday.Value.ToString("ddMMyyyy"),
                                        PasswordHash = ComFunction.HashSHA1(employeeData.dBirthday.Value.ToString("ddMMyyyy")),
                                        UseEncryptPassword = false,
                                        UseBiometric = true,
                                        dCreate = DateTime.Now.FixSecondAndMillisecond(4, 1),
                                        dUpdate = DateTime.Now.FixSecondAndMillisecond(4, 1),
                                    };

                                    dbMaster.TUsers.Add(userMasterObj);

                                    dbMaster.SaveChanges();

                                    userMasterObj.nSystemID = userMasterObj.sID;

                                    dbMaster.SaveChanges();


                                    stepLog = 2; // Step : New employee
                                    employeeObj = new TEmployee
                                    {
                                        sEmp = userMasterObj.sID,
                                        sName = employeeData.FirstnameTh,
                                        sLastname = employeeData.LastnameTh,
                                        sIdentification = employeeData.Identification,
                                        cSex = employeeData.Sex,
                                        sPhone = employeeData.Phone,
                                        //sEmail = employeeData.email,
                                        dPicUpdate = DateTime.Now.FixSecondAndMillisecond(4, 1),
                                        cType = employeeData.cEmployeeType,
                                        nDepartmentId = employeeData.iDepartment,
                                        nMoney = 0,
                                        dBirth = employeeData.dBirthday,
                                        sTumbon = employeeData.iSubDistrict?.ToString(),
                                        sAumpher = employeeData.iDistrict?.ToString(),
                                        sProvince = employeeData.iProvince?.ToString(),
                                        nPicversion = 0,
                                        sTitle = employeeData.Title,
                                        nJobid = employeeData.iJob,
                                        sAddress = employeeData.Address,
                                        nTimeType = 3,
                                        sHomeNumber = employeeData.HouseNumber,
                                        sSoy = employeeData.Alley,
                                        sMuu = employeeData.Village,
                                        sRoad = employeeData.Road,
                                        SchoolID = schoolID,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(4, 1)
                                    };
                                    dbSchool.TEmployees.Add(employeeObj);

                                    dbSchool.SaveChanges();


                                    stepLog = 3; // Step : New employee info
                                    int ID = (int)(dbSchool.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == userMasterObj.sID).Count() == 0 ? 1 : dbSchool.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == userMasterObj.sID).Max(m => m.ID) + 1);
                                    TEmployeeInfo employeeInfoObj = new TEmployeeInfo
                                    {
                                        sEmp = userMasterObj.sID,
                                        ID = ID,
                                        Code = employeeData.Code,
                                        FirstNameEn = employeeData.FirstnameEn,
                                        LastNameEn = employeeData.LastnameEn,
                                        Nationality = employeeData.Nationality,
                                        Ethnicity = employeeData.Race,
                                        Religion = employeeData.Religion,
                                        SchoolID = schoolID,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(4, 1),
                                        CreatedBy = userData.UserID
                                    };

                                    dbSchool.TEmployeeInfoes.Add(employeeInfoObj);

                                    dbSchool.SaveChanges();
                                }
                                else
                                {
                                    stepLog = 4; // Step : Modify master user
                                    processData.Method = "update";

                                    if (employeeData.dBirthday != null) userMasterObj.dBirth = employeeData.dBirthday;
                                    userMasterObj.cType = "1";
                                    if (employeeData.dBirthday != null && !userMasterObj.UseEncryptPassword)
                                    {
                                        userMasterObj.userpassword = employeeData.dBirthday.Value.ToString("ddMMyyyy");
                                        userMasterObj.PasswordHash = ComFunction.HashSHA1(employeeData.dBirthday.Value.ToString("ddMMyyyy"));
                                    }
                                    if (!string.IsNullOrEmpty(employeeData.Address)) userMasterObj.sAddress = employeeData.Address;
                                    if (employeeData.iSubDistrict != null) userMasterObj.DISTRICT_ID = employeeData.iSubDistrict;
                                    if (employeeData.iDistrict != null) userMasterObj.AMPHUR_ID = employeeData.iDistrict;
                                    if (employeeData.iProvince != null) userMasterObj.PROVINCE_ID = employeeData.iProvince;
                                    userMasterObj.cDel = null;
                                    userMasterObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(4, 2);

                                    dbMaster.SaveChanges();


                                    employeeObj = dbSchool.TEmployees.Where(w => w.sEmp == userMasterObj.sID).FirstOrDefault();
                                    if (employeeObj != null)
                                    {
                                        stepLog = 5; // Step : Modify employee
                                        employeeObj.sName = employeeData.FirstnameTh;
                                        employeeObj.sLastname = employeeData.LastnameTh;
                                        employeeObj.sIdentification = employeeData.Identification;
                                        employeeObj.cSex = employeeData.Sex;
                                        employeeObj.sPhone = employeeData.Phone;
                                        //employeeObj.sEmail = employeeData.email;
                                        employeeObj.cType = employeeData.cEmployeeType;
                                        employeeObj.nDepartmentId = employeeData.iDepartment;
                                        employeeObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(4, 2);
                                        employeeObj.dBirth = employeeData.dBirthday;
                                        employeeObj.sTumbon = employeeData.iSubDistrict?.ToString();
                                        employeeObj.sAumpher = employeeData.iDistrict?.ToString();
                                        employeeObj.sProvince = employeeData.iProvince?.ToString();
                                        employeeObj.nPicversion = 0;
                                        employeeObj.sTitle = employeeData.Title;
                                        employeeObj.nJobid = employeeData.iJob;
                                        employeeObj.sAddress = employeeData.Address;
                                        employeeObj.sHomeNumber = employeeData.HouseNumber;
                                        employeeObj.sSoy = employeeData.Alley;
                                        employeeObj.sMuu = employeeData.Village;
                                        employeeObj.sRoad = employeeData.Road;
                                        employeeObj.cDel = null;
                                        employeeObj.SchoolID = schoolID;
                                        employeeObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(4, 2);
                                        employeeObj.UpdatedBy = userData.UserID;
                                    }
                                    else
                                    {
                                        stepLog = 6; // Step : New employee
                                        employeeObj = new TEmployee
                                        {
                                            sEmp = userMasterObj.sID,
                                            sName = employeeData.FirstnameTh,
                                            sLastname = employeeData.LastnameTh,
                                            sIdentification = employeeData.Identification,
                                            cSex = employeeData.Sex,
                                            sPhone = employeeData.Phone,
                                            //sEmail = employeeData.email,
                                            dPicUpdate = DateTime.Now.FixSecondAndMillisecond(4, 3),
                                            cType = employeeData.cEmployeeType,
                                            nDepartmentId = employeeData.iDepartment,
                                            nMoney = 0,
                                            dBirth = employeeData.dBirthday,
                                            sTumbon = employeeData.iSubDistrict?.ToString(),
                                            sAumpher = employeeData.iDistrict?.ToString(),
                                            sProvince = employeeData.iProvince?.ToString(),
                                            nPicversion = 0,
                                            sTitle = employeeData.Title,
                                            nJobid = employeeData.iJob,
                                            sAddress = employeeData.Address,
                                            nTimeType = 3,
                                            sHomeNumber = employeeData.HouseNumber,
                                            sSoy = employeeData.Alley,
                                            sMuu = employeeData.Village,
                                            sRoad = employeeData.Road,
                                            SchoolID = schoolID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(4, 3),
                                            CreatedBy = userData.UserID
                                        };

                                        dbSchool.TEmployees.Add(employeeObj);
                                    }

                                    dbSchool.SaveChanges();


                                    var employeeInfoObj = dbSchool.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == userMasterObj.sID).OrderByDescending(o => o.ID).FirstOrDefault();
                                    if (employeeInfoObj != null)
                                    {
                                        stepLog = 7; // Step : Modify employee info
                                        if (!string.IsNullOrEmpty(employeeData.Code)) employeeInfoObj.Code = employeeData.Code;
                                        if (!string.IsNullOrEmpty(employeeData.FirstnameEn)) employeeInfoObj.FirstNameEn = employeeData.FirstnameEn;
                                        if (!string.IsNullOrEmpty(employeeData.LastnameEn)) employeeInfoObj.LastNameEn = employeeData.LastnameEn;
                                        if (!string.IsNullOrEmpty(employeeData.Nationality)) employeeInfoObj.Nationality = employeeData.Nationality;
                                        if (!string.IsNullOrEmpty(employeeData.Race)) employeeInfoObj.Ethnicity = employeeData.Race;
                                        if (!string.IsNullOrEmpty(employeeData.Religion)) employeeInfoObj.Religion = employeeData.Religion;
                                        employeeInfoObj.SchoolID = schoolID;
                                        employeeInfoObj.UpdateDate = DateTime.Now.FixSecondAndMillisecond(4, 2);
                                        employeeInfoObj.UpdateBy = userData.UserID;
                                    }
                                    else
                                    {
                                        stepLog = 8; // Step : New employee info
                                        int ID = (int)(dbSchool.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == userMasterObj.sID).Count() == 0 ? 1 : dbSchool.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == userMasterObj.sID).Max(m => m.ID) + 1);
                                        employeeInfoObj = new TEmployeeInfo
                                        {
                                            sEmp = userMasterObj.sID,
                                            ID = ID,
                                            Code = employeeData.Code,
                                            FirstNameEn = employeeData.FirstnameEn,
                                            LastNameEn = employeeData.LastnameEn,
                                            Nationality = employeeData.Nationality,
                                            Ethnicity = employeeData.Race,
                                            Religion = employeeData.Religion,
                                            SchoolID = schoolID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(4, 3),
                                            CreatedBy = userData.UserID
                                        };

                                        dbSchool.TEmployeeInfoes.Add(employeeInfoObj);
                                    }

                                    dbSchool.SaveChanges();

                                }
                                // 

                               

                                try
                                {
                                    JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, userMasterObj.sID + "", "1", "");
                                    UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                                    memory.Employee(employeeObj, userMasterObj);
                                }
                                catch { }

                                processData.Status = string.Format(@"success[{0}]", processData.Method);

                                if (processData.Method == "insert")
                                {
                                    passwordAllUsers.Add(sPassword);
                                }
                            }
                            catch (TransactionAbortedException ex)
                            {
                                

                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }
                            catch (DbEntityValidationException ex)
                            {
                                

                                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                                {
                                    // Get entry
                                    DbEntityEntry entry = item.Entry;
                                    switch (entry.State)
                                    {
                                        case EntityState.Added:
                                            entry.State = EntityState.Detached;
                                            break;
                                        case EntityState.Modified:
                                            entry.CurrentValues.SetValues(entry.OriginalValues);
                                            entry.State = EntityState.Unchanged;
                                            break;
                                        case EntityState.Deleted:
                                            entry.State = EntityState.Unchanged;
                                            break;
                                    }

                                    //string entityTypeName = entry.Entity.GetType().Name;

                                    //// Display or log error messages
                                    //foreach (DbValidationError subItem in item.ValidationErrors)
                                    //{
                                    //    string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                    //             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                    //    Console.WriteLine(message);
                                    //}
                                }

                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }
                            catch (Exception ex)
                            {
                                

                                processData.Errors.Add(new Error { Code = stepLog.ToString("000"), Log = SaveDataStepLogMessage(stepLog), Message = ex.Message, Status = "error" });
                                processData.Status = "error";
                            }
                        

                        processDatas.Add(processData);

                        rowIndex++;
                    }
                    // End loop foreach (var employeeData in listEmployee)

                    // Final check status
                    int errorCount = processDatas.Where(w => w.Status == "error").Count();
                    if (errorCount > 0)
                    {
                        success = false;
                        code = "500";
                        message = "Some data is missing or some data is inaccurate.";
                    }

                    HttpContext.Current.Session["EmployeeImportPercentage"] = 100M;
                }
            }

            var result = new { success, code, message, processDatas };

            return JsonConvert.SerializeObject(result);
        }

        private static string RandomNumber(JabJaiMasterEntities dbMaster, List<string> q_user)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;

            do
            {
                numIterations = rand.Next(100000, 999999);

            } while (q_user.Where(w => w == numIterations.ToString()).ToList().Count > 0);

            return numIterations.ToString();
        }

        private static string SaveDataStepLogMessage(double stepLog)
        {
            string stepLogMessage = "";
            switch (stepLog)
            {
                case 0: stepLogMessage = "Initial Error."; break;
                case 1: stepLogMessage = "In the process of adding master user data."; break;
                case 2: stepLogMessage = "In the process of adding school employee data."; break;
                case 3: stepLogMessage = "In the process of adding school employee info data."; break;

                case 4: stepLogMessage = "In the process of editing master user data."; break;
                case 5: stepLogMessage = "In the process of editing school employee data."; break;
                case 6: stepLogMessage = "In the process of editing(adding) school employee data."; break;
                case 7: stepLogMessage = "In the process of editing school employee info data."; break;
                case 8: stepLogMessage = "In the process of editing(adding) school employee info data."; break;
                default: break;
            }
            return stepLogMessage;
        }

        private static string PrepareDataStepLogMessage(double stepLog)
        {
            string stepLogMessage = "";
            switch (stepLog)
            {
                case 0: stepLogMessage = "Initial Loop."; break;
                case 1: stepLogMessage = "Prepare Employee Data #ข้อมูลบุคลากร."; break;
                default: break;
            }
            return stepLogMessage;
        }

        private static string ErrorCodeMessage(string code)
        {
            string stepLogMessage = "";
            switch (code)
            {
                case "501": stepLogMessage = "Error upload excel file"; break;
                case "502": stepLogMessage = "Error prepare data to object set (Tab ข้อมูลบุคลากร)"; break;
                default: break;
            }
            return stepLogMessage;
        }

    }

    public class ProcessData
    {
        [JsonProperty(PropertyName = "no")]
        public int? No { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string Lastname { get; set; }

        [JsonProperty(PropertyName = "idCardNumber")]
        public string IDCardNumber { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "status")]
        // ready(Prepare Data), success(Save Data), warning, error
        public string Status { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "log")]
        public string Log { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "status")]
        // warning, error 
        public string Status { get; set; }
    }

    public class EmployeeImportData
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Sex { get; set; }
        public string EmployeeType { get; set; }
        public string cEmployeeType { get; set; }
        public string Department { get; set; }
        public int? iDepartment { get; set; }
        public string Job { get; set; }
        public int? iJob { get; set; }
        public string FirstnameTh { get; set; }
        public string LastnameTh { get; set; }
        public string FirstnameEn { get; set; }
        public string LastnameEn { get; set; }
        public string Birthday { get; set; }
        public DateTime? dBirthday { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string HouseNumber { get; set; }
        public string Alley { get; set; }
        public string Village { get; set; }
        public string Road { get; set; }
        public string Address { get; set; }
        public string SubDistrict { get; set; }
        public int? iSubDistrict { get; set; }
        public string District { get; set; }
        public int? iDistrict { get; set; }
        public string Province { get; set; }
        public int? iProvince { get; set; }
        public string PostalCode { get; set; }
        public string Race { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
    }

}