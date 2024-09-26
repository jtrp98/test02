using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Amazon.Runtime.Internal.Transform;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JWT;
using JWT.Serializers;
using MasterEntity;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SchoolBright.DTO.DTO;
using JabjaiMainClass.Models;
using FingerprintPayment.Class;

namespace FingerprintPayment.App_Code
{
    public class StdCallingHub : Hub
    {
        private enum CallStatus : byte
        {
            Create = 1,
            Annouce = 2,
            Complete = 3
        }

        private enum LogType : byte
        {
            OnConnect = 1,
            OnReconnect = 2,
            OnDisconect = 3,
            OnError = 4,
        }

        static List<UserState> _listUser = new List<UserState>();
        static string CONNECTION_ID = "";
        public class UserState
        {
            // public string ContextId { get; set; }
            public int SID { get; set; }
            public int SchoolID { get; set; }
            public string ConnectionId { get; internal set; }
            public string IPAddress { get; internal set; }
            public DateTime Time { get; internal set; }
        }

        public class ModelStudent
        {
            public int SchoolID { get; set; }
            public string NickName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Code { get; set; }
            public string Receiver { get; set; }
            public int sID { get; internal set; }
            public int? nTermSubLevel2 { get; internal set; }
            public string Level { get; internal set; }
            public string Level1 { get; internal set; }
            public string Level2 { get; internal set; }
            public string Img { get; internal set; }
            public string Title { get; internal set; }
            public string Sound { get; internal set; }
            public string FullName { get; internal set; }
            public byte Status { get; internal set; }
            public string Time { get; internal set; }
            public TimeSpan tTime { get; internal set; }

            public TimeSpan? tTime1 { get; internal set; }
            public TimeSpan? tTime2 { get; internal set; }
            public TimeSpan? tTime3 { get; internal set; }
        }

    }
}