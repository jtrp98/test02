using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Permission
{
    public class MenuGroupModel
    {
        public int GroupID { get; set; }
        public string GroupMenu { get; set; }
        public int SegmentID { get; set; }
        public string SegmentName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public short? Role { get; set; }
        public string Type { get; set; }
        public short? OrderNo1 { get; set; }
        public short? OrderNo2 { get; set; }
    }

    public class UserGroupModel
    {
        public int UserID { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Remark { get; set; }
        public bool IsSelectable { get; set; }
    }

    [Serializable]
    public class AddOrModifyGroupModel
    {
        public int? GroupID { get; set; }
        public string GroupName { get; set; }
        public List<MenuSubModel> SelectedMenu { get; set; }
        public List<int?> SelectedUser { get; set; }

        //public List<int?> MenuWebCol2 { get; set; }
        //public List<int?> MenuWebCol3 { get; set; }

        //public List<int?> MenuAppCol1 { get; set; }
        //public List<int?> MenuAppCol2 { get; set; }

        public class MenuSubModel
        {
            public int MenuId { get; set; }
            public short Role { get; set; }
            public string Type { get; set; }
        }
    }

}