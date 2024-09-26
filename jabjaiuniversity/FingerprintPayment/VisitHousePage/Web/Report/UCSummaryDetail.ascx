<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSummaryDetail.ascx.cs" Inherits="FingerprintPayment.VisitHousePage.Web.Report.UCSummaryDetail" %>


<div class="col-md-12 " style="">
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 1/8</span>
            <br />
            <p class="font-weight-bold text-center">แบบสรุปผลการเยี่ยมบ้านนักเรียน</p>
            <p class="font-weight-bold text-center --all">ทั้งโรงเรียน</p>
            <p class="font-weight-bold">ผลการเยี่ยมบ้านนักเรียน สรุปผลดังนี้</p>
            <p class="font-weight-bold">สภาพการเยี่ยมบ้านนักเรียน</p>
            <p class="font-weight-bold">1.จำนวนนักเรียนที่ออกเยี่ยมบ้าน</p>
            <p class="font-weight-bold">ตารางที่ 1 จำนวนนักเรียนที่ออกเยี่ยมบ้าน</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">รายการ</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">นักเรียนชาย</td>
                        <td style="text-align: center"><%= ModelData.CountAllMale %></td>
                        <td style="text-align: center"><%= ModelData.CountAllMalePercent.ToString("0.##")  %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">นักเรียนหญิง</td>
                        <td style="text-align: center"><%= ModelData.CountAllFemale %></td>
                        <td style="text-align: center"><%= ModelData.CountAllFemalePercent.ToString("0.##")  %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">รวมนักเรียนทั้งหมด</td>
                        <td style="text-align: center"><%= ModelData.CountAll %></td>
                        <td style="text-align: center">100</td>
                    </tr>

                    <tr>
                        <td colspan="3" style="text-align: center">จำนวนนักเรียนที่ได้ออกเยี่ยมบ้าน</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">นักเรียนชาย</td>
                        <td style="text-align: center"><%= ModelData.CountVisitMale %></td>
                        <td style="text-align: center"><%= ModelData.CountVisitMalePercent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">นักเรียนหญิง</td>
                        <td style="text-align: center"><%= ModelData.CountVisitFemale %></td>
                        <td style="text-align: center"><%= ModelData.CountVisitFemalePercent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">รวมนักเรียนที่ออกเยี่ยมบ้านทั้งหมด</td>
                        <td style="text-align: center"><%= ModelData.CountVisitMale + ModelData.CountVisitFemale %></td>
                        <td style="text-align: center"><%= ModelData.CountVisitPercent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">นักเรียนที่ยังไม่ได้เยี่ยม</td>
                        <td style="text-align: center"><%= ModelData.CountNotVisit %></td>
                        <td style="text-align: center"><%= ModelData.CountNotVisitPercent.ToString("0.##") %></td>
                    </tr>
                </tbody>

            </table>

            <p class="font-weight-bold">2.บ้านที่พักอาศัย/ความสัมพันธ์ในครอบครัว</p>
            <p class="font-weight-bold">ตารางที่ 2.1 บ้านที่พักอาศัย</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">บ้านที่พักอาศัย</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">บ้านของตนเอง</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บ้านเช่า</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อาศัยอยู่กับผู้อื่น</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_03 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_03Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บ้านญาติ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_04 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_04Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">หอพักอาศัยอยู่กับ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_05 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_05Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_99 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew21_99Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>

            </table>

            <p class="font-weight-bold">2.2 สภาพแวดล้อมที่ออยู่อาศัย</p>
            <p class="font-weight-bold">ตารางที่ 2.2.1 บ้านของตนเอง</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">บ้านของตนเอง</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ดี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">พอใช้</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เก่าทรุดโทรม</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_03 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_03Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">พื้นที่คับแคบ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_04 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_04Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มีความเป็นสัดส่วน</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_05 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_1_05Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>

            </table>


            <p class="font-weight-bold">ตารางที่ 2.2.2 ความสะอาด</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ความสะอาด</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สะอาดมีระเบียบ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่ค่อยสะอาด</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">สกปรกไม่มีระเบียบ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_03 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_03Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_04 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_2_04Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>

            </table>

            <p class="font-weight-bold">ตารางที่ 2.2.3 สาธารณูปโภค ไฟฟ้า</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">สาธารณูปโภค ไฟฟ้า</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_3_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_3_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_3_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_3_02Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>



        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 2/8</span>
            <br />

            <p class="font-weight-bold">ตารางที่ 2.2.4 น้ำเพื่อให้อุปโภค/บริโภค</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">น้ำเพื่อให้อุปโภค/บริโภค</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_4_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_4_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_4_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_4_02Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <p class="font-weight-bold">ตารางที่ 2.2.5 ห้องสุขา</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ห้องสุขา</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_5_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_5_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_5_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew22_5_02Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <p class="font-weight-bold">ตารางที่ 2.4 ความสัมพันธ์ของสมาชิกในครอบครัว</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ความสัมพันธ์ของสมาชิกในครอบครัว</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">รักใคร่กันดี</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_01 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้งทะเลาะกันบางครั้ง</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_02 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้งทะเลาะกันบ่อยครั้ง</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_03 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_03Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างเหิน</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_04 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_04Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้งและทำร้ายร่างกายบางครั้ง</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_05 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_05Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_99 %></td>
                        <td style="text-align: center"><%= ModelData.ChoiseNew2_4_99Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>

            </table>

            <p class="font-weight-bold">ตารางที่ 2.5 ความสัมพันธ์ของสมาชิกในครอบครัว</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">บิดา</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R11 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R11Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R12 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R12Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R13 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R13Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R14 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R14Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R15 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R15Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">มารดา</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R21 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R21Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R22 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R22Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R23 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R23Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R24 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R24Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R25 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R25Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">พี่ชาย/น้องชาย</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R31 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R31Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R32 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R32Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R33 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R33Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R34 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R34Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R35 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R35Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">พี่สาว/น้องสาว</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R41 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R41Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R42 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R42Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R43 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R43Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R44 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R44Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R45 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R45Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 3/8</span>
            <br />
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ปู่/ย่า/ตา/ยาย</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R51 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R51Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R52 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R52Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R53 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R53Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R54 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R54Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R55 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R55Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ญาติ</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R61 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R61Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R62 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R62Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R63 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R63Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R64 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R64Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R65 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R65Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%" style="">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">อื่นๆ</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">สนิทสนม</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R991 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R991Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เฉยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R992 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R992Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห่างๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R993 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R993Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขัดแย้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R994 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R994Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มี</td>
                        <td style="text-align: center"><%= ModelData.Choise22_R995 %></td>
                        <td style="text-align: center"><%= ModelData.Choise22_R995Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>


            <p class="font-weight-bold">ตารางที่ 2.6 สมาชิกในครอบครัวมีเวลาอยู่ด้วยกันกี่ชมต่อวัน</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">จำนวนชั่วโมง</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">0-2</td>
                        <td style="text-align: center"><%= ModelData.Choise21_02 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">3-4</td>
                        <td style="text-align: center"><%= ModelData.Choise21_34 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_34Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">5-6</td>
                        <td style="text-align: center"><%= ModelData.Choise21_56 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_56Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">7-8</td>
                        <td style="text-align: center"><%= ModelData.Choise21_78 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_78Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">9-10</td>
                        <td style="text-align: center"><%= ModelData.Choise21_810 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_810Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มากกว่า 10 ชมต่อวัน</td>
                        <td style="text-align: center"><%= ModelData.Choise21_10 %></td>
                        <td style="text-align: center"><%= ModelData.Choise21_10Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>

            </table>

            <p class="font-weight-bold">ตารางที่ 2.9 กรณีที่ผู้ปกครองไม่อยู่บ้านฝากเด็กนักเรียนอยู่บ้านกับใคร</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ผู้ปกครองไม่อยู่บ้านฝากเด็กนักเรียนอยู่บ้านกับใคร</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ญาติ</td>
                        <td style="text-align: center"><%= ModelData.Choise23_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise23_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เพื่อนบ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise23_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise23_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อยู่ด้วยตนเอง</td>
                        <td style="text-align: center"><%= ModelData.Choise23_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise23_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise23_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise23_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 2.10 รายได้รวมของบิดา-มารดาและผู้ปกครอง/เดือน</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">รายได้รวมของบดิา-มารดาและผู้ปกครอง/เดือน </th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ต่ำกว่า15,000 </td>
                        <td style="text-align: center"><%= ModelData.Choise24_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise24_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">15,000-20,000</td>
                        <td style="text-align: center"><%= ModelData.Choise24_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise24_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">20,001-30,000</td>
                        <td style="text-align: center"><%= ModelData.Choise24_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise24_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มากกว่า30,000 </td>
                        <td style="text-align: center"><%= ModelData.Choise24_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise24_4Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <%--   <p class="font-weight-bold">ตาราง นักเรียนได้รับค่าใช้จ่ายจาก</p>--%>
        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 4/8</span>
            <br />
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">นักเรียนได้รับค่าใช้จ่ายจาก</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">บิดา</td>
                        <td style="text-align: center"><%= ModelData.Choise251_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise251_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มารดา</td>
                        <td style="text-align: center"><%= ModelData.Choise251_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise251_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ญาติ</td>
                        <td style="text-align: center"><%= ModelData.Choise251_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise251_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise251_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise251_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">นักเรียนทํางานหารายได้(อาชีพ)</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่มีอาชีพเสริม</td>
                        <td style="text-align: center"><%= ModelData.Choise252_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise252_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">พารท์ไทม์ </td>
                        <td style="text-align: center"><%= ModelData.Choise252_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise252_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ธุรกิจส่วนตัว</td>
                        <td style="text-align: center"><%= ModelData.Choise252_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise252_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อาชีพอิสระ</td>
                        <td style="text-align: center"><%= ModelData.Choise252_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise252_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise252_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise252_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">รายได้วันละ(บาท)</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่มีรายได้</td>
                        <td style="text-align: center"><%= ModelData.Choise253_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise253_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ต่ำกว่า 100 บาท </td>
                        <td style="text-align: center"><%= ModelData.Choise253_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise253_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">100 - 300 บาท</td>
                        <td style="text-align: center"><%= ModelData.Choise253_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise253_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มากกว่า 300 บาท</td>
                        <td style="text-align: center"><%= ModelData.Choise253_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise253_4Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">นักเรียนได้เงินมาโรงเรียนวันละ(บาท)</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ต่ำกว่า 50 บาท</td>
                        <td style="text-align: center"><%= ModelData.Choise254_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise254_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">50 - 100 บาท</td>
                        <td style="text-align: center"><%= ModelData.Choise254_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise254_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มากกว่า 100 บาท</td>
                        <td style="text-align: center"><%= ModelData.Choise254_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise254_3Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <p class="font-weight-bold">ตารางที่ 2.11 ความช่วยเหลือที่ครอบครัวเคยได้รับจากหน่วยงานหรือต้องการได้รับความช่วยเหลือ</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ความช่วยเหลือที่ครอบครัวเคยได้รับจากหน่วยงาน</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">มากที่สุด</td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_01 %></td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_01Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มาก</td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_02 %></td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_02Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ปานกลาง</td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_03 %></td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_03Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">น้อย</td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_04 %></td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_04Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่จำเป็น</td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise2_11_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <p class="font-weight-bold">ตารางที่ 2.13 สิ่งที่ผู้ปกครองต้องการให้โรงเรียนช่วยเหลือนักเรียน</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ผู้ปกครองต้องการให้โรงเรียนช่วยเหลือนักเรียน</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ด้านการเรียน</td>
                        <td style="text-align: center"><%= ModelData.Choise26_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise26_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ด้านเศรษฐกิจ(เช่นขอรับทุน)</td>
                        <td style="text-align: center"><%= ModelData.Choise26_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise26_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ด้านพฤติกรรม</td>
                        <td style="text-align: center"><%= ModelData.Choise26_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise26_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise26_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise26_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>



        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 5/8</span>
            <br />

            <p class="font-weight-bold">3.พฤติกรรมและความเสี่ยง</p>
            <p class="font-weight-bold">ตาราง 3.1 สุขภาพ</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">สุขภาพ</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">ร่างกายไม่แข็งแรง</td>
                        <td style="text-align: center"><%= ModelData.Choise31_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีโรคประจำตัวหรือเจ็บป่วยบ่อย</td>
                        <td style="text-align: center"><%= ModelData.Choise31_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีภาวะทุพโภชนาการ</td>
                        <td style="text-align: center"><%= ModelData.Choise31_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ป่วยเป็นโรคร้ายแรง/เรื้อรัง</td>
                        <td style="text-align: center"><%= ModelData.Choise31_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">สมรรถภาพทางร่างกายต่ำ</td>
                        <td style="text-align: center"><%= ModelData.Choise31_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">สุขภาพแข็งแรง</td>
                        <td style="text-align: center"><%= ModelData.Choise31_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise31_NormalPercent.ToString("0.##") %></td>
                        <%--  <td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise31_1 + ModelData.Choise31_2 + ModelData.Choise31_3 +ModelData.Choise31_4+ ModelData.Choise31_5)  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise31_1Percent + ModelData.Choise31_2Percent + ModelData.Choise31_3Percent +ModelData.Choise31_4Percent+ ModelData.Choise31_5Percent)).ToString("0.##")  %></td>--%>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.2 สถานะภาพความเป็นอยู่ของนักเรียน</p>
            <table class=" table-hover  table-bordered" width="100%">

                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">สถานะภาพความเป็นอยู่ของนักเรียน</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td style="text-align: left">พ่อแม่แยกทางกันหรือแต่งงานใหม่</td>
                        <td style="text-align: center"><%= ModelData.Choise32_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ที่พักอาศัยในชุมชนแออัดหรือใกล้แหล่งมั่วสุม/สถานเริง
