<%@ Page Title="" Language="C#" MasterPageFile="~/Material2.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.Edit" %>

<%@ Register Src="~/UserControls/YTLCFilter.ascx" TagPrefix="uc1" TagName="YTLCFilter" %>
<%@ Register Src="~/UserControls/StudentAutocomplete.ascx" TagPrefix="uc1" TagName="StudentAutocomplete" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <link rel="stylesheet" href="/Content/VisitHouse/assets/css/signature-pad.css">

    <link href="//cdn.jsdelivr.net/npm/sweetalert2@10.12.4/dist/sweetalert2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/material-list.css?v=<%=DateTime.Now.Ticks%>" />
    <link rel="stylesheet" href="//use.fontawesome.com/releases/v5.15.3/css/all.css" />
    <link rel="stylesheet" href="//cdn.jsdelivr.net/gh/kartik-v/bootstrap-fileinput@5.5.0/css/fileinput.min.css" />

    <style>
        label.error {
            color: red;
        }

        table.dataTable tbody tr:last-child td,
        table.dataTable thead tr th {
            border-bottom: 1px solid #000;
        }

        .dataTables_wrapper .btn-group {
            display: none;
        }

        label {
            color: #333333;
        }

        .wrapper-signature {
            position: relative;
            /* width: 400px;*/
            height: 300px;
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
            user-select: none;
            margin: 10px 0;
        }

        .signature-pad {
            position: absolute;
            left: 0;
            top: 0;
            height: 300px;
            background-color: #fafafa;
        }

        .kv-file-rotate, .file-drag-handle, .kv-file-upload {
            display: none !important;
        }

        .profile-title {
            /*  min-width: 200px !important;*/

            font-weight: bold;
        }

        .wrapper-part {
        }

        .card.disabled {
            pointer-events: none;
        }

        .circle-profile {
            height: 135px;
            width: 135px;
            background-color: transparent;
            border-radius: 50%;
            display: inline-block;
            margin-top: 15px;
            /* padding: 16px; */
            border: 1px solid #efefef;
            position: relative;
            background-size: cover;
        }

        h4 {
            font-weight: bold !important;
        }

        label {
            font-weight: normal !important;
        }

        .krajee-default .file-footer-caption {
            margin-bottom: 18px !important;
        }

        .krajee-default .file-caption-info, .krajee-default .file-size-info {
            height: 20px !important;
        }

        .kv-zoom-actions {
            min-width: 340px !important;
        }

        select.form-control {
            margin-left: 10px;
            appearance: auto !important;
            -webkit-appearance: auto !important;
            -moz-appearance: auto !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <!-- the main fileinput plugin script JS file -->
    <script src="//cdn.jsdelivr.net/gh/kartik-v/bootstrap-fileinput@5.5.0/js/fileinput.min.js"></script>
    <script src="//cdn.jsdelivr.net/gh/kartik-v/bootstrap-fileinput@5.5.0/themes/fa5/theme.min.js"></script>
    <%--    <script src="/Content/VisitHouse/assets/js/signature_pad.umd.js"></script>--%>
    <script src="//cdn.jsdelivr.net/npm/signature_pad@4.0.0/dist/signature_pad.umd.min.js"></script>
    <script src="/Content/VisitHouse/assets/js/jquery.charactercounter.js"></script>
    <script src="/Scripts/jquery.mask.min.js"></script>

    <script>

        //function preventMultipleSubmissions() {
        //    $('#formsubmit1').prop('disabled', true);
        //    $('#formsubmit1').html($('#formsubmit1').html() + '...');
        //}

        //window.onbeforeunload = preventMultipleSubmissions;


        var signaturePad1, signaturePad2, signaturePad3;
        var canvas1 = document.getElementById('signature-pad1');
        var canvas2 = document.getElementById('signature-pad2');
        var canvas3 = document.getElementById('signature-pad3');

        var $qst42, $qst43;

        var dtf42 = new DataTransfer();
        var dtf43 = new DataTransfer();

     
        function SaveFormData() {

            $('#aspnetForm').on('submit', function (e) {

                if (IsFormEmpty(1)) {
                    Swal.fire({
                        type: 'warning',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00788") %> 1',
                        //ext: response.msg,
                    });

                    return false;
                }

                if (IsFormEmpty(2)) {
                    Swal.fire({
                        type: 'warning',
                        title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00788") %> 2',
                        //ext: response.msg,
                    });

                    return false;
                }

                $('#formsubmit1').prop('disabled', true);
                setTimeout(function () { $('#formsubmit1').prop('disabled', false); }, 4000);

                var obj = {};
                obj.ID = <%= DataForm.Visit.ID%>;
                obj.SchoolID = <%= DataForm.Visit.SchoolID %>;
                obj.RoomID = <%= DataForm.Visit.RoomID %>;
                obj.StudentID = <%= DataForm.Visit.StudentID %>;
                obj.TermID = '<%= DataForm.Visit.TermID %>';
                obj.YearID = <%= DataForm.Profile.YearID %>;

                obj.LINE = $("#Line").val();
                obj.Facebook = $("#Facebook").val();
                //1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>
                obj.HaveParents = GetRadioByName("qst11");
                obj.Relationship = GetRadioByName("qst12");
                obj.RelationshipOther = GetTextById("qst12text");
                obj.Fullname = GetTextById("qst131");
                obj.PhoneNumber = GetTextById("qst132");
                obj.IDCardNumber = GetTextById("qst133");
                obj.Occupation = GetRadioByName("qst14");
                obj.OccupationOther = GetTextById("qst14text");
                obj.HighestEducation = GetRadioByName("qst15");
                obj.WelfareRegistersPoor = GetRadioByName("qst16");

                //2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133545") %>
                obj.ResidentialHouse = GetRadioByName("qnew21");
                obj.DormitoryLivingWith = GetTextById("qnew21text5");
                obj.ResidentialHouseOther = GetTextById("qnew21text99");
                obj.OwnHome = GetRadioByName("qnew221");
                obj.Cleanliness = GetRadioByName("qnew222");
                obj.CleanlinessOther = GetTextById("qnew222text99");
                obj.UtilitiesElectricity = GetRadioByName("qnew223");
                obj.WaterForConsumption = GetRadioByName("qnew224");
                obj.Toilet = GetRadioByName("qnew225");
                obj.LivingEnvironment = GetTextById("qnew226");
                obj.SpendTimeWithFamily = GetTextById("qst21text") ? +GetTextById("qst21text") : null;

                obj.StudentFamilyMembersAmount = GetTextById("qnew2311") ? +GetTextById("qnew2311") : null;
                obj.StudentFamilyMembersMale = GetTextById("qnew2312") ? +GetTextById("qnew2312") : null;
                obj.StudentFamilyMembersFemale = GetTextById("qnew2313") ? +GetTextById("qnew2313") : null;

                obj.SiblingsBornSameParentsAmount = GetTextById("qnew2321") ? +GetTextById("qnew2321") : null;
                obj.SiblingsBornSameParentsMale = GetTextById("qnew2322") ? +GetTextById("qnew2322") : null;
                obj.SiblingsBornSameParentsFemale = GetTextById("qnew2323") ? +GetTextById("qnew2323") : null;

                obj.SiblingsBornDifferentParentsAmount = GetTextById("qnew2331") ? +GetTextById("qnew2331") : null;
                obj.SiblingsBornDifferentParentsMale = GetTextById("qnew2332") ? +GetTextById("qnew2332") : null;
                obj.SiblingsBornDifferentParentsFemale = GetTextById("qnew2333") ? +GetTextById("qnew2333") : null;

                obj.FamiliesNeedSpecialAssistance = GetTextById("qnew2341") ? +GetTextById("qnew2341") : null;
                obj.FamiliesNeedSpecialAssistanceTotal = GetTextById("qnew2342") ? +GetTextById("qnew2342") : null;
                obj.FamilyRelationship = GetRadioByName("qnew24");
                obj.FamilyRelationshipOther = GetTextById("qnew24text99");

                var relArray = [];
                var r1 = GetRelation(1, 'qst221');
                if (r1.relationLevel) relArray.push(r1);
                var r2 = GetRelation(2, 'qst222');
                if (r2.relationLevel) relArray.push(r2);
                var r3 = GetRelation(3, 'qst223');
                if (r3.relationLevel) relArray.push(r3);
                var r4 = GetRelation(4, 'qst224');
                if (r4.relationLevel) relArray.push(r4);
                var r5 = GetRelation(5, 'qst225');
                if (r5.relationLevel) relArray.push(r5);
                var r6 = GetRelation(6, 'qst226');
                if (r6.relationLevel) relArray.push(r6);
                var r99 = GetRelation(99, 'qst227');
                if (r99.relationLevel) relArray.push(r99);

                obj.RelationshipMember = JSON.stringify(relArray);
                obj.LeaveStudent = GetRadioByName("qst23");
                obj.LeaveStudentOther = GetTextById("qst23text");
                obj.MedianHouseholdIncome = GetRadioByName("qst24");
                obj.ReceiveExpensesFrom = GetRadioByName("qst251");
                obj.ReceiveExpensesFromOther = GetTextById("qst251text");
                obj.StudentWorkIncome = GetRadioByName("qst252");
                obj.StudentWorkIncomeOther = GetTextById("qst252text");
                obj.DailyIncome = GetRadioByName("qst253");
                obj.PaidComeDay = GetRadioByName("qst254");
                //obj.ParentWantAgencyHelp = GetCheckByName("qst27");
                //obj.ParentWantAgencyHelpOther = GetTextById("qst27text");
                obj.WorkloadTheirFamilies = GetTextById("qnew27text");
                obj.LeisureActivities = GetTextById("qnew28text");
                obj.ParentWantAgencyHelp2 = GetCheckByName("qnew2_11");
                obj.ParentWantAgencyHelp2Other = GetTextById("qnew2_11text");
                obj.ParentConcerns = GetTextById("qst28text");
                obj.ParentWantSchoolsHelp = GetCheckByName("qst26");
                obj.ParentWantSchoolsHelpOther = GetTextById("qst26text");

                //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305114") %>
                obj.Health = GetCheckByName("qst31");
                obj.WelfareSafety = GetCheckByName("qst32");
                obj.DistanceHomeToSchool = GetRadioByName("qst331");
                obj.TravelTime = GetRadioByName("qst332");
                obj.StudentTravel = GetRadioByName("qst333");
                obj.StudentTravelOther = GetTextById("qst333text");
                obj.LivingConditions = GetCheckByName("qst34");
                obj.LivingConditionsOther = GetTextById("qst34text");
                obj.StudentResponsibilities = GetCheckByName("qst35");
                obj.StudentResponsibilitiesOther = GetTextById("qst35text");
                obj.Hobbies = GetCheckByName("qst36");
                obj.HobbiesOther = GetTextById("qst36text");
                obj.SubstanceAbuseBehavior = GetCheckByName("qst37");
                obj.ViolentBehavior = GetCheckByName("qst38");
                obj.ViolentBehaviorOther = GetTextById("qst38text");
                obj.SexualBehavior = GetCheckByName("qst39");
                obj.GameAddiction = GetCheckByName("qst310");
                obj.GameAddictionOther = GetTextById("qst310text");
                obj.AccessComputerInternet = GetRadioByName("qst311");
                obj.UseElectronicTools = GetCheckByName("qst312");
                obj.StudentInfoProvider = +$('#qst3131').val();
                obj.HouseStyle = GetRadioByName("qst41");

                obj.StudentSignature = signaturePad1.toDataURL();
                obj.ParentSignature = signaturePad2.toDataURL();
                obj.TeacherSignature = signaturePad3.toDataURL();
                obj.TeacherId = $('#qstTeacher').val();
                obj.Status = 2;
                ////f.append('qst42[]', $('#qst42')[0].files[0]);
                var d = new FormData();
                //var d = $('#aspnetForm').serializeObject();
                //$.each($("#qst42")[0].files, function (i, file) {
                $.each($qst42.fileinput('getFileList'), function (i, file) {
                    d.append('attFile1', file);
                });

                $.each($qst43.fileinput('getFileList'), function (i, file) {
                    d.append('attFile2', file);
                });
                d.append('delfile', $('#qstDelFile').val());
                d.append('data', JSON.stringify(obj));


                $.ajax({
                    type: "post",
                    //url: "/VisitHousePage/Web/Edit.aspx/SaveData", 
                    url: "SaveData.ashx?sid=<%= DataForm.Profile.sID %>&term=<%= DataForm.Profile.TermID %>",
                    data: d,
                    contentType: false, // Important
                    processData: false, // Important
                    success: function (result) {
                        Swal.fire({
                            type: 'success',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102183") %>',
                            showConfirmButton: false,
                            timer: 1500,
                            //willClose: () => {
                            //    window.location.reload();
                            //}
                        }).then((result) => {
                            window.location.reload();
                        })
                    },
                    error: function (result) {
                        Swal.fire({
                            type: 'error',
                            title: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107029") %>',
                            //ext: response.msg,
                        })
                    },
                });
                return false;

            });
        }


        function GetRadioByName(name) {
            var v = $('input[name=' + name + ']:checked').val();

            if (v) {
                return +v;
            }

            return null;
        }

        function GetTextById(id) {
            return $('#' + id).val() || null;
        }

        function GetCheckByName(name) {
            var d = $('input[name=' + name + ']:checked').map(function () {
                return +this.value;
            }).get();

            if (d.length == 0) {
                return null;
            }
            return JSON.stringify(d);
        }

        function GetRelation(relId, name) {
            var o = {};
            o.relation = relId;
            o.relationLevel = GetRadioByName(name);
            o.relationText = GetTextById(name + 'text');
            return o;
        }

        function OnLoad() {

            $('.onlynumber.--tel').mask('00-00000000');
            $('.onlynumber.--idcard').mask('0-0000-00000-00-0');

            $("#qst21text").on('keyup', function () {
                var $this = $(this);
                var v = parseInt($this.val());
                var min = parseInt($this.attr('min'));
                var max = parseInt($this.attr('max'));

                if (v < min) {
                    $this.val(min);
                } else if (v > max) {
                    $this.val(max);
                }

                $this.val(Math.abs(this.value));
            });

            $('#qst28text').simpleTxtCounter({
                maxLength: 500,
                countText: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00732") %>',
                countElem: '<div style="color:#ff9898"/>',
            });

            $qst42 = $("#qst42");
            $qst43 = $("#qst43");

            $qst42.fileinput({
                /* browseClass: "btn btn-info",*/
                /*mainClass: "d-grid",*/
                deleteUrl: "<%=ResolveUrl("~/ClassOnline/WorkUploadFile.ashx")%>", //just add dummy 
                uploadUrl: "#",
                append: true,
                purifyHtml: true,
                uploadAsync: true,
                maxFileCount: 2,
                maxFileSize: 5120,
                msgSizeTooLarge: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %> "{name}"(<b>{size}</b>) <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133338") %> <b>{maxSize}</b>. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132756") %>!',
                theme: 'fa5',
                showCaption: false,
                showRemove: false,
                showUpload: false,
                showBrowse: false,
                dropZoneEnabled: true,
                overwriteInitial: false,
                initialPreviewAsData: true,
                allowedFileExtensions: ['gif', 'jpg', 'jpeg', 'png'],

                initialPreview: [
                <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 1))
        { %>
                    "<%=f.Path %>",
                <% } %>
                ],
                initialPreviewConfig: [  <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 1))
        { %>
                    {
                        type: "image",
                        caption: "<%=f.FileName %>",
                        downloadUrl: "<%=f.Path%>",
                        key: "<%=f.ID %>",
                    },
                <% } %>
                ],
            })
                .on('filedeleted', function (event, key, jqXHR, data) {
                    $('#qstDelFile').val($('#qstDelFile').val() + ',' + key);
                });

            $qst43.fileinput({
                /* browseClass: "btn btn-info",*/
                /*mainClass: "d-grid",*/
                deleteUrl: "<%= ResolveUrl("~/ClassOnline/WorkUploadFile.ashx") %>",//just add dummy    
                uploadUrl: "#",
                append: true,
                purifyHtml: true,
                uploadAsync: true,
                maxFileCount: 2,
                maxFileSize: 5120,
                msgSizeTooLarge: '<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M202042") %> "{name}"(<b>{size}</b>) <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133338") %> <b>{maxSize}</b>. <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132756") %>!',
                theme: 'fa5',
                showCaption: false,
                showRemove: false,
                showUpload: false,
                showBrowse: false,
                dropZoneEnabled: true,
                overwriteInitial: false,
                initialPreviewAsData: true,
                allowedFileExtensions: ['gif', 'jpg', 'jpeg', 'png'],

                initialPreview: [
                <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 2))
        { %>
                    "<%=f.Path %>",
                <% } %>
                ],
                initialPreviewConfig: [
                     <% foreach (var f in DataForm.VisitFiles.Where(o => o.Type == 2))
        { %>
                    {
                        type: "image",
                        caption: "<%=f.FileName %>",
                        downloadUrl: "<%=f.Path%>",
                        //width: "120px",
                        key: "<%=f.ID %>",
                    }
                    ,
                    <% } %>
                ],
            })
                .on('filedeleted', function (event, key, jqXHR, data) {
                    $('#qstDelFile').val($('#qstDelFile').val() + ',' + key);
                });

            signaturePad1 = new SignaturePad(canvas1, {
                backgroundColor: 'rgb(255, 255, 255)' // necessary for saving image as JPEG; can be removed is only saving as PNG or SVG
            });

            signaturePad2 = new SignaturePad(canvas2, {
                backgroundColor: 'rgb(255, 255, 255)' // necessary for saving image as JPEG; can be removed is only saving as PNG or SVG
            });

            signaturePad3 = new SignaturePad(canvas3, {
                backgroundColor: 'rgb(255, 255, 255)' // necessary for saving image as JPEG; can be removed is only saving as PNG or SVG
            });

        }

        function ResizeCanvas() {
            // When zoomed out to less than 100%, for some very strange reason,
            // some browsers report devicePixelRatio as less than 1
            // and only part of the canvas is cleared then.

            if ($('#wrap-part2').is(':visible')) {
                var data1 = signaturePad1.toDataURL();
                var ratio = Math.max(window.devicePixelRatio || 1, 1);
                canvas1.width = canvas1.offsetWidth * ratio;
                canvas1.height = canvas1.offsetHeight * ratio;
                canvas1.getContext("2d").scale(ratio, ratio);
                signaturePad1.fromDataURL(data1); //Added

                var data2 = signaturePad2.toDataURL();
                canvas2.width = canvas2.offsetWidth * ratio;
                canvas2.height = canvas2.offsetHeight * ratio;
                canvas2.getContext("2d").scale(ratio, ratio);
                signaturePad2.fromDataURL(data2); //Added

                var data3 = signaturePad3.toDataURL();
                canvas3.width = canvas3.offsetWidth * ratio;
                canvas3.height = canvas3.offsetHeight * ratio;
                canvas3.getContext("2d").scale(ratio, ratio);
                signaturePad3.fromDataURL(data3); //Added
            }
        }

        function GoNext(page) {
            $('.wrapper-part').hide();
            $('#wrap-part' + page).show();

            if (page == 1) {
                $('#qst28text').trigger('input');// fix
                $("#btnPage1").show();
                $("#btnPage2").hide();
            }
            else {
                $("#btnPage1").hide();
                $("#btnPage2").show();
            }

            $([document.documentElement, document.body]).animate({
                scrollTop: $("#cardbody1").offset().top
            }, 500);
            //return false;
        }

        function IsEnableForm(val) {
            switch (val) {
                case 'true':
                    $('.card').removeClass('disabled');
                    $('#lockForm').show();
                    $('#editForm').hide();
                    break;
                case 'false':
                    $('.card').addClass('disabled');
                    break;
                default:
            }
        }

        function ClearSigature(type) {
            switch (type) {
                case 1:
                    signaturePad1.clear();
                    break;
                case 2:
                    signaturePad2.clear();
                    break;
                case 3:
                    signaturePad3.clear();
                    break;

                default:
            }
        }

        function ValidateBeforeSave() {

            var currentPage = 0;
            if ($('#wrap-part1').is(':visible')) {
                currentPage = 1;
            }
            else {
                currentPage = 2;
            }

            return IsFormEmpty((currentPage == 1 ? 2 : 1));
        }

        //async function processFile($el,$text) {
        //    var files = $el.fileinput('getFileList');

        //    var base64 = "";
        //    for (var i = 0; i < files.length; i++) {
        //        //var s = getBase64(files[i]);
        //        await toBase64(files[i]).then(data => {
        //            var o = { name = files[i].name, type = files[i].type, data = data.split("base64,")[1] + "?" };
        //            base64 += JSON.stringify(o);
        //            //base64 += files[i].name + "|" + files[i].type + "|" + data.split("base64,")[1] + "?"
        //            //console.log(data);
        //        }).catch((message) => {
        //            alert(message);
        //        });
        //        //base64 += s + ",";
        //    }
        //    $text.val(base64);
        //    return base64;
        //}

        //const toBase64 = file => new Promise((resolve, reject) => {
        //    const reader = new FileReader();
        //    reader.onload = () => resolve(reader.result);
        //    reader.onerror = error => reject(error);
        //    reader.readAsDataURL(file);
        //});

        function GetBase64Image(img) {
            var canvas = document.createElement("canvas");
            canvas.width = img.width;
            canvas.height = img.height;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0);
            var dataURL = canvas.toDataURL("image/png");
            //return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
            return dataURL;
        }

        function SetRadioByNameV2(name, value) {
            if (value)
                $('input[name=' + name + '][value=' + value + ']').prop('checked', true);
        }

        function SetRadioByName(name, value) {
            if (value)
                $('input[name=' + name + ']').val([value]);
        }

        function SetCheckboxByName(name, value) {
            if (value && value.length > 0)
                $('input[name=' + name + ']').val(value);
        }

        function SetTextById(id, value) {
            $('#' + id).val(value);
        }

        function EditForm() {
            //1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %>
            SetTextById("Line", `<%= DataForm.Visit.LINE %>`);
            SetTextById("Facebook", `<%= DataForm.Visit.Facebook %>`);
            SetTextById("qst12text", `<%= DataForm.Visit.RelationshipOther %>`);
            SetRadioByName("qst11",'<%= (DataForm.Visit.HaveParents.HasValue ? (DataForm.Visit.HaveParents == true ? 1 : 0)+"" : "") %>');
            SetRadioByName("qst12",<%= DataForm.Visit.Relationship  %>);
            SetTextById("qst12text",`<%= DataForm.Visit.RelationshipOther %>`);
            SetTextById("qst131",`<%= DataForm.Visit.Fullname %>`);
            SetTextById("qst132", `<%= DataForm.Visit.PhoneNumber %>`);
            SetTextById("qst133", `<%= DataForm.Visit.IDCardNumber %>`);
            SetRadioByName("qst14",<%= DataForm.Visit.Occupation  %>);
            SetTextById("qst14text", `<%= DataForm.Visit.OccupationOther %>`);
            SetRadioByName("qst15",<%= DataForm.Visit.HighestEducation  %>);
            SetRadioByName("qst16",<%= (DataForm.Visit.WelfareRegistersPoor == true ? 1 : 0) %>);

            //2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133545") %>
            SetRadioByName("qnew21",<%= (DataForm.Visit.ResidentialHouse) %>);
            SetTextById("qnew21text5", `<%= DataForm.Visit.DormitoryLivingWith %>`);
            SetTextById("qnew21text99", `<%= DataForm.Visit.ResidentialHouseOther %>`);
            SetRadioByName("qnew221",<%= (DataForm.Visit.OwnHome) %>);
            SetRadioByName("qnew222",<%= (DataForm.Visit.Cleanliness) %>);
            SetTextById("qnew222text99", `<%= DataForm.Visit.CleanlinessOther %>`);
            SetRadioByName("qnew223",<%= (DataForm.Visit.UtilitiesElectricity) %>);
            SetRadioByName("qnew224",<%= (DataForm.Visit.WaterForConsumption) %>);
            SetRadioByName("qnew225",<%= (DataForm.Visit.Toilet) %>);
            SetTextById("qnew226", `<%= DataForm.Visit.LivingEnvironment %>`);

            SetTextById("qst21text", `<%= (DataForm.Visit.SpendTimeWithFamily.HasValue ? DataForm.Visit.SpendTimeWithFamily.Value.ToString("#") : "") %>`);

            SetTextById("qnew2311", `<%= DataForm.Visit.StudentFamilyMembersAmount %>`);
            SetTextById("qnew2312", `<%= DataForm.Visit.StudentFamilyMembersMale %>`);
            SetTextById("qnew2313", `<%= DataForm.Visit.StudentFamilyMembersFemale %>`);

            SetTextById("qnew2321", `<%= DataForm.Visit.SiblingsBornSameParentsAmount %>`);
            SetTextById("qnew2322", `<%= DataForm.Visit.SiblingsBornSameParentsMale %>`);
            SetTextById("qnew2323", `<%= DataForm.Visit.SiblingsBornSameParentsFemale %>`);

            SetTextById("qnew2331", `<%= DataForm.Visit.SiblingsBornDifferentParentsAmount %>`);
            SetTextById("qnew2332", `<%= DataForm.Visit.SiblingsBornDifferentParentsMale %>`);
            SetTextById("qnew2333", `<%= DataForm.Visit.SiblingsBornDifferentParentsFemale %>`);

            SetTextById("qnew2341", `<%= DataForm.Visit.FamiliesNeedSpecialAssistance %>`);
            SetTextById("qnew2342", `<%= DataForm.Visit.FamiliesNeedSpecialAssistanceTotal %>`);
            SetRadioByName("qnew24",<%= (DataForm.Visit.FamilyRelationship) %>);
            SetTextById("qnew24text99", `<%= DataForm.Visit.FamilyRelationshipOther %>`);

            var relArray = <%= DataForm.Visit.RelationshipMember  ?? "[]" %>;
            //SetRadioByName("qst221",<%= DataForm.Visit.Occupation  %>);
            relArray.forEach(function (i) {
                if (i.relation == 99) {
                    SetRadioByName('qst227', i.relationLevel);
                    SetTextById("qst227text", i.relationText);
                }
                else {
                    SetRadioByName('qst22' + i.relation, i.relationLevel);
                }
            });
            SetRadioByName("qst23",<%= DataForm.Visit.LeaveStudent  %>);
            SetTextById("qst23text", `<%= DataForm.Visit.LeaveStudentOther %>`);
            SetRadioByName("qst24",<%= DataForm.Visit.MedianHouseholdIncome  %>);
            SetRadioByName("qst251",<%= DataForm.Visit.ReceiveExpensesFrom  %>);
            SetTextById("qst251text", `<%= DataForm.Visit.ReceiveExpensesFromOther %>`);
            SetRadioByName("qst252",<%= DataForm.Visit.StudentWorkIncome  %>);
            SetTextById("qst252text", `<%= DataForm.Visit.StudentWorkIncomeOther %>`);
            SetRadioByName("qst253",<%= DataForm.Visit.DailyIncome %>);
            SetRadioByName("qst254",<%= DataForm.Visit.PaidComeDay  %>);

            SetTextById("qnew27text", `<%= DataForm.Visit.WorkloadTheirFamilies %>`);
            SetTextById("qnew28text", `<%= DataForm.Visit.LeisureActivities %>`);
            SetCheckboxByName("qnew2_11",<%= DataForm.Visit.ParentWantAgencyHelp2  %>);
            SetTextById("qnew2_11text", `<%= DataForm.Visit.ParentWantAgencyHelp2Other %>`);           
            SetCheckboxByName("qst26",<%= DataForm.Visit.ParentWantSchoolsHelp  %>);
            SetTextById("qst26text", `<%= DataForm.Visit.ParentWantSchoolsHelpOther %>`);           
            <%--SetCheckboxByName("qst27",<%= DataForm.Visit.ParentWantAgencyHelp  %>);
            SetTextById("qst27text", `<%= DataForm.Visit.ParentWantAgencyHelpOther %>`);--%>
            SetTextById("qst28text", `<%= DataForm.Visit.ParentConcerns %>`);

            //<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305114") %>
            SetCheckboxByName("qst31",<%= DataForm.Visit.Health  %>);
            SetCheckboxByName("qst32",<%= DataForm.Visit.WelfareSafety  %>);
            SetRadioByName("qst331",<%= DataForm.Visit.DistanceHomeToSchool  %>);
            SetRadioByName("qst332",<%= DataForm.Visit.TravelTime  %>);
            SetRadioByName("qst333",<%= DataForm.Visit.StudentTravel  %>);
            SetTextById("qst333text", `<%= DataForm.Visit.StudentTravelOther %>`);
            SetCheckboxByName("qst34",<%= DataForm.Visit.LivingConditions  %>);
            SetTextById("qst34text", `<%= DataForm.Visit.LivingConditionsOther %>`);
            SetCheckboxByName("qst35",<%= DataForm.Visit.StudentResponsibilities ?? "[]" %>);
            SetTextById("qst35text", `<%= DataForm.Visit.StudentResponsibilitiesOther %>`);
            SetCheckboxByName("qst36",<%= DataForm.Visit.Hobbies  %>);
            SetTextById("qst36text", `<%= DataForm.Visit.HobbiesOther %>`);
            SetCheckboxByName("qst37",<%= DataForm.Visit.SubstanceAbuseBehavior  %>);
            SetCheckboxByName("qst38",<%= DataForm.Visit.ViolentBehavior  %>);
            SetTextById("qst38text",`<%= DataForm.Visit.ViolentBehaviorOther  %>`);
            SetCheckboxByName("qst39",<%= DataForm.Visit.SexualBehavior  %>);
            SetCheckboxByName("qst310",<%= DataForm.Visit.GameAddiction  %>);
            SetTextById("qst310text", `<%= DataForm.Visit.GameAddictionOther %>`);
            SetRadioByName("qst311",<%= DataForm.Visit.AccessComputerInternet  %>);
            SetCheckboxByName("qst312",<%= DataForm.Visit.UseElectronicTools  %>);
            $('#qst3131').val(<%= DataForm.Visit.StudentInfoProvider  %>);
            $('#qstTeacher').val(<%= DataForm.Visit.TeacherId  %>);
            //$('#qst3131').selectpicker('refresh');
            SetRadioByName("qst41",<%= DataForm.Visit.HouseStyle  %>);

            //var sig1 = GetBase64Image(document.getElementById("imgsig1"));
            if (!!'<%= DataForm.Visit.StudentSignature  %>')
                signaturePad1.fromDataURL('data:image/png;base64,<%= DataForm.Visit.StudentSignature  %>');

            if (!!'<%= DataForm.Visit.ParentSignature  %>')
                signaturePad2.fromDataURL('data:image/png;base64,<%= DataForm.Visit.ParentSignature  %>');

            if (!!'<%= DataForm.Visit.TeacherSignature  %>')
                signaturePad3.fromDataURL('data:image/png;base64,<%= DataForm.Visit.TeacherSignature  %>');
        }

        function IsFormEmpty(page) {

            var isEmpty = true;
            $("#wrap-part" + page + " input[type=radio]").each(function () {
                var element = $(this);
                if (element.is(":checked")) {
                    isEmpty = false;
                    return;
                }
            });
            if (isEmpty == false) return isEmpty;
            $("#wrap-part" + page + " input[type=checkbox]").each(function () {
                var element = $(this);
                if (element.is(":checked")) {
                    isEmpty = false;
                    return;
                }
            });
            if (isEmpty == false) return isEmpty;
            $("#wrap-part" + page + " input[type=text]").each(function () {
                var element = $(this);
                if (element.val().length > 0) {
                    isEmpty = false;
                    return;
                }
            });

            $("#wrap-part" + page + " input[type=file]").each(function () {
                var element = $(this);
                if (element[0].files.length > 0) {
                    isEmpty = false;
                    return;
                }
            });

            return isEmpty;
        }
    </script>


    <script defer>
        $(function () {

            OnLoad();

            SaveFormData();

            window.onresize = ResizeCanvas;

            ResizeCanvas();

            EditForm();

            GoNext(1);

            $("input.other99").on('mousedown', function (e) {

                var $input = $(this).parent().parent().find('input');

                if ($input.is(':checked')) {

                }
                else {
                    e.preventDefault();
                }
            });

            $('input[type=radio]').on('change', function (e) {

                var name = $(this).attr('name');

                $(`input[type=radio][name^=${name}]`).each(function (i, o) {
                    var $o = $(o);
                    if ($o.is(':checked')) {

                    }
                    else {
                        var $text = $o.parent().find('.other99');
                        if ($text) {
                            $text.val('');
                        }
                    }
                });
            });

            $('input[type=checkbox]').on('change', function (e) {
                var $input = $(this);
                if (!$input.is(':checked')) {

                    var $text = $input.parent().find('.other99');
                    //var name = $input.attr('name');
                    //var $text = $('#' + name + 'text');
                    if ($text) {
                        $text.val('');
                    }
                }
            });

          
                       
            $("#qst131").on('change', function (e) {
                $("#parentSig").text($(this).val());
            });
            $("#qst131").trigger('change');

            $('.--sum,.--male,.--female').on('keyup', function () {
                var $p = $(this).parents('div:eq(0)');
                var $sum = $p.find('.--sum');
                var male = +($p.find('.--male').val());
                var female = +($p.find('.--female').val());

                $sum.val(male + female);
            });

            //if (jQuery.validator) {//.messages

            //    //jQuery.extend(jQuery.validator.messages, {
            //    //    required: "<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105028") %>",
            //    //});

            //    //$("#aspnetForm").validate({  // initialize the plugin

            //    //    errorPlacement: function (error, element) {
            //    //        let _class = element.attr('class');

            //    //        if (_class.includes('--req-append-last')) {
            //    //            error.insertAfter(element.parent());
            //    //        }
            //    //        else {
            //    //            error.insertAfter(element);
            //    //        }

            //    //    }

            //    //});
            //}
        });
    </script>




