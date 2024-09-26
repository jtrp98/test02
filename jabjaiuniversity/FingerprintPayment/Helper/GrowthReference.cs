using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FingerprintPayment.ViewModels;

namespace FingerprintPayment.Helper
{
    public static class GrowthReference
    {

        public static List<GrowthByWidth> GetGrowthByWidths()
        {
            List<GrowthByWidth> GrowthByWidths = new List<GrowthByWidth>()
            {
               //ชาย
               new GrowthByWidth { Height= 85, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 9.9 }, new Measurement { Min = 10.0, Max = 10.3 }, new Measurement { Min = 10.4, Max=  13.9 },
                   new Measurement { Min = 14, Max = 14.7 }, new Measurement { Min = 14.8, Max = 16.2 }, new Measurement { Min = 16.3, Max = 200 })
               },
               new GrowthByWidth { Height= 86, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 10.1 }, new Measurement { Min = 10.2, Max = 10.5 }, new Measurement { Min = 10.6, Max = 14.1 },
                   new Measurement { Min = 14.2, Max = 14.9 }, new Measurement { Min = 15, Max = 16.4 }, new Measurement { Min = 16.5, Max = 200 })
               },
               new GrowthByWidth { Height= 87, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 10.3 }, new Measurement { Min = 10.4, Max = 10.7 }, new Measurement { Min = 10.8, Max = 14.3 },
                   new Measurement { Min = 14.4, Max = 15.1 }, new Measurement { Min = 15.2, Max = 16.6 }, new Measurement { Min = 16.7, Max = 200 })
               },
               new GrowthByWidth { Height= 88, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 10.5 }, new Measurement { Min = 10.6, Max = 10.9 }, new Measurement { Min = 11, Max = 14.6 },
                   new Measurement { Min = 14.7, Max = 15.4 }, new Measurement { Min = 15.5, Max = 16.9 }, new Measurement { Min = 17, Max = 200 })
               },
               new GrowthByWidth { Height= 89, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 10.8 }, new Measurement { Min = 10.9, Max = 11.3 }, new Measurement { Min = 11.4, Max = 15 },
                   new Measurement { Min = 15.1, Max = 15.7 }, new Measurement { Min = 15.8, Max = 17.1 }, new Measurement { Min = 17.2, Max = 200 })
               },
               new GrowthByWidth { Height= 90, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 11 }, new Measurement { Min = 11.1, Max = 11.5 }, new Measurement { Min = 11.6, Max = 15.2 },
                   new Measurement { Min = 15.3, Max = 15.9 }, new Measurement { Min = 16, Max = 17.3 }, new Measurement { Min = 17.4, Max = 200 })
               },
               new GrowthByWidth { Height= 91, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 11.2 }, new Measurement { Min = 11.3, Max = 11.7 }, new Measurement { Min = 11.8, Max = 15.5 },
                   new Measurement { Min = 15.6, Max = 16.2 }, new Measurement { Min = 16.3, Max = 17.6 }, new Measurement { Min = 17.7, Max = 200 })
               },
               new GrowthByWidth { Height= 92, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 11.4 }, new Measurement { Min = 11.5, Max = 11.9 }, new Measurement { Min = 12, Max = 15.8 },
                   new Measurement { Min = 15.9, Max = 16.5 }, new Measurement { Min = 16.6, Max = 17.9 }, new Measurement { Min = 18, Max = 200 })
               },
               new GrowthByWidth { Height= 93, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 11.6 }, new Measurement { Min = 11.7, Max = 12.1 }, new Measurement { Min = 12.2, Max = 16 },
                   new Measurement { Min = 16.1, Max = 16.8 }, new Measurement { Min = 16.9, Max = 18.3 }, new Measurement { Min = 18.4, Max = 200 })
               },
               new GrowthByWidth { Height= 94, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 11.9 }, new Measurement { Min = 12, Max = 12.4 }, new Measurement { Min = 12.5, Max = 16.4 },
                   new Measurement { Min = 16.5, Max = 17.2 }, new Measurement { Min = 17.3, Max = 18.7 }, new Measurement { Min = 18.8, Max = 200 })
               },
               new GrowthByWidth { Height= 95, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 12.1 }, new Measurement { Min = 12.2, Max = 12.6 }, new Measurement { Min = 12.7, Max = 16.7 },
                   new Measurement { Min = 16.8, Max = 17.5 }, new Measurement { Min = 17.6, Max = 19.1 }, new Measurement { Min = 19.2, Max = 200 })
               },
               new GrowthByWidth { Height= 96, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 12.3 }, new Measurement { Min = 12.4, Max = 12.8 }, new Measurement { Min = 12.9, Max = 17 },
                   new Measurement { Min = 17.1, Max = 17.8 }, new Measurement { Min = 17.9, Max = 19.4 }, new Measurement { Min = 19.5, Max = 200 })
               },
               new GrowthByWidth { Height= 97, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 12.5 }, new Measurement { Min = 12.6, Max = 13 }, new Measurement { Min = 13.1, Max = 17.3 },
                   new Measurement { Min = 17.4, Max = 18.4 }, new Measurement { Min = 18.2, Max = 19.7 }, new Measurement { Min = 19.8, Max = 200 })
               },
               new GrowthByWidth { Height= 98, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 12.7 }, new Measurement { Min = 12.8, Max = 13.2 }, new Measurement { Min = 13.3, Max = 17.6 },
                   new Measurement { Min = 17.7, Max = 18.4 }, new Measurement { Min = 18.5, Max = 20.1 }, new Measurement { Min = 20.2, Max = 200 })
               },
               new GrowthByWidth { Height= 99, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 13 }, new Measurement { Min = 13.1, Max = 13.5 }, new Measurement { Min = 13.6, Max = 17.9 },
                   new Measurement { Min = 18, Max = 18.7 }, new Measurement { Min = 18.8, Max = 20.4 }, new Measurement { Min = 20.5, Max = 200 })
               },
               new GrowthByWidth { Height= 100, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 13.2 }, new Measurement { Min = 13.3, Max = 13.7 }, new Measurement { Min = 13.8, Max = 18.2 },
                   new Measurement { Min = 18.3, Max = 19 }, new Measurement { Min = 19.1, Max = 20.7 }, new Measurement { Min = 20.8, Max = 200 })
               },
               new GrowthByWidth { Height= 101, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 13.4 }, new Measurement { Min = 13.5, Max = 13.9 }, new Measurement { Min = 14, Max = 18.4 },
                   new Measurement { Min = 18.5, Max = 19.3 }, new Measurement { Min = 19.4, Max = 21.1 }, new Measurement { Min = 21.2, Max = 200 })
               },
               new GrowthByWidth { Height= 102, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 13.7 }, new Measurement { Min = 12, Max = 13.8 }, new Measurement { Min = 14.3, Max = 18.7 },
                   new Measurement { Min = 18.8, Max = 19.6 }, new Measurement { Min = 19.7, Max = 21.4 }, new Measurement { Min = 21.5, Max = 200 })
               },
               new GrowthByWidth { Height= 103, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 13.9 }, new Measurement { Min = 14, Max = 14.5 }, new Measurement { Min = 14.6, Max = 19 },
                   new Measurement { Min = 19.1, Max = 19.9 }, new Measurement { Min = 20, Max = 21.7 }, new Measurement { Min = 21.8, Max = 200 })
               },
               new GrowthByWidth { Height= 104, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 14.1 }, new Measurement { Min = 14.2, Max = 14.7 }, new Measurement { Min = 14.8, Max = 19.4 },
                   new Measurement { Min = 19.5, Max = 20.3 }, new Measurement { Min = 20.4, Max = 22.2 }, new Measurement { Min = 22.3, Max = 200 })
               },
               new GrowthByWidth { Height= 105, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 14.4 }, new Measurement { Min = 14.5, Max = 15 }, new Measurement { Min = 15.1, Max = 19.7 },
                   new Measurement { Min = 19.8, Max = 20.6 }, new Measurement { Min = 20.7, Max = 22.5 }, new Measurement { Min = 22.6, Max = 200 })
               },
               new GrowthByWidth { Height= 106, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 14.5 }, new Measurement { Min = 14.6, Max = 15.2 }, new Measurement { Min = 15.3, Max = 20.1 },
                   new Measurement { Min = 20.2, Max = 21 }, new Measurement { Min = 21.1, Max = 22.9 }, new Measurement { Min = 23, Max = 200 })
               },
               new GrowthByWidth { Height= 107, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 14.8 }, new Measurement { Min = 14.9, Max = 15.5 }, new Measurement { Min = 15.6, Max = 20.5 },
                   new Measurement { Min = 20.6, Max = 21.4 }, new Measurement { Min = 21.5, Max = 23.4 }, new Measurement { Min = 23.5, Max = 200 })
               },
               new GrowthByWidth { Height= 108, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 15 }, new Measurement { Min = 15.1, Max = 15.7 }, new Measurement { Min = 15.8, Max = 20.8 },
                   new Measurement { Min = 20.9, Max = 21.7 }, new Measurement { Min = 21.8, Max = 23.7 }, new Measurement { Min = 23.8, Max = 200 })
               },
               new GrowthByWidth { Height= 109, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 15.3 }, new Measurement { Min = 15.4, Max = 16 }, new Measurement { Min = 16.1, Max = 21.1 },
                   new Measurement { Min = 21.2, Max = 22.1 }, new Measurement { Min = 22.2, Max = 24.2 }, new Measurement { Min = 24.3, Max = 200 })
               },
               new GrowthByWidth { Height= 110, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 15.6 }, new Measurement { Min = 15.7, Max = 16.3 }, new Measurement { Min = 16.4, Max = 21.6 },
                   new Measurement { Min = 21.7, Max = 22.7 }, new Measurement { Min = 22.8, Max=  24.8 }, new Measurement { Min = 24.9, Max = 200 })
               },
               new GrowthByWidth { Height= 111, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 15.9 }, new Measurement { Min = 16, Max = 16.6 }, new Measurement { Min = 16.7, Max = 22 },
                   new Measurement { Min = 22.1, Max = 23.1 }, new Measurement { Min = 23.2, Max=  25.2 }, new Measurement { Min = 25.3, Max = 200 })
               },
               new GrowthByWidth { Height= 112, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 16.1 }, new Measurement { Min = 16.2, Max = 16.8 }, new Measurement { Min = 16.9, Max = 22.3 },
                   new Measurement { Min = 22.4, Max = 23.5 }, new Measurement { Min = 23.6, Max=  25.7 }, new Measurement { Min = 25.8, Max = 200 })
               },
               new GrowthByWidth { Height= 113, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 16.4 }, new Measurement { Min = 16.5, Max = 17.1 }, new Measurement { Min = 17.2, Max = 22.8 },
                   new Measurement { Min = 22.9, Max = 24 }, new Measurement { Min = 24.1, Max=  26.3 }, new Measurement { Min = 26.4, Max = 200 })
               },
               new GrowthByWidth { Height= 114, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 16.7 }, new Measurement { Min = 16.8, Max = 17.4 }, new Measurement { Min = 17.5, Max = 23.3 },
                   new Measurement { Min = 23.4, Max = 24.5 }, new Measurement { Min = 24.6, Max=  26.9 }, new Measurement { Min = 27, Max = 200 })
               },
               new GrowthByWidth { Height= 115, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 17 }, new Measurement { Min = 17.1, Max = 17.8 }, new Measurement { Min = 17.9, Max = 23.8 },
                   new Measurement { Min = 23.9, Max = 25 }, new Measurement { Min = 25.1, Max=  27.4 }, new Measurement { Min = 27.5, Max = 200 })
               },
               new GrowthByWidth { Height= 116, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 17.3 }, new Measurement { Min = 17.4, Max = 18.1 }, new Measurement { Min = 18.2, Max = 24.2 },
                   new Measurement { Min = 24.3, Max = 25.5 }, new Measurement { Min = 25.6, Max=  28.1 }, new Measurement { Min = 28.2, Max = 200 })
               },
               new GrowthByWidth { Height= 117, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 17.6 }, new Measurement { Min = 17.7, Max = 18.4 }, new Measurement { Min = 18.5, Max = 24.8 },
                   new Measurement { Min = 24.9, Max = 26.1 }, new Measurement { Min = 26.2, Max=  28.8 }, new Measurement { Min = 28.9, Max = 200 })
               },
               new GrowthByWidth { Height= 118, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 18 }, new Measurement { Min = 18.1, Max = 18.8 }, new Measurement { Min = 18.9, Max = 25.2 },
                   new Measurement { Min = 25.3, Max = 26.6 }, new Measurement { Min = 26.7, Max=  29.4 }, new Measurement { Min = 29.5, Max = 200 })
               },
               new GrowthByWidth { Height= 119, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 18.3 }, new Measurement { Min = 18.4, Max = 19.1 }, new Measurement { Min = 19.2, Max = 25.8 },
                   new Measurement { Min = 25.9, Max = 27.2 }, new Measurement { Min = 27.3, Max=  30.1 }, new Measurement { Min = 30.2, Max = 200 })
               },
               new GrowthByWidth { Height= 120, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 18.6 }, new Measurement { Min = 18.7, Max = 19.4 }, new Measurement { Min = 19.5, Max = 26.4 },
                   new Measurement { Min = 26.5, Max = 27.9 }, new Measurement { Min = 28, Max=  30.8 }, new Measurement { Min = 30.9, Max = 200 })
               },
               new GrowthByWidth { Height= 121, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 18.9 }, new Measurement { Min = 19, Max = 19.7 }, new Measurement { Min = 19.8, Max = 26.9 },
                   new Measurement { Min = 27, Max = 28.5 }, new Measurement { Min = 28.6, Max = 31.6 }, new Measurement { Min = 31.1, Max = 200 })
               },
               new GrowthByWidth { Height= 122, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 19.3 }, new Measurement { Min = 19.4, Max = 20.1 }, new Measurement { Min = 20.2, Max = 27.5 },
                   new Measurement { Min = 27.6, Max = 29.1 }, new Measurement { Min = 29.2, Max = 32.3 }, new Measurement { Min = 32.4, Max = 200 })
               },
               new GrowthByWidth { Height= 123, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 19.6 }, new Measurement { Min = 19.7, Max = 20.4 }, new Measurement { Min = 20.5, Max = 28 },
                   new Measurement { Min = 28.1, Max = 29.7 }, new Measurement { Min = 29.8, Max = 33 }, new Measurement { Min = 33.1, Max = 200 })
               },
               new GrowthByWidth { Height= 124, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 20 }, new Measurement { Min = 20.1, Max = 20.8 }, new Measurement { Min = 20.9, Max = 28.7 },
                   new Measurement { Min = 28.8, Max = 30.4 }, new Measurement { Min = 30.5, Max = 33.9 }, new Measurement { Min = 34, Max = 200 })
               },
               new GrowthByWidth { Height= 125, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 20.3 }, new Measurement { Min = 20.4, Max = 21.2 }, new Measurement { Min = 21.3, Max = 29.3 },
                   new Measurement { Min = 29.4, Max = 31 }, new Measurement { Min = 31.1, Max=  34.5 }, new Measurement { Min = 34.6, Max = 200 })
               },
               new GrowthByWidth { Height= 126, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 20.7 }, new Measurement { Min = 20.8, Max = 21.6 }, new Measurement { Min = 21.7, Max = 29.9 },
                   new Measurement { Min = 30, Max = 31.7 }, new Measurement { Min = 31.8, Max = 35.3 }, new Measurement { Min = 35.4, Max = 200 })
               },
               new GrowthByWidth { Height= 127, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 21.1 }, new Measurement { Min = 21.2, Max = 22 }, new Measurement { Min = 22.1, Max = 30.7 },
                   new Measurement { Min = 30.8, Max = 32.6 }, new Measurement { Min = 32.7, Max = 36.3 }, new Measurement { Min = 36.4, Max = 200 })
               },
               new GrowthByWidth { Height= 128, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 21.4 }, new Measurement { Min = 21.5, Max = 22.4 }, new Measurement { Min = 22.5, Max = 31.3 },
                   new Measurement { Min = 31.4, Max = 33.3 }, new Measurement { Min = 33.4, Max = 37.1 }, new Measurement { Min = 37.2, Max = 200 })
               },
               new GrowthByWidth { Height= 129, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 21.8 }, new Measurement { Min = 21.9, Max = 22.8 }, new Measurement { Min = 22.9, Max = 32.1 },
                   new Measurement { Min = 32.2, Max = 34.1 }, new Measurement { Min = 34.2, Max = 38.1 }, new Measurement { Min = 38.2, Max = 200 })
               },
               new GrowthByWidth { Height= 130, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 22.1 }, new Measurement { Min = 22.2, Max = 23.2 }, new Measurement { Min = 23.3, Max = 23.9 },
                   new Measurement { Min = 33, Max = 35 }, new Measurement { Min = 35.1, Max = 39.2 }, new Measurement { Min = 39.3, Max = 200 })
               },
               new GrowthByWidth { Height= 131, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 22.5 }, new Measurement { Min = 22.6, Max = 23.7 }, new Measurement { Min = 23.8, Max = 33.7 },
                   new Measurement { Min = 33.8, Max = 35.9 }, new Measurement { Min = 36, Max = 40.3 }, new Measurement { Min = 40.4, Max = 200 })
               },
               new GrowthByWidth { Height= 132, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 23 }, new Measurement { Min = 23.1, Max = 24.2 }, new Measurement { Min = 24.3, Max = 34.6 },
                   new Measurement { Min = 34.7, Max = 36.8 }, new Measurement { Min = 36.9, Max = 41.4 }, new Measurement { Min = 41.5, Max = 200 })
               },
               new GrowthByWidth { Height= 133, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 23.4 }, new Measurement { Min = 23.5, Max = 24.6 }, new Measurement { Min = 24.7, Max = 35.4 },
                   new Measurement { Min = 35.5, Max = 37.8 }, new Measurement { Min = 37.9, Max = 42.5 }, new Measurement { Min = 42.6, Max = 200 })
               },
               new GrowthByWidth { Height= 134, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 23.8 }, new Measurement { Min = 23.9, Max = 25.1 }, new Measurement { Min = 25.2, Max = 36.2 },
                   new Measurement { Min = 36.3, Max = 38.7 }, new Measurement { Min = 38.8, Max = 43.6 }, new Measurement { Min = 43.7, Max = 200 })
               },
               new GrowthByWidth { Height= 135, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 24.3 }, new Measurement { Min = 24.4, Max = 25.6 }, new Measurement { Min = 25.7, Max = 37.1 },
                   new Measurement { Min = 37.2, Max = 39.6 }, new Measurement { Min = 39.7, Max = 44.7 }, new Measurement { Min = 44.8, Max = 200 })
               },
               new GrowthByWidth { Height= 136, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 24.7 }, new Measurement { Min = 24.8, Max = 26 }, new Measurement { Min = 26.1, Max = 37.9 },
                   new Measurement { Min = 38, Max = 40.5 }, new Measurement { Min = 40.6, Max = 45.7 }, new Measurement { Min = 45.8, Max = 200 })
               },
               new GrowthByWidth { Height= 137, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 25.2 }, new Measurement { Min = 25.3, Max = 26.5 }, new Measurement { Min = 26.6, Max = 38.8 },
                   new Measurement { Min = 38.9, Max = 41.4 }, new Measurement { Min = 41.5, Max = 46.8 }, new Measurement { Min = 46.9, Max = 200 })
               },
               new GrowthByWidth { Height= 138, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 25.6 }, new Measurement { Min = 25.7, Max = 27 }, new Measurement { Min = 27.1, Max = 39.6 },
                   new Measurement { Min = 39.7, Max = 42.4 }, new Measurement { Min = 42.5, Max = 47.8 }, new Measurement { Min = 47.9, Max = 200 })
               },
               new GrowthByWidth { Height= 139, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 26.1 }, new Measurement { Min = 26.2, Max = 27.5 }, new Measurement { Min = 27.6, Max = 40.4 },
                   new Measurement { Min = 40.5, Max = 43.2 }, new Measurement { Min = 43.3, Max = 48.7 }, new Measurement { Min = 48.8, Max = 200 })
               },
               new GrowthByWidth { Height= 140, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 26.5 }, new Measurement { Min = 26.6, Max = 28.1 }, new Measurement { Min = 28.2, Max = 41.2 },
                   new Measurement { Min = 41.3, Max = 44.1 }, new Measurement { Min = 44.2, Max = 49.8 }, new Measurement { Min = 49.9, Max = 200 })
               },
               new GrowthByWidth { Height= 141, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 27 }, new Measurement { Min = 27.1, Max = 28.6 }, new Measurement { Min = 28.7, Max = 42 },
                   new Measurement { Min = 42.1, Max = 44.9 }, new Measurement { Min = 45, Max = 50.7 }, new Measurement { Min = 50.8, Max = 200 })
               },
               new GrowthByWidth { Height= 142, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 27.5 }, new Measurement { Min = 27.6, Max = 29.1 }, new Measurement { Min = 29.2, Max = 42.8 },
                   new Measurement { Min = 42.9, Max = 45.7 }, new Measurement { Min = 45.8, Max = 51.5 }, new Measurement { Min = 51.6, Max = 200 })
               },
               new GrowthByWidth { Height= 143, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 28 }, new Measurement { Min = 28.1, Max = 29.6 }, new Measurement { Min = 29.7, Max = 43.5 },
                   new Measurement { Min = 43.6, Max = 46.5 }, new Measurement { Min = 46.6, Max = 52.5 }, new Measurement { Min = 52.6, Max = 200 })
               },
               new GrowthByWidth { Height= 144, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 28.5 }, new Measurement { Min = 28.6, Max = 30.2 }, new Measurement { Min = 30.3, Max = 44.3 },
                   new Measurement { Min = 44.4, Max = 47.3 }, new Measurement { Min = 47.4, Max = 53.4 }, new Measurement { Min = 53.5, Max = 200 })
               },
               new GrowthByWidth { Height= 145, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 29.1 }, new Measurement { Min = 29.2, Max = 30.8 }, new Measurement { Min = 30.9, Max = 45.2 },
                   new Measurement { Min = 45.3, Max = 48.2 }, new Measurement { Min = 48.3, Max = 54.4 }, new Measurement { Min = 54.5, Max = 200 })
               },
               new GrowthByWidth { Height= 146, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 29.6 }, new Measurement { Min = 29.7, Max = 31.3 }, new Measurement { Min = 31.4, Max = 45.9 },
                   new Measurement { Min = 46, Max = 49 }, new Measurement { Min = 49.1, Max = 55.3 }, new Measurement { Min = 55.4, Max = 200 })
               },
               new GrowthByWidth { Height= 147, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 30.2 }, new Measurement { Min = 30.3, Max = 31.9 }, new Measurement { Min = 32, Max = 46.7 },
                   new Measurement { Min = 46.8, Max = 49.9 }, new Measurement { Min = 50.5, Max = 56.2 }, new Measurement { Min = 56.3, Max = 200 })
               },
               new GrowthByWidth { Height= 148, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 30.8 }, new Measurement { Min = 30.9, Max = 32.5 }, new Measurement { Min = 32.6, Max = 47.6 },
                   new Measurement { Min = 47.7, Max = 50.8 }, new Measurement { Min = 50.9, Max = 57.2 }, new Measurement { Min = 57.3, Max = 200 })
               },
               new GrowthByWidth { Height= 149, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 31.4 }, new Measurement { Min = 31.5, Max = 33.2 }, new Measurement { Min = 33.3, Max = 48.4 },
                   new Measurement { Min = 48.5, Max = 51.6 }, new Measurement { Min = 51.7, Max = 58 }, new Measurement { Min = 58.1, Max = 200 })
               },
               new GrowthByWidth { Height= 150, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 32 }, new Measurement { Min = 32.1, Max = 33.8 }, new Measurement { Min = 33.9, Max = 49.1 },
                   new Measurement { Min = 49.2, Max = 52.4 }, new Measurement { Min = 52.5, Max = 58.9 }, new Measurement { Min = 59, Max = 200 })
               },
               new GrowthByWidth { Height= 151, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 32.7 }, new Measurement { Min = 32.8, Max = 34.5 }, new Measurement { Min = 34.6, Max = 49.9 },
                   new Measurement { Min = 50, Max = 53.2 }, new Measurement { Min = 53.3, Max = 59.7 }, new Measurement { Min = 59.8, Max = 200 })
               },
               new GrowthByWidth { Height= 152, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 33.2 }, new Measurement { Min = 33.3, Max = 35.1 }, new Measurement { Min = 35.2, Max = 50.7 },
                   new Measurement { Min = 50.8, Max = 54 }, new Measurement { Min = 54.1, Max = 60.5 }, new Measurement { Min = 60.6, Max = 200 })
               },
               new GrowthByWidth { Height= 153, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 33.9 }, new Measurement { Min = 34, Max = 35.9 }, new Measurement { Min = 36, Max = 51.5 },
                   new Measurement { Min = 51.6, Max = 54.8 }, new Measurement { Min = 54.9, Max = 61.3 }, new Measurement { Min = 61.4, Max = 200 })
               },
               new GrowthByWidth { Height= 154, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 34.5 }, new Measurement { Min = 34.6, Max = 36.5 }, new Measurement { Min = 36.6, Max = 52.3 },
                   new Measurement { Min = 52.4, Max = 55.6 }, new Measurement { Min = 55.7, Max = 62.1 }, new Measurement { Min = 62.2, Max = 200 })
               },
               new GrowthByWidth { Height= 155, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 35.2 }, new Measurement { Min = 35.3, Max = 37.2 }, new Measurement { Min = 37.3, Max = 53.1 },
                   new Measurement { Min = 53.2, Max = 56.4 }, new Measurement { Min = 56.5, Max = 62.9 }, new Measurement { Min = 63, Max = 200 })
               },
               new GrowthByWidth { Height= 156, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 35.9 }, new Measurement { Min = 36, Max = 38 }, new Measurement { Min = 38.1, Max = 54 },
                   new Measurement { Min = 54.1, Max = 57.2 }, new Measurement { Min = 57.3, Max = 63.6 }, new Measurement { Min = 63.7, Max = 200 })
               },
               new GrowthByWidth { Height= 157, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 36.6 }, new Measurement { Min = 36.7, Max = 38.7 }, new Measurement { Min = 38.8, Max = 54.8 },
                   new Measurement { Min = 54.9, Max = 58 }, new Measurement { Min = 58.4, Max = 65.3 }, new Measurement { Min = 64.4, Max = 200 })
               },
               new GrowthByWidth { Height= 158, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 37.3 }, new Measurement { Min = 37.4, Max = 39.5 }, new Measurement { Min = 39.6, Max = 55.6 },
                   new Measurement { Min = 55.7, Max = 58.8 }, new Measurement { Min = 58.9, Max = 64.1 }, new Measurement { Min = 65.2, Max = 200 })
               },
               new GrowthByWidth { Height= 159, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 38 }, new Measurement { Min = 38.1, Max = 40.2 }, new Measurement { Min = 40.3, Max = 56.5 },
                   new Measurement { Min = 56.6, Max = 59.7 }, new Measurement { Min = 59.8, Max = 66 }, new Measurement { Min = 66.1, Max = 200 })
               },
               new GrowthByWidth { Height= 160, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 38.6 }, new Measurement { Min = 38.7, Max = 41 }, new Measurement { Min = 41.1, Max = 57.3 },
                   new Measurement { Min = 57.4, Max = 60.4 }, new Measurement { Min = 60.5, Max = 66.7 }, new Measurement { Min = 66.8, Max = 200 })
               },
               new GrowthByWidth { Height= 161, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 39.4 }, new Measurement { Min = 39.5, Max = 41.8 }, new Measurement { Min = 41.9, Max = 58.2 },
                   new Measurement { Min = 58.3, Max = 61.2 }, new Measurement { Min = 61.3, Max = 67.4 }, new Measurement { Min = 67.5, Max = 200 })
               },
               new GrowthByWidth { Height= 162, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 40.1 }, new Measurement { Min = 40.2, Max = 42.5 }, new Measurement { Min = 42.6, Max = 59 },
                   new Measurement { Min = 59.1, Max = 62 }, new Measurement { Min = 62.1, Max = 68.1 }, new Measurement { Min = 68.2, Max = 200 })
               },
               new GrowthByWidth { Height= 163, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 40.8 }, new Measurement { Min = 40.9, Max = 43.3 }, new Measurement { Min = 43.4, Max = 59.8 },
                   new Measurement { Min = 59.9, Max = 62.8 }, new Measurement { Min = 62.9, Max = 68.8 }, new Measurement { Min = 68.9, Max = 200 })
               },
               new GrowthByWidth { Height= 164, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 41.5 }, new Measurement { Min = 41.6, Max = 44 }, new Measurement { Min = 44.1, Max = 60.6 },
                   new Measurement { Min = 60.7, Max = 63.6 }, new Measurement { Min = 63.7, Max = 69.6 }, new Measurement { Min = 69.7, Max = 200 })
               },
               new GrowthByWidth { Height= 165, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 42.2 }, new Measurement { Min = 42.3, Max = 44.8 }, new Measurement { Min = 44.9, Max = 61.5 },
                   new Measurement { Min = 61.6, Max = 64.4 }, new Measurement { Min = 64.5, Max = 70.2 }, new Measurement { Min = 70.3, Max = 200 })
               },
               new GrowthByWidth { Height= 166, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 43 }, new Measurement { Min = 43.1, Max = 45.6 }, new Measurement { Min = 45.7, Max = 62.3 },
                   new Measurement { Min = 62.4, Max = 65.2 }, new Measurement { Min = 65.3, Max = 71 }, new Measurement { Min = 71.1, Max = 200 })
               },
               new GrowthByWidth { Height= 167, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 43.7 }, new Measurement { Min = 43.8, Max = 46.3 }, new Measurement { Min = 46.4, Max = 63.1 },
                   new Measurement { Min = 63.2, Max = 66 }, new Measurement { Min = 66.1, Max = 71.7 }, new Measurement { Min = 71.8, Max = 200 })
               },
               new GrowthByWidth { Height= 168, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 44.4 }, new Measurement { Min = 44.5, Max = 47.1 }, new Measurement { Min = 47.2, Max = 63.9 },
                   new Measurement { Min = 64, Max = 66.8 }, new Measurement { Min = 66.9, Max = 72.5 }, new Measurement { Min = 72.6, Max = 200 })
               },
               new GrowthByWidth { Height= 169, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 45 }, new Measurement { Min = 45.1, Max = 47.8 }, new Measurement { Min = 47.9, Max = 64.7 },
                   new Measurement { Min = 64.8, Max = 67.6 }, new Measurement { Min = 67.7, Max = 73.3 }, new Measurement { Min = 73.4, Max = 200 })
               },
               new GrowthByWidth { Height= 170, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 45.7 }, new Measurement { Min = 45.8, Max = 48.5 }, new Measurement { Min = 48.6, Max = 65.5 },
                   new Measurement { Min = 65.6, Max = 68.3 }, new Measurement { Min = 68.4, Max = 73.8 }, new Measurement { Min = 73.9, Max = 200 })
               },
               new GrowthByWidth { Height= 171, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 46.4 }, new Measurement { Min = 46.5, Max = 49.3 }, new Measurement { Min = 49.4, Max = 66.3 },
                   new Measurement { Min = 66.4, Max = 69.1 }, new Measurement { Min = 69.2, Max = 74.6 }, new Measurement { Min = 74.7, Max = 200 })
               },
               new GrowthByWidth { Height= 172, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 47.1 }, new Measurement { Min = 47.2, Max = 50 }, new Measurement { Min = 50.1, Max = 67 },
                   new Measurement { Min = 67.1, Max = 69.8 }, new Measurement { Min = 69.9, Max = 75.3 }, new Measurement { Min = 75.4, Max = 200 })
               },
               new GrowthByWidth { Height= 173, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 47.9 }, new Measurement { Min = 48, Max = 50.8 }, new Measurement { Min = 50.9, Max = 67.7 },
                   new Measurement { Min = 67.8, Max = 70.5 }, new Measurement { Min = 70.6, Max = 75.9 }, new Measurement { Min = 76, Max = 200 })
               },
               new GrowthByWidth { Height= 174, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 48.6 }, new Measurement { Min = 48.7, Max = 51.5 }, new Measurement { Min = 51.6, Max = 68.5 },
                   new Measurement { Min = 68.6, Max = 71.1 }, new Measurement { Min = 71.2, Max = 76.5 }, new Measurement { Min = 76.6, Max = 200 })
               },
               new GrowthByWidth { Height= 175, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 49.4 }, new Measurement { Min = 49.5, Max = 52.3 }, new Measurement { Min = 52.4, Max = 69.2 },
                   new Measurement { Min = 69.3, Max = 71.8 }, new Measurement { Min = 71.9, Max = 77.2 }, new Measurement { Min = 77.3, Max = 200 })
               },
               new GrowthByWidth { Height= 176, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 50.1 }, new Measurement { Min = 50.2, Max = 53 }, new Measurement { Min = 53.1, Max = 69.9 },
                   new Measurement { Min = 70, Max = 72.5 }, new Measurement { Min = 72.6, Max = 77.9 }, new Measurement { Min = 78, Max = 200 })
               },
               new GrowthByWidth { Height= 177, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 50.9 }, new Measurement { Min = 51, Max = 53.8 }, new Measurement { Min = 53.9, Max = 70.6 },
                   new Measurement { Min = 70.7, Max = 73.2 }, new Measurement { Min = 73.3, Max = 78.6 }, new Measurement { Min = 78.7, Max = 200 })
               },
               new GrowthByWidth { Height= 178, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 51.7 }, new Measurement { Min = 51.8, Max = 54.6 }, new Measurement { Min = 54.7, Max = 71.2 },
                   new Measurement { Min = 71.3, Max = 73.8 }, new Measurement { Min = 73.9, Max = 79.1 }, new Measurement { Min = 79.2, Max = 200 })
               },
               new GrowthByWidth { Height= 179, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 52.5 }, new Measurement { Min = 52.6, Max = 55.4 }, new Measurement { Min = 55.5, Max = 71.9 },
                   new Measurement { Min = 72, Max = 74.5 }, new Measurement { Min = 74.6, Max = 79.8 }, new Measurement { Min = 79.9, Max = 200 })
               },
               new GrowthByWidth { Height= 180, LookUp = new Lookup("0",
                   new Measurement { Min = 1, Max = 53.3 }, new Measurement { Min = 53.4, Max = 56.1 }, new Measurement { Min = 56.2, Max = 72.4 },
                   new Measurement { Min = 72.5, Max = 75 }, new Measurement { Min = 75.1, Max = 80.2 }, new Measurement { Min = 80.3, Max = 200 })
               },

               //หญิง
               new GrowthByWidth { Height= 85, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 9.7 }, new Measurement { Min = 9.8, Max =  10.1 }, new Measurement { Min = 10.2, Max=  13.5 },
                   new Measurement { Min = 13.6, Max = 14.2 }, new Measurement { Min = 14.3, Max = 15.6 }, new Measurement { Min = 15.7, Max = 200 })
               },
               new GrowthByWidth { Height= 86, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max= 9.9 }, new Measurement { Min = 10, Max=  10.3 }, new Measurement { Min = 10.4, Max=  13.8 },
                   new Measurement { Min = 13.9, Max = 14.5 }, new Measurement { Min = 14.6, Max = 15.9 }, new Measurement { Min = 16, Max = 200 })
               },
               new GrowthByWidth { Height= 87, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 10.1 }, new Measurement { Min = 10.2, Max=  10.5 }, new Measurement { Min = 10.6, Max=  14 },
                   new Measurement { Min = 14.1, Max = 14.7 }, new Measurement { Min = 14.8, Max = 16 }, new Measurement { Min = 16.1, Max = 200 })
               },
               new GrowthByWidth { Height= 88, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 10.3 }, new Measurement { Min = 10.4, Max = 10.7 }, new Measurement { Min = 10.8, Max = 14.3 },
                   new Measurement { Min = 14.4, Max = 15 }, new Measurement { Min = 15.1, Max = 16.4 }, new Measurement { Min = 16.5, Max = 200 })
               },
               new GrowthByWidth { Height= 89, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 10.5 }, new Measurement { Min = 10.6, Max = 10.9 }, new Measurement { Min = 11, Max = 14.6 },
                   new Measurement { Min = 14.7, Max = 15.3 }, new Measurement { Min = 15.4, Max = 16.7 }, new Measurement { Min = 16.8, Max = 200 })
               },
               new GrowthByWidth { Height= 90, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 10.7 }, new Measurement { Min = 10.8, Max = 11.2 }, new Measurement { Min = 11.3, Max = 14.8 },
                   new Measurement { Min = 14.9, Max = 15.5 }, new Measurement { Min = 15.6, Max = 16.8 }, new Measurement { Min = 16.9, Max = 200 })
               },
               new GrowthByWidth { Height= 91, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 10.9 }, new Measurement { Min = 11, Max = 11.4 }, new Measurement { Min = 11.5, Max = 15.1 },
                   new Measurement { Min = 15.2, Max = 15.8 }, new Measurement { Min = 15.9, Max = 17.7 }, new Measurement { Min = 17.2, Max = 200 })
               },
               new GrowthByWidth { Height= 92, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 11.1 }, new Measurement { Min = 11.2, Max = 11.6 }, new Measurement { Min = 11.7, Max = 15.4 },
                   new Measurement { Min = 15.5, Max = 16.1 }, new Measurement { Min = 16.2, Max = 17.5 }, new Measurement { Min = 17.6, Max = 200 })
               },
               new GrowthByWidth { Height= 93, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 11.3 }, new Measurement { Min = 11.4, Max = 11.8 }, new Measurement { Min = 11.9, Max = 15.7 },
                   new Measurement { Min = 15.8, Max = 16.4 }, new Measurement { Min = 16.5, Max=  17.8 }, new Measurement { Min = 17.9, Max = 200 })
               },
               new GrowthByWidth { Height= 94, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 11.5 }, new Measurement { Min = 11.6, Max = 12 }, new Measurement { Min = 12.1, Max = 16 },
                   new Measurement { Min = 16.1, Max = 16.7 }, new Measurement { Min = 16.8, Max=  18.1 }, new Measurement { Min = 18.2, Max = 200 })
               },
               new GrowthByWidth { Height= 95, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 11.7 }, new Measurement { Min = 11.8, Max = 12.2 }, new Measurement { Min = 12.3, Max = 16.2 },
                   new Measurement { Min = 16.3, Max = 17 }, new Measurement { Min = 17.1, Max=  18.5 }, new Measurement { Min = 18.6, Max = 200 })
               },
               new GrowthByWidth { Height= 96, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 12 }, new Measurement { Min = 12.1, Max = 12.5 }, new Measurement { Min = 12.6, Max = 16.5 },
                   new Measurement { Min = 16.6, Max = 17.3 }, new Measurement { Min = 17.4, Max=  18.8 }, new Measurement { Min = 18.9, Max = 200 })
               },
               new GrowthByWidth { Height= 97, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 12.2 }, new Measurement { Min = 12.3, Max = 12.7 }, new Measurement { Min = 12.8, Max = 16.9 },
                   new Measurement { Min = 17, Max = 17.7 }, new Measurement { Min = 17.8, Max=  19.2 }, new Measurement { Min = 19.3, Max = 200 })
               },
               new GrowthByWidth { Height= 98, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 12.4 }, new Measurement { Min = 12.5, Max = 12.9 }, new Measurement { Min = 13, Max = 17.2 },
                   new Measurement { Min = 17.3, Max = 18 }, new Measurement { Min = 18.1, Max=  19.6 }, new Measurement { Min = 19.7, Max = 200 })
               },
               new GrowthByWidth { Height= 99, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 12.6 }, new Measurement { Min = 12.7, Max = 13.2 }, new Measurement { Min = 13.3, Max = 17.6 },
                   new Measurement { Min = 17.7, Max = 18.4 }, new Measurement { Min = 18.5, Max=  20 }, new Measurement { Min = 20.1, Max = 200 })
               },
               new GrowthByWidth { Height= 100, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 12.9 }, new Measurement { Min = 13, Max = 13.4 }, new Measurement { Min = 13.5, Max = 17.9 },
                   new Measurement { Min = 18, Max = 18.7 }, new Measurement { Min = 18.8, Max=  20.4 }, new Measurement { Min = 20.8, Max = 200 })
               },
               new GrowthByWidth { Height= 101, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 13.1 }, new Measurement { Min = 13.2, Max = 13.7 }, new Measurement { Min = 13.8, Max = 18.2 },
                   new Measurement { Min = 18.3, Max = 19.1 }, new Measurement { Min = 19.2, Max=  20.9 }, new Measurement { Min = 21, Max = 200 })
               },
               new GrowthByWidth { Height= 102, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 13.3 }, new Measurement { Min = 13.4, Max = 13.9 }, new Measurement { Min = 14, Max = 18.5 },
                   new Measurement { Min = 18.6, Max = 19.4 }, new Measurement { Min = 19.5, Max=  21.2 }, new Measurement { Min = 21.3, Max = 200 })
               },
               new GrowthByWidth { Height= 103, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 13.4 }, new Measurement { Min = 13.5, Max = 14.1 }, new Measurement { Min = 14.2, Max = 18.9 },
                   new Measurement { Min = 19, Max = 19.8 }, new Measurement { Min = 19.9, Max=  21.7 }, new Measurement { Min = 21.8, Max = 200 })
               },
               new GrowthByWidth { Height= 104, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 13.7 }, new Measurement { Min = 13.8, Max = 14.4 }, new Measurement { Min = 14.5, Max = 19.2 },
                   new Measurement { Min = 19.3, Max = 20.1 }, new Measurement { Min = 20.2, Max=  22 }, new Measurement { Min = 22.1, Max = 200 })
               },
               new GrowthByWidth { Height= 105, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 13.9 }, new Measurement { Min = 14, Max = 14.6 }, new Measurement { Min = 14.7, Max = 19.6 },
                   new Measurement { Min = 19.7, Max = 20.5 }, new Measurement { Min = 20.6, Max=  22.4 }, new Measurement { Min = 22.5, Max = 200 })
               },
               new GrowthByWidth { Height= 106, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 14.2 }, new Measurement { Min = 14.3, Max = 14.9 }, new Measurement { Min = 15, Max = 20 },
                   new Measurement { Min = 20.1, Max = 20.9 }, new Measurement { Min = 21, Max=  22.9 }, new Measurement { Min = 23, Max = 200 })
               },
               new GrowthByWidth { Height= 107, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 14.4 }, new Measurement { Min = 14.5, Max = 15.1 }, new Measurement { Min = 15.2, Max = 20.3 },
                   new Measurement { Min = 20.4, Max = 21.3 }, new Measurement { Min = 21.4, Max=  23.4 }, new Measurement { Min = 23.5, Max = 200 })
               },
               new GrowthByWidth { Height= 108, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 14.7 }, new Measurement { Min = 14.8, Max = 15.4 }, new Measurement { Min = 15.5, Max = 20.7 },
                   new Measurement { Min = 20.8, Max = 21.7 }, new Measurement { Min = 21.8, Max = 23.8 }, new Measurement { Min = 23.9, Max = 200 })
               },
               new GrowthByWidth { Height= 109, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 14.9 }, new Measurement { Min = 15, Max = 15.6 }, new Measurement { Min = 15.7, Max = 21.1 },
                   new Measurement { Min = 21.2, Max = 22.2 }, new Measurement { Min = 22.3, Max = 24.3 }, new Measurement { Min = 24.4, Max = 200 })
               },
               new GrowthByWidth { Height= 110, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 15.2 }, new Measurement { Min = 15.3, Max = 15.9 }, new Measurement { Min = 16, Max = 21.5 },
                   new Measurement { Min = 21.6, Max = 22.6 }, new Measurement { Min = 22.7, Max = 24.7 }, new Measurement { Min = 24.9, Max = 200 })
               },
               new GrowthByWidth { Height= 111, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 15.4 }, new Measurement { Min = 15.5, Max = 16.2 }, new Measurement { Min = 16.3, Max = 21.9 },
                   new Measurement { Min = 22, Max = 23.1 }, new Measurement { Min = 23.5, Max=  25.4 }, new Measurement { Min = 25.5, Max = 200 })
               },
               new GrowthByWidth { Height= 112, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 15.7 }, new Measurement { Min = 15.8, Max = 16.5 }, new Measurement { Min = 16.6, Max = 22.3 },
                   new Measurement { Min = 22.4, Max = 23.5 }, new Measurement { Min = 23.6, Max = 25.8 }, new Measurement { Min = 25.9, Max = 200 })
               },
               new GrowthByWidth { Height= 113, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 16 }, new Measurement { Min = 16.1, Max = 16.8 }, new Measurement { Min = 16.9, Max = 22.8 },
                   new Measurement { Min = 22.9, Max = 24 }, new Measurement { Min = 24.1, Max = 26.4 }, new Measurement { Min = 26.5, Max = 200 })
               },
               new GrowthByWidth { Height= 114, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 16.3 }, new Measurement { Min = 16.4, Max = 17.1 }, new Measurement { Min = 17.2, Max = 23.2 },
                   new Measurement { Min = 23.3, Max = 24.5 }, new Measurement { Min = 24.6, Max = 27 }, new Measurement { Min = 27.1, Max = 200 })
               },
               new GrowthByWidth { Height= 115, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 16.6 }, new Measurement { Min = 16.7, Max = 17.4 }, new Measurement { Min = 17.5, Max = 23.7 },
                   new Measurement { Min = 23.8, Max = 25 }, new Measurement { Min = 25.1, Max = 27.6 }, new Measurement { Min = 27.7, Max = 200 })
               },
               new GrowthByWidth { Height= 116, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 16.8 }, new Measurement { Min = 16.9, Max = 17.6 }, new Measurement { Min = 17.7, Max = 24.3 },
                   new Measurement { Min = 24.4, Max = 25.6 }, new Measurement { Min = 25.7, Max = 28.3 }, new Measurement { Min = 28.4, Max = 200 })
               },
               new GrowthByWidth { Height= 117, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 17.1 }, new Measurement { Min = 17.2, Max = 17.9 }, new Measurement { Min = 18, Max = 24.7 },
                   new Measurement { Min = 24.8, Max = 26.1 }, new Measurement { Min = 26.2, Max = 28.9 }, new Measurement { Min = 29, Max = 200 })
               },
               new GrowthByWidth { Height= 118, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 17.4 }, new Measurement { Min = 17.5, Max = 18.3 }, new Measurement { Min = 18.4, Max = 25.3 },
                   new Measurement { Min = 25.4, Max = 26.7 }, new Measurement { Min = 26.8, Max = 29.6 }, new Measurement { Min = 29.7, Max = 200 })
               },
               new GrowthByWidth { Height= 119, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 17.7 }, new Measurement { Min = 17.8, Max = 18.6 }, new Measurement { Min = 18.7, Max = 25.8 },
                   new Measurement { Min = 25.9, Max = 27.4 }, new Measurement { Min = 27.5, Max = 30.4 }, new Measurement { Min = 30.5, Max = 200 })
               },
               new GrowthByWidth { Height= 120, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 18.1 }, new Measurement { Min = 18.2, Max = 19 }, new Measurement { Min = 19.1, Max = 26.5 },
                   new Measurement { Min = 26.6, Max = 28.1 }, new Measurement { Min = 28.2, Max = 31.3 }, new Measurement { Min = 31.4, Max = 200 })
               },
               new GrowthByWidth { Height= 121, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 18.4 }, new Measurement { Min = 18.5, Max = 19.3 }, new Measurement { Min = 19.4, Max = 27.1 },
                   new Measurement { Min = 27.2, Max = 28.7 }, new Measurement { Min = 28.8, Max = 31.9 }, new Measurement { Min = 32, Max = 200 })
               },
               new GrowthByWidth { Height= 122, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 18.7 }, new Measurement { Min = 18.8, Max = 19.6 }, new Measurement { Min = 19.7, Max = 27.7 },
                   new Measurement { Min = 27.8, Max = 29.4 }, new Measurement { Min = 29.5, Max = 32.8 }, new Measurement { Min = 32.9, Max = 200 })
               },
               new GrowthByWidth { Height= 123, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 19 }, new Measurement { Min = 19.1, Max = 19.9 }, new Measurement { Min = 20, Max = 28.4 },
                   new Measurement { Min = 28.5, Max = 30.2 }, new Measurement { Min = 30.3, Max = 33.8 }, new Measurement { Min = 33.9, Max = 200 })
               },
               new GrowthByWidth { Height= 124, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 19.4 }, new Measurement { Min = 19.5, Max = 20.4 }, new Measurement { Min = 20.5, Max = 29.1 },
                   new Measurement { Min = 29.2, Max = 30.9 }, new Measurement { Min = 31, Max=  34.7 }, new Measurement { Min = 34.8, Max = 200 })
               },
               new GrowthByWidth { Height= 125, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 19.7 }, new Measurement { Min = 19.8, Max = 20.7 }, new Measurement { Min = 20.8, Max = 29.8 },
                   new Measurement { Min = 29.9, Max = 31.8 }, new Measurement { Min = 31.9, Max = 35.7 }, new Measurement { Min = 35.8, Max = 200 })
               },
               new GrowthByWidth { Height= 126, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 19.9 }, new Measurement { Min = 20, Max = 21 }, new Measurement { Min = 21.1, Max = 30.5 },
                   new Measurement { Min = 30.6, Max = 32.6 }, new Measurement { Min = 32.7, Max = 36.7 }, new Measurement { Min = 36.8, Max = 200 })
               },
               new GrowthByWidth { Height= 127, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 20.3 }, new Measurement { Min = 20.4, Max = 21.5 }, new Measurement { Min = 21.6, Max = 31.3 },
                   new Measurement { Min = 31.4, Max = 33.4 }, new Measurement { Min = 33.5, Max = 37.7 }, new Measurement { Min = 37.8, Max = 200 })
               },
               new GrowthByWidth { Height= 128, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 20.6 }, new Measurement { Min = 20.7, Max = 21.8 }, new Measurement { Min = 21.9, Max = 32 },
                   new Measurement { Min = 32.1, Max = 34.2 }, new Measurement { Min = 34.3, Max = 38.6 }, new Measurement { Min = 38.7, Max = 200 })
               },
               new GrowthByWidth { Height= 129, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 21 }, new Measurement { Min = 21.1, Max = 22.2 }, new Measurement { Min = 22.3, Max = 32.8 },
                   new Measurement { Min = 32.9, Max = 35.2 }, new Measurement { Min = 35.8, Max = 39.8 }, new Measurement { Min = 39.9, Max = 200 })
               },
               new GrowthByWidth { Height= 130, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 21.3 }, new Measurement { Min = 21.4, Max = 22.6 }, new Measurement { Min = 22.7, Max = 33.7 },
                   new Measurement { Min = 33.8, Max = 36.1 }, new Measurement { Min = 36.2, Max = 40.9 }, new Measurement { Min = 41, Max = 200 })
               },
               new GrowthByWidth { Height= 131, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 21.7 }, new Measurement { Min = 21.8, Max = 23 }, new Measurement { Min = 23.1, Max = 34.5 },
                   new Measurement { Min = 34.6, Max = 37 }, new Measurement { Min = 37.7, Max = 42 }, new Measurement { Min = 42.1, Max = 200 })
               },
               new GrowthByWidth { Height= 132, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 22.1 }, new Measurement { Min = 22.2, Max = 23.4 }, new Measurement { Min = 23.5, Max = 35.3 },
                   new Measurement { Min = 35.4, Max = 37.9 }, new Measurement { Min = 38, Max = 43.1 }, new Measurement { Min = 43.2, Max = 200 })
               },
               new GrowthByWidth { Height= 133, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 22.5 }, new Measurement { Min = 22.6, Max = 23.9 }, new Measurement { Min = 24, Max = 36.2 },
                   new Measurement { Min = 36.3, Max = 38.8 }, new Measurement { Min = 38.9, Max = 44.2 }, new Measurement { Min = 44.3, Max = 200 })
               },
               new GrowthByWidth { Height= 134, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 22.9 }, new Measurement { Min = 23, Max = 24.3 }, new Measurement { Min = 24.4, Max = 37.1 },
                   new Measurement { Min = 37.2, Max = 39.9 }, new Measurement { Min = 40, Max = 45.4 }, new Measurement { Min = 45.5, Max = 200 })
               },
               new GrowthByWidth { Height= 135, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 23.2 }, new Measurement { Min = 23.3, Max = 24.8 }, new Measurement { Min = 24.9, Max = 38 },
                   new Measurement { Min = 38.1, Max = 40.8 }, new Measurement { Min = 40.9, Max = 46.4 }, new Measurement { Min = 46.5, Max = 200 })
               },
               new GrowthByWidth { Height= 136, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 23.7 }, new Measurement { Min = 23.8, Max = 25.3 }, new Measurement { Min = 25.4, Max = 38.9 },
                   new Measurement { Min = 39, Max = 41.8 }, new Measurement { Min = 41.9, Max = 47.6 }, new Measurement { Min = 47.7, Max = 200 })
               },
               new GrowthByWidth { Height= 137, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 24.1 }, new Measurement { Min = 24.2, Max = 25.8 }, new Measurement { Min = 25.9, Max = 39.8 },
                   new Measurement { Min = 39.9, Max = 42.7 }, new Measurement { Min = 42.8, Max = 48.6 }, new Measurement { Min = 48.7, Max = 200 })
               },
               new GrowthByWidth { Height= 138, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 24.6 }, new Measurement { Min = 24.7, Max = 26.3 }, new Measurement { Min = 26.4, Max = 40.6 },
                   new Measurement { Min = 40.7, Max = 43.6 }, new Measurement { Min = 43.7, Max = 49.6 }, new Measurement { Min = 49.7, Max = 200 })
               },
               new GrowthByWidth { Height= 139, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 25.1 }, new Measurement { Min = 25.2, Max = 26.9 }, new Measurement { Min = 27, Max = 41.6 },
                   new Measurement { Min = 41.7, Max = 44.6 }, new Measurement { Min = 44.7, Max = 50.7 }, new Measurement { Min = 50.8, Max = 200 })
               },
               new GrowthByWidth { Height= 140, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 25.7 }, new Measurement { Min = 25.8, Max = 27.5 }, new Measurement { Min = 27.6, Max = 42.4 },
                   new Measurement { Min = 42.5, Max = 45.5 }, new Measurement { Min = 45.6, Max = 51.8 }, new Measurement { Min = 51.9, Max = 200 })
               },
               new GrowthByWidth { Height= 141, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 26.1 }, new Measurement { Min = 26.2, Max = 28.1 }, new Measurement { Min = 28.2, Max = 43.4 },
                   new Measurement { Min = 43.5, Max = 46.6 }, new Measurement { Min = 46.7, Max = 52.9 }, new Measurement { Min = 53, Max = 200 })
               },
               new GrowthByWidth { Height= 142, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 26.7 }, new Measurement { Min = 26.8, Max = 28.7 }, new Measurement { Min = 28.8, Max = 44.3 },
                   new Measurement { Min = 44.4, Max = 47.5 }, new Measurement { Min = 47.6, Max = 53.8 }, new Measurement { Min = 53.9, Max = 200 })
               },
               new GrowthByWidth { Height= 143, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 27.3 }, new Measurement { Min = 27.4, Max = 29.4 }, new Measurement { Min = 29.5, Max = 45.2 },
                   new Measurement { Min = 45.3, Max = 48.4 }, new Measurement { Min = 48.5, Max = 54.7 }, new Measurement { Min = 54.8, Max = 200 })
               },
               new GrowthByWidth { Height= 144, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 28 }, new Measurement { Min = 28.1, Max = 30.1 }, new Measurement { Min = 30.2, Max = 46.1 },
                   new Measurement { Min = 46.2, Max = 49.3 }, new Measurement { Min = 49.4, Max = 55.7 }, new Measurement { Min = 55.8, Max = 200 })
               },
               new GrowthByWidth { Height= 145, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 28.6 }, new Measurement { Min = 28.7, Max = 30.7 }, new Measurement { Min = 30.8, Max = 47 },
                   new Measurement { Min = 47.1, Max = 50.3 }, new Measurement { Min = 50.4, Max = 56.8 }, new Measurement { Min = 56.9, Max = 200 })
               },
               new GrowthByWidth { Height= 146, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 29.3 }, new Measurement { Min = 29.4, Max = 31.5 }, new Measurement { Min = 31.6, Max = 47.9 },
                   new Measurement { Min = 48, Max = 51.2 }, new Measurement { Min = 51.3, Max = 57.7 }, new Measurement { Min = 57.8, Max = 200 })
               },
               new GrowthByWidth { Height= 147, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 30.1 }, new Measurement { Min = 30.2, Max = 32.3 }, new Measurement { Min = 32.4, Max = 48.8 },
                   new Measurement { Min = 48.9, Max = 52.1 }, new Measurement { Min = 52.2, Max = 58.6 }, new Measurement { Min = 58.7, Max = 200 })
               },
               new GrowthByWidth { Height= 148, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 30.8 }, new Measurement { Min = 30.9, Max = 33 }, new Measurement { Min = 33.1, Max = 49.7 },
                   new Measurement { Min = 49.8, Max = 53 }, new Measurement { Min = 53.1, Max = 59.5 }, new Measurement { Min = 59.6, Max = 200 })
               },
               new GrowthByWidth { Height= 149, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 31.4 }, new Measurement { Min = 31.5, Max = 33.8 }, new Measurement { Min = 33.9, Max = 50.5 },
                   new Measurement { Min = 50.6, Max = 53.8 }, new Measurement { Min = 53.9, Max = 60.3 }, new Measurement { Min = 60.4, Max = 200 })
               },
               new GrowthByWidth { Height= 150, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 32.2 }, new Measurement { Min = 32.3, Max = 34.6 }, new Measurement { Min = 34.7, Max = 51.4 },
                   new Measurement { Min = 51.5, Max = 54.7 }, new Measurement { Min = 54.8, Max = 61.2 }, new Measurement { Min = 61.3, Max = 200 })
               },
               new GrowthByWidth { Height= 151, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 33 }, new Measurement { Min = 33.1, Max = 35.4 }, new Measurement { Min = 35.5, Max = 52.2 },
                   new Measurement { Min = 52.3, Max = 55.5 }, new Measurement { Min = 55.6, Max = 62 }, new Measurement { Min = 62.1, Max = 200 })
               },
               new GrowthByWidth { Height= 152, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 33.7 }, new Measurement { Min = 33.8, Max = 36.1 }, new Measurement { Min = 36.2, Max = 53 },
                   new Measurement { Min = 53.1, Max = 56.3 }, new Measurement { Min = 56.4, Max = 62.8 }, new Measurement { Min = 62.9, Max = 200 })
               },
               new GrowthByWidth { Height= 153, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 34.5 }, new Measurement { Min = 34.6, Max = 36.9 }, new Measurement { Min = 37, Max = 53.8 },
                   new Measurement { Min = 53.9, Max = 57.1 }, new Measurement { Min = 57.2, Max = 63.6 }, new Measurement { Min = 63.7, Max = 200 })
               },
               new GrowthByWidth { Height= 154, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 35.2 }, new Measurement { Min = 35.3, Max = 37.6 }, new Measurement { Min = 37.7, Max = 54.6 },
                   new Measurement { Min = 54.7, Max = 57.9 }, new Measurement { Min = 58, Max = 64.4 }, new Measurement { Min = 64.5, Max = 200 })
               },
               new GrowthByWidth { Height= 155, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 35.9 }, new Measurement { Min = 36, Max = 38.3 }, new Measurement { Min = 38.4, Max = 55.4 },
                   new Measurement { Min = 55.5, Max = 58.7 }, new Measurement { Min = 58.7, Max = 65.2 }, new Measurement { Min = 65.3, Max = 200 })
               },
               new GrowthByWidth { Height= 156, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 36.9 }, new Measurement { Min = 36.7, Max = 39.1 }, new Measurement { Min = 39.2, Max = 56.2 },
                   new Measurement { Min = 56.3, Max = 59.4 }, new Measurement { Min = 59.5, Max = 65.8 }, new Measurement { Min = 65.9, Max = 200 })
               },
               new GrowthByWidth { Height= 157, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 37.3 }, new Measurement { Min = 37.4, Max = 39.8 }, new Measurement { Min = 39.9, Max = 56.9 },
                   new Measurement { Min = 57, Max = 60.1 }, new Measurement { Min = 60.2, Max = 66.4 }, new Measurement { Min = 66.5, Max = 200 })
               },
               new GrowthByWidth { Height= 158, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 38 }, new Measurement { Min = 38.1, Max = 40.5 }, new Measurement { Min = 40.6, Max = 57.6 },
                   new Measurement { Min = 57.7, Max = 60.8 }, new Measurement { Min = 60.9, Max = 67.1 }, new Measurement { Min = 67.2, Max = 200 })
               },
               new GrowthByWidth { Height= 159, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 38.7 }, new Measurement { Min = 38.8, Max = 41.2 }, new Measurement { Min = 41.3, Max = 58.3 },
                   new Measurement { Min = 58.4, Max = 61.4 }, new Measurement { Min = 61.5, Max = 67.7 }, new Measurement { Min = 67.8, Max = 200 })
               },
               new GrowthByWidth { Height= 160, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 39.4 }, new Measurement { Min = 39.5, Max = 41.9 }, new Measurement { Min = 42, Max = 59 },
                   new Measurement { Min = 59.1, Max = 62.1 }, new Measurement { Min = 62.2, Max = 68.4 }, new Measurement { Min = 68.5, Max = 200 })
               },
               new GrowthByWidth { Height= 161, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 39.4 }, new Measurement { Min = 39.5, Max = 41.8 }, new Measurement { Min = 41.9, Max = 58.2 },
                   new Measurement { Min = 58.3, Max = 61.2 }, new Measurement { Min = 61.3, Max = 67.4 }, new Measurement { Min = 67.5, Max = 200 })
               },
               new GrowthByWidth { Height= 162, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 40.7 }, new Measurement { Min = 40.8, Max = 43.3 }, new Measurement { Min = 43.4, Max = 60.3 },
                   new Measurement { Min = 60.4, Max = 63.3 }, new Measurement { Min = 63.4, Max = 69.3 }, new Measurement { Min = 69.4, Max = 200 })
               },
               new GrowthByWidth { Height= 163, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 41.4 }, new Measurement { Min = 41.5, Max = 44.1 }, new Measurement { Min = 44.2, Max = 61.1 },
                   new Measurement { Min = 61.2, Max = 64 }, new Measurement { Min = 64.1, Max = 69.8 }, new Measurement { Min = 69.9, Max = 200 })
               },
               new GrowthByWidth { Height= 164, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 42 }, new Measurement { Min = 42.1, Max = 44.8 }, new Measurement { Min = 44.9, Max = 61.7 },
                   new Measurement { Min = 61.8, Max = 64.6 }, new Measurement { Min = 64.7, Max = 70.3 }, new Measurement { Min = 70.4, Max = 200 })
               },
               new GrowthByWidth { Height= 165, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 42.7 }, new Measurement { Min = 42.8, Max = 45.5 }, new Measurement { Min = 45.6, Max = 62.3 },
                   new Measurement { Min = 62.4, Max = 65.1 }, new Measurement { Min = 65.2, Max = 70.6 }, new Measurement { Min = 70.7, Max = 200 })
               },
               new GrowthByWidth { Height= 166, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 43.5 }, new Measurement { Min = 43.6, Max = 46.3 }, new Measurement { Min = 46.4, Max = 63 },
                   new Measurement { Min = 63.1, Max = 65.6 }, new Measurement { Min = 65.7, Max = 71 }, new Measurement { Min = 71.1, Max = 200 })
               },
               new GrowthByWidth { Height= 167, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 44.2 }, new Measurement { Min = 44.3, Max = 47.1 }, new Measurement { Min = 47.2, Max = 63.6 },
                   new Measurement { Min = 63.7, Max = 66.2 }, new Measurement { Min = 66.3, Max = 71.4 }, new Measurement { Min = 71.5, Max = 200 })
               },
               new GrowthByWidth { Height= 168, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 45 }, new Measurement { Min = 45.1, Max = 47.9 }, new Measurement { Min = 48, Max = 64.2 },
                   new Measurement { Min = 64.3, Max = 66.8 }, new Measurement { Min = 66.8, Max = 71.6 }, new Measurement { Min = 71.7, Max = 200 })
               },
               new GrowthByWidth { Height= 169, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 45.8 }, new Measurement { Min = 45.9, Max = 48.7 }, new Measurement { Min = 48.8, Max = 64.9 },
                   new Measurement { Min = 65, Max = 67.3 }, new Measurement { Min = 67.4, Max = 72.1 }, new Measurement { Min = 72.2, Max = 200 })
               },
               new GrowthByWidth { Height= 170, LookUp = new Lookup("1",
                   new Measurement { Min = 1, Max = 46.7 }, new Measurement { Min = 46.8, Max = 49.7 }, new Measurement { Min = 49.8, Max = 65.5 },
                   new Measurement { Min = 65.6, Max = 67.7 }, new Measurement { Min = 67.8, Max = 72.3 }, new Measurement { Min = 72.4, Max = 200 })
               }
            };
            return GrowthByWidths;
        }

        public static List<GrowthByHeight> GetgrowthByHeight()
        {
            List<GrowthByHeight> GrowthByHeight = new List<GrowthByHeight>()
            {
                new GrowthByHeight{ Age = 5, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 99.8 }, new Measurement { Min = 99.99, Max = 101.9 }, new Measurement { Min = 102, Max = 115.1 },
                    new Measurement { Min = 115.2, Max = 117.3 }, new Measurement { Min = 117.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 100.3 }, new Measurement { Min = 100.4, Max = 102.4 }, new Measurement { Min = 102.5, Max = 115.6 },
                    new Measurement { Min = 115.7, Max = 117.8 }, new Measurement { Min = 117.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 100.8 }, new Measurement { Min = 100.9, Max = 102.9 }, new Measurement { Min = 103, Max = 116.1 },
                    new Measurement { Min = 116.2, Max = 118.4 }, new Measurement { Min = 118.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 101.3 }, new Measurement { Min = 101.4, Max = 103.4 }, new Measurement { Min = 103.5, Max = 116.7 },
                    new Measurement { Min = 116.8, Max = 118.9 }, new Measurement { Min = 119, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 101.7 }, new Measurement { Min = 101.8, Max = 103.8 }, new Measurement { Min = 103.9, Max = 117.2 },
                    new Measurement { Min = 117.3, Max = 119.4 }, new Measurement { Min = 119.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 102.2 }, new Measurement { Min = 102.3, Max = 104.3 }, new Measurement { Min = 104.4, Max = 117.7 },
                    new Measurement { Min = 117.8, Max = 120 }, new Measurement { Min = 120.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 102.7 }, new Measurement { Min = 102.8, Max = 104.8 }, new Measurement { Min = 104.9, Max = 118.2 },
                    new Measurement { Min = 118.3, Max = 120.5 }, new Measurement { Min = 120.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 103.1 }, new Measurement { Min = 103.2, Max = 105.3 }, new Measurement { Min = 105.4, Max = 118.7 },
                    new Measurement { Min = 118.8, Max = 121 }, new Measurement { Min = 121.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 103.6 }, new Measurement { Min = 103.7, Max = 105.8 }, new Measurement { Min = 105.9, Max = 119.2 },
                    new Measurement { Min = 119.3, Max = 121.5 }, new Measurement { Min = 121.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 104 }, new Measurement { Min = 104.1, Max = 106.2 }, new Measurement { Min = 106.3, Max = 119.8 },
                    new Measurement { Min = 119.9, Max = 122 }, new Measurement { Min = 122.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 104.5 }, new Measurement { Min = 104.6, Max = 106.7 }, new Measurement { Min = 106.8, Max = 120.3 },
                    new Measurement { Min = 120.4, Max = 122.6 }, new Measurement { Min = 122.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 104.9 }, new Measurement { Min = 105, Max = 104.3 }, new Measurement { Min = 104.4, Max = 117.7 },
                    new Measurement { Min = 117.8, Max = 120 }, new Measurement { Min = 120.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 105.3 }, new Measurement { Min = 105.4, Max = 107.6 }, new Measurement { Min = 107.7, Max = 121.3 },
                    new Measurement { Min = 121.4, Max = 123.6 }, new Measurement { Min = 123.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 105.8 }, new Measurement { Min = 105.9, Max = 108 }, new Measurement { Min = 108.1, Max = 121.9 },
                    new Measurement { Min = 122, Max = 124.2 }, new Measurement { Min = 124.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 106.2 }, new Measurement { Min = 106.3, Max = 108.5 }, new Measurement { Min = 108.6, Max = 122.4 },
                    new Measurement { Min = 122.5, Max = 124.8 }, new Measurement { Min = 124.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 106.6 }, new Measurement { Min = 106.7, Max = 108.9 }, new Measurement { Min = 109, Max = 122.9 },
                    new Measurement { Min = 123, Max = 125.3 }, new Measurement { Min = 125.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 107.1 }, new Measurement { Min = 107.2, Max = 109.4 }, new Measurement { Min = 109.5, Max = 123.5 },
                    new Measurement { Min = 123.6, Max = 125.9 }, new Measurement { Min = 126, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 107.4 }, new Measurement { Min = 107.5, Max = 109.8 }, new Measurement { Min = 109.9, Max = 124 },
                    new Measurement { Min = 124.1, Max = 126.4 }, new Measurement { Min = 126.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 107.8 }, new Measurement { Min = 107.9, Max = 110.2 }, new Measurement { Min = 110.3, Max = 124.5 },
                    new Measurement { Min = 124.6, Max = 126.9 }, new Measurement { Min = 127, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 108.2 }, new Measurement { Min = 108.3, Max = 110.6 }, new Measurement { Min = 110.7, Max = 125 },
                    new Measurement { Min = 125.1, Max = 127.4 }, new Measurement { Min = 127.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 108.8 }, new Measurement { Min = 108.9, Max = 111.1 }, new Measurement { Min = 111.2, Max = 125.5 },
                    new Measurement { Min = 125.6, Max = 127.9 }, new Measurement { Min = 128, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 109.1 }, new Measurement { Min = 109.2, Max = 111.5 }, new Measurement { Min = 111.6, Max = 126 },
                    new Measurement { Min = 126.1, Max = 128.4 }, new Measurement { Min = 128.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 109.5 }, new Measurement { Min = 109.6, Max = 111.9 }, new Measurement { Min = 112, Max = 126 },
                    new Measurement { Min = 126.5, Max = 128.9 }, new Measurement { Min = 129, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 109.9 }, new Measurement { Min = 110, Max = 112.3 }, new Measurement { Min = 112.4, Max = 126.9 },
                    new Measurement { Min = 127, Max = 129.4 }, new Measurement { Min = 129.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 110.3 }, new Measurement { Min = 110.4, Max = 112.7 }, new Measurement { Min = 112.8, Max = 127.4 },
                    new Measurement { Min = 127.5, Max = 129.8 }, new Measurement { Min = 129.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 110.7 }, new Measurement { Min = 110.8, Max = 113.1 }, new Measurement { Min = 113.2, Max = 127.8 },
                    new Measurement { Min = 127.9, Max = 130.3 }, new Measurement { Min = 130.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 111 }, new Measurement { Min = 111.1, Max = 113.5 }, new Measurement { Min = 113.6, Max = 128.3 },
                    new Measurement { Min = 128.4, Max = 130.8 }, new Measurement { Min = 130.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 111.4 }, new Measurement { Min = 111.5, Max = 113.9 }, new Measurement { Min = 114, Max = 128.8 },
                    new Measurement { Min = 128.9, Max = 131.3 }, new Measurement { Min = 131.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 111.8 }, new Measurement { Min = 111.9, Max = 114.3 }, new Measurement { Min = 114.4, Max = 129.3 },
                    new Measurement { Min = 129.4, Max = 131.8 }, new Measurement { Min = 131.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 112.2 }, new Measurement { Min = 112.3, Max = 114.7 }, new Measurement { Min = 114.8, Max = 129.8 },
                    new Measurement { Min = 129.9, Max = 132.3 }, new Measurement { Min = 132.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 112.5 }, new Measurement { Min = 112.6, Max = 115 }, new Measurement { Min = 115.1, Max = 130.3 },
                    new Measurement { Min = 130.4, Max = 132.8 }, new Measurement { Min = 132.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 112.9 }, new Measurement { Min = 113, Max = 115.4 }, new Measurement { Min = 115.5, Max = 130.8 },
                    new Measurement { Min = 130.9, Max = 133.3 }, new Measurement { Min = 133.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 113.3 }, new Measurement { Min = 113.4, Max = 115.8 }, new Measurement { Min = 115.9, Max = 131.2 },
                    new Measurement { Min = 131.3, Max = 133.8 }, new Measurement { Min = 133.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 113.6 }, new Measurement { Min = 113.7, Max = 116.2 }, new Measurement { Min = 116.3, Max = 131.7 },
                    new Measurement { Min = 131.8, Max = 134.3 }, new Measurement { Min = 134.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 114 }, new Measurement { Min = 114.1, Max = 116.6 }, new Measurement { Min = 116.7, Max = 132.2 },
                    new Measurement { Min = 132.3, Max = 134.9 }, new Measurement { Min = 135, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 114.4 }, new Measurement { Min = 114.5, Max = 117 }, new Measurement { Min = 117.1, Max = 132.7 },
                    new Measurement { Min = 132.8, Max = 135.4 }, new Measurement { Min = 135.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 114.7 }, new Measurement { Min = 114.8, Max = 117.3 }, new Measurement { Min = 117.4, Max = 133.2 },
                    new Measurement { Min = 133.3, Max = 135.8 }, new Measurement { Min = 135.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 115.1 }, new Measurement { Min = 115.2, Max = 117.7 }, new Measurement { Min = 117.8, Max = 133.6 },
                    new Measurement { Min = 133.7, Max = 136.3 }, new Measurement { Min = 136.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 115.3 }, new Measurement { Min = 115.4, Max = 118 }, new Measurement { Min = 118.1, Max = 134 },
                    new Measurement { Min = 134.1, Max = 136.7 }, new Measurement { Min = 136.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 115.8 }, new Measurement { Min = 115.9, Max = 118.4 }, new Measurement { Min = 118.5, Max = 134.5 },
                    new Measurement { Min = 134.6, Max = 137.1 }, new Measurement { Min = 137.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 116 }, new Measurement { Min = 116.1, Max = 118.7 }, new Measurement { Min = 118.8, Max = 134.9 },
                    new Measurement { Min = 135, Max = 137.6 }, new Measurement { Min = 137.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 116.5 }, new Measurement { Min = 116.6, Max = 119.1 }, new Measurement { Min = 119.2, Max = 135.3 },
                    new Measurement { Min = 135.4, Max = 138 }, new Measurement { Min = 138.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 116.9 }, new Measurement { Min = 117, Max = 119.6 }, new Measurement { Min = 119.7, Max = 135.7 },
                    new Measurement { Min = 135.8, Max = 138.4 }, new Measurement { Min = 138.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 117.2 }, new Measurement { Min = 117.3, Max = 119.9 }, new Measurement { Min = 120, Max = 136.1 },
                    new Measurement { Min = 136.2, Max = 138.8 }, new Measurement { Min = 138.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 117.5 }, new Measurement { Min = 117.6, Max = 120.3 }, new Measurement { Min = 120.4, Max = 136.6 },
                    new Measurement { Min = 136.7, Max = 139.2 }, new Measurement { Min = 139.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 117.8 }, new Measurement { Min = 117.9, Max = 120.6 }, new Measurement { Min = 120.7, Max = 137 },
                    new Measurement { Min = 137.1, Max = 139.7 }, new Measurement { Min = 139.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 118.2 }, new Measurement { Min = 118.3, Max = 121 }, new Measurement { Min = 121.1, Max = 137.4 },
                    new Measurement { Min = 137.5, Max = 140.1 }, new Measurement { Min = 140.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 118.6 }, new Measurement { Min = 118.7, Max = 121.4 }, new Measurement { Min = 121.5, Max = 137.8 },
                    new Measurement { Min = 137.9, Max = 140.5 }, new Measurement { Min = 140.6, Max = 300 })
                },

                new GrowthByHeight{ Age = 9, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 118.9 }, new Measurement { Min = 119, Max = 121.7 }, new Measurement { Min = 121.8, Max = 139.3 },
                    new Measurement { Min = 138.4, Max = 140.9 }, new Measurement { Min = 141, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 119.3 }, new Measurement { Min = 119.4, Max = 121.1 }, new Measurement { Min = 122.2, Max = 138.7 },
                    new Measurement { Min = 138.8, Max = 141.4 }, new Measurement { Min = 141.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 119.6 }, new Measurement { Min = 119.7, Max = 122.5 }, new Measurement { Min = 122.6, Max = 139.1 },
                    new Measurement { Min = 139.2, Max = 141.8 }, new Measurement { Min = 141.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 120 }, new Measurement { Min = 120.1, Max = 122.9 }, new Measurement { Min = 123, Max = 139.6 },
                    new Measurement { Min = 139.7, Max = 142.2 }, new Measurement { Min = 142.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 120.3 }, new Measurement { Min = 120.4, Max = 123.2 }, new Measurement { Min = 123.3, Max = 140 },
                    new Measurement { Min = 140.1, Max = 142.7 }, new Measurement { Min = 142.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 120.7 }, new Measurement { Min = 120.8, Max = 123.6 }, new Measurement { Min = 123.7, Max = 140.4 },
                    new Measurement { Min = 140.5, Max = 143.1 }, new Measurement { Min = 143.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 121.1 }, new Measurement { Min = 121.2, Max = 124 }, new Measurement { Min = 124.1, Max = 140.8 },
                    new Measurement { Min = 140.9, Max = 143.5 }, new Measurement { Min = 143.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 121.4 }, new Measurement { Min = 121.5, Max = 124.3 }, new Measurement { Min = 124.4, Max = 141.3 },
                    new Measurement { Min = 141.4, Max = 144 }, new Measurement { Min = 144.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 121.9 }, new Measurement { Min = 122, Max = 124.7 }, new Measurement { Min = 124.8, Max = 141.7 },
                    new Measurement { Min = 141.8, Max = 144.4 }, new Measurement { Min = 144.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 122.1 }, new Measurement { Min = 122.2, Max = 125 }, new Measurement { Min = 125.1, Max = 142.1 },
                    new Measurement { Min = 142.2, Max = 144.8 }, new Measurement { Min = 144.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 122.5 }, new Measurement { Min = 122.6, Max = 125.4 }, new Measurement { Min = 125.5, Max = 142.5 },
                    new Measurement { Min = 142.6, Max = 145.3 }, new Measurement { Min = 145.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 122.9 }, new Measurement { Min = 123, Max = 125.8 }, new Measurement { Min = 125.9, Max = 142.9 },
                    new Measurement { Min = 143, Max = 145.7 }, new Measurement { Min = 145.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 123.2 }, new Measurement { Min = 123.3, Max = 126.1 }, new Measurement { Min = 126.2, Max = 143.4 },
                    new Measurement { Min = 143.5, Max = 146.2 }, new Measurement { Min = 146.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 123.6 }, new Measurement { Min = 123.7, Max = 126.5 }, new Measurement { Min = 126.6, Max = 143.8 },
                    new Measurement { Min = 143.9, Max = 146.6 }, new Measurement { Min = 146.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 123.9 }, new Measurement { Min = 124, Max = 126.8 }, new Measurement { Min = 126.9, Max = 144.2 },
                    new Measurement { Min = 144.3, Max = 147.1 }, new Measurement { Min = 147.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 124.5 }, new Measurement { Min = 124.6, Max = 127.3 }, new Measurement { Min = 127.4, Max = 144.7 },
                    new Measurement { Min = 144.8, Max = 147.6 }, new Measurement { Min = 147.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 124.7 }, new Measurement { Min = 124.8, Max = 127.6 }, new Measurement { Min = 127.7, Max = 145.1 },
                    new Measurement { Min = 145.2, Max = 148.1 }, new Measurement { Min = 148.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 125.1 }, new Measurement { Min = 125.2, Max = 128 }, new Measurement { Min = 128.1, Max = 145.6 },
                    new Measurement { Min = 145.7, Max = 148.6 }, new Measurement { Min = 148.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 125.4 }, new Measurement { Min = 125.5, Max = 128.3 }, new Measurement { Min = 128.4, Max = 146.1 },
                    new Measurement { Min = 146.2, Max = 149.1 }, new Measurement { Min = 149.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 125.8 }, new Measurement { Min = 125.9, Max = 128.7 }, new Measurement { Min = 128.8, Max = 146.6 },
                    new Measurement { Min = 146.7, Max = 149.7 }, new Measurement { Min = 149.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 126.2 }, new Measurement { Min = 126.3, Max = 129 }, new Measurement { Min = 129.1, Max = 147.2 },
                    new Measurement { Min = 147.3, Max = 150.3 }, new Measurement { Min = 150.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 126.5 }, new Measurement { Min = 126.6, Max = 129.4 }, new Measurement { Min = 129.5, Max = 147.7 },
                    new Measurement { Min = 147.8, Max = 150.9 }, new Measurement { Min = 151, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 126.8 }, new Measurement { Min = 126.9, Max = 129.7 }, new Measurement { Min = 129.8, Max = 148.3 },
                    new Measurement { Min = 148.4, Max = 151.5 }, new Measurement { Min = 151.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 127.1 }, new Measurement { Min = 127.2, Max = 130.1 }, new Measurement { Min = 130.2, Max = 148.9 },
                    new Measurement { Min = 149, Max = 152.1 }, new Measurement { Min = 152.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 127.4 }, new Measurement { Min = 127.5, Max = 130.4 }, new Measurement { Min = 130.5, Max = 149.4 },
                    new Measurement { Min = 149.5, Max = 152.8 }, new Measurement { Min = 152.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 127.8 }, new Measurement { Min = 127.9, Max = 130.8 }, new Measurement { Min = 130.9, Max = 150.1 },
                    new Measurement { Min = 150.2, Max = 153.4 }, new Measurement { Min = 153.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 128 }, new Measurement { Min = 128.1, Max = 131.1 }, new Measurement { Min = 131.2, Max = 150.7 },
                    new Measurement { Min = 150.8, Max = 154.1 }, new Measurement { Min = 154.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 128.3 }, new Measurement { Min = 128.4, Max = 131.4 }, new Measurement { Min = 131.5, Max = 151.3 },
                    new Measurement { Min = 151.4, Max = 154.8 }, new Measurement { Min = 154.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 128.8 }, new Measurement { Min = 128.9, Max = 131.9 }, new Measurement { Min = 132, Max = 151.9 },
                    new Measurement { Min = 152, Max = 155.4 }, new Measurement { Min = 155.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 129 }, new Measurement { Min = 129.1, Max = 132.2 }, new Measurement { Min = 132.3, Max = 152.5 },
                    new Measurement { Min = 152.6, Max = 156.1 }, new Measurement { Min = 156.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 129.3 }, new Measurement { Min = 129.4, Max = 132.5 }, new Measurement { Min = 132.6, Max = 153.2 },
                    new Measurement { Min = 153.3, Max = 156.8 }, new Measurement { Min = 156.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 129.7 }, new Measurement { Min = 129.8, Max = 133 }, new Measurement { Min = 133.1, Max = 153.8 },
                    new Measurement { Min = 153.9, Max = 157.4 }, new Measurement { Min = 157.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 130 }, new Measurement { Min = 130.1, Max = 133.3 }, new Measurement { Min = 133.4, Max = 154.4 },
                    new Measurement { Min = 154.5, Max = 158.1 }, new Measurement { Min = 158.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 130.4 }, new Measurement { Min = 130.5, Max = 133.8 }, new Measurement { Min = 133.9, Max = 155 },
                    new Measurement { Min = 155.1, Max = 158.7 }, new Measurement { Min = 158.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 130.8 }, new Measurement { Min = 130.9, Max = 134.2 }, new Measurement { Min = 134.3, Max = 155.7 },
                    new Measurement { Min = 155.8, Max = 159.4 }, new Measurement { Min = 159.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 131.1 }, new Measurement { Min = 131.2, Max = 134.5 }, new Measurement { Min = 134.6, Max = 156.3 },
                    new Measurement { Min = 156.4, Max = 160 }, new Measurement { Min = 160.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 131.5 }, new Measurement { Min = 131.6, Max = 135 }, new Measurement { Min = 135.1, Max = 156.9 },
                    new Measurement { Min = 157, Max = 160.7 }, new Measurement { Min = 160.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 131.8 }, new Measurement { Min = 131.9, Max = 135.4 }, new Measurement { Min = 135.5, Max = 157.5 },
                    new Measurement { Min = 157.6, Max = 161.3 }, new Measurement { Min = 161.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 132.2 }, new Measurement { Min = 132.3, Max = 135.9 }, new Measurement { Min = 136, Max = 158.2 },
                    new Measurement { Min = 158.3, Max = 161.9 }, new Measurement { Min = 162, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 132.6 }, new Measurement { Min = 132.7, Max = 136.3 }, new Measurement { Min = 136.4, Max = 158.8 },
                    new Measurement { Min = 158.9, Max = 162.5 }, new Measurement { Min = 162.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 133.1 }, new Measurement { Min = 133.2, Max = 136.8 }, new Measurement { Min = 136.9, Max = 159.4 },
                    new Measurement { Min = 159.5, Max = 163.2 }, new Measurement { Min = 163.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 133.5 }, new Measurement { Min = 133.6, Max = 137.3 }, new Measurement { Min = 137.4, Max = 160 },
                    new Measurement { Min = 160.1, Max = 163.8 }, new Measurement { Min = 163.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 133.9 }, new Measurement { Min = 134, Max = 137.7 }, new Measurement { Min = 137.8, Max = 160.7 },
                    new Measurement { Min = 160.8, Max = 164.4 }, new Measurement { Min = 164.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 134.4 }, new Measurement { Min = 134.5, Max = 138.3 }, new Measurement { Min = 138.4, Max = 161.3 },
                    new Measurement { Min = 161.4, Max = 165.1 }, new Measurement { Min = 165.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 134.9 }, new Measurement { Min = 135, Max = 138.8 }, new Measurement { Min = 138.9, Max = 161.9 },
                    new Measurement { Min = 162, Max = 165.7 }, new Measurement { Min = 165.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 135.3 }, new Measurement { Min = 135.4, Max = 139.2 }, new Measurement { Min = 139.3, Max = 162.6 },
                    new Measurement { Min = 162.7, Max = 166.3 }, new Measurement { Min = 166.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 135.7 }, new Measurement { Min = 135.8, Max = 139.8 }, new Measurement { Min = 139.9, Max = 163.2 },
                    new Measurement { Min = 163.3, Max = 166.9 }, new Measurement { Min = 167, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 136.2 }, new Measurement { Min = 136.3, Max = 140.3 }, new Measurement { Min = 140.4, Max = 163.8 },
                    new Measurement { Min = 163.9, Max = 167.5 }, new Measurement { Min = 167.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 136.7 }, new Measurement { Min = 136.8, Max = 140.8 }, new Measurement { Min = 140.9, Max = 164.4 },
                    new Measurement { Min = 164.5, Max = 168.2 }, new Measurement { Min = 168.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 137.2 }, new Measurement { Min = 137.3, Max = 141.3 }, new Measurement { Min = 141.4, Max = 165 },
                    new Measurement { Min = 165.1, Max = 168.7 }, new Measurement { Min = 168.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 137.7 }, new Measurement { Min = 137.8, Max = 141.9 }, new Measurement { Min = 142, Max = 165.5 },
                    new Measurement { Min = 165.6, Max = 169.2 }, new Measurement { Min = 169.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 138.2 }, new Measurement { Min = 138.3, Max = 142.4 }, new Measurement { Min = 142.5, Max = 166 },
                    new Measurement { Min = 166.1, Max = 169.7 }, new Measurement { Min = 169.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 138.7 }, new Measurement { Min = 138.8, Max = 142.9 }, new Measurement { Min = 143, Max = 166.6 },
                    new Measurement { Min = 166.7, Max = 170.2 }, new Measurement { Min = 170.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 139.2 }, new Measurement { Min = 139.3, Max = 143.4 }, new Measurement { Min = 143.5, Max = 167.1 },
                    new Measurement { Min = 167.2, Max = 170.7 }, new Measurement { Min = 170.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 139.7 }, new Measurement { Min = 139.8, Max = 144 }, new Measurement { Min = 144.1, Max = 167.6 },
                    new Measurement { Min = 167.7, Max = 171.1 }, new Measurement { Min = 171.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 140.3 }, new Measurement { Min = 140.4, Max = 144.6 }, new Measurement { Min = 144.7, Max = 168 },
                    new Measurement { Min = 168.1, Max = 171.5 }, new Measurement { Min = 171.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 140.8 }, new Measurement { Min = 140.9, Max = 145.1 }, new Measurement { Min = 145.2, Max = 168.5 },
                    new Measurement { Min = 168.6, Max = 171.9 }, new Measurement { Min = 172, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 141.4 }, new Measurement { Min = 141.5, Max = 145.7 }, new Measurement { Min = 145.8, Max = 168.9 },
                    new Measurement { Min = 169, Max = 172.3 }, new Measurement { Min = 172.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 141.9 }, new Measurement { Min = 142, Max = 146.2 }, new Measurement { Min = 146.3, Max = 169.3 },
                    new Measurement { Min = 169.4, Max = 172.6 }, new Measurement { Min = 172.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 142.4 }, new Measurement { Min = 142.5, Max = 146.7 }, new Measurement { Min = 146.8, Max = 169.7 },
                    new Measurement { Min = 169.8, Max = 172.9 }, new Measurement { Min = 173, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 142.9 }, new Measurement { Min = 143, Max = 147.2 }, new Measurement { Min = 147.3, Max = 170 },
                    new Measurement { Min = 170.1, Max = 173.2 }, new Measurement { Min = 173.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 143.5 }, new Measurement { Min = 143.6, Max = 147.8 }, new Measurement { Min = 147.9, Max = 170.3 },
                    new Measurement { Min = 170.4, Max = 173.5 }, new Measurement { Min = 173.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 144 }, new Measurement { Min = 144.1, Max = 148.3 }, new Measurement { Min = 148.4, Max = 170.6 },
                    new Measurement { Min = 170.7, Max = 173.7 }, new Measurement { Min = 173.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 144.5 }, new Measurement { Min = 144.6, Max = 148.8 }, new Measurement { Min = 148.9, Max = 170.9 },
                    new Measurement { Min = 171, Max = 173.9 }, new Measurement { Min = 174, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 145 }, new Measurement { Min = 145.1, Max = 149.3 }, new Measurement { Min = 149.4, Max = 171.2 },
                    new Measurement { Min = 171.3, Max = 174.1 }, new Measurement { Min = 174.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 145.5 }, new Measurement { Min = 145.6, Max = 149.8 }, new Measurement { Min = 149.9, Max = 171.4 },
                    new Measurement { Min = 171.5, Max = 174.4 }, new Measurement { Min = 174.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 146.1 }, new Measurement { Min = 146.2, Max = 150.3 }, new Measurement { Min = 150.4, Max = 171.7 },
                    new Measurement { Min = 171.8, Max = 174.6 }, new Measurement { Min = 174.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 146.6 }, new Measurement { Min = 146.7, Max = 150.8 }, new Measurement { Min = 150.9, Max = 172 },
                    new Measurement { Min = 172.1, Max = 174.8 }, new Measurement { Min = 174.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 147.2 }, new Measurement { Min = 147.3, Max = 151.3 }, new Measurement { Min = 151.4, Max = 172.2 },
                    new Measurement { Min = 172.3, Max = 175.1 }, new Measurement { Min = 175.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 147.8 }, new Measurement { Min = 147.9, Max = 151.8 }, new Measurement { Min = 151.9, Max = 172.5 },
                    new Measurement { Min = 172.6, Max = 175.3 }, new Measurement { Min = 175.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 148.4 }, new Measurement { Min = 148.5, Max = 152.3 }, new Measurement { Min = 152.4, Max = 172.7 },
                    new Measurement { Min = 172.8, Max = 175.6 }, new Measurement { Min = 175.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 149 }, new Measurement { Min = 149.1, Max = 152.9 }, new Measurement { Min = 153, Max = 173 },
                    new Measurement { Min = 173.1, Max = 175.8 }, new Measurement { Min = 175.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 149.7 }, new Measurement { Min = 149.8, Max = 153.4 }, new Measurement { Min = 153.5, Max = 173.2 },
                    new Measurement { Min = 173.3, Max = 176 }, new Measurement { Min = 176.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 150.3 }, new Measurement { Min = 150.4, Max = 153.9 }, new Measurement { Min = 154, Max = 173.5 },
                    new Measurement { Min = 173.6, Max = 176.3 }, new Measurement { Min = 176.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 150.9 }, new Measurement { Min = 151, Max = 154.4 }, new Measurement { Min = 154.5, Max = 173.7 },
                    new Measurement { Min = 173.8, Max = 176.5 }, new Measurement { Min = 176.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 151.5 }, new Measurement { Min = 151.6, Max = 154.9 }, new Measurement { Min = 155, Max = 174 },
                    new Measurement { Min = 174.1, Max = 176.8 }, new Measurement { Min = 176.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 152 }, new Measurement { Min = 152.1, Max = 155.4 }, new Measurement { Min = 155.5, Max = 174.2 },
                    new Measurement { Min = 174.3, Max = 177 }, new Measurement { Min = 177.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 152.5 }, new Measurement { Min = 152.6, Max = 155.8 }, new Measurement { Min = 155.9, Max = 174.4 },
                    new Measurement { Min = 174.5, Max = 177.3 }, new Measurement { Min = 177.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 153 }, new Measurement { Min = 153.1, Max = 156.3 }, new Measurement { Min = 156.4, Max = 174.7 },
                    new Measurement { Min = 174.8, Max = 177.5 }, new Measurement { Min = 177.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 153.4 }, new Measurement { Min = 153.5, Max = 156.7 }, new Measurement { Min = 156.8, Max = 174.9 },
                    new Measurement { Min = 175, Max = 177.7 }, new Measurement { Min = 177.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 153.8 }, new Measurement { Min = 153.9, Max = 157 }, new Measurement { Min = 157.1, Max = 175.1 },
                    new Measurement { Min = 175.2, Max = 177.9 }, new Measurement { Min = 178, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 154.2 }, new Measurement { Min = 154.3, Max = 157.3 }, new Measurement { Min = 157.4, Max = 175.3 },
                    new Measurement { Min = 175.4, Max = 178.1 }, new Measurement { Min = 178.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 154.5 }, new Measurement { Min = 154.6, Max = 157.7 }, new Measurement { Min = 157.8, Max = 175.5 },
                    new Measurement { Min = 175.6, Max = 178.3 }, new Measurement { Min = 178.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 154.9 }, new Measurement { Min = 155, Max = 157.9 }, new Measurement { Min = 158, Max = 175.7 },
                    new Measurement { Min = 175.8, Max = 178.5 }, new Measurement { Min = 178.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 155.1 }, new Measurement { Min = 155.2, Max = 158.2 }, new Measurement { Min = 158.3, Max = 175.9 },
                    new Measurement { Min = 176, Max = 178.7 }, new Measurement { Min = 178.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 155.4 }, new Measurement { Min = 155.5, Max = 158.5 }, new Measurement { Min = 158.6, Max = 176 },
                    new Measurement { Min = 176.1, Max = 178.8 }, new Measurement { Min = 178.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 155.6 }, new Measurement { Min = 155.7, Max = 158.7 }, new Measurement { Min = 158.8, Max = 176.1 },
                    new Measurement { Min = 176.2, Max = 178.9 }, new Measurement { Min = 179, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 155.9 }, new Measurement { Min = 156, Max = 158.9 }, new Measurement { Min = 159, Max = 176.3 },
                    new Measurement { Min = 176.4, Max = 179 }, new Measurement { Min = 179.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.1 }, new Measurement { Min = 156.2, Max = 159.1 }, new Measurement { Min = 159.2, Max = 176.4 },
                    new Measurement { Min = 176.5, Max = 179.1 }, new Measurement { Min = 179.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.3 }, new Measurement { Min = 156.4, Max = 159.3 }, new Measurement { Min = 159.4, Max = 176.5 },
                    new Measurement { Min = 176.6, Max = 179.2 }, new Measurement { Min = 179.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.4 }, new Measurement { Min = 156.5, Max = 159.5 }, new Measurement { Min = 159.6, Max = 176.6 },
                    new Measurement { Min = 176.7, Max = 179.2 }, new Measurement { Min = 179.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.6 }, new Measurement { Min = 156.7, Max = 159.6 }, new Measurement { Min = 159.7, Max = 176.7 },
                    new Measurement { Min = 176.8, Max = 179.3 }, new Measurement { Min = 179.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.8 }, new Measurement { Min = 156.9, Max = 159.8 }, new Measurement { Min = 159.9, Max = 176.8 },
                    new Measurement { Min = 176.9, Max = 179.4 }, new Measurement { Min = 179.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 156.9 }, new Measurement { Min = 157, Max = 159.9 }, new Measurement { Min = 160, Max = 176.9 },
                    new Measurement { Min = 177, Max = 179.5 }, new Measurement { Min = 179.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.1 }, new Measurement { Min = 157.2, Max = 160 }, new Measurement { Min = 160.1, Max = 1700 },
                    new Measurement { Min = 177.1, Max = 179.6 }, new Measurement { Min = 179.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.2 }, new Measurement { Min = 157.3, Max = 160.2 }, new Measurement { Min = 160.3, Max = 177.1 },
                    new Measurement { Min = 177.2, Max = 179.7 }, new Measurement { Min = 179.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.3 }, new Measurement { Min = 157.4, Max = 160.3 }, new Measurement { Min = 160.4, Max = 177.2 },
                    new Measurement { Min = 177.3, Max = 179.8 }, new Measurement { Min = 179.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.5 }, new Measurement { Min = 157.6, Max = 160.4 }, new Measurement { Min = 160.5, Max = 177.2 },
                    new Measurement { Min = 177.3, Max = 179.9 }, new Measurement { Min = 180, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.6 }, new Measurement { Min = 157.7, Max = 160.5 }, new Measurement { Min = 160.6, Max = 177.3 },
                    new Measurement { Min = 177.4, Max = 179.9 }, new Measurement { Min = 180, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.7 }, new Measurement { Min = 157.8, Max = 160.6 }, new Measurement { Min = 160.7, Max = 177.3 },
                    new Measurement { Min = 177.4, Max = 180 }, new Measurement { Min = 180.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 157.8 }, new Measurement { Min = 157.9, Max = 160.7 }, new Measurement { Min = 160.8, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180 }, new Measurement { Min = 180.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158 }, new Measurement { Min = 158.1, Max = 160.8 }, new Measurement { Min = 160.9, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.1 }, new Measurement { Min = 158.2, Max = 160.9 }, new Measurement { Min = 161, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.2 }, new Measurement { Min = 158.3, Max = 161 }, new Measurement { Min = 161.1, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.3 }, new Measurement { Min = 158.4, Max = 161 }, new Measurement { Min = 161.1, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.4 }, new Measurement { Min = 158.5, Max = 161.1 }, new Measurement { Min = 161.2, Max = 177.4 },
                    new Measurement { Min = 177.5, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.5 }, new Measurement { Min = 158.6, Max = 161.2 }, new Measurement { Min = 161.3, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.5 }, new Measurement { Min = 158.6, Max = 161.2 }, new Measurement { Min = 161.3, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 0, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.6 }, new Measurement { Min = 158.7, Max = 161.3 }, new Measurement { Min = 161.4, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 1, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.7 }, new Measurement { Min = 158.8, Max = 161.3 }, new Measurement { Min = 161.4, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 2, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.7 }, new Measurement { Min = 158.8, Max = 161.4 }, new Measurement { Min = 161.5, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 3, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.8 }, new Measurement { Min = 158.9, Max = 161.4 }, new Measurement { Min = 161.5, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 4, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.8 }, new Measurement { Min = 158.9, Max = 161.5 }, new Measurement { Min = 161.6, Max = 177.5 },
                    new Measurement { Min = 177.6, Max = 180.1 }, new Measurement { Min = 180.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 5, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.9 }, new Measurement { Min = 159, Max = 161.5 }, new Measurement { Min = 161.6, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 6, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.9 }, new Measurement { Min = 159, Max = 161.5 }, new Measurement { Min = 161.6, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 7, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.9 }, new Measurement { Min = 159, Max = 161.5 }, new Measurement { Min = 161.6, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 8, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 158.9 }, new Measurement { Min = 159, Max = 161.6 }, new Measurement { Min = 161.7, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 9, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 159 }, new Measurement { Min = 159.1, Max = 161.6 }, new Measurement { Min = 161.7, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 10, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 159 }, new Measurement { Min = 159.1, Max = 161.6 }, new Measurement { Min = 161.7, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 180.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 11, lookUpHeight = new LookUpHeight("0",
                    new Measurement { Min = 1, Max = 159 }, new Measurement { Min = 159.1, Max = 161.6 }, new Measurement { Min = 161.7, Max = 177.6 },
                    new Measurement { Min = 177.7, Max = 180.2 }, new Measurement { Min = 173, Max = 300 })
                },

                //Women

                new GrowthByHeight{ Age = 5, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 98.9 }, new Measurement { Min = 99, Max = 101 }, new Measurement { Min = 101.1, Max = 113.9 },
                    new Measurement { Min = 114, Max = 116 }, new Measurement { Min = 116.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 99.4 }, new Measurement { Min = 99.5, Max = 101.5 }, new Measurement { Min = 101.6, Max = 114.5 },
                    new Measurement { Min = 114.6, Max = 116.6 }, new Measurement { Min = 116.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 100 }, new Measurement { Min = 100.1, Max = 102.1 }, new Measurement { Min = 102.2, Max = 115.1 },
                    new Measurement { Min = 115.2, Max = 117.2 }, new Measurement { Min = 117.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 100.5 }, new Measurement { Min = 100.6, Max = 102.6 }, new Measurement { Min = 102.7, Max = 115.7 },
                    new Measurement { Min = 115.8, Max = 117.9 }, new Measurement { Min = 118, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 101 }, new Measurement { Min = 101.1, Max = 103.1 }, new Measurement { Min = 103.2, Max = 116.3 },
                    new Measurement { Min = 116.4, Max = 118.5 }, new Measurement { Min = 118.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 101.5 }, new Measurement { Min = 101.6, Max = 103.6 }, new Measurement { Min = 103.7, Max = 116.9 },
                    new Measurement { Min = 117, Max = 119.1 }, new Measurement { Min = 119.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 102.1 }, new Measurement { Min = 102.2, Max = 104.2 }, new Measurement { Min = 104.3, Max = 117.4 },
                    new Measurement { Min = 117.5, Max = 119.6 }, new Measurement { Min = 119.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 102.6 }, new Measurement { Min = 102.7, Max = 104.7 }, new Measurement { Min = 104.8, Max = 118 },
                    new Measurement { Min = 118.1, Max = 120.2 }, new Measurement { Min = 120.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 103.1 }, new Measurement { Min = 103.2, Max = 105.2 }, new Measurement { Min = 105.3, Max = 118.6 },
                    new Measurement { Min = 118.7, Max = 120.8 }, new Measurement { Min = 120.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 103.6 }, new Measurement { Min = 103.7, Max = 105.7 }, new Measurement { Min = 105.8, Max = 119.2 },
                    new Measurement { Min = 119.3, Max = 121.4 }, new Measurement { Min = 121.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 104.1 }, new Measurement { Min = 104.2, Max = 106.2 }, new Measurement { Min = 106.3, Max = 119.7 },
                    new Measurement { Min = 119.8, Max = 121.9 }, new Measurement { Min = 122, Max = 300 })
                },
                new GrowthByHeight{ Age = 5, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 104 }, new Measurement { Min = 104.7, Max = 106.8 }, new Measurement { Min = 106.9, Max = 120.3 },
                    new Measurement { Min = 120.4, Max = 122.5 }, new Measurement { Min = 122.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 105.1 }, new Measurement { Min = 105.1, Max = 107.3 }, new Measurement { Min = 107.4, Max = 120.8 },
                    new Measurement { Min = 120.9, Max = 123 }, new Measurement { Min = 123.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 105.5 }, new Measurement { Min = 105.6, Max = 107.7 }, new Measurement { Min = 107.8, Max = 121.3 },
                    new Measurement { Min = 121.4, Max = 123.7 }, new Measurement { Min = 123.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 106 }, new Measurement { Min = 106.1, Max = 108.2 }, new Measurement { Min = 108.3, Max = 121.9 },
                    new Measurement { Min = 122, Max = 124.1 }, new Measurement { Min = 124.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 106.4 }, new Measurement { Min = 106.5, Max = 108.6 }, new Measurement { Min = 108.7, Max = 122.4 },
                    new Measurement { Min = 122.5, Max = 124.6 }, new Measurement { Min = 124.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 106.9 }, new Measurement { Min = 107, Max = 109.1 }, new Measurement { Min = 109.2, Max = 122.9 },
                    new Measurement { Min = 123, Max = 125.3 }, new Measurement { Min = 125.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 107.3 }, new Measurement { Min = 107.4, Max = 109.5 }, new Measurement { Min = 109.6, Max = 123.4 },
                    new Measurement { Min = 123.5, Max = 125.8 }, new Measurement { Min = 125.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 107.7 }, new Measurement { Min = 107.8, Max = 109.9 }, new Measurement { Min = 110, Max = 123.9 },
                    new Measurement { Min = 124, Max = 126.3 }, new Measurement { Min = 126.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 108 }, new Measurement { Min = 108.1, Max = 110.4 }, new Measurement { Min = 110.5, Max = 124.4 },
                    new Measurement { Min = 124.5, Max = 126.8 }, new Measurement { Min = 126.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 108.4 }, new Measurement { Min = 108.5, Max = 110.8 }, new Measurement { Min = 110.9, Max = 124.9 },
                    new Measurement { Min = 125, Max = 127.3 }, new Measurement { Min = 127.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 108.7 }, new Measurement { Min = 108.8, Max = 111.1 }, new Measurement { Min = 111.2, Max = 125.4 },
                    new Measurement { Min = 125.5, Max = 127.8 }, new Measurement { Min = 127.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 109.1 }, new Measurement { Min = 109.2, Max = 111.5 }, new Measurement { Min = 111.6, Max = 125.9 },
                    new Measurement { Min = 126, Max = 128.3 }, new Measurement { Min = 128.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 6, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 109.5 }, new Measurement { Min = 109.6, Max = 111.9 }, new Measurement { Min = 112, Max = 126.4 },
                    new Measurement { Min = 126.5, Max = 128.8 }, new Measurement { Min = 128.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 109.8 }, new Measurement { Min = 109.9, Max = 112.3 }, new Measurement { Min = 112.4, Max = 126.8 },
                    new Measurement { Min = 126.9, Max = 129.2 }, new Measurement { Min = 129.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 110.2 }, new Measurement { Min = 110.3, Max = 112.7 }, new Measurement { Min = 112.8, Max = 127.3 },
                    new Measurement { Min = 127.4, Max = 129.7 }, new Measurement { Min = 129.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 110.6 }, new Measurement { Min = 110.7, Max = 113.1 }, new Measurement { Min = 113.2, Max = 127.8 },
                    new Measurement { Min = 127.9, Max = 130.2 }, new Measurement { Min = 130.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 110.9 }, new Measurement { Min = 111, Max = 113.4 }, new Measurement { Min = 113.5, Max = 128.2 },
                    new Measurement { Min = 128.3, Max = 130.6 }, new Measurement { Min = 130.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 111.3 }, new Measurement { Min = 111.4, Max = 113.8 }, new Measurement { Min = 113.9, Max = 128.6 },
                    new Measurement { Min = 128.7, Max = 131.1 }, new Measurement { Min = 131.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 111.7 }, new Measurement { Min = 111.8, Max = 114.2 }, new Measurement { Min = 114.3, Max = 129.1 },
                    new Measurement { Min = 129.2, Max = 131.6 }, new Measurement { Min = 131.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 112.1 }, new Measurement { Min = 112.2, Max = 114.6 }, new Measurement { Min = 114.7, Max = 129.5 },
                    new Measurement { Min = 129.6, Max = 132 }, new Measurement { Min = 132.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 112.4 }, new Measurement { Min = 112.5, Max = 114.9 }, new Measurement { Min = 115, Max = 130 },
                    new Measurement { Min = 130.1, Max = 132.5 }, new Measurement { Min = 132.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 112.8 }, new Measurement { Min = 112.9, Max = 115.3 }, new Measurement { Min = 115.4, Max = 130.5 },
                    new Measurement { Min = 130.6, Max = 133 }, new Measurement { Min = 133.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 113.2 }, new Measurement { Min = 113.3, Max = 115.7 }, new Measurement { Min = 115.8, Max = 131 },
                    new Measurement { Min = 131.1, Max = 133.5 }, new Measurement { Min = 133.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 113.6 }, new Measurement { Min = 113.7, Max = 116.1 }, new Measurement { Min = 116.2, Max = 131.5 },
                    new Measurement { Min = 131.6, Max = 134 }, new Measurement { Min = 134.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 7, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 114 }, new Measurement { Min = 114.1, Max = 116.5 }, new Measurement { Min = 116.6, Max = 132 },
                    new Measurement { Min = 132.1, Max = 134.5 }, new Measurement { Min = 134.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 114.3 }, new Measurement { Min = 114.4, Max = 116.9 }, new Measurement { Min = 117, Max = 132.5 },
                    new Measurement { Min = 132.6, Max = 135.1 }, new Measurement { Min = 135.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 114.7 }, new Measurement { Min = 114.8, Max = 117.3 }, new Measurement { Min = 117.4, Max = 133 },
                    new Measurement { Min = 133.1, Max = 135.6 }, new Measurement { Min = 135.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 115.1 }, new Measurement { Min = 115.2, Max = 117.7 }, new Measurement { Min = 117.8, Max = 133.5 },
                    new Measurement { Min = 133.6, Max = 136.1 }, new Measurement { Min = 136.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 115.5 }, new Measurement { Min = 115.6, Max = 118.1 }, new Measurement { Min = 118.2, Max = 134.1 },
                    new Measurement { Min = 134.2, Max = 136.7 }, new Measurement { Min = 136.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 115.9 }, new Measurement { Min = 116, Max = 118.5 }, new Measurement { Min = 118.6, Max = 134.7 },
                    new Measurement { Min = 134.8, Max = 137.3 }, new Measurement { Min = 137.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 116.3 }, new Measurement { Min = 116.4, Max = 118.9 }, new Measurement { Min = 119, Max = 135.2 },
                    new Measurement { Min = 135.3, Max = 137.8 }, new Measurement { Min = 137.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 116.7 }, new Measurement { Min = 116.8, Max = 119.4 }, new Measurement { Min = 119.5, Max = 135.7 },
                    new Measurement { Min = 135.8, Max = 138.4 }, new Measurement { Min = 138.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 117.1 }, new Measurement { Min = 117.2, Max = 119.8 }, new Measurement { Min = 119.9, Max = 136.3 },
                    new Measurement { Min = 136.4, Max = 139.1 }, new Measurement { Min = 139.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 117.5 }, new Measurement { Min = 117.6, Max = 120.2 }, new Measurement { Min = 120.3, Max = 136.9 },
                    new Measurement { Min = 137, Max = 139.7 }, new Measurement { Min = 139.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 117.8 }, new Measurement { Min = 117.9, Max = 120.6 }, new Measurement { Min = 120.7, Max = 137.4 },
                    new Measurement { Min = 137.5, Max = 140.3 }, new Measurement { Min = 140.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 118.2 }, new Measurement { Min = 118.3, Max = 121 }, new Measurement { Min = 121.1, Max = 138 },
                    new Measurement { Min = 138.1, Max = 140.9 }, new Measurement { Min = 141, Max = 300 })
                },
                new GrowthByHeight{ Age = 8, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 118.6 }, new Measurement { Min = 118.7, Max = 121.4 }, new Measurement { Min = 121.5, Max = 138.6 },
                    new Measurement { Min = 138.7, Max = 141.5 }, new Measurement { Min = 141.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 119 }, new Measurement { Min = 119.1, Max = 121.8 }, new Measurement { Min = 121.9, Max = 139.1 },
                    new Measurement { Min = 139.2, Max = 142.1 }, new Measurement { Min = 142.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 119.4 }, new Measurement { Min = 119.5, Max = 122.2 }, new Measurement { Min = 122.3, Max = 139.7 },
                    new Measurement { Min = 139.8, Max = 142.7 }, new Measurement { Min = 142.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 119.8 }, new Measurement { Min = 119.9, Max = 122.6 }, new Measurement { Min = 122.7, Max = 140.3 },
                    new Measurement { Min = 140.4, Max = 143.3 }, new Measurement { Min = 143.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 120.2 }, new Measurement { Min = 120.3, Max = 123 }, new Measurement { Min = 123.1, Max = 140.8 },
                    new Measurement { Min = 140.9, Max = 143.9 }, new Measurement { Min = 144, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 120.6 }, new Measurement { Min = 120.7, Max = 123.4 }, new Measurement { Min = 123.5, Max = 141.4 },
                    new Measurement { Min = 141.5, Max = 144.5 }, new Measurement { Min = 144.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 121 }, new Measurement { Min = 121.1, Max = 123.9 }, new Measurement { Min = 124, Max = 142 },
                    new Measurement { Min = 142.1, Max = 145.2 }, new Measurement { Min = 145.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 121.5 }, new Measurement { Min = 121.6, Max = 124.4 }, new Measurement { Min = 124.5, Max = 142.6 },
                    new Measurement { Min = 142.7, Max = 145.8 }, new Measurement { Min = 145.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 121.9 }, new Measurement { Min = 122, Max = 124.8 }, new Measurement { Min = 124.9, Max = 143.1 },
                    new Measurement { Min = 143.2, Max = 146.3 }, new Measurement { Min = 146.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 122.3 }, new Measurement { Min = 122.4, Max = 125.2 }, new Measurement { Min = 125.3, Max = 143.7 },
                    new Measurement { Min = 143.8, Max = 146.9 }, new Measurement { Min = 147, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 122.7 }, new Measurement { Min = 122.8, Max = 125.6 }, new Measurement { Min = 125.7, Max = 144.2 },
                    new Measurement { Min = 144.3, Max = 147.5 }, new Measurement { Min = 147.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 123.1 }, new Measurement { Min = 123.2, Max = 126.1 }, new Measurement { Min = 126.2, Max = 144.8 },
                    new Measurement { Min = 144.9, Max = 148.1 }, new Measurement { Min = 148.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 9, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 123.6 }, new Measurement { Min = 123.7, Max = 126.6 }, new Measurement { Min = 126.7, Max = 145.5 },
                    new Measurement { Min = 145.6, Max = 148.8 }, new Measurement { Min = 148.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 124 }, new Measurement { Min = 124.1, Max = 127 }, new Measurement { Min = 127.1, Max = 146.1 },
                    new Measurement { Min = 146.2, Max = 149.4 }, new Measurement { Min = 149.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 124.4 }, new Measurement { Min = 124.5, Max = 127.4 }, new Measurement { Min = 127.5, Max = 146.7 },
                    new Measurement { Min = 146.8, Max = 150 }, new Measurement { Min = 150.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 124.9 }, new Measurement { Min = 125, Max = 128 }, new Measurement { Min = 128.1, Max = 147.3 },
                    new Measurement { Min = 147.4, Max = 150.6 }, new Measurement { Min = 150.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 125.2 }, new Measurement { Min = 125.3, Max = 128.4 }, new Measurement { Min = 128.5, Max = 147.9 },
                    new Measurement { Min = 148, Max = 151.2 }, new Measurement { Min = 151.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 125.7 }, new Measurement { Min = 125.8, Max = 128.9 }, new Measurement { Min = 129, Max = 148.5 },
                    new Measurement { Min = 148.6, Max = 151.8 }, new Measurement { Min = 151.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 126.1 }, new Measurement { Min = 126.2, Max = 129.4 }, new Measurement { Min = 129.5, Max = 149.1 },
                    new Measurement { Min = 149.2, Max = 152.4 }, new Measurement { Min = 152.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 126.6 }, new Measurement { Min = 126.7, Max = 129.9 }, new Measurement { Min = 130, Max = 149.7 },
                    new Measurement { Min = 149.8, Max = 153 }, new Measurement { Min = 153.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 127.1 }, new Measurement { Min = 127.2, Max = 130.4 }, new Measurement { Min = 130.5, Max = 150.2 },
                    new Measurement { Min = 150.3, Max = 153.5 }, new Measurement { Min = 153.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 127.5 }, new Measurement { Min = 127.6, Max = 130.8 }, new Measurement { Min = 130.9, Max = 150.7 },
                    new Measurement { Min = 150.8, Max = 154 }, new Measurement { Min = 154.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 128 }, new Measurement { Min = 128.1, Max = 131.4 }, new Measurement { Min = 131.5, Max = 151.2 },
                    new Measurement { Min = 151.3, Max = 154.5 }, new Measurement { Min = 154.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 128.5 }, new Measurement { Min = 128.6, Max = 131.9 }, new Measurement { Min = 132, Max = 151.7 },
                    new Measurement { Min = 151.8, Max = 155 }, new Measurement { Min = 155.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 10, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 128.9 }, new Measurement { Min = 129, Max = 132.3 }, new Measurement { Min = 132.4, Max = 152.2 },
                    new Measurement { Min = 152.3, Max = 155.4 }, new Measurement { Min = 155.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 129.4 }, new Measurement { Min = 129.5, Max = 132.8 }, new Measurement { Min = 132.9, Max = 152.6 },
                    new Measurement { Min = 152.7, Max = 155.8 }, new Measurement { Min = 155.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 129.9 }, new Measurement { Min = 130, Max = 133.3 }, new Measurement { Min = 133.4, Max = 153 },
                    new Measurement { Min = 153.1, Max = 156.2 }, new Measurement { Min = 156.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 130.4 }, new Measurement { Min = 130.5, Max = 133.8 }, new Measurement { Min = 133.9, Max = 153.3 },
                    new Measurement { Min = 153.4, Max = 156.4 }, new Measurement { Min = 156.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 130.9 }, new Measurement { Min = 131, Max = 134.2 }, new Measurement { Min = 134.3, Max = 153.7 },
                    new Measurement { Min = 153.8, Max = 156.8 }, new Measurement { Min = 156.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 131.4 }, new Measurement { Min = 131.5, Max = 134.7 }, new Measurement { Min = 134.8, Max = 154.1 },
                    new Measurement { Min = 154.2, Max = 157.1 }, new Measurement { Min = 157.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 131.8 }, new Measurement { Min = 131.9, Max = 135.1 }, new Measurement { Min = 135.2, Max = 154.4 },
                    new Measurement { Min = 154.5, Max = 157.4 }, new Measurement { Min = 157.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 132.3 }, new Measurement { Min = 132.4, Max = 135.6 }, new Measurement { Min = 135.7, Max = 154.7 },
                    new Measurement { Min = 154.8, Max = 157.7 }, new Measurement { Min = 157.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 132.8 }, new Measurement { Min = 132.9, Max = 136.1 }, new Measurement { Min = 136.2, Max = 155.1 },
                    new Measurement { Min = 155.2, Max = 158.1 }, new Measurement { Min = 158.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 133.3 }, new Measurement { Min = 133.4, Max = 136.6 }, new Measurement { Min = 136.7, Max = 155.5 },
                    new Measurement { Min = 155.6, Max = 158.4 }, new Measurement { Min = 158.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 133.8 }, new Measurement { Min = 133.9, Max = 137.1 }, new Measurement { Min = 137.2, Max = 155.8 },
                    new Measurement { Min = 155.9, Max = 158.7 }, new Measurement { Min = 158.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 134.3 }, new Measurement { Min = 134.4, Max = 137.7 }, new Measurement { Min = 137.8, Max = 156.2 },
                    new Measurement { Min = 156.3, Max = 159 }, new Measurement { Min = 159.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 11, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 134.8 }, new Measurement { Min = 134.9, Max = 138.2 }, new Measurement { Min = 138.3, Max = 156.5 },
                    new Measurement { Min = 156.6, Max = 159.3 }, new Measurement { Min = 159.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 135.3 }, new Measurement { Min = 135.4, Max = 138.7 }, new Measurement { Min = 138.8, Max = 156.9 },
                    new Measurement { Min = 157, Max = 159.5 }, new Measurement { Min = 159.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 135.8 }, new Measurement { Min = 135.9, Max = 139.1 }, new Measurement { Min = 139.2, Max = 157.2 },
                    new Measurement { Min = 157.3, Max = 159.8 }, new Measurement { Min = 159.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 136.3 }, new Measurement { Min = 136.4, Max = 139.6 }, new Measurement { Min = 139.7, Max = 157.5 },
                    new Measurement { Min = 157.6, Max = 160.1 }, new Measurement { Min = 160.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 136.7 }, new Measurement { Min = 136.8, Max = 140 }, new Measurement { Min = 140.1, Max = 157.8 },
                    new Measurement { Min = 157.9, Max = 160.4 }, new Measurement { Min = 160.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 137.2 }, new Measurement { Min = 137.3, Max = 140.5 }, new Measurement { Min = 140.6, Max = 158.1 },
                    new Measurement { Min = 158.2, Max = 160.6 }, new Measurement { Min = 160.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 137.6 }, new Measurement { Min = 137.7, Max = 140.9 }, new Measurement { Min = 141, Max = 158.4 },
                    new Measurement { Min = 158.5, Max = 160.9 }, new Measurement { Min = 161, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 138 }, new Measurement { Min = 138.1, Max = 141.3 }, new Measurement { Min = 141.4, Max = 158.7 },
                    new Measurement { Min = 158.8, Max = 161.2 }, new Measurement { Min = 161.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 138.4 }, new Measurement { Min = 138.5, Max = 141.6 }, new Measurement { Min = 141.7, Max = 159 },
                    new Measurement { Min = 159.1, Max = 161.5 }, new Measurement { Min = 161.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 138.8 }, new Measurement { Min = 138.9, Max = 142 }, new Measurement { Min = 142.1, Max = 159.2 },
                    new Measurement { Min = 159.3, Max = 161.7 }, new Measurement { Min = 161.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 139.2 }, new Measurement { Min = 139.3, Max = 142.4 }, new Measurement { Min = 142.5, Max = 159.5 },
                    new Measurement { Min = 159.6, Max = 162 }, new Measurement { Min = 162.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 139.5 }, new Measurement { Min = 139.6, Max = 142.7 }, new Measurement { Min = 142.8, Max = 159.7 },
                    new Measurement { Min = 159.8, Max = 162.2 }, new Measurement { Min = 162.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 12, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 140 }, new Measurement { Min = 140.1, Max = 143.1 }, new Measurement { Min = 143.2, Max = 160 },
                    new Measurement { Min = 160.1, Max = 162.5 }, new Measurement { Min = 162.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 140.4 }, new Measurement { Min = 140.5, Max = 143.4 }, new Measurement { Min = 143.5, Max = 160.2 },
                    new Measurement { Min = 160.3, Max = 162.7 }, new Measurement { Min = 162.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 140.7 }, new Measurement { Min = 140.8, Max = 143.7 }, new Measurement { Min = 143.8, Max = 160.4 },
                    new Measurement { Min = 160.5, Max = 162.9 }, new Measurement { Min = 163, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 141.1 }, new Measurement { Min = 141.2, Max = 144.1 }, new Measurement { Min = 144.2, Max = 160.6 },
                    new Measurement { Min = 160.7, Max = 163.1 }, new Measurement { Min = 163.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 141.5 }, new Measurement { Min = 141.6, Max = 144.4 }, new Measurement { Min = 144.5, Max = 160.8 },
                    new Measurement { Min = 160.9, Max = 163.3 }, new Measurement { Min = 163.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 141.8 }, new Measurement { Min = 141.9, Max = 144.7 }, new Measurement { Min = 144.8, Max = 161 },
                    new Measurement { Min = 161.1, Max = 163.5 }, new Measurement { Min = 163.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 142.2 }, new Measurement { Min = 142.3, Max = 145 }, new Measurement { Min = 145.1, Max = 161.2 },
                    new Measurement { Min = 161.3, Max = 163.7 }, new Measurement { Min = 163.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 142.5 }, new Measurement { Min = 142.6, Max = 145.3 }, new Measurement { Min = 145.4, Max = 161.4 },
                    new Measurement { Min = 161.5, Max = 163.9 }, new Measurement { Min = 164, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 142.9 }, new Measurement { Min = 143, Max = 145.6 }, new Measurement { Min = 145.7, Max = 161.5 },
                    new Measurement { Min = 161.6, Max = 164.1 }, new Measurement { Min = 164.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 143.2 }, new Measurement { Min = 143.3, Max = 145.8 }, new Measurement { Min = 145.9, Max = 161.7 },
                    new Measurement { Min = 161.8, Max = 164.2 }, new Measurement { Min = 164.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 143.5 }, new Measurement { Min = 143.6, Max = 146.1 }, new Measurement { Min = 146.2, Max = 161.9 },
                    new Measurement { Min = 162, Max = 164.4 }, new Measurement { Min = 164.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 143.8 }, new Measurement { Min = 143.9, Max = 146.4 }, new Measurement { Min = 146.5, Max = 162 },
                    new Measurement { Min = 162.1, Max = 164.5 }, new Measurement { Min = 164.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 13, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144 }, new Measurement { Min = 144.1, Max = 146.6 }, new Measurement { Min = 146.7, Max = 162.2 },
                    new Measurement { Min = 162.3, Max = 164.7 }, new Measurement { Min = 164.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144.3 }, new Measurement { Min = 144.4, Max = 146.9 }, new Measurement { Min = 147, Max = 162.3 },
                    new Measurement { Min = 162.4, Max = 164.8 }, new Measurement { Min = 164.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144.5 }, new Measurement { Min = 144.6, Max = 147 }, new Measurement { Min = 147.1, Max = 162.4 },
                    new Measurement { Min = 162.5, Max = 164.9 }, new Measurement { Min = 165, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144.6 }, new Measurement { Min = 144.7, Max = 147.2 }, new Measurement { Min = 147.3, Max = 162.5 },
                    new Measurement { Min = 162.6, Max = 165 }, new Measurement { Min = 165.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144.8 }, new Measurement { Min = 144.9, Max = 147.3 }, new Measurement { Min = 147.4, Max = 162.7 },
                    new Measurement { Min = 162.8, Max = 165.2 }, new Measurement { Min = 165.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 144.9 }, new Measurement { Min = 145, Max = 147.4 }, new Measurement { Min = 147.5, Max = 162.8 },
                    new Measurement { Min = 162.9, Max = 165.3 }, new Measurement { Min = 165.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.1 }, new Measurement { Min = 145.2, Max = 147.6 }, new Measurement { Min = 147.7, Max = 162.9 },
                    new Measurement { Min = 163, Max = 165.4 }, new Measurement { Min = 165.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.2 }, new Measurement { Min = 145.3, Max = 147.7 }, new Measurement { Min = 147.8, Max = 163 },
                    new Measurement { Min = 163.1, Max = 165.5 }, new Measurement { Min = 165.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.3 }, new Measurement { Min = 145.4, Max = 147.8 }, new Measurement { Min = 147.9, Max = 163.1 },
                    new Measurement { Min = 163.2, Max = 165.6 }, new Measurement { Min = 165.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.4 }, new Measurement { Min = 145.5, Max = 147.9 }, new Measurement { Min = 148, Max = 163.1 },
                    new Measurement { Min = 163.2, Max = 165.6 }, new Measurement { Min = 165.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.5 }, new Measurement { Min = 145.6, Max = 148 }, new Measurement { Min = 148.1, Max = 163.2 },
                    new Measurement { Min = 163.3, Max = 165.7 }, new Measurement { Min = 165.8, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.6 }, new Measurement { Min = 145.7, Max = 148.1 }, new Measurement { Min = 148.2, Max = 163.4 },
                    new Measurement { Min = 163.5, Max = 165.8 }, new Measurement { Min = 165.9, Max = 300 })
                },
                new GrowthByHeight{ Age = 14, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.7 }, new Measurement { Min = 145.8, Max = 148.2 }, new Measurement { Min = 148.3, Max = 163.4 },
                    new Measurement { Min = 163.5, Max = 165.9 }, new Measurement { Min = 166, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.8 }, new Measurement { Min = 145.9, Max = 148.3 }, new Measurement { Min = 148.4, Max = 163.5 },
                    new Measurement { Min = 163.6, Max = 166 }, new Measurement { Min = 166.1, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.8 }, new Measurement { Min = 145.9, Max = 148.3 }, new Measurement { Min = 148.4, Max = 163.6 },
                    new Measurement { Min = 163.7, Max = 166.1 }, new Measurement { Min = 166.2, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 145.9 }, new Measurement { Min = 146, Max = 148.4 }, new Measurement { Min = 148.5, Max = 163.7 },
                    new Measurement { Min = 163.8, Max = 166.2 }, new Measurement { Min = 166.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146 }, new Measurement { Min = 146.1, Max = 148.5 }, new Measurement { Min = 148.6, Max = 163.7 },
                    new Measurement { Min = 163.8, Max = 166.2 }, new Measurement { Min = 166.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.1 }, new Measurement { Min = 146.2, Max = 148.6 }, new Measurement { Min = 148.7, Max = 163.7 },
                    new Measurement { Min = 163.8, Max = 166.2 }, new Measurement { Min = 166.3, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.1 }, new Measurement { Min = 146.2, Max = 148.6 }, new Measurement { Min = 148.7, Max = 163.8 },
                    new Measurement { Min = 163.9, Max = 166.3 }, new Measurement { Min = 166.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.2 }, new Measurement { Min = 146.3, Max = 148.7 }, new Measurement { Min = 148.8, Max = 163.8 },
                    new Measurement { Min = 163.9, Max = 166.3 }, new Measurement { Min = 166.4, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.2 }, new Measurement { Min = 146.3, Max = 148.7 }, new Measurement { Min = 148.8, Max = 163.9 },
                    new Measurement { Min = 164, Max = 166.4 }, new Measurement { Min = 166.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.3 }, new Measurement { Min = 146.4, Max = 148.8 }, new Measurement { Min = 148.9, Max = 163.9 },
                    new Measurement { Min = 164, Max = 166.4 }, new Measurement { Min = 166.5, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.3 }, new Measurement { Min = 146.4, Max = 148.8 }, new Measurement { Min = 148.9, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.4 }, new Measurement { Min = 146.5, Max = 148.9 }, new Measurement { Min = 149, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 15, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.4 }, new Measurement { Min = 146.5, Max = 148.9 }, new Measurement { Min = 149, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.5 }, new Measurement { Min = 146.6, Max = 149 }, new Measurement { Min = 149.1, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.5 }, new Measurement { Min = 146.6, Max = 149 }, new Measurement { Min = 149.1, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.5 }, new Measurement { Min = 146.6, Max = 149 }, new Measurement { Min = 149.1, Max = 164 },
                    new Measurement { Min = 164.1, Max = 166.5 }, new Measurement { Min = 166.6, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.6 }, new Measurement { Min = 146.7, Max = 149.1 }, new Measurement { Min = 149.2, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.6 }, new Measurement { Min = 146.7, Max = 149.1 }, new Measurement { Min = 149.2, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.6 }, new Measurement { Min = 146.7, Max = 149.1 }, new Measurement { Min = 149.2, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.7 }, new Measurement { Min = 146.8, Max = 149.2 }, new Measurement { Min = 149.3, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.7 }, new Measurement { Min = 146.8, Max = 149.2 }, new Measurement { Min = 149.3, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.7 }, new Measurement { Min = 146.8, Max = 149.2 }, new Measurement { Min = 149.3, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.8 }, new Measurement { Min = 146.9, Max = 149.3 }, new Measurement { Min = 149.4, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.8 }, new Measurement { Min = 146.9, Max = 149.3 }, new Measurement { Min = 149.4, Max = 164.1 },
                    new Measurement { Min = 164.2, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 16, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.9 }, new Measurement { Min = 147, Max = 149.4 }, new Measurement { Min = 149.5, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 146.9 }, new Measurement { Min = 147, Max = 149.4 }, new Measurement { Min = 149.5, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147 }, new Measurement { Min = 147.1, Max = 149.5 }, new Measurement { Min = 149.6, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147 }, new Measurement { Min = 147.1, Max = 149.5 }, new Measurement { Min = 149.6, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.1 }, new Measurement { Min = 147.2, Max = 149.5 }, new Measurement { Min = 149.6, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.1 }, new Measurement { Min = 147.2, Max = 149.5 }, new Measurement { Min = 149.6, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.2 }, new Measurement { Min = 147.3, Max = 149.6 }, new Measurement { Min = 149.7, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.2 }, new Measurement { Min = 147.3, Max = 149.6 }, new Measurement { Min = 149.7, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.2 }, new Measurement { Min = 147.3, Max = 149.6 }, new Measurement { Min = 149.7, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 17, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 0, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 1, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 2, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 3, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 4, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 5, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 6, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 7, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 8, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 9, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 10, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                },
                new GrowthByHeight{ Age = 18, Month = 11, lookUpHeight = new LookUpHeight("1",
                    new Measurement { Min = 1, Max = 147.3 }, new Measurement { Min = 147.4, Max = 149.7 }, new Measurement { Min = 149.8, Max = 164.2 },
                    new Measurement { Min = 164.3, Max = 166.6 }, new Measurement { Min = 166.7, Max = 300 })
                }
            };
            return GrowthByHeight;
        }
    }
}