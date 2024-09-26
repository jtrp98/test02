
using Google.Cloud.TextToSpeech.V1;
using JabjaiEntity.DB;
using MasterEntity;
using Microsoft.AspNet.SignalR;
using System;

using System.Linq;

using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/StudentCall")]
    public class StudentCallController : ApiController
    {
        //private readonly IHubContext<ChatHub> _hub;
        //public JWTToken.userData userData;
        //public StudentCallController()
        //{
        //    JWTToken token = new JWTToken();
        //    userData = new JWTToken.userData();
        //    if (token.CheckToken(HttpContext.Current))
        //    {
        //        userData = token.getTokenValues(HttpContext.Current);
        //    }
        //}


        [Route("SetAnnouncement")]
        [HttpGet]
        public IHttpActionResult SetAnnouncement(int schoolId, int sid)
        {
            //string url = "https://dev.schoolbright.co/";
            var hub = GlobalHost.ConnectionManager.GetHubContext("StdCallingHub");

            var std = new ModelStudent();

            using (var _ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolId)))
            {
                std = (from a in _ctx.TUsers.Where(o => o.SchoolID == schoolId && o.sID == sid)
                       from b in _ctx.TTermSubLevel2.Where(o => o.SchoolID == schoolId && o.nTermSubLevel2 == a.nTermSubLevel2)
                       from c in _ctx.TSubLevels.Where(o => o.SchoolID == schoolId && o.nTSubLevel == b.nTSubLevel)
                       from d in _ctx.TTitleLists.Where(o => o.SchoolID == schoolId && o.nTitleid + "" == a.sStudentTitle).DefaultIfEmpty()

                       select new ModelStudent
                       {
                           sID = a.sID,
                           nTermSubLevel2 = a.nTermSubLevel2,
                           NickName = a.sNickName,
                           FirstName = a.sName,
                           LastName = a.sLastname,
                           Level1 = c.fullName,
                           Level2 = b.nTSubLevel2,
                           Code = a.sStudentID,
                           Receiver = "",
                           Img = a.sStudentPicture,
                           Title = d != null ? d.titleDescription : a.sStudentTitle,
                       })
                     .FirstOrDefault();

                if (std == null) return Json(new { status = "fail", msg = "invalid data" });
                std.Level1 = std.Level1.Replace("ศึกษาปีที่", "")
                       .Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส")
                       .Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช");
            }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var sound = dbmaster.TSound_Student
                    .FirstOrDefault(o => o.SchoolID == schoolId && o.sID == std.sID && o.nTermSubLevel2 == std.nTermSubLevel2 && o.IsDel == false);

                if (sound == null)
                {
                    sound = new TSound_Student();

                    //setting first time , get sound from google tts
                    var keyPath = HttpContext.Current.Server.MapPath("/StudentCall/GoogleKey/key.json");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);

                    TextToSpeechClient client = TextToSpeechClient.Create();

                    // Build the voice request, select the language code ("en-US"),
                    // and the SSML voice gender ("neutral").
                    VoiceSelectionParams voice = new VoiceSelectionParams
                    {
                        LanguageCode = "th-TH",
                        SsmlGender = SsmlVoiceGender.Female,
                    };

                    // Select the type of audio file you want returned.
                    AudioConfig config = new AudioConfig
                    {
                        AudioEncoding = AudioEncoding.Mp3,
                        SpeakingRate = 0.9d,
                        Pitch = -0.5d,
                    };

                    SynthesisInput input = new SynthesisInput
                    {
                        Text = $"ชั้น {std.Level1} ทับ {std.Level2} น้อง {std.NickName} {std.FirstName} "
                    };


                    var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
                    {
                        Input = input,
                        Voice = voice,
                        AudioConfig = config
                    });

                    sound.Base64Sound = response.AudioContent.ToBase64();
                    sound.Created = DateTime.Now;
                    sound.Modified = DateTime.Now;
                    sound.IsDel = false;
                    sound.nTermSubLevel2 = std.nTermSubLevel2.Value;
                    sound.sID = std.sID;
                    sound.SchoolID = schoolId;

                    //insert to db
                    dbmaster.TSound_Student.Add(sound);
                    dbmaster.SaveChanges();
                }
                //else
                //{
                //    //get from db
                //}

                hub.Clients.Group("S" + schoolId)
                    .SendAnnouncement(new
                    {
                        sID = std.sID,
                        Sound = sound.Base64Sound,
                        NickName = std.NickName + "",
                        FullName = $"{std.Title} {std.FirstName} {std.LastName} ",
                        Level = $"{std.Level1}/{std.Level2}",
                        Code = std.Code,
                        Img = std.Img,
                        Time = DateTime.Now.ToString("HH:mm:ss"),
                        Reciever = ""
                    }
                );
            }

            //var h = new ChatHub(schoolId);
            //h.BroadcastInComing("ป6 ชื่อ แม็ก อับดุลมูฮัยมิน เจ๊ะหลง");

            // Write the binary AudioContent of the response to an MP3 file.
            //using (Stream output = File.Create("output.mp3"))
            //{
            //    response.AudioContent.WriteTo(output);
            //    Console.WriteLine($"Audio content written to file 'sample.mp3'");
            //}

            //var ms = new MemoryStream();
            //response.AudioContent.CopyTo()
            //var bytes = ms.ToArray();
            //ttsresp.Audio = ms.ToArray();

            //  await _hub.Clients.Group("SCHOOL" + schoolId).Clients.//.SendAsync("ReceiveMessage", data);
            //await _hub.Clients.All.SendAsync("ReceiveMessage", user, message);
            //await _hub.Clients.All.InvokeAsync("x");
            return Json(new { status = "ok" });
        }

        public class ModelStudent
        {
            public string NickName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Code { get; set; }
            public string Receiver { get; set; }
            public int sID { get; internal set; }
            public int? nTermSubLevel2 { get; internal set; }
            public string Level1 { get; internal set; }
            public string Level2 { get; internal set; }
            public string Img { get; internal set; }
            public string Title { get; internal set; }
        }
    }
}
