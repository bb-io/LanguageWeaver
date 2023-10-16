using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Dto
{
    public class QualityEstimationDto
    {
        [Display("Percentage poor")]
        public int? poor { get; set; }

        [Display("Percentage adequate")]
        public int? adequate { get; set; }

        [Display("Percentage good")]
        public int? good { get; set; }
    }
}
