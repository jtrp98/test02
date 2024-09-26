<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeacherAutocomplete.ascx.cs" Inherits="FingerprintPayment.UserControls.TeacherAutocomplete" %>

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


    function TeacherAutocomplete() {

        var _my = {};
        var _acStudentList;
        var _acStudent = { id: '', text: '' };

        Ajax2 = function () {
            $.ajax({
                url: "/App_Logic/modalJSON.aspx?mode=teacher",
                type: "get",
                dataType: "json",
                global: false,
                //async: false,
                success: function (res) {

                    //$.each(res, function (index) {
                    //    //            var newObject = {
                    //    //                label: objjson[index].sName + ' ' + objjson[index].sLastname,
                    //    //                value: objjson[index].sEmp,
                    //    //                code: objjson[index].Code,
                    //    //            };

                    //    //            availableValueUsers.push(newObject);
                    //    //        });
                    //}
                    _acStudentList = res.map(o => (
                        { id: o.sEmp, text: o.sName + ' ' + o.sLastname, code: o.Code + '' }
                    ));
                },
                error: function () {

                }
            });
        };

        LoadData = function () {


            //Promise.all([Ajax1(), Ajax2()])
            $.when(Ajax2())
                .then(() => {
                    $('#txtStudentAutocomplete').autoComplete({
                        resolver: 'custom',
                        minLength: 1,
                        // noResultsText: '',
                        events: {
                            search: function (qry, callback) {

                                if (_acStudentList != null) {
                                    var res = _acStudentList.filter((v) => {
                                        if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ||
                                            v.code.indexOf(qry) > -1) {
                                            return v;
                                        }
                                    })
                                    callback(res);
                                }
                            }
                        }
                    });

                    $('#txtStudentAutocomplete').on('autocomplete.select', function (evt, item) {
                        _acStudent = item;

                    });
                }).catch(() => {

                })




        };

        LoadData();

        _my.GetUserName = function () {
            var v = $('#txtStudentAutocomplete').val();//_acStudent.text 
            if (!!v) {
                var text = _acStudent.text;
                if (text)
                    return text;
            }
            return v;
        };

        _my.GetUserID = function () {
            var v = $('#txtStudentAutocomplete').val();//_acStudent.text 
            if (!!v) {
                var id = _acStudent.id;
                if (id)
                    return id;
            }
            return "";
        };

        _my.GetUser = function () {
            return _acStudent;
        };

        _my.GetUserList = function () {
            return _acStudentList;
        };

        _my.Clear = function () {
            $('#txtStudentAutocomplete').val('');
        };

        return _my;
    }
    var TAC;
    $(function () {
        TAC = TeacherAutocomplete();
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