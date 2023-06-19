using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Requests
{
    public class IdentifyFileLanguageRequest
    {
        public byte[] File { get; set; }

        public string Filename { get; set; }
    }
}
