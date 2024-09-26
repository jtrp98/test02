<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentAutocomplete.ascx.cs" Inherits="FingerprintPayment.UserControls.StudentAutocomplete" %>

<style>

</style>
<script defer>

    //class SAC//Student Autocomplete
    //{
    //    //static _studentList;
    //    //static _selected;

    //    constructor() {
    //        this.LoadData();
    //    }


    //    static GetStudentList() {
    //        return this._studentList;
    //    }
    //    static GetStudent() {
    //        return this._selected;
    //    }

    //    static SetStudent(v) {
    //        this._selected = v;
    //    }
    //}

    //var _sac = new SAC();


    function StudentAutocomplete() {

        var _my = {};
        var _acStudentList;
        var _acStudent = { id : '' , text : ''};

        Ajax1 = function () {
            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudentv2&isGraduated=<%= IsGraduated %>&nelevel=&nsublevel=",
                type: "get",
                dataType: "json",
                global: false,
                //async: false,
                success: function (res) {
                    _acStudentList = res.map(o => (
                        { id: o.sID, text: o.sName, code: o.studentid + '' }
                    ));
                },
                error: function () {

                }
            });
        };

        LoadData = function () {

            Promise.all([Ajax1()])
                .then(() => {

                $('#txtStudentAutocomplete').autoComplete({
                    resolver: 'custom',
                    minLength: 1,
                    // noResultsText: '',
                    events: {
                        search: function (qry, callback) {
                            var res = _acStudentList.filter((v) => {
                                if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 || v.code.indexOf(qry) > -1) {
                                    return v;
                                }
                            })
                            callback(res);
                        }
                    }
                });

                $('#txtStudentAutocomplete').on('autocomplete.select', function (evt, item) {
                    _acStudent = item;

                });
            });
        };

        LoadData();

        _my.GetTypeText = function () {
            var v = $('#txtStudentAutocomplete').val();
           
            return v;
        };

        _my.GetStudentName = function () {
            var v = $('#txtStudentAutocomplete').val();
            if (!!v) {
                var text = _acStudent.text;
                if (text)
                    return text;
            }
            return v;
        };

        _my.GetStudentID = function () {
            var v = $('#txtStudentAutocomplete').val();//_acStudent.text 
            if (!!v) {
                var id = _acStudent.id;
                if (id)
                    return id;
            }
            return "";
        };

        _my.GetStudent = function () {
            return _acStudent;
        };

        _my.GetStudentList = function () {
            return _acStudentList;
        };

        return _my;
    }
    var SAC;
    $(function () {
        SAC = StudentAutocomplete();
    });
</script>

<input type="text" 
    id="txtStudentAutocomplete" 
    name="txtStudentAutocomplete" 
    autocomplete="off"
    value=""
    class="form-control"
    placeholder="ค้นหา ชื่อ/รหัส"
    data-noresults-text="ไม่มีข้อมูล"
    <%= (IsRequired ? "required" : "") %>
    />
<%--<asp:TextBox ID="txtStudentAutocomplete"
    ClientIDMode="Static"
    CssClass="form-control"
    placeholder="ค้นหา ชื่อ/รหัส"
    data-noresults-text="ไม่มีข้อมูล"
    autocomplete="off"
    runat="server">
</asp:TextBox>--%>