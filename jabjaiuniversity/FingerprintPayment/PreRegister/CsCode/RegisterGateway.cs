using FingerprintPayment.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.CsCode
{
    public class RegisterGateway : System.Web.UI.Page
    {
        private string lettersShuffle = "JabJaiEntities";
        protected string LettersShuffle { get { return lettersShuffle; } }

        protected override void OnLoad(EventArgs e)
        {
            if (Session["RegisterOnlineEntities"] == null)
            {
                Response.Redirect("RegisterStart.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static string GetLettersShuffle()
        {
            return "JabJaiEntities";
        }
    }
}