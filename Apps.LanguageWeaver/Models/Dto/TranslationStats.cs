using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Dto
{
    public class TranslationStats
    {
        [Display("Source word count")]
        public int inputWordCount { get; set; }

        [Display("Source character count")]
        public int inputCharCount { get; set; }

        [Display("Target word count")]
        public int translationWordCount { get; set; }

        [Display("Target character count")]
        public int translationCharCount { get; set; }
    }
}
