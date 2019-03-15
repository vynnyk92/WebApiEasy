using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailFrom = Startup.configuration["mailSettings:mailFrom"];
        private string _mailTo = Startup.configuration["mailSettings:mailTo"];

        public void Send(string subj, string body)
        {
            Debug.WriteLine($"From {_mailFrom}. To {_mailTo}. With cloud mail service.");
            Debug.WriteLine($"{subj}");
            Debug.WriteLine($"{body}");

        }
    }

        public class LocalMailService: IMailService
    { 
        private string _mailFrom = "s@gmail.com";
        private string _mailTo = "s@i.ua";

        public void Send(string subj, string body)
        {
            Debug.WriteLine($"From {_mailFrom}. To {_mailTo}");
            Debug.WriteLine($"{subj}");
            Debug.WriteLine($"{body}");

        }
    }

    public interface IMailService
    {
        void Send(string subj, string body);
    }
}
