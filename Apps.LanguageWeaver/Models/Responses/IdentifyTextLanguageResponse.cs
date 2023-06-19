using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Responses
{
    public class IdentifyTextLanguageResponse
    {
        public List<Language> Languages { get; set; }
        public List<Script> Scripts { get; set; }
    }

    public class Language
    {
        public string Code { get; set; }
        public string Name { get; set; }

        [Display("Language tag")]
        public string LanguageTag { get; set; }
        public double Score { get; set; }
    }

    public class Script
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
    }
}
