using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Models
{
    public enum Mode
    {
        Staging,
        Live
    }
    public class ApiConfiguration
    {
        public Mode Mode { get; set; }
        public string Company { get; set; }
        public string BaseUrlLive { get; set; }
        public string BaseUrlStaging { get; set; }
        public string TapSecretKeyLive { get; set; }
        public string TapSecretKeyStaging { get; set; }
        public string TapSecretKey { get { return Mode == Mode.Live ? TapSecretKeyLive : TapSecretKeyStaging; } }
        public string BaseUrl { get { return Mode == Mode.Live ? BaseUrlLive : BaseUrlStaging; } }
        public string PostEndPoint { get; set; }
        public string RedirectEndPoint { get; set; }
    }
}
