using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace theWorld.Services
{
    using System.Diagnostics;

    public class DebugMailService:IMailService
    {
        public bool SendMail(string to, string @from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail:To:{to},Subject:{subject}");
            return true;
        }
    }
}
