using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Parse.Ps4Disk
{
    internal class PS4DiskSettings : IParserSettings
    {

        public PS4DiskSettings(int Start, int End)
        {
            StartPoint = Start;
            EndPoint = End;
        }

        public string BaseUrl { get; set; } = "https://igroteka.club/playstation/ps4/diski-ps4";
        public string Prefix { get; set; } = "?page={CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
