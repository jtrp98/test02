<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductAutocomplete.ascx.cs" Inherits="FingerprintPayment.UserControls.ProductAutocomplete" %>

<style>

</style>
<script defer>

    function ProductAutocomplete() {

        var _my = {};
        var _acDataList;
        var _searchRes;
        var _acObj = { id: '', text: '' };
     
        Ajax2 = function (shopid) {
            $.ajax({
                url: `/App_Logic/dataGenericListData.ashx?mode=listproduct&id=${shopid}`,                
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
                    _acDataList = res.map(o => (
                        { id: o.id, text: o.name , code: o.code , unit : o.unit  }
                    ));
                },
                error: function () {

                }
            });
        };

        LoadData = function (shopid) {

            $.when(Ajax2(shopid))
                .then(() => {
                    $('#txtProductAutocomplete').autoComplete({
                        resolver: 'custom',
                        minLength: 1,
                        events: {
                            search: function (qry, callback) {

                                if (_acDataList != null) {
                                    var res = _acDataList.filter((v) => {
                                        if (v.text.toLowerCase().indexOf(qry.toLowerCase()) > -1 ||
                                            v.code.indexOf(qry) > -1) {
                                            return v;
                                        }
                                    });
                                    _searchRes = res;
                                    callback(_searchRes);
                                }
                            }
                        }
                    });

                    $('#txtProductAutocomplete').on('autocomplete.select', function (evt, item) {
                        _acObj = item;
                    });

                }).catch(() => {

                });

        };

        var _id = $('#txtProductAutocomplete').data('shopid');
        if (_id) {
            LoadData(_id);
        }

        _my.LoadProduct = function (shopid) {
            LoadData(shopid);
        };

        _my.GetProductName = function () {
            var v = $('#txtProductAutocomplete').val();//_acObj.text 
            if (!!v) {
                var text = _acObj.text;
                if (text)
                    return text;
            }
            return v;
        };

        _my.GetProductID = function () {
            var v = $('#txtProductAutocomplete').val();//_acObj.text 
            if (!!v) {
                var id = _acObj.id;
                if (id)
                    return id;
            }
            return "";
        };

        _my.GetProduct = function () {
            return _acObj;
        };

        _my.GetProductList = function () {
            return _acDataList;
        };

        _my.GetSearchList = function () {
            return _searchRes;
        };

        _my.Clear = function () {
            $('#txtProductAutocomplete').val('');
        };

        _my.GetShopID = function () {
           return $('#txtProductAutocomplete').data('shopid');
        };

        return _my;
    }
    var PAC;
    $(function () {
        PAC = ProductAutocomplete();
    });
</script>

<asp:TextBox ID="txtProductAutocomplete"
    ClientIDMode="Static"
    CssClass="form-control"
    placeholder="ค้นหา ชื่อ/รหัส"
    data-noresults-text="ไม่มีข้อมูล"
    autocomplete="off"  
    runat="server">
</asp:TextBox>