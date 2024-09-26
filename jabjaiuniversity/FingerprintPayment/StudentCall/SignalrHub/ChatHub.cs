using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace FingerprintPayment.StudentCall.SignalrHub
{
    public class ChatHub : Hub
    {
        //private readonly IHubContext<ChatHub> _hub;

        public int SchoolID { get; set; }
        public ChatHub(int schoolid)
        {
            SchoolID = schoolid;
        }

        public void Hello()
        {
            Clients.All.hello();
        }


        //public void BroadcastInComing(string text)
        //{
        //    // Instantiate a client
        //    TextToSpeechClient client = TextToSpeechClient.Create();

        //    // Set the text input to be synthesized.
        //    SynthesisInput input = new SynthesisInput
        //    {
        //        Text = text
        //    };

        //    // Build the voice request, select the language code ("en-US"),
        //    // and the SSML voice gender ("neutral").
        //    VoiceSelectionParams voice = new VoiceSelectionParams
        //    {
        //        LanguageCode = "th-TH",
        //        SsmlGender = SsmlVoiceGender.Female,
        //    };

        //    // Select the type of audio file you want returned.
        //    AudioConfig config = new AudioConfig
        //    {
        //        AudioEncoding = AudioEncoding.Mp3
        //    };

        //    // Perform the Text-to-Speech request, passing the text input
        //    // with the selected voice parameters and audio file type
        //    var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
        //    {
        //        Input = input,
        //        Voice = voice,
        //        AudioConfig = config
        //    });

        //    var data = response.AudioContent.ToBase64();

        //    Clients.Group("School" + SchoolID).BroadcastInComing(data);
        //}
    }
}