รมย์</td>
                        <td style="text-align: center"><%= ModelData.Choise32_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีบุคคลในครอบครัวเจ็บป่วยด้วยโรคร้ายแรง/เรื้อรัง/
ติดต่อ</td>
                        <td style="text-align: center"><%= ModelData.Choise32_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บุคคลในครอบครัวติดสารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise32_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บุคคลในครอบครัวเล่นการพนัน</td>
                        <td style="text-align: center"><%= ModelData.Choise32_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีความขัดแย้ง/ทะเลาะกันในครอบครัว</td>
                        <td style="text-align: center"><%= ModelData.Choise32_6 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_6Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่มีผู้ดูแล</td>
                        <td style="text-align: center"><%= ModelData.Choise32_7 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_7Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีความขัดแย้งและมีการใช้ความรุนแรงในครอบครัว</td>
                        <td style="text-align: center"><%= ModelData.Choise32_8 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_8Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ถูกทารุณ/ทำร้ายจากบุคคลในครอบครัว/เพื่อนบ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise32_9 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_9Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ถูกล่วงละเมิดทางเพศ</td>
                        <td style="text-align: center"><%= ModelData.Choise32_10 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_10Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เล่นการพนัน</td>
                        <td style="text-align: center"><%= ModelData.Choise32_11 %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_11Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ปกติ</td>
                        <td style="text-align: center"><%= ModelData.Choise32_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise32_NormalPercent.ToString("0.##") %></td>
                        <%--  <td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise32_1 + ModelData.Choise32_2 + ModelData.Choise32_3 + ModelData.Choise32_4 + ModelData.Choise32_5 + ModelData.Choise32_6 + ModelData.Choise32_7 + ModelData.Choise32_8 + ModelData.Choise32_9 + ModelData.Choise32_10 + ModelData.Choise32_11 )  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise32_1Percent + ModelData.Choise32_2Percent + ModelData.Choise32_3Percent + ModelData.Choise32_4Percent + ModelData.Choise32_5Percent + ModelData.Choise32_6Percent + ModelData.Choise32_7Percent + ModelData.Choise32_8Percent + ModelData.Choise32_9Percent + ModelData.Choise32_10Percent + ModelData.Choise32_11Percent )).ToString("0.##")  %></td>--%>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.3 ระยะทางระหว่างบ้านไปโรงเรียน(ไป/กลับ)</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ระยะทางระหว่างบ้านไปโรงเรียน(ไป/กลับ)</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">1 - 5 กิโลเมตร</td>
                        <td style="text-align: center"><%= ModelData.Choise331_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise331_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">6 - 10 กิโลเมตร</td>
                        <td style="text-align: center"><%= ModelData.Choise331_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise331_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">11 - 15 กิโลเมตร</td>
                        <td style="text-align: center"><%= ModelData.Choise331_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise331_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">16 - 20 กิโลเมตร</td>
                        <td style="text-align: center"><%= ModelData.Choise331_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise331_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">20 กิโลเมตรขึ้นไป</td>
                        <td style="text-align: center"><%= ModelData.Choise331_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise331_5Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>
            </table>
            <p class="font-weight-bold">ใช้เวลาเดินทาง</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ใช้เวลาเดินทาง</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ต่ำกว่า 30 นาที</td>
                        <td style="text-align: center"><%= ModelData.Choise332_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise332_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">30 นาที - 1 ชั่วโมง</td>
                        <td style="text-align: center"><%= ModelData.Choise332_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise332_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มากกว่า 1 ชั่วโมง</td>
                        <td style="text-align: center"><%= ModelData.Choise332_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise332_3Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 6/8</span>
            <br />
            <p class="font-weight-bold">การเดินทางของนักเรียนไปโรงเรียน</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">การเดินทางของนักเรียนไปโรงเรียน</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ผู้ปกครองมาส่งโดยรถยนต์</td>
                        <td style="text-align: center"><%= ModelData.Choise333_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ผู้ปกครองมาส่งโดยรถจักรยานยนต์</td>
                        <td style="text-align: center"><%= ModelData.Choise333_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">รถโดยสารประจำทาง</td>
                        <td style="text-align: center"><%= ModelData.Choise333_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">รถจักรยานยนต์รับจ้าง</td>
                        <td style="text-align: center"><%= ModelData.Choise333_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">รถโรงเรียน</td>
                        <td style="text-align: center"><%= ModelData.Choise333_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">จักรยาน</td>
                        <td style="text-align: center"><%= ModelData.Choise333_6 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_6Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เดิน</td>
                        <td style="text-align: center"><%= ModelData.Choise333_7 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_7Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise333_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise333_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

            <p class="font-weight-bold">ตารางที่ 3.4 สภาพที่อยู่อาศัย</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">สภาพที่อยู่อาศัย</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">บ้านไม้</td>
                        <td style="text-align: center"><%= ModelData.Choise34_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บ้านปูน</td>
                        <td style="text-align: center"><%= ModelData.Choise34_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">บ้านครึ่งปูนครึ่งไม้</td>
                        <td style="text-align: center"><%= ModelData.Choise34_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ห้องแถว/ทาวน์เฮาส์</td>
                        <td style="text-align: center"><%= ModelData.Choise34_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">คอนโดมิเนียม/อะพาร์ตเมนต์/แฟลต</td>
                        <td style="text-align: center"><%= ModelData.Choise34_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise34_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise34_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.5 ภาระงานความรับผิดชอบของนักเรียนที่มีต่อครอบครัว</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">ภาระงานความรับผิดชอบของนักเรียนที่มีต่อครอบครัว</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ช่วยงานบ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise35_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ช่วยดูแลคนเจ็บป่วย/พิการ</td>
                        <td style="text-align: center"><%= ModelData.Choise35_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ช่วยค้าขายเล็กๆน้อยๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise35_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ทำงานแถวบ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise35_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ช่วยงานในนาไร่</td>
                        <td style="text-align: center"><%= ModelData.Choise35_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise35_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise35_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.6 กิจกรรมยามว่างหรืองานอดิเรก</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">กิจกรรมยามว่างหรืองานอดิเรก</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ดูโทรทัศน์</td>
                        <td style="text-align: center"><%= ModelData.Choise36_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไปเที่ยวห้างสรรพสินค้า/ดูภาพยนตร์</td>
                        <td style="text-align: center"><%= ModelData.Choise36_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไปหาเพื่อน</td>
                        <td style="text-align: center"><%= ModelData.Choise36_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เล่นเกมคอมพิวเตอร์/โทรศัพท์มือถือ</td>
                        <td style="text-align: center"><%= ModelData.Choise36_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไปร้านสนุกเกอร์</td>
                        <td style="text-align: center"><%= ModelData.Choise36_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อ่านหนังสือ</td>
                        <td style="text-align: center"><%= ModelData.Choise36_6%></td>
                        <td style="text-align: center"><%= ModelData.Choise36_6Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขี่รถจักรยานยนต์</td>
                        <td style="text-align: center"><%= ModelData.Choise36_7 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_7Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไปสวนสาธารณะ</td>
                        <td style="text-align: center"><%= ModelData.Choise36_8 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_8Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise36_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise36_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 7/8</span>
            <br />
            <p class="font-weight-bold">ตารางที่ 3.7 พฤติกรรมการใช้สารเสพติด</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">พฤติกรรมการใช้สารเสพติด</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่มีพฤติกรรมเกี่ยวข้องกับการใช้สารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise37_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_NormalPercent.ToString("0.##") %></td>
                        <%--    <td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise37_1 + ModelData.Choise37_2 + ModelData.Choise37_3 + ModelData.Choise37_4 + ModelData.Choise37_5 )  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise37_1Percent + ModelData.Choise37_2Percent + ModelData.Choise37_3Percent + ModelData.Choise37_4Percent + ModelData.Choise37_5Percent)).ToString("0.##")  %></td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: left">คบเพื่อนในกลุ่มที่ใช้สารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise37_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">สมาชิกในครอบครัวเกี่ยวข้องกับสารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise37_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อยู่ในสภาพแวดล้อมที่ใช้สารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise37_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ปัจจุบันเกี่ยวข้องกับสารเสพติด</td>
                        <td style="text-align: center"><%= ModelData.Choise37_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เป็นผู้ติดบุหรี่ สุรา หรือการใช้สารเสพติดอื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise37_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise37_5Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.8 พฤติกรรมการใช้ความรุนแรง</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">พฤติกรรมการใช้ความรุนแรง</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่มีพฤติกรรมการใช้ความรุนแรง</td>
                        <td style="text-align: center"><%= ModelData.Choise38_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_NormalPercent.ToString("0.##") %></td>
                        <%-- <td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise38_1 + ModelData.Choise38_2 + ModelData.Choise38_3 + ModelData.Choise38_4 + ModelData.Choise38_5 )  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise38_1Percent + ModelData.Choise38_2Percent + ModelData.Choise38_3Percent + ModelData.Choise38_4Percent + ModelData.Choise38_5Percent)).ToString("0.##")  %></td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีการทะเลาะวิวาท</td>
                        <td style="text-align: center"><%= ModelData.Choise38_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ก้าวร้าว เกเร</td>
                        <td style="text-align: center"><%= ModelData.Choise38_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ทะเลาะวิวาทเป็นประจำ</td>
                        <td style="text-align: center"><%= ModelData.Choise38_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ทำร้ายร่างกายผู้อื่น</td>
                        <td style="text-align: center"><%= ModelData.Choise38_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_4Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ทำร้ายร่างกายตนเอง</td>
                        <td style="text-align: center"><%= ModelData.Choise38_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise38_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise38_99Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.9 พฤติกรรมทางเพศ</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">พฤติกรรมทางเพศ</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่เกี่ยวข้องกับพฤติกรรมดังกล่าว</td>
                        <td style="text-align: center"><%= ModelData.Choise39_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_NormalPercent.ToString("0.##") %></td>
                        <%--  <td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise39_1 + ModelData.Choise39_2 + ModelData.Choise39_3 + ModelData.Choise39_4 + ModelData.Choise39_5 + ModelData.Choise39_6)  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise39_1Percent + ModelData.Choise39_2Percent + ModelData.Choise39_3Percent + ModelData.Choise39_4Percent + ModelData.Choise39_5Percent + ModelData.Choise39_6Percent)).ToString("0.##")  %></td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: left">อยู่ในกลุ่มขายบริการ</td>
                        <td style="text-align: center"><%= ModelData.Choise39_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ใช้เครื่องมือสื่อสารที่เกี่ยวข้องกับด้านเพศเป็นเวลานานและบ่อยครั้ง</td>
                        <td style="text-align: center"><%= ModelData.Choise39_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ตั้งครรภ์</td>
                        <td style="text-align: center"><%= ModelData.Choise39_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขายบริการทางเพศ</td>
                        <td style="text-align: center"><%= ModelData.Choise39_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_4Percent.ToString("0.##") %></td>
                    </tr>

                    <tr>
                        <td style="text-align: left">หมกมุ่นในการใช้เครื่องมือสื่อสารที่เกี่ยวข้องทางเพศ</td>
                        <td style="text-align: center"><%= ModelData.Choise39_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">มีการมั่วสุมทางเพศ</td>
                        <td style="text-align: center"><%= ModelData.Choise39_6 %></td>
                        <td style="text-align: center"><%= ModelData.Choise39_6Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.10 การติดเกม</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">การติดเกม</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ไม่มีพฤติกรรมดังกล่าว</td>
                        <td style="text-align: center"><%= ModelData.Choise310_Normal %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_NormalPercent.ToString("0.##") %></td>
                        <%--<td style="text-align: center"><%= ModelData.CountAll - (ModelData.Choise310_1 + ModelData.Choise310_2 + ModelData.Choise310_3 + ModelData.Choise310_4 + ModelData.Choise310_5 + ModelData.Choise310_6 + ModelData.Choise310_7 + ModelData.Choise310_8 + ModelData.Choise310_99)  %></td>
                <td style="text-align: center"><%= (100 - (ModelData.Choise310_1Percent + ModelData.Choise310_2Percent + ModelData.Choise310_3Percent + ModelData.Choise310_4Percent + ModelData.Choise310_5Percent + ModelData.Choise310_6Percent + ModelData.Choise310_7Percent + ModelData.Choise310_8Percent + ModelData.Choise310_99Percent)).ToString("0.##")  %></td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: left">ขาดจินตนาการและความคิดสร้างสรรค์</td>
                        <td style="text-align: center"><%= ModelData.Choise310_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">เก็บตัว แยกตัวจากกลุ่มเพื่อน</td>
                        <td style="text-align: center"><%= ModelData.Choise310_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_2Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ใช้จ่ายเงินผิดปกติ</td>
                        <td style="text-align: center"><%= ModelData.Choise310_3 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_3Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อยู่ในกลุ่มเพื่อนเล่นเกม</td>
                        <td style="text-align: center"><%= ModelData.Choise310_4 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_4Percent.ToString("0.##") %></td>
                    </tr>

                    <tr>
                        <td style="text-align: left">ร้านเกมอยู่ใกล้บ้านหรือโรงเรียน</td>
                        <td style="text-align: center"><%= ModelData.Choise310_5 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_5Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ใช้เวลาเล่นเกมเกิน 2 ชั่วโมง</td>
                        <td style="text-align: center"><%= ModelData.Choise310_6 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_6Percent.ToString("0.##") %></td>
                    </tr>

                    <tr>
                        <td style="text-align: left">หมกมุ่น จริงจังในการเล่นเกม</td>
                        <td style="text-align: center"><%= ModelData.Choise310_7 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_7Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ใช้เงินสิ้นเปลือง โกหก ลักขโมยเงินเพื่อเล่นเกม</td>
                        <td style="text-align: center"><%= ModelData.Choise310_8 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_8Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">อื่นๆ</td>
                        <td style="text-align: center"><%= ModelData.Choise310_99 %></td>
                        <td style="text-align: center"><%= ModelData.Choise310_99Percent.ToString("0.##") %></td>
                    </tr>

                </tbody>
            </table>

        </div>
    </div>

    <div class="page">
        <div class="sub-page">
            <span class="page-number">หน้า 8/8</span>
            <br />
            <p class="font-weight-bold">ตารางที่ 3.11 การเข้าถึงสื่อคอมพิวเตอร์และอินเตอร์เน็ตที่บ้าน</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">การเข้าถึงสื่อคอมพิวเตอร์และอินเตอร์เน็ตที่บ้าน</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">สามารถเข้าถึงอินเทอร์เน็ตได้จากที่บ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise311_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise311_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ไม่สามารถเข้าถึงอินเทอร์เน็ตได้จากที่บ้าน</td>
                        <td style="text-align: center"><%= ModelData.Choise311_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise311_2Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <p class="font-weight-bold">ตารางที่ 3.12 การใช้เครื่องมือสื่อสารอิเล็กทรอนิกส์</p>
            <table class=" table-hover  table-bordered" width="100%">
                <thead>
                    <tr>
                        <th style="text-align: center; width: 48%">การใช้เครื่องมือสื่อสารอิเล็กทรอนิกส์</th>
                        <th style="text-align: center; width: 26%">จำนวนนักเรียน(คน)</th>
                        <th style="text-align: center; width: 26%">คิดเป็นร้อยละ</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="text-align: left">ใช้โขเชียลมีเดีย/เกม (ไม่เกินวันละ 3 ชั่วโมง)</td>
                        <td style="text-align: center"><%= ModelData.Choise312_1 %></td>
                        <td style="text-align: center"><%= ModelData.Choise312_1Percent.ToString("0.##") %></td>
                    </tr>
                    <tr>
                        <td style="text-align: left">ใช้โชเชียลมีเดีย/เกม (วันละ 3 ชั่วโมงขึ้นไป)</td>
                        <td style="text-align: center"><%= ModelData.Choise312_2 %></td>
                        <td style="text-align: center"><%= ModelData.Choise312_2Percent.ToString("0.##") %></td>
                    </tr>
                </tbody>
            </table>
            <table class=" table-hover " style="border: 0px !important" width="100%">
                <tbody>
                    <tr>
                        <td style="text-align: right; border: 0px !important">ลงชื่อ......................................................ผู้อํานวยการ</td>

                    </tr>
                    <tr>
                        <td style="text-align: right; border: 0px !important">(................................................................................)</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>
