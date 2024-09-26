using MasterEntity;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentCall.SignalrHub
{
    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            //Debug.WriteLine("=> Exception " + exceptionContext.Error.Message);
            //if (exceptionContext.Error.InnerException != null)
            //{
            //    Debug.WriteLine("=> Inner Exception " + exceptionContext.Error.InnerException.Message);
            //}

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                dbmaster.TStudentCall_Log.Add(new TStudentCall_Log
                {
                    LogDate = DateTime.Now,
                    LogText = $"OnIncomingError {exceptionContext.Error.Message}",
                });

                dbmaster.SaveChanges();
            }


            base.OnIncomingError(exceptionContext, invokerContext);

        }
    }
}