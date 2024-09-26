<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPstudent_Growth.aspx.cs" MasterPageFile="~/mp.Master" Inherits="FingerprintPayment.Report.RPstudent_Growth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/Styles/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Scripts/tableExport/tableExport.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jquery.base64.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/sprintf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/jspdf.js"></script>
    <script type="text/javascript" src="../../Scripts/tableExport/jspdf/libs/base64.js"></script>

    <script src="../bootstrap SB2/bower_components/flot/excanvas.min.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot/jquery.flot.time.js" type="text/javascript"></script>
    <script src="../bootstrap SB2/bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js" type="text/javascript"></script>

    <link href="../Styles/SettingDialog.css" rel="stylesheet" />
    <script src="ScriptReport.js" type="text/javascript"></script>
    <script src="../javascript/jquery-number/jquery.number.js" type="text/javascript"></script>
    <script src="../Scripts/FileSaver.js" type="text/javascript"></script>

    <script src="Script/RP_sTudentGrowth.js" type="text/javascript"></script>

    <style type="text/css">
        @media (max-width: 999px) {
            .report-container {
                font-size: 18px;
            }

            label {
                font-weight: normal;
                font-size: 18px;
            }

            legend {
                padding-left: 30px;
                font-size: 18px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 18px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 20px;
            }
        }

        @media (min-width: 1000px) and (max-width: 1199px) {
            .report-container {
                font-size: 22px;
            }

            label {
                font-weight: normal;
                font-size: 22px;
            }

            legend {
                padding-left: 30px;
                font-size: 22px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 22px;
                width: 100%;
                padding-left: 30px;
                padding-right: 30px;
            }

            .table-show-result {
                font-size: 22px;
            }
        }

        @media (min-width: 1200px) {
            .report-container {
                font-size: 26px;
            }

            label {
                font-weight: normal;
                font-size: 26px;
            }

            legend {
                padding-left: 30px;
                font-size: 26px;
                font-weight: bold;
            }

            .button-custom {
                font-size: 26px;
                padding-left: 30px;
                padding-right: 30px;
                width: 100%;
            }

            .table-show-result {
                font-size: 26px;
            }
        }

        .ui-autocomplete {
            position: absolute;
            cursor: default;
            z-index: 1060 !important;
        }

        .centerText {
            text-align: center;
        }

        .setmin-width0 {
            min-width: 0px !important;
        }

        .setfont-size14 {
            font-size: 14px !important;
        }

        @media (min-width: 1300px) {
            #page-wrapper {
                position: inherit;
                margin: 0 0 0 250px;
                padding: 0 30px;
                border-left: 1px solid #e7e7e7;
                background-color: #eee;
                padding-top: 30px;
                padding-bottom: 30px;
                min-height: 900px;
            }
        }

        .header_01 {
            min-width: 100px;
        }

        .header_02 {
            min-width: 50px;
        }

        .btn_red.active {
            background-color: #337AB7;
        }

        .btn_red {
            min-width: 70px;
        }

        table.centerText {
            text-align: center;
        }
    </style>

    <script type="text/javascript">

        var sTudent_Growth = new sTudentGrowth();

        var yEar = "";
        var tErm = "";
        var tLeveL = "";
        var sUbLV = "";
        var sUbLV2 = "";

        $(function () {
            $('select[id*=ddlyear]').change(function () {
                GetListTrem();
            });

            $('select[id*=ddlsublevel]').change(function () {
                getListSubLV2();
            });
        });

        function SearchData() {
            yEar = $('select[id*=ddlyear').val();
            tErm = $('select[id*=semister').val();
            tLeveL = $('select[id*=tlevel').val();
            sUbLV = $('select[id*=ddlsublevel').val();
            sUbLV2 = $('select[id*=ddlSubLV2').val();
            var data = {
                "yEar_Id": yEar,
                "tErm_Id": tErm,
                "tLeveL_Id": tLeveL
                //"sUbLV_Id": sUbLV,
                //"sUbLV2_Id": sUbLV2,
            };
            searchData = data;
            $("body").mLoading();
            PageMethods.reports_Growth(data, function (e) {
                console.log(e.data);
                sTudent_Growth.reports_Growth = e;
                sTudent_Growth.RenderHtml_Growth("example", false);
            });
        }

        function GetListTrem() {
            var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
            $('select[id*=semister] option').remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
                success: function (msg) {
                    $.each(msg, function (index) {
                        $('select[id*=semister]').append($("<option></option>")
                            .attr("value", msg[index].nTerm)
                            .text(msg[index].sTerm));
                    });
                }
            });
        }

        function showModalBMI() {
            $("#modalBMI").modal()
        }
        function showModalHight() {
            $("#modalHIGHT").modal()
        }

        //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132984") %>
        function getListSubLV2() {
            var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
            $('select[id*=ddlSubLV2] option').remove();
            $.ajax({
                url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
                success: function (msg) {
                    $("select[id*=ddlSubLV2] option").remove();
                    $("select[id*=ddlSubLV2]").append($("<option></option>")
                        .attr("value", "")
                        .text("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>"));
                    $.each(msg, function (index) {
                        $('select[id*=ddlSubLV2]')
                            .append($("<option></option>")
                                .attr("value", msg[index].nTermSubLevel2)
                                .text(msg[index].nTSubLevel2));
                    });
                }
            });
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />

    <asp:HiddenField ID="hdfschoolname" runat="server" />

    <div class="full-card box-content">
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107004") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlyear" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107006") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="semister" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="tlevel" runat="server" class="form-control">
                        <asp:ListItem Text="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701054") %>" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6 hidden">
            </div>
        </div>

        <%--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133194") %>--%>
        <div class="row hidden">
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107005") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlsublevel" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group col-sm-6">
                <label class="col-md-5 col-sm-6 control-label"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107007") %> :</label>
                <div class="col-md-7 col-sm-6">
                    <asp:DropDownList ID="ddlSubLV2" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <input type="button" class="btn btn-primary btn-block" value="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101004") %>" onclick="SearchData();" />
            </div>
        </div>

        <div class="row--space">
        </div>

        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %> </div>
            <div class="col-lg-8 col-md-8 col-sm-8"></div>
            <div class="col-lg-2 col-md-2 col-sm-2">
                <div class="btn btn-success button-custom" id="exportfile" onclick="sTudent_Growth.export_excel()">Export File</div>
            </div>
        </div>

        <asp:Literal ID="ltrHeaderReport" runat="server" />

        <div class="row border-bottom">
            <br />
            <div class="col-sm-12">
                <fieldset>
                    <asp:ListView ID="lvReport" runat="server"></asp:ListView>
                    <table id="example" class="table table-condensed table-bordered table-show-result" cellspacing="0" width="100%">
                    </table>
                </fieldset>
            </div>
        </div>


    </div>
    <%--div-content--%>

    <fieldset class="hidden" id="export_excel">
        <table id="table_exports" class="table table-condensed table-bordered table-show-result" style="font-size: 16px;" cellspacing="0" width="100%">
        </table>
    </fieldset>
    <iframe id="txtArea1" style="display: none"></iframe>


    <div class="modal fade" id="modalHIGHT" tabindex="-1" role="dialog" aria-labelledby="myExtraLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <table class="table table-bordered center" <%--style="margin-top: 15px;"--%>>
                    <thead>
                        <tr>
                            <th class="text-center" colspan="9">
                                <h3 style="margin-top: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133195") %> </h3>
                            </th>
                        </tr>
                        <tr>
                            <th class="text-center" rowspan="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133196") %></th>
                            <th class="text-center" colspan="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></th>
                            <th class="text-center" colspan="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></th>
                        </tr>
                        <tr>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133198") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133197") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133199") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306016") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133198") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133197") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133199") %></th>
                            <th class="text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M306016") %></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th class="text-center">5</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 105</td>
                            <td>106-114</td>
                            <td>115-120</td>
                            <td>121 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 105</td>
                            <td>106-114</td>
                            <td>115-120</td>
                            <td>121 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">6</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 112</td>
                            <td>113-117</td>
                            <td>118-125</td>
                            <td>126 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 110</td>
                            <td>111-117</td>
                            <td>118-123</td>
                            <td>124 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">7</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 117</td>
                            <td>118-124</td>
                            <td>125-132</td>
                            <td>133 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 117</td>
                            <td>118-122</td>
                            <td>123-132</td>
                            <td>133 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">8</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 122</td>
                            <td>123-135</td>
                            <td>136-145</td>
                            <td>146 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 123</td>
                            <td>124-136</td>
                            <td>137-145</td>
                            <td>146 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">9</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 126</td>
                            <td>127-141</td>
                            <td>142-152</td>
                            <td>153 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 128</td>
                            <td>129-144</td>
                            <td>145-150</td>
                            <td>151 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">10</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 132</td>
                            <td>133-145</td>
                            <td>146-158</td>
                            <td>159 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 132</td>
                            <td>133-146</td>
                            <td>147-152</td>
                            <td>153 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">11</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 136</td>
                            <td>137-157</td>
                            <td>158-163</td>
                            <td>164 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 137</td>
                            <td>138-152</td>
                            <td>153-162</td>
                            <td>163 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">12</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 142</td>
                            <td>143-162</td>
                            <td>163-168</td>
                            <td>169 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 145</td>
                            <td>146-156</td>
                            <td>157-165</td>
                            <td>166 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">13</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 146</td>
                            <td>147-164</td>
                            <td>165-170</td>
                            <td>171 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 149</td>
                            <td>150-162</td>
                            <td>163-168</td>
                            <td>169 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">14</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 150</td>
                            <td>151-171</td>
                            <td>172-175</td>
                            <td>176 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 152</td>
                            <td>153-164</td>
                            <td>165-170</td>
                            <td>171 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">15</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 157</td>
                            <td>158-176</td>
                            <td>177-185</td>
                            <td>186 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 155</td>
                            <td>156-166</td>
                            <td>167-171</td>
                            <td>172 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">16</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 160</td>
                            <td>161-180</td>
                            <td>181-187</td>
                            <td>188 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 157</td>
                            <td>158-169</td>
                            <td>170-173</td>
                            <td>174 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">17</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 163</td>
                            <td>164-183</td>
                            <td>184-187</td>
                            <td>188 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 157</td>
                            <td>158-170</td>
                            <td>171-173</td>
                            <td>174 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">18</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 165</td>
                            <td>166-185</td>
                            <td>186-188</td>
                            <td>189 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 158</td>
                            <td>159-171</td>
                            <td>172-175</td>
                            <td>176 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">19</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 165</td>
                            <td>166-186</td>
                            <td>187-190</td>
                            <td>191 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 159</td>
                            <td>160-171</td>
                            <td>172-177</td>
                            <td>178 121 ขึ้นไป</td>
                        </tr>
                        <tr>
                            <th class="text-center">20</th>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 165</td>
                            <td>166-187</td>
                            <td>188-192</td>
                            <td>193 121 ขึ้นไป</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 160</td>
                            <td>161-171</td>
                            <td>172-179</td>
                            <td>180 121 ขึ้นไป</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalBMI" tabindex="-1" role="dialog" aria-labelledby="myExtraLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <table class="table table-bordered center">
                    <thead>
                        <tr>
                            <th class="center" colspan="2">
                                <h3 style="margin-top: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133201") %> </h3>
                            </th>
                        </tr>
                        <tr>
                            <th class="center">BMI(Body Mass Index)</th>
                            <th class="center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133202") %></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133203") %></td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111032") %></td>
                        </tr>
                        <tr>
                            <td>18.5 - 25</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133204") %></td>
                        </tr>
                        <tr>
                            <td>25 - 30</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133205") %></td>
                        </tr>
                        <tr>
                            <td>30 - 35</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133207") %></td>
                        </tr>
                        <tr>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01319") %> 35</td>
                            <td><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133206") %></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>


</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="CPH3" runat="server">
</asp:Content>

