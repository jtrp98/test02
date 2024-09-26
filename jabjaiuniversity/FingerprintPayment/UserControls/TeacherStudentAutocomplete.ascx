<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeacherStudentAutocomplete.ascx.cs" Inherits="FingerprintPayment.UserControls.TeacherStudentAutocomplete" %>

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


    function TeacherStudentAutocomplete() {

        var _my = {};
        var _dataList = [];
        var _dataObject = { id: '', text: '' };
        var _d1, d2;

        Ajax1 = function () {
            $.ajax({
                url: "/App_Logic/dataGenericListData.ashx?mode=liststudentv3&nelevel=&nsublevel=",
                type: "get",
                dataType: "json",
                global: false,
                //async: false,
                success: function (res) {
                    res.map(o => (
                        _dataList.push({ id: o.sID, text: o.sName, code: o.studentid + '', type: 'student' })
                    ));
                    // _dataList.concat(o);
                },
                error: function () {

                }
            });
        };

        Ajax2 = function () {
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                type: "get",
                dataType: "json",
                global: false,
                //async: false,
                success: function (res) {

                    res.map(o => (
                        _dataList.push({ id: o.sEmp, text: o.sName + ' ' + o.sLastname, code: o.Code + '', type: 'teacher' })
                    ));

                    //_dataList.concat(o);
                },
                error: function () {

                }
            });
        };

        LoadData = function () {


            //Promise.all([Ajax1(), Ajax2()])
            $.when(Ajax1(), Ajax2())
                .done(() => {
                    // _dataList.concat(_d1, _d2);

                    $('#txtStudentAutocomplete').autoComplete({
                        resolver: 'custom',
                        minLength: 1,
                        // noResultsText: '',
                        events: {
                            search: function (qry, callback) {
                                var res = _dataList.filter((v) => {
                                    if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ||
                                        v.code.indexOf(qry) > -1) {
                                        return v;
                                    }
                                })
                                callback(res);
                            }
                        }
                    });

                    $('#txtStudentAutocomplete').on('autocomplete.select', function (evt, item) {
                        _dataObject = item;
                    });
                }).catch(() => {

                })

        };

        LoadData();

        _my.GetUserName = function () {
            var v = $('#txtStudentAutocomplete').val();//_acStudent.text 
            if (!!v) {
                var text = _dataObject.text;
                if (text)
                    return text;
            }
            return v;
        };

        _my.GetUserID = function () {
            var v = $('#txtStudentAutocomplete').val();//_acStudent.text 
            if (!!v) {
                var id = _dataObject.id;
                if (id)
                    return id;
            }
            return "";
        };

        _my.GetUser = function () {
            return _dataObject;
        };

        _my.GetUserList = function () {
            return _dataList;
        };

        return _my;
    }
    var TSAC;
    $(function () {
        TSAC = TeacherStudentAutocomplete();
    });
</script>
<asp:TextBox ID="txtStudentAutocomplete"
    ClientIDMode="Static"
    CssClass="form-control"
    placeholder="ค้นหา ชื่อ/รหัส"
    data-noresults-text="ไม่มีข้อมูล"
    autocomplete="off"
    runat="server">
</asp:TextBox>