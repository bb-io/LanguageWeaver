using Apps.LanguageWeaver.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageWeaver.Models.Responses.Translation
{
    public class TranslateFileWithQeResponse
    {
        public File File { get; set; }
        public QualityEstimationDto Quality { get; set; }
        public TranslationStats Stats { get; set; }
    }
}