</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <p class="text-muted" style="font-size: small;">
                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305002") %>
            </p>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">

            <form runat="server" id="form1" enctype="multipart/form-data">

                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>
                <div class="card ">
                    <div class="card-header card-header-info card-header-icon">
                        <div class="card-icon">
                            <i class="material-icons">search</i>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01117") %> </h4>
                    </div>
                    <div id="cardbody1" class="card-body ">

                        <div class="d-flex align-items-top justify-content-between">
                            <div class="pl-8 my-3 d-flex align-items-start">

                                <%--<img src="<%= DataForm.Profile.Picture %>" alt="profile" class="mx-auto d-block">--%>
                                <span class="circle-profile mx-auto d-block" style="background-image: url('<%= string.IsNullOrEmpty(DataForm.Profile.Picture) ? "/Content/VisitHouse/assets/img/unknow"+DataForm.Profile.Sex+".jpg" : DataForm.Profile.Picture %>')"></span>
                                <div class="ml-4 col-8">
                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.FullName %></span>
                                        </div>
                                    </div>
                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M104024") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.NickName %></span>
                                        </div>
                                    </div>
                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107017") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.Code %></span>
                                        </div>
                                    </div>

                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107019") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.Level %></span>
                                        </div>
                                    </div>


                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M121068") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.Address %></span>
                                        </div>
                                    </div>

                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title "><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132941") %> : </h4>
                                        </div>
                                        <div class="col-8">
                                            <span class="font-normal"><%= DataForm.Profile.Mobile %></span>
                                        </div>
                                    </div>

                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title mt-2">Line : </h4>
                                        </div>
                                        <div class="col-8">
                                            <input type="text" id="Line" class="form-control " value="">
                                        </div>
                                    </div>

                                    <div class=" my-2 row">
                                        <div class="col-4">
                                            <h4 class="profile-title mt-2">Facebook : </h4>
                                        </div>
                                        <div class="col-8">
                                            <input type="text" id="Facebook" class="form-control" value="">
                                        </div>
                                    </div>
                                    <div class="w-100 mt-4">
                                        <div class="d-inline-block mb-3">
                                            <div class="m-icons">
                                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305004") %>
                                                <div class="m-icon-default mx-2"></div>
                                            </div>
                                        </div>
                                        <div class="d-inline-block mb-3">
                                            <div class="m-icons"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305006") %> <i class="material-icons m-icon-success mx-2">check</i></div>
                                        </div>
                                        <%-- <div class="d-inline-block mb-3">
                                        <div class="m-icons"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133546") %> <i class="material-icons text-success mx-2">check</i></div>
                                    </div>--%>
                                        <%--<div class="d-inline-block mb-3">
                                        <div class="m-icons"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133547") %> <i class="material-icons m-icon-success mx-2">hourglass_empty</i></div>
                                    </div>--%>
                                        <div class="d-inline-block mb-3">
                                            <div class="m-icons"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %> <i class="material-icons m-icon-danger mx-2">check</i></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="screen-check">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02085") %></h4>
                                <% if (DataForm.Visit.Status == 3)
                                    {  %>

                                <i class="material-icons m-icon-danger mx-2">check</i>
                                <h4 class="mt-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305005") %></h4>

                                <% }
                                    else if (DataForm.Visit.Status == 2)
                                    { %>
                                <i class="material-icons m-icon-success mx-2">check</i>
                                <h4 class="mt-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305006") %></h4>
                                <% }
                                    else
                                    {  %>
                                <div class="m-icon-default mx-2" style="width: 120px; height: 120px;"></div>
                                <h4 class="mt-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305004") %></h4>
                                <% } %>
                                <div class="mt-4">

                                    <% if (DataForm.Visit.Status >= 2)
                                        {  %>
                                    <%--<a href="PrintPreview.aspx?sId=<%=DataForm.Profile.sID %>" class="m-2" target="_blank">--%>
                                    <div class="row">
                                        <div class="col-6 p-0">
                                            <div class="dropdown">
                                                <a class=" dropdown-togglex" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <i class="material-icons in-icon bg-info ">file_download</i>
                                                </a>

                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                    <a class="dropdown-item" style="cursor:pointer;" onclick="window.open('PrintPreview.aspx?sId=<%=DataForm.Profile.sID %>&term=<%=DataForm.Profile.TermID%>', '_blank');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105043") %></a>
                                                    <a class="dropdown-item" style="cursor:pointer;" onclick="window.open('PrintPreview2.aspx?sId=<%=DataForm.Profile.sID %>&term=<%=DataForm.Profile.TermID%>', '_blank');"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105044") %></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 p-0">
                                            <%-- </a> --%>
                                            <i id="editForm" class="material-icons in-icon bg-info" onclick="IsEnableForm('true')">edit</i>
                                            <i id="lockForm" class="material-icons in-icon bg-success" style="display: none">edit</i>
                                        </div>
                                    </div>

                                    <% }
                                        else
                                        {  %>
                                    <i id="lockForm" class="material-icons in-icon bg-success m-2" style="">edit</i>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="card <%= (DataForm.Visit.Status >= 2 ? "disabled" : "") %>">
                    <div class="card-header card-header-primary card-header-icon">
                        <div class="card-icon">
                            <span class="material-icons">account_box</span>
                        </div>
                        <h4 class="card-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></h4>

                    </div>

                    <div class="card-body h-question">
                        <div id="wrap-part1" class="wrapper-part">
                            <div class=" mb-3">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107055") %></h4>
                                <button type="button" onclick="GoNext(2)" class="btn btn-success " style="pointer-events: auto !important; position: relative; right: 0; float: right; top: -50px;">
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                                    <span class="btn-label">
                                        <i class="material-icons">chevron_right</i>
                                    </span>
                                </button>
                            </div>
                            <h4 class="border-bottom w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00350") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></label>
                                <input type="checkbox" class="checkbox-round mx-2" disabled>
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02228") %></label>
                            </div>
                            <div class="d-flex align-items-center">
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></label>
                                <input type="checkbox" class="checkbox-square mx-2" disabled>
                                <label><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133477") %></label>
                            </div>
                            <div class="q-title">1.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101179") %></div>
                            <div class="d-flex align-items-center mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst111" name="qst11" value="1">
                                    <label for="qst111" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01338") %></label>
                                </div>
                                <div class="d-flex align-items-center mb-3 ml-5">
                                    <input type="radio" class="checkbox-round" id="qst112" name="qst11" value="0">
                                    <label for="qst112" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></label>
                                </div>
                            </div>
                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101192") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst121" name="qst12" value="1">
                                        <label for="qst121" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst122" name="qst12" value="2">
                                        <label for="qst122" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst123" name="qst12" value="3">
                                        <label for="qst123" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133478") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst124" name="qst12" value="4">
                                        <label for="qst124" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst125" name="qst12" value="99" data-has-other="1">
                                        <label for="qst125" class="ml-3">อื่น ๆ</label>
                                        <input type="text" id="qst12text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <div class="w-50">
                                <div class="d-flex align-items-center mb-3">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M105031") %></label>
                                    <input type="text" class="form-control ml-3" id="qst131" name="qst131">
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></label>
                                    <input type="text" class="form-control ml-3 onlynumber --tel" id="qst132" name="qst132">
                                </div>
                                <div class="align-items-center mb-3 d-none">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101284") %></label>
                                    <input type="text" class="form-control ml-3 onlynumber --idcard" id="qst133" name="qst133">
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst141" name="qst14" value="1">
                                        <label for="qst141" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305090") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst142" name="qst14" value="2">
                                        <label for="qst142" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01168") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst143" name="qst14" value="3">
                                        <label for="qst143" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01169") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst144" name="qst14" value="4">
                                        <label for="qst144" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00128") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst145" name="qst14" value="5">
                                        <label for="qst145" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00228") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst146" name="qst14" value="6">
                                        <label for="qst146" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01594") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst147" name="qst14" value="99" data-has-other="1">
                                        <label for="qst147" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qst14text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131287") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst151" name="qst15" value="1">
                                        <label for="qst151" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102054") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst152" name="qst15" value="2">
                                        <label for="qst152" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102055") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst153" name="qst15" value="3">
                                        <label for="qst153" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133524") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst154" name="qst15" value="4">
                                        <label for="qst154" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802034") %>/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M802035") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst155" name="qst15" value="5">
                                        <label for="qst155" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103103") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst156" name="qst15" value="6">
                                        <label for="qst156" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103104") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst157" name="qst15" value="7">
                                        <label for="qst157" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133527") %></label>
                                    </div>
                                </div>
                            </div>

                            <div class="d-flex align-items-center my-3">
                                <input type="checkbox" class="checkbox-square" id="qst16" name="qst16" value="1">
                                <label for="qst16" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00376") %></label>
                            </div>
                            <div class="q-title">2.<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305027") %></div>

                            <h4 class="mt-3">2.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305028") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew211" name="qnew21" value="1">
                                        <label for="qnew211" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305029") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew212" name="qnew21" value="2">
                                        <label for="qnew212" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101161") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew213" name="qnew21" value="3">
                                        <label for="qnew213" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305030") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew214" name="qnew21" value="4">
                                        <label for="qnew214" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101160") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew215" name="qnew21" value="5" data-has-other="1">
                                        <label for="qnew215" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305031") %></label>
                                        <input type="text" id="qnew21text5" class="form-control ml-3 other99">
                                    </div>
                                </div>
                                <br />
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew216" name="qnew21" value="99" data-has-other="1">
                                        <label for="qnew216" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qnew21text99" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>
                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305032") %></h4>
                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133488") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2211" name="qnew221" value="1">
                                        <label for="qnew2211" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2212" name="qnew221" value="2">
                                        <label for="qnew2212" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206543") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2213" name="qnew221" value="3">
                                        <label for="qnew2213" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305036") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2214" name="qnew221" value="4">
                                        <label for="qnew2214" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305037") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2215" name="qnew221" value="5">
                                        <label for="qnew2215" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305038") %></label>
                                    </div>
                                </div>
                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133489") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2221" name="qnew222" value="1">
                                        <label for="qnew2221" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305041") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2222" name="qnew222" value="2">
                                        <label for="qnew2222" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305042") %> </label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2223" name="qnew222" value="3">
                                        <label for="qnew2223" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305043") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2224" name="qnew222" value="99">
                                        <label for="qnew2224" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qnew222text99" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133490") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2231" name="qnew223" value="1">
                                        <label for="qnew2231" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2232" name="qnew223" value="0">
                                        <label for="qnew2232" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %> </label>
                                    </div>
                                </div>

                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133491") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2241" name="qnew224" value="1">
                                        <label for="qnew2241" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2242" name="qnew224" value="0">
                                        <label for="qnew2242" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %> </label>
                                    </div>
                                </div>

                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133492") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2251" name="qnew225" value="1">
                                        <label for="qnew2251" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew2252" name="qnew225" value="0">
                                        <label for="qnew2252" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %> </label>
                                    </div>
                                </div>

                            </div>
                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133493") %></h4>
                            <textarea class="mt-3 w-50" cols="30" rows="5" id="qnew226"></textarea>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133494") %></h4>
                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133495") %></h4>
                            <div class="mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></label>
                                    <input type="number" class="form-control ml-3 --sum" id="qnew2311" name="qnew2311">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></label>
                                    <input type="number" class="form-control ml-3 --male" id="qnew2312" name="qnew2312">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></label>
                                    <input type="number" class="form-control ml-3 --female" id="qnew2313" name="qnew2313">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></label>
                                </div>
                            </div>
                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133496") %></h4>
                            <div class="mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></label>
                                    <input type="number" class="form-control ml-3 --sum" id="qnew2321" name="qnew2321">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></label>
                                    <input type="number" class="form-control ml-3 --male" id="qnew2322" name="qnew2322">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></label>
                                    <input type="number" class="form-control ml-3 --female" id="qnew2323" name="qnew2323">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></label>
                                </div>
                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133497") %></h4>
                            <div class="mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></label>
                                    <input type="number" class="form-control ml-3 --sum" id="qnew2331" name="qnew2331">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101063") %></label>
                                    <input type="number" class="form-control ml-3 --male" id="qnew2332" name="qnew2332">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101064") %></label>
                                    <input type="number" class="form-control ml-3 --female" id="qnew2333" name="qnew2333">
                                    <label for="" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></label>
                                </div>
                            </div>

                            <div class="d-flex align-items-center mt-3">
                                <h4 class="mt-3 d-flex">2.3.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00024") %></h4>
                                <input type="number" class="form-control ml-3" id="qnew2341" name="qnew2341" />
                                <h4 for="" class="mt-3 ml-3 d-flex"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M405031") %></h4>
                                <input type="number" class="form-control ml-3" id="qnew2342" name="qnew2342" />
                                <h4 for="" class="mt-3 ml-3 d-flex"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101322") %></h4>
                            </div>

                            <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133499") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew241" name="qnew24" value="1">
                                        <label for="qnew241" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305053") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew242" name="qnew24" value="2">
                                        <label for="qnew242" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305054") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew243" name="qnew24" value="3">
                                        <label for="qnew243" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305055") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew244" name="qnew24" value="4">
                                        <label for="qnew244" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew245" name="qnew24" value="5">
                                        <label for="qnew245" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305057") %></label>
                                    </div>
                                </div>
                                <br />
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qnew246" name="qnew24" value="99">
                                        <label for="qnew246" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qnew24text99" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133500") %></h4>

                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2211" name="qst221" value="1">
                                                <label for="qst2211" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2212" name="qst221" value="2">
                                                <label for="qst2212" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2213" name="qst221" value="3">
                                                <label for="qst2213" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2214" name="qst221" value="4">
                                                <label for="qst2214" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2215" name="qst221" value="5">
                                                <label for="qst2215" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2221" name="qst222" value="1">
                                                <label for="qst2221" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2222" name="qst222" value="2">
                                                <label for="qst2222" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2223" name="qst222" value="3">
                                                <label for="qst2223" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2224" name="qst222" value="4">
                                                <label for="qst2224" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2225" name="qst222" value="5">
                                                <label for="qst2225" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305063") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2231" name="qst223" value="1">
                                                <label for="qst2231" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2232" name="qst223" value="2">
                                                <label for="qst2232" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2233" name="qst223" value="3">
                                                <label for="qst2233" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2234" name="qst223" value="4">
                                                <label for="qst2234" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2235" name="qst223" value="5">
                                                <label for="qst2235" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305064") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2241" name="qst224" value="1">
                                                <label for="qst2241" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2242" name="qst224" value="2">
                                                <label for="qst2242" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2243" name="qst224" value="3">
                                                <label for="qst2243" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2244" name="qst224" value="4">
                                                <label for="qst2244" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2245" name="qst224" value="5">
                                                <label for="qst2245" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305066") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2251" name="qst225" value="1">
                                                <label for="qst2251" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2252" name="qst225" value="2">
                                                <label for="qst2252" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2253" name="qst225" value="3">
                                                <label for="qst2253" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2254" name="qst225" value="4">
                                                <label for="qst2254" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2255" name="qst225" value="5">
                                                <label for="qst2255" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></h4>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2261" name="qst226" value="1">
                                                <label for="qst2261" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2262" name="qst226" value="2">
                                                <label for="qst2262" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2263" name="qst226" value="3">
                                                <label for="qst2263" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2264" name="qst226" value="4">
                                                <label for="qst2264" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2265" name="qst226" value="5">
                                                <label for="qst2265" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 p-0">
                                    <div class="d-flex align-items-center mb-3">
                                        <h4 class="mt-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %>  </h4>
                                        <input type="text" id="qst227text" class="form-control ml-3">
                                    </div>
                                </div>
                                <div class="col-sm-9 p-0">
                                    <div class="mt-3">
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2271" name="qst227" value="1">
                                                <label for="qst2271" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2272" name="qst227" value="2">
                                                <label for="qst2272" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2273" name="qst227" value="3">
                                                <label for="qst2273" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2274" name="qst227" value="4">
                                                <label for="qst2274" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></label>
                                            </div>
                                        </div>
                                        <div class="check-block">
                                            <div class="d-flex align-items-center mb-3">
                                                <input type="radio" class="checkbox-round" id="qst2275" name="qst227" value="5">
                                                <label for="qst2275" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="d-flex align-items-center my-3">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133501") %> </h4>
                                <input type="number" id="qst21text" class="form-control ml-3 " max="24">
                            </div>

                            <div class="d-flex align-items-center my-3">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133502") %></h4>
                                <input type="text" id="qnew27text" class="form-control ml-3 " min="0">
                            </div>

                            <div class="d-flex align-items-center my-3">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133503") %></h4>
                                <input type="text" id="qnew28text" class="form-control ml-3 " min="0">
                            </div>

                            <h4>2.9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00023") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst231" name="qst23" value="1">
                                        <label for="qst231" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst232" name="qst23" value="2">
                                        <label for="qst232" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305077") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst233" name="qst23" value="3">
                                        <label for="qst233" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00834") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst234" name="qst23" value="99" data-has-other="1">
                                        <label for="qst234" class="ml-3">อื่น ๆ</label>
                                        <input type="text" id="qst23text" class="form-control ml-3 other99">
                                    </div>
                                </div>

                            </div>
                            <h4>2.10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01730") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst241" name="qst24" value="1">
                                        <label for="qst241" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00741") %> 15,000 </label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst242" name="qst24" value="2">
                                        <label for="qst242" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305082") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst243" name="qst24" value="3">
                                        <label for="qst243" class="ml-3">20,001 -30,000</label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst244" name="qst24" value="4">
                                        <label for="qst244" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01319") %> 30,000</label>
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305086") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2511" name="qst251" value="1">
                                        <label for="qst2511" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2512" name="qst251" value="2">
                                        <label for="qst2512" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2513" name="qst251" value="3">
                                        <label for="qst2513" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2514" name="qst251" value="99" data-has-other="1">
                                        <label for="qst2514" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qst251text" class="form-control ml-3 other99">
                                    </div>
                                </div>

                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00820") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2521" name="qst252" value="1">
                                        <label for="qst2521" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305088") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2522" name="qst252" value="2">
                                        <label for="qst2522" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01191") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2523" name="qst252" value="3">
                                        <label for="qst2523" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305090") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2524" name="qst252" value="4">
                                        <label for="qst2524" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305091") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2525" name="qst252" value="99" data-has-other="1">
                                        <label for="qst2525" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qst252text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01734") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2531" name="qst253" value="1">
                                        <label for="qst2531" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305093") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2532" name="qst253" value="2">
                                        <label for="qst2532" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305094") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2533" name="qst253" value="3">
                                        <label for="qst2533" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305095") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2534" name="qst253" value="4">
                                        <label for="qst2534" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305096") %></label>
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00816") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2541" name="qst254" value="1">
                                        <label for="qst2541" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305098") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2542" name="qst254" value="2">
                                        <label for="qst2542" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305099") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst2543" name="qst254" value="3">
                                        <label for="qst2543" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305100") %></label>
                                    </div>
                                </div>
                            </div>

                            <%--  

                            <h4 class="mt-3">2.12 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00278") %></h4>
                            <textarea class="mt-3 w-50" cols="30" rows="5" id="qnew2_12text"></textarea>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133508") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_13_1" name="qnew2_13" value="1">
                                        <label for="qnew2_13_1" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_13_2" name="qnew2_13" value="2">
                                        <label for="qnew2_13_2" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00629") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_13_3" name="qnew2_13" value="3">
                                        <label for="qnew2_13_3" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></label>
                                    </div>
                                </div>                                 
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_13_5" name="qnew2_13" value="99" data-has-other="1">
                                        <label for="qnew2_13_5" class="ml-3"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qnew2_13text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>--%>

                            <%--  <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133506") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst271" name="qst27" value="1">
                                        <label for="qst271" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131246") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst272" name="qst27" value="2">
                                        <label for="qst272" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133548") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst273" name="qst27" value="99" data-has-other="1">
                                        <label for="qst273" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qst27text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>--%>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133506") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_11_1" name="qnew2_11" value="1">
                                        <label for="qnew2_11_1" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305103") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_11_2" name="qnew2_11" value="2">
                                        <label for="qnew2_11_2" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305104") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_11_3" name="qnew2_11" value="3">
                                        <label for="qnew2_11_3" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204043") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_11_4" name="qnew2_11" value="4">
                                        <label for="qnew2_11_4" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305106") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qnew2_11_5" name="qnew2_11" value="5" data-has-other="1">
                                        <label for="qnew2_11_5" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133541") %></label>
                                        <input type="text" id="qnew2_11text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133507") %></h4>
                            <textarea class="mt-3 w-50" cols="30" rows="5" id="qst28text"></textarea>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133508") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst261" name="qst26" value="1">
                                        <label for="qst261" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst262" name="qst26" value="2">
                                        <label for="qst262" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00629") %> </label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst263" name="qst26" value="3">
                                        <label for="qst263" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="checkbox" class="checkbox-square" id="qst264" name="qst26" value="99" data-has-other="1">
                                        <label for="qst26" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                        <input type="text" id="qst26text" class="form-control ml-3 other99">
                                    </div>
                                </div>
                            </div>


                            <div class="q-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305114") %></div>
                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133509") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst311" name="qst31" value="1">
                                <label for="qst311" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst312" name="qst31" value="2">
                                <label for="qst312" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305118") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst313" name="qst31" value="3">
                                <label for="qst313" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305119") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst314" name="qst31" value="4">
                                <label for="qst314" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305120") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst315" name="qst31" value="5">
                                <label for="qst315" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305121") %></label>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133510") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst321" name="qst32" value="1">
                                <label for="qst321" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01180") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst322" name="qst32" value="2">
                                <label for="qst322" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00796") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst323" name="qst32" value="3">
                                <label for="qst323" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01335") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst324" name="qst32" value="4">
                                <label for="qst324" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305128") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst325" name="qst32" value="5">
                                <label for="qst325" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305129") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst326" name="qst32" value="6">
                                <label for="qst326" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst327" name="qst32" value="7">
                                <label for="qst327" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst328" name="qst32" value="8">
                                <label for="qst328" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305132") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst329" name="qst32" value="9">
                                <label for="qst329" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305133") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3210" name="qst32" value="10">
                                <label for="qst3210" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305134") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3211" name="qst32" value="11">
                                <label for="qst3211" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305135") %></label>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133511") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3311" name="qst331" value="1">
                                        <label for="qst3311" class="ml-3">1-5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3312" name="qst331" value="2">
                                        <label for="qst3312" class="ml-3">6-10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3313" name="qst331" value="3">
                                        <label for="qst3313" class="ml-3">11-15 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %> </label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3314" name="qst331" value="4">
                                        <label for="qst3314" class="ml-3">16-20 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %> </label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3315" name="qst331" value="5">
                                        <label for="qst3315" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305142") %> </label>
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %></h4>
                            <div class="mt-3">
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3321" name="qst332" value="1">
                                        <label for="qst3321" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305144") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3322" name="qst332" value="2">
                                        <label for="qst3322" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305145") %></label>
                                    </div>
                                </div>
                                <div class="check-block">
                                    <div class="d-flex align-items-center mb-3">
                                        <input type="radio" class="checkbox-round" id="qst3323" name="qst332" value="3">
                                        <label for="qst3323" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305146") %> </label>
                                    </div>
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00091") %></h4>
                            <div class="mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3331" name="qst333" value="1">
                                    <label for="qst3331" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305149") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3332" name="qst333" value="2">
                                    <label for="qst3332" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305150") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3333" name="qst333" value="3">
                                    <label for="qst3333" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305151") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3334" name="qst333" value="4">
                                    <label for="qst3334" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305152") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3335" name="qst333" value="5">
                                    <label for="qst3335" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305153") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3336" name="qst333" value="6">
                                    <label for="qst3336" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305154") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3337" name="qst333" value="7">
                                    <label for="qst3337" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305155") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst3338" name="qst333" value="99" data-has-other="1">
                                    <label for="qst3338" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                    <input type="text" id="qst333text" class="form-control ml-3 other99">
                                </div>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133512") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst341" name="qst34" value="1">
                                <label for="qst341" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305158") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst342" name="qst34" value="2">
                                <label for="qst342" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305159") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst343" name="qst34" value="3">
                                <label for="qst343" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305160") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst344" name="qst34" value="4">
                                <label for="qst344" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02247") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst345" name="qst34" value="5">
                                <label for="qst345" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00302") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst346" name="qst34" value="99" data-has-other="1">
                                <label for="qst346" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                <input type="text" id="qst34text" class="form-control ml-3 other99">
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133513") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst351" name="qst35" value="1">
                                <label for="qst351" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305165") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst352" name="qst35" value="2">
                                <label for="qst352" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305166") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst353" name="qst35" value="3">
                                <label for="qst353" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305167") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst354" name="qst35" value="4">
                                <label for="qst354" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305168") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst355" name="qst35" value="5">
                                <label for="qst355" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305169") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst356" name="qst35" value="99" data-has-other="1">
                                <label for="qst356" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                <input type="text" id="qst35text" class="form-control ml-3 other99">
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133514") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst361" name="qst36" value="1">
                                <label for="qst361" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305172") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst362" name="qst36" value="2">
                                <label for="qst362" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305173") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst363" name="qst36" value="3">
                                <label for="qst363" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305174") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst364" name="qst36" value="4">
                                <label for="qst364" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01876") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst365" name="qst36" value="5">
                                <label for="qst365" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305176") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst366" name="qst36" value="6">
                                <label for="qst366" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305177") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst367" name="qst36" value="7">
                                <label for="qst367" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305178") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst368" name="qst36" value="8">
                                <label for="qst368" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305179") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst369" name="qst36" value="99" data-has-other="1">
                                <label for="qst369" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                <input type="text" id="qst36text" class="form-control ml-3 other99">
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133515") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst371" name="qst37" value="1">
                                <label for="qst371" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305184") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst372" name="qst37" value="2">
                                <label for="qst372" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02105") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst373" name="qst37" value="3">
                                <label for="qst373" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305186") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst374" name="qst37" value="4">
                                <label for="qst374" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305187") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst375" name="qst37" value="5">
                                <label for="qst375" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305188") %></label>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133516") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst381" name="qst38" value="1">
                                <label for="qst381" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305192") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst382" name="qst38" value="2">
                                <label for="qst382" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305193") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst383" name="qst38" value="3">
                                <label for="qst383" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305194") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst384" name="qst38" value="4">
                                <label for="qst384" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305195") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst385" name="qst38" value="5">
                                <label for="qst385" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305196") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst386" name="qst38" value="99" data-has-other="1">
                                <label for="qst386" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                <input type="text" id="qst38text" class="form-control ml-3 other99">
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133517") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst391" name="qst39" value="1">
                                <label for="qst391" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305200") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst392" name="qst39" value="2">
                                <label for="qst392" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305201") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst393" name="qst39" value="3">
                                <label for="qst393" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305202") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst394" name="qst39" value="4">
                                <label for="qst394" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305203") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst395" name="qst39" value="5">
                                <label for="qst395" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305204") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst396" name="qst39" value="6">
                                <label for="qst3106" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></label>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133518") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst3101" name="qst310" value="1">
                                <label for="qst3101" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305209") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3102" name="qst310" value="2">
                                <label for="qst3102" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305210") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3103" name="qst310" value="3">
                                <label for="qst3103" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305211") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3104" name="qst310" value="4">
                                <label for="qst3104" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305212") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3105" name="qst310" value="5">
                                <label for="qst3105" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01609") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3106" name="qst310" value="6">
                                <label for="qst3106" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305213") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3107" name="qst310" value="7">
                                <label for="qst3107" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305214") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3108" name="qst310" value="8">
                                <label for="qst3108" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305215") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3109" name="qst310" value="99" data-has-other="1">
                                <label for="qst3109" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></label>
                                <input type="text" id="qst310text" class="form-control ml-3 other99">
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133519") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="radio" class="checkbox-round" id="qst3111" name="qst311" value="1">
                                <label for="qst3111" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305219") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="radio" class="checkbox-round" id="qst3112" name="qst311" value="2">
                                <label for="qst3112" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305220") %></label>
                            </div>

                            <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133520") %></h4>
                            <div class="d-flex align-items-center mb-3">
                                <input type="checkbox" class="checkbox-square" id="qst3121" name="qst312" value="1">
                                <label for="qst3121" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305223") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <input type="checkbox" class="checkbox-square" id="qst3122" name="qst312" value="2">
                                <label for="qst3122" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305224") %></label>
                            </div>
                            <div class="d-flex align-items-center mb-3 ">
                                <h4 style="margin-right: 10px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133521") %></h4>
                                <select id="qst3131" class="selectpickerx form-control " style="width: 200px!important;">
                                    <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00061") %></option>
                                    <option value="1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></option>
                                    <option value="2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></option>
                                    <option value="3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01221") %></option>
                                    <option value="4"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01225") %></option>
                                    <option value="5"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00835") %></option>
                                    <option value="6"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %></option>
                                    <option value="7"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %></option>
                                    <option value="8"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01862") %></option>
                                    <option value="9"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131272") %></option>
                                    <option value="10"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %></option>
                                    <option value="11"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %></option>
                                    <option value="12"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01458") %></option>
                                    <option value="13"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00771") %></option>
                                    <option value="14"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00935") %></option>
                                    <option value="15"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01325") %></option>
                                </select>
                            </div>

                            <%--  <div class="d-flex align-items-center mb-3">
                               
                            </div>--%>
                        </div>

                        <div id="wrap-part2" class="wrapper-part" style="">

                            <div class=" mb-3">
                                <h4><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00186") %></h4>
                                <button type="button" onclick="GoNext(1)" class="btn btn-success " style="pointer-events: auto !important; position: relative; right: 0; float: right; top: -50px;">
                                    <span class="btn-label">
                                        <i class="material-icons">chevron_left</i>
                                    </span>
                                    <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00081") %>
                                </button>
                            </div>
                            <h4 class="border-bottom w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01305") %></h4>
                            <div class="mt-3">
                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst411" name="qst41" value="1">
                                    <label for="qst411" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00923") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst412" name="qst41" value="2">
                                    <label for="qst412" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst413" name="qst41" value="3">
                                    <label for="qst413" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131276") %></label>
                                </div>

                                <div class="d-flex align-items-center mb-3">
                                    <input type="radio" class="checkbox-round" id="qst414" name="qst41" value="4">
                                    <label for="qst414" class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133549") %></label>
                                </div>

                            </div>
                            <h4 class="border-bottomx w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01753") %> 1 : <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01306") %> </h4>
                            <input type="hidden" name="qstDelFile" id="qstDelFile" value="" />
                            <div class="mt-3">
                                <input type="file" id="qst42" name="qst42[]" accept="image/png, image/gif, image/jpeg, image/jpg" data-browse-on-zone-click="true" multiple data-msg-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106018") %>" />
                                <%--  <textarea  name="qst42file" id="qst42file" style="display:none" />--%>
                                <label style="color: red">* <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00409") %></label>
                            </div>
                            <div class="mt-3"></div>
                            <h4 class="border-bottomx w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01753") %> 2 : <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01304") %></h4>
                            <div class="mt-3">
                                <input type="file" id="qst43" name="qst43[]" accept="image/png, image/gif, image/jpeg, image/jpg" data-browse-on-zone-click="true" multiple data-msg-placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106018") %>" />
                                <%-- <textarea  name="qst43file" id="qst43file" style="display:none" />--%>
                                <label style="color: red">* <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00409") %></label>
                            </div>
                            <div class="mt-3"></div>
                            <h4 class="border-bottomx w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></h4>
                            <div class="mt-3">
                                <div class="wrapper-signature">
                                    <canvas id="signature-pad1" class="signature-pad"></canvas>
                                    <button type="button" onclick="ClearSigature(1)" class="btn btn-danger " style="position: absolute; bottom: 0; right: 0;">
                                        <span class="btn-label">
                                            <i class="material-icons">delete</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                    </button>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></label>
                                    <label class="ml-3"><%= DataForm.Profile.FullName %></label>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></label>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01958") %></label>
                                    <label class="ml-3"><%=  (DataForm.Visit.UpdatedDate ?? DateTime.Today).ToString("dd MMMM yyyy" , new System.Globalization.CultureInfo("th-TH")) %></label>
                                </div>
                            </div>
                            <h4 class="border-bottomx w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></h4>
                            <div class="mt-3">
                                <div class="wrapper-signature">
                                    <canvas id="signature-pad2" class="signature-pad"></canvas>
                                    <button type="button" onclick="ClearSigature(2)" class="btn btn-danger " style="position: absolute; bottom: 0; right: 0;">
                                        <span class="btn-label">
                                            <i class="material-icons">delete</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                    </button>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></label>
                                    <label class="ml-3" id="parentSig"></label>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106142") %></label>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01958") %></label>
                                    <label class="ml-3"><%=  (DataForm.Visit.UpdatedDate ?? DateTime.Today).ToString("dd MMMM yyyy" , new System.Globalization.CultureInfo("th-TH")) %></label>
                                </div>
                            </div>
                            <h4 class="border-bottomx w-fit-content"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00205") %></h4>
                            <div class="mt-3">
                                <div class="wrapper-signature">
                                    <canvas id="signature-pad3" class="signature-pad"></canvas>
                                    <button type="button" onclick="ClearSigature(3)" class="btn btn-danger " style="position: absolute; bottom: 0; right: 0;">
                                        <span class="btn-label">
                                            <i class="material-icons">delete</i>
                                        </span>
                                        <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101241") %>
                                    </button>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %></label>

                                    <select id="qstTeacher" class="selectpickerx form-control" style="width: 200px!important;">
                                        <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206002") %></option>
                                        <%foreach (var r in DataForm.TeacherList)%>
                                        <%{%>
                                        <option value="<%=r.Id %>"><%=r.Name %></option>
                                        <%} %>
                                    </select>

                                    <%--<label class="ml-3"><%= DataForm.Profile.Teacher %></label>--%>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102133") %></label>
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M210005") %></label>
                                </div>
                                <div class="d-flex align-items-center mb-3">
                                    <label class="ml-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01958") %></label>
                                    <label class="ml-3"><%= (DataForm.Visit.UpdatedDate ?? DateTime.Today).ToString("dd MMMM yyyy" , new System.Globalization.CultureInfo("th-TH")) %></label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--  <div class="d-none" >
                        <img id="imgsig1" src="<%= DataForm.Visit.StudentSignature %>"  >
                        <img id="imgsig2" src="<%= DataForm.Visit.ParentSignature %>"  >
                        <img id="imgsig3" src="<%= DataForm.Visit.TeacherSignature %>"  >
                    </div>--%>
                    <div class="row mt-2">
                        <div class="col-md-12 text-center">

                            <button id="btnPage2" type="button" onclick="GoNext(1)" class="btn btn-success " style="pointer-events: auto !important;">
                                <span class="btn-label">
                                    <i class="material-icons">chevron_left</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00081") %>
                            </button>

                            <button id="formsubmit1" type="submit" class="btn btn-success">
                                <span class="btn-label">
                                    <i class="material-icons">save</i>
                                </span>
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101125") %>
                            </button>

                            <button id="btnPage1" type="button" onclick="GoNext(2)" class="btn btn-success " style="pointer-events: auto !important;">
                                <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102243") %>
                            <span class="btn-label">
                                <i class="material-icons">chevron_right</i>
                            </span>
                            </button>
                        </div>
                    </div>
                    <div class="card-footer">
                    </div>
                </div>
            </form>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModalPopup" runat="server">
</asp:Content>
