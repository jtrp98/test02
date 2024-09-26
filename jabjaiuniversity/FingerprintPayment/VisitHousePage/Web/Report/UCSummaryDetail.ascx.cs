﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Web.Report
{
    public partial class UCSummaryDetail : System.Web.UI.UserControl
    {
        public class ModelDetail
        {
         

            public string Test { get; set; }

            public int CountAll { get; internal set; }

            public int CountAllMale { get; set; }
            public double CountAllMalePercent { get; set; }
            public int CountAllFemale { get; set; }
            public double CountAllFemalePercent { get; set; }

            public int? CountMale { get; set; }
            public int? CountFemale { get; set; }

            public int CountVisit { get; set; }
            public int CountNotVisit { get; set; }
            public int CountVisitMale { get; set; }
            public double CountVisitMalePercent { get; internal set; }
            public int CountVisitFemale { get; set; }
            public double CountVisitFemalePercent { get; internal set; }
            public double CountVisitPercent { get; internal set; }
            public double CountNotVisitPercent { get; internal set; }
            public int Choise21_02 { get; internal set; }
            public int Choise21_34 { get; internal set; }
            public int Choise21_56 { get; internal set; }
            public int Choise21_78 { get; internal set; }
            public int Choise21_810 { get; internal set; }
            public int Choise21_10 { get; internal set; }
            public double Choise21_02Percent { get; internal set; }
            public double Choise21_34Percent { get; internal set; }
            public double Choise21_56Percent { get; internal set; }
            public double Choise21_78Percent { get; internal set; }
            public double Choise21_810Percent { get; internal set; }
            public double Choise21_10Percent { get; internal set; }
            public int Choise22_R11 { get; internal set; }
            public int Choise22_R12 { get; internal set; }
            public int Choise22_R14 { get; internal set; }
            public int Choise22_R13 { get; internal set; }
            public int Choise22_R15 { get; internal set; }
            public double Choise22_R11Percent { get; internal set; }
            public double Choise22_R12Percent { get; internal set; }
            public double Choise22_R13Percent { get; internal set; }
            public double Choise22_R14Percent { get; internal set; }
            public double Choise22_R15Percent { get; internal set; }
            public int Choise22_R21 { get; internal set; }
            public int Choise22_R22 { get; internal set; }
            public int Choise22_R23 { get; internal set; }
            public int Choise22_R24 { get; internal set; }
            public int Choise22_R25 { get; internal set; }
            public double Choise22_R21Percent { get; internal set; }
            public double Choise22_R22Percent { get; internal set; }
            public double Choise22_R23Percent { get; internal set; }
            public double Choise22_R24Percent { get; internal set; }
            public double Choise22_R25Percent { get; internal set; }
            public int Choise22_R31 { get; internal set; }
            public int Choise22_R32 { get; internal set; }
            public int Choise22_R33 { get; internal set; }
            public int Choise22_R34 { get; internal set; }
            public int Choise22_R35 { get; internal set; }
            public double Choise22_R31Percent { get; internal set; }
            public double Choise22_R32Percent { get; internal set; }
            public double Choise22_R33Percent { get; internal set; }
            public double Choise22_R34Percent { get; internal set; }
            public double Choise22_R35Percent { get; internal set; }
            public int Choise22_R41 { get; internal set; }
            public int Choise22_R42 { get; internal set; }
            public int Choise22_R43 { get; internal set; }
            public int Choise22_R44 { get; internal set; }
            public int Choise22_R45 { get; internal set; }
            public double Choise22_R41Percent { get; internal set; }
            public double Choise22_R42Percent { get; internal set; }
            public double Choise22_R43Percent { get; internal set; }
            public double Choise22_R44Percent { get; internal set; }
            public double Choise22_R45Percent { get; internal set; }
            public int Choise22_R51 { get; internal set; }
            public int Choise22_R52 { get; internal set; }
            public int Choise22_R53 { get; internal set; }
            public int Choise22_R54 { get; internal set; }
            public int Choise22_R55 { get; internal set; }
            public double Choise22_R51Percent { get; internal set; }
            public double Choise22_R52Percent { get; internal set; }
            public double Choise22_R53Percent { get; internal set; }
            public double Choise22_R54Percent { get; internal set; }
            public double Choise22_R55Percent { get; internal set; }
            public int Choise22_R61 { get; internal set; }
            public int Choise22_R62 { get; internal set; }
            public int Choise22_R63 { get; internal set; }
            public int Choise22_R64 { get; internal set; }
            public int Choise22_R65 { get; internal set; }
            public double Choise22_R61Percent { get; internal set; }
            public double Choise22_R62Percent { get; internal set; }
            public double Choise22_R63Percent { get; internal set; }
            public double Choise22_R64Percent { get; internal set; }
            public double Choise22_R65Percent { get; internal set; }
            public int Choise22_R991 { get; internal set; }
            public int Choise22_R992 { get; internal set; }
            public int Choise22_R993 { get; internal set; }
            public int Choise22_R994 { get; internal set; }
            public int Choise22_R995 { get; internal set; }
            public double Choise22_R991Percent { get; internal set; }
            public double Choise22_R992Percent { get; internal set; }
            public double Choise22_R993Percent { get; internal set; }
            public double Choise22_R994Percent { get; internal set; }
            public double Choise22_R995Percent { get; internal set; }
            public int Choise23_1 { get; internal set; }
            public int Choise23_2 { get; internal set; }
            public int Choise23_3 { get; internal set; }
            public int Choise23_99 { get; internal set; }
            public double Choise23_1Percent { get; internal set; }
            public double Choise23_2Percent { get; internal set; }
            public double Choise23_3Percent { get; internal set; }
            public double Choise23_99Percent { get; internal set; }
            public int Choise31_1 { get; internal set; }
            public int Choise31_2 { get; internal set; }
            public int Choise31_3 { get; internal set; }
            public int Choise31_4 { get; internal set; }
            public double Choise31_1Percent { get; internal set; }
            public int Choise31_5 { get; internal set; }
            public double Choise31_2Percent { get; internal set; }
            public double Choise31_3Percent { get; internal set; }
            public double Choise31_5Percent { get; internal set; }
            public double Choise31_4Percent { get; internal set; }
            public int Choise32_1 { get; internal set; }
            public int Choise32_2 { get; internal set; }
            public int Choise32_3 { get; internal set; }
            public int Choise32_4 { get; internal set; }
            public int Choise32_5 { get; internal set; }
            public int Choise32_6 { get; internal set; }
            public int Choise32_7 { get; internal set; }
            public int Choise32_8 { get; internal set; }
            public int Choise32_9 { get; internal set; }
            public int Choise32_10 { get; internal set; }
            public int Choise32_11 { get; internal set; }
            public double Choise32_1Percent { get; internal set; }
            public double Choise32_2Percent { get; internal set; }
            public double Choise32_3Percent { get; internal set; }
            public double Choise32_4Percent { get; internal set; }
            public double Choise32_5Percent { get; internal set; }
            public double Choise32_6Percent { get; internal set; }
            public double Choise32_7Percent { get; internal set; }
            public double Choise32_8Percent { get; internal set; }
            public double Choise32_9Percent { get; internal set; }
            public double Choise32_10Percent { get; internal set; }
            public double Choise32_11Percent { get; internal set; }
            public int Choise331_1 { get; internal set; }
            public int Choise331_2 { get; internal set; }
            public int Choise331_4 { get; internal set; }
            public int Choise331_5 { get; internal set; }
            public double Choise331_1Percent { get; internal set; }
            public double Choise331_2Percent { get; internal set; }
            public int Choise331_3 { get; internal set; }
            public double Choise331_3Percent { get; internal set; }
            public double Choise331_4Percent { get; internal set; }
            public double Choise331_5Percent { get; internal set; }
            public int Choise332_1 { get; internal set; }
            public int Choise332_2 { get; internal set; }
            public int Choise332_3 { get; internal set; }
            public double Choise332_1Percent { get; internal set; }
            public double Choise332_2Percent { get; internal set; }
            public double Choise332_3Percent { get; internal set; }
            public int Choise333_1 { get; internal set; }
            public int Choise333_2 { get; internal set; }
            public int Choise333_3 { get; internal set; }
            public int Choise333_4 { get; internal set; }
            public int Choise333_5 { get; internal set; }
            public int Choise333_6 { get; internal set; }
            public int Choise333_7 { get; internal set; }
            public int Choise333_99 { get; internal set; }
            public double Choise333_1Percent { get; internal set; }
            public double Choise333_2Percent { get; internal set; }
            public double Choise333_3Percent { get; internal set; }
            public double Choise333_4Percent { get; internal set; }
            public double Choise333_5Percent { get; internal set; }
            public double Choise333_6Percent { get; internal set; }
            public double Choise333_7Percent { get; internal set; }
            public double Choise333_99Percent { get; internal set; }
            public int Choise34_1 { get; internal set; }
            public int Choise34_2 { get; internal set; }
            public int Choise34_3 { get; internal set; }
            public int Choise34_4 { get; internal set; }
            public int Choise34_5 { get; internal set; }
            public int Choise34_99 { get; internal set; }
            public double Choise34_1Percent { get; internal set; }
            public double Choise34_2Percent { get; internal set; }
            public double Choise34_3Percent { get; internal set; }
            public double Choise34_4Percent { get; internal set; }
            public double Choise34_5Percent { get; internal set; }
            public double Choise34_99Percent { get; internal set; }
            public int Choise35_1 { get; internal set; }
            public int Choise35_2 { get; internal set; }
            public int Choise35_3 { get; internal set; }
            public int Choise35_4 { get; internal set; }
            public int Choise35_5 { get; internal set; }
            public int Choise35_99 { get; internal set; }
            public double Choise35_1Percent { get; internal set; }
            public double Choise35_2Percent { get; internal set; }
            public double Choise35_3Percent { get; internal set; }
            public double Choise35_4Percent { get; internal set; }
            public double Choise35_5Percent { get; internal set; }
            public double Choise35_99Percent { get; internal set; }
            public int Choise36_1 { get; internal set; }
            public int Choise36_2 { get; internal set; }
            public int Choise36_3 { get; internal set; }
            public int Choise36_4 { get; internal set; }
            public int Choise36_5 { get; internal set; }
            public int Choise36_6 { get; internal set; }
            public int Choise36_7 { get; internal set; }
            public int Choise36_8 { get; internal set; }
            public int Choise36_99 { get; internal set; }
            public double Choise36_1Percent { get; internal set; }
            public double Choise36_2Percent { get; internal set; }
            public double Choise36_3Percent { get; internal set; }
            public double Choise36_4Percent { get; internal set; }
            public double Choise36_5Percent { get; internal set; }
            public double Choise36_6Percent { get; internal set; }
            public double Choise36_7Percent { get; internal set; }
            public double Choise36_8Percent { get; internal set; }
            public double Choise36_99Percent { get; internal set; }
            public int Choise37_1 { get; internal set; }
            public int Choise37_2 { get; internal set; }
            public int Choise37_3 { get; internal set; }
            public int Choise37_4 { get; internal set; }
            public int Choise37_5 { get; internal set; }
            public double Choise37_1Percent { get; internal set; }
            public double Choise37_2Percent { get; internal set; }
            public double Choise37_3Percent { get; internal set; }
            public double Choise37_4Percent { get; internal set; }
            public double Choise37_5Percent { get; internal set; }
            public int Choise38_1 { get; internal set; }
            public int Choise38_2 { get; internal set; }
            public int Choise38_3 { get; internal set; }
            public int Choise38_4 { get; internal set; }
            public int Choise38_5 { get; internal set; }
            public double Choise38_1Percent { get; internal set; }
            public double Choise38_2Percent { get; internal set; }
            public double Choise38_3Percent { get; internal set; }
            public double Choise38_4Percent { get; internal set; }
            public double Choise38_5Percent { get; internal set; }
            public int Choise39_1 { get; internal set; }
            public int Choise39_2 { get; internal set; }
            public int Choise39_3 { get; internal set; }
            public int Choise39_4 { get; internal set; }
            public int Choise39_5 { get; internal set; }

            public double Choise39_1Percent { get; internal set; }
            public double Choise39_2Percent { get; internal set; }
            public double Choise39_3Percent { get; internal set; }
            public double Choise39_4Percent { get; internal set; }
            public double Choise39_5Percent { get; internal set; }
            public int Choise39_6 { get; internal set; }
            public double Choise39_6Percent { get; internal set; }
            public int Choise310_1 { get; internal set; }
            public int Choise310_2 { get; internal set; }
            public int Choise310_3 { get; internal set; }
            public int Choise310_4 { get; internal set; }
            public int Choise310_5 { get; internal set; }
            public int Choise310_6 { get; internal set; }
            public int Choise310_7 { get; internal set; }
            public int Choise310_8 { get; internal set; }
            public int Choise310_99 { get; internal set; }
            public double Choise310_1Percent { get; internal set; }
            public double Choise310_2Percent { get; internal set; }
            public double Choise310_3Percent { get; internal set; }
            public double Choise310_4Percent { get; internal set; }
            public double Choise310_5Percent { get; internal set; }
            public double Choise310_6Percent { get; internal set; }
            public double Choise310_7Percent { get; internal set; }
            public double Choise310_8Percent { get; internal set; }
            public double Choise310_99Percent { get; internal set; }
            public int Choise311_1 { get; internal set; }
            public int Choise311_2 { get; internal set; }
            public double Choise311_1Percent { get; internal set; }
            public double Choise311_2Percent { get; internal set; }
            public int Choise312_1 { get; internal set; }
            public int Choise312_2 { get; internal set; }
            public double Choise312_1Percent { get; internal set; }
            public double Choise312_2Percent { get; internal set; }
            public int Choise31_Normal { get; internal set; }
            public double Choise31_NormalPercent { get; internal set; }
            public int Choise32_Normal { get; internal set; }
            public double Choise32_NormalPercent { get; internal set; }
            public int Choise37_Normal { get; internal set; }
            public double Choise37_NormalPercent { get; internal set; }
            public int Choise38_Normal { get; internal set; }
            public double Choise38_NormalPercent { get; internal set; }
            public int Choise39_Normal { get; internal set; }
            public double Choise39_NormalPercent { get; internal set; }
            public int Choise310_Normal { get; internal set; }
            public double Choise310_NormalPercent { get; internal set; }
            public int Choise24_1 { get; internal set; }
            public int Choise24_2 { get; internal set; }
            public int Choise24_3 { get; internal set; }
            public int Choise24_4 { get; internal set; }
            public double Choise24_1Percent { get; internal set; }
            public double Choise24_2Percent { get; internal set; }
            public double Choise24_3Percent { get; internal set; }
            public double Choise24_4Percent { get; internal set; }
            public int Choise251_1 { get; internal set; }
            public int Choise251_2 { get; internal set; }
            public int Choise251_3 { get; internal set; }
            public double Choise251_1Percent { get; internal set; }
            public double Choise251_2Percent { get; internal set; }
            public double Choise251_3Percent { get; internal set; }
            public int Choise251_99 { get; internal set; }
            public double Choise251_99Percent { get; internal set; }
            public int Choise252_1 { get; internal set; }
            public int Choise252_2 { get; internal set; }
            public int Choise252_3 { get; internal set; }
            public int Choise252_4 { get; internal set; }
            public int Choise252_99 { get; internal set; }
            public double Choise252_1Percent { get; internal set; }
            public double Choise252_2Percent { get; internal set; }
            public double Choise252_3Percent { get; internal set; }
            public double Choise252_4Percent { get; internal set; }
            public double Choise252_99Percent { get; internal set; }
            public int Choise253_1 { get; internal set; }
            public int Choise253_2 { get; internal set; }
            public int Choise253_3 { get; internal set; }
            public int Choise253_4 { get; internal set; }
            public double Choise253_1Percent { get; internal set; }
            public double Choise253_2Percent { get; internal set; }
            public double Choise253_3Percent { get; internal set; }
            public double Choise253_4Percent { get; internal set; }
            public int Choise254_1 { get; internal set; }
            public int Choise254_2 { get; internal set; }
            public int Choise254_3 { get; internal set; }
            public double Choise254_1Percent { get; internal set; }
            public double Choise254_2Percent { get; internal set; }
            public double Choise254_3Percent { get; internal set; }
            public int Choise26_1 { get; internal set; }
            public int Choise26_2 { get; internal set; }
            public int Choise26_3 { get; internal set; }
            public int Choise26_99 { get; internal set; }
            public double Choise26_1Percent { get; internal set; }
            public double Choise26_2Percent { get; internal set; }
            public double Choise26_3Percent { get; internal set; }
            public double Choise26_99Percent { get; internal set; }
            public int Choise27_1 { get; internal set; }
            public int Choise27_2 { get; internal set; }
            public int Choise27_99 { get; internal set; }
            public double Choise27_1Percent { get; internal set; }
            public double Choise27_2Percent { get; internal set; }
            public double Choise27_99Percent { get; internal set; }
            public int Choise27_No { get; internal set; }
            public double Choise27_NoPercent { get; internal set; }
            public int ChoiseNew21_01 { get; internal set; }
            public int ChoiseNew21_02 { get; internal set; }
            public int ChoiseNew21_03 { get; internal set; }
            public int ChoiseNew21_04 { get; internal set; }
            public int ChoiseNew21_05 { get; internal set; }
            public int ChoiseNew21_99 { get; internal set; }
            public double ChoiseNew21_99Percent { get; internal set; }
            public double ChoiseNew21_01Percent { get; internal set; }
            public double ChoiseNew21_02Percent { get; internal set; }
            public double ChoiseNew21_04Percent { get; internal set; }
            public double ChoiseNew21_03Percent { get; internal set; }
            public double ChoiseNew21_05Percent { get; internal set; }
            public int ChoiseNew22_1_01 { get; internal set; }
            public int ChoiseNew22_1_02 { get; internal set; }
            public int ChoiseNew22_1_03 { get; internal set; }
            public int ChoiseNew22_1_04 { get; internal set; }
            public int ChoiseNew22_1_05 { get; internal set; }
            public double ChoiseNew22_1_01Percent { get; internal set; }
            public double ChoiseNew22_1_02Percent { get; internal set; }
            public double ChoiseNew22_1_03Percent { get; internal set; }
            public double ChoiseNew22_1_04Percent { get; internal set; }
            public double ChoiseNew22_1_05Percent { get; internal set; }
            public int ChoiseNew22_2_01 { get; internal set; }
            public int ChoiseNew22_2_02 { get; internal set; }
            public int ChoiseNew22_2_03 { get; internal set; }
            public int ChoiseNew22_2_04 { get; internal set; }
            public double ChoiseNew22_2_01Percent { get; internal set; }
            public double ChoiseNew22_2_02Percent { get; internal set; }
            public double ChoiseNew22_2_03Percent { get; internal set; }
            public double ChoiseNew22_2_04Percent { get; internal set; }
            public int ChoiseNew22_3_01 { get; internal set; }
            public int ChoiseNew22_3_02 { get; internal set; }
            public double ChoiseNew22_3_01Percent { get; internal set; }
            public double ChoiseNew22_3_02Percent { get; internal set; }
            public int ChoiseNew22_4_01 { get; internal set; }
            public int ChoiseNew22_4_02 { get; internal set; }
            public double ChoiseNew22_4_01Percent { get; internal set; }
            public double ChoiseNew22_4_02Percent { get; internal set; }
            public int ChoiseNew22_5_01 { get; internal set; }
            public int ChoiseNew22_5_02 { get; internal set; }
            public double ChoiseNew22_5_01Percent { get; internal set; }
            public double ChoiseNew22_5_02Percent { get; internal set; }
            public int ChoiseNew2_4_01 { get; internal set; }
            public int ChoiseNew2_4_02 { get; internal set; }
            public int ChoiseNew2_4_03 { get; internal set; }
            public int ChoiseNew2_4_05 { get; internal set; }
            public int ChoiseNew2_4_99 { get; internal set; }
            public int ChoiseNew2_4_04 { get; internal set; }
            public double ChoiseNew2_4_01Percent { get; internal set; }
            public double ChoiseNew2_4_02Percent { get; internal set; }
            public double ChoiseNew2_4_03Percent { get; internal set; }
            public double ChoiseNew2_4_04Percent { get; internal set; }
            public double ChoiseNew2_4_05Percent { get; internal set; }
            public double ChoiseNew2_4_99Percent { get; internal set; }
            public int Choise2_11_01 { get; internal set; }
            public int Choise2_11_02 { get; internal set; }
            public int Choise2_11_03 { get; internal set; }
            public int Choise2_11_04 { get; internal set; }
            public int Choise2_11_99 { get; internal set; }
            public double Choise2_11_01Percent { get; internal set; }
            public double Choise2_11_02Percent { get; internal set; }
            public double Choise2_11_03Percent { get; internal set; }
            public double Choise2_11_04Percent { get; internal set; }
            public double Choise2_11_99Percent { get; internal set; }
            public int Choise38_99 { get; internal set; }
            public double Choise38_99Percent { get; internal set; }
        }

        public ModelDetail ModelData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            //MODEL = new ModelDetail();
        }
    }
}