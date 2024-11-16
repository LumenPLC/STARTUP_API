using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Google_Drive
{
    public class Credential
    {
        public string USER_NAME { get; set; }
        public string APPLICATION_NAME { get; set; }
        public string CLIENT_ID { get; set; }
        public string CLIENT_SECRET { get; set; }
        public string REFRESH_TOKEN { get; set; }
        public string ACCESS_TOKEN { get; set; }
    }
}
