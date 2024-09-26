using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Handlers.AspNet;
using System.Configuration;
using System.Web;

namespace FingerprintPayment.Class
{
    public class AWSXRay : HttpApplication
    {
        public AWSXRayRecorder Register()
        {
            string XRayWebApp = ConfigurationManager.AppSettings["XRayWebApp"];
            ////HttpApplication application = HttpContext.Current.ApplicationInstance;
            //AWSXRayASPNET.RegisterXRay(this, XRayWebApp); // default name of the web app
            //string url = HttpContext.Current.Request.Url.Host;
            HttpContext context = HttpContext.Current;

            AWSXRayRecorder recorder = AWSXRayRecorder.Instance;
            if (context != null && context.Items.Contains(AWSXRayASPNET.XRayEntity))
            {
                Segment requestSegment = (Segment)context.Items[AWSXRayASPNET.XRayEntity];
                requestSegment.Name = XRayWebApp;
                recorder.SetEntity(requestSegment);
            }

            var traceId = TraceId.NewId();
            //Segment requestSegment = (Segment)context.Items[AWSXRayASPNET.XRayEntity];
            //recorder.SetEntity(requestSegment);
            //recorder.AddHttpInformation(XRayWebApp,)
            if (XRayWebApp != null)
                recorder.BeginSegment(XRayWebApp, traceId);

            return recorder;
        }

        public override void Init()
        {
            base.Init();
            string XRayWebApp = ConfigurationManager.AppSettings["XRayWebApp"];
            AWSXRayASPNET.RegisterXRay(this, XRayWebApp); // default name of the web app
        }
    }
}