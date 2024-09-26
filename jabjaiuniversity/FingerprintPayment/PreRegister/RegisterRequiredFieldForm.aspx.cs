using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterRequiredFieldForm : PreRegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            int schoolID = UserData.CompanyID;
            string v = Request.QueryString["v"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = "";
                List<PreRegisterRequiredFieldData> listPreRegisterRequiredField = new List<PreRegisterRequiredFieldData>();
                switch (v)
                {
                    case "form01":
                        MvContent.ActiveViewIndex = 0;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 1 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField01.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form02":
                        MvContent.ActiveViewIndex = 1;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 2 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField02.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form03":
                        MvContent.ActiveViewIndex = 2;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 3 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField03.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form04":
                        MvContent.ActiveViewIndex = 3;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 4 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField04.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form05":
                        MvContent.ActiveViewIndex = 4;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 5 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField05.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form06":
                        MvContent.ActiveViewIndex = 5;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 6 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField06.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form07":
                        MvContent.ActiveViewIndex = 6;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 7 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField07.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form08":
                        MvContent.ActiveViewIndex = 7;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 8 
ORDER BY rfi.[Order]", schoolID);
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        ltrRequiredField08.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                    case "form09":
                        MvContent.ActiveViewIndex = 8;

                        query = string.Format(@"
SELECT rfi.VFIID, rfi.CategoryID, rfi.IsHead, rfi.No, rfi.FieldName, rfi.FieldNameEn, (CASE WHEN rf.Status IS NULL THEN rfi.DefaultStatus ELSE rf.Status END) 'Status'
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND rfi.CategoryID = 9 {1}{2}
ORDER BY rfi.[Order]", schoolID, (schoolID != 229 ? " AND rfi.VFIID NOT IN (18, 19)" : ""), (schoolID != 1043 ? " AND rfi.VFIID NOT IN (166, 167, 168)" : ""));
                        listPreRegisterRequiredField = en.Database.SqlQuery<PreRegisterRequiredFieldData>(query).ToList();

                        // Only SchoolID = 229 (โรงเรียนอัสสัมชัญศรีราชา)
                        // Only SchoolID = 1043 (โรงเรียนสวนกุหลาบวิทยาลัย นนทบุรี)
                        if (schoolID == 1043)
                        {
                            // Re No
                            var onlyVFI1043 = new int[] { 14, 15, 16, 17 };
                            var onlyList1043 = listPreRegisterRequiredField.Where(w => onlyVFI1043.Contains(w.VFIID)).ToList();
                            foreach (var l in onlyList1043)
                            {
                                l.No = (Convert.ToInt32(l.No) + 3).ToString();
                            }
                        }

                        ltrRequiredField09.Text = GenerateTableRequiredField(listPreRegisterRequiredField);

                        break;
                }
            }

        }

        public string GenerateTableRequiredField(List<PreRegisterRequiredFieldData> preRegisterRequiredFieldDatas)
        {
            string elementRows = "";

            foreach (var rfi in preRegisterRequiredFieldDatas)
            {
                if ((bool)rfi.IsHead)
                {
                    elementRows += string.Format(@"<tr role=""row"">
                                    <td class=""text-center"">{0}.</td>
                                    <td>{1}<br/>({2})</td>
                                    <td class=""text-center""></td>
                                    <td class=""text-center""></td>
                                </tr>", rfi.No, rfi.FieldName, rfi.FieldNameEn);
                }
                else
                {
                    elementRows += string.Format(@"<tr role=""row"">
                                    <td class=""text-center"">{0}.</td>
                                    <td>{1}<br/>({2})</td>
                                    <td class=""text-center"">
                                        <div data-vfiid=""{3}"" data-cid=""{4}"" style=""margin-left: auto; margin-right: auto; width: 140px;"">
                                            <p style=""background-color: {5}; width: 140px; height: 60px; color: white; margin: -9px 0px; padding-top: 8px;"">{6}</p>
                                        </div>
                                    </td>
                                    <td class=""text-center"">
                                        <label class=""el-switch el-switch"">
                                            <input type=""checkbox"" class=""switch-button"" data-vfiid=""{3}"" data-cid=""{4}"" {7} />
                                            <span class=""el-switch-style""></span>
                                        </label>
                                    </td>
                                </tr>", rfi.No, rfi.FieldName, rfi.FieldNameEn, rfi.VFIID, rfi.CategoryID, (rfi.Status == null || rfi.Status == false ? "#ff514b" : "#4caf50"), (rfi.Status == null || rfi.Status == false ? "ไม่จำเป็นต้องระบุ<br/>(Not Required)" : "จำเป็นต้องระบุ<br/>(Required)"), (rfi.Status == null || rfi.Status == false ? "" : "checked"));
                }
            }

            return elementRows;
        }

        public class PreRegisterRequiredFieldData
        {
            public int VFIID { get; set; }
            public int CategoryID { get; set; }
            public bool IsHead { get; set; }
            public string No { get; set; }
            public string FieldName { get; set; }
            public string FieldNameEn { get; set; }
            public bool? Status { get; set; }
        }

    }
}