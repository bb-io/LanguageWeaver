using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Requests
{
    public class CreateInsightsForFileRequest
    {
        public byte[] File { get; set; }
        public string Filename { get; set; }


        [Display("Source language")]
        public string SourceLanguage { get; set; }
    }
}
