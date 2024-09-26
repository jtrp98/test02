<%@ Page Language="C#" Title="School Bright / Login" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FingerprintPayment._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="images/School Bright logo only.png" sizes="16x16 32x32" type="image/png">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.0/css/bootstrap.min.css" />
    <!-- Fonts and icons -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <link rel="stylesheet" href="/Scripts/Easy-Customizable-Loading/jquery.mloading.css" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.14.0-beta3/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="/Content/Material/login.css?v=<%=DateTime.Now.Ticks%>" />


</head>
<body>
    <div class="container-fluid p-0">
        <div class="row g-0">
            <div class="d-lg-flex col-12 col-lg-7">
                <div id="carouselCircle" class="carousel carousel-dark slide w-100" data-bs-ride="carousel" style="display: table;">
                    <div class="carousel-indicators">
                        <% for (int i = 0; i < BANNER_LIST.Count; i++)
                            {%>
                        <button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="<%=i %>" class="rounded-circle <%=i == 0 ? "active" : "" %>" aria-current="true" aria-label="Slide <%= i %>"></button>
                        <%  } %>
                        <%--  <button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="0" class="rounded-circle active" aria-current="true" aria-label="Slide 1"></button>
                       <button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="1" class="rounded-circle" aria-label="Slide 2"></button>--%>
                        <%--<button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="1" class="rounded-circle" aria-label="Slide 2"></button>
                        <button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="2" class="rounded-circle" aria-label="Slide 3"></button>
                        <button type="button" data-bs-target="#carouselCircle" data-bs-slide-to="3" class="rounded-circle" aria-label="Slide 4"></button>--%>
                    </div>
                    <div class="carousel-inner" style="display: table-cell; vertical-align: middle;">
                        <% for (int i = 0; i < BANNER_LIST.Count; i++)
                            {%>
                        <div class="carousel-item <%=i == 0 ? "active" : "" %>" data-bs-interval="5000">
                            <img src="<%=BANNER_LIST[i] %>" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>
                        <%  } %>
                        <%-- <div class="carousel-item active" data-bs-interval="5000">
                            <img src="images/school-bright-app-v.7.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>
                         <div class="carousel-item" data-bs-interval="5000">
                            <img src="images/login-image-slide-2.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>--%>

                        <%--<div class="carousel-item active" data-bs-interval="5000">
                            <img src="images/login-image-slide-1.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>
                        <div class="carousel-item" data-bs-interval="5000">
                            <img src="images/login-image-slide-2.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>
                        <div class="carousel-item" data-bs-interval="5000">
                            <img src="images/login-image-slide-3.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>
                        <div class="carousel-item" data-bs-interval="5000">
                            <img src="images/login-image-slide-4.png" class="d-block w-100" style="height: 886px;" alt="..." />
                        </div>--%>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselCircle" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselCircle" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>
            <div class="col-12 col-lg-5">
                <div class="login d-flex align-items-center py-5">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-9 col-lg-8 mx-auto">
                                <div class="brand-wrapper text-center mb-4">
                                    <img src="/images/School Bright logo 1 storke222 shadow.png" alt="logo" class="logo" style="height: 90px;">
                                </div>
                                <h3 class="login-heading text-center mb-4 fw-bold text-secondary fs-4"> <%= FingerprintPayment.Resources.Resource.LoginTeacher %>  </h3>

                                <!-- Sign In Form -->
                                <form>
                                    <div class="form-floating mb-3">
                                        <span class="material-icons" style="position: absolute; margin: 13px 0px 0px 15px; z-index: 1;">school</span>
                                        <select id="sltSchool" class="form-select selectpicker rounded-pill login-input" data-live-search="true" title="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111101") %>" data-width="100%">
                                            <option value=""><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111101") %></option>
                                            <asp:Literal ID="ltrSchoolList" runat="server"></asp:Literal>
                                        </select>
                                        <label for="sltSchool"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M111101") %></label>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <input id="iptUsername" type="text" class="form-control rounded-pill login-input" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131003") %>">
                                        <label for="iptUsername"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %></label>
                                        <span class="material-icons" style="position: absolute; margin: -36px 0px 0px 15px;">account_circle</span>
                                    </div>
                                    <div class="form-floating mb-3">
                                        <input id="iptPassword" type="password" class="form-control rounded-pill login-input" placeholder="<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01511") %>">
                                        <label for="iptPassword"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01511") %></label>
                                        <span class="material-icons" style="position: absolute; margin: -36px 0px 0px 15px;">lock_outline</span>
                                    </div>
                                    <div class="d-grid">
                                        <button id="btnLogin" class="btn btn-lg btn-login text-uppercase fw-bold mb-2 fs-6 rounded-pill"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00237") %></button>
                                    </div>
                                </form>

                    

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="pt-4 pb-3">
            <div class="row gx-0 justify-content-center">
                <div class="col-6 col-sm-4  col-md-2 mx-2">
                    <h6><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01129") %></h6>
                    <ul class="nav flex-column">
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103149") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01554") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01567") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204001") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M203001") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01568") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206342") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01549") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01544") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01551") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01548") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01566") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M404003") %></a></li>
                    </ul>
                </div>

                <div class="col-6 col-sm-4 col-md-2 mx-2">
                    <h6><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01128") %></h6>
                    <ul class="nav flex-column">
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00068") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00069") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00378") %></a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02333") %></a></li>
                    </ul>
                </div>

                <div class="col-6 col-sm-4 col-md-2 mx-2">
                    <h6>Contact</h6>
                    <ul class="nav flex-column">
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url">Jabjai Corporation Co.,Ltd</a></li>
                        <li class="nav-item mb-2 d-flex">
                            <a class="nav-link p-0 text-light footer-url d-flex">
                                <span class="material-icons">home</span>
                                <p>
                                    708/13 Charansanitwong 3 soi<br>
                                    Charansanitwong Road, Wat Tha Phra,<br>
                                    Bangkok Yai, Bangkok 10600
                                </p>
                            </a>
                        </li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><span class="material-icons">call</span> 02-096-2550</a></li>
                        <li class="nav-item mb-2"><a class="nav-link p-0 text-light footer-url"><span class="material-icons">email</span> cs@schoolbright.co</a></li>
                    </ul>
                </div>

                <div class="col-6 col-sm-4 col-md-2 ms-4">
                    <h6>Follow Us</h6>
                    <ul class="nav flex-column">
                        <li class="nav-item mb-2 d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class="bi bi-facebook" viewBox="0 0 16 16">
                                <path d="M16 8.049c0-4.446-3.582-8.05-8-8.05C3.58 0-.002 3.603-.002 8.05c0 4.017 2.926 7.347 6.75 7.951v-5.625h-2.03V8.05H6.75V6.275c<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305069") %>.017 1.195-3.131 3.022-3.131.876 0 1.791.157 1.791.157v1.98h-1.009c-.993 0-1.303.621-1.303 1.258v1.51h2.218l-.354 2.326H9.25V16c3.824-.604 6.75-3.934 6.75-7.951z"></path>
                            </svg><a href="https://www.facebook.com/jabjaithailand/" class="nav-link p-0 text-light footer-url">Facebook</a></li>
                        <li class="nav-item mb-2 d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class="bi bi-youtube" viewBox="0 0 16 16">
                                <path d="M8.051 1.999h.089c.822.003 4.987.033 6.11.335a2.01 2.01 0 0 1 1.415 1.42c.101.38.172.883.22 1.402l.01.104.022.26.008.104c.065.914.073 1.77.074 1.957v.075c-.001.194-.01 1.108-.082 2.06l-.008.105-.009.104c-.05.572-.124 1.14-.235 1.558a2.007 2.007 0 0 1-1.415 1.42c-1.16.312-5.569.334-6.18.335h-.142c-.309 0-1.587-.006-2.927-.052l-.17-.006-.087-.004-.171-.007-.171-.007c-1.11-.049-2.167-.128-2.654-.26a2.007 2.007 0 0 1-1.415-1.419c-.111-.417-.185-.986-.235-1.558L.09 9.82l-.008-.104A31.4 31.4 0 0 1 0 7.68v-.123c.002-.215.01-.958.064-1.778l.007-.103.003-.052.008-.104.022-.26.01-.104c.048-.519.119-1.023.22-1.402a2.007 2.007 0 0 1 1.415-1.42c.487-.13 1.544-.21 2.654-.26l.17-.007.172-.006.086-.003.171-.007A99.788 99.788 0 0 1 7.858 2h.193zM6.4 5.209v4.818l4.157-2.408L6.4 5.209z" />
                            </svg><a href="https://www.youtube.com/channel/UCiKXsX7k-5ISHvgQI26jPSg" class="nav-link p-0 text-light footer-url">Youtube</a></li>
                        <li class="nav-item mb-2 d-flex">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class="bi bi-line" viewBox="0 0 16 16">
                                <path d="M8 0c4.411 0 8 2.912 8 6.492 0 1.433-.555 2.723-1.715 3.994-1.678 1.932-5.431 4.285-6.285 4.645-.83.35-.734-.197-.696-.413l.003-.018.114-.685c.027-.204.055-.521-.026-.723-.09-.223-.444-.339-.704-.395C2.846 12.39 0 9.701 0 6.492 0 2.912 3.59 0 8 0ZM5.022 7.686H3.497V4.918a.156.156 0 0 0-.155-.156H2.78a.156.156 0 0 0-.156.156v3.486c0 .041.017.08.044.107v.001l.002.002.002.002a.154.154 0 0 0 .108.043h2.242c.086 0 .155-.07.155-.156v-.56a.156.156 0 0 0-.155-.157Zm.791-2.924a.156.156 0 0 0-.156.156v3.486c0 .086.07.155.156.155h.562c.086 0 .155-.07.155-.155V4.918a.156.156 0 0 0-.155-.156h-.562Zm3.863 0a.156.156 0 0 0-.156.156v2.07L7.923 4.832a.17.17 0 0 0-.013-.015v-.001a.139.139 0 0 0-.01-.01l-.003-.003a.092.092 0 0 0-.011-.009h-.001L7.88 4.79l-.003-.002a.029.029 0 0 0-.005-.003l-.008-.005h-.002l-.003-.002-.01-.004-.004-.002a.093.093 0 0 0-.01-.003h-.002l-.003-.001-.009-.002h-.006l-.003-.001h-.004l-.002-.001h-.574a.156.156 0 0 0-.156.155v3.486c0 .086.07.155.156.155h.56c.087 0 .157-.07.157-.155v-2.07l1.6 2.16a.154.154 0 0 0 .039.038l.001.001.01.006.004.002a.066.066 0 0 0 .008.004l.007.003.005.002a.168.168 0 0 0 .01.003h.003a.155.155 0 0 0 .04.006h.56c.087 0 .157-.07.157-.155V4.918a.156.156 0 0 0-.156-.156h-.561Zm3.815.717v-.56a.156.156 0 0 0-.155-.157h-2.242a.155.155 0 0 0-.108.044h-.001l-.001.002-.002.003a.155.155 0 0 0-.044.107v3.486c0 .041.017.08.044.107l.002.003.002.002a.155.155 0 0 0 .108.043h2.242c.086 0 .155-.07.155-.156v-.56a.156.156 0 0 0-.155-.157H11.81v-.589h1.525c.086 0 .155-.07.155-.156v-.56a.156.156 0 0 0-.155-.157H11.81v-.589h1.525c.086 0 .155-.07.155-.156Z" />
                            </svg><a href="https://line.me/R/ti/p/%40jabjai" class="nav-link p-0 text-light footer-url">Line</a></li>
                    </ul>
                </div>
            </div>

            <div class="py-4 mx-5 mt-4 border-top text-center copyrights">
                <p>Copyrights © 2021 All Rights Reserved by Jabjai Corporation Co.,Ltd. <a href="privacypolicy.html" target="_blank">Privacy Policy</a></p>
            </div>
        </footer>
    </div>

    <%--<div id="modalAlert" class="modal fade alertBoxInfo" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" style="top: 150px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="modal-close" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 style="font-family: thaifont; font-size: 30px; text-align: center; font-weight: bold" class="modal-title" id="modal-header"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %></h4>
                </div>
                <div class="modal-body" id="modal-content" style="padding: 40px 10px; text-align: center; font-size: 28px; font-weight: normal; font-family: thaifont;">
                </div>
                <div class="modal-footer" style="display: block; text-align: center;">
                    <button type="button" id="modal-cancel" class="btn btn-danger" style="font-size: 16px !important" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>--%>

    <div id="modalAlert" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-md" style="top: 150px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    <h4 class="modal-title text-center"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %></h4>
                </div>
                <div class="modal-body" id="modal-content">
                </div>
                <div class="modal-footer text-center">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.1/jquery.min.js" integrity="sha512-aVKKRRi/Q/YV+4mjoKBsE4x3H+BkegoM/em46NNlCqNTmUYADjBbeNefNxYV7giUp0VxICtqdrbqU7iVaeZNXA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.0/js/bootstrap.bundle.min.js"></script>
    <script src="/Scripts/Easy-Customizable-Loading/jquery.mloading.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.14.0-beta3/js/bootstrap-select.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/js-cookie/2.2.1/js.cookie.min.js"></script>

    <script>

        function getParameterByName(name, url = window.location.href) {
            name = name.replace(/[\[\]]/g, '\\$&');
            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, ' '));
        }

        $(document).ready(function () {
            $("#iptPassword").keydown(function (e) {
                if (e.keyCode == 13) {
                    Login();
                }
            });

            $("#btnLogin").click(function (e) {
                e.preventDefault();
                Login();
            });
            $("#btnLangEng").click(function (e) {
                e.preventDefault();
                SetLanguage();
            });
            $("#btnLangTH").click(function (e) {
                e.preventDefault();
                SetLanguage();
            });
            $('#sltSchool').selectpicker();
            $("#sltSchool").selectpicker('val', Cookies.get('schoolSelected') || '');

        });

        function SetLanguage() {
            $.ajax({
                type: "POST",
                url: 'Default.aspx/SetLanguage',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Handle success if needed
                    console.log("Server method called successfully.");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    // Handle error if needed
                    console.log("Error calling server method.");
                }
            });
        }

        function Login() {

            //$("#modal-content").html("<h1><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111104') %>ที่เคา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><br/><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701067") %> School Bright จะ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206010") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801046") %>ทุกส่วนงาน<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133177") %> 12-14 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107044") %> 2564 เ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ื่อให้<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106043") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ะสิทธิภา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %>ที่<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206542") %>ยิ่ง<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132170") %>ึ้น<br/><br/>ด้วยความเคา<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132094") %><br/>" +
            //    "บ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ิษัท จับจ่าย คอ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>์<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132093") %>อเ<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206415") %>ชั่น จำกัด<br/><br/>" +
            //    "Dear Users<br/><br/>" +
            //    "Please be adviced that School Bright will be conducting system maintenance from 12-14 April 2020. All services will not be available.<br/><br/>" +
            //    "We apologize for any inconvenience caused.<br/><br/>" +
            //    "Jabjai Corporation Co., Ltd." +
            //    "</h1>");
            //$("#modalAlert").modal();
            //return;

            var schoolId = $("#sltSchool").val();
            var username = $("#iptUsername").val();
            var password = $("#iptPassword").val();
            if (schoolId == "") {
                $("#modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03001') %>");
                $("#modalAlert").modal('show');
            }
            else if (username == "") {
                $("#modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03002') %>");
                $("#modalAlert").modal('show');
            }
            else if (password == "") {
                $("#modal-header").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03000') %>");
                $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03003') %>");
                $("#modalAlert").modal('show');
            }
            else {
                $(".login-input").prop("disabled", "disabled");
                $("body").mLoading();
                $.ajax({
                    async: false,
                    type: "POST",
                    url: 'Default.aspx/Login',
                    data: '{schoolId: ' + schoolId + ', username: \'' + username + '\', password: \'' + password + '\'}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: onSuccess,
                    error: onError
                });
            }

            Cookies.set('schoolSelected', schoolId);
        }

        function onSuccess(response) {
            var r = JSON.parse(response.d);
            switch (r.StatusCode) {
                case "200":
                    var returnUrl = getParameterByName('returnUrl');
                    window.location.href = returnUrl || "/AdminMain.aspx";
                    break;
                case "501":
                    $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03004') %><br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M03005') %>");
                    $("#modalAlert").modal('show');
                    $("#iptPassword").val("");
                    $(".login-input").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                case "502":
                    $("#modal-content").html(r.Message);
                    $("#modalAlert").modal('show');
                    $("#iptPassword").val("");
                    $(".login-input").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                case "404":
                    $("#modal-content").html("ท่านไม่มีสิทธิในการเข้าใช้ระบบ กรุณาติดต่อผู้ดูแลระบบโรงเรียนท่านเพื่อทำการเปิดสิทธิ");
                    $("#modalAlert").modal('show');
                    $("#btnLogin").removeClass("disabled");
                    $(".login-input").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                case "405":
                    $("#modal-content").html("<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111103') %><br/>Oops! Your school account has been temporarily suspended, please contact your school administrator to resolve the issue.");
                    $("#modalAlert").modal('show');
                    $("#btnLogin").removeClass("disabled");
                    $(".login-input").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
                default:
                    $("#modal-content").html("<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111104') %><br/>" +
                        "<p><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111105') %>" +

                        "<br/><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString('M111106') %></p>");
                    $("#modalAlert").modal('show');
                    $("#iptPassword").val("");
                    $(".login-input").prop("disabled", "");
                    $("body").mLoading('hide');
                    break;
            }
        }

        function onError(xhr, errorType, exception) {

            $("#iptPassword").val("");
            $(".login-input").prop("disabled", "");
            $("body").mLoading('hide');

            var responseText;
            try {
                responseText = jQuery.parseJSON(xhr.responseText);
                var errorMessage = "[" + errorType + ", " + exception + "] Exception:" + responseText.ExceptionType + ", StackTrace:" + responseText.StackTrace + ", Message:" + responseText.Message;

                $("#modal-content").html(errorMessage);
                $("#modalAlert").modal('show');
            } catch (e) {
                responseText = xhr.responseText;

                $("#modal-content").html(errorMessage);
                $("#modalAlert").modal('show');
            }
        }

    </script>
</body>
</html>
